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
  public class InvalidArgumentException : BaseException
  {
    #region Construction .......................................................
    public InvalidArgumentException() { }

    public InvalidArgumentException(string msg) : base(msg) { }

    public InvalidArgumentException(string msg, System.Exception innerException) : base(msg, innerException) { }

    public InvalidArgumentException(object src, int code, string msg) : base(src, code, msg) { }

    public InvalidArgumentException(object src, int code, string msg, System.Exception innerException) : base(src, code, msg, innerException) { }

    public InvalidArgumentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    #endregion .................................................... Construction


    #region Interface BaseException implementations ............................
    public override string ExceptionTypeName
    {
      get { return GetType().FullName; }
    }
    #endregion ......................... Interface BaseException implementations
  }
}
