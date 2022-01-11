using Butikk.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Butikk.Services
{
    public class CartItemService
    {
        public int GetUserCartCount()
        {
            var connection = DependencyService.Get<ISQLite>().GetConnection();
            var count = connection.Table<CartItem>().Count();
            connection.Close();
            return count;
        }

        public void RemoveItemsFromCart()
        {
            var connection = DependencyService.Get<ISQLite>().GetConnection();
            connection.DeleteAll<CartItem>();
            connection.Commit();
            connection.Close();
        }
    }
}
