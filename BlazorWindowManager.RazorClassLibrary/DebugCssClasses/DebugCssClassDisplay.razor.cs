using BlazorWindowManager.ClassLibrary.DebugCssClasses;
using Microsoft.AspNetCore.Components;

namespace BlazorWindowManager.RazorClassLibrary.DebugCssClasses;

public partial class DebugCssClassDisplay : ComponentBase
{
    [CascadingParameter]
    public Action<(Guid debugCssClassSectionId, Guid debugCssClassId)> DispatchToggleDebugCssClassAction { get; set; } = null!;
    
    [Parameter, EditorRequired]
    public Guid DebugCssClassSectionId { get; set; }
    [Parameter, EditorRequired]
    public DebugCssClass DebugCssClass { get; set; } = null!;

    private string IsActiveCssClass => DebugCssClass.IsActive
        ? "bwmt_active"
        : string.Empty;
    
    private void FireDispatchToggleDebugCssClassAction()
    {
        DispatchToggleDebugCssClassAction((DebugCssClassSectionId, DebugCssClass.DebugCssClassId));
    }
}