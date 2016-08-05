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
	using System.Collections.Generic;

	using Fbb.Exceptions.Base;
	using Mediator.Configuration;
	using Mediator.Exceptions.Matches;
	using Mediator.Identifiers;
	using Mediator.Matches;
	using Mediator.Proxies;
	using Mediator.Security;
	using Fbb.Output;


	public class MatchComponentProxy : AbstractComponentProxy, IMatchProxy
  {
    #region Instance Variable(s) ...............................................
    private static volatile MatchComponentProxy s_instance     = null;
    private static readonly object              s_lockInstance = new object();
    private        readonly Zone                _log           = null;
    #endregion ............................................ Instance Variable(s)


    #region Private properties .................................................
    private Zone Log
    {
      get { return _log ?? Out.ZoneFactory(Ids.REGION_ENGINE, GetType().Name); }
    }
    #endregion .............................................. Private properties


    #region Construction .......................................................
		private MatchComponentProxy()
    {
    }
    #endregion .................................................... Construction


    #region Singleton impl. ....................................................
		public static MatchComponentProxy Instance
    {
      get
      {
        lock (s_lockInstance)
        {
					return s_instance ?? (s_instance = new MatchComponentProxy());
        }
      }
    }
    #endregion ................................................. Singleton impl.

		
    #region Interface IComponentProxy impl. ....................................
    protected override void InitializeKeyKeeper(ComponentAccessKey componentAccessKey)
    {
      KeyKeeper = MatchKeyKeeper.InitializeInstance(componentAccessKey);
    }

    public override void Initialize()
    {
      Log.Debug("{0}.Initialize(): - Begin", GetType().Name);

      lock (ManagerLock)
      {
        Manager = new MatchManager();
        Manager.Start();
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
		private new MatchManager Manager
		{
			get { return (MatchManager)base.Manager; } 
			set { base.Manager = value; }
		}
		#endregion
		#endregion ................................. Interface IComponentProxy impl.

		
		#region Interface IMatchProxy impl. ........................................
		/// <summary>
		/// Returns the total number of matches found during the search regardless 
		/// of project dependency, category, rule or severity.
		/// </summary>
		public int MatchesFound()
		{
			try
			{
				lock (ManagerLock)
				{
					return Manager.NumberOfMatchesFound();
				}
			}
			catch (BaseException be)
			{
				throw new MatchComponentException(this, -1, "Unable to retrieve the number of matches found.", be);
			}
		}

		/// <summary>
		/// Returns all the matches that was found during the search regardless of 
		/// project dependency, category, rule or severity.
		/// 
		/// Note: All matches are returned as clones of the internal representation 
		/// of the match.
		/// </summary>
		public List<IMatch> Matches()
		{
			try
			{
				lock (ManagerLock)
				{
					return Manager.Matches();
				}
			}
			catch (BaseException be)
			{
				throw new MatchComponentException(this, -1, "Unable to retrieve the matches found, that is cloned copies.", be);
			}
		}

		public int MatchesFoundByProject(int id)
		{
			try
			{
				lock (ManagerLock)
				{
					return Manager.MatchesFoundByProject(id);
				}
			}
			catch (BaseException be)
			{
				string s = string.Format("Unable to retrieve the number of matches found by 'project'. ID = '{0}'.", id);
				throw new MatchComponentException(this, -1, s, be);
			}
		}

		public List<IMatch> MatchesByProject(int id)
		{
			try
			{
				lock (ManagerLock)
				{
					return Manager.MatchesByProject(id);
				}
			}
			catch (BaseException be)
			{
				string s = string.Format("Unable to retrieve matches by 'project'. ID = '{0}'.", id);
				throw new MatchComponentException(this, -1, s, be);
			}
		}

		public int MatchesFoundByCategory(int id)
		{
			try
			{
				lock (ManagerLock)
				{
					return Manager.MatchesFoundByCategory(id);
				}
			}
			catch (BaseException be)
			{
				string s = string.Format("Unable to retrieve the number of matches found by 'category'. ID = '{0}'.", id);
				throw new MatchComponentException(this, -1, s, be);
			}
		}

		public List<IMatch> MatchesByCategory(int id)
		{
			try
			{
				lock (ManagerLock)
				{
					return Manager.MatchesByCategory(id);
				}
			}
			catch (BaseException be)
			{
				string s = string.Format("Unable to retrieve matches by 'category'. ID = '{0}'.", id);
				throw new MatchComponentException(this, -1, s, be);
			}
		}

		public int MatchesFoundByRule(int id)
		{
			try
			{
				lock (ManagerLock)
				{
					return Manager.MatchesFoundByRule(id);
				}
			}
			catch (BaseException be)
			{
				string s = string.Format("Unable to retrieve the number of matches found by 'rule'. ID = '{0}'.", id);
				throw new MatchComponentException(this, -1, s, be);
			}
		}

		public List<IMatch> MatchesByRules(int id)
		{
			try
			{
				lock (ManagerLock)
				{
					return Manager.MatchesByRules(id);
				}
			}
			catch (BaseException be)
			{
				string s = string.Format("Unable to retrieve matches by 'rule'. ID = '{0}'.", id);
				throw new MatchComponentException(this, -1, s, be);
			}
		}

		public int MatchesFoundBySeverity(RuleSeverity severity)
		{
			try
			{
				lock (ManagerLock)
				{
					return Manager.MatchesFoundBySeverity(severity);
				}
			}
			catch (BaseException be)
			{
				string s = string.Format("Unable to retrieve the number of matches found by 'severity'. Severity = '{0}'.", severity);
				throw new MatchComponentException(this, -1, s, be);
			}
		}

		public List<IMatch> MatchesBySeverity(RuleSeverity severity)
		{
			try
			{
				lock (ManagerLock)
				{
					return Manager.MatchesBySeverity(severity);
				}
			}
			catch (BaseException be)
			{
				string s = string.Format("Unable to retrieve matches by 'severity'. Severity = '{0}'.", severity);
				throw new MatchComponentException(this, -1, s, be);
			}
		}

		public int MatchesFoundInDirectory(string path)
		{
			try
			{
				lock (ManagerLock)
				{
					return Manager.MatchesFoundInDirectory(path);
				}
			}
			catch (BaseException be)
			{
				string s = string.Format("Unable to retrieve the number of matches found in directory 'path'. Path= '{0}'.", path);
				throw new MatchComponentException(this, -1, s, be);
			}
		}

		public List<IMatch> MatchesInDirectory(string path)
		{
			try
			{
				lock (ManagerLock)
				{
					return Manager.MatchesInDirectory(path);
				}
			}
			catch (BaseException be)
			{
				string s = string.Format("Unable to retrieve matches in 'directory'. Path = '{0}'.", path);
				throw new MatchComponentException(this, -1, s, be);
			}
		}

		public int MatchesFoundInFile(string path)
		{
			try
			{
				lock (ManagerLock)
				{
					return Manager.MatchesFoundInFile(path);
				}
			}
			catch (BaseException be)
			{
				string s = string.Format("Unable to retrieve the number of matches found in file 'path'. Path= '{0}'.", path);
				throw new MatchComponentException(this, -1, s, be);
			}
		}

		public List<IMatch> MatchesInFile(string path)
		{
			try
			{
				lock (ManagerLock)
				{
					return Manager.MatchesInFile(path);
				}
			}
			catch (BaseException be)
			{
				string s = string.Format("Unable to retrieve matches in 'file'. Path = '{0}'.", path);
				throw new MatchComponentException(this, -1, s, be);
			}
		}

		public void AddMatch(IMatch match)
		{
			try
			{
				lock (ManagerLock)
				{
					Manager.AddMatch(match);
				}
			}
			catch (BaseException be)
			{
				string s = string.Format("Unable to add match to internal repository.");
				throw new MatchComponentException(this, -1, s, be);
			}
		}
		#endregion ..................................... Interface IMatchProxy impl.


    #region Interface ICongigurationProxy impl. (FACTORY) ......................
		public T MatchesFactory<T>(Type type)
		{
			lock (ManagerLock) 
			{
				try
				{
					if (type == typeof(IMatchInfoReferences)) { return (T)(Manager).Factory.MatchInfoReferenceFactory(); }
					if (type == typeof(IMatch))               { return (T)(Manager).Factory.MatchFactory(); }
					throw new MatchComponentException(this, -1, string.Format("Unknow type '{0}' specified for match factory.", type));
				}
				catch (Exception e)
				{
					throw new MatchComponentException(this, -1, string.Format("Unable to instantiate an object of type '{0}' through the match factory.", type), e);
				}

			}
		}
    #endregion ................... Interface ICongigurationProxy impl. (FACTORY)	
	}
}