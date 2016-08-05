// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Matches
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Fbb.Output;
	using Mediator;
	using Mediator.Configuration;
	using Mediator.DataAccess;
	using Mediator.Exceptions.Matches;
	using Mediator.Identifiers;
	using Mediator.Managers;
	using Mediator.Matches;


	internal class MatchManager : AbstractManager 
  {
    #region Internal enums .....................................................
    private enum Filter
    {
      Project,
      Category,
      Rule,
      RootDir,
      Filename,
      Severity,
    }
    #endregion .................................................. Internal enums


    #region Instance Variables .................................................
    private readonly object _lockInternalMatches = new object();
    #endregion .............................................. Instance Variables


		#region AbstractManager Property overrides .................................
		protected override Zone Log
		{
			get { return _log ?? Out.ZoneFactory(Ids.REGION_MATCHES, GetType().Name); }
		}
		#endregion .............................. AbstractManager Property overrides


		#region Auto properties ....................................................
    private List<IMatch> InternalMatches { get; set; }
		public  Factory      Factory         { get; private set; }
		#endregion ................................................. Auto properties

		
		#region Construction .......................................................
		public MatchManager()
    {
			Factory = new Factory();
		}
    #endregion .................................................... Construction


		#region Abstract class AbstractManager overrides ...........................
		public override void Initialize()
		{
			InternalMatches = new List<IMatch>();
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
				string s = string.Format("Unexpected behaviour detected during the initialization of the matches manager!");
				Log.Error(s, e);

				throw new MatchComponentException(this, -1, s, e);
			}
		}

		public override void Stop()
		{
			Dispose(true);
		}
		#endregion ........................ Abstract class AbstractManager overrides


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
			InternalMatches = null;
			Disposed        = true;
		}
		#endregion ................................... Implementation of IDisposable


    #region Matches access .....................................................
    /// <summary>
    /// Returns the number of matches found in the project.
    /// </summary>
    /// <param name="id">Project ID.</param>
    /// <returns></returns>
    public int MatchesFoundByProject(int id)
    {
      lock (_lockInternalMatches)
      {
        if (InternalMatches.Count <= 0)
          return 0;
        
        return FindNumberOfMatchesFromFilterAndId(Filter.Project, id);
      }
    }

    /// <summary>
    /// Returns the matches found in a specific project.
    /// </summary>
    /// <param name="id">Project ID.</param>
    /// <returns></returns>
    public List<IMatch> MatchesByProject(int id)
    {
      lock (_lockInternalMatches)
      {
        return RetrieveMatchesFromFilterAndId(Filter.Project, id);
      }
    }

    /// <summary>
    /// Returns the number of matches found in the category.
    /// </summary>
    /// <param name="id">Category ID.</param>
    /// <returns></returns>
    public int MatchesFoundByCategory(int id)
    {
      lock (_lockInternalMatches)
      {
        if (InternalMatches.Count <= 0)
          return 0;

        return FindNumberOfMatchesFromFilterAndId(Filter.Category, id);
      }
    }

    /// <summary>
    /// Returns the matches found in a specific category.
    /// </summary>
    /// <param name="id">Category ID.</param>
    /// <returns></returns>
    public List<IMatch> MatchesByCategory(int id)
    {
      lock (_lockInternalMatches)
      {
        return RetrieveMatchesFromFilterAndId(Filter.Category, id);
      }
    }

    /// <summary>
    /// Returns the number of matches found using the specified rule.
    /// </summary>
    /// <param name="id">Rule ID.</param>
    /// <returns></returns>
    public int MatchesFoundByRule(int id)
    {
      lock (_lockInternalMatches)
      {
        if (InternalMatches.Count <= 0)
          return 0;

        return FindNumberOfMatchesFromFilterAndId(Filter.Rule, id);
      }
    }

    /// <summary>
    /// Returns the matches found using the specified rule.
    /// </summary>
    /// <param name="id">Rule ID.</param>
    /// <returns></returns>
    public List<IMatch> MatchesByRules(int id)
    {
      lock (_lockInternalMatches)
      {
        return RetrieveMatchesFromFilterAndId(Filter.Rule, id);
      }
    }

    /// <summary>
    /// Returns the number of matches found with the specified rule severity.
    /// </summary>
    /// <param name="severity">Defines a rules severity.</param>
    /// <returns></returns>
    public int MatchesFoundBySeverity(RuleSeverity severity)
    {
      lock (_lockInternalMatches)
      {
        if (InternalMatches.Count <= 0)
          return 0;

        return InternalMatches.Count(match => match.Severity == severity);
      }
    }

    /// <summary>
    /// Returns the matches found with the specified rule severity.
    /// </summary>
    /// <param name="severity">Defines a rules severity.</param>
    /// <returns></returns>
    public List<IMatch> MatchesBySeverity(RuleSeverity severity)
    {
      lock (_lockInternalMatches)
      {
        return (from match in InternalMatches where match.Severity == severity select match.Clone() as IMatch).ToList();
      }
    }

    /// <summary>
    /// Returns the number of matches found in a root directory.
    /// </summary>
    /// <param name="path">Defines a root directory path.</param>
    /// <returns></returns>
    public int MatchesFoundInDirectory(string path)
    {
      if (InternalMatches.Count <= 0)
        return 0;

      return FindNumberOfMatchesFromFilterAndPath(Filter.RootDir, path);
    }

    /// <summary>
    /// Returns the matches found in a root directory.
    /// </summary>
    /// <param name="path">Defines a root directory path.</param>
    /// <returns></returns>
    public List<IMatch> MatchesInDirectory(string path)
    {
      lock (_lockInternalMatches)
      {
        return RetrieveMatchesFromFilterAndPath(Filter.RootDir, path);
      }
    }

    /// <summary>
    /// Returns the number of matches found in the specified file.
    /// </summary>
    /// <param name="path">Defines the path of a file.</param>
    /// <returns></returns>
    public int MatchesFoundInFile(string path)
    {
      if (InternalMatches.Count <= 0)
        return 0;

      return FindNumberOfMatchesFromFilterAndPath(Filter.Filename, path);
    }

    /// <summary>
    /// Returns the matches found in the specified file.
    /// </summary>
    /// <param name="path">Defines the path of a file.</param>
    /// <returns></returns>
    public List<IMatch> MatchesInFile(string path)
    {
      lock (_lockInternalMatches)
      {
        return RetrieveMatchesFromFilterAndPath(Filter.Filename, path);
      }
    }


    #region Helpers: Matches filter methods
    private int FindNumberOfMatchesFromFilterAndId(Filter filter, int id)
    {
      if (id <= 0)
        throw new ArgumentException("Argument 'id' cannot be less than or equal to '0'.");

      switch (filter)
      {
        case Filter.Rule: { return InternalMatches.Count(match => match.RuleDeclarationRef.Id == id); }
        default: return 0;
      }
    }

    private int FindNumberOfMatchesFromFilterAndPath(Filter filter, string path)
    {
      if (string.IsNullOrWhiteSpace(path))
        throw new ArgumentNullException("path");

      switch (filter)
      {
        case Filter.RootDir:  { return InternalMatches.Count(match => match.RootDirectoryPath == path); }
        case Filter.Filename: { return InternalMatches.Count(match => match.Filename == path); }
        default: return 0;
      }
    }

    private List<IMatch> RetrieveMatchesFromFilterAndId(Filter filter, int id)
    {
      if (id <= 0)
        throw new ArgumentException("Argument 'id' cannot be less than or equal to '0'.");

      switch (filter)
      {
        case Filter.Rule: { return (from match in InternalMatches where match.RuleDeclarationRef.Id == id select match.Clone() as IMatch).ToList(); }
        default: return null;
      }
    } 

    private List<IMatch> RetrieveMatchesFromFilterAndPath(Filter filter, string path)
    {
      if (string.IsNullOrWhiteSpace(path))
        throw new ArgumentNullException("path");

      switch (filter)
      {
        case Filter.RootDir:  { return (from match in InternalMatches where string.Compare(match.RootDirectoryPath, path, StringComparison.OrdinalIgnoreCase)==0 select match.Clone() as IMatch).ToList(); }
        case Filter.Filename: { return (from match in InternalMatches where string.Compare(match.Filename, path, StringComparison.OrdinalIgnoreCase) == 0 select match.Clone() as IMatch).ToList(); }
        default: return null;
      }
    }
    #endregion
    #endregion .................................................. Matches access


    #region Add matches ........................................................
    public void AddMatch(IMatch match)
    {
      lock (_lockInternalMatches)
      {
        if (match == null)
          throw new ArgumentNullException("match");

        if (match.Result != MatchStatus.Match)
          return;

        InternalMatches.Add(match);
      }
    }

		/// <summary>
		/// Returns the total number of matches found during the search regardless of categories or severity.
		/// </summary>
		public int NumberOfMatchesFound()
		{
			lock (_lockInternalMatches)
			{
				return InternalMatches.Count;
			}
		}

		/// <summary>
		/// Returns all the matches that was found during the search regardless of categories or severity.
		/// 
		/// Note: All matches returned are clones of the original matches.
		/// </summary>
		public List<IMatch> Matches()
		{
			lock (_lockInternalMatches)
			{
				return InternalMatches.Select(match => (IMatch)match.Clone()).ToList();
			}
		}
		#endregion ..................................................... Add matches
  }
}
