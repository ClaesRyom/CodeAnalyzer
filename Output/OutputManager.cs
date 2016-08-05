// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Output
{
  using Fbb.Output;
  using Mediator;
  using Mediator.Identifiers;
  using Mediator.Managers;
  using Mediator.Output;


	internal class OutputManager : AbstractManager
  {
    #region Auto properties ....................................................
    private IOutput    Output { get; set; }
    #endregion ................................................. Auto properties


    #region Construction .......................................................
    public OutputManager()
    {
    }
    #endregion .................................................... Construction


    #region Facilitate the output process ......................................
    public void GenerateOutput(string dir, ReportOutputType type)
    {
      if (dir.Trim() == "*")
      {
        // '*' indicates current execution directory.
        dir = DirHandler.Instance.CurrentDirectory;
      }

      Output = OutputFactory.Create(type);
      Output.GenerateOutput(dir);
    }
    #endregion ................................... Facilitate the output process


    #region Abstract class AbstractManager impl. ...............................
    public override void Initialize()
    {
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
      get { return _log ?? (_log = Out.ZoneFactory("" + Ids.REGION_OUTPUT, GetType().Name)); }
    }
    #endregion ......................................................... Logging
  }
}