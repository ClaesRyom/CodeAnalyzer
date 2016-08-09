// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2012 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator
{
  using System.Configuration;

  using Configuration;
  using Exceptions.Configuration;
  using Engine;
  using Output;
  

  public class Settings 
  {
    public class Configuration
    {
      /// <summary>
      /// The StorageType defines whether configuration data such as declarations and definitions should be loaded from XML configuration file(s) or from a database. If the 'StorageType' is set to 'Db' the configuration data will be loaded from the database. If the 'StorageType' is set to 'Xml' the configuration data will be loaded from XML file(s). Note: If 'StorageType' is set to 'Db' then a valid connection string to the data must be specified. The 'StorageType' field is case sensitive. Valid values are either 'Db' or 'Xml'.
      /// </summary>
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
      public static StorageType StorageType
      {
        get
        {
          string s = ConfigurationManager.AppSettings["Configuration.StorageType"];
          if (string.IsNullOrEmpty(s))
            throw new ConfigurationComponentException("Configuration key 'StorageType' from the group 'Configuration' was either null or empty.");

          StorageType result;
          if (!StorageType.TryParse(s.Trim(), out result))
            throw new ConfigurationComponentException("Unable to parse the configuration key 'StorageType' from the group 'Configuration'.");

          return result;
        }
      }

    }

    public class Report
    {
      /// <summary>
      /// Enables or disables the generation of the report. If 'True' is specified a report will be generated and if 'False' is specified no report will be generated. The type of report is specified in the 'Report.OutputType' attribute and the output directory is specified in the 'Report.OutputDir' attribute.
      /// </summary>
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
      public static bool OutputEnabled
      {
        get
        {
          string s = ConfigurationManager.AppSettings["Report.OutputEnabled"];
          if (string.IsNullOrEmpty(s))
            throw new ConfigurationComponentException("Configuration key 'OutputEnabled' from the group 'Report' was either null or empty.");

          bool result;
          if (!bool.TryParse(s.Trim(), out result))
            throw new ConfigurationComponentException("Unable to parse the configuration key 'OutputEnabled' from the group 'Report'.");

          return result;
        }
      }

      /// <summary>
      /// Directory for where the output files should be placed. If '*' is specified the applications execution directory will be used. This field is mandatory.
      /// </summary>
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
      public static string OutputDir
      {
        get
        {
          string s = ConfigurationManager.AppSettings["Report.OutputDir"];
          if (string.IsNullOrEmpty(s))
            throw new ConfigurationComponentException("Configuration key 'OutputDir' from the group 'Report' was either null or empty.");

          return s.Trim();
        }
      }

      /// <summary>
      /// Two output types are supported for the report, web and Excel. Setting the 'OutputType' to 'WEB' will result in a web page (index.html) that will encapsulate the resulting report. Setting the 'OutputType' to 'EXCEL' will result in an Excel file encapsulating the resulting report. This field is mandatory.
      /// </summary>
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
      public static ReportOutputType OutputType
      {
        get
        {
          string s = ConfigurationManager.AppSettings["Report.OutputType"];
          if (string.IsNullOrEmpty(s))
            throw new ConfigurationComponentException("Configuration key 'OutputType' from the group 'Report' was either null or empty.");

          ReportOutputType result;
          if (!ReportOutputType.TryParse(s.Trim(), out result))
            throw new ConfigurationComponentException("Unable to parse the configuration key 'OutputType' from the group 'Report'.");

          return result;
        }
      }

    }

    public class Engine
    {
      /// <summary>
      /// Valid assignments are 'Match', 'Lines' and 'File'. 
      /// </summary>
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
      public static CodeExtractType CodeExtract
      {
        get
        {
          string s = ConfigurationManager.AppSettings["Engine.CodeExtract"];
          if (string.IsNullOrEmpty(s))
            throw new ConfigurationComponentException("Configuration key 'CodeExtract' from the group 'Engine' was either null or empty.");

          CodeExtractType result;
          if (!CodeExtractType.TryParse(s.Trim(), out result))
            throw new ConfigurationComponentException("Unable to parse the configuration key 'CodeExtract' from the group 'Engine'.");

          return result;
        }
      }

      /// <summary>
      /// When the search engine finds a match in a file it will extract the code from the source file. The value of 'LinesBeforeMatch' tells the search engine how many line before the match should be extracted.
      /// </summary>
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
      public static int LinesBeforeMatch
      {
        get
        {
          string s = ConfigurationManager.AppSettings["Engine.LinesBeforeMatch"];
          if (string.IsNullOrEmpty(s))
            throw new ConfigurationComponentException("Configuration key 'LinesBeforeMatch' from the group 'Engine' was either null or empty.");

          int result;
          if (!int.TryParse(s.Trim(), out result))
            throw new ConfigurationComponentException("Unable to parse the configuration key 'LinesBeforeMatch' from the group 'Engine'.");

          return result;
        }
      }

      /// <summary>
      /// When the search engine finds a match in a file it will extract the code from the source file. The value of 'LinesAfterMatch' tells the search engine how many line after the match should be extracted.
      /// </summary>
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
      public static int LinesAfterMatch
      {
        get
        {
          string s = ConfigurationManager.AppSettings["Engine.LinesAfterMatch"];
          if (string.IsNullOrEmpty(s))
            throw new ConfigurationComponentException("Configuration key 'LinesAfterMatch' from the group 'Engine' was either null or empty.");

          int result;
          if (!int.TryParse(s.Trim(), out result))
            throw new ConfigurationComponentException("Unable to parse the configuration key 'LinesAfterMatch' from the group 'Engine'.");

          return result;
        }
      }

      /// <summary>
      /// Settings the value of 'InsertLineNumbersInCodeSummary' to 'true' will tell the search engine to alter each line in the resulting code summary in each found match in such a way that a line number is injected in the beginning of each line. Setting it to 'false' and this feature will be disabled.
      /// </summary>
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
      public static bool InsertLineNumbersInCodeSummary
      {
        get
        {
          string s = ConfigurationManager.AppSettings["Engine.InsertLineNumbersInCodeSummary"];
          if (string.IsNullOrEmpty(s))
            throw new ConfigurationComponentException("Configuration key 'InsertLineNumbersInCodeSummary' from the group 'Engine' was either null or empty.");

          bool result;
          if (!bool.TryParse(s.Trim(), out result))
            throw new ConfigurationComponentException("Unable to parse the configuration key 'InsertLineNumbersInCodeSummary' from the group 'Engine'.");

          return result;
        }
      }

    }

    public Settings()
    {
      StorageType storagetype = Configuration.StorageType;
      bool outputenabled = Report.OutputEnabled;
      string outputdir = Report.OutputDir;
      ReportOutputType outputtype = Report.OutputType;
      CodeExtractType codeextract = Engine.CodeExtract;
      int linesbeforematch = Engine.LinesBeforeMatch;
      int linesaftermatch = Engine.LinesAfterMatch;
      bool insertlinenumbersincodesummary = Engine.InsertLineNumbersInCodeSummary;
    }
  }
}
