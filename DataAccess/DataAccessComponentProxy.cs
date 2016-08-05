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
	using Fbb.Output;
	using Mediator.Configuration.Declarations;
	using Mediator.Configuration.Definitions;
	using Mediator.DataAccess;
	using Mediator.Exceptions.DataAccess;
	using Mediator.Identifiers;
	using Mediator.Matches;
	using Mediator.Proxies;
	using Mediator.Security;


	public class DataAccessComponentProxy : AbstractComponentProxy, IDataAccessProxy, IVersionDataAccessProxy
	{
		#region Construction .......................................................
		private DataAccessComponentProxy()
		{
		}
		#endregion .................................................... Construction


		#region Private properties .................................................
		private Zone Log
		{
			get { return _log ?? Out.ZoneFactory(Ids.REGION_DATA_ACCESS, GetType().Name); }
		}
		#endregion .............................................. Private properties


		#region Singleton impl. ....................................................
		public static DataAccessComponentProxy Instance
		{
			get
			{
				lock (s_lockInstance)
				{
					return s_instance ?? (s_instance = new DataAccessComponentProxy());
				}
			}
		}
		#endregion ................................................. Singleton impl.


		#region Instance Variable(s) ...............................................
		private static volatile DataAccessComponentProxy s_instance;
		private static readonly object                   s_lockInstance = new object();
		private        readonly Zone                     _log           = null;
		#endregion ............................................ Instance Variable(s)


		#region Interface IComponentProxy impl. ....................................
		protected override void InitializeKeyKeeper(ComponentAccessKey componentAccessKey)
		{
			KeyKeeper = DataAccessKeyKeeper.InitializeInstance(componentAccessKey);
		}

		public override void Initialize()
		{
			Log.Debug("{0}.Initialize(): - Begin", GetType().Name);

			lock (ManagerLock)
			{
				try
				{
					Manager = new DataAccessManager();
					Manager.Start();
				}
				catch (Exception e)
				{
					throw new DataAccessComponentException(this, -1, $"Unable to initialize the {GetType().Name}.", e);
				}
			}

			Log.Debug("{0}.Initialize(): - End", GetType().Name);
		}

		public override void Start()
		{
			Initialize();
		}

		public override void Stop()
		{
			Dispose();
		}


		#region Helper
		private new DataAccessManager Manager
		{
			get { return (DataAccessManager) base.Manager; }
			set { base.Manager = value; }
		}
		#endregion


		#endregion ................................. Interface IComponentProxy impl.


		#region Interface IDataAccessProxy impl. ...................................


		#region Match data access...
		public void CreateMatch(IMatch match)
		{
			lock (s_lockInstance)
			{
				try
				{
					Manager.CreateMatch(match);
				}
				catch (Exception e)
				{
					throw new DataAccessComponentException(this, -1,
						$"Failed to create match: BatchId={match.Batch.Id}; Id={match.Id}; RuleDeclaration={match.RuleDeclarationRef}; RootDirectoryPath={match.RootDirectoryPath}; Filename={match.Filename}; LineNumber={match.LineNumber}",
						e);
				}
			}
		}

		public void ReadMatch(int id)
		{
			throw new NotImplementedException();
		}

		public void UpdateMatch(IMatch match)
		{
			throw new NotImplementedException();
		}

		public void DeleteMatch(int id)
		{
			throw new NotImplementedException();
		}

		public void DeleteMatch(IMatch match)
		{
			throw new NotImplementedException();
		}

		public void CreateBulkOfMatches(List<IMatch> matches)
		{
			lock (s_lockInstance)
			{
				try
				{
					Manager.CreateBulkOfMatches(matches);
				}
				catch (Exception e)
				{
					throw new DataAccessComponentException(this, -1, $"Failed to create bulk of matches.", e);
				}
			}
		}
		#endregion


		#region Configuration data access...
		public IEnumerable<ILanguageDeclaration> LoadLanguageDeclarations()
		{
			lock (s_lockInstance)
			{
				try
				{
					return Manager.LoadLanguageDeclarations();
				}
				catch (Exception e)
				{
					throw new DataAccessComponentException(this, -1, $"Failed to load language declarations.", e);
				}
			}
		}

		public IEnumerable<ICategoryDeclaration> LoadCategoryDeclarations()
		{
			lock (s_lockInstance)
			{
				try
				{
					return Manager.LoadCategoryDeclarations();
				}
				catch (Exception e)
				{
					throw new DataAccessComponentException(this, -1, $"Failed to load category declarations.", e);
				}
			}
		}

		public IEnumerable<IProjectDefinition> LoadProjectDefinitions()
		{
			lock (s_lockInstance)
			{
				try
				{
					return Manager.LoadProjectDefinitions();
				}
				catch (Exception e)
				{
					throw new DataAccessComponentException(this, -1, $"Failed to load project definitions.", e);
				}
			}
		}
		#endregion


		#endregion ................................ Interface IDataAccessProxy impl.


		#region Interface IVersionDataAccessProxy impl. ............................
		public void CreateVersionNumber()
		{
			lock (s_lockInstance)
			{
				try
				{
					Manager.CreateVersionNumber();
				}
				catch (Exception e)
				{
					throw new DataAccessComponentException(this, -1, $"Failed to create version number and save it to the database.", e);
				}
			}
		}

		public void UpdateVersionNumber(Guid id, string newVersNum)
		{
			lock (s_lockInstance)
			{
				try
				{
					Manager.UpdateVersionNumber(id, newVersNum);
				}
				catch (Exception e)
				{
					throw new DataAccessComponentException(this, -1, $"Failed to update version number. ID='{id}', Value='{newVersNum}'", e);
				}
			}
		}

		public string ReadVersionNumber(Guid id)
		{
			lock (s_lockInstance)
			{
				try
				{
					return Manager.ReadVersionNumber(id);
				}
				catch (Exception e)
				{
					throw new DataAccessComponentException(this, -1, $"Failed to read version number. ID='{id}'", e);
				}
			}
		}

		public void DeleteVersionNumber(Guid id)
		{
			lock (s_lockInstance)
			{
				try
				{
					Manager.DeleteVersionNumber(id);
				}
				catch (Exception e)
				{
					throw new DataAccessComponentException(this, -1, $"Failed to delete version number. ID='{id}'", e);
				}
			}
		}
		#endregion ......................... Interface IVersionDataAccessProxy impl.
	}
}