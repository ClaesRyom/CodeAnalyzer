// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Configuration
{
  using System;
  using System.Collections.Generic;

  using Declarations;
  using Definitions;


  public interface IConfigurationProxy
  {
    #region Declarations .......................................................
    string                                ConfigurationFileVersion();

    Dictionary<int, ILanguageDeclaration> LanguageDeclarations();
    ILanguageDeclaration                  LanguageDeclaration(int languageId);

    Dictionary<int, ICategoryDeclaration> CategoryDeclarations();
    ICategoryDeclaration                  CategoryDeclaration(int categoryId);

    Dictionary<int, IRuleDeclaration>     RuleDeclarationsFromCategoryId(int categoryId);
    IRuleDeclaration                      RuleDeclarationFromRuleId(int ruleId);
    IRuleDeclaration                      RuleDeclarationFromCategoryIdAndRuleId(int categoryId, int ruleId);
    #endregion .................................................... Declarations


    #region Definitions ........................................................
    Dictionary<int, IProjectDefinition>  Projects();
    IProjectDefinition                   Project(int projectId);

    void                                 CreateProject(int projectId, string projectName, bool isProjectEnabled);

    List<IDirectoryDefinition>           ProjectDirectories(int projectId);
    List<IDirectoryDefinition>           ProjectExcludedDirectories(int projectId);

    List<IFileDefinition>                ProjectFiles(int projectId);
    List<IFileDefinition>                ProjectExcludedFiles(int projectId);

    Dictionary<int, ICategoryDefinition> ProjectCategories(int projectId);
    Dictionary<int, IRuleDefinition>     ProjectRules(int projectId);
    #endregion ..................................................... Definitions
	}
}