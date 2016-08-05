// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Configuration.Definitions
{
  using Mediator.Configuration.Definitions;


  internal sealed class RuleDefinition : IRuleDefinition
  {
    #region Auto properties ....................................................
    public int                 Id                           { get; set; }
    public bool                Enabled                      { get; set; }
    public ICategoryDefinition ParentDefinition             { get; set; }
    public int                 RuleDeclarationReferenceId   { get; set; }
    public string              RuleDeclarationReferenceName { get; set; }
    #endregion ................................................. Auto properties
  }
}