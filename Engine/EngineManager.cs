// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------

using CodeAnalyzer.Mediator.DataAccess;
using CodeAnalyzer.Mediator.Engine;

namespace CodeAnalyzer.Engine
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Threading.Tasks;

  using Fbb.Output;
  using Fbb.Util.IO;
  using Fbb.Util.TimeTracking;
  using Mediator;
  using Mediator.Configuration;
  using Mediator.Configuration.Declarations;
  using Mediator.Configuration.Definitions;
  using Mediator.Exceptions.Engine;
  using Mediator.Identifiers;
  using Mediator.Label;
  using Mediator.Managers;
  using Mediator.Matches;
  using Mediator.Statistics;


	internal sealed class EngineManager : AbstractManager
  {
    #region Inner class(es) ....................................................
    private class LinkFile2Language
    {
      public List<string>         Filenames { get; set; }
      public ILanguageDeclaration Language  { get; set; }

      public LinkFile2Language()
      {
        Filenames = new List<string>();
      }
    }
    #endregion ................................................. Inner class(es)


    #region Construction .......................................................
    public EngineManager()
    {
    }
    #endregion .................................................... Construction


    #region Properties .........................................................
    protected override Zone Log
    {
      get { return _log ?? Out.ZoneFactory(Ids.REGION_ENGINE, GetType().Name); }
    }
    #endregion ...................................................... Properties


    #region Do the actual search ...............................................
    private List<LinkFile2Language> BindProjectLanguagesToFileTypes(IProjectDefinition project)
    {
      IConfigurationProxy configProxy = ProxyHome.Instance.RetrieveConfigurationProxy(EngineKeyKeeper.Instance.AccessKey);


      // Find all the language Id's that are referenced in the current projects
      // rule definitions.
      List<int> languagesInPlay = project.RetrieveLanguageIdsFromRules();


      // Create the list of link classes (LinkFile2Language) - linking 'Languages' and 'files'
      // When searching for different type of files the files found will have to be hold in 
      // collections according to their language type; ex. all csharp files with the extension
      // *.cs will be added to the list of file names in an instance from the class 
      // LinkFile2Language where the Language is set to 'C#' or 'csharp'.
      return languagesInPlay.Select(id => new LinkFile2Language { Language = configProxy.LanguageDeclaration(id) }).ToList();
    }

    private void StartSearch()
    {

      IConfigurationProxy cfgProxy = ProxyHome.Instance.RetrieveConfigurationProxy(EngineKeyKeeper.Instance.AccessKey);

      List<LinkFile2Language> bindedProj2LanguageFileType = null;
			
      // Retrieve project definitions from the Configuration component.
      Dictionary<int, IProjectDefinition> projects = cfgProxy.Projects();

      // Let's run through all the projects and do our thing.
      foreach (KeyValuePair<int, IProjectDefinition> pair in projects)
      {
        IProjectDefinition project = pair.Value;
        if (!project.Enabled)
          continue; // Project definition was disabled.
        

        // Here we create the language file type containers for all the languages 
        // that are in play for the current project.
        bindedProj2LanguageFileType = BindProjectLanguagesToFileTypes(project);
        

        // Find all files associated with the language file extension in 
        // the current file container 'linkFile2Language'.
        foreach (LinkFile2Language linkFile2Language in bindedProj2LanguageFileType)
        {
          foreach (IDirectoryDefinition dir in project.Directories)
          {
						if (!dir.Enabled)
							continue;

            if (!Directory.Exists(dir.Path))
            {
              string s = string.Format("No directory found ({0})", dir.Path);
              throw new IOException(s);
            }

            IRootDirectoryStatistics rds = ProxyHome.Instance.RetrieveStatisticsProxy(EngineKeyKeeper.Instance.AccessKey).CreateRootDirectory(project.Id);
            rds.RootDirectory = dir.Path;

            FileSearchEngine fileSearchEngine = new FileSearchEngine(dir, linkFile2Language.Language.Extension);
            fileSearchEngine.ExcludeDirs = project.ExcludedDirectories.Select(d => d).ToList();
            fileSearchEngine.IncludeSubDirsInSearch = true;
						fileSearchEngine.FileFoundDuringSearch.AddRange(project.Files.Select(f => f.Path));
						fileSearchEngine.ExcludeFiles.AddRange(project.ExcludedFiles.Select((f => f)));
            fileSearchEngine.Search();

            // Adding all the files found with the extention given by 
            // 'linkFile2Language.Language.Extension' to the file container 'linkFile2Language'.
            linkFile2Language.Filenames.AddRange(fileSearchEngine.FileFoundDuringSearch);

            // Adding all the files found to the StatisticsComponentProxy.
            ProxyHome.Instance.RetrieveStatisticsProxy(EngineKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.Files, fileSearchEngine.FileCount);
            rds.Filenames.AddRange(fileSearchEngine.FileFoundDuringSearch);
          }
        }


        TimeTracker tt = new TimeTracker("Execution of regular expression on each file.");
        tt.Start();


	      IMatchProxy matchProxy = ProxyHome.Instance.RetrieveMatchProxy(EngineKeyKeeper.Instance.AccessKey);

        // Let's execute each regular expression from each rule that are enabled 
        // and should be applied to the current language.
        foreach (LinkFile2Language linkFile2Language in bindedProj2LanguageFileType)
        {
          foreach (KeyValuePair<int, ICategoryDefinition> categoryDefinition in project.Categories)
          {
            if (!categoryDefinition.Value.Enabled)
              continue;


            foreach (KeyValuePair<int, IRuleDefinition> ruleDefinition in categoryDefinition.Value.Rules)
            {
              if (!ruleDefinition.Value.Enabled)
                continue;


              // Let's check whether or not the current 'rule' is associated with the current 'language'?
              IRuleDeclaration ruleDeclaration = cfgProxy.RuleDeclarationFromCategoryIdAndRuleId(categoryDefinition.Value.CategoryDeclarationReferenceId, ruleDefinition.Value.RuleDeclarationReferenceId);
              foreach (KeyValuePair<int, ILanguageDeclaration> languageRef in ruleDeclaration.Languages)
              {
                if (languageRef.Key == linkFile2Language.Language.Id)
                {
                  // The language reference on the current rule is identical to the current 'linkFile2Language'
                  // meaning that we should execute the regular expression from the current rule on all the 
                  // files placed in the 'linkFile2Language':
	                IMatchInfoReferences references = matchProxy.MatchesFactory<IMatchInfoReferences>(typeof(IMatchInfoReferences));

                  references.ProjectDefinitionReference   = project;
                  references.CategoryDeclarationReference = cfgProxy.CategoryDeclaration(categoryDefinition.Value.CategoryDeclarationReferenceId);
									references.CategoryDefinitionReference  = categoryDefinition.Value;
	                references.RuleDeclarationReference     = ruleDeclaration;
									references.RuleDefinitionReference      = ruleDefinition.Value;
	                references.LanguageDeclarationReference = languageRef.Value;

                  Parallel.ForEach(linkFile2Language.Filenames, file => ExecuteRegularExpressionsOnBuffer(file, references, ruleDeclaration));
                }
              }
            }
          }
        }

        tt.Stop("We are done.");
        tt.ToLog(Log);
      }
    }

    private string LoadFile(string filename)
    {
      FileAccesser fa = new FileAccesser(filename);
      fa.Load();
      return fa.InputBuffer;
    }

    private void ExecuteRegularExpressionsOnBuffer(string filename, IMatchInfoReferences references, IRuleDeclaration ruleDeclaration)
    {
      const string summary = "summary";

      string buffer = LoadFile(filename);

      if (string.IsNullOrEmpty(buffer))
        return;

	    int lastLineNum;
			string[] lines = buffer.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
	    lastLineNum = (lines != null && lines.Length >= 1) ? lines.Length - 1 : 0;


			#region Statistics: update/increment the total number of lines and total number of whitespace lines in tested/scanned files.
			if (!ProxyHome.Instance.CheckCache(filename))
	    {
		    ProxyHome.Instance.RetrieveStatisticsProxy(EngineKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.TotalNumberOfLinesInFiles, lastLineNum);
		    ProxyHome.Instance.RetrieveStatisticsProxy(EngineKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.TotalNumberOfWhitespaceLinesInFiles, lines.Count(string.IsNullOrWhiteSpace));
				ProxyHome.Instance.InsertInCache(filename);
	    }
	    #endregion


			string correctedRegExp = "(?<" + summary + ">" + ruleDeclaration.Expression + ")";


	    IMatchProxy matchProxy = ProxyHome.Instance.RetrieveMatchProxy(EngineKeyKeeper.Instance.AccessKey);
      IBatch      batch      = ProxyHome.Instance.RetrieveExecutionIdentification(EngineKeyKeeper.Instance.AccessKey);

      MatchCollection matches = Regex.Matches(buffer, correctedRegExp, RegexOptions.IgnoreCase);
      foreach (System.Text.RegularExpressions.Match match in matches)
      {

        int startIndx = match.Index;
//        int endIndx   = startIndx + match.Groups[summary].Value.Length;
        int endIndx   = startIndx + (match.Length - 1);

        int accumulation    = 0;
        int startLineNumber = 0;
        int endLineNumber   = 0;
        foreach (string line in lines)
        {
          if (accumulation < startIndx)
          {
	          // This is for finding the line number where the match starts...
            accumulation += line.Length + Environment.NewLine.Length;
						endLineNumber = startLineNumber++;
          }
          else
          {
            if (accumulation < endIndx)
            {
              // This is for finding the line number where the match ends...
              accumulation += line.Length + Environment.NewLine.Length;

	            if ((endLineNumber + 1) < lastLineNum)
	            {
		            endLineNumber++;
	            }
	            else
	            {
		            endLineNumber = lastLineNum;

								// Extract the code from the actual file...
								string matchExtract = ExtractTheMatchAndSurroundings(lines, buffer.Length, ++startLineNumber, ++endLineNumber);

								// Create the match and insert it into the MatchProxy...
								GenerateMatch(matchProxy, references, batch, filename, startLineNumber, matchExtract);

								// Let's update the statistics manager about the match we just found.
								ProxyHome.Instance.RetrieveStatisticsProxy(EngineKeyKeeper.Instance.AccessKey).IncrementCounter(RuleSeverityMapper.Severity2CounterId((RuleSeverity)ruleDeclaration.Severity));
							}
            }
            else 
            {
							// Extract the code from the actual file...
							string matchExtract = ExtractTheMatchAndSurroundings(lines, buffer.Length, ++startLineNumber, ++endLineNumber);

							// Create the match and insert it into the MatchProxy...
							GenerateMatch(matchProxy, references, batch, filename, startLineNumber, matchExtract);

							// Let's update the statistics manager about the match we just found.
							ProxyHome.Instance.RetrieveStatisticsProxy(EngineKeyKeeper.Instance.AccessKey).IncrementCounter(RuleSeverityMapper.Severity2CounterId((RuleSeverity)ruleDeclaration.Severity));
              break;
            }
          }
        }
      }
    }

		private void GenerateMatch(IMatchProxy matchProxy, IMatchInfoReferences references, IBatch batch, string filename, int startLineNumber, string matchExtract)
		{
			IMatch m = matchProxy.MatchesFactory<IMatch>(typeof(IMatch));
			m.Id                     = Guid.NewGuid();
			m.Batch                  = batch;
			m.ProjectDefinitionRef   = references.ProjectDefinitionReference;
			m.CategoryDeclarationRef = references.CategoryDeclarationReference;
			m.CategoryDefinitionRef  = references.CategoryDefinitionReference;
			m.LanguageDeclarationRef = references.LanguageDeclarationReference;
			m.RuleDeclarationRef     = references.RuleDeclarationReference;
			m.RootDirectoryPath      = @"N/A";
			m.Filename               = filename;
			m.CodeExtract            = matchExtract;
			m.LineNumber             = startLineNumber;
			m.Result                 = MatchStatus.Match;
			m.Severity               = references.RuleDeclarationReference.Severity;

			matchProxy.AddMatch(m);
		}

		private string ExtractTheMatchAndSurroundings(string[] lines, int sizeOfLines, int lineNumStartOfMatch, int lineNumEndOfMatch)
    {
			bool injectLineNumbers = Settings.Engine.InsertLineNumbersInCodeSummary;
			bool extractWholeFile  = false;
      int  beforeMatch       = 0;
      int  afterMatch        = 0;
			int  lineStart         = 0;
			int  lineEnd           = 0;


			switch (Settings.Engine.CodeExtract)
			{
				case CodeExtractType.Match:
				{
					break;
				}
				case CodeExtractType.Lines:
				{
					beforeMatch = Settings.Engine.LinesBeforeMatch;
					afterMatch  = Settings.Engine.LinesAfterMatch;
					break;
				}
				case CodeExtractType.File:
				{
					extractWholeFile  = true;
					break;
				}
				default:
				{
					break;
				}
			}


      // Find the start line number...
      if (extractWholeFile  ||  (lineNumStartOfMatch-1 - beforeMatch <= 0))
      {
        // Extract from first line...
        lineStart = 0;
      }
      else
      {
        // Extract from line = (lineNumStartOfMatch - beforeMatch)
        lineStart = lineNumStartOfMatch - 1 - beforeMatch;
      }


      // Find the end line number...
      if (extractWholeFile  ||  (lineNumEndOfMatch - 1 + afterMatch >= lines.Length - 1))
      {
        // Extract to the last line...
        lineEnd = lines.Length - 1;
      }
      else
      {
        // Extract to line = lineNumEndOfMatch + afterMatch
        lineEnd = lineNumEndOfMatch - 1 + afterMatch;
      }


      int sizeOfLargestLineNum = lineEnd.ToString().Length; // Largest line number in character length.
	    int sizeOfStrbuilder		 = sizeOfLines*2; //+(numOfLines * charsAddedToEachLine * sizeOfEachChar);

      // Calculate the size of the string builder in order to ensure that it will  
      // NOT result in a OutOfMemoryException.
      StringBuilder sb = new StringBuilder(sizeOfStrbuilder);

      // Do the actual extraction...
      for (int i = lineStart; i <= lineEnd; i++)
      {
        if (injectLineNumbers)
        {
          string lineNumber = (i + 1).ToString().PadLeft(sizeOfLargestLineNum);
          //string s = "{0," + sizeOfLargestLineNum + "}";
          //string lineNumber = string.Format(s, i+1); // (i + 1).ToString().PadLeft(sizeOfLargestLineNum);
          if (i == lineNumStartOfMatch - 1 && (lineNumStartOfMatch == lineNumEndOfMatch))
          {
            sb.Append("BE>");
          }
          else if (i == lineNumStartOfMatch - 1)
          {
            sb.Append("B >");
          }
          else if (i == lineNumEndOfMatch - 1)
          {
            sb.Append("E >");
          }
          else
          {
            sb.Append("   ");
          }
          sb.Append(lineNumber).Append("│  ").Append(lines[i]).Append(Environment.NewLine);
        }
        else
        {
          sb.Append(lines[i] + Environment.NewLine);
        }
      }
      return sb.ToString();
    }
    #endregion ............................................ Do the actual search

    
    #region Interface IManager impl. ...........................................
    public override void Initialize()
    {
    }

    public override void Start()
    {
      try
      {
        // Let's do the initialization...
        Initialize();
      }
      catch (Exception e)
      {
        string s = string.Format("Unexpected behaviour detected during the search!");
        Log.Error(s, e);

        throw new EngineComponentException(this, -1, s, e);
      }

      
      try
      {
        // Let's start the search for code constructions that matches our rules...
        StartSearch();
      }
      catch (Exception e)
      {
        string s = string.Format("Unexpected behaviour detected during the search!");
        Log.Error(s, e);

        throw new EngineComponentException(this, -1, s, e);
      }
    }

    public override void Stop()
    {
      Dispose(true);
    }
    #endregion ........................................ Interface IManager impl.


    #region Implementation of IDisposable ......................................
    protected override void Dispose(bool disposing)
    {
      // If you need thread safety, use a lock around these  
      // operations, as well as in your methods that use the resource. 
      if (Disposed)
        return;

      // If disposing equals true, dispose all managed  
      // and unmanaged resources. 
      if (disposing)
      {
        //// Dispose managed resources.
        //if (Home != null)
        //{
        //  Home.Dispose();
        //}

        // Call the appropriate methods to clean up  
        // unmanaged resources here. 
        // If disposing is false,  
        // only the following code is executed.
      }

      // Indicate that the instance has been disposed.
      Disposed = true;
    }
    #endregion ................................... Implementation of IDisposable
  }
}
