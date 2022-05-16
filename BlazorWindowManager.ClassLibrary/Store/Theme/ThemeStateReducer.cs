using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.Theme;

public class ThemeStateReducer
{
    [ReducerMethod]
    public ThemeState ReduceSetThemeStateAction(ThemeState previousThemeState, SetThemeStateAction setThemeStateAction)
    {
        return new ThemeState(setThemeStateAction.BlazorWindowManagerThemeKind, setThemeStateAction.CssClassForOverridingColors);
    }
}
