using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shanghai.Data.Entities;
using Shanghai.System.BLL;

namespace Shanghai.WebApp.UserControls
{
    public delegate void OnItemsSelected(object s, ItemSelectedEventArgs e);

    public delegate void OnItemsUpdated(object s, ItemsUpdatedEventArgs e);

    public partial class ComboItemSelector : UserControl
    {
        public event OnItemsSelected SelectionSubmitted;

        public event OnItemsUpdated ItemsUpdated;

        public int AllowedSelections
        {
            get
            {
                if (ViewState["AllowedSelections"] == null)
                    return 3;
                return (int)ViewState["AllowedSelections"];
            }
            set
            {
                ViewState["AllowedSelections"] = value;
            }
        }

        public List<Data.Entities.MenuItem> SelectedItems
        {
            get
            {
                if (ViewState["SelectedInts"] == null)
                    return new List<Data.Entities.MenuItem>();
                return ViewState["SelectedInts"] as List<Data.Entities.MenuItem>;

            }
            set
            {
                ViewState["SelectedInts"] = value;
            }
        }

        public List<ComboItemSelection> OriginalSelections
        {
            get
            {
                return ViewState["OriginalSelections"] as List<ComboItemSelection>;
            }
            set
            {
                ViewState["OriginalSelections"] = value;
            }
        }

        public int ParentItemID
        {
            get
            {
                return (int)ViewState["ParentItemID"];
            }
            set
            {
                ViewState["ParentItemID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Show(int itemID)
        {
            ParentItemID = itemID;
            Update.Visible = false;
            Select.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "pop", "showComboItemPopUp();", true);
        }

        protected void Select_Click(object sender, EventArgs e)
        {
            if (SelectedItems.Count != AllowedSelections)
            {
                ErrorTextBox.Text = "You must select " + AllowedSelections.ToString() + " items...";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "pop", "showComboItemPopUp();", true);
            }
            else
            {
                
                SelectionSubmitted(sender, new ItemSelectedEventArgs(SelectedItems.Select(x => x.MenuItemID).ToList()));
                ErrorTextBox.Text = "";
               

            }
        }

        public void EditComboItem(List<ComboItemSelection> selections)
        {
            OriginalSelections = selections;
            MenuItemController sysmgr = new MenuItemController();
            SelectedItems = sysmgr.getItemByComboSelection(selections);
            SelectedItemsRepeater.DataBind();
            AllowedSelections = selections.Count();
            Select.Visible = false;
            Update.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "pop", "showComboItemPopUp();", true);
        }

        protected void SelectItem_Click(object sender, EventArgs e)
        {
            int selectedInt = int.Parse(((Button)sender).CommandArgument.Split(';')[0]);
            string MenuItemName = ((Button)sender).CommandArgument.Split(';')[1];
            Data.Entities.MenuItem item = new Data.Entities.MenuItem() { MenuItemID = selectedInt, MenuItemName = MenuItemName };
            if (SelectedItems.Count != AllowedSelections)
            {
                List<Data.Entities.MenuItem> list = SelectedItems;
                list.Add(item);
                SelectedItems = list;
            }
            else
            {
                ErrorTextBox.Text = "You may only select " + AllowedSelections.ToString() + " items...";
            }
            SelectedItemsRepeater.DataBind();
        }

        protected void RemoveItem_Click(object sender, EventArgs e)
        {
            int selectedInt = int.Parse(((Button)sender).CommandArgument.Split(';')[0]);
            string MenuItemName = ((Button)sender).CommandArgument.Split(';')[1];
            List<Data.Entities.MenuItem> list = SelectedItems;
            list.Remove(list.Where(x => x.MenuItemID == selectedInt).FirstOrDefault());
            SelectedItems = list;
            SelectedItemsRepeater.DataBind();
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            MenuItemController sysmgr = new MenuItemController();
            sysmgr.UpdateComboItems(OriginalSelections, SelectedItems);
            ItemsUpdated(sender, new ItemsUpdatedEventArgs());
        }
    }
}