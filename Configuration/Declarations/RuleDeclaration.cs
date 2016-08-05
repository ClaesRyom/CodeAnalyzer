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
  using System.Collections.Generic;
  using System.Linq;

  using Mediator.Configuration;
  using Mediator.Configuration.Declarations;


  internal sealed class RuleDeclaration : IRuleDeclaration
  {
    #region Auto properties ....................................................
    public int                                   Id                { get; set; }
    public string                                Name              { get; set; }
    public string                                Description       { get; set; }
    public RuleSeverity                          Severity          { get; set; }
    public string                                Expression        { get; set; }
    public Dictionary<int, ILanguageDeclaration> Languages         { get; set; }
    public ICategoryDeclaration                  ParentDeclaration { get; set; }
    #endregion ................................................. Auto properties


    #region Construction .......................................................
    public RuleDeclaration()
    {
      Severity  = RuleSeverity.Info;
      Languages = new Dictionary<int, ILanguageDeclaration>();
    }
    #endregion .................................................... Construction


    #region Interface ICloneable impl. .........................................
    public object Clone()
    {
      RuleDeclaration rd = new RuleDeclaration();

      rd.Id                = Id;
      rd.ParentDeclaration = ParentDeclaration;
      rd.Name              = (Name != null) ? Name.Clone() as string : null;
      rd.Description       = (Description != null) ? Description.Clone() as string : null;
      rd.Severity          = Severity;
      rd.Expression        = (Expression != null) ? Expression.Clone() as string : null;
      rd.Languages         = Languages.ToDictionary(pair => pair.Key, pair => pair.Value.Clone() as ILanguageDeclaration);

      return rd;
    }
    #endregion ...................................... Interface ICloneable impl.
  }
}
