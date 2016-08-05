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
	using Mediator.Matches;


	internal class Factory : IMatchFactory
	{
		public IMatchInfoReferences MatchInfoReferenceFactory()
		{
			return new MatchInfoReferences();
		}

		public IMatch MatchFactory()
		{
			return new Match();
		}
	}
}