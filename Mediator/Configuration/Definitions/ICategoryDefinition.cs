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


  public interface ICategoryDefinition
  {
    #region Auto properties ....................................................
    int                              Id                               { get; set; }
    bool                             Enabled                          { get; set; }
    IProjectDefinition               ParentDefinition                 { get; set; }
    Dictionary<int, IRuleDefinition> Rules                            { get; set; }
    string                           CategoryDeclarationReferenceName { get; set; }
    int                              CategoryDeclarationReferenceId   { get; set; }
    #endregion ................................................. Auto properties
  }
}