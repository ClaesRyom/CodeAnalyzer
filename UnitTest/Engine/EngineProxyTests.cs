// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.UnitTest.Engine
{
  using System;
  using CodeAnalyzer.Mediator;
  using CodeAnalyzer.Mediator.Engine;
  using CodeAnalyzer.Mediator.Proxies;
  using Microsoft.VisualStudio.TestTools.UnitTesting;

  using CodeAnalyzer.Engine;
  using Coordination;


	public class EngineProxyTests
  {
    [TestClass]
    public class EngineDependencies
    {
      #region Auto properties ..................................................
      protected DirHandler             Dirs       { get; set; }
      protected ProxyHome              ProxisHome { get; set; }
      protected ApplicationManager     AppManager { get; set; }
      protected ApplicationInitializer AppInit    { get; set; }
      #endregion ............................................... Auto properties


      [TestInitialize]
      public void EnsureDependencies()
      {
        EnsureConfigurationFilesAreAvailableDuringTest();

        AppManager = new ApplicationManager();
        AppInit    = new ApplicationInitializer();

        Dirs = DirHandler.Instance;
        Dirs.CurrentDirectory = Environment.CurrentDirectory;

        ProxisHome = ProxyHome.Instance;
				ProxisHome.StatisticsProxy    = AppInit.InitializeStatisticsComponent(AppManager);
				ProxisHome.ConfigurationProxy = AppInit.InitializeConfigurationComponentProxy(AppManager);
      }

      [TestMethod]
      [TestCategory("Engine.Dependencies")]
      [DeploymentItem(@"..\..\..\Configuration\Config\", @"Config\")]
      [DeploymentItem(@"..\..\..\Output\Repository\", @"Repository\")]
      [DeploymentItem(@"..\..\..\ConsoleApp\app.config", @"")]
      public void EnsureConfigurationFilesAreAvailableDuringTest()
      {
        // It is on purpose that this method is empty!
        //
        // The reason for having an empty test method is that this is a base
        // test class for all inner test classes testing the Configuration
        // component and these test classes are dependent on specific 
        // configuration files that must be present during the test execution.
        //
        // The DeploymentItem attribute on the method ensures that the specified
        // files are copied to the test output directory.
	      int i = 0;
      }
    }

    [TestClass]
    public class TheConstructor
    {
      [TestMethod]
      [TestCategory("Engine")]
      public void DefaultConstructor()
      {
        // This may NOT throw any exceptions!!!
        IEngineProxy engineProxy = EngineComponentProxy.Instance;

        Assert.IsNotNull(engineProxy);
      }
    }


    [TestClass]
    public class TheInitilizeMethod : EngineDependencies
    {
      [TestMethod]
      [TestCategory("Engine")]
      public void TheInitilizeMethodWithoutAnyExceptions()
      {
        // This may NOT throw any exceptions!!!
        EngineComponentProxy proxy = EngineComponentProxy.Instance;
        proxy.InitializeComponentAccessPermissions(AppManager);
        ((IComponentProxy)proxy).Initialize();
      }
    }


    [TestClass]
    public class TheStartMethod : EngineDependencies
    {
      [TestMethod]
      [TestCategory("Engine")]
      public void TheStartMethodWithoutAnyExceptions()
      {
        // This may NOT throw any exceptions!!!
        EngineComponentProxy componentProxy = EngineComponentProxy.Instance;
        componentProxy.Start();
      }
    }


    [TestClass]
    public class TheStopMethod : EngineDependencies
    {
      [TestMethod]
      [TestCategory("Engine")]
      public void TheStopMethodWithoutAnyExceptions()
      {
        // This may NOT throw any exceptions!!!
        EngineComponentProxy componentProxy = EngineComponentProxy.Instance;
        componentProxy.Start();
        componentProxy.Stop();
      }
    }
  }
}
