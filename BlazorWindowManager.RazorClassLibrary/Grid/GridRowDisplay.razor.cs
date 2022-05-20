using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using System.Collections.Immutable;
using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Grid;
using BlazorWindowManager.ClassLibrary.Html;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using BlazorWindowManager.ClassLibrary.Store.Html;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridRowDisplay : FluxorComponent
{
    [Inject]
    private IState<HtmlElementRecordsState> HtmlElementRecordsState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;
    
    [Parameter, EditorRequired]
    public ImmutableArray<GridItemRecord> Row { get; set; }
    [Parameter, EditorRequired]
    public int RowIndex { get; set; }
    [Parameter, EditorRequired]
    public int TotalRowCount { get; set; }

    private HtmlElementRecordKey _rowHtmlElementRecordKey = new(Guid.NewGuid());
    private HtmlElementRecord? _cachedHtmlElementRecord;
    private Guid? _previousHtmlElementSequence;
    
    protected override async Task OnInitializedAsync()
    {
        var initialWidth = new DimensionValuedUnit(100.0, DimensionUnitKind.PercentageOfParent);
        var initialHeight = new DimensionValuedUnit(100.0 / TotalRowCount, DimensionUnitKind.PercentageOfParent);
        var initialLeft = new DimensionValuedUnit(0, DimensionUnitKind.Pixels);
        var initialTop = new DimensionValuedUnit(0, DimensionUnitKind.Pixels);
        
        var registerHtmlElementAction = new RegisterHtmlElementAction(_rowHtmlElementRecordKey,
            new DimensionsRecord(initialWidth, initialHeight, initialLeft, initialTop),
            new ZIndexRecord(0));

        Dispatcher.Dispatch(registerHtmlElementAction);

        ShouldRender();

        await base.OnInitializedAsync();
    }
    
    protected override bool ShouldRender()
    {
        bool shouldRender;

        try
        {
            // Get HtmlElementRecord
            _cachedHtmlElementRecord = HtmlElementRecordsState.Value
                .LookupHtmlElementRecord(_rowHtmlElementRecordKey);

            if (_previousHtmlElementSequence is null ||
                _previousHtmlElementSequence.Value != _cachedHtmlElementRecord.HtmlElementSequence)
            {
                shouldRender = true;
            }
            else
            {
                shouldRender = false;
            }

            _previousHtmlElementSequence = _cachedHtmlElementRecord.HtmlElementSequence;
        }
        catch (KeyNotFoundException)
        {
            shouldRender = false;
        }

        return shouldRender;
    }
}