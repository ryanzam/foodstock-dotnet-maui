using FoodStockMAUI.Models;
using FoodStockMAUI.Services;

namespace FoodStockMAUI.Pages;

[QueryProperty(nameof(FoodStock), "FoodStock")]
public partial class HandleStock : ContentPage
{
    private readonly IFoodStockService Service;
    bool _isNew;
    FoodStock _food;

    public FoodStock FoodStock
    {
        get => _food;
        set
        {
            _isNew = IsNewFood(value);
            _food = value;
            OnPropertyChanged();
        }
    }

    public HandleStock(IFoodStockService service)
    {
        InitializeComponent();
        Service = service;
        BindingContext = this;
    }

    bool IsNewFood(FoodStock food)
    {
        return food.Id == 0;
    }

    async void OnAdd(object sender, EventArgs e)
    {
        if (_isNew)
        {
            await Service.AddFood(FoodStock);
        }
        else
        {
            await Service.UpdateFood(FoodStock);
        }
        await Shell.Current.GoToAsync("..");
    }

    async void OnDelete(object sender, EventArgs e)
    {
        await Service.DeleteFood(FoodStock.Id);
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancel(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}