namespace Shanghai.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    [Table("ComboItemSelections")]
    public partial class ComboItemSelection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComboItemSelectionID { get; set; }

        public int? BillItemID { get; set; }

        public int MenuItemId { get; set; }

        public int? OrderItemID { get; set; }

        public int? ComboMenuItemID { get; set; }

        public int? ShoppingCartItemID { get; set; }


    }
}
