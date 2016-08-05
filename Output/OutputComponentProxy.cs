// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------

using CodeAnalyzer.Mediator;

namespace CodeAnalyzer.Output
{
  using System;

  using Fbb.Exceptions.Base;
  using Mediator.Exceptions.Output;
  using Mediator.Exceptions.Statistics;
  using Mediator.Output;
  using Mediator.Proxies;
  using Mediator.Security;


	public sealed class OutputComponentProxy : AbstractComponentProxy, IOutputProxy
  {
    #region Instance Variable(s) ...............................................
    private static volatile OutputComponentProxy s_Instance    = null;
    private static readonly object               _LockInstance = new object();
    #endregion ............................................ Instance Variable(s)


    #region Construction .......................................................
    private OutputComponentProxy()
    {
    }
    #endregion .................................................... Construction


    #region Singleton impl. ....................................................
    public static OutputComponentProxy Instance
    {
      get
      {
        lock (_LockInstance)
        {
          return s_Instance ?? (s_Instance = new OutputComponentProxy());
        }
      }
    }
    #endregion ................................................. Singleton impl.


    #region Interface IComponentProxy impl. ....................................
    protected override void InitializeKeyKeeper(ComponentAccessKey componentAccessKey)
    {
      KeyKeeper = OutputKeyKeeper.InitializeInstance(componentAccessKey);
    }

    public override void Initialize()
    {
      lock (ManagerLock)
      {
        try
        {
          Manager = new OutputManager();
          Manager.Start();
        }
        catch (BaseException baseException)
        {
          throw new StatisticsComponentException(this, -1, string.Format("Unable to initialize '{0}", GetType().Name), baseException);
        }
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


    #region Interface IOutputProxy impl. .......................................
    public void GenerateReport(string dir, ReportOutputType type)
    {
      lock (ManagerLock)
      {
        if (string.IsNullOrWhiteSpace(dir))
          throw new ArgumentNullException("dir");

        try
        {
          ((OutputManager)Manager).GenerateOutput(dir, type);
        }
        catch (BaseException baseException)
        {
          throw new OutputComponentException(this, -1, string.Format("Unable to initialize '{0}", GetType().Name), baseException);
        }
      }
    }
    #endregion .................................... Interface IOutputProxy impl.
  }
}