// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2014 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.DataAccess
{
  using System;


  public interface IVersionDataAccessProxy
  {
    void   CreateVersionNumber();
    void   UpdateVersionNumber(Guid id, string newVersNum);
		string ReadVersionNumber(Guid id);
		void   DeleteVersionNumber(Guid id);
	}
}