// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Label
{
	using System;


	public class Batch : IBatch
	{
		#region Auto properties ....................................................
		public Guid     Id        { get; private set; }
		public DateTime TimeStamp { get; private set; }
		#endregion ................................................. Auto properties


		#region Construction .......................................................
		public Batch() : this(DateTime.Now) { }

		public Batch(DateTime timeStamp)
		{
			Id        = Guid.NewGuid();
			TimeStamp = timeStamp;
		}
		#endregion .................................................... Construction
	}
}