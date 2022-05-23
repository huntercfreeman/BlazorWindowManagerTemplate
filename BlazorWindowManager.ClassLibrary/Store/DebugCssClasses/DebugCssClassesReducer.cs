using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.DebugCssClasses;

public class DebugCssClassesReducer
{
    [ReducerMethod]
    public static DebugCssClassesState ReduceToggleDebugCssClassAction(DebugCssClassesState previousDebugCssClassesState,
        ToggleDebugCssClassAction toggleDebugCssClassAction)
    {
        return new DebugCssClassesState(previousDebugCssClassesState,
            toggleDebugCssClassAction.DebugCssClassSectionId,
            toggleDebugCssClassAction.DebugCssClassId);
    }
}