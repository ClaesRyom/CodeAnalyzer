// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Matches
{
	using System;
	using System.Collections.Generic;
	
	using Configuration;
	using Initialization;


	public interface IMatchProxy : IInitializer
	{
		#region Match access .......................................................
		/// <summary>
		/// Returns the total number of matches found during the search regardless 
		/// of project dependency, category, rule or severity.
		/// </summary>
		int MatchesFound();

		/// <summary>
		/// Returns all the matches that was found during the search regardless of 
		/// project dependency, category, rule or severity.
		/// 
		/// Note: All matches are returned as clones of the internal representation 
		/// of the match.
		/// </summary>
		List<IMatch> Matches();

		/// <summary>
		/// Returns the number of matches found in the project.
		/// </summary>
		/// <param name="id">Project ID.</param>
		/// <returns></returns>
		int MatchesFoundByProject(int id);

		/// <summary>
		/// Returns the matches found in a specific project.
		/// </summary>
		/// <param name="id">Project ID.</param>
		/// <returns></returns>
		List<IMatch> MatchesByProject(int id);

		/// <summary>
		/// Returns the number of matches found in the category.
		/// </summary>
		/// <param name="id">Category ID.</param>
		/// <returns></returns>
		int MatchesFoundByCategory(int id);

		/// <summary>
		/// Returns the matches found in a specific category.
		/// </summary>
		/// <param name="id">Category ID.</param>
		/// <returns></returns>
		List<IMatch> MatchesByCategory(int id);

		/// <summary>
		/// Returns the number of matches found using the specified rule.
		/// </summary>
		/// <param name="id">Rule ID.</param>
		/// <returns></returns>
    int MatchesFoundByRule(int id);

		/// <summary>
		/// Returns the matches found using the specified rule.
		/// </summary>
		/// <param name="id">Rule ID.</param>
		/// <returns></returns>
    List<IMatch> MatchesByRules(int id);

		/// <summary>
		/// Returns the number of matches found with the specified rule severity.
		/// </summary>
		/// <param name="severity">Defines a rules severity.</param>
		/// <returns></returns>
		int MatchesFoundBySeverity(RuleSeverity severity);

		/// <summary>
		/// Returns the matches found with the specified rule severity.
		/// </summary>
		/// <param name="severity">Defines a rules severity.</param>
		/// <returns></returns>
		List<IMatch> MatchesBySeverity(RuleSeverity severity);

		/// <summary>
		/// Returns the number of matches found in a root directory.
		/// </summary>
		/// <param name="path">Defines a root directory path.</param>
		/// <returns></returns>
		int MatchesFoundInDirectory(string path);

		/// <summary>
		/// Returns the matches found in a root directory.
		/// </summary>
		/// <param name="path">Defines a root directory path.</param>
		/// <returns></returns>
		List<IMatch> MatchesInDirectory(string path);

		/// <summary>
		/// Returns the number of matches found in the specified file.
		/// </summary>
		/// <param name="path">Defines the path of a file.</param>
		/// <returns></returns>
		int MatchesFoundInFile(string path);

		/// <summary>
		/// Returns the matches found in the specified file.
		/// </summary>
		/// <param name="path">Defines the path of a file.</param>
		/// <returns></returns>
		List<IMatch> MatchesInFile(string path);
		#endregion .................................................... Match access


		#region Add matches ........................................................
		void AddMatch(IMatch match);
		#endregion ..................................................... Add matches


    #region Factory ............................................................
	  T MatchesFactory<T>(Type type);
		#endregion ......................................................... Factory	
	}
}