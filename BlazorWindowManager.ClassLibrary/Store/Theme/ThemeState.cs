using BlazorWindowManager.ClassLibrary.Theme;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Store.Theme;

[FeatureState]
public record ThemeState(BlazorWindowManagerThemeKind BlazorWindowManagerThemeKind, string? CssClassForOverridingColors)
{
    public ThemeState() : this(BlazorWindowManagerThemeKind.LightTheme, null)
    {

    }
}
