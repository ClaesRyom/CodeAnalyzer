namespace CodeAnalyzer.Statistics
{
  using System;
  using System.Collections.Generic;

  using Mediator.Statistics;


  internal class RootDirectoryStatistics : IRootDirectoryStatistics
  {
    #region Interface IRootDirectoryStatistics impl. ...........................
    public int          Id            { get; set; }
    public string       RootDirectory { get; set; }
    public List<string> Filenames     { get; set; }
    #endregion ........................ Interface IRootDirectoryStatistics impl.


    #region Construction .......................................................
    public RootDirectoryStatistics()
    {
      Filenames = new List<string>();
    }
    #endregion .................................................... Construction
  }
}