// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Output
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Xml;

  using Fbb.Output;
  using Fbb.Util.IO;
  using Mediator;
  using Mediator.Configuration;
  using Mediator.Configuration.Declarations;
  using Mediator.Configuration.Definitions;
  using Mediator.Engine;
  using Mediator.Engine.Summaries;
  using Mediator.Exceptions.Output;
  using Mediator.Identifiers;
  using Mediator.Matches;
  using Mediator.Statistics;


  /// <summary>
  /// </summary>
  internal class WebOutput : AbstractOutput
  {
    #region Instance Variable(s) ...............................................
    private readonly Zone _Log = null;
    #endregion ............................................ Instance Variable(s)


    #region Auto properties ....................................................
    private List<XmlNode> ContainerOfRuleNodes  { get; set; } 
    private List<XmlNode> ContainerOfMatchNodes { get; set; }
    private string        OutputRootDir         { get; set; }
    #endregion ................................................. Auto properties


    #region Properties .........................................................
    private Zone Log
    {
      get { return _Log ?? Out.ZoneFactory(Ids.REGION_OUTPUT, GetType().Name); }
    }
    #endregion ...................................................... Properties


    #region Construction .......................................................
    /// <summary>
    /// </summary>
    /// <param name="home"></param>
    public WebOutput() : base()
    {
      ContainerOfRuleNodes  = new List<XmlNode>();
      ContainerOfMatchNodes = new List<XmlNode>();
    }
    #endregion .................................................... Construction


    #region Interface IOutput impl. ............................................
    /// <summary>
    /// This is the entry point of the xml generation. This is the entry point
    /// of the xml generation. This is the entry point of the xml generation.
    /// This is the entry point of the xml generation.
    /// </summary>
    /// <param name="outputRootDir"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="OutputComponentException"></exception>
    public override void GenerateOutput(string outputRootDir)
    {
      #region Input validation
      if (string.IsNullOrWhiteSpace(outputRootDir))
        throw new ArgumentNullException("outputRootDir");

      if (!Directory.Exists(outputRootDir))
        throw new ArgumentException(string.Format("Unable to find directory {0}.", outputRootDir));
      #endregion


      #region Update temporary auto properties
      OutputRootDir = outputRootDir;
      #endregion


      #region Creating directories
      try
      {
        CreateDirectories();
      }
      catch (Exception e)
      {
        const string s = "Failed to create directories while generating the report.";
        Log.Error(s, e);
        throw new OutputComponentException(s, e);
      }
      #endregion


      #region Creating files
      try
      {
        CopyFiles();
      }
      catch (Exception e)
      {
        const string s = "Failed to create one or more files while generating the report.";
        Log.Error(s, e);
        throw new OutputComponentException(s, e);
      }
      #endregion


      #region Generate 'summary.xml' file
      CreateSummaryFile();
      #endregion


      #region Creating languages declaration file
      CreateLanguageDeclarationFile();
      #endregion


      #region Creating category declaration file
      CreateCategoryDeclarationFile();
      #endregion


      #region Creating rule declaration file
      CreateRuleDeclarationFile();
      #endregion


      #region Creating project definition file
      CreateProjectDefinitionsFile();
      #endregion


      #region Creating category definition file
      CreateCategoryDefinitionsFile();
      #endregion


      #region Creating project definition file
      CreateRuleDefinitionsFile();
      #endregion


      #region Generate the 'toc.xml' file and the 'subtoc.xml' files
      //XmlDocument tocDoc = new XmlDocument();
      //TocContent(outputRootDir, tocDoc);
      //tocDoc.Save(Path.Combine(outputRootDir, Ids.OUTPUT_DIR, Ids.TOC_FILE));
      #endregion


      #region Generate the 'allrules.xml' file
      GenerateAllFilesFoundDuringSearch();
      //GenerateAllRulesFile(outputRootDir);
      CreateAllMatchesFile();
      CreateInfofWarnCritFatalMatchesFile();
      #endregion
    }

    #endregion ......................................... Interface IOutput impl.


    #region Summary Content XML helpers ........................................
    private void CreateSummaryFile()
    {
      XmlNode node;
      XmlDocument doc = XmlFactory.CreateXmlFile(Ids.XSLT_DIR, Ids.SUMMARY_XSLT_FILE, "Summary", out node);
      doc.AppendChild(XmlFactory.CreateSummaryXmlNode(doc, node));
      XmlFactory.SaveXmlFile(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.SUMMARY_FILE), doc);
    }
    #endregion ..................................... Summary Content XML helpers


    #region Create Declaration files ...........................................
    private void CreateLanguageDeclarationFile()
    {
      IConfigurationProxy proxy = ProxyHome.Instance.RetrieveConfigurationProxy(OutputKeyKeeper.Instance.AccessKey);

      XmlNode node;
      XmlDocument doc = XmlFactory.CreateXmlFile(Ids.XSLT_DIR, Ids.ALL_LANGUAGE_DECLARATIONS_XSLT_FILE, "LanguageDeclarations", out node);

      // <LanguageDeclarations>
      //   <LanguageDeclaration />
      // </LanguageDeclarations>
      foreach (KeyValuePair<int, ILanguageDeclaration> pair in proxy.LanguageDeclarations())
      {
        node.AppendChild(XmlFactory.CreateLanguageDeclarationXmlNode(doc, pair.Value));
        doc.AppendChild(node);
      }

      XmlFactory.SaveXmlFile(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.ALL_LANGUAGE_DECLARATIONS_XML_FILE), doc);
    }

    private void CreateCategoryDeclarationFile()
    {
      IConfigurationProxy proxy = ProxyHome.Instance.RetrieveConfigurationProxy(OutputKeyKeeper.Instance.AccessKey);

      XmlNode node;
      XmlDocument doc  = XmlFactory.CreateXmlFile(Ids.XSLT_DIR, Ids.ALL_CATEGORY_DECLARATIONS_XSLT_FILE, "CategoryDeclarations", out node);

      // <CategoryDeclarations>
      //   <CategoryDeclaration />
      // </CategoryDeclarations>
      foreach (KeyValuePair<int, ICategoryDeclaration> pair in proxy.CategoryDeclarations())
      {
        node.AppendChild(XmlFactory.CreateCategoryDeclarationXmlNode(doc, pair.Value));
        doc.AppendChild(node);
      }

      XmlFactory.SaveXmlFile(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.ALL_CATEGORY_DECLARATIONS_XML_FILE), doc);
    }

    private void CreateRuleDeclarationFile()
    {
      IConfigurationProxy proxy = ProxyHome.Instance.RetrieveConfigurationProxy(OutputKeyKeeper.Instance.AccessKey);

      XmlNode node;
      XmlDocument doc = XmlFactory.CreateXmlFile(Ids.XSLT_DIR, Ids.ALL_RULE_DECLARATIONS_XSLT_FILE, "RuleDeclarations", out node);

      // <RuleDeclarations>
      //   <RuleDeclaration />
      // </RuleDeclarations>
      foreach (KeyValuePair<int, ICategoryDeclaration> categoryDeclaration in proxy.CategoryDeclarations())
      {
        foreach (KeyValuePair<int, IRuleDeclaration> pair in proxy.RuleDeclarationsFromCategoryId(categoryDeclaration.Value.Id))
        {
          node.AppendChild(XmlFactory.CreateRuleDeclarationXmlNode(doc, pair.Value));
          doc.AppendChild(node);
        }
      }

      XmlFactory.SaveXmlFile(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.ALL_RULE_DECLARATIONS_XML_FILE), doc);
    }
    #endregion ........................................ Create Declaration files


    #region Create Match files .................................................
    private void CreateAllMatchesFile()
    {
			IMatchProxy matchProxy = ProxyHome.Instance.RetrieveMatchProxy(OutputKeyKeeper.Instance.AccessKey);

      XmlNode node;
      XmlDocument doc = XmlFactory.CreateXmlFile(Ids.XSLT_DIR, Ids.ALL_MATCHES_XSLT_FILE, "Matches", out node);


      // <Matches>  
      //   <Match />
      // </Matches>  
      int num = 1;
      foreach (IMatch match in matchProxy.Matches())
      {
        node.AppendChild(XmlFactory.CreateMatchXmlNode(doc, num++, match));
      }
			doc.AppendChild(node);

      XmlFactory.SaveXmlFile(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.ALL_MATCHES_XML_FILE), doc);
    }

    private void CreateInfofWarnCritFatalMatchesFile()
    {
			IMatchProxy matchProxy = ProxyHome.Instance.RetrieveMatchProxy(OutputKeyKeeper.Instance.AccessKey);

      XmlNode infoNode;
      XmlNode warningNode;
      XmlNode criticalNode;
      XmlNode fatalNode;

      XmlDocument infoDoc     = XmlFactory.CreateXmlFile(Ids.XSLT_DIR, Ids.ALL_MATCHES_XSLT_FILE, "Matches", out infoNode);
      XmlDocument warningDoc  = XmlFactory.CreateXmlFile(Ids.XSLT_DIR, Ids.ALL_MATCHES_XSLT_FILE, "Matches", out warningNode);
      XmlDocument criticalDoc = XmlFactory.CreateXmlFile(Ids.XSLT_DIR, Ids.ALL_MATCHES_XSLT_FILE, "Matches", out criticalNode);
      XmlDocument fatalDoc    = XmlFactory.CreateXmlFile(Ids.XSLT_DIR, Ids.ALL_MATCHES_XSLT_FILE, "Matches", out fatalNode);

      infoDoc.AppendChild(infoNode);
      warningDoc.AppendChild(warningNode);
      criticalDoc.AppendChild(criticalNode);
      fatalDoc.AppendChild(fatalNode);

      // <Matches>  
      //   <Match />
      // </Matches>  
      int num = 1;
			foreach (IMatch match in matchProxy.Matches())
      {
        if (match.Severity == RuleSeverity.Info)
        {
          infoNode.AppendChild(XmlFactory.CreateMatchXmlNode(infoDoc, num++, match));
        }
        
        if (match.Severity == RuleSeverity.Warning)
        {
          warningNode.AppendChild(XmlFactory.CreateMatchXmlNode(warningDoc, num++, match));
        }

        if (match.Severity == RuleSeverity.Critical)
        {
          criticalNode.AppendChild(XmlFactory.CreateMatchXmlNode(criticalDoc, num++, match));
        }

        if (match.Severity == RuleSeverity.Fatal)
        {
          fatalNode.AppendChild(XmlFactory.CreateMatchXmlNode(fatalDoc, num++, match));
        }
      }

      XmlFactory.SaveXmlFile(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.ALL_INFO_MATCHES_XML_FILE),     infoDoc);
      XmlFactory.SaveXmlFile(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.ALL_WARNING_MATCHES_XML_FILE),  warningDoc);
      XmlFactory.SaveXmlFile(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.ALL_CRITICAL_MATCHES_XML_FILE), criticalDoc);
      XmlFactory.SaveXmlFile(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.ALL_FATAL_MATCHES_XML_FILE),    fatalDoc);
    }
    #endregion .............................................. Create Match files


    #region Create Definition files ............................................
    private void CreateProjectDefinitionsFile()
    {
      IConfigurationProxy proxy = ProxyHome.Instance.RetrieveConfigurationProxy(OutputKeyKeeper.Instance.AccessKey);

      XmlNode node;
      XmlDocument doc = XmlFactory.CreateXmlFile(Ids.XSLT_DIR, Ids.ALL_PROJECT_DEFINITIONS_XSLT_FILE, "ProjectDefinitions", out node);

      //<ProjectDefinitions>
      //  <ProjectDefinition />
      //</ProjectDefinitions>
      foreach (KeyValuePair<int, IProjectDefinition> pair in proxy.Projects())
      {
        IProjectDefinition project = pair.Value;

        node.AppendChild(XmlFactory.CreateProjectDefinitionXmlNode(doc, project));
        doc.AppendChild(node);
      }

      XmlFactory.SaveXmlFile(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.ALL_PROJECT_DEFINITIONS_XML_FILE), doc);
    }

    private void CreateCategoryDefinitionsFile()
    {
      IConfigurationProxy proxy = ProxyHome.Instance.RetrieveConfigurationProxy(OutputKeyKeeper.Instance.AccessKey);

      XmlNode node;
      XmlDocument doc = XmlFactory.CreateXmlFile(Ids.XSLT_DIR, Ids.ALL_CATEGORY_DEFINITIONS_XSLT_FILE, "CategoryDefinitions", out node);

      //<CategoryDefinitions>
      //  <CategoryDefinition />
      //</CategoryDefinitions>
      foreach (KeyValuePair<int, IProjectDefinition> projectDefinition in proxy.Projects())
      {
        foreach (KeyValuePair<int, ICategoryDefinition> pair in proxy.ProjectCategories(projectDefinition.Value.Id))
        {
          ICategoryDefinition categoryDefinition = pair.Value;

          node.AppendChild(XmlFactory.CreateCategoryDefinitionXmlNode(doc, categoryDefinition));
          doc.AppendChild(node);
        }
      }

      XmlFactory.SaveXmlFile(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.ALL_CATEGORY_DEFINITIONS_XML_FILE), doc);
    }

    private void CreateRuleDefinitionsFile()
    {
      IConfigurationProxy proxy = ProxyHome.Instance.RetrieveConfigurationProxy(OutputKeyKeeper.Instance.AccessKey);

      XmlNode node;
      XmlDocument doc = XmlFactory.CreateXmlFile(Ids.XSLT_DIR, Ids.ALL_RULE_DEFINITIONS_XSLT_FILE, "RuleDefinitions", out node);

      //<RuleDefinitions>
      //  <RuleDefinition />
      //</RuleDefinitions>
      foreach (KeyValuePair<int, IProjectDefinition> projectDefinition in proxy.Projects())
      {
        foreach (KeyValuePair<int, IRuleDefinition> pair in proxy.ProjectRules(projectDefinition.Value.Id))
        {
          IRuleDefinition ruleDefinition = pair.Value;

          IRuleDeclaration ruleDeclaration = proxy.RuleDeclarationFromRuleId(ruleDefinition.RuleDeclarationReferenceId);
          node.AppendChild(XmlFactory.CreateRuleDefinitionXmlNode(doc, ruleDeclaration, ruleDefinition));
          doc.AppendChild(node);
        }
      }

      XmlFactory.SaveXmlFile(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.ALL_RULE_DEFINITIONS_XML_FILE), doc);
    }
    #endregion ......................................... Create Definition files


    #region TOC Content XML helpers ............................................
    private void TocContent(string outputRootDir, XmlDocument doc)
    {
      XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
      doc.AppendChild(declaration);

      var pi = doc.CreateProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + Ids.XSLT_DIR + "/" + Ids.TOC_XSLT_FILE + "\"");
      doc.AppendChild(pi);

      XmlNode toc        = doc.CreateNode(XmlNodeType.Element, "Toc", null);
      XmlNode categories = doc.CreateNode(XmlNodeType.Element, "CategoryDeclarations", null);

      // Create the actual xml content for the TOC.
      //TocCategoryContent(outputRootDir, doc, categories);

      toc.AppendChild(categories);
      doc.AppendChild(toc);
    }

    //private void TocCategoryContent(string outputRootDir, XmlDocument doc, XmlNode node)
    //{
    //  #region Validate arguments
    //  if (doc == null) throw new ArgumentNullException("doc");
    //  if (node == null) throw new ArgumentNullException("node");
    //  #endregion


    //  ProxyHome.Instance.ConfigurationComponentProxy.Projects();

    //  List<string> retrieveCategories = MatchHome.RetrieveCategories();
    //  retrieveCategories.Sort();
      
    //  int counterId = 1;
    //  foreach (string category in retrieveCategories)
    //  {
    //    // Generate 'Id' - the id is needed for the javascript to work properly in the resulting html output file.
    //    // Category name = 'category' from the surrounding 'foreach'. :-)
    //    // Count - how many matches was found in the current category?
    //    // Description - Description is part of each Match, so get the description from the first element in the categorylist.
    //    // Filename - the filename must be constructed for each category.

    //    ICategorySummary categorySummary = MatchHome.RetriveCategorySummary(category);
    //    categorySummary.Id = counterId++;

    //    // Create the link for the current category's rules file...
    //    categorySummary.LinkToCategoryXmlFile = CreateNameForRulesFile(category);

    //    // Create the xml for the current category...
    //    XmlNode categoryNode = doc.CreateNode(XmlNodeType.Element, "Category", null);
    //    TocCategoryXml(doc, categoryNode, categorySummary);

    //    // For each category a set of rules can be defined - these rules will have to have their
    //    // own sub-toc file, so let's go ahead and create that file.
    //    SubTocRulesContent(outputRootDir, categorySummary);

    //    node.AppendChild(categoryNode);
    //  }
    //}

    private void TocCategoryXml(XmlDocument doc, XmlNode categoryNode, ICategorySummary categorySummary)
    {
      // The following is an example of how a category is represented in the resulting xml file:
      // =======================================================================================
      //
      // <Category Id="2" Name="Test name 2">
      //   <Count>123</Count>
      //   <Description>This is a test description...</Description>
      //   <Filename>XmlReport.xml</Filename>
      // </Category>
      //
      // =======================================================================================

      XmlNode countNode       = doc.CreateNode(XmlNodeType.Element, "Count", null);
      XmlNode descriptionNode = doc.CreateNode(XmlNodeType.Element, "Description", null);
      XmlNode filenameNode    = doc.CreateNode(XmlNodeType.Element, "Filename", null);
      XmlNode severityNode    = doc.CreateNode(XmlNodeType.Element, "Severity", null);

      // Category node...
      XmlAttribute idAttrib   = doc.CreateAttribute("Id");
      XmlAttribute nameAttrib = doc.CreateAttribute("Name");
      idAttrib.Value   = categorySummary.Id.ToString();
      nameAttrib.Value = categorySummary.Name;

      categoryNode.Attributes.Append(idAttrib);
      categoryNode.Attributes.Append(nameAttrib);

      // Count node...
      countNode.InnerText = categorySummary.Count.ToString();
      categoryNode.AppendChild(countNode);

      // Description node...
      descriptionNode.InnerText = categorySummary.Description;
      categoryNode.AppendChild(descriptionNode);

      // Filename node...
      filenameNode.InnerText = categorySummary.LinkToCategoryXmlFile;
      categoryNode.AppendChild(filenameNode);

      // Severity node...
      XmlAttribute severityValueAttrib = doc.CreateAttribute("Value");
      severityValueAttrib.Value = (int)categorySummary.Severity + "";
      severityNode.Attributes.Append(severityValueAttrib);

      severityNode.InnerText = categorySummary.Severity.ToString();
      categoryNode.AppendChild(severityNode);
    }

    private void GenerateAllFilesFoundDuringSearch()
    {
      IConfigurationProxy cfgProxy = ProxyHome.Instance.RetrieveConfigurationProxy(OutputKeyKeeper.Instance.AccessKey);
      IStatisticsProxy   statProxy = ProxyHome.Instance.RetrieveStatisticsProxy(OutputKeyKeeper.Instance.AccessKey);


      XmlDocument    doc         = new XmlDocument();
      XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
      doc.AppendChild(declaration);

      var pi = doc.CreateProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + Ids.XSLT_DIR + "/" + Ids.FILES_SEARCHED_XSLT_FILE + "\"");
      doc.AppendChild(pi);


      // <Projects>
      //   <Project Name="string" Id="guid">
      //     <RootDirectory Path="string">
      //       <File Path="string" />
      //       <File Path="string" />
      //       <File Path="string" />
      //     </RootDirectory>
      //   </Project>
      // </Projects>
      XmlNode projects = doc.CreateNode(XmlNodeType.Element, "Projects", null);

      foreach (KeyValuePair<int, IProjectDefinition> pair in cfgProxy.Projects())
      {
        IProjectDefinition projectDefinition = pair.Value;
        if (!pair.Value.Enabled)
          continue;

        XmlNode      project    = doc.CreateNode(XmlNodeType.Element, "Project", null);
        XmlAttribute nameAttrib = doc.CreateAttribute("Name");
        XmlAttribute idAttrib   = doc.CreateAttribute("Id");

        nameAttrib.Value        = projectDefinition.Name;
        idAttrib.Value          = projectDefinition.Id.ToString();

        project.Attributes.Append(nameAttrib);
        project.Attributes.Append(idAttrib);
        

        foreach (IRootDirectoryStatistics rootDirectoryStats in statProxy.GetRootDirectoriesFromId(projectDefinition.Id))
        {
          XmlNode rootDir = doc.CreateNode(XmlNodeType.Element, "RootDirectory", null);
          XmlAttribute dirPath = doc.CreateAttribute("Path");
          dirPath.Value = rootDirectoryStats.RootDirectory;
          rootDir.Attributes.Append(dirPath);

          foreach (string filename in rootDirectoryStats.Filenames)
          {
            XmlNode file = doc.CreateNode(XmlNodeType.Element, "File", null);
            XmlAttribute filePath = doc.CreateAttribute("Path");
            filePath.Value = filename;
            file.Attributes.Append(filePath);

            rootDir.AppendChild(file);
          }

          project.AppendChild(rootDir);
        }

        projects.AppendChild(project);
      }

      doc.AppendChild(projects);
      doc.Save(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.FILES_SEARCHED_XML_FILE));
    }

    private void GenerateAllRulesFile(string outputRootDir)
    {
      // First we need to update the id on each rule node.
      int num = 1;
      foreach (XmlNode node in ContainerOfRuleNodes)
      {
        node.Attributes["Id"].Value = num++ + "";
      }

      XmlDocument doc = new XmlDocument();
      XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
      doc.AppendChild(declaration);

      var pi = doc.CreateProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + Ids.XSLT_DIR + "/" + Ids.SUB_TOC_XSLT_FILE + "\"");
      doc.AppendChild(pi);

      XmlNode rules = doc.CreateNode(XmlNodeType.Element, "Rules", null);

      foreach (XmlNode ruleNode in ContainerOfRuleNodes)
      {
        rules.AppendChild(doc.ImportNode(ruleNode, true));
      }

      doc.AppendChild(rules);
      doc.Save(Path.Combine(outputRootDir, Ids.OUTPUT_DIR, Ids.ALL_RULES_FILE));
    }


    //private void SubTocRulesContent(string outputRootDir, ICategorySummary categorySummary)
    //{
    //  XmlDocument    doc         = new XmlDocument();
    //  XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
    //  doc.AppendChild(declaration);

    //  var pi = doc.CreateProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + Ids.XSLT_DIR + "/" + Ids.SUB_TOC_XSLT_FILE + "\"");
    //  doc.AppendChild(pi);

    //  XmlNode rules = doc.CreateNode(XmlNodeType.Element, "Rules", null);

    //  // Creating a list to hold all the 'ruleSummary summaries'...
    //  Dictionary<string, IRuleSummary> ruleSummaries = new Dictionary<string, IRuleSummary>();

    //  // Extract all the matches related to the current category...
    //  List<IMatch> matches = MatchHome.MatchesByCategory(categorySummary.Name);

    //  // Extract all the rules from the matches in the current category...
    //  int ruleCounter = 0;
    //  foreach (IMatch match in matches)
    //  {
    //    if (!ruleSummaries.ContainsKey(match.RegExp.Name))
    //    {
    //      IRuleSummary ruleSummary = ProxyHome.Instance.EngineComponentProxy.InstatiateRuleSummary(++ruleCounter,
    //                                                                                      match.RegExp.Enabled, 
    //                                                                                      1,
    //                                                                                      match.RegExp.Name,
    //                                                                                      match.RegExp.Description,
    //                                                                                      match.RegExp.Expression,
    //                                                                                      match.Severity,
    //                                                                                      CreateNameForMatchFile(categorySummary.Name, match.RegExp.Name));
            
    //      ruleSummary.Matches.Add(match);

    //      // First ruleSummary of this specific ruleSummary.
    //      ruleSummaries.Add(ruleSummary.Name, ruleSummary);
    //    }
    //    else
    //    {
    //      // Second or n'th encounter of this specific ruleSummary.
    //      ruleSummaries[match.RegExp.Name].Count++;
    //      ruleSummaries[match.RegExp.Name].Matches.Add(match);
    //    }
    //  }


    //  // Now that have extracted all the 'ruleSummary summaries' let's go ahead and create the 
    //  // ruleSummary xml nodes...
    //  foreach (KeyValuePair<string, IRuleSummary> pair in ruleSummaries)
    //  {
    //    RuleXml(doc, rules, outputRootDir, categorySummary, pair.Value);
    //    MatchFile(outputRootDir, categorySummary, pair.Value);
    //  }

    //  doc.AppendChild(rules);
    //  doc.Save(CreateFullPathForRulesFile(outputRootDir, categorySummary.Name));
    //}

    private void RuleXml(XmlDocument doc, XmlNode node, string outputRootDir, ICategorySummary category, IRuleSummary ruleSummary)
    {
      // The following is an example of how a category is represented in the resulting report xml file:
      // ==============================================================================================
      //
      // <Rule Id="1">
      //   <Count>123</Count>
      //   <Enabled>true</Enabled>
      //   <Name>Finds classes</Name>
      //   <Description>Finds all the class definitions.</Description>
      //   <Severity>Critical</Severity>
      //   <Expression>\s*public\s*class\s*</Expression>
      //   <Link>SomeFile.xml</Link>
      // </Rule>
      //
      // ==============================================================================================
      XmlNode ruleNode        = doc.CreateNode(XmlNodeType.Element, "Rule", null);
      XmlNode countNode       = doc.CreateNode(XmlNodeType.Element, "Count", null);
      XmlNode enabledNode     = doc.CreateNode(XmlNodeType.Element, "Enabled", null);
      XmlNode nameNode        = doc.CreateNode(XmlNodeType.Element, "Name", null);
      XmlNode descriptionNode = doc.CreateNode(XmlNodeType.Element, "Description", null);
      XmlNode severityNode    = doc.CreateNode(XmlNodeType.Element, "Severity", null);
      XmlNode expressionNode  = doc.CreateNode(XmlNodeType.Element, "Expression", null);
      XmlNode linkNode        = doc.CreateNode(XmlNodeType.Element, "Link", null);


      // Rule node...
      XmlAttribute idAttrib = doc.CreateAttribute("Id");
      idAttrib.Value = ruleSummary.Id.ToString();
      ruleNode.Attributes.Append(idAttrib);

      // Count node...
      countNode.InnerText = ruleSummary.Count.ToString();

      // Enabled node...
      enabledNode.InnerText = ruleSummary.Enabled.ToString();

      // Name node...
      XmlCDataSection cdataNameNode = doc.CreateCDataSection(ruleSummary.Name);
      nameNode.AppendChild(cdataNameNode);

      // Description node...
      XmlCDataSection cdataDescriptionNode = doc.CreateCDataSection(ruleSummary.Description);
      descriptionNode.AppendChild(cdataDescriptionNode);

      // Expression node...
      XmlCDataSection cdataExpression = doc.CreateCDataSection(ruleSummary.Expression);
      expressionNode.AppendChild(cdataExpression);

      // Severity node...
      severityNode.InnerText = ruleSummary.Severity.ToString();

      // Link node...
      linkNode.InnerText = CreateNameForMatchFile(category.Name, ruleSummary.Name);

      // Add all nodes to the 'node' given in the arguments.
      ruleNode.AppendChild(countNode);
      ruleNode.AppendChild(enabledNode);
      ruleNode.AppendChild(nameNode);
      ruleNode.AppendChild(descriptionNode);
      ruleNode.AppendChild(severityNode);
      ruleNode.AppendChild(expressionNode);
      ruleNode.AppendChild(linkNode);
      node.AppendChild(ruleNode);

      // Finally let's add the current rule node to the overall container of rule nodes.
      // Later in the process all the rule nodes will be persisted to a AllRules.xml file.
      ContainerOfRuleNodes.Add(ruleNode);
    }
    #endregion ......................................... TOC Content XML helpers


    #region Match Content XML Helpers ..........................................
    private void MatchFile(string outputRootDir, ICategorySummary category, IRuleSummary ruleSummary)
    {
      XmlDocument doc = new XmlDocument();

      XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
      doc.AppendChild(declaration);

      var pi = doc.CreateProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + Ids.XSLT_DIR + "/" + Ids.MATCHES_XSLT_FILE + "\"");
      doc.AppendChild(pi);

      XmlNode matches = doc.CreateNode(XmlNodeType.Element, "Matches", null);
      XmlNode   summaryNode = doc.CreateNode(XmlNodeType.Element, "Summary", null);
      XmlNode     countNode       = doc.CreateNode(XmlNodeType.Element, "Count", null);
      XmlNode     ruleNameNode    = doc.CreateNode(XmlNodeType.Element, "Name", null);
      XmlNode     descriptionNode = doc.CreateNode(XmlNodeType.Element, "Description", null);
      XmlNode     expressionNode  = doc.CreateNode(XmlNodeType.Element, "Expression", null);

      summaryNode.AppendChild(expressionNode);
      summaryNode.AppendChild(ruleNameNode);
      summaryNode.AppendChild(descriptionNode);
      summaryNode.AppendChild(countNode);
      matches.AppendChild(summaryNode);
      doc.AppendChild(matches);

      // Summary.Name node...
      XmlCDataSection cdataRuleNameNode = doc.CreateCDataSection(ruleSummary.Name);
      ruleNameNode.AppendChild(cdataRuleNameNode);

      // Summary.Description node...
      XmlCDataSection cdataDescriptionNode = doc.CreateCDataSection(ruleSummary.Description);
      descriptionNode.AppendChild(cdataDescriptionNode);

      // Summary.Expression node...
      XmlCDataSection cdataExpressionNode = doc.CreateCDataSection(ruleSummary.Expression);
      expressionNode.AppendChild(cdataExpressionNode);

      // Summary.Count node...
      countNode.InnerText = ruleSummary.Matches.Count.ToString();

      //int matchId = 1;
      foreach (IMatch match in ruleSummary.Matches)
      {
        //MatchXml(doc, matches, matchId++, match);
      }

      doc.Save(CreateFullPathForMatchFile(outputRootDir, category.Name, ruleSummary.Name));
    }

    #endregion ....................................... Match Content XML Helpers


    #region Create filenames for report files ..................................
    private string CreateNameForRulesFile(string category)
    {
      #region Validate arguments...
      if (string.IsNullOrWhiteSpace(category)) throw new ArgumentNullException("category");
      #endregion

      string rulesFilename = category + "_Rules" + Ids.XML_FILE_EXTENSION;
      return Path.Combine(rulesFilename);
    }

    private string CreateFullPathForRulesFile(string outputRootDir, string category)
    {
      #region Validate arguments...
      if (string.IsNullOrWhiteSpace(outputRootDir)) throw new ArgumentNullException("outputRootDir");
      if (string.IsNullOrWhiteSpace(category)) throw new ArgumentNullException("category");
      #endregion

      string rulesFilename = category + "_Rules" + Ids.XML_FILE_EXTENSION;
      return Path.Combine(outputRootDir, Ids.OUTPUT_DIR, rulesFilename);
    }

    private string CreateNameForMatchFile(string category, string rule)
    {
      #region Validate arguments...
      if (string.IsNullOrWhiteSpace(category)) throw new ArgumentNullException("category");
      if (string.IsNullOrWhiteSpace(rule)) throw new ArgumentNullException("rule");
      #endregion

      string matchFilename = category + "_" + rule + "_Match" + Ids.XML_FILE_EXTENSION;
      return Path.Combine(matchFilename);
    }

    private string CreateFullPathForMatchFile(string outputRootDir, string category, string rule)
    {
      #region Validate arguments...
      if (string.IsNullOrWhiteSpace(outputRootDir)) throw new ArgumentNullException("outputRootDir");
      if (string.IsNullOrWhiteSpace(category)) throw new ArgumentNullException("category");
      if (string.IsNullOrWhiteSpace(rule)) throw new ArgumentNullException("rule");
      #endregion

      string matchFilename = category + "_" + rule + "_Match" + Ids.XML_FILE_EXTENSION;
      return Path.Combine(outputRootDir, Ids.OUTPUT_DIR, matchFilename);
    }
    #endregion ............................... Create filenames for report files


    #region IO related helpers .................................................
    private void CreateDirectories()
    {
      // Create the needed directories...
      FileAccesser.CreateFullPath(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.CSS_DIR));
      FileAccesser.CreateFullPath(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.IMAGES_DIR));
      FileAccesser.CreateFullPath(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.JAVASCRIPT_DIR));
      FileAccesser.CreateFullPath(Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.XSLT_DIR));
    }

    private void CopyFiles()
    {
      string currentDirectory = DirHandler.Instance.CurrentDirectory;

      // Copy Css files from the Repository dir to the Output Css dir...
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.CSS_DIR, Ids.COMMON_CSS_FILE), Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.CSS_DIR, Ids.COMMON_CSS_FILE));

      // Copy Images from the Repository dir to the Output Images dir...
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.IMAGES_DIR, Ids.PLUS_PNG_FILE),  Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.IMAGES_DIR, Ids.PLUS_PNG_FILE));
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.IMAGES_DIR, Ids.MINUS_PNG_FILE), Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.IMAGES_DIR, Ids.MINUS_PNG_FILE));

      // Copy js files from the Repository dir to the Output JavaScript dir...
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.JAVASCRIPT_DIR, Ids.COMMON_JS_FILE), Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.JAVASCRIPT_DIR, Ids.COMMON_JS_FILE));

      // Copy xslt files from the Repository dir to the Output XSLT dir...
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.XSLT_DIR, Ids.ALL_MATCHES_XSLT_FILE),               Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.XSLT_DIR, Ids.ALL_MATCHES_XSLT_FILE));
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.XSLT_DIR, Ids.ALL_PROJECT_DEFINITIONS_XSLT_FILE),   Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.XSLT_DIR, Ids.ALL_PROJECT_DEFINITIONS_XSLT_FILE));
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.XSLT_DIR, Ids.ALL_CATEGORY_DEFINITIONS_XSLT_FILE),  Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.XSLT_DIR, Ids.ALL_CATEGORY_DEFINITIONS_XSLT_FILE));
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.XSLT_DIR, Ids.ALL_RULE_DEFINITIONS_XSLT_FILE),      Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.XSLT_DIR, Ids.ALL_RULE_DEFINITIONS_XSLT_FILE));
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.XSLT_DIR, Ids.ALL_CATEGORY_DECLARATIONS_XSLT_FILE), Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.XSLT_DIR, Ids.ALL_CATEGORY_DECLARATIONS_XSLT_FILE));
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.XSLT_DIR, Ids.ALL_LANGUAGE_DECLARATIONS_XSLT_FILE), Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.XSLT_DIR, Ids.ALL_LANGUAGE_DECLARATIONS_XSLT_FILE));
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.XSLT_DIR, Ids.ALL_RULE_DECLARATIONS_XSLT_FILE),     Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.XSLT_DIR, Ids.ALL_RULE_DECLARATIONS_XSLT_FILE));
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.XSLT_DIR, Ids.TOC_XSLT_FILE),                       Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.XSLT_DIR, Ids.TOC_XSLT_FILE));
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.XSLT_DIR, Ids.SUMMARY_XSLT_FILE),                   Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.XSLT_DIR, Ids.SUMMARY_XSLT_FILE));
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.XSLT_DIR, Ids.SUB_TOC_XSLT_FILE),                   Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.XSLT_DIR, Ids.SUB_TOC_XSLT_FILE));
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.XSLT_DIR, Ids.FILES_SEARCHED_XSLT_FILE),            Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.XSLT_DIR, Ids.FILES_SEARCHED_XSLT_FILE));

      // Copy xml files from the Repository dir to the Output dir (empty fallback files)...
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.SUMMARY_FILE),     Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.SUMMARY_FILE));
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.SUB_TOC_XML_FILE), Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.SUB_TOC_XML_FILE));

      // Copy index file from the Repository dir to the Output dir...
      FileAccesser.CopyFile(Path.Combine(currentDirectory, Ids.REPOSITORY_DIR, Ids.INDEX_HTML_FILE), Path.Combine(OutputRootDir, Ids.OUTPUT_DIR, Ids.INDEX_HTML_FILE));
    }
    #endregion .............................................. IO related helpers
  }
}