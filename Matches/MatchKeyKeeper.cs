// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Matches
{
	using System;

	using Mediator.Exceptions;
	using Mediator.Security;


	internal sealed class MatchKeyKeeper : IKeyKeeper
  {
    #region Instance Variable(s) ...............................................
    private static          MatchKeyKeeper s_instance;
    private static readonly object         s_lockInstance = new object();
    #endregion ............................................ Instance Variable(s)


    #region Auto properties ....................................................
    private static bool IsInitialized { get; set; }

    public ComponentAccessKey AccessKey { get; private set; }
    #endregion ................................................. Auto properties


    #region Construction .......................................................
		private MatchKeyKeeper()
    {
    }
    #endregion .................................................... Construction


    #region Initialize .........................................................
		public static MatchKeyKeeper InitializeInstance(ComponentAccessKey componentAccessKey)
    {
      if (componentAccessKey == null)
        throw new ArgumentNullException("componentAccessKey");

      IsInitialized = true;

			MatchKeyKeeper keyKeeper = Instance;
      keyKeeper.AccessKey      = componentAccessKey;
      return keyKeeper;
    }
    #endregion ...................................................... Initialize


    #region Singleton Entry Point ..............................................
		public static MatchKeyKeeper Instance
    {
      get
      {
        lock (s_lockInstance)
        {
          if (!IsInitialized)
            throw new MissingInitializationException("Invalid initialization. Call 'InitializeInstance(ComponentAccessKey componentAccessKey)' with a valid component access key before calling this 'Instance' method.");

          return s_instance ?? (s_instance = new MatchKeyKeeper());
        }
      }
    }
    #endregion ........................................... Singleton Entry Point 
  }
}