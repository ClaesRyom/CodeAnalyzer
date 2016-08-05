// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2014 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Config.LoadFactory.Factories
{
	using Mediator.Configuration;
	using Mediator.Exceptions.Configuration;
	using Products;


	internal class ConfigLoaderFactory : IConfigLoaderFactory
  {
    #region Interface IConfigLoaderFactory impl. ...............................
    public IConfigLoaderProduct InitializeConfigurationLoader(StorageType type, ConfigManager manager)
    {
      switch (type)
      {
        case StorageType.Db:  { return new DbConfigLoaderProduct(manager);  }
        case StorageType.Xml: { return new XmlConfigLoaderProduct(manager); }
        default: throw new ConfigurationComponentException(this, -1, "Unable to determine where the configuration data should be loaded from?");
      }
    }
    #endregion ............................ Interface IConfigLoaderFactory impl.
  }
}