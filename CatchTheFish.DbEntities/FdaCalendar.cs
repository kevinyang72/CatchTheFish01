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
    
    public partial class FdaCalendar
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> CatalystDate { get; set; }
        public string CatelystNotes { get; set; }
        public Nullable<System.DateTime> LastMoidfiedDate { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string MarketCapital { get; set; }
    }
}
