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
	using Mediator.Configuration;
	using Mediator.Configuration.Declarations;
	using Mediator.Configuration.Definitions;
	using Mediator.Exceptions.Configuration;
	using Mediator.Factories;
	using Mediator.Identifiers;
	using Mediator.Security;


	public sealed class ConfigurationComponentFactory : AbstractComponentFactory, IConfigurationFactory, IInternalConfigurationFactory
  {
    #region Instance Variable(s) ...............................................
    private static volatile ConfigurationComponentFactory s_instance     = null;
    private static readonly object                        s_lockInstance = new object();
    private        readonly Zone                          _log           = null;
    #endregion ............................................ Instance Variable(s)


    #region Private properties .................................................
    private Zone Log
    {
      get { return _log ?? Out.ZoneFactory(Ids.REGION_CONFIG, GetType().Name); }
    }
    #endregion .............................................. Private properties


    #region Construction .......................................................
    private ConfigurationComponentFactory()
    {
    }
    #endregion .................................................... Construction


    #region Singleton impl. ....................................................
    public static ConfigurationComponentFactory Instance
    {
      get
      {
        lock (s_lockInstance)
        {
          return s_instance ?? (s_instance = new ConfigurationComponentFactory());
        }
      }
    }
    #endregion ................................................. Singleton impl.


    #region AbstractComponentFactory impl. .....................................
    protected override void InitializeKeyKeeper(ComponentAccessKey componentAccessKey)
    {
      KeyKeeper = ConfigKeyKeeper.InitializeInstance(componentAccessKey);
    }

    public override void Initialize()
    {
    }
    #endregion .................................. AbstractComponentFactory impl.


    #region Factory Declarations ...............................................
    public ILanguageDeclaration CreateLanguageDeclaration()
    {
      return new LanguageDeclaration();
    }

    public ICategoryDeclaration CreateCategoryDeclaration()
    {
      return new CategoryDeclaration();
    }

    public IRuleDeclaration CreateRuleDeclaration()
    {
      return new RuleDeclaration();
    }
    #endregion ............................................ Factory Declarations

    
    #region Factory Definitions ................................................
    public IProjectDefinition CreateProjectDefinition()
    {
      return new ProjectDefinition();
    }

    public ICategoryDefinition CreateCategoryDefinition()
    {
      return new CategoryDefinition();
    }

    public IRuleDefinition CreateRuleDefinition()
    {
      return new RuleDefinition();
    }

    public IDirectoryDefinition CreateDirectoryDefinition()
    {
      return new DirectoryDefinition();
    }

    public IFileDefinition CreateFileDefinition()
    {
      return new FileDefinition();
    }
    #endregion ............................................. Factory Definitions


    #region Interface IComponentFactory impl. (FACTORY) ........................
		public override T ConfigurationFactory<T>(Type type)
		{
			try
			{
				if (type == typeof(ILanguageDeclaration)) { return (T)CreateLanguageDeclaration(); }
				if (type == typeof(IRuleDeclaration))     { return (T)CreateRuleDeclaration();     }
				if (type == typeof(ICategoryDeclaration)) { return (T)CreateCategoryDeclaration(); }
				if (type == typeof(IProjectDefinition))   { return (T)CreateProjectDefinition();   }
				if (type == typeof(ICategoryDefinition))  { return (T)CreateCategoryDefinition();  }
				if (type == typeof(IRuleDefinition))      { return (T)CreateRuleDefinition();      }
				if (type == typeof(IDirectoryDefinition)) { return (T)CreateDirectoryDefinition(); }
				if (type == typeof(IFileDefinition))      { return (T)CreateFileDefinition();      }
				throw new ConfigurationComponentException(this, -1, string.Format("Unknown type '{0}' specified for configuration factory.", type));
			}
			catch (Exception e)
			{
				throw new ConfigurationComponentException(this, -1, string.Format("Unable to instantiate an object of type '{0}' through the configuration factory.", type), e);
			}
		}
    #endregion ..................... Interface IComponentFactory impl. (FACTORY)
  }
}