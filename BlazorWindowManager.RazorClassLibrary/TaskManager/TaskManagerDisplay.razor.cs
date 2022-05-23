using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using Fluxor.Blazor.Web.Components;
using Fluxor;
using BlazorWindowManager.ClassLibrary.Store.TaskManager;
using BlazorWindowManager.ClassLibrary.Store.Theme;
using System.Text;
using BlazorWindowManager.ClassLibrary.Theme;

namespace BlazorWindowManager.RazorClassLibrary.TaskManager;

public partial class TaskManagerDisplay : FluxorComponent
{
    [Inject]
    private IState<TaskManagerState> TaskManagerState { get; set; } = null!;
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = null!;

    private string GetCssClasses()
    {
        var classBuilder = new StringBuilder();

        classBuilder.Append(ThemeState.Value.BlazorWindowManagerThemeKind.ConvertToCssClass());

        if (!string.IsNullOrWhiteSpace(ThemeState.Value.CssClassForOverridingColors))
            classBuilder.Append(ThemeState.Value.CssClassForOverridingColors);

        return classBuilder.ToString();
    }
}