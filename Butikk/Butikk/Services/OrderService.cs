using Butikk.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Butikk.Services
{
    //Class for adding orders (cart items) to the firebase database
    public class OrderService
    {
        FirebaseClient client;

        public OrderService()
        {
            client = new FirebaseClient("https://homestore-1713e-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public async Task<string> PlaceOrderAsync()
        {
            var connection = DependencyService.Get<ISQLite>().GetConnection();
            var data = connection.Table<CartItem>().ToList();

            //Generate unique ID
            var orderId = Guid.NewGuid().ToString();
            var uname = Preferences.Get("UserName", "Guest");

            decimal totalCost = 0;

            foreach (var item in data)
            {
                OrderDetails orderDetails = new OrderDetails()
                {
                    OrderId = orderId,
                    OrderDetailId = Guid.NewGuid().ToString(),
                    ProductID = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                };
                totalCost += item.Price * item.Quantity;

                await client.Child("OrderDetails").PostAsync(orderDetails);
            }
            await client.Child("Orders").PostAsync(
                new Order()
                {
                    OrderId=orderId,
                    Username = uname,
                    TotalCost = totalCost
                });
            return orderId;
        }
    }
}
