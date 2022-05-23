using BlazorWindowManager.ClassLibrary;
using Fluxor;
using BlazorWindowManager.RazorClassLibrary.Dimensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Theme;

namespace BlazorWindowManager.RazorClassLibrary;

public static class BlazorWindowManagerRazorClassLibraryServiceProvider
{
    /// <summary>
    /// The optional parameter called "themeCssClass" is used to override the colors set by the passed
    /// in BlazorWindowManagerThemeKind typed parameter.
    /// 
    /// In the case that themeCssClass is passed, the css class would need to replace css variables
    /// used in this library.
    /// 
    /// Example: .my-color-overrides { --bwmt_window-manager-dialog-display-background-color: #f9f2e8; ... etc. }
    /// </summary>
    public static IServiceCollection AddBlazorWindowManagerRazorClassLibraryServices(this IServiceCollection services,
        BlazorWindowManagerThemeKind blazorWindowManagerThemeKind,
        bool useDebugCssClasses,
        string? themeCssClass = null)
    {
        return services
            .AddBlazorWindowManagerClassLibraryServices()
            .AddViewportDimensionsService();
    }

    private static IServiceCollection AddViewportDimensionsService(this IServiceCollection services)
    {
        return services
            .AddScoped<IViewportDimensionsService, ViewportDimensionsService>(serviceProvider =>
                new ViewportDimensionsService(serviceProvider.GetRequiredService<IJSRuntime>()));
    }
}