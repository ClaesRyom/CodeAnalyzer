// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Config.Definitions
{
	using System.Collections.Generic;
	using Mediator.Configuration.Definitions;


	internal sealed class CategoryDefinition : ICategoryDefinition
  {
    #region Auto properties ....................................................
    public int                              Id                               { get; set; }
    public bool                             Enabled                          { get; set; }
    public IProjectDefinition               ParentDefinition                 { get; set; }
    public Dictionary<int, IRuleDefinition> Rules                            { get; set; }

    public int                              CategoryDeclarationReferenceId   { get; set; }
    public string                           CategoryDeclarationReferenceName { get; set; }
    #endregion ................................................. Auto properties


    #region Construction .......................................................
    public CategoryDefinition()
    {
      Rules = new Dictionary<int, IRuleDefinition>();
    }
    #endregion .................................................... Construction
  }
}