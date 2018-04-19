using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shanghai.WebApp.UserControls
{
    public class AddMemoEventArgs : EventArgs
    {
        public string Memo { get; set; }

        public AddMemoEventArgs(string memo)
        {
            Memo = memo;
        }
    }

    public class AlterComboEventArgs : EventArgs
    {
        public AlterComboEventArgs()
        {

        }
    }

    public class ShiftTradeActionEventArgs : EventArgs
    {
        public ShiftTradeActionEventArgs()
        {

        }
    }

    public class AddDiscountEventArgs : EventArgs
    {
        public decimal? DiscountDollar { get; set; }
        public decimal? DiscountPercent { get; set; }
        public bool DiscountRemove { get; set; }

        public AddDiscountEventArgs(decimal discountDollar)
        {
            DiscountDollar = discountDollar;
            DiscountPercent = null;
            DiscountRemove = false;
        }

        public AddDiscountEventArgs(bool isDollar, decimal discount)
        {
            DiscountRemove = false;
            if (isDollar)
            {
                DiscountDollar = discount;
                DiscountPercent = null;
            }
            else
            {
                DiscountDollar = null;
                DiscountPercent = discount;
            }
        }

        public AddDiscountEventArgs(bool remove)
        {
            DiscountRemove = remove;
        }
    }

    public class RemoveItemEventArgs : EventArgs
    {
    }

    public class DateChangedEventArgs : EventArgs
    {
        public DateTime WeekEnding { get; set; }
        public DateTime WeekStarting { get; set; }
        public DateChangedEventArgs(DateTime dateEnd, DateTime dateStart)
        {
            WeekEnding = dateEnd;
            WeekStarting = dateStart;
        }
    }

    public class BillSplitEventArgs : EventArgs
    {
        public bool wasSuccess { get; set; }

        public BillSplitEventArgs(bool result)
        {
            wasSuccess = result;
        }
    }

    public class ItemSelectedEventArgs : EventArgs
    {
        public List<int> SelectedMenuIDs { get; set; }
        public ItemSelectedEventArgs(List<int> ids)
        {
            SelectedMenuIDs = ids;
        }
    }

    public class ItemsUpdatedEventArgs : EventArgs
    {
        public ItemsUpdatedEventArgs()
        {

        }
    }

    public class OptionUpdateComboEventArgs : EventArgs
    {
        public OptionUpdateComboEventArgs()
        {

        }

    }
    
}