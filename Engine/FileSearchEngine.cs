// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Engine
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using Fbb.Exceptions.Base;
  using Fbb.Output;
  using Mediator;
  using Mediator.Configuration.Definitions;
  using Mediator.Exceptions;
  using Mediator.Exceptions.Engine;
  using Mediator.Identifiers;


  /// <summary>
  /// Providing the class with a directory and type of files to start the searching for the class
  /// will return a list of files found, that is all files matching the file extension in the 
  /// given directory as well as in all the subdirectories. 
  /// </summary>
  internal sealed class FileSearchEngine
  {
    #region Construction .......................................................
	  /// <summary>
	  /// Initializes a new instance of the <see cref="FileSearchEngine"/> class.
	  /// </summary>
	  /// <param name="dir">
	  /// The dir represents the starting point for the search started by <see cref="Search"/> method.
	  /// </param>
	  /// <param name="extension"> </param>
	  /// <exception cref="Fbb.Exceptions.General.InvalidArgumentException">
	  /// </exception>
	  public FileSearchEngine(IDirectoryDefinition dir, string extension)
    {
      if (dir == null)
      {
        throw new InvalidArgumentException("The argument 'dir' can not be null!");
      }

      if (string.IsNullOrEmpty(dir.Path))
      {
        throw new InvalidArgumentException("The argument 'dir.Path' can not be null or empty!");
      }

      if (string.IsNullOrEmpty(extension))
      {
        throw new InvalidArgumentException("The argument 'extension' can not be null or empty!");
      }

      DirCount               = 0;
      FileCount              = 0;
      Dir                    = dir;
      Extension              = extension;
      FileFoundDuringSearch  = new List<string>();
      ExcludeDirs            = new List<IDirectoryDefinition>();
      IncludeSubDirsInSearch = true;
      Log                    = Out.ZoneFactory(Ids.REGION_SEARCH, GetType().Name);
    }
    #endregion .................................................... Construction

 
    #region Properties .........................................................
    /// <summary>
    /// Gets DirCount. Represents the number of directories encountered 
    /// during the search.
    /// </summary>
    public int DirCount { get; private set; }

    /// <summary>
    /// Gets FileCount. Represents the number of files encountered during 
    /// the search.
    /// </summary>
    public int FileCount { get; private set; }

    /// <summary>
    /// Gets Dir. Represents the starting point for the search - must be 
    /// given in the constructor.
    /// </summary>
    public IDirectoryDefinition Dir { get; private set; }

    /// <summary>
    /// Gets FilterByExtension. The extension of the files that should be 
    /// retrieved through the search.
    /// </summary>
    public string Extension { get; private set; }

    /// <summary>
    /// Gets FileFoundDuringSearch. The resulting list of file names found during the search.
    /// </summary>
    public List<string> FileFoundDuringSearch { get; private set; }

    /// <summary>
    /// Gets or Sets ExcludeDirs. List of directories that must be excluded 
    /// from the search. Remember that the directory name are case sensitive!
    /// </summary>
    public List<IDirectoryDefinition> ExcludeDirs { get; set; }

    /// <summary>
    /// Gets or Sets IncludeSubDirsInSearch. Indicates whether or not directories
    /// beneath the directory given as starting point for the search should be
    /// included in the search.
    /// </summary>
    public bool IncludeSubDirsInSearch { get; set; }

    /// <summary>
    /// Gets or sets Log.
    /// </summary>
    private Zone Log { get; set; }
    #endregion ...................................................... Properties


    #region Searching ..........................................................
    /// <summary>
    /// Method for initiating the search for files described by the filterByExtension 
    /// and dir in the constructor.
    /// </summary>
    public void Search()
    {
      foreach (IDirectoryDefinition excludeDir in ExcludeDirs)
      {
        if (!excludeDir.Enabled)
          continue;

        if (string.Compare(excludeDir.Path, Dir.Path, StringComparison.OrdinalIgnoreCase) == 0)
        {
          // No need to initiate the search if the start Dir is also excluded.
          return;
        }
      }

      SearchInternal(Dir);
    }

    /// <summary>
    /// The internal search method - remember that the method is reqursive. 
    /// </summary>
    /// <param name="directoryDefinition">
    /// The directoryDefinition is the starting point for each call to the internal search method.
    /// </param>
    /// <exception cref="SearchException">
    /// </exception>
    private void SearchInternal(IDirectoryDefinition directoryDefinition)
    {
      string[] dirs = new string[0];
      string[] files;

			#region Get directories
			try
      {
        if (!Directory.Exists(directoryDefinition.Path))
        {
          return;
        }

        if (!directoryDefinition.Enabled)
        {
          return;
        }

        if (IncludeSubDirsInSearch)
        {
          dirs      = Directory.GetDirectories(directoryDefinition.Path);
          DirCount += dirs.Length;
        }
      }
      catch (UnauthorizedAccessException uae)
      {
        Log.Info("Unable to access directory {0} due to UnauthorizedAccessException. \n\n{1}", directoryDefinition, BaseException.Format(this, -1, "Unable to access directory " + directoryDefinition, uae));
        return;
      }
      catch (Exception exception)
      {
        string s = string.Format("Unable to retrieve directories from '{0}'.", directoryDefinition.Path);
        throw new SearchException(this, -1, s, exception);
			}
			#endregion


			#region Get files
			try
      {
        files = Directory.GetFiles(directoryDefinition.Path, Extension);
        FileCount += files.Length;
      }
      catch (Exception exception)
      {
        string s = string.Format("Unable to retrieve files from '{0}'.", directoryDefinition);
        throw new SearchException(this, -1, s, exception);
			}
			#endregion 


			#region Handle the found directories
			foreach (string dir in dirs)
      {
        bool exclude = false;
        foreach (IDirectoryDefinition excludedDir in ExcludeDirs)
        {
          if (!excludedDir.Enabled)
            continue;

          if (string.Compare(excludedDir.Path, dir, StringComparison.OrdinalIgnoreCase) == 0)
            exclude = true;
        }

        if (!exclude)
        {
          // Not excluded - let's continue...
	        IDirectoryDefinition dirDef = ProxyHome.Instance.RetrieveConfigurationFactory(EngineKeyKeeper.Instance.AccessKey).ConfigurationFactory<IDirectoryDefinition>(typeof(IDirectoryDefinition));
          dirDef.Enabled = true;
          dirDef.Path    = dir;

          SearchInternal(dirDef);
        }
			}
			#endregion


			#region Handle the found files
			foreach (string file in files)
      {
        FileFoundDuringSearch.Add(file);
			}
			#endregion
		}
    #endregion ....................................................... Searching
  }
}
