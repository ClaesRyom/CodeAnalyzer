// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Coordination
{
  using System;
  using Config;
  using DataAccess;
  using Engine;
  using Fbb.Output;
  using Matches;
  using Mediator.Configuration;
  using Mediator.DataAccess;
  using Mediator.Engine;
  using Mediator.Factories;
  using Mediator.Identifiers;
  using Mediator.Label;
  using Mediator.Managers;
  using Mediator.Matches;
  using Mediator.Output;
  using Mediator.Proxies;
  using Mediator.Statistics;
  using Output;
  using Statistics;


  public class ApplicationInitializer
  {
    #region Instance Variables .................................................
    private Zone _Log = null;
    #endregion .............................................. Instance Variables


    #region Initialize Configuration Component Proxy ...........................
    public IConfigurationProxy InitializeConfigurationComponentProxy(IApplicationManager appManager)
    {
      IConfigurationProxy proxy = ConfigurationComponentProxy.Instance;
      ((AbstractComponentProxy)proxy).InitializeComponentAccessPermissions(appManager);
      //((IComponentProxy)proxy).Start();
      return proxy;
    }
    #endregion ........................ Initialize Configuration Component Proxy


    #region Initialize Configuration Component Factory .........................
    public IComponentFactory InitializeConfigurationComponentFactory(IApplicationManager appManager)
    {
      var factory = ConfigurationComponentFactory.Instance;
      factory.InitializeComponentAccessPermissions(appManager);
      return factory;
    }
    #endregion ...................... Initialize Configuration Component Factory


    #region Initialize Data Access Component ...................................
    public IDataAccessProxy InitializeDataAccessComponent(IApplicationManager appManager)
		{
			IDataAccessProxy proxy = DataAccessComponentProxy.Instance;
			((AbstractComponentProxy)proxy).InitializeComponentAccessPermissions(appManager);
      //((IComponentProxy)proxy).Start();
			return proxy;
		}
		#endregion ................................ Initialize Data Access Component


		#region Initialize Statistics Management ...................................
		public IStatisticsProxy InitializeStatisticsComponent(IApplicationManager appManager)
    {
      IStatisticsProxy proxy = StatisticsComponentProxy.Instance;
      ((AbstractComponentProxy)proxy).InitializeComponentAccessPermissions(appManager);
      //((IComponentProxy)proxy).Start();
      return proxy;
    }
    #endregion ................................ Initialize Statistics Management


    #region Initialize Engine Component ........................................
    public IEngineProxy InitializeEngineComponent(IApplicationManager appManager)
    {
      IEngineProxy proxy = EngineComponentProxy.Instance;
      ((AbstractComponentProxy)proxy).InitializeComponentAccessPermissions(appManager);
      //((IComponentProxy)proxy).Start();
      return proxy;
    }
    #endregion ..................................... Initialize Engine Component


    #region Initialize Matches Component .......................................
    public IMatchProxy InitializeMatchComponent(IApplicationManager appManager)
    {
      IMatchProxy proxy = MatchComponentProxy.Instance;
      ((AbstractComponentProxy)proxy).InitializeComponentAccessPermissions(appManager);
      //((IComponentProxy)proxy).Start();
      return proxy;
    }
    #endregion .................................... Initialize Matches Component


    #region Initialize Output Component ........................................
    public IOutputProxy InitializeOutputComponent(IApplicationManager appManager)
    {
      IOutputProxy proxy = OutputComponentProxy.Instance;
      ((AbstractComponentProxy)proxy).InitializeComponentAccessPermissions(appManager);
      //((IComponentProxy)proxy).Start();
      return proxy;
    }
    #endregion ..................................... Initialize Output Component


    #region Logging ............................................................
    public Zone Log
    {
      get { return _Log ?? (_Log = Out.ZoneFactory("" + Ids.REGION_APP_INITIALIZATION, GetType().Name)); }
    }
    #endregion ......................................................... Logging
  }
}