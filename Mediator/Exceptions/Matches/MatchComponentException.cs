// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Exceptions.Matches
{
	using System;
	using System.Runtime.Serialization;

	using Fbb.Exceptions.Base;


	[Serializable()]
	public class MatchComponentException : BaseException
	{
		#region Construction .......................................................
		public MatchComponentException() { }

		public MatchComponentException(string msg) : base(msg) { }

		public MatchComponentException(string msg, Exception innerException) : base(msg, innerException) { }

		public MatchComponentException(object src, int code, string msg) : base(src, code, msg) { }

		public MatchComponentException(object src, int code, string msg, Exception innerException) : base(src, code, msg, innerException) { }

		public MatchComponentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
		#endregion .................................................... Construction


		#region Interface BaseException implementations ............................
		public override string ExceptionTypeName
		{
			get { return GetType().FullName; }
		}
		#endregion ......................... Interface BaseException implementations  }
	}
}