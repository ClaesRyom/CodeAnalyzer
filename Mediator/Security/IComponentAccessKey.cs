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
  using Proxies;


  public interface IComponentAccessKey
  {
    #region Auto properties ....................................................
    IKeyConsumer KeyConsumer { get; }
    string       Description { get; }
    #endregion ................................................. Auto properties
  }
}