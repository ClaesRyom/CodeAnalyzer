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
	using Configuration.Declarations;
	using Configuration.Definitions;


  public interface IMatchInfoReferences
	{
    IProjectDefinition   ProjectDefinitionReference   { get; set; }
    ICategoryDeclaration CategoryDeclarationReference { get; set; }
		ICategoryDefinition  CategoryDefinitionReference  { get; set; }
    IRuleDeclaration     RuleDeclarationReference     { get; set; }
		IRuleDefinition      RuleDefinitionReference      { get; set; }
    ILanguageDeclaration LanguageDeclarationReference { get; set; }
    string               RootDirectory                { get; set; }
	}
}