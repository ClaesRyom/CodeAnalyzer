// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Statistics
{
  using System;

  using Mediator.Exceptions;
  using Mediator.Security;


  internal sealed class StatisticsKeyKeeper : IKeyKeeper
  {
    #region Instance Variable(s) ...............................................
    private static StatisticsKeyKeeper s_Instance = null;
    private static readonly object _LockInstance = new object();
    #endregion ............................................ Instance Variable(s)


    #region Auto properties ....................................................
    private static bool IsInitialized { get; set; }

    public ComponentAccessKey AccessKey { get; private set; }
    #endregion ................................................. Auto properties


    #region Construction .......................................................
    private StatisticsKeyKeeper()
    {
    }
    #endregion .................................................... Construction


    #region Initialize .........................................................
    public static StatisticsKeyKeeper InitializeInstance(ComponentAccessKey componentAccessKey)
    {
      if (componentAccessKey == null)
        throw new ArgumentNullException("componentAccessKey");

      IsInitialized = true;

      StatisticsKeyKeeper keyKeeper = Instance;
      keyKeeper.AccessKey = componentAccessKey;
      return keyKeeper;
    }
    #endregion ...................................................... Initialize


    #region Singleton Entry Point ..............................................
    public static StatisticsKeyKeeper Instance
    {
      get
      {
        lock (_LockInstance)
        {
          if (!IsInitialized)
            throw new MissingInitializationException("Invalid initialization. Call 'InitializeInstance(ComponentAccessKey componentAccessKey)' with a valid component access key before calling this 'Instance' method.");

          return s_Instance ?? (s_Instance = new StatisticsKeyKeeper());
        }
      }
    }
    #endregion ........................................... Singleton Entry Point
  }
}