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


  public class AbstractKey : IKey
  {
    #region Instance Variable(s) ...............................................
    private byte[] _Key = null;
    #endregion ............................................ Instance Variable(s)


    #region Interface IKey property impl. ......................................
    public byte[] Key
    {
      get { return _Key; }
      private set
      {
        if (value.Length <= 0)
          throw new ArgumentException("Invalid key.");
        _Key = value;
      }
    }
    #endregion ................................... Interface IKey property impl.


    #region Construction .......................................................
    protected AbstractKey()
    {
      Key = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
    }

    protected AbstractKey(byte[] key)
    {
      Key = key;
    }
    #endregion .................................................... Construction


    #region Output stuff .......................................................
    public override string ToString()
    {
      return Encoding.UTF8.GetString(Key);
    }
    #endregion .................................................... Output stuff
  }
}