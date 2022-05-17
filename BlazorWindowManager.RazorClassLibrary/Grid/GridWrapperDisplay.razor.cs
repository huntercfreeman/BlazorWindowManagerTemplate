using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using Fluxor.Blazor.Web.Components;
using Fluxor;
using BlazorWindowManager.ClassLibrary.Store.WindowManagerDialog;
using BlazorWindowManager.ClassLibrary.Store.Theme;
using System.Text;
using BlazorWindowManager.ClassLibrary.Theme;
using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Grid;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridWrapperDisplay : FluxorComponent
{
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = null!;

    [Parameter, EditorRequired]
    public GridRecord GridRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public DimensionsRecord InitialDimensionsRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public Type AddWindowToGridRenderedType { get; set; } = null!;
    [Parameter, EditorRequired]
    public Dictionary<string, object> AddWindowToGridRenderedTypeParameters { get; set; } = null!;
    [Parameter]
    public bool IsResizable { get; set; } = false;

    private string GetCssClasses()
    {
        var classBuilder = new StringBuilder();

        classBuilder.Append(ThemeState.Value.BlazorWindowManagerThemeKind.ConvertToCssClass());

        if (!string.IsNullOrWhiteSpace(ThemeState.Value.CssClassForOverridingColors))
            classBuilder.Append(ThemeState.Value.CssClassForOverridingColors);

        return classBuilder.ToString();
    }
}