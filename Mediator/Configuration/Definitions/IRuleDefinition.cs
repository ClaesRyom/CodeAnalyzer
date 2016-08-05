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


  public interface IRuleDefinition
  {
    #region Auto properties ....................................................
    int                 Id                           { get; set; }
    bool                Enabled                      { get; set; }
    ICategoryDefinition ParentDefinition             { get; set; }
    string              RuleDeclarationReferenceName { get; set; }
    int                 RuleDeclarationReferenceId   { get; set; }
    #endregion ................................................. Auto properties
  }
}