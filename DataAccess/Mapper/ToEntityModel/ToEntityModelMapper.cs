// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2014 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------

using System.Linq;

namespace CodeAnalyzer.DataAccess.Mapper.ToEntityModel
{
  using System;
  using System.Collections.Generic;

  using CodeAnalyzer.Version;
  using Mediator.Configuration.Declarations;
  using Mediator.Configuration.Definitions;
  using Mediator.Label;
  using Mediator.Matches;
  using Mediator.User;
  using Model;
  using MatchStatus = Mediator.Matches.MatchStatus;


  internal static class ToEntityModelMapper
  {
    public static Model.ProjectDefinition ProjectDefinitionMapper(IProjectDefinition projectDefinition)
    {
      Model.ProjectDefinition p = new Model.ProjectDefinition() 
      { 
        Id      = (projectDefinition.Id != int.MinValue) ? projectDefinition.Id : int.MinValue,
        Enabled = projectDefinition.Enabled, 
        Name    = projectDefinition.Name,
      };

      // Category definitions...
      foreach (KeyValuePair<int, ICategoryDefinition> pair in projectDefinition.Categories) { p.CategoryDefinitions.Add(CategoryDefinitionMapper(pair.Value)); }

      // Directories...
      foreach (IDirectoryDefinition directoryDefinition in projectDefinition.Directories)
      {
        p.Directories.Add(new Model.Directory() 
        {
          Id                = (directoryDefinition.Id != int.MinValue) ? directoryDefinition.Id : int.MinValue,
          Enabled           = directoryDefinition.Enabled,
          Path              = directoryDefinition.Path,
          ProjectDefinition = p,
        });
      }

      // Excluded directories...
      foreach (IDirectoryDefinition directoryDefinition in projectDefinition.ExcludedDirectories)
      {
        p.ExcludedDirectories.Add(new Model.ExcludedDirectory() 
        {
          Id                = (directoryDefinition.Id != int.MinValue) ? directoryDefinition.Id : int.MinValue,
          Enabled           = directoryDefinition.Enabled,
          Path              = directoryDefinition.Path,
          ProjectDefinition = p,
        });
      }

      // Files...
      foreach (IFileDefinition fileDefinition in projectDefinition.Files)
      {
        p.Files.Add(new Model.File()
        {
          Id                = (fileDefinition.Id != int.MinValue) ? fileDefinition.Id : int.MinValue,
          Enabled           = fileDefinition.Enabled,
          Path              = fileDefinition.Path,
          ProjectDefinition = p,
        });
      }

      // Excluded files...
      foreach (var fileDefinition in projectDefinition.ExcludedFiles)
      {
        p.ExcludedFiles.Add(new Model.ExcludedFile()
        {
          Id                = (fileDefinition.Id != int.MinValue) ? fileDefinition.Id : int.MinValue,
          Enabled           = fileDefinition.Enabled,
          Path              = fileDefinition.Path,
          ProjectDefinition = p,
        });
      }

      // Owner...
      p.Owner = new Model.User() { FirstName = "Claes", MiddleName = "", LastName = "Ryom", Email = "claesryom@gmail.com", Password = "claeser" };

      // Contributers...
      p.Contributers = new Model.User() { FirstName = "Kim", MiddleName = "Stig", LastName = "Ryom", Email = "kim.ryom@gmail.com", Password = "kimmer" };

      // Followers...
      p.Contributers = new Model.User() { FirstName = "Marianne", MiddleName = "", LastName = "Ryom", Email = "marianneryom@hotmail.com", Password = "Dulle" };

      return p;
    }

    public static Model.CategoryDefinition CategoryDefinitionMapper(ICategoryDefinition categoryDefinition)
    {
      throw new NotImplementedException();
    }

    public static Model.RuleDefinition RuleDefinitionMapper(IRuleDefinition ruleDefinition)
    {
      return new Model.RuleDefinition()
      {
        Enabled = ruleDefinition.Enabled,

      };
    }

    public static Model.User UserMapper(IUser user)
    {
      throw new NotImplementedException();
    }

    public static Model.UserRole UserRoleMapper()
    {
      throw new NotImplementedException();
    }

		public static Model.CategoryDeclaration CategoryDeclarationMapper(ICategoryDeclaration declaration, CodeAnalyzerContainer context)
    {
			Model.CategoryDeclaration categoryDeclaration = context.CategoryDeclaration.SingleOrDefault(c => c.Id == declaration.Id);
			if (categoryDeclaration == null)
				return new Model.CategoryDeclaration
				{
					Name        = declaration.Name,
					Description = declaration.Description,
				};
			return categoryDeclaration;
    }

		public static Model.RuleDeclaration RuleDeclarationMapper(IRuleDeclaration declaration, CodeAnalyzerContainer context)
    {
			Model.RuleDeclaration ruleDeclaration = context.RuleDeclaration.SingleOrDefault(r => r.Id == declaration.Id);
			if (ruleDeclaration == null)
				return new Model.RuleDeclaration
				{
					Name                = declaration.Name,
					Description         = declaration.Description,
					Severity            = (int)declaration.Severity,
					Expression          = declaration.Expression,
					CategoryDeclaration = CategoryDeclarationMapper(declaration.ParentDeclaration, context),
				};
			return ruleDeclaration;
    }

		public static Model.LanguageDeclaration LanguageDeclarationMapper(ILanguageDeclaration declaration, CodeAnalyzerContainer context)
    {
			Model.LanguageDeclaration languageDeclaration = context.LanguageDeclaration.SingleOrDefault(l => l.Id == declaration.Id);
			if (languageDeclaration == null)
				return new Model.LanguageDeclaration
				{
					Name      = declaration.Name,
					Extension = declaration.Extension,
				};
			return languageDeclaration;
		}

		public static Model.Match MatchMapper(IMatch match, CodeAnalyzerContainer context)
    {
      return new Model.Match
      {
        LineNumber        = match.LineNumber,
        CodeExtract       = match.CodeExtract,
        RootDirectoryPath = match.RootDirectoryPath,
        Filename          = match.Filename,
        Batch             = BatchMapper(match.Batch, context),
        Status            = (int)match.Result,
				RuleDeclaration   = RuleDeclarationMapper(match.RuleDeclarationRef, context),
      };
    }

		public static Model.Batch BatchMapper(IBatch batch, CodeAnalyzerContainer context)
		{
			Model.Batch modelBatch = context.Batch.SingleOrDefault(b => b.Id == batch.Id);
			if (modelBatch == null)
				return new Model.Batch {TimeStamp = batch.TimeStamp};
			return modelBatch;
		}

    public static Model.Version VersionMapper()
    {
      return new Model.Version {VersionNumber = Vers.Application};
    }
  }
}