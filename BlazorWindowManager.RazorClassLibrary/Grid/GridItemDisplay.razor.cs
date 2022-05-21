using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWindowManager.ClassLibrary.Dimension;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.Grid;
using BlazorWindowManager.ClassLibrary.Html;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using BlazorWindowManager.ClassLibrary.Store.Html;
using BlazorWindowManager.ClassLibrary.Store.Theme;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using BlazorWindowManager.RazorClassLibrary.Html;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridItemDisplay : FluxorComponent
{
    [Inject]
    private IState<GridItemRecordsState> GridItemRecordsState { get; set; } = null!;
    [Inject]
    private IState<HtmlElementRecordsState> HtmlElementRecordsState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;
    
    [CascadingParameter]
    public GridRecordKey GridRecordKey { get; set; } = null!;
    [CascadingParameter]
    public RenderFragment ChooseGridTabRecordRenderFragment { get; set; } = null!;
    [CascadingParameter]
    public int TotalGridItemCountInRow { get; set; }
    
    [Parameter, EditorRequired]
    public GridItemRecord GridItemRecord { get; set; } = null!;
    
    private GridTabContainerRecord? _cachedGridTabContainer;
    private HtmlElementRecord? _cachedHtmlElementRecord;
    private int _previousTotalGridItemCountInRow;

    private DimensionValuedUnit _heightOfGridTabDimensionValuedUnit = new DimensionValuedUnit(2, DimensionUnitKind.Rem);
    
    protected override void OnInitialized()
    {
        GridItemRecordsState.StateChanged += OnStateChanged;
        HtmlElementRecordsState.StateChanged += OnStateChanged;
        
        try
        {
            _cachedHtmlElementRecord = HtmlElementRecordsState.Value
                .LookupHtmlElementRecord(GridItemRecord.HtmlElementRecordKey);
        }
        catch (KeyNotFoundException)
        {
            // Not yet initialized
            var registerHtmlElementAction = new RegisterHtmlElementAction(GridItemRecord.HtmlElementRecordKey,
                GetDimensionsRecord(),
                new ZIndexRecord(0));

            Dispatcher.Dispatch(registerHtmlElementAction);
        }
        
        try
        {
            _cachedGridTabContainer = GridItemRecordsState.Value
                .LookupGridTabContainer(GridItemRecord.GridItemRecordKey);
        }
        catch (KeyNotFoundException)
        {
            // Not yet initialized
            var registerGridTabContainerRecordAction = new RegisterGridTabContainerRecordAction(GridItemRecord.GridItemRecordKey);

            Dispatcher.Dispatch(registerGridTabContainerRecordAction);
        }
        
        base.OnInitialized();
    }

    private void OnStateChanged(object? sender, EventArgs e)
    {
        try
        {
            if (_previousTotalGridItemCountInRow != TotalGridItemCountInRow)
            {
                _previousTotalGridItemCountInRow = TotalGridItemCountInRow;
                
                var replaceHtmlElementDimensionsRecordAction = new ReplaceHtmlElementDimensionsRecordAction(GridItemRecord.HtmlElementRecordKey,
                    GetDimensionsRecord());
                
                Dispatcher.Dispatch(replaceHtmlElementDimensionsRecordAction);
            }
            
            _cachedHtmlElementRecord = HtmlElementRecordsState.Value
                .LookupHtmlElementRecord(GridItemRecord.HtmlElementRecordKey);
            
            _cachedGridTabContainer = GridItemRecordsState.Value
                .LookupGridTabContainer(GridItemRecord.GridItemRecordKey);
        }
        catch (KeyNotFoundException)
        {
        }
    }

    private DimensionsRecord GetDimensionsRecord()
    {
        var initialWidth = 
            new DimensionValuedUnit(100.0 / (TotalGridItemCountInRow == 0 ? 1 : TotalGridItemCountInRow),
                DimensionUnitKind.PercentageOfParent);
        var initialHeight = new DimensionValuedUnit(100.0, DimensionUnitKind.PercentageOfParent);
        var initialLeft = new DimensionValuedUnit(0, DimensionUnitKind.Pixels);
        var initialTop = new DimensionValuedUnit(0, DimensionUnitKind.Pixels);

        return new DimensionsRecord(initialWidth, initialHeight, initialLeft, initialTop);
    }

    private string GetKey()
    {
        return $"{TotalGridItemCountInRow}" +
               $"{_cachedGridTabContainer.GridTabContainerRecordSequence}" +
               $"{_cachedHtmlElementRecord}";
    }

    private void AddGridTabRecordOnClick()
    {
        var addGridTabRecordAction = new AddGridTabRecordAction(GridItemRecord.GridItemRecordKey,
            new GridTabRecord(new GridTabRecordKey(Guid.NewGuid()),
                              typeof(HtmlElementExampleWrapperDisplay),
                              nameof(HtmlElementExampleWrapperDisplay)),
            0);

        Dispatcher.Dispatch(addGridTabRecordAction);
    }

    private void OnGridTabRecordChosenAction((Type renderedContentType, string renderedContentTabDisplayName) argumentTuple)
    {
        var addGridTabRecordAction = new AddGridTabRecordAction(GridItemRecord.GridItemRecordKey,
            new GridTabRecord(new GridTabRecordKey(Guid.NewGuid()), argumentTuple.renderedContentType,
                argumentTuple.renderedContentTabDisplayName),
            0);

        Dispatcher.Dispatch(addGridTabRecordAction);
    }
    
    private Type? GetGridBodyRenderedContentType()
    {
        if(_cachedGridTabContainer is not null &&
           _cachedGridTabContainer.ActiveGridTabIndex is not null)
        {
            return _cachedGridTabContainer.GridTabRecords[_cachedGridTabContainer.ActiveGridTabIndex.Value].RenderedContentType;
        }

        return null;
    }

    private Guid? GetActiveGridTabId()
    {
        if(_cachedGridTabContainer is not null &&
           _cachedGridTabContainer.ActiveGridTabIndex is not null)
        {
            return _cachedGridTabContainer
                .GridTabRecords[_cachedGridTabContainer.ActiveGridTabIndex.Value]
                .GridTabRecordKey.Id;
        }

        return null;
    }
    
    protected override void Dispose(bool disposing)
    {
        GridItemRecordsState.StateChanged -= OnStateChanged;
        HtmlElementRecordsState.StateChanged -= OnStateChanged;
        
        var unregisterHtmlElementAction = new UnregisterHtmlElementAction(GridItemRecord.HtmlElementRecordKey);

        Dispatcher.Dispatch(unregisterHtmlElementAction);

        base.Dispose(disposing);
    }
}