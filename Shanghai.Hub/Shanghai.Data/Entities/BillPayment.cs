using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.Data.Entities
{
    [Serializable]
    [Table("BillPayment")]
    public class BillPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillPaymentID { get; set; }

        public int BillID { get; set; }

        [ForeignKey("BillID")]
        public virtual Bill Bill { get; set; }

        public int PaymentTypeID { get; set; }
        public virtual PaymentType PaymentType { get; set; }

        public decimal PaymentAmount { get; set; }

        public bool isVoid { get; set; }
    }
}
