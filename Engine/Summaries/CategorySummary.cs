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
  using Mediator.Configuration;
  using Mediator.Engine;
  using Mediator.Engine.Summaries;


  public struct CategorySummary : ICategorySummary
  {
    #region Auto properties ....................................................
    public int          Id                    { get; set; }
    public string       Name                  { get; set; }
    public string       Description           { get; set; }
    public RuleSeverity Severity              { get; set; }
    public int          Count                 { get; set; }
    public string       LinkToCategoryXmlFile { get; set; }
    #endregion ................................................. Auto properties
  }
}