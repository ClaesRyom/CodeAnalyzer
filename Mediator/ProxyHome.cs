// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator
{
  using System;
  using System.Globalization;
  using System.Runtime.Caching;
  using System.Text;

  using Configuration;
  using Cryptography;
  using DataAccess;
  using Engine;
  using Factories;
  using Label;
  using Matches;
  using Output;
  using Proxies;
  using Security;
  using Statistics;


  public sealed class ProxyHome : IDisposable
  {
    #region Instance Variables .................................................
    private static volatile ProxyHome s_instance;
    private static readonly object    s_lockInstance = new object();
    #endregion .............................................. Instance Variables


    #region Auto properties ....................................................
		private  bool                Disposed                { get; set; }
    private  MemoryCache         Cache                   { get; set; }
    internal IConfigurationProxy ConfigurationProxy      { get; set; }
    internal IComponentFactory   ConfigurationFactory    { get; set; }
    internal IEngineProxy        EngineProxy             { get; set; }
		internal IMatchProxy         MatchProxy              { get; set; }
    internal IStatisticsProxy    StatisticsProxy         { get; set; }
    internal IOutputProxy        OutputProxy             { get; set; }
		internal IDataAccessProxy    DataAccessProxy         { get; set; }
    internal IBatch              BatchExecutionIdentification { get; set; }
		#endregion ................................................. Auto properties


    #region Proxies & Factories access methods .................................
    public IBatch RetrieveExecutionIdentification(ComponentAccessKey key)
    {
      // Check the cache before trying to unfold/decrypt the key. The first time 
      // we look into the cache it will be empty and we will be forced to unfold/decrypt
      // the key but that is the plan. :-)
      if (CheckCache(key))
        return BatchExecutionIdentification;


      // Let's try to unfold/decrypt the key and validate whether or not the key represents
      // the correct permissions.
      ComponentDeclaration accessKey = UnfoldKey(key);
      if ((accessKey & ComponentDeclaration.ExecutionId) != ComponentDeclaration.ExecutionId)
        throw new ArgumentException("Invalid key! The key did NOT contain permissions for retrieving the execution identification.");


      // Key contained the correct permissions, so let's update the cache.
      InsertInCache(key);


      // We should be ready to release the proxy object...
      return BatchExecutionIdentification;
    }

    public IConfigurationProxy RetrieveConfigurationProxy(ComponentAccessKey key)
    {
      // Check the cache before trying to unfold/decrypt the key. The first time 
      // we look into the cache it will be empty and we will be forced to unfold/decrypt
      // the key but that is the plan. :-)
      if (CheckCache(key))
        return ConfigurationProxy;


      // Let's try to unfold/decrypt the key and validate whether or not the key represents
      // the correct permissions.
      ComponentDeclaration accessKey = UnfoldKey(key);
      if ((accessKey & ComponentDeclaration.Configuration) != ComponentDeclaration.Configuration)
        throw new ArgumentException("Invalid key! The key did NOT contain permissions for retrieving the configuration proxy.");


      // Key contained the correct permissions, so let's update the cache.
      InsertInCache(key);


      // We should be ready to release the proxy object...
      return ConfigurationProxy;
    }

    public IComponentFactory RetrieveConfigurationFactory(ComponentAccessKey key)
    {
      // Check the cache before trying to unfold/decrypt the key. The first time 
      // we look into the cache it will be empty and we will be forced to unfold/decrypt
      // the key but that is the plan. :-)
      if (CheckCache(key))
        return ConfigurationFactory;


      // Let's try to unfold/decrypt the key and validate whether or not the key represents
      // the correct permissions.
      ComponentDeclaration accessKey = UnfoldKey(key);
      if ((accessKey & ComponentDeclaration.Configuration) != ComponentDeclaration.Configuration)
        throw new ArgumentException("Invalid key! The key did NOT contain permissions for retrieving the configuration factory.");


      // Key contained the correct permissions, so let's update the cache.
      InsertInCache(key);


      // We should be ready to release the proxy object...
      return ConfigurationFactory;
    }

    public IEngineProxy RetrieveEngineProxy(ComponentAccessKey key)
    {
      // Check the cache before trying to unfold/decrypt the key. The first time 
      // we look into the cache it will be empty and we will be forced to unfold/decrypt
      // the key but that is the plan. :-)
      if (CheckCache(key))
        return EngineProxy;


      // Let's try to unfold/decrypt the key and validate whether or not the key represents
      // the correct permissions.
      ComponentDeclaration accessKey = UnfoldKey(key);
      if ((accessKey & ComponentDeclaration.Engine) != ComponentDeclaration.Engine)
        throw new ArgumentException("Invalid key! The key did NOT contain permissions for retrieving the engine proxy.");


      // Key contained the correct permissions, so let's update the cache.
      InsertInCache(key);


      // We should be ready to release the proxy object...
      return EngineProxy;
    }

    public IMatchProxy RetrieveMatchProxy(ComponentAccessKey key)
    {
      // Check the cache before trying to unfold/decrypt the key. The first time 
      // we look into the cache it will be empty and we will be forced to unfold/decrypt
      // the key but that is the plan. :-)
      if (CheckCache(key))
        return MatchProxy;


      // Let's try to unfold/decrypt the key and validate whether or not the key represents
      // the correct permissions.
      ComponentDeclaration accessKey = UnfoldKey(key);
      if ((accessKey & ComponentDeclaration.Matches) != ComponentDeclaration.Matches)
        throw new ArgumentException("Invalid key! The key did NOT contain permissions for retrieving the matches proxy.");


      // Key contained the correct permissions, so let's update the cache.
      InsertInCache(key);


      // We should be ready to release the proxy object...
			return MatchProxy;
    }

    public IStatisticsProxy RetrieveStatisticsProxy(ComponentAccessKey key)
    {
      // Check the cache before trying to unfold/decrypt the key. The first time 
      // we look into the cache it will be empty and we will be forced to unfold/decrypt
      // the key but that is the plan. :-)
      if (CheckCache(key))
        return StatisticsProxy;


      // Let's try to unfold/decrypt the key and validate whether or not the key represents
      // the correct permissions.
      ComponentDeclaration accessKey = UnfoldKey(key);
      if ((accessKey & ComponentDeclaration.Statistics) != ComponentDeclaration.Statistics)
        throw new ArgumentException("Invalid key! The key did NOT contain permissions for retrieving the statistics proxy.");


      // Key contained the correct permissions, so let's update the cache.
      InsertInCache(key);


      // We should be ready to release the proxy object...
      return StatisticsProxy;
    }

    public IOutputProxy RetrieveOutputProxy(ComponentAccessKey key)
    {
      // Check the cache before trying to unfold/decrypt the key. The first time 
      // we look into the cache it will be empty and we will be forced to unfold/decrypt
      // the key but that is the plan. :-)
      if (CheckCache(key))
        return OutputProxy;


      // Let's try to unfold/decrypt the key and validate whether or not the key represents
      // the correct permissions.
      ComponentDeclaration accessKey = UnfoldKey(key);
      if ((accessKey & ComponentDeclaration.Output) != ComponentDeclaration.Output)
        throw new ArgumentException("Invalid key! The key did NOT contain permissions for retrieving the output proxy.");


      // Key contained the correct permissions, so let's update the cache.
      InsertInCache(key);


      // We should be ready to release the proxy object...
      return OutputProxy;
    }

    public IConfigDataAccessProxy RetrieveConfigDataAccessProxy(ComponentAccessKey key)
    {
      return RetrieveDataAccessProxy(key);
    }

    public IMatchDataAccessProxy RetrieveMatchDataAccessProxy(ComponentAccessKey key)
    {
      return RetrieveDataAccessProxy(key);
    }

    public IDataAccessProxy RetrieveDataAccessProxy(ComponentAccessKey key)
    {
      // Check the cache before trying to unfold/decrypt the key. The first time 
      // we look into the cache it will be empty and we will be forced to unfold/decrypt
      // the key but that is the plan. :-)
      if (CheckCache(key))
        return (IDataAccessProxy)DataAccessProxy;


      // Let's try to unfold/decrypt the key and validate whether or not the key represents
      // the correct permissions.
      ComponentDeclaration accessKey = UnfoldKey(key);
			if ((accessKey & ComponentDeclaration.DataAccess) != ComponentDeclaration.DataAccess)
        throw new ArgumentException("Invalid key! The key did NOT contain permissions for retrieving the match data access proxy.");


      // Key contained the correct permissions, so let's update the cache.
      InsertInCache(key);


      // We should be ready to release the proxy object...
			return (IDataAccessProxy)DataAccessProxy;
    }

    private ComponentDeclaration UnfoldKey(ComponentAccessKey key)
    {
      if (key == null)
        throw new ArgumentNullException("key");

      AesCryptography aes = new AesCryptography();
      byte[] bytes = aes.Decrypt(SecurityIds.Password, Encoding.UTF8.GetBytes(SecurityIds.Salt), key.Key);

      string keyString = Encoding.UTF8.GetString(bytes);

      ComponentDeclaration accessKey;
      bool result = Enum.TryParse(keyString, true, out accessKey);
      if (!result)
        throw new ArgumentException("Invalid key! Unable to parse key");
      return accessKey;
    }

    private void InsertInCache(ComponentAccessKey key)
    {
      Cache.Add(new CacheItem(key.KeyConsumer.GetHashCode().ToString(CultureInfo.InvariantCulture), key.KeyConsumer),
                new CacheItemPolicy { AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration, Priority = CacheItemPriority.NotRemovable });
    }

    private bool CheckCache(ComponentAccessKey key)
    {
      return Cache.Contains(key.KeyConsumer.GetHashCode().ToString(CultureInfo.InvariantCulture));
    }
		
		public void InsertInCache(string filename)
		{
			Cache.Add(new CacheItem(filename, filename),
								new CacheItemPolicy { AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration, Priority = CacheItemPriority.NotRemovable });
		}

		public bool CheckCache(string filename)
		{
			return Cache.Contains(filename);
		}
    #endregion .............................. Proxies & Factories access methods


    #region Construction .......................................................
    private ProxyHome()
    {
      Cache = MemoryCache.Default;
    }
    #endregion .................................................... Construction


    #region Destruction ........................................................
    // Implement IDisposable. 
    // Do not make this method virtual. 
    // A derived class should not be able to override this method.
    public void Dispose()
    {
      Dispose(true);

      // This object will be cleaned up by the Dispose method. 
      // Therefore, you should call GC.SupressFinalize to 
      // take this object off the finalization queue  
      // and prevent finalization code for this object 
      // from executing a second time.
      GC.SuppressFinalize(this);
    }

    // Dispose(bool disposing) executes in two distinct scenarios. 
    // If disposing equals true, the method has been called directly 
    // or indirectly by a user's code. Managed and unmanaged resources 
    // can be disposed. 
    // If disposing equals false, the method has been called by the  
    // runtime from inside the finalizer and you should not reference  
    // other objects. Only unmanaged resources can be disposed. 
    private void Dispose(bool disposing)
    {
      lock (s_lockInstance)
      {
        // If you need thread safety, use a lock around these  
        // operations, as well as in your methods that use the resource. 
        if (Disposed)
          return;

        // If disposing equals true, dispose all managed  
        // and unmanaged resources. 
        if (disposing)
        {
          // Dispose managed resources.
          if (ConfigurationProxy != null)
            ((IComponentProxy)ConfigurationProxy).Dispose();

          if (StatisticsProxy != null)
            ((IComponentProxy)StatisticsProxy).Dispose();

          if (EngineProxy != null)
            ((IComponentProxy)EngineProxy).Dispose();

          if (MatchProxy != null)
						((IComponentProxy)MatchProxy).Dispose();

          if (Cache != null)
            Cache.Dispose();

          // Call the appropriate methods to clean up  
          // unmanaged resources here. 
          // If disposing is false,  
          // only the following code is executed.
        }

        // Indicate that the instance has been disposed.
        ConfigurationProxy = null;
        StatisticsProxy    = null;
        EngineProxy        = null;
        MatchProxy         = null;
        Cache              = null;
        Disposed           = true;
      }
    }
    #endregion ..................................................... Destruction


    #region Singleton impl. ....................................................
    public static ProxyHome Instance
    {
      get
      {
        lock (s_lockInstance)
        {
          return s_instance ?? (s_instance = new ProxyHome());
        }
      }
    }
    #endregion ................................................. Singleton impl.
  }
}