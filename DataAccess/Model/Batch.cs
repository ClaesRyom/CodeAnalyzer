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
    
    public partial class Batch
    {
        public Batch()
        {
            this.Match = new HashSet<Match>();
        }
    
        public System.Guid Id { get; set; }
        public System.DateTime TimeStamp { get; set; }
    
        public virtual ICollection<Match> Match { get; set; }
    }
}