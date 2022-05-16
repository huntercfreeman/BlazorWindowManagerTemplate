using Fluxor;
using BlazorWindowManager.ClassLibrary.Direction;
using BlazorWindowManager.ClassLibrary.Element;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using Microsoft.AspNetCore.Components;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridColumnDisplay : ComponentBase
{
    [Inject]
    private IState<GridState> GridState { get; set; } = null!;

    [Parameter, EditorRequired]
    public int GridColumnIndex { get; set; }
    [Parameter, EditorRequired]
    public int GridTotalColumnCount { get; set; }
    [Parameter, EditorRequired]
    public GridModel GridModel { get; set; } = null!;
    [Parameter, EditorRequired]
    public EventCallback<(CardinalDirectionKind CardinalDirectionKind, int GridColumnIndex)> AddWindowEventCallback { get; set; }

    private string GetStyle => $"width: calc({100.0 / GridTotalColumnCount}% - 3px);";

    private void OnAddWindowEventCallback(CardinalDirectionKind cardinalDirectionKind)
    {
        if (AddWindowEventCallback.HasDelegate)
            AddWindowEventCallback.InvokeAsync((cardinalDirectionKind, GridColumnIndex));
    }
}