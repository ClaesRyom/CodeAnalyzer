// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator
{
  using System;
  using System.IO;


  public class DirHandler
  {
    #region Instance Variables .................................................
    private static volatile DirHandler s_instance        = null;
    private static readonly object     s_lockInstance    = new object();
    private                 string     _currentDirectory = string.Empty;
    #endregion .............................................. Instance Variables


    #region Properties .........................................................
    public string CurrentDirectory
    {
      get { return _currentDirectory; } 
      set
      {
        if (string.IsNullOrWhiteSpace(value))
          throw new ArgumentNullException("value");

        string tmp = value;

        if (!Directory.Exists(tmp))
          throw new IOException(string.Format("Unable to find directory '{0}'.", tmp));

        _currentDirectory = tmp;
      }
    }
    #endregion ...................................................... Properties
    

    #region Construction .......................................................
    private DirHandler()
    {
    }
    #endregion .................................................... Construction


    #region Singleton impl. ....................................................
    public static DirHandler Instance
    {
      get
      {
        lock (s_lockInstance)
        {
          return s_instance ?? (s_instance = new DirHandler());
        }
      }
    }
    #endregion ................................................. Singleton impl.  
  }
}