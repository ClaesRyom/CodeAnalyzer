// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Configuration
{
  using System;


  public interface IReference : ICloneable
  {
    #region Auto properties ....................................................
    string Name { get; set; }
    int    Id   { get; set; }
    #endregion ................................................. Auto properties
  }
}