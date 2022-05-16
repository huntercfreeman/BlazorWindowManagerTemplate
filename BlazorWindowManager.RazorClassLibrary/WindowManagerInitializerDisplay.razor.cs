using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorWindowManager.ClassLibrary.Store.Drag;
using BlazorWindowManager.ClassLibrary.Store.Theme;
using BlazorWindowManager.ClassLibrary.Theme;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorWindowManager.RazorClassLibrary;

public partial class WindowManagerInitializerDisplay : FluxorComponent
{
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = null!;

    // TODO: Make the website entirely unselectable when dragging
    //private string GetIsSelectableCssClass => DragState.Value.MouseEventArgs is null
    //    ? string.Empty
    //    : "bwmt_unselectable";

    private string GetCssClasses()
    {
        var classBuilder = new StringBuilder();

        classBuilder.Append(ThemeState.Value.BlazorWindowManagerThemeKind.ConvertToCssClass());

        if(!string.IsNullOrWhiteSpace(ThemeState.Value.CssClassForOverridingColors))
            classBuilder.Append(ThemeState.Value.CssClassForOverridingColors);

        return classBuilder.ToString();
    }
}