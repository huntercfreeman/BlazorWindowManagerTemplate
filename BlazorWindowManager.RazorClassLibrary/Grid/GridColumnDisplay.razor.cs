using Fluxor;
using BlazorWindowManager.ClassLibrary.Direction;
using BlazorWindowManager.ClassLibrary.Element;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using Microsoft.AspNetCore.Components;
using BlazorWindowManager.ClassLibrary.Dimension;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridColumnDisplay : ComponentBase, IDisposable
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

    protected override void OnInitialized()
    {
        GridModel.DragEventOffsetInPixelsChangedEventHandler += GridModel_DragEventOffsetInPixelsChangedEventHandler;

        base.OnInitialized();
    }

    private async void GridModel_DragEventOffsetInPixelsChangedEventHandler(object? sender, EventArgs e)
    {
        await UpdateUi();
    }

    public async Task UpdateUi()
    {
        await InvokeAsync(StateHasChanged);
    }

    private string GetStyle()
    {
        var leftOperator = new DimensionValuedUnit(100.0 / GridTotalColumnCount, DimensionUnitKind.PercentageOfParentAsDecimal);

        var operand = DimensionValuedUnitCalculationOperatorKind.Addition;

        var rightOperator = new DimensionValuedUnit(GridModel.DragEventOffsetInPixels - 3, DimensionUnitKind.Pixels);

        var dimensionValuedUnitCalculation = new DimensionValuedUnitCalculation(leftOperator,
            operand,
            rightOperator);

        return $"width: {dimensionValuedUnitCalculation.BuildCssStyleString()};";
    }

    private void OnAddWindowEventCallback(CardinalDirectionKind cardinalDirectionKind)
    {
        if (AddWindowEventCallback.HasDelegate)
            AddWindowEventCallback.InvokeAsync((cardinalDirectionKind, GridColumnIndex));
    }

    public void Dispose()
    {
        GridModel.DragEventOffsetInPixelsChangedEventHandler -= GridModel_DragEventOffsetInPixelsChangedEventHandler;
    }
}