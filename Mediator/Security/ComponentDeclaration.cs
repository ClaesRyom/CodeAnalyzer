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


  [Flags]
  internal enum ComponentDeclaration : short
  {
    NotDefined      = 0x0000,
    ConsoleApp      = 0x0001,
    Configuration   = 0x0002,
    Coordination    = 0x0004,
    Engine          = 0x0008,
    Mediator        = 0x0010,
    Output          = 0x0020,
    Statistics      = 0x0040,
    Version         = 0x0080,
    Matches         = 0x0100,
    DataAccess      = 0x0200,
    ExecutionId     = 0x0400,
  }
}