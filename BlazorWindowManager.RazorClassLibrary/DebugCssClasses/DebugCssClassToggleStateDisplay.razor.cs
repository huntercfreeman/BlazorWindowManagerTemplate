using BlazorWindowManager.ClassLibrary.Store.DebugCssClasses;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorWindowManager.RazorClassLibrary.DebugCssClasses;

public partial class DebugCssClassToggleStateDisplay : FluxorComponent
{
    [Inject]
    private IState<DebugCssClassesState> DebugCssClassesState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    private void DispatchToggleDebugCssClassAction((Guid debugCssClassSectionId, Guid debugCssClassId) argumentTuple)
    {
        var toggleDebugCssClassAction = 
            new ToggleDebugCssClassAction(argumentTuple.debugCssClassSectionId, argumentTuple.debugCssClassId);
        
        Dispatcher.Dispatch(toggleDebugCssClassAction);
    }
}