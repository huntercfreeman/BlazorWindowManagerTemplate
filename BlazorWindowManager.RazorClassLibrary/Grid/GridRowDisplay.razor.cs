﻿using BlazorWindowManager.ClassLibrary.Direction;
using BlazorWindowManager.ClassLibrary.Element;
using Microsoft.AspNetCore.Components;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridRowDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public int GridRowIndex { get; set; }
    [Parameter, EditorRequired]
    public int GridTotalRowCount { get; set; }
    [Parameter, EditorRequired]
    public List<GridModel> GridRowElementReferences { get; set; } = null!;
    [Parameter, EditorRequired]
    public EventCallback<(CardinalDirectionKind CardinalDirectionKind, int GridColumnIndex, int GridRowIndex)> AddWindowEventCallback { get; set; }

    private string GetStyle => $"height: calc({100.0 / GridTotalRowCount}% - 3px);";

    private void OnAddWindowEventCallback((CardinalDirectionKind CardinalDirectionKind, int GridColumnIndex) argumentTuple)
    {
        if (AddWindowEventCallback.HasDelegate)
            AddWindowEventCallback.InvokeAsync((argumentTuple.CardinalDirectionKind, argumentTuple.GridColumnIndex, GridRowIndex));
    }
}