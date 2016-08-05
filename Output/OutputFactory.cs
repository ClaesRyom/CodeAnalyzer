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
  using System;

  using Mediator.Output;


	internal class OutputFactory
  {
    public static IOutput Create(ReportOutputType type)
    {
      switch (type)
      {
        case ReportOutputType.Web:   { return new WebOutput(); }
        case ReportOutputType.Excel: { return new ExcelOutput(); }

        default: 
          throw new ArgumentException("Invalid report output type specified in the app.config file.");
      }
    }
  }
}