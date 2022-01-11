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
    public class AddFurnitureItemData
    {
        FirebaseClient client;
        public List<FurnitureItem> FurnitureItems { get; set; }

        public AddFurnitureItemData()
        {
            client = new FirebaseClient("https://homestore-1713e-default-rtdb.europe-west1.firebasedatabase.app/");
            FurnitureItems = new List<FurnitureItem>()
            {
                new FurnitureItem
                {
                    ProductID = 6,
                    CategoryID = 1,
                    ImageUrl =  "chair_2",
                    Name =  "Chair",
                    Description = "A normal chair",
                    Rating = "3.8",
                    RatingDetail = " (21 ratings)",
                    HomeSelected = "EmptyHeart",
                    Price = 79
                },
                new FurnitureItem
                {
                    ProductID = 7,
                    CategoryID = 2,
                    ImageUrl =  "bed_2",
                    Name =  "Decent bed",
                    Description = "A bed to sleep in",
                    Rating = "4.4",
                    RatingDetail = " (33 ratings)",
                    HomeSelected = "EmptyHeart",
                    Price = 2399
                },
                new FurnitureItem
                {
                    ProductID = 8,
                    CategoryID = 3,
                    ImageUrl =  "table_1",
                    Name =  "Square Walnut Table",
                    Description = "Coffee Table",
                    Rating = "4.1",
                    RatingDetail = " (21 ratings)",
                    HomeSelected = "EmptyHeart",
                    Price = 49
                },
                new FurnitureItem
                {
                    ProductID = 9,
                    CategoryID = 3,
                    ImageUrl =  "table_2",
                    Name =  "Square Oak Table",
                    Description = "Coffee Table",
                    Rating = "4.4",
                    RatingDetail = " (28 ratings)",
                    HomeSelected = "EmptyHeart",
                    Price = 54
                },
                new FurnitureItem
                {
                    ProductID = 10,
                    CategoryID = 3,
                    ImageUrl =  "table_3",
                    Name =  "Round Walnut Table",
                    Description = "Coffee Table",
                    Rating = "3.6",
                    RatingDetail = " (18 ratings)",
                    HomeSelected = "EmptyHeart",
                    Price = 49
                },
                new FurnitureItem
                {
                    ProductID = 11,
                    CategoryID = 3,
                    ImageUrl =  "table_4",
                    Name =  "Triangle Mahogny Table",
                    Description = "Coffee Table",
                    Rating = "3.3",
                    RatingDetail = " (11 ratings)",
                    HomeSelected = "EmptyHeart",
                    Price = 39
                },
                new FurnitureItem
                {
                    ProductID = 12,
                    CategoryID = 3,
                    ImageUrl =  "table_5",
                    Name =  "Glass Pine Table",
                    Description = "Coffee Table",
                    Rating = "5.6",
                    RatingDetail = " (38 ratings)",
                    HomeSelected = "EmptyHeart",
                    Price = 59
                },
                new FurnitureItem
                {
                    ProductID = 13,
                    CategoryID = 4,
                    ImageUrl =  "refrigerator_1",
                    Name =  "Smart Fridge F88",
                    Description = "Make life easier!",
                    Rating = "4.6",
                    RatingDetail = " (78 ratings)",
                    HomeSelected = "EmptyHeart",
                    Price = 1229
                },
                new FurnitureItem
                {
                    ProductID = 10,
                    CategoryID = 5,
                    ImageUrl =  "misc_kitchen_2",
                    Name =  "Kitchen Kit",
                    Description = "All you need for your kitchen",
                    Rating = "3.6",
                    RatingDetail = " (32 ratings)",
                    HomeSelected = "EmptyHeart",
                    Price = 89
                }
            };
        }

        public async Task AddFurnitureItemsAsync()
        {
            try
            {
                foreach (var item in FurnitureItems)
                {
                    await client.Child("FurnitureItems").PostAsync(new FurnitureItem()
                    {
                        CategoryID = item.CategoryID,
                        ImageUrl = item.ImageUrl,
                        Name = item.Name,
                        Description = item.Description,
                        Rating = item.Rating,
                        RatingDetail = item.RatingDetail,
                        HomeSelected = item.HomeSelected,
                        Price = item.Price,
                        ProductID = item.ProductID
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
