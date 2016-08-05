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
  using System.Xml;

  using Mediator;
  using Mediator.Configuration;
  using Mediator.Configuration.Declarations;
  using Mediator.Configuration.Definitions;
  using Mediator.Identifiers;
  using Mediator.Matches;
  using Mediator.Statistics;


	internal class XmlFactory
  {
    #region Create and Save xml file ...........................................
    public static XmlDocument CreateXmlFile(string xsltDir, string xsltName, string rootNodeName, out XmlNode node)
    {
      #region Validate input
      if (string.IsNullOrWhiteSpace(xsltDir))      
        throw new ArgumentNullException("xsltDir");

      if (string.IsNullOrWhiteSpace(xsltName))     
        throw new ArgumentNullException("xsltName");

      if (string.IsNullOrWhiteSpace(rootNodeName)) 
        throw new ArgumentNullException("rootNodeName");
      #endregion


      XmlDocument    doc         = new XmlDocument();
      XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
      doc.AppendChild(declaration);

      var pi = doc.CreateProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + xsltDir + "/" + xsltName + "\"");
      doc.AppendChild(pi);

      node = doc.CreateNode(XmlNodeType.Element, rootNodeName, null);

      return doc;
    }

    public static void SaveXmlFile(string path, XmlDocument doc)
    {
      if (string.IsNullOrWhiteSpace(path))
        throw new ArgumentNullException("path");

      doc.Save(path);
    }
    #endregion ........................................ Create and Save xml file


    #region Summary ............................................................
    public static XmlNode CreateSummaryXmlNode(XmlDocument doc, XmlNode node)
    {
      XmlNode createdNode                     = doc.CreateNode(XmlNodeType.Element, "Creation",                            null);
      XmlNode languageDeclarationsNode        = doc.CreateNode(XmlNodeType.Element, "NumberOfLanguageDeclarations",        null);
      XmlNode categoryDeclarationsNode        = doc.CreateNode(XmlNodeType.Element, "NumberOfCategoryDeclarations",        null);
      XmlNode ruleDeclarationsNode            = doc.CreateNode(XmlNodeType.Element, "NumberOfRuleDeclarations",            null);
      XmlNode totalProjectDefinitionsNode     = doc.CreateNode(XmlNodeType.Element, "NumberOfTotalProjectDefinitions",     null);
      XmlNode activeProjectDefinitionsNode    = doc.CreateNode(XmlNodeType.Element, "NumberOfActiveProjectDefinitions",    null);
      XmlNode inactiveProjectDefinitionsNode  = doc.CreateNode(XmlNodeType.Element, "NumberOfInactiveProjectDefinitions",  null);
      XmlNode totalCategoryDefinitionsNode    = doc.CreateNode(XmlNodeType.Element, "NumberOfTotalCategoryDefinitions",    null);
      XmlNode activeCategoryDefinitionsNode   = doc.CreateNode(XmlNodeType.Element, "NumberOfActiveCategoryDefinitions",   null);
      XmlNode inactiveCategoryDefinitionsNode = doc.CreateNode(XmlNodeType.Element, "NumberOfInactiveCategoryDefinitions", null);
      XmlNode totalRuleDefinitionsNode        = doc.CreateNode(XmlNodeType.Element, "NumberOfTotalRuleDefinitions",        null);
      XmlNode activeRuleDefinitionsNode       = doc.CreateNode(XmlNodeType.Element, "NumberOfActiveRuleDefinitions",       null);
      XmlNode inactiveRuleDefinitionsNode     = doc.CreateNode(XmlNodeType.Element, "NumberOfInactiveRuleDefinitions",     null);
      XmlNode fileCountNode                   = doc.CreateNode(XmlNodeType.Element, "NumberOfFilesSearched",               null);
      XmlNode totalMatchCountNode             = doc.CreateNode(XmlNodeType.Element, "NumberOfTotalMatches",                null);
      XmlNode infoMatchCountNode              = doc.CreateNode(XmlNodeType.Element, "NumberOfInfoMatches",                 null);
      XmlNode warningMatchCountNode           = doc.CreateNode(XmlNodeType.Element, "NumberOfWarningMatches",              null);
      XmlNode criticalMatchCountNode          = doc.CreateNode(XmlNodeType.Element, "NumberOfCriticalMatches",             null);
      XmlNode fatalMatchCountNode             = doc.CreateNode(XmlNodeType.Element, "NumberOfFatalMatches",                null);

      IStatisticsProxy statProxy = ProxyHome.Instance.RetrieveStatisticsProxy(OutputKeyKeeper.Instance.AccessKey);

      // Creation...
      createdNode.InnerText = statProxy.StartTimeStamp.ToLongDateString() + " - " + statProxy.StartTimeStamp.ToLongTimeString();

      // Declarations...
      languageDeclarationsNode.InnerText = statProxy.ReadCounter(CounterIds.LanguageDeclarations) + "";
      categoryDeclarationsNode.InnerText = statProxy.ReadCounter(CounterIds.CategoryDeclarations) + "";
      ruleDeclarationsNode.InnerText     = statProxy.ReadCounter(CounterIds.RuleDeclarations) + "";

      // Project definitions...
      int totalProjectDefinitions  = statProxy.ReadCounter(CounterIds.TotalProjectDefinitions);
      int activeProjectDefinitions = statProxy.ReadCounter(CounterIds.ActiveProjectDefinitions);

      totalProjectDefinitionsNode.InnerText    = totalProjectDefinitions + "";
      activeProjectDefinitionsNode.InnerText   = activeProjectDefinitions + "";
      inactiveProjectDefinitionsNode.InnerText = totalProjectDefinitions - activeProjectDefinitions + "";

      // Category definitions...
      int totalCategoryDefinitions  = statProxy.ReadCounter(CounterIds.TotalCategoryDefinitions);
      int activeCategoryDefinitions = statProxy.ReadCounter(CounterIds.ActiveCategoryDefinitions);

      totalCategoryDefinitionsNode.InnerText    = totalCategoryDefinitions + "";
      activeCategoryDefinitionsNode.InnerText   = activeCategoryDefinitions + "";
      inactiveCategoryDefinitionsNode.InnerText = totalCategoryDefinitions - activeCategoryDefinitions + "";

      // Rule definitions...
      int totalRuleDefinitions  = statProxy.ReadCounter(CounterIds.TotalRuleDefinitions);
      int activeRuleDefinitions = statProxy.ReadCounter(CounterIds.ActiveRuleDefinitions);

      totalRuleDefinitionsNode.InnerText    = totalRuleDefinitions + "";
      activeRuleDefinitionsNode.InnerText   = activeRuleDefinitions + "";
      inactiveRuleDefinitionsNode.InnerText = totalRuleDefinitions - activeRuleDefinitions + "";

      // File searched...
      fileCountNode.InnerText = statProxy.ReadCounter(CounterIds.Files) + "";

      // Matches...
      int info     = statProxy.ReadCounter(CounterIds.InfoMatches);
      int warning  = statProxy.ReadCounter(CounterIds.WarningMatches);
      int critical = statProxy.ReadCounter(CounterIds.CriticalMatches);
      int fatal    = statProxy.ReadCounter(CounterIds.FatalMatches);

      infoMatchCountNode.InnerText     = info + "";
      warningMatchCountNode.InnerText  = warning + "";
      criticalMatchCountNode.InnerText = critical + "";
      fatalMatchCountNode.InnerText    = fatal + "";
      totalMatchCountNode.InnerText    = (info + warning + critical + fatal) + "";

      // Adding nodes to the parent summary node...
      node.AppendChild(createdNode);
      node.AppendChild(languageDeclarationsNode);
      node.AppendChild(categoryDeclarationsNode);
      node.AppendChild(ruleDeclarationsNode);
      node.AppendChild(totalProjectDefinitionsNode);
      node.AppendChild(activeProjectDefinitionsNode);
      node.AppendChild(inactiveProjectDefinitionsNode);
      node.AppendChild(totalCategoryDefinitionsNode);
      node.AppendChild(activeCategoryDefinitionsNode);
      node.AppendChild(inactiveCategoryDefinitionsNode);
      node.AppendChild(totalRuleDefinitionsNode);
      node.AppendChild(activeRuleDefinitionsNode);
      node.AppendChild(inactiveRuleDefinitionsNode);
      node.AppendChild(fileCountNode);
      node.AppendChild(totalMatchCountNode);
      node.AppendChild(infoMatchCountNode);
      node.AppendChild(warningMatchCountNode);
      node.AppendChild(criticalMatchCountNode);
      node.AppendChild(fatalMatchCountNode);
      return node;
    }
    #endregion ......................................................... Summary


    #region Declarations .......................................................
    public static XmlNode CreateLanguageDeclarationXmlNode(XmlDocument doc, ILanguageDeclaration declaration)
    {
      // <LanguageDeclaration Id="{C224974D-E493-42C2-960F-DBE6BB64C946}">
      //   <Name><CDATA ELEMENT></Name>
      //   <Extension>*.cs</Extension>
      // </LanguageDeclaration>
      XmlNode      languageDeclarationNode = doc.CreateNode(XmlNodeType.Element, "LanguageDeclaration", null);
      XmlNode      nameNode                = doc.CreateNode(XmlNodeType.Element, "Name",                null);
      XmlNode      extensionNode           = doc.CreateNode(XmlNodeType.Element, "Extension",           null);
      XmlAttribute idAttrib                = doc.CreateAttribute("Id");

      idAttrib.Value = declaration.Id.ToString();
      languageDeclarationNode.Attributes.Append(idAttrib);

      XmlCDataSection cdataNameNode = doc.CreateCDataSection(declaration.Name);
      nameNode.AppendChild(cdataNameNode);

      extensionNode.InnerText = declaration.Extension;

      languageDeclarationNode.AppendChild(nameNode);
      languageDeclarationNode.AppendChild(extensionNode);
      return languageDeclarationNode;
    }

    public static XmlNode CreateCategoryDeclarationXmlNode(XmlDocument doc, ICategoryDeclaration declaration)
    {
      // <CategoryDeclaration Name="Static constructions" Id="{867FDD48-C061-4C1A-9ADF-FD56CFB27BF0}">
      //   <Description><CDATA ELEMENT></Description>
      // </CategoryDeclaration>
      XmlNode      declarationNode = doc.CreateNode(XmlNodeType.Element, "CategoryDeclaration", null);
      XmlNode      descriptionNode = doc.CreateNode(XmlNodeType.Element, "Description",         null);
      XmlAttribute nameAttrib      = doc.CreateAttribute("Name");
      XmlAttribute idAttrib        = doc.CreateAttribute("Id");

      idAttrib.Value   = declaration.Id.ToString();
      nameAttrib.Value = declaration.Name;
      declarationNode.Attributes.Append(idAttrib);
      declarationNode.Attributes.Append(nameAttrib);

      XmlCDataSection cdataNameNode = doc.CreateCDataSection(declaration.Description);
      descriptionNode.AppendChild(cdataNameNode);

      declarationNode.AppendChild(descriptionNode);
      return declarationNode;
    }

    public static XmlNode CreateRuleDeclarationXmlNode(XmlDocument doc, IRuleDeclaration declaration)
    {
      // <RuleDeclaration Name="Static classes" Id="{A971681E-5E9A-4DD8-8870-35A4341752F4}">
      //   <Description><CDATA ELEMENT></Description>
      //   <Severity>Critical</Severity>
      //   <Expression><CDATA ELEMENT></Expression>
      //   <AppliesTo>
      //     <Language RefName="C#" RefId="{C224974D-E493-42C2-960F-DBE6BB64C946}" />
      //   </AppliesTo>
      // </RuleDeclaration>
      XmlNode      declarationNode = doc.CreateNode(XmlNodeType.Element, "RuleDeclaration", null);
      XmlNode      descriptionNode = doc.CreateNode(XmlNodeType.Element, "Description",     null);
      XmlNode      severityNode    = doc.CreateNode(XmlNodeType.Element, "Severity",        null);
      XmlNode      expressionNode  = doc.CreateNode(XmlNodeType.Element, "Expression",      null);
      XmlNode      appliesToNode   = doc.CreateNode(XmlNodeType.Element, "AppliesTo",       null);
      XmlAttribute severityAttrib  = doc.CreateAttribute("Value");
      XmlAttribute nameAttrib      = doc.CreateAttribute("Name");
      XmlAttribute idAttrib        = doc.CreateAttribute("Id");

      idAttrib.Value   = declaration.Id.ToString();
      nameAttrib.Value = declaration.Name;

      declarationNode.Attributes.Append(idAttrib);
      declarationNode.Attributes.Append(nameAttrib);

      XmlCDataSection descriptionCData = doc.CreateCDataSection(declaration.Description);
      descriptionNode.AppendChild(descriptionCData);


      severityAttrib.Value   = (int)declaration.Severity + "";
      severityNode.InnerText = declaration.Severity + "";
      severityNode.Attributes.Append(severityAttrib);


      XmlCDataSection expressionCData = doc.CreateCDataSection(declaration.Expression);
      expressionNode.AppendChild(expressionCData);

      foreach (KeyValuePair<int, ILanguageDeclaration> pair in declaration.Languages)
      {
        XmlNode      languageNode  = doc.CreateNode(XmlNodeType.Element, "Language", null);
        XmlAttribute refNameAttrib = doc.CreateAttribute("RefName");
        XmlAttribute refIdAttrib   = doc.CreateAttribute("RefId");

        refIdAttrib.Value   = pair.Value.Id + "";
        refNameAttrib.Value = pair.Value.Name;

        languageNode.Attributes.Append(refIdAttrib);
        languageNode.Attributes.Append(refNameAttrib);

        appliesToNode.AppendChild(languageNode);
      }


      declarationNode.AppendChild(descriptionNode);
      declarationNode.AppendChild(severityNode);
      declarationNode.AppendChild(expressionNode);
      declarationNode.AppendChild(appliesToNode);
      return declarationNode;
    }
    #endregion .................................................... Declarations


    #region Definitions ........................................................

    #region Directory definitions...
    public static XmlNode CreateDirectoriesDefinitionXmlNode(XmlDocument doc, IProjectDefinition definition)
    {
      // <Directories>
      //   <Include>
      //     <Directory />
      //   </Include>
      //   <Exclude>
      //     <Directory />
      //   </Exclude>
      // </Directories>
      XmlNode directoriesNode = doc.CreateNode(XmlNodeType.Element, "Directories", null);
      XmlNode includeNode     = doc.CreateNode(XmlNodeType.Element, "Include", null);
      XmlNode excludeNode     = doc.CreateNode(XmlNodeType.Element, "Exclude", null);

      foreach (IDirectoryDefinition directoryDefinition in definition.Directories)
      {
        includeNode.AppendChild(CreateDirectoryDefinitionXmlNode(doc, directoryDefinition));
      }

      foreach (IDirectoryDefinition directoryDefinition in definition.ExcludedDirectories)
      {
        excludeNode.AppendChild(CreateDirectoryDefinitionXmlNode(doc, directoryDefinition));
      }

      directoriesNode.AppendChild(includeNode);
      directoriesNode.AppendChild(excludeNode);
      return directoriesNode;
    }

    public static XmlNode CreateDirectoryDefinitionXmlNode(XmlDocument doc, IDirectoryDefinition definition)
    {
      // <Directory Enabled="true" Path="c:\CLR\TFSPreview\Frameworks\CodeAnalysis\Mediator\obj"/>
      XmlNode fileNode = doc.CreateNode(XmlNodeType.Element, "Directory", null);
      XmlAttribute enabledAttrib = doc.CreateAttribute("Enabled");
      XmlAttribute pathAttrib = doc.CreateAttribute("Path");

      enabledAttrib.Value = definition.Enabled.ToString();
      pathAttrib.Value = definition.Path;

      fileNode.Attributes.Append(enabledAttrib);
      fileNode.Attributes.Append(pathAttrib);

      return fileNode;
    }
    #endregion

    #region File definitions...
    public static XmlNode CreateFileDefinitionsXmlNode(XmlDocument doc, IProjectDefinition definition)
    {
      // <Files>
      //   <Include>
      //     <File />
      //   </Include>
      //   <Exclude>
      //     <File />
      //   </Exclude>
      // </Files>
      XmlNode filesNode   = doc.CreateNode(XmlNodeType.Element, "Files", null);
      XmlNode includeNode = doc.CreateNode(XmlNodeType.Element, "Include", null);
      XmlNode excludeNode = doc.CreateNode(XmlNodeType.Element, "Exclude", null);

      foreach (IFileDefinition fileDefinition in definition.Files)
      {
        includeNode.AppendChild(CreateFileDefinitionXmlNode(doc, fileDefinition));
      }

      foreach (IFileDefinition fileDefinition in definition.ExcludedFiles)
      {
        excludeNode.AppendChild(CreateFileDefinitionXmlNode(doc, fileDefinition));
      }

      filesNode.AppendChild(includeNode);
      filesNode.AppendChild(excludeNode);
      return filesNode;
    }

    public static XmlNode CreateFileDefinitionXmlNode(XmlDocument doc, IFileDefinition definition)
    {
      // <File Enabled="true" Path="c:\CLR\TFSPreview\Frameworks\CodeAnalysis\"/>
      XmlNode      fileNode      = doc.CreateNode(XmlNodeType.Element, "File", null);
      XmlAttribute enabledAttrib = doc.CreateAttribute("Enabled");
      XmlAttribute pathAttrib    = doc.CreateAttribute("Path");

      enabledAttrib.Value = definition.Enabled.ToString();
      pathAttrib.Value    = definition.Path;

      fileNode.Attributes.Append(enabledAttrib);
      fileNode.Attributes.Append(pathAttrib);

      return fileNode;
    }
    #endregion

    #region Category definitions...
    public static XmlNode CreateCategoryDefinitionsXmlNode(XmlDocument doc, IProjectDefinition definition)
    {
      // <CategoryDefinitions>
      //   <CategoryDefinition />
      // </CategoryDefinitions>
      XmlNode categoryDefinitionsNode = doc.CreateNode(XmlNodeType.Element, "CategoryDefinitions", null);

      foreach (KeyValuePair<int, ICategoryDefinition> pair in definition.Categories)
      {
        ICategoryDefinition categoryDefinition = pair.Value;

        categoryDefinitionsNode.AppendChild(CreateCategoryDefinitionXmlNode(doc, categoryDefinition));
      }
      return categoryDefinitionsNode;
    }

    public static XmlNode CreateCategoryDefinitionXmlNode(XmlDocument doc, ICategoryDefinition definition)
    {
      // <CategoryDefinition Enabled="true" Id="{GUID}" RefName="Try-Catches" RefId="{GUID}">
      //   <RuleDefinition Enabled="true" Id="{GUID}" RefName="No explicit exception identifier" RefId="{GUID}"/>
      //   <RuleDefinition Enabled="true" Id="{GUID}" RefName="Empty try-catch" RefId="{GUID}"/>
      //   <RuleDefinition Enabled="true" Id="{GUID}" RefName="Try-catch vs. throw exception" RefId="{GUID}"/>
      // </CategoryDefinition>
      XmlNode categoryDefinitionNode = doc.CreateNode(XmlNodeType.Element, "CategoryDefinition", null);
      XmlAttribute     idAttrib      = doc.CreateAttribute("Id");
      XmlAttribute     enabledAttrib = doc.CreateAttribute("Enabled");
      XmlAttribute     refNameAttrib = doc.CreateAttribute("RefName");
      XmlAttribute     refIdAttrib   = doc.CreateAttribute("RefId");

      idAttrib.Value      = definition.Id.ToString();
      enabledAttrib.Value = definition.Enabled.ToString();
      refNameAttrib.Value = definition.CategoryDeclarationReferenceName;
      refIdAttrib.Value   = definition.CategoryDeclarationReferenceId + "";

      categoryDefinitionNode.Attributes.Append(idAttrib);
      categoryDefinitionNode.Attributes.Append(enabledAttrib);
      categoryDefinitionNode.Attributes.Append(refNameAttrib);
      categoryDefinitionNode.Attributes.Append(refIdAttrib);

      foreach (KeyValuePair<int, IRuleDefinition> pair in definition.Rules)
      {
        IRuleDefinition ruleDef = pair.Value;

        XmlNode ruleDefinitionNode = doc.CreateNode(XmlNodeType.Element, "RuleDefinition", null);
        XmlAttribute       id      = doc.CreateAttribute("Id");
        XmlAttribute       enabled = doc.CreateAttribute("Enabled");
        XmlAttribute       refName = doc.CreateAttribute("RefName");
        XmlAttribute       refId   = doc.CreateAttribute("RefId");

        id.Value      = ruleDef.Id.ToString();
        enabled.Value = ruleDef.Enabled.ToString();
        refName.Value = ruleDef.RuleDeclarationReferenceName;
        refId.Value   = ruleDef.RuleDeclarationReferenceId + "";

        ruleDefinitionNode.Attributes.Append(id);
        ruleDefinitionNode.Attributes.Append(enabled);
        ruleDefinitionNode.Attributes.Append(refName);
        ruleDefinitionNode.Attributes.Append(refId);

        categoryDefinitionNode.AppendChild(ruleDefinitionNode);
      }
      return categoryDefinitionNode;
    }
    #endregion

    #region Rule definitions...
    public static XmlNode CreateRuleDefinitionXmlNode(XmlDocument doc, IRuleDeclaration declaration, IRuleDefinition definition)
    {
      // <RuleDefinition Enabled="true" RefName="No explicit exception identifier" RefId="{6C021885-99A7-48D7-97DB-C1FE3242182A}" >
      //   <Description><CDATA ELEMENT /></Description>
      // </RuleDefinition>
      XmlNode ruleDefinitionXmlNode   = doc.CreateNode(XmlNodeType.Element, "RuleDefinition", null);
      XmlNode descriptionXmlNode      = doc.CreateNode(XmlNodeType.Element, "Description", null);
      XmlNode projectRelationXmlNode  = doc.CreateNode(XmlNodeType.Element, "ProjectDefinitionRelation", null);
      XmlNode categoryRelationXmlNode = doc.CreateNode(XmlNodeType.Element, "CategoryDeclarationRelation", null);
      XmlAttribute idAttrib           = doc.CreateAttribute("Id");
      XmlAttribute enabledAttrib      = doc.CreateAttribute("Enabled");
      XmlAttribute refNameAttrib      = doc.CreateAttribute("RefName");
      XmlAttribute refIdAttrib        = doc.CreateAttribute("RefId");
      
      idAttrib.Value      = definition.Id.ToString();
      enabledAttrib.Value = definition.Enabled.ToString();
      refNameAttrib.Value = definition.RuleDeclarationReferenceName;
      refIdAttrib.Value   = definition.RuleDeclarationReferenceId + "";
      
      ruleDefinitionXmlNode.Attributes.Append(idAttrib);
      ruleDefinitionXmlNode.Attributes.Append(enabledAttrib);
      ruleDefinitionXmlNode.Attributes.Append(refNameAttrib);
      ruleDefinitionXmlNode.Attributes.Append(refIdAttrib);

      XmlCDataSection cdataNameNode = doc.CreateCDataSection(declaration.Description);
      descriptionXmlNode.AppendChild(cdataNameNode);

      projectRelationXmlNode.InnerText  = definition.ParentDefinition.ParentDefinition.Name;
      categoryRelationXmlNode.InnerText = declaration.ParentDeclaration.Name;

      ruleDefinitionXmlNode.AppendChild(descriptionXmlNode);
      ruleDefinitionXmlNode.AppendChild(projectRelationXmlNode);
      ruleDefinitionXmlNode.AppendChild(categoryRelationXmlNode);
      return ruleDefinitionXmlNode;
    }
    #endregion

    #region Project definitions...
    public static XmlNode CreateProjectDefinitionXmlNode(XmlDocument doc, IProjectDefinition definition)
    {
      //  <ProjectDefinition Enabled="false" Name="Fundamental Building Blocks" Id="{D2A54030-4F7E-421D-BD0C-CC80896FE4D2}">
      //    <Directories />
      //    <Files />
      //    <CategoryDefinitions />
      //  </Project>
      XmlNode projectDefinitionNode = doc.CreateNode(XmlNodeType.Element, "ProjectDefinition", null);
      XmlAttribute    enabledAttrib = doc.CreateAttribute("Enabled");
      XmlAttribute    nameAttrib    = doc.CreateAttribute("Name");
      XmlAttribute    idAttrib      = doc.CreateAttribute("Id");

      enabledAttrib.Value = definition.Enabled.ToString();
      nameAttrib.Value    = definition.Name;
      idAttrib.Value      = definition.Id.ToString();

      projectDefinitionNode.Attributes.Append(enabledAttrib);
      projectDefinitionNode.Attributes.Append(nameAttrib);
      projectDefinitionNode.Attributes.Append(idAttrib);

      projectDefinitionNode.AppendChild(CreateDirectoriesDefinitionXmlNode(doc, definition));
      projectDefinitionNode.AppendChild(CreateFileDefinitionsXmlNode(doc, definition));
      projectDefinitionNode.AppendChild(CreateCategoryDefinitionsXmlNode(doc, definition));
      return projectDefinitionNode;
    }
    #endregion

    #endregion ..................................................... Definitions


    #region Matches ............................................................
    public static XmlNode CreateMatchXmlNode(XmlDocument doc, int id, IMatch match)
    {
      //throw new NotImplementedException();

      // <Match Id="" Name="" Open="">
      //   <File />
      //   <LineNumber />
      //   <RegExpName><CDATA ELEMENT></RegExpName>
      //   <RegExpDescription><CDATA ELEMENT></RegExpDescription>
      //   <RegExpExpression><CDATA ELEMENT></RegExpExpression>
      //   <RegExpSummary><CDATA ELEMENT></RegExpSummary>
      //   <Severity Value="">Fatal|Critical|Warning|Info</Severity>
      // </Match>

      XmlNode matchNode           = doc.CreateNode(XmlNodeType.Element, "Match",           null);
      XmlNode projectNode         = doc.CreateNode(XmlNodeType.Element, "Project",         null);
      XmlNode categoryNode        = doc.CreateNode(XmlNodeType.Element, "Category",        null);
      XmlNode rootDirectoryNode   = doc.CreateNode(XmlNodeType.Element, "RootDirectory",   null);
      XmlNode filenameNode        = doc.CreateNode(XmlNodeType.Element, "FileName",        null);
      XmlNode lineNumberNode      = doc.CreateNode(XmlNodeType.Element, "LineNumber",      null);
      XmlNode ruleNameNode        = doc.CreateNode(XmlNodeType.Element, "RuleName",        null);
      XmlNode ruleDescriptionNode = doc.CreateNode(XmlNodeType.Element, "RuleDescription", null);
      XmlNode ruleExpressionNode  = doc.CreateNode(XmlNodeType.Element, "RuleExpression",  null);
      XmlNode codeExtractNode     = doc.CreateNode(XmlNodeType.Element, "CodeExtract",     null);
      XmlNode severityNode        = doc.CreateNode(XmlNodeType.Element, "Severity",        null);

      IConfigurationProxy cfgProxy = ProxyHome.Instance.RetrieveConfigurationProxy(OutputKeyKeeper.Instance.AccessKey);

      // Match node...
      XmlAttribute idAttrib   = doc.CreateAttribute("Id");
      XmlAttribute nameAttrib = doc.CreateAttribute("Name");
      XmlAttribute openAttrib = doc.CreateAttribute("Open");

      idAttrib.Value   = id + "";
      nameAttrib.Value = match.Filename;
      openAttrib.Value = "false";

      matchNode.Attributes.Append(idAttrib);
      matchNode.Attributes.Append(nameAttrib);
      matchNode.Attributes.Append(openAttrib);


      IProjectDefinition   projectDefinition   = cfgProxy.Project(match.ProjectDefinitionRef.Id);
      ICategoryDeclaration categoryDeclaration = cfgProxy.CategoryDeclaration(match.RuleDeclarationRef.ParentDeclaration.Id);
      IRuleDeclaration     ruleDeclaration     = cfgProxy.RuleDeclarationFromRuleId(match.RuleDeclarationRef.Id);


      // File node and Line number node...
      projectNode.InnerText       = projectDefinition.Name;
      categoryNode.InnerText      = categoryDeclaration.Name;
      rootDirectoryNode.InnerText = match.RootDirectoryPath;
      filenameNode.InnerText      = match.Filename;
      lineNumberNode.InnerText    = match.LineNumber + "";


      // RegExpName node...
      XmlCDataSection cdataRegExpNameNode = doc.CreateCDataSection(ruleDeclaration.Name);
      ruleNameNode.AppendChild(cdataRegExpNameNode);

      // RegExpDescription node...
      XmlCDataSection cdataRegExpDescriptionNode = doc.CreateCDataSection(ruleDeclaration.Description);
      ruleDescriptionNode.AppendChild(cdataRegExpDescriptionNode);

      // RegExpExpression node...
      XmlCDataSection cdataRegExpExpression = doc.CreateCDataSection(ruleDeclaration.Expression);
      ruleExpressionNode.AppendChild(cdataRegExpExpression);

      // RegExpSummary node...
      XmlCDataSection cdataRegExpSummary = doc.CreateCDataSection(match.CodeExtract);
      codeExtractNode.AppendChild(cdataRegExpSummary);

      // Severity node...
      XmlAttribute severityValueAttrib = doc.CreateAttribute("Value");
      severityValueAttrib.Value        = (int)match.Severity + "";
      severityNode.InnerText           = match.Severity.ToString();
      severityNode.Attributes.Append(severityValueAttrib);

      // Add all nodes to the 'node' given in the arguments.
      matchNode.AppendChild(projectNode);
      matchNode.AppendChild(categoryNode);
      matchNode.AppendChild(ruleNameNode);
      matchNode.AppendChild(ruleDescriptionNode);
      matchNode.AppendChild(ruleExpressionNode);
      matchNode.AppendChild(rootDirectoryNode);
      matchNode.AppendChild(filenameNode);
      matchNode.AppendChild(lineNumberNode);
      matchNode.AppendChild(codeExtractNode);
      matchNode.AppendChild(severityNode);
      return matchNode;
    }
    #endregion ......................................................... Matches
  }
}