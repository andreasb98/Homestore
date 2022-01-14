using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Butikk.Models;
using Butikk.Services;

namespace Butikk.ViewModels
{
    public class SearchResultsViewModel : BaseViewModel
    {
        public ObservableCollection<FurnitureItem> ItemsByQuery { get; set; }
        private int _TotalItems;

        public int TotalItems
        {
            get { return _TotalItems; }
            set 
            {
                _TotalItems = value;
                OnPropertyChanged();
            }
        }


        public SearchResultsViewModel(string searchTerm)
        {
            ItemsByQuery = new ObservableCollection<FurnitureItem>();
            GetItemsByQueryAsync(searchTerm);
        }

        private async void GetItemsByQueryAsync(string searchTerm)
        {
            var data = await new FurnitureItemService().GetItemsByQueryAsync(searchTerm);
            ItemsByQuery.Clear();
            
            foreach (var item in data)
            {
                ItemsByQuery.Add(item);
            }
            TotalItems = ItemsByQuery.Count;
        }
    }
}
