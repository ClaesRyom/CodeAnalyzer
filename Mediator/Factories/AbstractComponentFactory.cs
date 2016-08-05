// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Factories
{
  using System;

  using Managers;
  using Proxies;
  using Security;


  public abstract class AbstractComponentFactory : IComponentFactory, IKeyConsumer
  {
    #region Auto properties ....................................................
    private   bool       Disposed  { get; set; }
    protected IKeyKeeper KeyKeeper { get; set; }
    #endregion ................................................. Auto properties


    #region Manager related properties .........................................
    protected IApplicationManager AppManager { get; private set; }
    #endregion ...................................... Manager related properties


    #region Construction .......................................................
    protected AbstractComponentFactory()
    {
      Disposed = false;
    }

    protected abstract void InitializeKeyKeeper(ComponentAccessKey componentAccessKey);

    public void InitializeComponentAccessPermissions(IApplicationManager appManager)
    {
      if (appManager == null)
        throw new ArgumentNullException("appManager");

      AppManager = appManager;

      InitializeKeyKeeper(GetComponentAccessKey());
    }
    #endregion .................................................... Construction


    #region Destruction ........................................................
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

    // Dispose(bool disposing) executes in two distinct scenarios. 
    // If disposing equals true, the method has been called directly 
    // or indirectly by a user's code. Managed and unmanaged resources 
    // can be disposed. 
    // If disposing equals false, the method has been called by the  
    // runtime from inside the finalizer and you should not reference  
    // other objects. Only unmanaged resources can be disposed. 
    protected virtual void Dispose(bool disposing)
    {
      // If you need thread safety, use a lock around these  
      // operations, as well as in your methods that use the resource. 
      if (Disposed) 
        return;

      // If disposing equals true, dispose all managed  
      // and unmanaged resources. 
      if (disposing)
      {
        // Dispose managed resources.

        // Call the appropriate methods to clean up  
        // unmanaged resources here. 
        // If disposing is false,  
        // only the following code is executed.
      }

      // Indicate that the instance has been disposed.
      Disposed = true;
    }
    #endregion ..................................................... Destruction


    #region Interface IKeyConsumer impl. .......................................
    public ComponentAccessKey GetComponentAccessKey()
    {
      return AppManager.ProvideComponentAccessKey(this) as ComponentAccessKey;
    }
    #endregion .................................... Interface IKeyConsumer impl.
    

    #region Interface IComponentProxy impl. ....................................
    public abstract void Initialize();
    #endregion ................................. Interface IComponentProxy impl.


    #region Interface IComponentFactory impl. ..................................
    public abstract T ConfigurationFactory<T>(Type type);
    #endregion ............................... Interface IComponentFactory impl.
  }
}