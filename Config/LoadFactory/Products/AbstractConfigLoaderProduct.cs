// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2014 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Config.LoadFactory.Products
{
	using Fbb.Output;


	internal abstract class AbstractConfigLoaderProduct : IConfigLoaderProduct
  {
    #region Instance Variables .................................................
    protected Zone _log = null;
    #endregion .............................................. Instance Variables

    
    #region Auto properties ....................................................
    protected ConfigManager Manager { get; set; }
    #endregion ................................................. Auto properties


    #region Logging ............................................................
    protected abstract Zone Log { get; }
    #endregion ......................................................... Logging


    #region Construction .......................................................
    protected AbstractConfigLoaderProduct(ConfigManager manager)
    {
      Manager = manager;
    }
    #endregion .................................................... Construction


    #region Interface IConfigLoaderProduct impl. ...............................
    public abstract void LoadLanguageDeclarations();

    public abstract void LoadCategoryDeclarations();

    public abstract void LoadProjectDefinitions();
    #endregion ............................ Interface IConfigLoaderProduct impl.
  }
}