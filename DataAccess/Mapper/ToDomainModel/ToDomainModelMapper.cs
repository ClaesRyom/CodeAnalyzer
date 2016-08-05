// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2014 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.DataAccess.Mapper.ToDomainModel
{
  using System;

  using Mediator;
  using Mediator.Configuration;
  using Mediator.Configuration.Declarations;
  using Mediator.Configuration.Definitions;
  using Mediator.Exceptions.DataAccess;
  using Mediator.Factories;
  using Mediator.Proxies;


  internal static class ToDomainModelMapper
  {
    #region Mappers from Entity Framework Model to Domain Model ................
    public static ILanguageDeclaration LanguageDeclarationMapper(Model.LanguageDeclaration languageDeclaration)
    {
      ILanguageDeclaration language = null;
      try
      {
        language = ConfigurationComponentFactory().ConfigurationFactory<ILanguageDeclaration>(typeof (ILanguageDeclaration));
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Configuration proxy factory failure - unable to create an instance of " + typeof(ILanguageDeclaration) + "?", e);
      }


      try
      {
        language.Id        = languageDeclaration.Id;
        language.Name      = languageDeclaration.Name;
        language.Extension = languageDeclaration.Extension;
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Mapping process failure?", e);
      }
      return language;
    }

    public static ICategoryDeclaration CategoryDeclarationMapper(Model.CategoryDeclaration categoryDeclaration)
    {
      ICategoryDeclaration category = null;
      try
      {
        category = ConfigurationComponentFactory().ConfigurationFactory<ICategoryDeclaration>(typeof(ICategoryDeclaration));
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Configuration proxy factory failure - unable to create an instance of " + typeof(ICategoryDeclaration) + "?", e);
      }


      try
      {
        category.Id          = categoryDeclaration.Id;
        category.Name        = categoryDeclaration.Name;
        category.Description = categoryDeclaration.Description;

        foreach (var rule in categoryDeclaration.Rules) { category.Rules.Add(rule.Id, RuleDeclarationMapper(category, rule)); }
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Mapping process failure?", e);
      }
      return category;
    }

    public static IRuleDeclaration RuleDeclarationMapper(Model.RuleDeclaration ruleDeclaration)
    {
      return RuleDeclarationMapper(null, ruleDeclaration);
    }

    public static IRuleDeclaration RuleDeclarationMapper(ICategoryDeclaration parentDeclaration, Model.RuleDeclaration ruleDeclaration)
    {
      IRuleDeclaration rule = null;
      try
      {
        rule = ConfigurationComponentFactory().ConfigurationFactory<IRuleDeclaration>(typeof(IRuleDeclaration));
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Configuration proxy factory failure - unable to create an instance of " + typeof(IRuleDeclaration) + "?", e);
      }


      try
      {
        rule.Id                = ruleDeclaration.Id;
        rule.Name              = ruleDeclaration.Name;
        rule.Description       = ruleDeclaration.Description;
        rule.Expression        = ruleDeclaration.Expression;
        rule.Severity          = RuleSeverityMapper.Int2RuleSeverity(ruleDeclaration.Severity);
        rule.ParentDeclaration = parentDeclaration;

        foreach (var language in ruleDeclaration.LanguageDeclaration) { rule.Languages.Add(language.Id, LanguageDeclarationMapper(language)); }
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Mapping process failure?", e);
      }
      return rule;
    }

    public static IProjectDefinition ProjectDefinitionMapper(Model.ProjectDefinition projectDefinition)
    {
      if (projectDefinition == null)
        throw new ArgumentNullException("projectDefinition");


      // Check if the project definition is already loaded...
      IProjectDefinition project = ConfigurationComponentProxy().Project(projectDefinition.Id);
      if (project == null)
      {
        try
        {
          project = ConfigurationComponentFactory().ConfigurationFactory<IProjectDefinition>(typeof (IProjectDefinition));

          project.Id      = projectDefinition.Id;
          project.Name    = projectDefinition.Name;
          project.Enabled = projectDefinition.Enabled;

          // Let's add the project to the configuration component...
          //ConfigurationComponentProxy().CreateProject(project.Id, project.Name, project.Enabled);
        }
        catch (Exception e)
        {
          throw new DataAccessComponentException(null, -1, "Configuration proxy factory failure - unable to create an instance of " + typeof (IProjectDefinition) + "?",
            e);
        }
      }


      try
      {
        foreach (var catDef in projectDefinition.CategoryDefinitions) { project.Categories.Add(catDef.Id, CategoryDefinitionMapper(project, catDef)); } // Category definitions...
        foreach (var dir    in projectDefinition.Directories)         { project.Directories.Add(DirectoryDefinitionMapper(dir));                      } // Included directories...
        foreach (var exDir  in projectDefinition.ExcludedDirectories) { project.ExcludedDirectories.Add(ExcludedDirectoryDefinitionMapper(exDir));    } // Excluded directories...
        foreach (var file   in projectDefinition.Files)               { project.Files.Add(FileDefinitionMapper(file));                                } // Files...
        foreach (var exFile in projectDefinition.ExcludedFiles)       { project.ExcludedFiles.Add(ExcludedFileDefinitionMapper(exFile));              } // Excluded files...
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Mapping process failure?", e);
      }
      return project;
    }

    public static ICategoryDefinition CategoryDefinitionMapper(Model.CategoryDefinition categoryDefinition)
    {
      return CategoryDefinitionMapper(null, categoryDefinition);
    }

    public static ICategoryDefinition CategoryDefinitionMapper(IProjectDefinition parentProjectDefinition, Model.CategoryDefinition categoryDefinition)
    {
      ICategoryDefinition category = null;
      try
      {
        category = ConfigurationComponentFactory().ConfigurationFactory<ICategoryDefinition>(typeof (ICategoryDefinition));
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Configuration proxy factory failure - unable to create an instance of " + typeof(ICategoryDefinition) + "?", e);
      }


      try
      {
        category.Id                               = categoryDefinition.Id;
        category.Enabled                          = categoryDefinition.Enabled;
        category.ParentDefinition                 = parentProjectDefinition;
        category.CategoryDeclarationReferenceId   = categoryDefinition.CategoryDeclaration.Id;
        category.CategoryDeclarationReferenceName = categoryDefinition.CategoryDeclaration.Name;

        foreach (var ruleDef in categoryDefinition.RuleDefinitions) { category.Rules.Add(ruleDef.Id, RuleDefinitionMapper(category, ruleDef)); }
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Mapping process failure?", e);
      }
      return category;
    }

    public static IRuleDefinition RuleDefinitionMapper(Model.RuleDefinition ruleDefinition)
    {
      return RuleDefinitionMapper(null, ruleDefinition);
    }

    public static IRuleDefinition RuleDefinitionMapper(ICategoryDefinition parentCategoryDefinition, Model.RuleDefinition ruleDefinition)
    {
      IRuleDefinition rule = null;
      try
      {
        rule = ConfigurationComponentFactory().ConfigurationFactory<IRuleDefinition>(typeof(IRuleDefinition));
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Configuration proxy factory failure - unable to create an instance of " + typeof(IRuleDefinition) + "?", e);
      }


      try
      {
        rule.Id                           = ruleDefinition.Id;
        rule.Enabled                      = ruleDefinition.Enabled;
        rule.ParentDefinition             = parentCategoryDefinition;
        rule.RuleDeclarationReferenceId   = ruleDefinition.RuleDeclaration.Id;
        rule.RuleDeclarationReferenceName = ruleDefinition.RuleDeclaration.Name;
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Mapping process failure?", e);
      }
      return rule;
    }

    public static IDirectoryDefinition DirectoryDefinitionMapper(Model.Directory directoryDefinition)
    {
      IDirectoryDefinition directory = null;
      try
      {
        directory = ConfigurationComponentFactory().ConfigurationFactory<IDirectoryDefinition>(typeof(IDirectoryDefinition));
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Configuration proxy factory failure - unable to create an instance of " + typeof(IDirectoryDefinition) + "?", e);
      }


      try
      {
        directory.Enabled = directoryDefinition.Enabled;
        directory.Path    = directoryDefinition.Path;
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Mapping process failure?", e);
      }
      return directory;
    }

    public static IDirectoryDefinition ExcludedDirectoryDefinitionMapper(Model.ExcludedDirectory excludedDirectoryDefinition)
    {
      IDirectoryDefinition directory = null;
      try
      {
        directory = ConfigurationComponentFactory().ConfigurationFactory<IDirectoryDefinition>(typeof(IDirectoryDefinition));
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Configuration proxy factory failure - unable to create an instance of " + typeof(IDirectoryDefinition) + "?", e);
      }


      try
      {
        directory.Enabled = excludedDirectoryDefinition.Enabled;
        directory.Path    = excludedDirectoryDefinition.Path;
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Mapping process failure?", e);
      }
      return directory;
    }

    public static IFileDefinition FileDefinitionMapper(Model.File fileDefinition)
    {
      IFileDefinition file = null;
      try
      {
        file = ConfigurationComponentFactory().ConfigurationFactory<IFileDefinition>(typeof(IFileDefinition));
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Configuration proxy factory failure - unable to create an instance of " + typeof(IFileDefinition) + "?", e);
      }


      try
      {
        file.Enabled = fileDefinition.Enabled;
        file.Path    = fileDefinition.Path;
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Mapping process failure?", e);
      }
      return file;
    }

    public static IFileDefinition ExcludedFileDefinitionMapper(Model.ExcludedFile excludedFileDefinition)
    {
      IFileDefinition file = null;
      try
      {
        file = ConfigurationComponentFactory().ConfigurationFactory<IFileDefinition>(typeof(IFileDefinition));
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Configuration proxy factory failure - unable to create an instance of " + typeof(IFileDefinition) + "?", e);
      }


      try
      {
        file.Enabled = excludedFileDefinition.Enabled;
        file.Path    = excludedFileDefinition.Path;
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Mapping process failure?", e);
      }
      return file;
    }
    #endregion ............. Mappers from Entity Framework Model to Domain Model


    #region Helper methods .....................................................
    private static IConfigurationProxy ConfigurationComponentProxy()
    {
      IConfigurationProxy proxy = null;
      try
      {
        proxy = ProxyHome.Instance.RetrieveConfigurationProxy(DataAccessKeyKeeper.Instance.AccessKey);
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Error: Something unexpected occurred while trying to get an instance of the configuration proxy, " + typeof(IConfigurationProxy) + "?", e);
      }

      if (proxy == null)
        throw new DataAccessComponentException(null, -1, "Error: " + typeof(IConfigurationProxy) + " was null?");

      return proxy;
    }

    private static IComponentFactory ConfigurationComponentFactory()
    {
      IComponentFactory factory = null;
      try
      {
        factory = ProxyHome.Instance.RetrieveConfigurationFactory(DataAccessKeyKeeper.Instance.AccessKey);
      }
      catch (Exception e)
      {
        throw new DataAccessComponentException(null, -1, "Error: Something unexpected occurred while trying to get an instance of the configuration factory, " + typeof(IComponentFactory) + "?", e);
      }

      if (factory == null)
        throw new DataAccessComponentException(null, -1, "Error: " + typeof(IComponentFactory) + " was null?");

      return factory;
    }
    #endregion .................................................. Helper methods
  }
}
