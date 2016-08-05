// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------

using Fbb.Util.TimeTracking;


namespace CodeAnalyzer.Statistics
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using Fbb.Output;
  using Mediator.Identifiers;
  using Mediator.Managers;
  using Mediator.Statistics;


  internal sealed class StatisticsManager : AbstractManager 
  {
    #region Instance Variable(s) ...............................................
    private readonly object _CounterLock               = new object();
		private readonly object _FileAndDirectoryStatsLock = new object();
		private readonly object _ApplicationTimerLock      = new object();
		#endregion ............................................ Instance Variable(s)


    #region Auto properties ....................................................
    private Dictionary<CounterIds, int>    Counters                { get; set; }
    private List<IRootDirectoryStatistics> RootDirectoryStatistics { get; set; }
		private TimeTracker                    ApplicationTimer        { get; set; }
    public  DateTime                       StartTimeStamp          { get; private set; }
    #endregion ................................................. Auto properties


    #region Construction .......................................................
    public StatisticsManager()
    {
    }
    #endregion .................................................... Construction


    #region Increament counter. ................................................
    public void IncrementCounter(CounterIds ids)
    {
      lock (_CounterLock)
      {
        if (!Counters.ContainsKey(ids))
          return;

        Counters[ids]++;
      }
    }

    public void IncrementCounter(CounterIds ids, int num)
    {
      lock (_CounterLock)
      {
        if (!Counters.ContainsKey(ids))
          return;

        Counters[ids] += num;
      }
    }
    #endregion .............................................. Increament counter
    

    #region Read counters ......................................................
    public int ReadCounter(CounterIds ids)
    {
      lock (_CounterLock)
      {
        if (!Counters.ContainsKey(ids))
          return 0;

        return Counters[ids];
      }
    }
    #endregion ................................................... Read counters


		#region Overall application timer ..........................................
	  public void StartTimer()
	  {
		  lock (_ApplicationTimerLock)
		  {
			  ApplicationTimer = new TimeTracker(Ids.APPLICATION_TIMER_INIT);
				ApplicationTimer.Start();
		  }
	  }

	  public void SplitTimer(string text)
	  {
			lock (_ApplicationTimerLock)
			{
				ApplicationTimer.Split(text);
			}
		}

	  public void SplitTimer(int time, string text)
	  {
			lock (_ApplicationTimerLock)
			{
				ApplicationTimer.Split(time, text);
			}
		}

	  public void StopTimer()
	  {
			lock (_ApplicationTimerLock)
			{
				ApplicationTimer.Stop(Ids.APPLICATION_TIMER_WE_ARE_DONE);
			}
		}

	  public string ExtractTimerMeasurings()
	  {
		  lock (_ApplicationTimerLock)
		  {
			  return ApplicationTimer.ToString();
		  }
	  }
		#endregion ....................................... Overall application timer
		

    #region File and Directory statistics ......................................
    public IRootDirectoryStatistics CreateRootDirectory(int id)
    {
      lock (_FileAndDirectoryStatsLock)
      {
        RootDirectoryStatistics rds = new RootDirectoryStatistics { Id = id };
        RootDirectoryStatistics.Add(rds);
        return rds;
      }
    }

    public List<IRootDirectoryStatistics> GetRootDirectoriesFromId(int id)
    {
      lock (_FileAndDirectoryStatsLock)
      {
        return RootDirectoryStatistics.Where(rootDirectoryStats => rootDirectoryStats.Id == id).ToList();
      }
    }

    public List<IRootDirectoryStatistics> GetRootDirectoriesFromIdAndPath(int id, string path)
    {
      lock (_FileAndDirectoryStatsLock)
      {
        return RootDirectoryStatistics.Where(rootDirectoryStats => rootDirectoryStats.Id == id && (string.Compare(rootDirectoryStats.RootDirectory, path, StringComparison.OrdinalIgnoreCase))==0).ToList();
      }
    }
    #endregion ................................... File and Directory statistics


    #region Abstract class AbstractManager impl. ...............................
    public override void Initialize()
    {
      StartTimeStamp = DateTime.Now;

      Counters                = new Dictionary<CounterIds, int>();
      RootDirectoryStatistics = new List<IRootDirectoryStatistics>();

      // Create all the counters defined by the enum 'CounterIds'.
      foreach (CounterIds val in Enum.GetValues(typeof(CounterIds)))
      {
        Counters.Add(val, 0);
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

    protected override void Dispose(bool disposing)
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
        //if (Counters != null)
        //{
        //  Counters.Dispose();
        //}

        // Call the appropriate methods to clean up  
        // unmanaged resources here. 
        // If disposing is false,  
        // only the following code is executed.
      }

      // Indicate that the instance has been disposed.
      //Home = null;
      //RulesDoc = null;
      Disposed = true;
    }
    #endregion ............................ Abstract class AbstractManager impl.


    #region Logging ............................................................
    protected override Zone Log
    {
      get { return _log ?? (_log = Out.ZoneFactory("" + Ids.REGION_STATISTICS, GetType().Name)); }
    }
    #endregion ......................................................... Logging
  }
}
