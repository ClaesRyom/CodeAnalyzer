// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Statistics
{
  using System;
  using System.Collections.Generic;

  using Identifiers;


  public interface IStatisticsProxy
  {
    #region Auto properties ....................................................
    DateTime StartTimeStamp { get; }
    #endregion ................................................. Auto properties


		#region Overall application timer ..........................................
	  void StartTimer();
	  void SplitTimer(string text);
	  void SplitTimer(int time, string text);
	  void StopTimer();
	  string ExtractTimerMeasurings();
		#endregion ....................................... Overall application timer


		#region Increament counter. ................................................
		void IncrementCounter(CounterIds ids);
    void IncrementCounter(CounterIds ids, int num);
    #endregion .............................................. Increament counter


    #region Read counters ......................................................
    int ReadCounter(CounterIds ids);
    #endregion ................................................... Read counters


    #region File and Directory statistics ......................................
    IRootDirectoryStatistics       CreateRootDirectory(int id);
    List<IRootDirectoryStatistics> GetRootDirectoriesFromId(int id);
    List<IRootDirectoryStatistics> GetRootDirectoriesFromIdAndPath(int id, string path);
    #endregion ................................... File and Directory statistics
  }
}