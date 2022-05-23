using BlazorWindowManager.ClassLibrary.DebugCssClasses;
using Microsoft.AspNetCore.Components;

namespace BlazorWindowManager.RazorClassLibrary.DebugCssClasses;

public partial class DebugCssClassSectionDisplay : ComponentBase
{
    [Parameter]
    public DebugCssClassSection DebugCssClassSection { get; set; } = null!;
}