using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace Inflow;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        var services = builder.Services;
        services.AddMauiBlazorWebView();
        services.AddMudServices();

#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}