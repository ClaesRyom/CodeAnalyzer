// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Engine
{
  using Fbb.Output;
  using Mediator.Engine;
  using Mediator.Identifiers;
  using Mediator.Proxies;
  using Mediator.Security;


	public class EngineComponentProxy : AbstractComponentProxy, IEngineProxy
  {
    #region Instance Variable(s) ...............................................
    private static volatile EngineComponentProxy s_instance    = null;
    private static readonly object               _lockInstance = new object();
    private        readonly Zone                 _log          = null;
    #endregion ............................................ Instance Variable(s)


    #region Private properties .................................................
    private Zone Log
    {
      get { return _log ?? Out.ZoneFactory(Ids.REGION_ENGINE, GetType().Name); }
    }
    #endregion .............................................. Private properties


    #region Construction .......................................................
    private EngineComponentProxy()
    {
    }
    #endregion .................................................... Construction


    #region Singleton impl. ....................................................
    public static EngineComponentProxy Instance
    {
      get
      {
        lock (_lockInstance)
        {
          return s_instance ?? (s_instance = new EngineComponentProxy());
        }
      }
    }
    #endregion ................................................. Singleton impl.


    #region Interface IComponentProxy impl. ....................................
    protected override void InitializeKeyKeeper(ComponentAccessKey componentAccessKey)
    {
      KeyKeeper = EngineKeyKeeper.InitializeInstance(componentAccessKey);
    }

    public override void Initialize()
    {
      lock (ManagerLock)
      {
				Log.Debug("{0}.Initialize(): - Begin", GetType().Name);

				Manager = new EngineManager();
        Manager.Start();

				Log.Debug("{0}.Initialize(): - End", GetType().Name);
			}
    }

    public override void Start()
    {
      Initialize();
    }

    public override void Stop()
    {
      Dispose();
    }
    #endregion ................................. Interface IComponentProxy impl.
  }
}