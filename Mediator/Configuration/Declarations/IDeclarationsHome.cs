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


  public interface IDeclarationsHome : IDisposable
  {
    #region Auto properties ....................................................
    string                                Version    { get; set; }
    Dictionary<int, ILanguageDeclaration> Languages  { get; set; }
    Dictionary<int, ICategoryDeclaration> Categories { get; set; }
    Dictionary<int, IRuleDeclaration>     Rules      { get; set; }
    #endregion ................................................. Auto properties


    #region Validation .........................................................
    bool IsSettingsValid();
    bool IsCategoryDeclared(string referenceName, int referenceId);
    #endregion ...................................................... Validation
  }
}