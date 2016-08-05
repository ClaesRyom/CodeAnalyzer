// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Config.Declarations
{
	using System.Collections.Generic;
	using Mediator.Configuration.Declarations;


	internal sealed class CategoryDeclaration : ICategoryDeclaration
  {
    #region Interface ICategoryDeclaration impl. ...............................
    public int                               Id          { get; set; }
    public string                            Name        { get; set; }     
    public string                            Description { get; set; }
    public Dictionary<int, IRuleDeclaration> Rules       { get; set; }
    #endregion ............................ Interface ICategoryDeclaration impl.


    #region Construction .......................................................
    public CategoryDeclaration()
    {
      Rules = new Dictionary<int, IRuleDeclaration>();
    }
    #endregion .................................................... Construction


    #region Interface ICloneable impl. .........................................
    public object Clone()
    {
      CategoryDeclaration cd = new CategoryDeclaration();
      cd.Id = Id;
      cd.Name = (Name != null) ? (string) Name.Clone() : null;
      cd.Description = (Description != null) ? (string) Description.Clone() : null;

      foreach (KeyValuePair<int, IRuleDeclaration> valuePair in Rules)
      {
        cd.Rules.Add(valuePair.Key, valuePair.Value.Clone() as IRuleDeclaration);
      }
      return cd;
    }
    #endregion ...................................... Interface ICloneable impl.
  }
}