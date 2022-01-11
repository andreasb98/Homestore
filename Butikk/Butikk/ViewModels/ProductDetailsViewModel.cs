using Butikk.Models;
using Butikk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Butikk.ViewModels
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        private FurnitureItem _SelectedFurnitureItem;

        public FurnitureItem SelectedFurnitureItem
        {
            get { return _SelectedFurnitureItem; }
            set 
            { 
                _SelectedFurnitureItem = value;
                OnPropertyChanged();
            }
        }

        private int _TotalQuantity;

        public int TotalQuantity
        {
            get { return _TotalQuantity; }
            set 
            { 
                this._TotalQuantity = value;
                if (this._TotalQuantity < 0)
                    this._TotalQuantity = 0;
                if (this._TotalQuantity > 10)
                    this._TotalQuantity -= 1;
                OnPropertyChanged();
            }
        }

        public Command IncrementOrderCommand { get; set; }
        public Command DecrementOrderCommand { get; set; }
        public Command AddToCartCommand { get; set; }
        public Command ViewCartCommand { get; set; }
        public Command HomeCommand { get; set; }

        public ProductDetailsViewModel(FurnitureItem furnitureItem)
        {
            SelectedFurnitureItem = furnitureItem;
            TotalQuantity = 0;

            IncrementOrderCommand = new Command(() => IncrementOrder());
            DecrementOrderCommand = new Command(() => DecrementOrder());
            AddToCartCommand = new Command(() => AddToCart());
            ViewCartCommand = new Command(async () => await ViewCartAsync());
            HomeCommand = new Command(async () => await GoToHomeAsync());
        }

        private async Task GoToHomeAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsView());
        }

        private async Task ViewCartAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CartView());
        }

        private void AddToCart()
        {
            var connection = DependencyService.Get<ISQLite>().GetConnection();
            try
            {
                CartItem ci = new CartItem()
                {
                    ProductId = SelectedFurnitureItem.ProductID,
                    ProductName = SelectedFurnitureItem.Name,
                    Price = SelectedFurnitureItem.Price,
                    Quantity = TotalQuantity
                };
                var item = connection.Table<CartItem>().ToList()
                    .FirstOrDefault(c => c.ProductId == SelectedFurnitureItem.ProductID);
                if (item == null)
                    connection.Insert(ci);
                else
                {
                    item.Quantity += TotalQuantity;
                    connection.Update(item);
                }
                connection.Commit();
                connection.Close();
                Application.Current.MainPage.DisplayAlert("Cart", "Selected Item added to Cart", "OK");
            }
            catch (Exception ex)
            {

                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                connection.Close();
            }
        }

        private void DecrementOrder()
        {
            TotalQuantity--;
        }

        private void IncrementOrder()
        {
            TotalQuantity++;
        }
    }
}
