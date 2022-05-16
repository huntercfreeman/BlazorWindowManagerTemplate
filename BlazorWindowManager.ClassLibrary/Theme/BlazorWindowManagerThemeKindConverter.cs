using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Theme;

public static class BlazorWindowManagerThemeKindConverter
{
    public static string ConvertToCssClass(this BlazorWindowManagerThemeKind blazorWindowManagerThemeKind) =>
        blazorWindowManagerThemeKind switch
        {
            BlazorWindowManagerThemeKind.LightTheme => "bwmt_window-manager-theme-light",
            BlazorWindowManagerThemeKind.DarkTheme => "bwmt_window-manager-theme-dark",
            _ => ""
        };
}
