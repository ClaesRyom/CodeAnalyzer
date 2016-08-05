// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------

using System.Collections.Generic;

namespace CodeAnalyzer.DataAccess.Match
{
	using System;

	using Mediator.Matches;
	

	internal interface IMatchDataAccess
	{
		void CreateMatch(IMatch match);

		void ReadMatch(Guid id);

		void UpdateMatch(IMatch match);

		void DeleteMatch(Guid id);

		void DeleteMatch(IMatch match);

		void CreateBulkOfMatches(List<IMatch> matches);
	}
}