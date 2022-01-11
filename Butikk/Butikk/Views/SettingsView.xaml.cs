using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Butikk.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Butikk.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        async void ButtonProducts_Clicked(object sender, EventArgs e)
        {
            var afd = new AddFurnitureItemData();
            await afd.AddFurnitureItemsAsync();
        }

        async void ButtonCategories_Clicked(object sender, EventArgs e)
        {
            
            var acd = new AddCategoryData();
            await acd.AddCategoriesAsync();
        }

        private void ButtonCart_Clicked(object sender, EventArgs e)
        {
            var cct = new CreateCartTable();
            if (cct.CreateTable())
                DisplayAlert("Success", "Cart Table Created", "OK");
            else
                DisplayAlert("Error", "Error while creating table", "OK");
        }
    }
}