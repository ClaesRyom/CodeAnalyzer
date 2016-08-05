// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Engine.Summaries
{
  using System.Collections.Generic;

  using Mediator.Configuration;
  using Mediator.Engine;
  using Mediator.Engine.Summaries;
  using Mediator.Matches;


	public class RuleSummary : IRuleSummary
  {
    #region Auto properties ....................................................
    public int           Id                 { get; set; }
    public bool          Enabled            { get; set; }
    public int           Count              { get; set; }
    public string        Name               { get; set; }
    public string        Description        { get; set; }
    public RuleSeverity  Severity           { get; set; }
    public string        Expression         { get; set; }
    public string        LinkToMatchXmlFile { get; set; }
    public List<IMatch>  Matches            { get; set; }
    #endregion ................................................. Auto properties


    #region Construction .......................................................
    public RuleSummary()
    {
      Matches = new List<IMatch>();
    }
    #endregion .................................................... Construction
  }
}