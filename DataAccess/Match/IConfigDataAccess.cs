// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2014 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.DataAccess.Match
{
  using System.Collections.Generic;

  using Mediator.Configuration.Declarations;
  using Mediator.Configuration.Definitions;


  internal interface IConfigDataAccess
  {
    IEnumerable<ILanguageDeclaration> LoadLanguageDeclarations();
    IEnumerable<ICategoryDeclaration> LoadCategoryDeclarations();
    IEnumerable<IProjectDefinition> LoadProjectDefinitions();
  }
}