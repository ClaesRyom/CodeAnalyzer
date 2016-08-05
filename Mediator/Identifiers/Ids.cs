// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Identifiers
{
  public class Ids
	{
		#region Application Timer Ids ..............................................
		public const string APPLICATION_TIMER_INIT        = @"Overall Application Timer";
		public const string APPLICATION_TIMER_WE_ARE_DONE = @"We are done measuring time, bye!";
		#endregion ........................................... Application Timer Ids


		#region Database Ids .......................................................
	  public const string DATABASE_VERSION_NUMBER = @"1.0.0.0";
		#endregion .................................................... Database Ids


		#region Application log regions ............................................
		public const string REGION_CONFIG             = @"CONFIG";
    public const string REGION_DATA_ACCESS        = @"DATA ACCESS";
    public const string REGION_ENGINE             = @"ENGINE";
    public const string REGION_OUTPUT             = @"OUTPUT";
    public const string REGION_MATCHES            = @"MATCHES";
    public const string REGION_SEARCH             = @"SEARCH";
    public const string REGION_STATISTICS         = @"STATISTICS";
    public const string REGION_APP_STARTER        = @"APP STARTER";
    public const string REGION_APP_INITIALIZATION = @"APP INITIALIZATION";
    #endregion ......................................... Application log regions


    #region Directories ........................................................

    #region Application Dirs...
    // Repository - where dirs and files are copied from to the output dir.
    // This must be in the execution dir and should therefore be created during 
    // installation or deployment!!!
    public const string REPOSITORY_DIR    = @"repository";
    public const string CONFIGURATION_DIR = @"configuration";
    public const string CONFIG_DIR        = @"config";
    #endregion
    

    #region Output Dirs...
    // Root output directory...
    public const string OUTPUT_DIR = @"Output";
    
    // Output sub-directories...
    public const string CSS_DIR         = @"css";
    public const string EXAMPLES_DIR    = @"examples";
    public const string IMAGES_DIR      = @"images";
    public const string JAVASCRIPT_DIR  = @"javascript";
    public const string XSLT_DIR        = @"xslt";
    public const string EXCEL_DIR       = @"excel";
    #endregion

    #endregion ..................................................... Directories


    #region Filenames ..........................................................
    public const string LOG_FILE        = @"analyser.log";
    public const string LOG_CONFIG_FILE = @"log.config";

    public const string RULES_CONFIG_FILE        = @"rules.config";
    public const string RULES_CONFIG_SCHEMA_FILE = @"rules.xsd";
    public const string INDEX_HTML_FILE          = @"index.html";
    public const string MATCHES_XSLT_FILE        = @"Matches.xslt";
    public const string TOC_FILE                 = @"TOC.xml";
    public const string TOC_XSLT_FILE            = @"TOC.xslt";
    public const string FILES_SEARCHED_XSLT_FILE = @"FilesSearched.xslt";
    public const string FILES_SEARCHED_XML_FILE  = @"FilesSearched.xml";
    public const string SUB_TOC_XSLT_FILE        = @"SubTOC.xslt";
    public const string SUB_TOC_XML_FILE         = @"SubTOC.xml";
    public const string SUMMARY_FILE             = @"Summary.xml";
    public const string SUMMARY_XSLT_FILE        = @"Summary.xslt";
    public const string COMMON_CSS_FILE          = @"common.css";
    public const string COMMON_JS_FILE           = @"common.js";
    public const string PLUS_PNG_FILE            = @"plus.png";
    public const string MINUS_PNG_FILE           = @"minus.png";

    public const string EXCEL_FILE = @"Code Analyzer Report.xlsx";

    public const string ALL_RULES_FILE                = @"AllRules.xml";
    public const string ALL_MATCHES_XML_FILE          = @"AllMatches.xml";
    public const string ALL_INFO_MATCHES_XML_FILE     = @"InfoMatches.xml";
    public const string ALL_WARNING_MATCHES_XML_FILE  = @"WarningMatches.xml";
    public const string ALL_CRITICAL_MATCHES_XML_FILE = @"CriticalMatches.xml";
    public const string ALL_FATAL_MATCHES_XML_FILE    = @"FatalMatches.xml";
    public const string ALL_MATCHES_XSLT_FILE         = @"AllMatches.xslt";

    public const string ALL_LANGUAGE_DECLARATIONS_XML_FILE  = @"LanguagesDeclarations.xml";
    public const string ALL_LANGUAGE_DECLARATIONS_XSLT_FILE = @"LanguagesDeclarations.xslt";

    public const string ALL_CATEGORY_DECLARATIONS_XML_FILE  = @"CategoryDeclarations.xml";
    public const string ALL_CATEGORY_DECLARATIONS_XSLT_FILE = @"CategoryDeclarations.xslt";

    public const string ALL_RULE_DECLARATIONS_XML_FILE      = @"RuleDeclarations.xml";
    public const string ALL_RULE_DECLARATIONS_XSLT_FILE     = @"RuleDeclarations.xslt";

    public const string ALL_PROJECT_DEFINITIONS_XML_FILE    = @"ProjectDefinitions.xml";
    public const string ALL_PROJECT_DEFINITIONS_XSLT_FILE   = @"ProjectDefinitions.xslt";

    public const string ALL_CATEGORY_DEFINITIONS_XML_FILE   = @"CategoryDefinitions.xml";
    public const string ALL_CATEGORY_DEFINITIONS_XSLT_FILE  = @"CategoryDefinitions.xslt";

    public const string ALL_RULE_DEFINITIONS_XML_FILE       = @"RuleDefinitions.xml";
    public const string ALL_RULE_DEFINITIONS_XSLT_FILE      = @"RuleDefinitions.xslt";
    #endregion ....................................................... Filenames


    #region Extensions .........................................................
    public const string XML_FILE_EXTENSION  = @".xml";
    public const string XSLT_FILE_EXTENSION = @".xslt";
    #endregion ...................................................... Extensions


    #region XML tags ...........................................................
    public const string XML_REPORT_BEGIN        = @"<Report>";
    public const string XML_REPORT_END          = @"</Report>";
    public const string SUMMARY_BEGIN           = @"  <CodeExtract>";
    public const string SUMMARY_END             = @"  </CodeExtract>";
    public const string NUM_OF_MATCHES_BEGIN    = @"    <NumOfMatches>";
    public const string NUM_OF_MATCHES_END      = @"</NumOfMatches>";
    public const string MATCHES_BEGIN           = @"  <Matches>";
    public const string MATCHES_END             = @"</Matches>";

    public const string TOC_BEGIN               = @"<Toc>";
    public const string TOC_END                 = @"</Toc>";
    public const string CATEGORIES_BEGIN        = @"  <CategoryDeclarations>";
    public const string CATEGORIES_END          = @"  </CategoryDeclarations>";

    public const string MATCH_BEGIN_1           = @"    <Match Id=""";
    public const string MATCH_BEGIN_2           = @""" Name=""";
    public const string MATCH_BEGIN_3           = @""" Open=""false"">";
    public const string MATCH_END               = @"    </Match>";

    public const string FILENAME_BEGIN          = @"      <File>";
    public const string FILENAME_END            = @"</File>";
    public const string LINE_NUMBER_BEGIN       = @"      <LineNumber>";
    public const string LINE_NUMBER_END         = @"</LineNumber>";
    public const string REGEXP_NAME_BEGIN       = @"      <RegExpName>";
    public const string REGEXP_NAME_END         = @"</RegExpName>";
    public const string REGEXP_DESC_BEGIN       = @"      <RegExpDescription>";
    public const string REGEXP_DESC_END         = @"</RegExpDescription>";
    public const string REGEXP_EXPRESSION_BEGIN = @"      <RegExpExpression>";
    public const string REGEXP_EXPRESSION_END   = @"</RegExpExpression>";
    public const string REGEXP_SUMMARY_BEGIN    = @"      <RegExpSummary>";
    public const string REGEXP_SUMMARY_END      = @"</RegExpSummary>";
    #endregion ........................................................ XML tags
  }
}
