// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Security
{
  public interface IKey
  {
    #region Auto properties ....................................................
    byte[] Key { get; }
    #endregion ................................................. Auto properties 


    #region Output stuff .......................................................
    string ToString();
    #endregion .................................................... Output stuff
  }
}