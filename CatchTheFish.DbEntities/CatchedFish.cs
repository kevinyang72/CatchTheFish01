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
    
    public partial class CatchedFish
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
        public Nullable<int> Volumn { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> PriceChangePercentage { get; set; }
        public System.DateTime WhenCreated { get; set; }
    }
}
