// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.UnitTest.Statistics
{
	using CodeAnalyzer.Mediator.Exceptions.Statistics;
	using CodeAnalyzer.Mediator.Identifiers;
	using CodeAnalyzer.Mediator.Statistics;
  using Microsoft.VisualStudio.TestTools.UnitTesting;

  using CodeAnalyzer.Statistics;


	public class StatisticsProxyTests
  {
    [TestClass]
    public class StatisticsDependencies
    {
      [TestInitialize()]
      public void EnsureDependencies()
      {
        EnsureConfigurationFilesAreAvailableDuringTest();
      }

      [TestMethod]
      [TestCategory("Statistics.Dependencies")]
      [DeploymentItem(@"..\..\..\Configuration\Config\", @"Config\")]
      [DeploymentItem(@"..\..\..\ConsoleApp\app.config", @"")]
      public void EnsureConfigurationFilesAreAvailableDuringTest()
      {
        // It is on purpose that this method is empty!
        //
        // The reason for having an empty test method is that this is a base
        // test class for all inner test classes testing the Configutation
        // component and these test classes are dependent on specific 
        // configuration files that must be present during the test execution.
        //
        // The DeploymentItem attribute on the method ensures that the specified
        // files are copied to the test output directory.
      }
    }

    [TestClass]
    public class TheConstructor
    {
      [TestMethod]
      [TestCategory("Statistics")]
      public void DefaultConstructor()
      {
        // This may NOT throw any exceptions!!!
        IStatisticsProxy statisticsProxy = StatisticsComponentProxy.Instance;
      }
    }

    [TestClass]
    public class TheInitilizeMethod : StatisticsDependencies
    {
      [TestMethod]
      [TestCategory("Statistics")]
      public void TheInitilizeMethodWithoutAnyExceptions()
      {
        // This may NOT throw any exceptions!!!
        StatisticsComponentProxy componentProxy = StatisticsComponentProxy.Instance;
        componentProxy.Start();
        componentProxy.Initialize();
      }
    }

    [TestClass]
    public class TheStartMethod : StatisticsDependencies
    {
      [TestMethod]
      [TestCategory("Statistics")]
      public void TheStartMethodWithoutAnyExceptions()
      {
        // This may NOT throw any exceptions!!!
        StatisticsComponentProxy componentProxy = StatisticsComponentProxy.Instance;
        componentProxy.Start();
      }
    }

    [TestClass]
    public class TheStopMethod : StatisticsDependencies
    {
      [TestMethod]
      [TestCategory("Statistics")]
      public void TheStopMethodWithoutAnyExceptions()
      {
        // This may NOT throw any exceptions!!!
        StatisticsComponentProxy componentProxy = StatisticsComponentProxy.Instance;
        componentProxy.Start();
        componentProxy.Stop();
      }
    }

    [TestClass]
    public class TheIncrementAndReadCounterMethods : StatisticsDependencies
    {
      [TestMethod]
      [TestCategory("Statistics")]
      [ExpectedException(typeof(StatisticsComponentException), "The statistics manager was not initialized before calling this method.")]
      public void IncrementWithASingleStepWithoutProperInitialization()
      {
        StatisticsComponentProxy componentProxy = StatisticsComponentProxy.Instance;
        componentProxy.IncrementCounter(CounterIds.ActiveRuleDefinitions);
      }

      [TestMethod]
      [TestCategory("Statistics")]
      [ExpectedException(typeof(StatisticsComponentException), "The statistics manager was not initialized before calling this method.")]
      public void IncrementWithCustomStepSizeWithoutProperInitialization()
      {
        StatisticsComponentProxy componentProxy = StatisticsComponentProxy.Instance;
        componentProxy.IncrementCounter(CounterIds.ActiveRuleDefinitions, 10);
      }

      [TestMethod]
      [TestCategory("Statistics")]
      [ExpectedException(typeof(StatisticsComponentException), "The statistics manager was not initialized before calling this method.")]
      public void ReadCounterWithoutProperInitialization()
      {
        StatisticsComponentProxy componentProxy = StatisticsComponentProxy.Instance;
        componentProxy.ReadCounter(CounterIds.ActiveRuleDefinitions);
      }

      [TestMethod]
      [TestCategory("Statistics")]
      public void IncrementWithASingleStep()
      {
        // This may NOT throw any exceptions!!!
        StatisticsComponentProxy componentProxy = StatisticsComponentProxy.Instance;
        componentProxy.Start();

        for (int i = 0; i < 1000; i++) componentProxy.IncrementCounter(CounterIds.ActiveRuleDefinitions);
        for (int i = 0; i < 1000; i++) componentProxy.IncrementCounter(CounterIds.CategoryDeclarations);
        for (int i = 0; i < 1000; i++) componentProxy.IncrementCounter(CounterIds.CriticalMatches);
        for (int i = 0; i < 1000; i++) componentProxy.IncrementCounter(CounterIds.FatalMatches);
        for (int i = 0; i < 1000; i++) componentProxy.IncrementCounter(CounterIds.Files);
        for (int i = 0; i < 1000; i++) componentProxy.IncrementCounter(CounterIds.InfoMatches);
        for (int i = 0; i < 1000; i++) componentProxy.IncrementCounter(CounterIds.RuleDeclarations);
        for (int i = 0; i < 1000; i++) componentProxy.IncrementCounter(CounterIds.WarningMatches);

        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.ActiveRuleDefinitions), 0);
        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.CategoryDeclarations), 0);
        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.CriticalMatches), 0);
        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.FatalMatches), 0);
        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.Files), 0);
        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.InfoMatches), 0);
        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.RuleDeclarations), 0);
        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.WarningMatches), 0);
      }

      [TestMethod]
      [TestCategory("Statistics")]
      public void IncrementWithCustomStepSize()
      {
        // This may NOT throw any exceptions!!!
        StatisticsComponentProxy componentProxy = StatisticsComponentProxy.Instance;
        componentProxy.Start();

        for (int i = 0; i < 100; i++) componentProxy.IncrementCounter(CounterIds.ActiveRuleDefinitions, 10);
        for (int i = 0; i < 100; i++) componentProxy.IncrementCounter(CounterIds.CategoryDeclarations, 10);
        for (int i = 0; i < 100; i++) componentProxy.IncrementCounter(CounterIds.CriticalMatches, 10);
        for (int i = 0; i < 100; i++) componentProxy.IncrementCounter(CounterIds.FatalMatches, 10);
        for (int i = 0; i < 100; i++) componentProxy.IncrementCounter(CounterIds.Files, 10);
        for (int i = 0; i < 100; i++) componentProxy.IncrementCounter(CounterIds.InfoMatches, 10);
        for (int i = 0; i < 100; i++) componentProxy.IncrementCounter(CounterIds.RuleDeclarations, 10);
        for (int i = 0; i < 100; i++) componentProxy.IncrementCounter(CounterIds.WarningMatches, 10);

        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.ActiveRuleDefinitions), 0);
        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.CategoryDeclarations), 0);
        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.CriticalMatches), 0);
        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.FatalMatches), 0);
        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.Files), 0);
        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.InfoMatches), 0);
        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.RuleDeclarations), 0);
        Assert.AreEqual(1000, componentProxy.ReadCounter(CounterIds.WarningMatches), 0);
      }
    }
  }
}


