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
  using Proxies;


  public sealed class ComponentAccessKey : AbstractKey, IComponentAccessKey
  {
    #region Auto properties ....................................................
    public IKeyConsumer KeyConsumer { get; private set; }
    public string       Description { get; private set; }
    #endregion ................................................. Auto properties


    #region Construction .......................................................
    public ComponentAccessKey(byte[] key, string description, IKeyConsumer keyConsumer) : base(key)
    {
      #region Validate input arguments...
      if (key.Length <= 0)
        throw new ArgumentException("Invalid key.");

      if (string.IsNullOrWhiteSpace(description))
        throw new ArgumentNullException("description");

      if (keyConsumer == null)
        throw new ArgumentNullException("keyConsumer");
      #endregion

      Description = description;
      KeyConsumer = keyConsumer;
    }
    #endregion .................................................... Construction
  }
}