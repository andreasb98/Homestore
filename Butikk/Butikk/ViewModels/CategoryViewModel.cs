using Butikk.Models;
using Butikk.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Butikk.ViewModels
{
    public class CategoryViewModel: BaseViewModel
    {
        private Category _SelectedCategory;

        public Category SelectedCategory
        {
            get { return _SelectedCategory; }
            set 
            { 
                _SelectedCategory = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<FurnitureItem> ItemsByCategory { get; set; }

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

        public CategoryViewModel(Category category)
        {
            SelectedCategory = category;
            ItemsByCategory = new ObservableCollection<FurnitureItem>();
            GetFurnitureItems(category.CategoryID);
        }

        private async void GetFurnitureItems(int categoryID)
        {
            var data = await new FurnitureItemService().GetItemsByCategoryAsync(categoryID);
            ItemsByCategory.Clear();
            foreach (var item in data)
            {
                ItemsByCategory.Add(item);
            }
            TotalItems = ItemsByCategory.Count;
        }
    }
}
