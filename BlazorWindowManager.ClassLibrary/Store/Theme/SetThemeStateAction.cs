using BlazorWindowManager.ClassLibrary.Theme;

namespace BlazorWindowManager.ClassLibrary.Store.Theme;

public record SetThemeStateAction(BlazorWindowManagerThemeKind BlazorWindowManagerThemeKind, string? CssClassForOverridingColors);
