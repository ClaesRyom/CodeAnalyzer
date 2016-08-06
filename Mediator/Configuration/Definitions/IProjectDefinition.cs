// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Configuration.Definitions
{
  using System.Collections.Generic;


  public interface IProjectDefinition
  {
    #region Auto properties ....................................................
    bool                                 Enabled             { get; set; }
    string                               Name                { get; set; }
    int                                  Id                  { get; set; }

		IDirectoryDefinition                 RootDirectory       { get; set; }
    List<IDirectoryDefinition>           Directories         { get; set; }
    List<IDirectoryDefinition>           ExcludedDirectories { get; set; }
    List<IDirectoryDefinition>           InvalidDirectories  { get; set; }

    List<IFileDefinition>                Files               { get; set; }
    List<IFileDefinition>                ExcludedFiles       { get; set; }
    List<IFileDefinition>                InvalidFiles        { get; set; }

    Dictionary<int, ICategoryDefinition> Categories          { get; set; }
    #endregion ................................................. Auto properties


    #region Retrievers .........................................................
    List<int>                        RetrieveRuleReferenceIdsInProject();
    List<int>                        RetrieveLanguageIdsFromRules();
    Dictionary<int, IRuleDefinition> AllRulesInProject();
    #endregion ...................................................... Retrievers
  }
}