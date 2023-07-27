using FoodStockMAUI.Models;
using System.Diagnostics;
using System.Text.Json;

namespace FoodStockMAUI.Services
{
    public class FoodStockService : IFoodStockService
    {
        private readonly HttpClient HttpClient;
        private readonly string BaseAddr;
        private readonly string Url;
        private readonly JsonSerializerOptions JsonSerializerOpts;

        public FoodStockService()
        {
            HttpClient = new HttpClient();
            BaseAddr = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5223" : "http://localhost:5223";
            Url = BaseAddr + "/api/foodstock";
            JsonSerializerOpts = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        public async Task<List<FoodStock>> GetFoodStocks()

        {
            List<FoodStock> foodStocks = new();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet.");
                return foodStocks;
            };
            try
            {
                HttpResponseMessage response = await HttpClient.GetAsync(Url);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    foodStocks = JsonSerializer.Deserialize<List<FoodStock>>(content, JsonSerializerOpts);
                }
                else
                {
                    Debug.WriteLine($"Not success status code.");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error occured while fetching foodstocks: " + e.Message);
            }
            return foodStocks;
        }

        public async Task<FoodStock> GetFood(int id)
        {
            var foodStock = new FoodStock();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet.");
                return foodStock;
            };
            try
            {
                HttpResponseMessage response = await HttpClient.GetAsync(Url + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    foodStock = JsonSerializer.Deserialize<FoodStock>(content, JsonSerializerOpts);
                }
                else
                {
                    Debug.WriteLine($"Not success status code.");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error occured while fetching foodstocks: " + e.Message);
            }
            return foodStock;
        }

        public async Task AddFood(FoodStock food)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet.");
                return;
            };
            try
            {
                string jsonFoodStock = JsonSerializer.Serialize<FoodStock>(food, JsonSerializerOpts);
                StringContent stringContent = new StringContent(jsonFoodStock, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await HttpClient.PostAsync(Url, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully added food.");
                }
                else
                {
                    Debug.WriteLine("Adding food failed.");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error occured while fetching foodstocks: " + e.Message);
            }
        }

        public async Task UpdateFood(FoodStock food)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet.");
                return;
            };
            try
            {
                string jsonFoodStock = JsonSerializer.Serialize<FoodStock>(food, JsonSerializerOpts);
                StringContent stringContent = new StringContent(jsonFoodStock, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await HttpClient.PutAsync($"{Url}/{food.Id}", stringContent);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully added food.");
                }
                else
                {
                    Debug.WriteLine("Adding food failed.");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error occured while fetching foodstocks: " + e.Message);
            }
        }

        public async Task DeleteFood(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet.");
                return;
            };
            try
            {
                HttpResponseMessage response = await HttpClient.DeleteAsync(Url + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully deleted.");
                }
                else
                {
                    Debug.WriteLine("Adding food failed.");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error occured while fetching foodstocks: " + e.Message);
            }
        }
    }
}
