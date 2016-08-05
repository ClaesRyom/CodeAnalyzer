// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2014 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Configuration.LoadFactory.Products
{
  using System.Collections.Generic;

  using Fbb.Output;
  using Mediator;
  using Mediator.Configuration.Declarations;
  using Mediator.Configuration.Definitions;
  using Mediator.DataAccess;
  using Mediator.Identifiers;


  internal class DbConfigLoaderProduct : AbstractConfigLoaderProduct
  {
    #region Properties .........................................................
    protected override Zone Log
    {
      get { return _log ?? Out.ZoneFactory(Ids.REGION_CONFIG, GetType().Name); }
    }
    #endregion ...................................................... Properties

    
    #region Construction .......................................................
    public DbConfigLoaderProduct(ConfigManager manager) : base(manager) { }
    #endregion .................................................... Construction


    #region Interface IConfigLoaderProduct impl. ...............................
    public override void LoadLanguageDeclarations()
    {
      IConfigDataAccessProxy proxy = ProxyHome.Instance.RetrieveConfigDataAccessProxy(ConfigKeyKeeper.Instance.AccessKey);

      IEnumerable<ILanguageDeclaration> languageDeclarations = proxy.LoadLanguageDeclarations();
      foreach (ILanguageDeclaration declaration in languageDeclarations)
      {
        Manager.Declarations.Languages.Add(declaration.Id, declaration);

        // Update 'Language Declaration' statistics counters...
        ProxyHome.Instance.RetrieveStatisticsProxy(ConfigKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.LanguageDeclarations);
      }
    }

    public override void LoadCategoryDeclarations()
    {
      IConfigDataAccessProxy proxy = ProxyHome.Instance.RetrieveConfigDataAccessProxy(ConfigKeyKeeper.Instance.AccessKey);

      IEnumerable<ICategoryDeclaration> categoryDeclarations = proxy.LoadCategoryDeclarations();
      foreach (ICategoryDeclaration declaration in categoryDeclarations)
      {
        Manager.Declarations.Categories.Add(declaration.Id, declaration);

        foreach (KeyValuePair<int, IRuleDeclaration> pair in declaration.Rules)
        {
          Manager.Declarations.Rules.Add(pair.Value.Id, pair.Value);
        }

        // Update 'Category Declaration' statistics counters...
        ProxyHome.Instance.RetrieveStatisticsProxy(ConfigKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.CategoryDeclarations);
      }
    }

    public override void LoadProjectDefinitions()
    {
      IConfigDataAccessProxy proxy = ProxyHome.Instance.RetrieveConfigDataAccessProxy(ConfigKeyKeeper.Instance.AccessKey);

      IEnumerable<IProjectDefinition> projectDefinitions = proxy.LoadProjectDefinitions();
      foreach (IProjectDefinition definition in projectDefinitions)
      {
        Manager.Definitions.Projects.Add(definition.Id, definition);

        // Update 'Project Definition' statistics counters...
        ProxyHome.Instance.RetrieveStatisticsProxy(ConfigKeyKeeper.Instance.AccessKey).IncrementCounter(CounterIds.TotalProjectDefinitions);
      }
    }
    #endregion ............................ Interface IConfigLoaderProduct impl.
  }
}