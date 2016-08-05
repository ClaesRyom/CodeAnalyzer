// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Output
{
	using System;
	using System.ComponentModel;


	internal class MatchMappedToExcel
	{
		[DisplayName("BatchId Id")]
		public string BatchId { get; set; }

		[DisplayName("Timestamp")]
		public DateTime Timestamp { get; set; }
		
		[DisplayName("Project name")]
		public string ProjectName { get; set; }
		
		[DisplayName("Match Id")]
		public string MatchId { get; set; }
		
		[DisplayName("Severity")]
		public string Severity { get; set; }
		
		[DisplayName("Language")]
		public string Language { get; set; }
		
		[DisplayName("Category")]
		public string Category { get; set; }
		
		[DisplayName("Category description")]
		public string CategoryDescription { get; set; }
		
		[DisplayName("Rule")]
		public string Rule { get; set; }
		
		[DisplayName("Rule description")]
		public string RuleDescription { get; set; }
		
		[DisplayName("Rule expression")]
		public string RuleExpression { get; set; }
		
		[DisplayName("File name")]
		public string Filename { get; set; }
		
		[DisplayName("Match on line")]
		public string MatchOnLine { get; set; }
		
		[DisplayName("Code extract")]
		public string CodeExtract { get; set; }
	}
}