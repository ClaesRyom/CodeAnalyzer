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


  public interface ILanguageDeclaration : ICloneable
  {
    #region Auto properties ....................................................
    int    Id        { get; set; }
    string Name      { get; set; }
    string Extension { get; set; }
    #endregion ................................................. Auto properties
  }
}