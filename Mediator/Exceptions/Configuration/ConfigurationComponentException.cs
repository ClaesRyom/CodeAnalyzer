﻿// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Exceptions.Configuration
{
  using System;
  using System.Runtime.Serialization;
  using Fbb.Exceptions.Base;


  [Serializable()]
  public class ConfigurationComponentException : BaseException
  {
    #region Construction .......................................................
    public ConfigurationComponentException() { }

    public ConfigurationComponentException(string msg) : base(msg) { }

    public ConfigurationComponentException(string msg, Exception innerException) : base(msg, innerException) { }

    public ConfigurationComponentException(object src, int code, string msg) : base(src, code, msg) { }

    public ConfigurationComponentException(object src, int code, string msg, Exception innerException) : base(src, code, msg, innerException) { }

    public ConfigurationComponentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    #endregion .................................................... Construction


    #region Interface BaseException implementations ............................
    public override string ExceptionTypeName
    {
      get { return GetType().FullName; }
    }
    #endregion ......................... Interface BaseException implementations
  }
}
