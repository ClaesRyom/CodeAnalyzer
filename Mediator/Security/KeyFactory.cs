// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Security
{
  using System;
  using System.Text;

  using Configuration;
  using Cryptography;
  using DataAccess;
  using Engine;
  using Matches;
  using Output;
  using Proxies;
  using Statistics;


  public sealed class KeyFactory
	{
		#region Generate component access keys .....................................
		internal ComponentAccessKey GenerateComponentAccessKey(string password, string salt, IKeyConsumer keyConsumer)
    {
      #region Validate input arguments...
      if (string.IsNullOrWhiteSpace(password))
        throw new ArgumentNullException("password");

      if (string.IsNullOrWhiteSpace(salt))
        throw new ArgumentNullException("salt");

      if (keyConsumer == null)
        throw new ArgumentNullException("keyConsumer");
      #endregion
      

      // All flags are default cleared!
      ComponentDeclaration accessSetup = ComponentDeclaration.NotDefined;


      #region Setup the component access flags for the 'Configuration' component
      if (keyConsumer is IConfigurationProxy)
      {
        accessSetup = accessSetup | ComponentDeclaration.Configuration;
        accessSetup = accessSetup | ComponentDeclaration.Statistics;
        accessSetup = accessSetup | ComponentDeclaration.DataAccess;
      }
      #endregion


      #region Setup the component access flags for the 'Configuration' component
      if (keyConsumer is IConfigurationFactory)
      {
        accessSetup = accessSetup | ComponentDeclaration.Statistics;
      }
      #endregion


      #region Setup the component access flags for the 'Engine' component
      if (keyConsumer is IEngineProxy)
      {
        // Setup the component access flags for the configuration component.
        accessSetup = accessSetup | ComponentDeclaration.Configuration;
        accessSetup = accessSetup | ComponentDeclaration.Engine;
        accessSetup = accessSetup | ComponentDeclaration.Matches;
        accessSetup = accessSetup | ComponentDeclaration.Statistics;
      }
      #endregion


      #region Setup the component access flags for the 'Output' component
      if (keyConsumer is IOutputProxy)
      {
        // Setup the component access flags for the configuration component.
        accessSetup = accessSetup | ComponentDeclaration.Configuration;
        // accessSetup = accessSetup | ComponentDeclaration.Engine;
        accessSetup = accessSetup | ComponentDeclaration.Statistics;
        accessSetup = accessSetup | ComponentDeclaration.Matches;
      }
      #endregion


      #region Setup the component access flags for the 'Matches' component
			if (keyConsumer is IMatchProxy)
      {
        // Setup the component access flags for the configuration component.
        accessSetup = accessSetup | ComponentDeclaration.Statistics;
        accessSetup = accessSetup | ComponentDeclaration.DataAccess;
      }
      #endregion


      #region Setup the component access flags for the 'Statistics' component
      if (keyConsumer is IStatisticsProxy)
      {
        // Setup the component access flags for the configuration component.
        accessSetup = accessSetup | ComponentDeclaration.Statistics;
      }
      #endregion


      #region Setup the component access flags for the 'DataAccess' component
      if (keyConsumer is IDataAccessProxy)
      {
        // Setup the component access flags for the configuration component.
        accessSetup = accessSetup | ComponentDeclaration.DataAccess;
        accessSetup = accessSetup | ComponentDeclaration.Configuration;
      }
      #endregion


      // Create the description for the key - the description must declare
      // what components the flags give access to.
      string description = GenerateKeyDescription(accessSetup);

      // Let's do the actual encryption...
      AesCryptography aes = new AesCryptography();
      byte[]        bytes = aes.Encrypt(password, Encoding.UTF8.GetBytes(salt), Encoding.UTF8.GetBytes(accessSetup + ""));

      // Constructing the actual key for accessing the components...
      return new ComponentAccessKey(bytes, description, keyConsumer);
    }
		#endregion .................................. Generate component access keys

		
		#region Helper methods .....................................................
		private string GenerateKeyDescription(ComponentDeclaration flag)
    {
      StringBuilder sb = new StringBuilder();

     
      sb.Append("This key provide access to the following components: \n");

      if ((flag & ComponentDeclaration.Configuration) == ComponentDeclaration.Configuration) 
        sb.Append("  - ").Append(ComponentDeclaration.Configuration + "\n");

      if ((flag & ComponentDeclaration.Engine) == ComponentDeclaration.Engine)
        sb.Append("  - ").Append(ComponentDeclaration.Engine + "\n");

			if ((flag & ComponentDeclaration.Output) == ComponentDeclaration.Output)
				sb.Append("  - ").Append(ComponentDeclaration.Output + "\n");

      if ((flag & ComponentDeclaration.Matches) == ComponentDeclaration.Matches)
        sb.Append("  - ").Append(ComponentDeclaration.Matches + "\n");

      if ((flag & ComponentDeclaration.Statistics) == ComponentDeclaration.Statistics)
        sb.Append("  - ").Append(ComponentDeclaration.Statistics + "\n");

			if ((flag & ComponentDeclaration.DataAccess) == ComponentDeclaration.DataAccess)
				sb.Append("  - ").Append(ComponentDeclaration.DataAccess + "\n");

      return sb.ToString();
    }

    private byte[] GetBytes(string str)
    {
      byte[] bytes = new byte[str.Length * sizeof(char)];
      Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
      return bytes;
    }

    private string GetString(byte[] bytes)
    {
      char[] chars = new char[bytes.Length / sizeof(char)];
      Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
      return new string(chars);
    }
    #endregion .................................................. Helper methods
  }
}