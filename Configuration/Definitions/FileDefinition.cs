// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Configuration.Definitions
{
  using Mediator.Configuration.Definitions;


  internal sealed class FileDefinition : IFileDefinition 
  {
    public int    Id       { get; set; }
    public bool   Enabled  { get; set; }
    public string Path     { get; set; }
  }
}