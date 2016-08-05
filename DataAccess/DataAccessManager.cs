// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.DataAccess
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Fbb.Output;
	using Mapper.ToDomainModel;
	using Mapper.ToEntityModel;
	using Match;
	using Mediator;
	using Mediator.Configuration.Declarations;
	using Mediator.Configuration.Definitions;
	using Mediator.Exceptions.DataAccess;
	using Mediator.Identifiers;
	using Mediator.Managers;
	using Mediator.Matches;
	using Mediator.Label;
	using Model;
	using Version;


	internal sealed class DataAccessManager : AbstractManager, IMatchDataAccess, IVersionDataAccess, IConfigDataAccess
	{
		#region AbstractManager Property overrides .................................
		protected override Zone Log
		{
			get { return _log ?? Out.ZoneFactory(Ids.REGION_DATA_ACCESS, GetType().Name); }
		}
		#endregion .............................. AbstractManager Property overrides


		#region Construction .......................................................
		public DataAccessManager() {}
		#endregion .................................................... Construction


		#region Abstract class AbstractManager overrides ...........................
		public override void Initialize()
		{
		}

		public override void Start()
		{
			try
			{
				// Let's do the initialization...
				Initialize();
			}
			catch (Exception e)
			{
				string s = string.Format("Unexpected behaviour detected during the initialization of the data access manager!");
				Log.Error(s, e);

				throw new DataAccessComponentException(this, -1, s, e);
			}
		}

		public override void Stop()
		{
			Dispose(true);
		}
		#endregion ........................ Abstract class AbstractManager overrides


		#region Implementation of IDisposable ......................................
		protected override void Dispose(bool disposing)
		{
			// If you need thread safety, use a lock around these  
			// operations, as well as in your methods that use the resource. 
			if (Disposed)
				return;

			// If disposing equals true, dispose all managed  
			// and unmanaged resources. 
			if (disposing)
			{
				//// Dispose managed resources.
				//if (Home != null)
				//{
				//  Home.Dispose();
				//}

				// Call the appropriate methods to clean up  
				// unmanaged resources here. 
				// If disposing is false,  
				// only the following code is executed.
			}

			// Indicate that the instance has been disposed.
			Disposed = true;
		}
		#endregion ................................... Implementation of IDisposable


		#region Interface IMatchDataAccess .........................................
		public void CreateMatch(IMatch match)
		{
      if (match == null)
        throw new ArgumentNullException("match");

      using (CodeAnalyzerContainer context = new CodeAnalyzerContainer())
      {
        // Mapping: Domain Model => Entity Model.
        Model.Match m = ToEntityModelMapper.MatchMapper(match, context);

        // Do the actual DB update.
        context.Match.Add(m);
        context.SaveChanges();
      }
		}

		public void ReadMatch(Guid id)
		{
			throw new NotImplementedException();
		}

		public void UpdateMatch(IMatch match)
		{
			throw new NotImplementedException();
		}

		public void DeleteMatch(Guid id)
		{
			throw new NotImplementedException();
		}

		public void DeleteMatch(IMatch match)
		{
			throw new NotImplementedException();
		}

	  public void CreateBulkOfMatches(List<IMatch> matches)
	  {
			if (matches == null)
				throw new ArgumentNullException("matches");

			// Get the overall batch identification from the Mediator.
			IBatch batchId = ProxyHome.Instance.RetrieveExecutionIdentification(DataAccessKeyKeeper.Instance.AccessKey);

			using (CodeAnalyzerContainer ctx = new CodeAnalyzerContainer())
			{
				Model.Batch batch = new Model.Batch { TimeStamp = batchId.TimeStamp };

				foreach (IMatch match in matches)
				{
					// Mapping: Domain Model => Entity Model.
					Model.Match m = new Model.Match()
					{
						LineNumber        = match.LineNumber,
						CodeExtract       = match.CodeExtract,
						RootDirectoryPath = match.RootDirectoryPath,
						Filename          = match.Filename,
						Batch             = batch,
						Status            = (int)match.Result,
						RuleDeclaration   = ToEntityModelMapper.RuleDeclarationMapper(match.RuleDeclarationRef, ctx),
					};
					
					// Do the actual DB update.
					ctx.Match.Add(m);
				}
				ctx.SaveChanges();
			}
		}
		#endregion ...................................... Interface IMatchDataAccess


    #region Implementation of IConfigDataAccess ................................
    public IEnumerable<ILanguageDeclaration> LoadLanguageDeclarations()
    {
      using (CodeAnalyzerContainer context = new CodeAnalyzerContainer())
      {
				var result = context.LanguageDeclaration.Select(l => l);
				//var result = from languages in context.LanguageDeclaration
				//						 select languages;


        List<ILanguageDeclaration> domainLanguageDeclarations = new List<ILanguageDeclaration>();
        foreach (LanguageDeclaration lang in result)
        {
          ILanguageDeclaration l = ToDomainModelMapper.LanguageDeclarationMapper(lang);

          domainLanguageDeclarations.Add(l);
        }
        return domainLanguageDeclarations;
      }
    }

    public IEnumerable<ICategoryDeclaration> LoadCategoryDeclarations()
    {
      using (CodeAnalyzerContainer context = new CodeAnalyzerContainer())
      {
        var result = from categoryDeclarations in context.CategoryDeclaration
                     select categoryDeclarations;


        List<ICategoryDeclaration> domainCategoryDeclarations = new List<ICategoryDeclaration>();
        foreach (CategoryDeclaration categoryDeclaration in result)
        {
          ICategoryDeclaration c = ToDomainModelMapper.CategoryDeclarationMapper(categoryDeclaration);

          domainCategoryDeclarations.Add(c);
        }
        return domainCategoryDeclarations;
      }
    }

    public IEnumerable<IProjectDefinition> LoadProjectDefinitions()
    {
      using (CodeAnalyzerContainer context = new CodeAnalyzerContainer())
      {
				//var result = from projectDefinitions in context.ProjectDefinition
				//						 select projectDefinitions;

	      var result = context.ProjectDefinition.Select(p => p);

        List<IProjectDefinition> domainProjectDefinitions = new List<IProjectDefinition>();
        foreach (ProjectDefinition projectDefinition in result)
        {
          IProjectDefinition p = ToDomainModelMapper.ProjectDefinitionMapper(projectDefinition);
          domainProjectDefinitions.Add(p);
        }
        return domainProjectDefinitions;
      }
    }
    #endregion ............................. Implementation of IConfigDataAccess 


    #region Interface IVersionDataAccess .......................................
    public void CreateVersionNumber()
    {
      using (CodeAnalyzerContainer context = new CodeAnalyzerContainer())
      {
        // Mapping: Domain Model => Entity Model.
	      Model.Version v = new Model.Version { VersionNumber = Ids.DATABASE_VERSION_NUMBER };
				
	      // Do the actual DB update.
        context.Version.Add(v);
        context.SaveChanges();
      }
    }

    public void UpdateVersionNumber(Guid id, string newVersNum)
    {
			using (CodeAnalyzerContainer context = new CodeAnalyzerContainer())
			{
				var query = context.Version.Find(id);

				if(query != null)
				{
					query.VersionNumber = newVersNum;
					context.SaveChanges();
				}
			}
		}

		public string ReadVersionNumber(Guid id)
		{
			using (CodeAnalyzerContainer context = new CodeAnalyzerContainer())
			{
				var query = context.Version.Find(id);
				return query?.VersionNumber;
			}
		}

		public void DeleteVersionNumber(Guid id)
    {
			//using (CodeAnalyzerContainer context = new CodeAnalyzerContainer())
			//{
			//	var query = context.Version.Find(id);
			//	return query?.VersionNumber;
			//}
		}
		#endregion .................................... Interface IVersionDataAccess
	}
}