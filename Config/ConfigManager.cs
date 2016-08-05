// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Config
{
	using System;
	using Declarations;
	using Definitions;
	using Fbb.Output;
	using LoadFactory;
	using LoadFactory.Factories;
	using Mediator;
	using Mediator.Configuration.Declarations;
	using Mediator.Configuration.Definitions;
	using Mediator.Exceptions.Configuration;
	using Mediator.Identifiers;
	using Mediator.Managers;


	internal sealed class ConfigManager : AbstractManager
  {
    #region Auto properties ....................................................
    private IConfigLoaderProduct ConfigLoader  { get; set; }
    public  IDeclarationsHome    Declarations  { get; set; }
    public  IDefinitionsHome     Definitions   { get; set; }
    #endregion ................................................. Auto properties


    #region Properties .........................................................
    protected override Zone Log
    {
      get { return _log ?? Out.ZoneFactory(Ids.REGION_CONFIG, GetType().Name); }
    }
    #endregion ...................................................... Properties


    #region Construction .......................................................
    public ConfigManager()
    {
    }
    #endregion .................................................... Construction


    #region Interface IManager impl. ...........................................
    public override void Initialize()
    {
      Declarations  = new DeclarationsHome();
      Definitions   = new DefinitionsHome(Declarations);


      try
      {
        // Are we loading configuration data from xml file or a database? That is
        // specified in the 'Settings.Configuration.StorageType'. 
        // Values are 'Db' or 'Xml'.
        IConfigLoaderFactory configLoaderFactory = new ConfigLoaderFactory();
        ConfigLoader = configLoaderFactory.InitializeConfigurationLoader(Settings.Configuration.StorageType, this);

				ProxyHome.Instance.RetrieveStatisticsProxy(ConfigKeyKeeper.Instance.AccessKey).SplitTimer($@"... ... initialized configuration loader with storage type '{Settings.Configuration.StorageType}'");
      }
      catch (Exception e)
      {
        string s = "Unexpected error occurred while trying to initialize the configuration loader?";
        Log.Error(s, e);
        throw new ConfigurationComponentException(this, -1, s, e);
      }
    }

    public override void Start()
    {
      #region Initialize...
      try
      {
        Initialize();
      }
      catch (Exception e)
      {
        string s = string.Format("Unexpected behaviour detected during initialization of an instance of {0}!", GetType().Name);
        Log.Error(s, e);
        throw new ConfigurationComponentException(this, -1, s, e);
      }
      #endregion


      #region Load Language Declarations...
      try
      {
        // Let's load the language declarations...
        ConfigLoader.LoadLanguageDeclarations();
      }
      catch (Exception e)
      {
        string s = "Unexpected behaviour detected while loading the \'Language\' Declarations or while converting the content into a object structure?";
        Log.Error(s, e);
        throw new ConfigurationComponentException(this, -1, s, e);
      }
      #endregion

      
      #region Load Category Declarations...
      try
      {
        // Let's load the categories declarations and rule declarations...
        ConfigLoader.LoadCategoryDeclarations();
      }
      catch (Exception e)
      {
        string s = "Unexpected behaviour detected while loading the \'Category\' and \'Rule\' Declarations or while converting the content into a object structure?";
        Log.Error(s, e);
        throw new ConfigurationComponentException(this, -1, s, e);
      }
      #endregion

      
      #region Load Project Definitions...
      try
      {
        // Let's load the area definitions...
        ConfigLoader.LoadProjectDefinitions();
      }
      catch (Exception e)
      {
        string s = "Unexpected behaviour detected while loading the Definitions or while converting the content into a object structure?";
        Log.Error(s, e);
        throw new ConfigurationComponentException(this, -1, s, e);
      }
      #endregion
		}

    public override void Stop()
    {
      try
      {
        Dispose();
      }
      catch (Exception e)
      {
        string s = string.Format("Unexpected behaviour detedted while disposing an instance of {0}!", GetType().Name);
        Log.Error(s, e);

        throw new ConfigurationComponentException(this, -1, s, e);
      }
    }
    #endregion ........................................ Interface IManager impl.


    #region Implementation of IDisposable ......................................
    // Dispose(bool disposing) executes in two distinct scenarios. 
    // If disposing equals true, the method has been called directly 
    // or indirectly by a user's code. Managed and unmanaged resources 
    // can be disposed. 
    // If disposing equals false, the method has been called by the  
    // runtime from inside the finalizer and you should not reference  
    // other objects. Only unmanaged resources can be disposed. 
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
        if (Declarations != null)
        {
          Declarations.Dispose();
        }

        if (Definitions != null)
        {
          Definitions.Dispose();
        }

        // Call the appropriate methods to clean up  
        // unmanaged resources here. 
        // If disposing is false,  
        // only the following code is executed.
      }

      // Indicate that the instance has been disposed.
      Declarations = null;
      Definitions  = null;
      Disposed     = true;
    }
    #endregion ................................... Implementation of IDisposable


    #region Handling Project Definitions .......................................
    public void CreateProject(int projectId, string projectName, bool isProjectEnabled)
    {
      if (projectId <= 0)
        throw new ArgumentException("The argument 'projectId' cannot be less than or equal to '0'!");

      if (Definitions.Projects.ContainsKey(projectId))
        return;

      Definitions.Projects.Add(projectId, new ProjectDefinition() {Id = projectId, Name = projectName, Enabled = isProjectEnabled});
    }
    #endregion .................................... Handling Project Definitions
  }
}