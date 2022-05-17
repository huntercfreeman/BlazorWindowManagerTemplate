using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Direction;
using BlazorWindowManager.ClassLibrary.Element;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

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
    [Parameter, EditorRequired]
    public DimensionsRecord DimensionsRecord { get; set; } = null!;

    public int DragEventOffsetInPixels;

    public async Task UpdateUi()
    {
        await InvokeAsync(StateHasChanged);
    }

    private MouseEventArgs? _previousMouseEventArgs;

    private string GetStyle()
    {
        var leftOperator = new DimensionValuedUnit(100.0 / GridTotalRowCount, DimensionUnitKind.PercentageOfParentAsDecimal);

        var operand = DimensionValuedUnitCalculationOperatorKind.Subtraction;

        var rightOperator = new DimensionValuedUnit(DragEventOffsetInPixels + 3, DimensionUnitKind.Pixels);

        var dimensionValuedUnitCalculation = new DimensionValuedUnitCalculation(leftOperator,
            operand,
            rightOperator);

        return $"height: {dimensionValuedUnitCalculation.BuildCssStyleString()};";
    }

    private void OnAddWindowEventCallback((CardinalDirectionKind CardinalDirectionKind, int GridColumnIndex) argumentTuple)
    {
        if (AddWindowEventCallback.HasDelegate)
            AddWindowEventCallback.InvokeAsync((argumentTuple.CardinalDirectionKind, argumentTuple.GridColumnIndex, GridRowIndex));
    }

    private void OnMouseDownEventCallback((GridModel LeftGridModel, GridModel RightGridModel) argumentTuple)
    {
        argumentTuple.LeftGridModel.DragEventOffsetInPixels -= 5;
        argumentTuple.RightGridModel.DragEventOffsetInPixels += 5;

        argumentTuple.LeftGridModel.DragEventOffsetInPixelsChangedEventHandlerInvoke(this, EventArgs.Empty);
        argumentTuple.RightGridModel.DragEventOffsetInPixelsChangedEventHandlerInvoke(this, EventArgs.Empty);
    }
}