// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Exceptions.Output
{
  using System;
  using System.Runtime.Serialization;
  using Fbb.Exceptions.Base;


  [Serializable()]
  public class OutputComponentException : BaseException
  {
    #region Construction .......................................................
    public OutputComponentException() { }

    public OutputComponentException(string msg) : base(msg) { }

    public OutputComponentException(string msg, Exception innerException) : base(msg, innerException) { }

    public OutputComponentException(object src, int code, string msg) : base(src, code, msg) { }

    public OutputComponentException(object src, int code, string msg, Exception innerException) : base(src, code, msg, innerException) { }

    public OutputComponentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    #endregion .................................................... Construction


    #region Interface BaseException implementations ............................
    public override string ExceptionTypeName
    {
      get { return GetType().FullName; }
    }
    #endregion ......................... Interface BaseException implementations
  }
}