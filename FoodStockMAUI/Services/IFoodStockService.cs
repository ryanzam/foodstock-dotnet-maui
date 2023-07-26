using FoodStockMAUI.Models;

namespace FoodStockMAUI.Services
{
    public interface IFoodStockService
    {
        Task AddFood(FoodStock food);
        Task DeleteFood(int id);
        Task<FoodStock> GetFood(int id);
        Task<List<FoodStock>> GetFoodStocks();
        Task UpdateFood(FoodStock food);
    }
}