// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Configuration.Definitions
{
  using System;
  using System.Collections.Generic;


  public interface IDefinitionsHome : IDisposable
  {
    #region Auto properties ....................................................
    Dictionary<int, IProjectDefinition>  Projects   { get; set; }
    Dictionary<int, ICategoryDefinition> Categories { get; set; }
    Dictionary<int, IRuleDefinition>     Rules      { get; set; }
    #endregion ................................................. Auto properties


    #region Validation .........................................................
    bool IsSettingsValid();
    #endregion ...................................................... Validation
  }
}