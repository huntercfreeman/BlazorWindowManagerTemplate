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
    
    [CascadingParameter]
    public int RowIndex { get; set; }
    
    [Parameter, EditorRequired]
    public GridRowRecord GridRowRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public int TotalRowCount { get; set; }

    private HtmlElementRecordKey _rowHtmlElementRecordKey = new(Guid.NewGuid());
    private HtmlElementRecord? _cachedHtmlElementRecord;
    
    private int _previousTotalRowCount;
    
    protected override async Task OnInitializedAsync()
    {
        HtmlElementRecordsState.StateChanged += OnStateChanged;
        
        var registerHtmlElementAction = new RegisterHtmlElementAction(_rowHtmlElementRecordKey,
            GetDimensionsRecord(),
            new ZIndexRecord(0));

        Dispatcher.Dispatch(registerHtmlElementAction);

        await base.OnInitializedAsync();
    }

    private void OnStateChanged(object? sender, EventArgs e)
    {
        try
        {
            if (_previousTotalRowCount != TotalRowCount)
            {
                _previousTotalRowCount = TotalRowCount;
                
                var replaceHtmlElementDimensionsRecordAction = new ReplaceHtmlElementDimensionsRecordAction(_rowHtmlElementRecordKey,
                    GetDimensionsRecord());
                
                Dispatcher.Dispatch(replaceHtmlElementDimensionsRecordAction);
            }
            
            _cachedHtmlElementRecord = HtmlElementRecordsState.Value
                .LookupHtmlElementRecord(_rowHtmlElementRecordKey);
        }
        catch (KeyNotFoundException)
        {
        }
    }

    private DimensionsRecord GetDimensionsRecord()
    {
        var initialWidth = new DimensionValuedUnit(100.0, DimensionUnitKind.PercentageOfParent);
        var initialHeight = new DimensionValuedUnit(100.0 / TotalRowCount, DimensionUnitKind.PercentageOfParent);
        var initialLeft = new DimensionValuedUnit(0, DimensionUnitKind.Pixels);
        var initialTop = new DimensionValuedUnit(0, DimensionUnitKind.Pixels);

        return new DimensionsRecord(initialWidth, initialHeight, initialLeft, initialTop);
    }

    private string GetKey()
    {
        return $"{_previousTotalRowCount}" +
               $"{_cachedHtmlElementRecord.HtmlElementSequence}" +
               $"{GridRowRecord.GridRowSequence}";
    }

    protected override void Dispose(bool disposing)
    {
        HtmlElementRecordsState.StateChanged -= OnStateChanged;
        
        base.Dispose(disposing);
    }
}