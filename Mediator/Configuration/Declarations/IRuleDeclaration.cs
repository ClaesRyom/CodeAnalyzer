// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Configuration.Declarations
{
  using System;
  using System.Collections.Generic;


  public interface IRuleDeclaration : ICloneable
  {
    #region Auto properties ....................................................
    int                                   Id                { get; set; }
    string                                Name              { get; set; }
    string                                Description       { get; set; }
    RuleSeverity                          Severity          { get; set; }
    string                                Expression        { get; set; }
    Dictionary<int, ILanguageDeclaration> Languages         { get; set; }
    ICategoryDeclaration                  ParentDeclaration { get; set; }
    #endregion ................................................. Auto properties
  }
}