// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Matches
{
	using System;

	using Configuration;
	using Configuration.Declarations;
	using Configuration.Definitions;
	using Label;


	public interface IMatch : ICloneable
  {
    #region Auto properties ....................................................
		Guid                 Id                     { get; set; }
		IBatch               Batch                  { get; set; }
    MatchStatus          Result                 { get; set; }
    RuleSeverity         Severity               { get; set; }
    int                  LineNumber             { get; set; }
    string               CodeExtract            { get; set; }
    IRuleDeclaration     RuleDeclarationRef     { get; set; }
		IRuleDefinition      RuleDefinitionRef      { get; set; }
		ICategoryDeclaration CategoryDeclarationRef { get; set; }
		ICategoryDefinition  CategoryDefinitionRef  { get; set; }
		IProjectDefinition   ProjectDefinitionRef   { get; set; }
		ILanguageDeclaration LanguageDeclarationRef { get; set; }
		string               RootDirectoryPath      { get; set; }
    string               Filename               { get; set; }
    #endregion ................................................. Auto properties
  }
}