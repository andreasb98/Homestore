using Butikk.Models;
using Butikk.Services;
using Butikk.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Butikk.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        public ObservableCollection<UserCartItem> CartItems { get; set; }
        private decimal _TotalCost;
        public decimal TotalCost
        {
            get { return _TotalCost; }
            set
            {
                _TotalCost = value;
                OnPropertyChanged();
            }
        }

        public Command PlaceOrdersCommand { get; set; }

        public CartViewModel()
        {
            CartItems = new ObservableCollection<UserCartItem>();
            LoadItems();
            PlaceOrdersCommand = new Command(async () => await PlaceOrdersAsync());
        }

        private void LoadItems()
        {
            var connection = DependencyService.Get<ISQLite>().GetConnection();
            var items = connection.Table<CartItem>().ToList();
            CartItems.Clear();

            foreach (var item in items)
            {
                CartItems.Add(new UserCartItem()
                {
                    CartItemId = item.CartItemID,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Cost = item.Price * item.Quantity
                });

                TotalCost += item.Price * item.Quantity;
            }
        }

        private async Task PlaceOrdersAsync()
        {
            var id = await new OrderService().PlaceOrderAsync() as string;
            RemoveItemsFromCart();
            await Application.Current.MainPage.Navigation.PushModalAsync(new OrdersView(id));
        }

        private void RemoveItemsFromCart()
        {
            var cartItemService = new CartItemService();
            cartItemService.RemoveItemsFromCart();
        }
    }
}
