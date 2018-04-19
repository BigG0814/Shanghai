using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shanghai.WebApp.UserControls
{
    public delegate void OnMemoAdded(object s, AddMemoEventArgs e);

    public delegate void OnRemove(object s, RemoveItemEventArgs e);

    public delegate void OnDiscountAdded(object s, AddDiscountEventArgs e);

    public delegate void OnUpdateItemSelected(object s, OptionUpdateComboEventArgs e);

    public partial class MenuItemInfoDropDown : UserControl
    {
        public string memo { get { return MemoTextBox.Text; } set
            {
                MemoTextBox.Text = value;
            }
        }

        public bool isCombo
        {
            get
            {
                return editcomboLI.Visible;
            }
            set
            {
                editcomboLI.Visible = value;
            }
        }
        public decimal discount { get; set; }

        public event OnUpdateItemSelected UpdateItems;

        public event OnMemoAdded MemoAdded;

        public event OnRemove Remove;

        public event OnDiscountAdded DiscountAdded;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void addMemoBtn_Click(object sender, EventArgs e)
        {
            memo = MemoTextBox.Text;
            MemoAdded(this, new AddMemoEventArgs(MemoTextBox.Text));
        }

        protected void addDiscountBtn_Click(object sender, EventArgs e)
        {
            decimal Discount;
            if(decimal.TryParse(DiscountBox.Text, out Discount))
            {
                discount = Discount;
                DiscountAdded(this, new AddDiscountEventArgs(false, discount));
            }
            else
            {
                throw new Exception("Discount must be a valid number");
            }
            
        }

        protected void RemoveItemClick(object sender, EventArgs e)
        {
            Remove(this, new RemoveItemEventArgs());
        }

        protected void addDiscountBtnDollar_Click(object sender, EventArgs e)
        {
            decimal Discount;
            if (decimal.TryParse(DiscountBox.Text, out Discount))
            {
                discount = Discount;
                DiscountAdded(this, new AddDiscountEventArgs(discount));
            }
            else
            {
                throw new Exception("Discount must be a valid number");
            }
        }

        protected void ClearDiscountBtn_Click(object sender, EventArgs e)
        {
            DiscountAdded(this, new AddDiscountEventArgs(true));
        }

        protected void editItems_Click(object sender, EventArgs e)
        {
            UpdateItems(this, new OptionUpdateComboEventArgs());
        }
    }
}