// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Configuration.Declarations
{
  using Mediator.Configuration.Declarations;


  internal sealed class LanguageDeclaration : ILanguageDeclaration
  {
    #region Auto properties ....................................................
    public int    Id        { get; set; }
    public string Name      { get; set; }
    public string Extension { get; set; }
    #endregion ................................................. Auto properties


    #region Interface ICloneable impl. .........................................
    public object Clone()
    {
      LanguageDeclaration ld = new LanguageDeclaration();

      ld.Id        = Id;
      ld.Name      = (Name != null) ? Name.Clone() as string : null;
      ld.Extension = (Extension != null) ? Extension.Clone() as string : null;

      return ld;
    }
    #endregion ...................................... Interface ICloneable impl.
  }
}