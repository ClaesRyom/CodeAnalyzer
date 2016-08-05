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

  using Fbb.Output;


  public abstract class AbstractManager : IManager
  {
    #region Instance Variables .................................................
    protected Zone _log = null;
    #endregion .............................................. Instance Variables


    #region Auto properties ....................................................
    protected bool Disposed { get; set; }
    #endregion ................................................. Auto properties


    #region Logging ............................................................
    protected abstract Zone Log { get; }
    #endregion ......................................................... Logging

    
    #region Interface IManager impl. ...........................................
    public abstract void Initialize();
    public abstract void Start();
    public abstract void Stop();
    #endregion ........................................ Interface IManager impl.


    #region Interface IDisposable impl. ........................................
    // Implement IDisposable. 
    // Do not make this method virtual. 
    // A derived class should not be able to override this method.
    public void Dispose()
    {
      Dispose(true);

      // This object will be cleaned up by the Dispose method. 
      // Therefore, you should call GC.SupressFinalize to 
      // take this object off the finalization queue  
      // and prevent finalization code for this object 
      // from executing a second time.
      GC.SuppressFinalize(this);
    }

    protected abstract void Dispose(bool disposing);
    #endregion ..................................... Interface IDisposable impl.
  }
}