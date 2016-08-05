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
  using System;

  using Initialization;


  public interface IManager : IInitializer, IDisposable
  {
    #region Start & Stop managers ..............................................
    void Start();
    void Stop();
    #endregion ........................................... Start & Stop managers
  }
}
