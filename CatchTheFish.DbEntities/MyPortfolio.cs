//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CatchTheFish.DbEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class MyPortfolio
    {
        public long Id { get; set; }
        public Nullable<System.Guid> ProfileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
    }
}
