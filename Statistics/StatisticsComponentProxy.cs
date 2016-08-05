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
  using System.Collections.Generic;
  using System.Diagnostics;

  using Fbb.Exceptions.Base;
  using Mediator.Exceptions.Statistics;
  using Mediator.Identifiers;
  using Mediator.Proxies;
  using Mediator.Security;
  using Mediator.Statistics;


  public sealed class StatisticsComponentProxy : AbstractComponentProxy, IStatisticsProxy
  {
    #region Instance Variable(s) ...............................................
    private static volatile StatisticsComponentProxy s_instance     = null;
    private static readonly object                   s_lockInstance = new object();
    #endregion ............................................ Instance Variable(s)


    #region Construction .......................................................
    private StatisticsComponentProxy()
    {
    }
    #endregion .................................................... Construction


    #region Singleton impl. ....................................................
    public static StatisticsComponentProxy Instance
    {
      get
      {
        lock (s_lockInstance)
        {
          return s_instance ?? (s_instance = new StatisticsComponentProxy());
        }
      }
    }
    #endregion ................................................. Singleton impl.


    #region Interface IComponentProxy impl. ....................................
    protected override void InitializeKeyKeeper(ComponentAccessKey componentAccessKey)
    {
      KeyKeeper = StatisticsKeyKeeper.InitializeInstance(componentAccessKey);
    }

    public override void Initialize()
    {
      lock (ManagerLock)
      {
        try
        {
          Manager = new StatisticsManager();
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


    #region Interface IStatisticsProxy impl. ...................................
		public DateTime StartTimeStamp
		{
			get
			{
				CheckManagerForNullReferenceBeforeUse();

				try
				{
					return ((StatisticsManager)Manager).StartTimeStamp;
				}
				catch (BaseException baseException)
				{
					throw new StatisticsComponentException(this, -1, string.Format("Failed to retrieve start time stamp from the '{0}'.", Manager.GetType().Name), baseException);
				}
			}
		}

	  public void StartTimer()
	  {
			CheckManagerForNullReferenceBeforeUse();

			try
			{
				((StatisticsManager)Manager).StartTimer();
			}
			catch (BaseException baseException)
			{
				throw new StatisticsComponentException(this, -1, "Failed to initialize the overall application timer.", baseException);
			}
		}

	  public void SplitTimer(string text)
	  {
			CheckManagerForNullReferenceBeforeUse();

			try
			{
				((StatisticsManager)Manager).SplitTimer(text);
			}
			catch (BaseException baseException)
			{
				throw new StatisticsComponentException(this, -1, "Failed to add a \'split time\' to the overall application timer.", baseException);
			}
		}

	  public void SplitTimer(int time, string text)
	  {
			CheckManagerForNullReferenceBeforeUse();

			try
			{
				((StatisticsManager)Manager).SplitTimer(time, text);
			}
			catch (BaseException baseException)
			{
				throw new StatisticsComponentException(this, -1, "Failed to add a \'split time\' to the overall application timer.", baseException);
			}
		}

	  public void StopTimer()
	  {
			CheckManagerForNullReferenceBeforeUse();

			try
			{
				((StatisticsManager)Manager).StopTimer();
			}
			catch (BaseException baseException)
			{
				throw new StatisticsComponentException(this, -1, "Failed to stop the overall application timer.", baseException);
			}
		}

		public string ExtractTimerMeasurings()
		{
			CheckManagerForNullReferenceBeforeUse();

			try
			{
				return ((StatisticsManager) Manager).ExtractTimerMeasurings();
			}
			catch (BaseException baseException)
			{
				throw new StatisticsComponentException(this, -1, "Failed to extract the overall application timers measurements?", baseException);
			}
		}
		
	  public void IncrementCounter(CounterIds ids)
    {
      CheckManagerForNullReferenceBeforeUse();

      try
      {
        ((StatisticsManager)Manager).IncrementCounter(ids);
      }
      catch (BaseException baseException)
      {
        throw new StatisticsComponentException(this, -1, string.Format("Failed to increment counter '{0}'.", ids), baseException);
      }
    }

	  public void IncrementCounter(CounterIds ids, int num)
    {
      CheckManagerForNullReferenceBeforeUse();

      try
      {
        ((StatisticsManager)Manager).IncrementCounter(ids, num);
      }
      catch (BaseException baseException)
      {
        throw new StatisticsComponentException(this, -1, string.Format("Failed to increment counter '{0}' with '{1}'.", ids, num), baseException);
      }
    }

    public int ReadCounter(CounterIds ids)
    {
      CheckManagerForNullReferenceBeforeUse();

      try
      {
        return ((StatisticsManager)Manager).ReadCounter(ids);
      }
      catch (BaseException baseException)
      {
        throw new StatisticsComponentException(this, -1, string.Format("Failed to read counter '{0}'.", ids), baseException);
      }
    }

    public IRootDirectoryStatistics CreateRootDirectory(int id)
    {
      CheckManagerForNullReferenceBeforeUse();

      if (id <= 0)
        throw new StatisticsComponentException(this, -1, "The argument 'id' for method 'CreateRootDirectory' cannot be less than or equal to '0'.");

      try
      {
        return ((StatisticsManager) Manager).CreateRootDirectory(id);
      }
      catch (BaseException baseException)
      {
        throw new StatisticsComponentException(this, -1, string.Format("Failed to create an instance of 'IRootDirectoryStatistics' with id = '{0}'.", id), baseException);
      }
    }

    public List<IRootDirectoryStatistics> GetRootDirectoriesFromId(int id)
    {
      CheckManagerForNullReferenceBeforeUse();

      if (id <= 0)
        throw new StatisticsComponentException(this, -1, "The argument 'id' for method 'GetRootDirectoriesFromId' cannot be less than or equal to '0'.");

      try
      {
        return ((StatisticsManager)Manager).GetRootDirectoriesFromId(id);
      }
      catch (BaseException baseException)
      {
        throw new StatisticsComponentException(this, -1, string.Format("Failed to retrieve any instances of 'IRootDirectoryStatistics' with id = '{0}'.", id), baseException);
      }
    }

    public List<IRootDirectoryStatistics> GetRootDirectoriesFromIdAndPath(int id, string path)
    {
      CheckManagerForNullReferenceBeforeUse();

      if (id <= 0) 
        throw new StatisticsComponentException(this, -1, "The argument 'id' for method 'GetRootDirectoriesFromIdAndPath' cannot be less than or equal to '0'.");
      if (string.IsNullOrWhiteSpace(path)) 
        throw new StatisticsComponentException(this, -1, "The argument 'path' for method 'GetRootDirectoriesFromIdAndPath' cannot be equal to 'null' or 'WhiteSpace'.");

      try
      {
        return ((StatisticsManager)Manager).GetRootDirectoriesFromIdAndPath(id, path);
      }
      catch (BaseException baseException)
      {
        throw new StatisticsComponentException(this, -1, string.Format("Failed to retrieve any instances of 'IRootDirectoryStatistics' with id = '{0}'.", id), baseException);
      }
    }
    #endregion ................................ Interface IStatisticsProxy impl.
    

    #region Helper methods .....................................................
    private void CheckManagerForNullReferenceBeforeUse()
    {
      if (Manager == null)
      {
        StackTrace st = new StackTrace();

        string s = string.Format("Property 'Manager' was NOT initialized before calling method '{0}' on '{1}'.", st.GetFrame(1).GetMethod().Name, GetType().Name);
        throw new StatisticsComponentException(this, -1, s);
      }
    }
    #endregion .................................................. Helper methods
  }
}