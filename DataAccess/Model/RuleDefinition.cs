//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeAnalyzer.DataAccess.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class RuleDefinition
    {
        public RuleDefinition()
        {
            this.Match = new HashSet<Match>();
        }
    
        public int Id { get; set; }
        public bool Enabled { get; set; }
    
        public virtual CategoryDefinition CategoryDefinition { get; set; }
        public virtual RuleDeclaration RuleDeclaration { get; set; }
        public virtual ICollection<Match> Match { get; set; }
    }
}
