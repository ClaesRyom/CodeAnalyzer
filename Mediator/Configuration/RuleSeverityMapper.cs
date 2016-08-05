// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Configuration
{
  using System;

  using Identifiers;


  public class RuleSeverityMapper
  {
    #region Map a int to a RuleSeverity ........................................
    public static RuleSeverity Int2RuleSeverity(int severity)
    {
      if (severity == 1) return RuleSeverity.Fatal;
      if (severity == 2) return RuleSeverity.Critical;
      if (severity == 3) return RuleSeverity.Warning;
      if (severity == 4) return RuleSeverity.Info;
      return RuleSeverity.NotDefined;
    }
    #endregion ..................................... Map a int to a RuleSeverity


    #region Map a string to a RuleSeverity .....................................
    public static RuleSeverity String2RuleSeverity(string str)
    {
      if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException("str");

      if (string.Compare(str, "INFO",     StringComparison.OrdinalIgnoreCase) == 0) return RuleSeverity.Info;
      if (string.Compare(str, "WARNING",  StringComparison.OrdinalIgnoreCase) == 0) return RuleSeverity.Warning;
      if (string.Compare(str, "CRITICAL", StringComparison.OrdinalIgnoreCase) == 0) return RuleSeverity.Critical;
      if (string.Compare(str, "FATAL",    StringComparison.OrdinalIgnoreCase) == 0) return RuleSeverity.Fatal;
      return RuleSeverity.NotDefined;
    }
    #endregion .................................. Map a string to a RuleSeverity


    #region Map expression severity to StatManager Counter ID's ................
    public static CounterIds Severity2CounterId(RuleSeverity severity)
    {
      switch (severity)
      {
        case RuleSeverity.Info:     return CounterIds.InfoMatches;
        case RuleSeverity.Warning:  return CounterIds.WarningMatches;
        case RuleSeverity.Critical: return CounterIds.CriticalMatches;
        case RuleSeverity.Fatal:    return CounterIds.FatalMatches;
        default:
          throw new ArgumentException("severity");
      }
    }
    #endregion ............. Map expression severity to StatManager Counter ID's
  }
}