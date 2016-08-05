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
	using System;
	using Mediator.Configuration;
	using Mediator.Configuration.Declarations;
	using Mediator.Configuration.Definitions;
	using Mediator.Label;
	using Mediator.Matches;


	internal class Match : IMatch
  {
    #region Auto properties ....................................................
		public Guid                 Id                     { get; set; }
		public IBatch               Batch                  { get; set; }
    public MatchStatus          Result                 { get; set; }
    public RuleSeverity         Severity               { get; set; }
    public int                  LineNumber             { get; set; }
    public string               CodeExtract            { get; set; }
    public IRuleDeclaration     RuleDeclarationRef     { get; set; }
		public IRuleDefinition      RuleDefinitionRef      { get; set; }
		public ICategoryDeclaration CategoryDeclarationRef { get; set; }
		public ICategoryDefinition  CategoryDefinitionRef  { get; set; }
		public IProjectDefinition   ProjectDefinitionRef   { get; set; }
		public ILanguageDeclaration LanguageDeclarationRef { get; set; }
		public string               RootDirectoryPath      { get; set; }
    public string               Filename               { get; set; }
    #endregion ................................................. Auto properties


    #region Construction .......................................................
    public Match()
    {
      Result                = MatchStatus.NotDefined;
      Severity              = RuleSeverity.NotDefined;
      LineNumber            = int.MinValue;
      CodeExtract           = string.Empty;
      RuleDeclarationRef    = null;
			RuleDefinitionRef     = null;
	    CategoryDefinitionRef = null;
	    ProjectDefinitionRef  = null;
      RootDirectoryPath     = string.Empty;
      Filename              = string.Empty;
    }
    #endregion .................................................... Construction


    #region Interface ICloneable impl. .........................................
    public object Clone()
    {
      Match match = new Match();

			match.Id                     = Id;
			match.Batch                  = Batch;
      match.Result                 = Result;
      match.Severity               = Severity;
      match.LineNumber             = LineNumber;
      match.CodeExtract            = (string.IsNullOrWhiteSpace(CodeExtract)) ? string.Empty : CodeExtract.Clone() as string;
      match.RuleDeclarationRef     = RuleDeclarationRef;
			match.RuleDefinitionRef      = RuleDefinitionRef;
	    match.CategoryDeclarationRef = CategoryDeclarationRef;
			match.CategoryDefinitionRef  = CategoryDefinitionRef;
			match.ProjectDefinitionRef   = ProjectDefinitionRef;
	    match.LanguageDeclarationRef = LanguageDeclarationRef;
      match.RootDirectoryPath      = RootDirectoryPath.Clone() as string;
      match.Filename               = Filename.Clone() as string;

      return match;
    }
    #endregion ...................................... Interface ICloneable impl.
  }
}
