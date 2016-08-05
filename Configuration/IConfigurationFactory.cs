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
  using Mediator.Configuration.Declarations;
  using Mediator.Configuration.Definitions;


  internal interface IInternalConfigurationFactory
  {
    #region Factory Declarations ...............................................
    ILanguageDeclaration CreateLanguageDeclaration();
	  ICategoryDeclaration CreateCategoryDeclaration();
    IRuleDeclaration     CreateRuleDeclaration();
    #endregion ............................................ Factory Declarations


    #region Factory Definitions ................................................
    IProjectDefinition   CreateProjectDefinition();
    ICategoryDefinition  CreateCategoryDefinition();
    IRuleDefinition      CreateRuleDefinition();
    IDirectoryDefinition CreateDirectoryDefinition();
    IFileDefinition      CreateFileDefinition();
    #endregion ............................................. Factory Definitions
  }
}