namespace Shanghai.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    [Table("ShiftTrades")]
    [Serializable]
    public partial class ShiftTrade
    {
        [Key]
        public int TradeID { get; set; }

        public int ShiftID { get; set; }

        public int OriginalEmployee { get; set; }

        public int? NewEmployee { get; set; }

        public int? ApprovingEmployee { get; set; }

        public bool IsApproved { get; set; }

        public DateTime TimePosted { get; set; }

        public string Reason { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual Employee Employee2 { get; set; }

        public virtual Shift Shift { get; set; }
    }
}
