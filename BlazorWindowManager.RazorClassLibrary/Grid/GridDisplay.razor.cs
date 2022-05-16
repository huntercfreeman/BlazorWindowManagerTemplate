using Fluxor;
using Fluxor.Blazor.Web.Components;
using BlazorWindowManager.ClassLibrary.Direction;
using BlazorWindowManager.ClassLibrary.Element;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using Microsoft.AspNetCore.Components;
using BlazorWindowManager.ClassLibrary.Dimension;
using System.Text;
using BlazorWindowManager.ClassLibrary.Store.Theme;
using BlazorWindowManager.ClassLibrary.Theme;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridDisplay : FluxorComponent
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = null!;
    
    [Parameter, EditorRequired]
    public GridModel GridModel { get; set; } = null!;

    private DimensionsRecord _dimensionsRecord = new(new DimensionValuedUnit(400, DimensionUnitKind.Pixels),
        new DimensionValuedUnit(400, DimensionUnitKind.Pixels),
        new DimensionValuedUnit(0, DimensionUnitKind.Pixels),
        new DimensionValuedUnit(0, DimensionUnitKind.Pixels));

    private string GetCssClasses()
    {
        var classBuilder = new StringBuilder();

        classBuilder.Append(ThemeState.Value.BlazorWindowManagerThemeKind.ConvertToCssClass());

        if (!string.IsNullOrWhiteSpace(ThemeState.Value.CssClassForOverridingColors))
            classBuilder.Append(ThemeState.Value.CssClassForOverridingColors);

        return classBuilder.ToString();
    }

    private void OnAddWindowEventCallback((CardinalDirectionKind CardinalDirectionKind, int GridColumnIndex, int GridRowIndex) argumentTuple)
    {
        GridModel.AddGridModel(argumentTuple, new GridModel(Guid.NewGuid(), GridModel.RenderedContentType, new()));
    }

    private async Task OnDimensionsRecordChangedEventCallback(DimensionsRecord dimensionsRecord)
    {
        _dimensionsRecord = dimensionsRecord;

        await InvokeAsync(StateHasChanged);
    }
}