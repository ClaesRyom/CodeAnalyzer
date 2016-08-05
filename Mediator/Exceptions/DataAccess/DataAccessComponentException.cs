// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Exceptions.DataAccess
{
  using System;
  using System.Runtime.Serialization;

  using Fbb.Exceptions.Base;


  [Serializable()]
  public class DataAccessComponentException : BaseException
  {
    #region Construction .......................................................
    public DataAccessComponentException() { }

    public DataAccessComponentException(string msg) : base(msg) { }

    public DataAccessComponentException(string msg, Exception innerException) : base(msg, innerException) { }

    public DataAccessComponentException(object src, int code, string msg) : base(src, code, msg) { }

    public DataAccessComponentException(object src, int code, string msg, Exception innerException) : base(src, code, msg, innerException) { }

		public DataAccessComponentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    #endregion .................................................... Construction


    #region Interface BaseException implementations ............................
    public override string ExceptionTypeName
    {
      get { return GetType().FullName; }
    }
    #endregion ......................... Interface BaseException implementations  }
  }
}