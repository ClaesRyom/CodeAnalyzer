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


  public interface IRootDirectoryStatistics
  {
    int          Id            { get; set; }
    string       RootDirectory { get; set; }
    List<string> Filenames     { get; set; }
  }
}