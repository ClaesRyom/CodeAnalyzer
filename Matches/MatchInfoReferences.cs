// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Matches
{
	using Mediator.Configuration.Declarations;
	using Mediator.Configuration.Definitions;
	using Mediator.Matches;


	internal struct MatchInfoReferences : IMatchInfoReferences
  {
    public IProjectDefinition   ProjectDefinitionReference   { get; set; }
    public ICategoryDeclaration CategoryDeclarationReference { get; set; }
		public ICategoryDefinition  CategoryDefinitionReference  { get; set; }
		public IRuleDeclaration     RuleDeclarationReference     { get; set; }
		public IRuleDefinition      RuleDefinitionReference      { get; set; }
		public ILanguageDeclaration LanguageDeclarationReference { get; set; }
    public string               RootDirectory                { get; set; }
  }
}