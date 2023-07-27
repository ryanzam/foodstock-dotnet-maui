using FoodStockMAUI.Pages;

namespace FoodStockMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(HandleStock), typeof(HandleStock));
        }
    }
}