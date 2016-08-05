// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Exceptions
{
  using System;
  using System.Runtime.Serialization;

  using Fbb.Exceptions.Base;


  [Serializable()]
  public class MissingInitializationException : BaseException
  {
    #region Construction .......................................................
    public MissingInitializationException() {}

    public MissingInitializationException(string msg) : base(msg) {}

    public MissingInitializationException(string msg, System.Exception innerException) : base(msg, innerException) {}

    public MissingInitializationException(object src, int code, string msg) : base(src, code, msg) {}

    public MissingInitializationException(object src, int code, string msg, System.Exception innerException) : base(src, code, msg, innerException) {}

    public MissingInitializationException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    #endregion .................................................... Construction


    #region Interface BaseException implementations ............................
    public override string ExceptionTypeName
    {
      get { return GetType().FullName; }
    }
    #endregion ......................... Interface BaseException implementations
  }
}
