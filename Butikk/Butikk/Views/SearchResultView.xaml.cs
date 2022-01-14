using Butikk.Models;
using Butikk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Butikk.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultView : ContentPage
    {
        SearchResultsViewModel srvm;
        public SearchResultView(string searchTerm)
        {
            InitializeComponent();
            srvm = new SearchResultsViewModel(searchTerm);
            this.BindingContext = srvm;

            LabelName.Text = $"Welcome {Preferences.Get("Username", "Guest")}";
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProduct = e.CurrentSelection.FirstOrDefault() as FurnitureItem;
            if (selectedProduct == null)
                return;

            await Navigation.PushModalAsync(new ProductDetailsView(selectedProduct));
            ((CollectionView)sender).SelectedItem = null;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}