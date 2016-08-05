// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2014 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------

using System.Collections.Generic;

namespace CodeAnalyzer.Mediator.DataAccess
{
  using Matches;


  public interface IMatchDataAccessProxy
  {
    void CreateMatch(IMatch match);
    void ReadMatch(int id);
    void UpdateMatch(IMatch match);
    void DeleteMatch(int id);
    void DeleteMatch(IMatch match);

	  void CreateBulkOfMatches(List<IMatch> matches);
  }
}