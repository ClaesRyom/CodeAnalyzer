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


  public class Reference : IReference
  {
    #region Auto properties ....................................................
    public string Name { get; set; }
    public int    Id   { get; set; }
    #endregion ................................................. Auto properties


    #region Construction .......................................................
    public Reference() {}

    public Reference(string name, int id)
    {
      Name = name;
      Id   = id;
    }
    #endregion .................................................... Construction


    #region Interface ICloneable impl. .........................................
    public object Clone()
    {
      return new Reference
      {
        Id   = Id,
        Name = (Name != null) ? (string)Name.Clone() : null,
      };
    }
    #endregion ...................................... Interface ICloneable impl.
  }
}