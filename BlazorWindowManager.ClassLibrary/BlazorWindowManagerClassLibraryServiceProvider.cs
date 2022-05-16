using BlazorWindowManager.ClassLibrary.WindowManagerDialog;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWindowManager.ClassLibrary;

public static class BlazorWindowManagerClassLibraryServiceProvider
{
    public static IServiceCollection AddBlazorWindowManagerClassLibraryServices(this IServiceCollection services)
    {
        return services
            .AddBlazorWindowManagerClassLibraryFluxorServices()
            .AddWindowManagerDialogService();
    }
    
    private static IServiceCollection AddBlazorWindowManagerClassLibraryFluxorServices(this IServiceCollection services)
    {
        return services
            .AddFluxor(options => 
                options.ScanAssemblies(typeof(BlazorWindowManagerClassLibraryServiceProvider).Assembly));
    }
    
    private static IServiceCollection AddWindowManagerDialogService(this IServiceCollection services)
    {
        return services
            .AddScoped<IWindowManagerDialogService, WindowManagerDialogService>();
    }
}