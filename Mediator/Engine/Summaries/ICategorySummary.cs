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
  using Configuration;


  public interface ICategorySummary
  {
    #region Auto properties ....................................................
    int          Id                    { get; set; }
    string       Name                  { get; set; }
    string       Description           { get; set; }
    RuleSeverity Severity              { get; set; }
    int          Count                 { get; set; }
    string       LinkToCategoryXmlFile { get; set; }
    #endregion ................................................. Auto properties
  }
}