// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2014 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Configuration.LoadFactory.Products
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Xml.Linq;
  using System.Xml.Schema;

  using Declarations;
  using Definitions;
  using Fbb.Output;
  using Fbb.Util.IO;
  using Mediator;
  using Mediator.Configuration;
  using Mediator.Configuration.Declarations;
  using Mediator.Configuration.Definitions;
  using Mediator.Exceptions;
  using Mediator.Exceptions.Configuration;
  using Mediator.Identifiers;


  internal class XmlConfigLoaderProduct : AbstractConfigLoaderProduct
  {
    #region Language Declaration Constants .....................................
    private const string LANGUAGE_DECLARATIONS = @"LanguageDeclarations";
    private const string LANGUAGE_DECLARATION  = @"LanguageDeclaration";
    private const string LANGUAGE_EXTENSION    = @"Extension";
    #endregion .................................. Language Declaration Constants


    #region CategoryDeclarations & Rules Declaration Constants .................
    private const string CATEGORIES            = @"CategoryDeclarations";
    private const string CATEGORY_DECLARATION  = @"CategoryDeclaration";
    private const string CATEGORY_DESCRIPTION  = @"Description";
    private const string RULES                 = @"RuleDeclarations";
    private const string RULE_DECLARATION      = @"RuleDeclaration";
    private const string RULE_DESCRIPTION      = @"Description";
    private const string RULE_SEVERITY         = @"Severity";
    private const string RULE_EXPRESSION       = @"Expression";
    private const string RULE_LANGUAGE         = @"Language";
    #endregion .............. CategoryDeclarations & Rules Declaration Constants


    #region Project Definition Constants .......................................
    private const string PROJECT_DEFINITION  = @"ProjectDefinition";
    private const string PROJECT_DIRECTORIES = @"Directories";
    private const string PROJECT_FILES       = @"Files";
    private const string PROJECT_INCLUDE     = @"Include";
    private const string PROJECT_EXCLUDE     = @"Exclude";
    private const string PROJECT_DIRECTORY   = @"Directory";
    private const string PROJECT_FILE        = @"File";
    private const string PROJECT_PATH        = @"Path";
    private const string PROJECT_CATEGORIES  = @"CategoryDefinitions";
    private const string PROJECT_CATEGORY    = @"CategoryDefinition";
    private const string PROJECT_RULE        = @"RuleDefinition";

    private const string ENABLED             = @"Enabled";
    private const string ID                  = @"Id";
    private const string NAME                = @"Name";
    private const string REF_ID              = @"RefId";
    private const string REF_NAME            = @"RefName";
    #endregion .................................... Project Definition Constants


    #region Instance Variables .................................................
    private readonly object _lockSchemaLoad        = new object();
    private readonly object _lockConfigurationFile = new object();
    #endregion .............................................. Instance Variables


    #region Auto properties ....................................................
    private string              RulesConfigFilePath      { get; set; }
    private string              RulesConfigSchemaPath    { get; set; }
    private bool                IsConfigFileValid        { get; set; }
    private ValidationEventArgs ValidationEventArguments { get; set; }
    private XmlSchemaSet        RulesSchema              { get; set; }
    private XDocument           Doc                      { get; set; }
    #endregion ................................................. Auto properties

    
    #region Properties .........................................................
    protected override Zone Log
    {
      get { return _log ?? Out.ZoneFactory(Ids.REGION_CONFIG, GetType().Name); }
    }
    #endregion ...................................................... Properties


    #region Construction .......................................................
    public XmlConfigLoaderProduct(ConfigManager manager) : base(manager)
    {
      Doc               = null;
      IsConfigFileValid = true;


      string currentDirectory = DirHandler.Instance.CurrentDirectory;
      RulesConfigFilePath     = Path.Combine(currentDirectory, Ids.CONFIG_DIR, Ids.RULES_CONFIG_FILE);
      RulesConfigSchemaPath   = Path.Combine(currentDirectory, Ids.CONFIG_DIR, Ids.RULES_CONFIG_SCHEMA_FILE);

      // Check the for existence of the configuration file...
      if (string.IsNullOrWhiteSpace(RulesConfigFilePath) || !File.Exists(RulesConfigFilePath))
      {
        // Copy default configuration file 'rules.config' from the 'Repository\examples\' to current execution dir...
        FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.EXAMPLES_DIR, Ids.RULES_CONFIG_FILE), Path.Combine(currentDirectory, Ids.CONFIG_DIR, Ids.RULES_CONFIG_FILE));

        string warnTxt = string.Format("Unable to find the configuration file. Expected to find it here: {0}. Created default file for entering settings in. Change the content of the configuration file {1} and run the application again.", RulesConfigFilePath, Ids.RULES_CONFIG_FILE);
        Log.Warning(warnTxt);

        throw new InitializationException(this, -1, warnTxt);
      }

      // Check for existence of the schema file for the configuration file...
      if (string.IsNullOrWhiteSpace(RulesConfigSchemaPath) || !File.Exists(RulesConfigSchemaPath))
      {
        string s = string.Format("Unable to find the schema validation file for the configuration file. Expected to find the schema validation file here: {0}.", RulesConfigSchemaPath);
        Log.Error(s);

        throw new InitializationException(this, -1, s);
      }

      // Load the configuration file and place the content of it in a XDocument...
      Doc = LoadConfigurationFile();
      if (Doc == null)
        throw new NullReferenceException("Failed to load the configuration file.");
    }
    #endregion .................................................... Construction


    #region Load Configurations ................................................

    #region Load schema
    private XmlSchemaSet LoadSchema(string path)
    {
      lock (_lockSchemaLoad)
      {
        if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
        if (!File.Exists(path)) throw new ArgumentException(string.Format("Invalid argument 'path' - unable to find file '{0}'.", path));

        XmlSchemaSet schemaSet = new XmlSchemaSet();
        schemaSet.Add(null, path);

        return schemaSet;
      }
    }
    #endregion


    #region Load rules configuration file
    private XDocument LoadConfigurationFile()
    {
      lock (_lockConfigurationFile)
      {
        // Prepare for a new schema validation loop...
        ValidationEventArguments = null;
        IsConfigFileValid = true;

        // Validate input...
        if (string.IsNullOrWhiteSpace(RulesConfigFilePath))
          throw new InitializationException("Property 'RulesConfigFilePath' must be set before calling this method.");
        if (!File.Exists(RulesConfigFilePath))
          throw new InitializationException(string.Format("Invalid value found in property 'RulesConfigFilePath' - unable to find file '{0}'.", RulesConfigFilePath));

        try
        {
          // Load platform configuration file and validate it against it's associated schema file.
          XDocument doc = XDocument.Load(RulesConfigFilePath);
          doc.Validate(LoadSchema(RulesConfigSchemaPath), ValidationCallBack, false);

          if (!IsConfigFileValid)
          {
            // If we end up here it is because the schema validation of the platform configuration file resulted in errors.
            throw new ConfigurationComponentException(string.Format("Invalid configuration file '{0}', error during schema validation.", RulesConfigFilePath), (ValidationEventArguments != null) ? ValidationEventArguments.Exception : null);
          }

          Doc = doc;
          return Doc;
        }
        catch (Exception e)
        {
          throw new ConfigurationComponentException(string.Format("Failed to load rules from configuration file '{0}'.", RulesConfigFilePath), e);
        }
      }
    }
    #endregion


    #region Load Project Definitions
    private void LoadProjectDefinitions(XDocument doc)
    {
      lock (_lockConfigurationFile)
      {
        // Validate input...
        if (doc == null)
          throw new ArgumentNullException("doc");

        List<IProjectDefinition> projectDefinitions = new List<IProjectDefinition>();

        foreach (XElement projDefinition in doc.Descendants(PROJECT_DEFINITION))
        {
          ProjectDefinition projectDefinition = new ProjectDefinition();

          // Load the attributes: "Enabled" and "Name"...
          projectDefinition.Id = int.Parse(projDefinition.Attribute(XName.Get(ID)).Value);
          projectDefinition.Enabled = bool.Parse(projDefinition.Attribute(XName.Get(ENABLED)).Value); // XSD should have validated input...
          projectDefinition.Name = projDefinition.Attribute(XName.Get(NAME)).Value;


          // Load the directories to "Include" and the directories to "Exclude"...
          XElement includeDirs = projDefinition.Descendants(PROJECT_DIRECTORIES).Descendants(PROJECT_INCLUDE).First();
          XElement excludeDirs = projDefinition.Descendants(PROJECT_DIRECTORIES).Descendants(PROJECT_EXCLUDE).First();
          projectDefinition.Directories.AddRange(LoadDirectoryDefinitions(includeDirs));
          projectDefinition.ExcludedDirectories.AddRange(LoadDirectoryDefinitions(excludeDirs));


          // Load the directories to "Include" and the directories to "Exclude"...
          XElement includeFiles = projDefinition.Descendants(PROJECT_FILES).Descendants(PROJECT_INCLUDE).First();
          XElement excludeFiles = projDefinition.Descendants(PROJECT_FILES).Descendants(PROJECT_EXCLUDE).First();
          projectDefinition.Files.AddRange(LoadFileDefinitions(includeFiles));
          projectDefinition.ExcludedFiles.AddRange(LoadFileDefinitions(excludeFiles));


          // Load the category definitions...
          foreach (XElement category in projDefinition.Descendants(PROJECT_CATEGORY))
          {
            CategoryDefinition categoryDefinition = LoadCategoryDefinition(category);
            categoryDefinition.ParentDefinition = projectDefinition;
            projectDefinition.Categories.Add(categoryDefinition.CategoryDeclarationReferenceId, categoryDefinition);

            // Update 'Category Definition' statistics counters...
            ProxyHome.Instance.RetrieveStatisticsProxy(ConfigKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.TotalCategoryDefinitions);
            if (categoryDefinition.Enabled)
              ProxyHome.Instance.RetrieveStatisticsProxy(ConfigKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.ActiveCategoryDefinitions);
          }

          Manager.Definitions.Projects.Add(projectDefinition.Id, projectDefinition);

          // Update 'Project Definition' statistics counters...
          ProxyHome.Instance.RetrieveStatisticsProxy(ConfigKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.TotalProjectDefinitions);
          if (projectDefinition.Enabled)
            ProxyHome.Instance.RetrieveStatisticsProxy(ConfigKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.ActiveProjectDefinitions);
        }
      }
    }
    #endregion


    #region Load Language Declarations
    private void LoadLanguageDeclarations(XDocument doc)
    {
      lock (_lockConfigurationFile)
      {
        // Validate input...
        if (doc == null)
          throw new ArgumentNullException("doc");

        foreach (XElement languageDeclaration in doc.Descendants(LANGUAGE_DECLARATION))
        {
          LanguageDeclaration declaration = new LanguageDeclaration();

          declaration.Id        = int.Parse(languageDeclaration.Attribute(XName.Get(ID)).Value);
          declaration.Name      = languageDeclaration.Element(XName.Get(NAME)).Value;
          declaration.Extension = languageDeclaration.Element(XName.Get(LANGUAGE_EXTENSION)).Value;

          Manager.Declarations.Languages.Add(declaration.Id, declaration);

          // Update 'Language Declaration' statistics counters...
          ProxyHome.Instance.RetrieveStatisticsProxy(ConfigKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.LanguageDeclarations);
        }
      }
    }
    #endregion


    #region Load Category Declarations
    private void LoadCategoryDeclarations(XDocument doc)
    {
      lock (_lockConfigurationFile)
      {
        // Validate input...
        if (doc == null)
          throw new ArgumentNullException("doc");

        foreach (XElement categoryDeclaration in doc.Descendants(CATEGORY_DECLARATION))
        {
          CategoryDeclaration cd = new CategoryDeclaration();
          cd.Id = int.Parse(categoryDeclaration.Attribute(XName.Get(ID)).Value);
          cd.Name = categoryDeclaration.Attribute(XName.Get(NAME)).Value;
          cd.Description = categoryDeclaration.Element(XName.Get(CATEGORY_DESCRIPTION)).Value;
          cd.Rules = new Dictionary<int, IRuleDeclaration>();

          foreach (XElement ruleDeclaration in categoryDeclaration.Descendants(XName.Get(RULE_DECLARATION)))
          {
            RuleDeclaration rd = new RuleDeclaration();
            rd.ParentDeclaration = cd;
            rd.Id = int.Parse(ruleDeclaration.Attribute(XName.Get(ID)).Value);
            rd.Name = ruleDeclaration.Attribute(XName.Get(NAME)).Value;
            rd.Description = ruleDeclaration.Descendants(XName.Get(RULE_DESCRIPTION)).Single().Value;
            rd.Severity = RuleSeverityMapper.String2RuleSeverity(ruleDeclaration.Descendants(XName.Get(RULE_SEVERITY)).Single().Value);
            rd.Expression = ruleDeclaration.Descendants(XName.Get(RULE_EXPRESSION)).Single().Value;

            IEnumerable<XElement> languages = ruleDeclaration.Descendants(XName.Get("Language"));
            foreach (XElement l in languages)
            {
              int id = int.Parse(l.Attribute(XName.Get("RefId")).Value);
              string name = l.Attribute(XName.Get("RefName")).Value;
              rd.Languages.Add(id, Manager.Declarations.Languages[id]);
            }

            cd.Rules.Add(rd.Id, rd);

            // Update 'Rule Declaration' statistics counters...
            ProxyHome.Instance.RetrieveStatisticsProxy(ConfigKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.RuleDeclarations);
          }

          Manager.Declarations.Categories.Add(cd.Id, cd);

          // Update 'Category Declaration' statistics counters...
          ProxyHome.Instance.RetrieveStatisticsProxy(ConfigKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.CategoryDeclarations);
        }
      }
    }
    #endregion


    #region Definition Load Helpers
    private List<DirectoryDefinition> LoadDirectoryDefinitions(XElement includeElement)
    {
      List<DirectoryDefinition> list = new List<DirectoryDefinition>();

      foreach (XElement dir in includeElement.Descendants(PROJECT_DIRECTORY))
      {
        DirectoryDefinition directoryDefinition = new DirectoryDefinition();

        directoryDefinition.Enabled = bool.Parse(dir.Attribute(XName.Get(ENABLED)).Value); // XSD should have validated input...
        directoryDefinition.Path = dir.Attribute(XName.Get(PROJECT_PATH)).Value;

        list.Add(directoryDefinition);
      }
      return list;
    }

    private List<FileDefinition> LoadFileDefinitions(XElement includeElement)
    {
      List<FileDefinition> list = new List<FileDefinition>();

      foreach (XElement file in includeElement.Descendants(PROJECT_FILE))
      {
        FileDefinition fileDefinition = new FileDefinition();

        fileDefinition.Enabled = bool.Parse(file.Attribute(XName.Get(ENABLED)).Value); // XSD should have validated input...
        fileDefinition.Path = file.Attribute(XName.Get(PROJECT_PATH)).Value;

        list.Add(fileDefinition);
      }
      return list;
    }

    private CategoryDefinition LoadCategoryDefinition(XElement categoryElement)
    {
      CategoryDefinition categoryDefinition = new CategoryDefinition();
      categoryDefinition.Id = int.Parse(categoryElement.Attribute(XName.Get(ID)).Value);
      categoryDefinition.Enabled = bool.Parse(categoryElement.Attribute(XName.Get(ENABLED)).Value); // XSD should have validated input...
      categoryDefinition.CategoryDeclarationReferenceId = int.Parse(categoryElement.Attribute(XName.Get(REF_ID)).Value);
      categoryDefinition.CategoryDeclarationReferenceName = categoryElement.Attribute(XName.Get(REF_NAME)).Value;

      foreach (XElement rule in categoryElement.Descendants(PROJECT_RULE))
      {
        RuleDefinition ruleDefinition = new RuleDefinition();
        ruleDefinition.ParentDefinition = categoryDefinition;
        ruleDefinition.Id = int.Parse(rule.Attribute(XName.Get(ID)).Value);
        ruleDefinition.Enabled = bool.Parse(rule.Attribute(XName.Get(ENABLED)).Value); // XSD should have validated input...
        ruleDefinition.RuleDeclarationReferenceId = int.Parse(rule.Attribute(XName.Get(REF_ID)).Value);
        ruleDefinition.RuleDeclarationReferenceName = rule.Attribute(XName.Get(REF_NAME)).Value;

        categoryDefinition.Rules.Add(ruleDefinition.RuleDeclarationReferenceId, ruleDefinition);

        // Update 'Rule Definition' statistics counters...
        ProxyHome.Instance.RetrieveStatisticsProxy(ConfigKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.TotalRuleDefinitions);
        if (ruleDefinition.Enabled)
          ProxyHome.Instance.RetrieveStatisticsProxy(ConfigKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.ActiveRuleDefinitions);
      }
      return categoryDefinition;
    }
    #endregion
    #endregion ............................................. Load Configurations


    #region Schema validation event handler ....................................
    private void ValidationCallBack(object sender, ValidationEventArgs e)
    {
      switch (e.Severity)
      {
        case XmlSeverityType.Error: { Log.Error(e.Message, e.Exception); break; }
        case XmlSeverityType.Warning: { Log.Warning(e.Message); break; }
        default: { break; }
      }

      ValidationEventArguments = e;
      IsConfigFileValid = false;
    }
    #endregion ................................. Schema validation event handler
    

    #region Interface IConfigLoaderProduct impl. ...............................
    public override void LoadLanguageDeclarations()
    {
      LoadLanguageDeclarations(Doc);
    }

    public override void LoadCategoryDeclarations()
    {
      LoadCategoryDeclarations(Doc);
    }

    public override void LoadProjectDefinitions()
    {
      LoadProjectDefinitions(Doc);
    }
    #endregion ............................ Interface IConfigLoaderProduct impl.
  }
}