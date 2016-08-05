// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Engine.Summaries
{
  using System.Collections.Generic;

  using Configuration;
  using Matches;


	public interface IRuleSummary
  {
    #region Auto properties ....................................................
    int           Id                 { get; set; }
    bool          Enabled            { get; set; }
    int           Count              { get; set; }
    string        Name               { get; set; }
    string        Description        { get; set; }
    RuleSeverity  Severity           { get; set; }
    string        Expression         { get; set; }
    string        LinkToMatchXmlFile { get; set; }
    List<IMatch>  Matches            { get; set; }
    #endregion ................................................. Auto properties
  }
}