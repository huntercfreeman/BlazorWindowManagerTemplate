using BlazorWindowManager.ClassLibrary.Direction;
using Microsoft.AspNetCore.Components;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridIsEmptyDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public EventCallback<CardinalDirectionKind> AddWindowEventCallback { get; set; }

    private void OnAddWindowEventCallback(CardinalDirectionKind cardinalDirectionKind)
    {
        if (AddWindowEventCallback.HasDelegate)
            AddWindowEventCallback.InvokeAsync((cardinalDirectionKind));
    }
}