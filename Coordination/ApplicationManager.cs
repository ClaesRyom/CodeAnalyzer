// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using CodeAnalyzer.Mediator.Matches;

namespace CodeAnalyzer.Coordination
{
	using System;
	using System.IO;
	using Mediator.DataAccess;
	using Mediator.Exceptions.DataAccess;
	using Mediator.Label;
	using Trace = System.Diagnostics.Trace;

	using Fbb.Exceptions.Base;
	using Fbb.Output;
	using Fbb.Output.Definitions;
	using Fbb.Output.Factory.Concrete.Console;
	using Fbb.Output.Factory.Concrete.Log;
	using Mediator;
	using Mediator.Exceptions.Configuration;
	using Mediator.Exceptions.Engine;
	using Mediator.Exceptions.Matches;
	using Mediator.Exceptions.Statistics;
	using Mediator.Identifiers;
	using Mediator.Managers;
	using Mediator.Proxies;
	using Mediator.Security;


  public class ApplicationManager : AbstractManager, IApplicationManager
	{
		#region Inner classes ......................................................
	  private struct Split
	  {
			public int    Time { get; set; }
			public string Text { get; set; }
	  }
		#endregion ................................................... Inner classes


		#region Auto properties ....................................................
		protected ProxyHome              Proxies           { get; set; }
    private   ApplicationInitializer Initializer       { get; set; }
		private   List<Split>            SplitMeasurements { get; set; }
    #endregion ................................................. Auto properties


    #region Construction .......................................................
    public ApplicationManager()
    {
    }
    #endregion .................................................... Construction


    #region Initializers .......................................................
    private void InitializeLogSystem()
    {
      string curr = DirHandler.Instance.CurrentDirectory;

      string logConfigFile = Path.Combine(curr, Ids.CONFIG_DIR, Ids.LOG_CONFIG_FILE);
      if (string.IsNullOrEmpty(logConfigFile) || !File.Exists(logConfigFile))
      {
        // Unable to find log configuration file. So let's fall back upon default targets - Console and Log. 
        Trace.WriteLine("Unable to locate the log configuration file - using default settings.");

        LogInitializationData     log     = new LogInitializationData     { Active = true, TraceLevel = LogLevel.VERYHIGH, Filename = Path.Combine(curr, Ids.LOG_FILE) };
        ConsoleInitializationData console = new ConsoleInitializationData { Active = true, TraceLevel = LogLevel.VERYHIGH, Filter   = {DebugEnabled = false} };

        Out.Create(log);
        Out.Create(console);
      }
      else
      {
        // Log configuration file found, so log system will be based upon the settings defined in config file.
        try
        {
          Out.LoadApplicationConfigFile(new FileInfo(logConfigFile));
        }
        catch (BaseException e)
        {
          Trace.WriteLine("Failed to load log configuration file, {0}", logConfigFile);
          Trace.WriteLine("Error description: \n{0}", e.ExceptionSummary());
          return;
        }

        try
        {
          // Start monitoring the log configuration file - changes are identified on file save occurrence.
          Out.StartApplicationConfigMonitor(logConfigFile, Out.LASTWRITE);
        }
        catch (BaseException e)
        {
          Trace.WriteLine("Failed to start the file monitor for monitoring the log configuration file, {0}. Continuing without monitoring the log configuration file.", logConfigFile);
          Trace.WriteLine("Error description: \n{0}", e.ExceptionSummary());
        }
      }
    }

	  private void StartSystem()
    {
      Log.Debug("Starting the initialization process in {0}.", GetType().Name);

			Settings settings = new Settings();

      // Initialize the overall ProxyHome within the Mediator project - this 
      // ProxyHome is the one you need to acces in order to obtain access to all the 
      // other subsystems or components.
      Proxies = ProxyHome.Instance;

			SplitMeasurements.Add(new Split {Time = Environment.TickCount, Text = @"... settings loaded from app.config file"});


      #region Initialize all the components proxies & factories ................

      #region Initialize the Batch Execution Identification
      try
      {
        Proxies.BatchExecutionIdentification = new Batch(DateTime.Now);
      }
      catch (Exception e)
      {
        string s = "Failed to initialize the Batch Execution Identification.";
        Log.Error(s, e);
        throw new CoordinationException(s, e);
      }
      #endregion


			SplitMeasurements.Add(new Split { Time = Environment.TickCount, Text = @"... batch identification generated" });


      #region Initialize the Configuration Component Factory
      try
      {
        // Initlialize the Configuration Component Factory...
        Proxies.ConfigurationFactory = Initializer.InitializeConfigurationComponentFactory(this);
      }
      catch (ConfigurationComponentException cce)
      {
        string s = "Failed to initialize the configuration component factory.";
        Log.Error(s, cce);
        throw new CoordinationException(s, cce);
      }
      #endregion


			SplitMeasurements.Add(new Split { Time = Environment.TickCount, Text = @"... configuration component factory generated" });


			#region Initialize the Statistics Component Proxy
      try
      {
        // Initlialize the Statistics Management System...
        Proxies.StatisticsProxy = Initializer.InitializeStatisticsComponent(this);
      }
      catch (StatisticsComponentException sce)
      {
        string s = "Failed to initialize the Statistics component.";
        Log.Error(s, sce);
        throw new CoordinationException(s, sce);
      }
      #endregion


			SplitMeasurements.Add(new Split { Time = Environment.TickCount, Text = @"... statistics component proxy generated" });


			#region Initialize the Data Access Component Proxy
      try
      {
        // Initlialize the Data Access component...
        Proxies.DataAccessProxy = Initializer.InitializeDataAccessComponent(this);
      }
      catch (DataAccessComponentException dace)
      {
        string s = "Failed to initialize the Data Access component.";
        Log.Error(s, dace);
        throw new CoordinationException(s, dace);
      }
      #endregion


			SplitMeasurements.Add(new Split { Time = Environment.TickCount, Text = @"... data access component proxy generated" });


			#region Initialize the Configuration Component Proxy
      try
      {
        // Initlialize the Configuration Component Proxy...
        Proxies.ConfigurationProxy = Initializer.InitializeConfigurationComponentProxy(this);
      }
      catch (ConfigurationComponentException cce)
      {
        string s = "Failed to initialize the configuration component proxy.";
        Log.Error(s, cce);
        throw new CoordinationException(s, cce);
      }
      #endregion


			SplitMeasurements.Add(new Split { Time = Environment.TickCount, Text = @"... configuration component proxy generated" });


			#region Initialize the Match Component Proxy
      try
			{
				// Initlialize the Match component...
				Proxies.MatchProxy = Initializer.InitializeMatchComponent(this);
			}
			catch (MatchComponentException ece)
			{
				string s = "Failed to initialize the Match component.";
				Log.Error(s, ece);
				throw new CoordinationException(s, ece);
			}
			#endregion


			SplitMeasurements.Add(new Split { Time = Environment.TickCount, Text = @"... match component proxy generated" });


			#region Initialize the Engine Component Proxy
      try
      {
        // Initlialize the Engine component...
        Proxies.EngineProxy = Initializer.InitializeEngineComponent(this);
      }
      catch (EngineComponentException ece)
      {
        string s = "Failed to initialize the Engine component.";
        Log.Error(s, ece);
        throw new CoordinationException(s, ece);
      }
      #endregion


			SplitMeasurements.Add(new Split { Time = Environment.TickCount, Text = @"... engine component proxy generated" });


			#region Initialize the Output Component Proxy
      try
      {
        // Initlialize the Output component...
        Proxies.OutputProxy = Initializer.InitializeOutputComponent(this);
      }
      catch (EngineComponentException ece)
      {
        string s = "Failed to initialize the Engine component.";
        Log.Error(s, ece);
        throw new CoordinationException(s, ece);
      }
      #endregion


			SplitMeasurements.Add(new Split { Time = Environment.TickCount, Text = @"... output component proxy generated" });


			#endregion ............. Initialize all the components proxies & factories



      #region Start all component proxies in correct order .....................
      ((IComponentProxy)Proxies.StatisticsProxy).Start();
			Proxies.StatisticsProxy.StartTimer();
		  foreach (Split split in SplitMeasurements)
		  {
			  Proxies.StatisticsProxy.SplitTimer(split.Time, split.Text);
		  }
			Proxies.StatisticsProxy.SplitTimer(@"... statistics proxy started");

			((IComponentProxy)Proxies.DataAccessProxy).Start();
			Proxies.StatisticsProxy.SplitTimer(@"... data access proxy started");

			((IComponentProxy)Proxies.ConfigurationProxy).Start();
			Proxies.StatisticsProxy.SplitTimer(@"... configuration proxy started");
			
			((IComponentProxy)Proxies.MatchProxy).Start();
			Proxies.StatisticsProxy.SplitTimer(@"... match proxy started");

			((IComponentProxy)Proxies.EngineProxy).Start();
			Proxies.StatisticsProxy.SplitTimer(@"... engine proxy started");

			((IComponentProxy)Proxies.OutputProxy).Start();
			Proxies.StatisticsProxy.SplitTimer(@"... output proxy started");
			#endregion .................. Start all component proxies in correct order


			//IDataAccessProxy dataAccessProxy = Proxies.DataAccessProxy;
			//dataAccessProxy.CreateBulkOfMatches(Proxies.MatchProxy.Matches());

			Proxies.StatisticsProxy.SplitTimer(@"... matches saved to database");


			// Check to see if report generation is Enabled or Disabled?
			if (Settings.Report.OutputEnabled) {

				// Let's do the report...
				ProxyHome.Instance.OutputProxy.GenerateReport(Settings.Report.OutputDir, Settings.Report.OutputType);
			}
			
			
      Log.Info("Initializing ApplicationManager finished with success.");

	    int totalLines      = Proxies.StatisticsProxy.ReadCounter(CounterIds.TotalNumberOfLinesInFiles);
	    int whitespaceLines = Proxies.StatisticsProxy.ReadCounter(CounterIds.TotalNumberOfWhitespaceLinesInFiles);
	    int actualCodeLines = totalLines - whitespaceLines;

			Log.Info("");
			Log.Info("----------------------------------------------------------------");
			Log.Info("Lines in files            : {0}", totalLines);
			Log.Info("Whitespace lines in files : {0}", whitespaceLines);
			Log.Info("Actual code lines in files: {0}", actualCodeLines);
			Log.Info("----------------------------------------------------------------");
			Log.Info("");
		}

	  private void StopSystem()
    {
      Dispose();
    }
    #endregion .................................................... Initializers


    #region Interface IManager impl. ...........................................
    public override void Initialize()
    {
      Proxies = null;
			SplitMeasurements = new List<Split>();

			SplitMeasurements.Add(new Split { Time = Environment.TickCount, Text = @"... first split before anything else" });

			InitializeLogSystem();

			SplitMeasurements.Add(new Split { Time = Environment.TickCount, Text = @"... log system initialized" });

      Initializer = new ApplicationInitializer();
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
        if (Proxies != null)
        {
          Proxies.Dispose();
        }

        // Call the appropriate methods to clean up  
        // unmanaged resources here. 
        // If disposing is false,  
        // only the following code is executed.
      }

      // Indicate that the instance has been disposed.
      Proxies = null;
      Disposed = true;
    }

    public override void Start()
    {
      try
      {
        Initialize();
      }
      catch (Exception e)
      {
        string s = "An unexpected error occurred during the initialization of the application.";
        Log.Error(s, e);
        throw new CoordinationException(s, e);
      }


      try
      {
        StartSystem();
      }
      catch (Exception e)
      {
        string s = "An unexpected error occurred during the process of starting the application.";
        Log.Error(s, e);
        throw new CoordinationException(s, e);
      }
    }

    public override void Stop()
    {
      try
      {
        StopSystem();
      }
      catch (Exception e)
      {
        string s = "An unexpected error occurred during the process of stopping the application.";
        Log.Error(s, e);
        throw new CoordinationException(s, e);
      }
    }
    #endregion ........................................ Interface IManager impl.


    #region Interface IApplicationManager impl. ................................
    public ComponentAccessKey ProvideComponentAccessKey(IKeyConsumer keyConsumer)
    {
      KeyFactory keyFactory = new KeyFactory();
      return keyFactory.GenerateComponentAccessKey(SecurityIds.Password, SecurityIds.Salt, keyConsumer);
    }
    #endregion ............................. Interface IApplicationManager impl.


    #region Logging ............................................................
    protected override Zone Log
    {
      get { return _log ?? (_log = Out.ZoneFactory("" + Ids.REGION_APP_STARTER, GetType().Name)); }
    }
    #endregion ......................................................... Logging
  }
}