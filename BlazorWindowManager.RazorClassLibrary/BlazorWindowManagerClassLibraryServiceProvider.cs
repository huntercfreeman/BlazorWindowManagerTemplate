using BlazorWindowManager.ClassLibrary;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWindowManager.RazorClassLibrary;

public static class BlazorWindowManagerRazorClassLibraryServiceProvider
{
    public static IServiceCollection AddBlazorWindowManagerRazorClassLibraryServices(this IServiceCollection services)
    {
        return services
            .AddBlazorWindowManagerClassLibraryServices();
    }
}