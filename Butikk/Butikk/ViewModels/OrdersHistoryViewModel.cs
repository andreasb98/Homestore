using Butikk.Models;
using Butikk.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Butikk.ViewModels
{
    public class OrdersHistoryViewModel : BaseViewModel
    {
        public ObservableCollection<OrdersHistory> OrderDetails { get; set; }
        private bool _IsBusy;

        public bool IsBusy
        {
            get { return _IsBusy; }
            set 
            { 
                _IsBusy = value;
                OnPropertyChanged();
            }
        }

        public OrdersHistoryViewModel()
        {
            OrderDetails = new ObservableCollection<OrdersHistory>();
            LoadUserOrders();
        }

        private async void LoadUserOrders()
        {
            try
            {
                IsBusy = true;
                OrderDetails.Clear();
                var service = new OrderHistoryService();
                var details = await service.GetOrderDetailsAsync();

                foreach (var order in details)
                {
                    OrderDetails.Add(order);
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
