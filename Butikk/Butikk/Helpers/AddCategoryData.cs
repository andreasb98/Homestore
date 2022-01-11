using Butikk.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Butikk.Helpers
{
    public class AddCategoryData
    {
        public List<Category> Categories { get; set; }

        FirebaseClient client;
        public AddCategoryData()
        {
            client = new FirebaseClient("https://homestore-1713e-default-rtdb.europe-west1.firebasedatabase.app/");
            Categories = new List<Category>()
            {
                new Category()
                {
                    CategoryID = 1,
                    CategoryName = "Chairs",
                    CategoryPoster = "MainChairs",
                    ImageUrl = "baseline_chair_white_24dp.png"
                },
                new Category()
                {
                    CategoryID = 2,
                    CategoryName = "Beds",
                    CategoryPoster = "MainBeds",
                    ImageUrl = "outline_bed_white_24dp.png"
                },
                new Category()
                {
                    CategoryID = 3,
                    CategoryName = "Tables",
                    CategoryPoster = "MainTables",
                    ImageUrl = "baseline_table_bar_white_24dp.png"
                },
                new Category()
                {
                    CategoryID = 4,
                    CategoryName = "Appliances",
                    CategoryPoster = "MainAppliances",
                    ImageUrl = "baseline_local_laundry_service_white_24dp.png"
                },
                new Category()
                {
                    CategoryID = 5,
                    CategoryName = "Kitchen Misc",
                    CategoryPoster = "MainKitchen",
                    ImageUrl = "baseline_blender_white_24dp.png"
                },
            };
        }

        public async Task AddCategoriesAsync()
        {
            try
            {
                foreach (var category in Categories)
                {
                    await client.Child("Categories").PostAsync(new Category()
                    {
                        CategoryID = category.CategoryID,
                        CategoryName = category.CategoryName,
                        CategoryPoster = category.CategoryPoster,
                        ImageUrl = category.ImageUrl,
                    });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
