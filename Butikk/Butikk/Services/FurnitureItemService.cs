using Butikk.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace Butikk.Services
{
    public class FurnitureItemService
    {
        FirebaseClient client;
        public FurnitureItemService()
        {
            client = new FirebaseClient("https://homestore-1713e-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        //Returns all items from the Firebase DB
        public async Task<List<FurnitureItem>> GetFurnitureItemsAsync()
        {
            var products = (await client.Child("FurnitureItems")
                .OnceAsync<FurnitureItem>())
                .Select(f => new FurnitureItem
                {
                    CategoryID = f.Object.CategoryID,
                    Description = f.Object.Description,
                    HomeSelected = f.Object.HomeSelected,
                    ImageUrl = f.Object.ImageUrl,
                    Name = f.Object.Name,
                    Price = f.Object.Price,
                    ProductID = f.Object.ProductID,
                    Rating = f.Object.Rating,
                    RatingDetail = f.Object.RatingDetail
                }).ToList();
            return products;
        }

        //Returns items based on category
        public async Task<ObservableCollection<FurnitureItem>> GetItemsByCategoryAsync(int categoryID)
        {
            var itemsByCategory = new ObservableCollection<FurnitureItem>();
            var items = (await GetFurnitureItemsAsync()).Where(p => p.CategoryID == categoryID).ToList();

            foreach (var item in items)
            {
                itemsByCategory.Add(item);
            }
            return itemsByCategory;
        }

        public async Task<ObservableCollection<FurnitureItem>> GetLatestItemAsync()
        {
            var latestItems = new ObservableCollection<FurnitureItem>();
            var items = (await GetFurnitureItemsAsync()).OrderByDescending(f => f.ProductID).Take(3);

            foreach (var item in items)
            {
                latestItems.Add(item);
            }
            return latestItems;
        }

        //Search the DB
        public async Task<ObservableCollection<FurnitureItem>> GetItemsByQueryAsync(string searchTerm)
        {
            var itemsByQuery = new ObservableCollection<FurnitureItem>();
            var items = (await GetFurnitureItemsAsync()).Where(
                p => p.Name.Contains(searchTerm)).ToList();

            foreach (var item in items)
            {
                itemsByQuery.Add(item);
            }
            return itemsByQuery;
        }
    }
}
