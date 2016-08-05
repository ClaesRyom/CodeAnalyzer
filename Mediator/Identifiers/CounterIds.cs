// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Identifiers
{
  public enum CounterIds
  {
    LanguageDeclarations,
    CategoryDeclarations,
    RuleDeclarations,

    Files,
		TotalNumberOfLinesInFiles,
		TotalNumberOfWhitespaceLinesInFiles,

    TotalProjectDefinitions,
    ActiveProjectDefinitions,

    TotalCategoryDefinitions,
    ActiveCategoryDefinitions,

    TotalRuleDefinitions,
    ActiveRuleDefinitions,

    InfoMatches,
    WarningMatches,
    CriticalMatches,
    FatalMatches,
  }
}
