using FoodStockMAUI.Models;
using FoodStockMAUI.Pages;
using FoodStockMAUI.Services;

namespace FoodStockMAUI
{
    public partial class MainPage : ContentPage
    {
        public IFoodStockService Service { get; }

        public MainPage(IFoodStockService stockService)
        {
            InitializeComponent();
            Service = stockService;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await Service.GetFoodStocks();
        }

        async void OnAddFood(object sender, EventArgs e)
        {
            var navParam = new Dictionary<string, object>
            {
                {nameof(FoodStock), new FoodStock() }
            };
            await Shell.Current.GoToAsync(nameof(HandleStock), navParam);
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var navParam = new Dictionary<string, object>
            {
                {nameof(FoodStock), e.CurrentSelection.FirstOrDefault() as FoodStock }
            };
            await Shell.Current.GoToAsync(nameof(HandleStock), navParam);
        }
    }
}