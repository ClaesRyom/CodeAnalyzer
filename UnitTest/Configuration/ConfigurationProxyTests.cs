// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.UnitTest.Configuration
{
  using System;
  using System.Diagnostics;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	
	using CodeAnalyzer.Mediator.Configuration.Declarations;
  using CodeAnalyzer.Mediator.Configuration.Definitions;
  using CodeAnalyzer.Mediator.Exceptions.Configuration;
	using CodeAnalyzer.Mediator;
  using Coordination;
  using Config;


	public static class ConfigurationProxyTests
	{
		#region Test dependencies
		[TestClass]
    public class ConfigurationDependencies
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
        ProxisHome.StatisticsProxy = AppInit.InitializeStatisticsComponent(AppManager);
      }

      [TestMethod]
      [TestCategory("Configuration.Dependencies")]
			[DeploymentItem(@"..\..\..\Configuration\Config\", @"Config\")]
			[DeploymentItem(@"..\..\..\Output\Repository\", @"Repository\")]
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
		#endregion


		#region Test initializing
		[TestClass]
    public class TheInitilizeMethod : ConfigurationDependencies
    {
      [TestMethod]
      [TestCategory("Configuration")]
      public void TheInitilizeMethod_WithoutAnyExceptions()
      {
        // This may NOT throw any exceptions!!!
        ConfigurationComponentProxy proxy = ConfigurationComponentProxy.Instance;
        proxy.InitializeComponentAccessPermissions(AppManager);
        proxy.Initialize();
      }
    }
		#endregion


		#region Test Start & Stop
		[TestClass]
    public class TheStartMethod : ConfigurationDependencies
    {
      [TestMethod]
      [TestCategory("Configuration")]
      public void TheStartMethodWithoutAnyExceptions()
      {
        // This may NOT throw any exceptions!!!
        ConfigurationComponentProxy proxy = ConfigurationComponentProxy.Instance;
        proxy.InitializeComponentAccessPermissions(AppManager);
        proxy.Start();
      }
    }


    [TestClass]
    public class TheStopMethod : ConfigurationDependencies
    {
      [TestMethod]
      [TestCategory("Configuration")]
      public void TheStopMethodWithoutAnyExceptions()
      {
        // This may NOT throw any exceptions!!!
        ConfigurationComponentProxy proxy = ConfigurationComponentProxy.Instance;
        proxy.InitializeComponentAccessPermissions(AppManager);
        proxy.Start();
        proxy.Stop();
      }
    }
		#endregion 


		#region Test Factories
		[TestClass]
		public class TheConfigurationFactoryMethod : ConfigurationDependencies
    {

			[TestMethod]
			[TestCategory("Configuration.Factory")]
			[ExpectedException(typeof(ConfigurationComponentException), "Invlid 'Type' given as argument to the Configuration Component Factory.")]
			public void ConfigurationFactory_InValidType_ThrowsException()
			{
        ConfigurationComponentFactory factory = ConfigurationComponentFactory.Instance;
				factory.InitializeComponentAccessPermissions(AppManager);

				Debug.Assert(factory != null, "proxy != null");
				factory.ConfigurationFactory<string>(typeof(string));
			}
			
			[TestMethod]
      [TestCategory("Configuration.Factory")]
			public void ConfigurationFactory_AllSupportedTypes_ReturnsValidObject()
      {
        ConfigurationComponentFactory factory = ConfigurationComponentFactory.Instance;
				factory.InitializeComponentAccessPermissions(AppManager);

	      Debug.Assert(factory != null, "proxy != null");
	      ILanguageDeclaration languageDeclaration = factory.ConfigurationFactory<ILanguageDeclaration>(typeof(ILanguageDeclaration));
	      IRuleDeclaration     ruleDeclaration     = factory.ConfigurationFactory<IRuleDeclaration>(typeof(IRuleDeclaration));
	      IProjectDefinition   projectDefinition   = factory.ConfigurationFactory<IProjectDefinition>(typeof(IProjectDefinition));
	      ICategoryDefinition  categoryDefinition  = factory.ConfigurationFactory<ICategoryDefinition>(typeof(ICategoryDefinition));
	      IRuleDefinition      ruleDefinition      = factory.ConfigurationFactory<IRuleDefinition>(typeof(IRuleDefinition));
	      IDirectoryDefinition directoryDefinition = factory.ConfigurationFactory<IDirectoryDefinition>(typeof(IDirectoryDefinition));
	      IFileDefinition      fileDefinition      = factory.ConfigurationFactory<IFileDefinition>(typeof(IFileDefinition));

				Assert.IsNotNull(languageDeclaration);
				Assert.IsNotNull(ruleDeclaration);
				Assert.IsNotNull(projectDefinition);
				Assert.IsNotNull(categoryDefinition);
				Assert.IsNotNull(ruleDefinition);
				Assert.IsNotNull(directoryDefinition);
				Assert.IsNotNull(fileDefinition);
			}
		}
		#endregion
	}
}
