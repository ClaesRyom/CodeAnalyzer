// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.UnitTest.DataAccess
{
	using System;
	using System.Configuration;
	using CodeAnalyzer.Engine;
	using CodeAnalyzer.Mediator;
	using CodeAnalyzer.Mediator.Configuration;
	using CodeAnalyzer.Mediator.DataAccess;
	using CodeAnalyzer.Mediator.Proxies;
	using Coordination;
	using Microsoft.VisualStudio.TestTools.UnitTesting;


	public class DataAccessProxyTests
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

				Dirs                  = DirHandler.Instance;
				Dirs.CurrentDirectory = Environment.CurrentDirectory;

				ProxisHome                    = ProxyHome.Instance;
				ProxisHome.StatisticsProxy    = AppInit.InitializeStatisticsComponent(AppManager);
				ProxisHome.ConfigurationProxy = AppInit.InitializeConfigurationComponentProxy(AppManager);
				ProxisHome.DataAccessProxy    = AppInit.InitializeDataAccessComponent(AppManager);
				((IComponentProxy)ProxisHome.DataAccessProxy).Start();
			}

			[TestMethod]
			[TestCategory("DataAccess.Dependencies")]
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
			}
		}

		[TestClass]
		public class TheConstructor
		{
			[TestMethod]
			[TestCategory("DataAccess")]
			public void DefaultConstructor()
			{
				// This may NOT throw any exceptions!!!
				IDataAccessProxy proxy = CodeAnalyzer.DataAccess.DataAccessComponentProxy.Instance;
				Assert.IsNotNull(proxy);
			}
		}


		[TestClass]
		public class CreateVersionNumber : EngineDependencies
		{
			[TestMethod]
			[TestCategory("DataAccess")]
			public void CreateVersionNumber_ValidInput_Ok()
			{
				// This may NOT throw any exceptions!!!
				IVersionDataAccessProxy proxy = CodeAnalyzer.DataAccess.DataAccessComponentProxy.Instance;
				proxy.CreateVersionNumber();

				//
				Assert.IsNotNull(proxy);

				// Ids.DATABASE_VERSION_NUMBER
			}
		}
	}
}
