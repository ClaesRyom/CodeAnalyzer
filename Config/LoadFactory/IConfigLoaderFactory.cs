// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2014 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Config.LoadFactory
{
	using Mediator.Configuration;


	internal interface IConfigLoaderFactory
  {
    IConfigLoaderProduct InitializeConfigurationLoader(StorageType type, ConfigManager manager);
  }
}