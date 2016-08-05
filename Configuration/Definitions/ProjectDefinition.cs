// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Configuration.Definitions
{
  using System.Collections.Generic;
  using System.Linq;

  using Mediator;
  using Mediator.Configuration;
  using Mediator.Configuration.Declarations;
  using Mediator.Configuration.Definitions;


  internal sealed class ProjectDefinition : IProjectDefinition
  {
    #region Auto properties ....................................................
    public int                                  Id                  { get; set; }
    public bool                                 Enabled             { get; set; }
    public string                               Name                { get; set; }

    public List<IDirectoryDefinition>           Directories         { get; set; }
    public List<IDirectoryDefinition>           ExcludedDirectories { get; set; }
    public List<IDirectoryDefinition>           InvalidDirectories  { get; set; }

    public List<IFileDefinition>                Files               { get; set; }
    public List<IFileDefinition>                ExcludedFiles       { get; set; }
    public List<IFileDefinition>                InvalidFiles        { get; set; }

    public Dictionary<int, ICategoryDefinition> Categories          { get; set; }
    #endregion ................................................. Auto properties


    #region Construction .......................................................
    public ProjectDefinition()
    {
      Directories         = new List<IDirectoryDefinition>();
      ExcludedDirectories = new List<IDirectoryDefinition>();
      InvalidDirectories  = new List<IDirectoryDefinition>();

      Files               = new List<IFileDefinition>();
      ExcludedFiles       = new List<IFileDefinition>();
      InvalidFiles        = new List<IFileDefinition>();

      Categories          = new Dictionary<int, ICategoryDefinition>();
    }
    #endregion .................................................... Construction


    #region Retrievers .........................................................
    public List<int> RetrieveRuleReferenceIdsInProject()
    {
      return (from categoryDefinition in Categories from ruleDefinition in categoryDefinition.Value.Rules where ruleDefinition.Value.Enabled select ruleDefinition.Value.RuleDeclarationReferenceId).ToList();
    }

    public List<int> RetrieveLanguageIdsFromRules()
    {
      List<int> list = new List<int>();
      foreach (KeyValuePair<int, ICategoryDefinition> categoryDefinition in Categories)
      {
        if (!categoryDefinition.Value.Enabled)
          continue;

        foreach (KeyValuePair<int, IRuleDefinition> ruleDefinition in categoryDefinition.Value.Rules)
        {
          if (!ruleDefinition.Value.Enabled)
            continue;

          IConfigurationProxy  configProxy = ProxyHome.Instance.RetrieveConfigurationProxy(ConfigKeyKeeper.Instance.AccessKey);
          IRuleDeclaration ruleDeclaration = configProxy.RuleDeclarationFromCategoryIdAndRuleId(categoryDefinition.Value.CategoryDeclarationReferenceId, ruleDefinition.Value.RuleDeclarationReferenceId);

          foreach (KeyValuePair<int, ILanguageDeclaration> pair in ruleDeclaration.Languages)
          {
            ILanguageDeclaration languageReference = pair.Value;
            if (!list.Contains(languageReference.Id))
              list.Add(languageReference.Id);
          }
        }
      }
      return list;
    }

    public Dictionary<int, IRuleDefinition> AllRulesInProject()
    {
      Dictionary<int, IRuleDefinition> dictionary = new Dictionary<int, IRuleDefinition>();
      foreach (KeyValuePair<int, ICategoryDefinition> categoryDefinition in Categories)
      {
        foreach (KeyValuePair<int, IRuleDefinition> ruleDefinition in categoryDefinition.Value.Rules)
        {
          dictionary.Add(ruleDefinition.Key, ruleDefinition.Value);
        }
      }
      return dictionary;
    }
    #endregion ...................................................... Retrievers
  }
}