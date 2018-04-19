using Shanghai.Data.Entities;
using Shanghai.Data.POCOs;
using Shanghai.System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.System.BLL
{
    [DataObject]
    public class CartController
    {
        // KW - List, Update, Delete Methods for ViewCart
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ShoppingCartItem> ListCartItems(int cartID)
        {
            using (var context = new ShanghaiContext())
            {
                return context.ShoppingCartItem.Where(x => x.ShoppingCartID == cartID).Include(x => x.MenuItem).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateCartItem(ShoppingCartItem cartItem)
        {
            using (var context = new ShanghaiContext())
            {
                var existing = context.Entry(cartItem);
                existing.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void DeleteCartItem(ShoppingCartItem cartItem)
        {
            using (var context = new ShanghaiContext())
            {
                var existing = context.ShoppingCartItem.Find(cartItem.ShoppingCartItemID);
                context.ShoppingCartItem.Remove(existing);
                context.SaveChanges();
            }
        }

        // KW - Method to open a new cart when a user opens page
        public int StartNewCart()
        {
            using (var context = new ShanghaiContext())
            {
                ShoppingCart userCart = new ShoppingCart();
                userCart.CreatedOn = DateTime.Now;
                context.ShoppingCart.Add(userCart);
                context.SaveChanges();
                return userCart.ShoppingCartID;
            }
        }

        // KW - Method to delete open carts
        public void DeleteOpenCart(int cartID)
        {
            using (var context = new ShanghaiContext())
            {
                var itemsToDelete = from items in context.ShoppingCartItem
                                    where items.ShoppingCartID == cartID
                                    select items;
                foreach (var item in itemsToDelete)
                {
                    context.ShoppingCartItem.Remove(item);
                }

                var cartToDelete = (from cart in context.ShoppingCart
                                    where cart.ShoppingCartID == cartID
                                    select cart).Single();
                context.ShoppingCart.Remove(cartToDelete);
                context.SaveChanges();
            }
        }

        //KW - Method to delete shopping carts if they were made the day before
        public void ShoppingCartCleanup()
        {
            using (var context = new ShanghaiContext())
            {
                List<int> cartsToDelete = (from carts in context.ShoppingCart
                                           where carts.CreatedOn.Day < DateTime.Now.Day || carts.CreatedOn.Month < DateTime.Now.Month
                                           select carts.ShoppingCartID).ToList();
                foreach (var cartId in cartsToDelete)
                {
                    var itemsToDelete = from items in context.ShoppingCartItem
                                        where items.ShoppingCartID == cartId
                                        select items;
                    foreach (var item in itemsToDelete)
                    {
                        context.ShoppingCartItem.Remove(item);
                    }

                    var cartToDelete = (from cart in context.ShoppingCart
                                        where cart.ShoppingCartID == cartId
                                        select cart).Single();
                    context.ShoppingCart.Remove(cartToDelete);
                    if (cartsToDelete.Count > 0)
                    {
                        context.SaveChanges();
                    }
                }
            }
        }

        //KW - Method to add items selected from the menu into the cart
        public int AddItemToCart(int cartId, int itemId)
        {
            using (var context = new ShanghaiContext())
            {
                ShoppingCartItem cartItem = new ShoppingCartItem();
                ShoppingCart userCart = context.ShoppingCart.Where(x => x.ShoppingCartID == cartId).FirstOrDefault();

                if (userCart != null)
                {
                    CartItem item = (from i in context.MenuItems
                                     where i.MenuItemID == itemId
                                     select new CartItem
                                     {
                                         MenuItemID = i.MenuItemID,
                                         MenuItemName = i.MenuItemName,
                                         Quantity = 1,
                                         Price = i.CurrentPrice
                                     }).SingleOrDefault();

                    // checks if the item is in the cart already
                    cartItem = userCart.ShoppingCartItems.Where(x => x.MenuItemID == item.MenuItemID).SingleOrDefault();
                    if (cartItem == null)
                    {
                        cartItem = new ShoppingCartItem
                        {
                            MenuItemID = item.MenuItemID,
                            Quantity = item.Quantity,
                            ShoppingCartID = cartId

                        };
                        //if not it adds it to the cart
                        userCart.ShoppingCartItems.Add(cartItem);


                    }
                    else
                    {
                        // if it exists in the cart already it adds one more to the quantity (since each click would be adding the item once)
                        userCart.ShoppingCartItems.Single(x => x.MenuItemID == item.MenuItemID).Quantity = (userCart.ShoppingCartItems.Single(x => x.MenuItemID == item.MenuItemID).Quantity + 1);
                    }
                    userCart.UpdatedOn = DateTime.Now;
                    var entry = context.Entry(userCart);
                    entry.State = EntityState.Modified;
                }
                else
                {
                    // creates a new cart...should never reach this code
                    userCart = new ShoppingCart();

                    CartItem item = (from i in context.MenuItems
                                     where i.MenuItemID == itemId
                                     select new CartItem
                                     {
                                         MenuItemID = i.MenuItemID,
                                         MenuItemName = i.MenuItemName,
                                         Quantity = 1,
                                         Price = i.CurrentPrice
                                     }).SingleOrDefault();

                    userCart.ShoppingCartItems.Add(new ShoppingCartItem
                    {
                        MenuItemID = item.MenuItemID,
                        Quantity = item.Quantity,
                        ShoppingCartID = cartId

                    });
                    userCart.CreatedOn = DateTime.Now;
                    context.ShoppingCart.Add(userCart);
                }

                //return the item you just added!
                context.SaveChanges();
                return context.ShoppingCartItem.Where(x => x.ShoppingCartID == cartId).Where(x => x.MenuItemID == itemId).FirstOrDefault().ShoppingCartItemID;
            }
            

        }


        //KW - method to add combo selections to DB (in the shopping cart table..
        public int addComboSelections(List<ComboItemSelection> comboSelections, int cartID)
        {
            using (var context = new ShanghaiContext())
            {
                ShoppingCartItem item = new ShoppingCartItem();
                item.Quantity = 1;
                item.MenuItemID = comboSelections[0].ComboMenuItemID.Value;
                item.ShoppingCartID = cartID;
                context.ShoppingCartItem.Add(item);
                context.SaveChanges();
                try
                {
                    context.ComboItemSelections.AddRange(comboSelections);
                    context.SaveChanges();
                }
                catch
                {
                    var existing = context.ShoppingCartItem.Find(item.ShoppingCartItemID);
                    context.ShoppingCartItem.Remove(existing);
                }

                return item.ShoppingCartItemID;
                
            }

        }

        //KW - Method to update the combo selection items with ShoppingCartItemID
        public void updateComboSelection(List<ComboItemSelection> updateItems, int cartItemId)
        {
            using (var context = new ShanghaiContext())
            {
                foreach(var item in updateItems)
                {
                    var result = context.ComboItemSelections.Where(x => x.ComboItemSelectionID == item.ComboItemSelectionID).FirstOrDefault();
                    if(cartItemId != 0)
                        result.ShoppingCartItemID = cartItemId;
                    context.ComboItemSelections.Add(result);
                    var existing = context.Entry(result);
                    existing.State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        //public void updateComboSelection(List<int> ids, int orderId)
        //{
        //    using (var context = new ShanghaiContext())
        //    {
        //        foreach (var item in ids)
        //        {
        //            var result = context.ComboItemSelections.Where(x => x.ShoppingCartItemID == item).ToList();
        //            foreach(var selection in result)
        //            {
        //                selection.OrderItemID = orderId;
        //                context.ComboItemSelections.Add(selection);
        //                var existing = context.Entry(selection);
        //                existing.State = EntityState.Modified;
        //                context.SaveChanges();
        //            }
        //        }
        //    }
        //}

        // KW - Method to get the count of items in the cart, and the total price - to display in header
        public Tuple<int, decimal> getCountAndPrice(int shoppingCartId)
        {
            int itemCount = 0;
            decimal priceTotal = 0.00M;

            using (var context = new ShanghaiContext())
            {
                List<ShoppingCartItem> userCart = context.ShoppingCartItem.Where(x => x.ShoppingCartID == shoppingCartId).ToList();

                foreach (var cartItem in userCart)
                {
                    itemCount = itemCount + cartItem.Quantity;
                    priceTotal = priceTotal + (cartItem.Quantity * cartItem.MenuItem.CurrentPrice);
                }
            }


            return Tuple.Create(itemCount, priceTotal);
        }
    }
}
