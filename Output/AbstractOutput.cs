// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Output
{
	internal abstract class AbstractOutput : IOutput
  {
    #region Construction .......................................................
    protected AbstractOutput()
    {
    }
    #endregion .................................................... Construction


    #region Interface IOutput impl. ............................................
    public abstract void GenerateOutput(string outputRootDir);
    #endregion ......................................... Interface IOutput impl.
  }
}