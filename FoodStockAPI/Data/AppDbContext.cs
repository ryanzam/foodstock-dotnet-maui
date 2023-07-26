using FoodStockAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodStockAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
        {

        }

        public DbSet<FoodStock> FoodStocks => Set<FoodStock>();
    }
}
