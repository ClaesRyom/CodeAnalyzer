// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Managers
{
  using Proxies;
  using Security;


  public interface IApplicationManager
  {
    #region Provide components with access keys ................................
    ComponentAccessKey ProvideComponentAccessKey(IKeyConsumer keyConsumer);
    #endregion ............................. Provide components with access keys
  }
}