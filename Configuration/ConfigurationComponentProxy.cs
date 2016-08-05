// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Configuration
{
  using System;
  using System.Collections.Generic;

  using Fbb.Output;
  using Mediator.Configuration;
  using Mediator.Configuration.Declarations;
  using Mediator.Configuration.Definitions;
  using Mediator.Exceptions.Configuration;
  using Mediator.Identifiers;
  using Mediator.Proxies;
  using Mediator.Security;


  public sealed class ConfigurationComponentProxy : AbstractComponentProxy, IConfigurationProxy
  {
    #region Instance Variable(s) ...............................................
    private static volatile ConfigurationComponentProxy s_instance     = null;
    private static readonly object                      s_lockInstance = new object();
    private        readonly Zone                        _log           = null;
    #endregion ............................................ Instance Variable(s)


    #region Private properties .................................................
    private Zone Log
    {
      get { return _log ?? Out.ZoneFactory(Ids.REGION_CONFIG, GetType().Name); }
    }
    #endregion .............................................. Private properties


    #region Construction .......................................................
    private ConfigurationComponentProxy()
    {
    }
    #endregion .................................................... Construction


    #region Singleton impl. ....................................................
    public static ConfigurationComponentProxy Instance
    {
      get
      {
        lock (s_lockInstance)
        {
          return s_instance ?? (s_instance = new ConfigurationComponentProxy());
        }
      }
    }
    #endregion ................................................. Singleton impl.
    

    #region Abstract class AbstractComponentProxy impl. ........................
    protected override void InitializeKeyKeeper(ComponentAccessKey componentAccessKey)
    {
      KeyKeeper = ConfigKeyKeeper.InitializeInstance(componentAccessKey);
    }

    public override void Initialize()
    {
      Log.Debug("{0}.Initialize(): - Begin", GetType().Name);

      lock (ManagerLock)
      {
        Manager = new ConfigManager();
        Manager.Start();
      }

      Log.Debug("{0}.Initialize(): - End", GetType().Name);
    }

    public override void Start()
    {
      Initialize();
    }

    public override void Stop()
    {
      Dispose();
    }
    #endregion ..................... Abstract class AbstractComponentProxy impl.


    #region Interface IConfigurationProxy impl. (DECLARATIONS) .................
    public string ConfigurationFileVersion()
    {
      lock (ManagerLock)
      {
        return ((ConfigManager)Manager).Declarations.Version;
      }
    }

    public Dictionary<int, ILanguageDeclaration> LanguageDeclarations()
    {
      lock (ManagerLock)
      {
        return ((ConfigManager)Manager).Declarations.Languages;
      }
    }

    public ILanguageDeclaration LanguageDeclaration(int languageId)
    {
      lock (ManagerLock)
      {
        if (!((ConfigManager)Manager).Declarations.Languages.ContainsKey(languageId))
          throw new ConfigurationComponentException(string.Format("Unable to find language declaration from 'languageId'='{0}'", languageId));

        return ((ConfigManager)Manager).Declarations.Languages[languageId];
      }
    }

    public Dictionary<int, ICategoryDeclaration> CategoryDeclarations()
    {
      lock (ManagerLock)
      {
        return ((ConfigManager)Manager).Declarations.Categories;
      }
    }

    public ICategoryDeclaration CategoryDeclaration(int categoryId)
    {
      lock (ManagerLock)
      {
        Dictionary<int, ICategoryDeclaration> categories = CategoryDeclarations();
        if (!categories.ContainsKey(categoryId))
          return null;

        return ((ConfigManager)Manager).Declarations.Categories[categoryId];
      }
    }

    public Dictionary<int, IRuleDeclaration> RuleDeclarationsFromCategoryId(int categoryId)
    {
      lock (ManagerLock)
      {
        Dictionary<int, ICategoryDeclaration> categories = CategoryDeclarations();
        if (!categories.ContainsKey(categoryId))
          throw new ConfigurationComponentException(string.Format("Unable to find category declaration from 'categoryId'='{0}'", categoryId));

        ICategoryDeclaration category = categories[categoryId];
        return category.Rules;
      }
    }

    public IRuleDeclaration RuleDeclarationFromRuleId(int ruleId)
    {
      lock (ManagerLock)
      {
        foreach (KeyValuePair<int, ICategoryDeclaration> categoryDeclaration in ((ConfigManager)Manager).Declarations.Categories)
        {
          foreach (KeyValuePair<int, IRuleDeclaration> ruleDeclaration in categoryDeclaration.Value.Rules)
          {
            if (ruleDeclaration.Value.Id == ruleId)
              return ruleDeclaration.Value;
          }
        }
        return null;
      }
    }

    public IRuleDeclaration RuleDeclarationFromCategoryIdAndRuleId(int categoryId, int ruleId)
    {
      lock (ManagerLock)
      {
        Dictionary<int, IRuleDeclaration> rules = RuleDeclarationsFromCategoryId(categoryId);
        if (!rules.ContainsKey(ruleId))
          throw new ConfigurationComponentException(string.Format("Unable to find rule declaration from 'ruleId'='{0}' in category declaration with 'categoryId'='{1}'.", ruleId, categoryId));

        return rules[ruleId];
      }
    }
    #endregion .............. Interface IConfigurationProxy impl. (DECLARATIONS)


    #region Interface IConfigurationProxy impl. (DEFINITIONS) ..................
    public Dictionary<int, IProjectDefinition> Projects()
    {
      lock (ManagerLock)
      {
        return ((ConfigManager)Manager).Definitions.Projects;
      }
    }

    public IProjectDefinition Project(int projectId)
    {
      lock (ManagerLock)
      {
        if (projectId <= 0)
          throw new ArgumentException("The argument 'projectId' cannot be less than or equal to '0'!");

        Dictionary<int, IProjectDefinition> projects = ((ConfigManager)Manager).Definitions.Projects;
        if (projects == null)
          return null;

        if (!projects.ContainsKey(projectId))
          return null;

        return projects[projectId];
      }
    }

    public void CreateProject(int projectId, string projectName, bool isProjectEnabled)
    {
      lock (ManagerLock)
      {
        try
        {
          ((ConfigManager)Manager).CreateProject(projectId, projectName, isProjectEnabled);
        }
        catch (Exception e)
        {
          throw new ConfigurationComponentException(Manager, -1, "Unable to create project: Project Id='" + projectId + "', Project name='" + projectName + "'.", e);
        }
      }
    }

    public List<IDirectoryDefinition> ProjectDirectories(int projectId)
    {
      lock (ManagerLock)
      {
        IProjectDefinition project = Project(projectId);
        return project.Directories;
      }
    }

    public List<IDirectoryDefinition> ProjectExcludedDirectories(int projectId)
    {
      lock (ManagerLock)
      {
        IProjectDefinition project = Project(projectId);
        return project.ExcludedDirectories;
      }
    }

    public List<IFileDefinition> ProjectFiles(int projectId)
    {
      lock (ManagerLock)
      {
        IProjectDefinition project = Project(projectId);
        return project.Files;
      }
    }

    public List<IFileDefinition> ProjectExcludedFiles(int projectId)
    {
      lock (ManagerLock)
      {
        IProjectDefinition project = Project(projectId);
        return project.ExcludedFiles;
      }
    }

    public Dictionary<int, ICategoryDefinition> ProjectCategories(int projectId)
    {
      lock (ManagerLock)
      {
        IProjectDefinition project = Project(projectId);
        return project.Categories;
      }
    }

    public Dictionary<int, IRuleDefinition> ProjectRules(int projectId)
    {
      lock (ManagerLock)
      {
        IProjectDefinition project = Project(projectId);
        return project.AllRulesInProject();
      }
    }
    #endregion ............... Interface IConfigurationProxy impl. (DEFINITIONS)
  }
}
