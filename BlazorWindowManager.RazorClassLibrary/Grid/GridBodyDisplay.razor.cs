using Microsoft.AspNetCore.Components;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridBodyDisplay : ComponentBase
{
    [CascadingParameter]
    public RenderFragment EmptyGridTabContainerRenderFragment { get; set; } = null!;

    [Parameter, EditorRequired]
    public Type? RenderedContentType { get; set; }
}