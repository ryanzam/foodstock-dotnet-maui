using CommunityToolkit.Maui;
using FoodStockMAUI.Pages;
using FoodStockMAUI.Services;
using Microsoft.Extensions.Logging;

namespace FoodStockMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddHttpClient<IFoodStockService, FoodStockService>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<HandleStock>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}