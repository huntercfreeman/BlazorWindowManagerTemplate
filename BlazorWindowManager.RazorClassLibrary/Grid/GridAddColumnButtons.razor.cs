using BlazorWindowManager.ClassLibrary.Direction;
using Microsoft.AspNetCore.Components;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridAddColumnButtons : ComponentBase
{
    [Parameter, EditorRequired]
    public EventCallback<CardinalDirectionKind> AddWindowEventCallback { get; set; }

    private void FireCardinalDirectionKind(CardinalDirectionKind cardinalDirectionKind)
    {
        if (AddWindowEventCallback.HasDelegate)
            AddWindowEventCallback.InvokeAsync(cardinalDirectionKind);
    }
}