// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Configuration.Definitions
{
  public interface IPathDefinition
  {
    int    Id      { get; set; }
    bool   Enabled { get; set; }
    string Path    { get; set; }
  }
}