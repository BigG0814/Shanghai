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
    [Table("PaymentType")]
    public class PaymentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PaymentType()
        {
            Bills = new HashSet<BillPayment>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentTypeID { get; set; }

        [Required]
        [StringLength(300)]
        public string PaymentDescription { get; set; }

        public virtual ICollection<BillPayment> Bills { get; set; }

    }
}
