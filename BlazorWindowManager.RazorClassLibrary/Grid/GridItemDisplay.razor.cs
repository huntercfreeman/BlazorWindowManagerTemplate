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
    public RenderFragment ChooseGridTabRecordRenderFragment { get; set; } = null!;
    
    [Parameter, EditorRequired]
    public GridItemRecord GridItemRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public int TotalGridItemCountInRow { get; set; }
    
    private GridTabContainerRecord? _cachedGridTabContainer;
    private Guid? _previousGridTabContainerSequence;
    
    private HtmlElementRecord? _cachedHtmlElementRecord;
    private Guid? _previousHtmlElementSequence;

    private DimensionValuedUnit _heightOfGridTabDimensionValuedUnit = new DimensionValuedUnit(2, DimensionUnitKind.Rem);
    
    protected override void OnInitialized()
    {
        var initialWidth = 
            new DimensionValuedUnit(100.0 / (TotalGridItemCountInRow == 0 ? 1 : TotalGridItemCountInRow),
                DimensionUnitKind.PercentageOfParent);
        var initialHeight = new DimensionValuedUnit(100.0, DimensionUnitKind.PercentageOfParent);
        var initialLeft = new DimensionValuedUnit(0, DimensionUnitKind.Pixels);
        var initialTop = new DimensionValuedUnit(0, DimensionUnitKind.Pixels);
        
        var registerHtmlElementAction = new RegisterHtmlElementAction(GridItemRecord.HtmlElementRecordKey,
            new DimensionsRecord(initialWidth, initialHeight, initialLeft, initialTop),
            new ZIndexRecord(0));

        Dispatcher.Dispatch(registerHtmlElementAction);
        
        var registerGridTabContainerRecordAction = new RegisterGridTabContainerRecordAction(GridItemRecord.GridItemRecordKey);

        Dispatcher.Dispatch(registerGridTabContainerRecordAction);

        ShouldRender();
        
        base.OnInitialized();
    }

    protected override bool ShouldRender()
    {
        bool shouldRender;

        try
        {
            // Get HtmlElementRecord
            var htmlElementRecordStepNeedsRerender = false;
            
            _cachedHtmlElementRecord = HtmlElementRecordsState.Value
                .LookupHtmlElementRecord(GridItemRecord.HtmlElementRecordKey);

            if (_previousHtmlElementSequence is null ||
                _previousHtmlElementSequence.Value != _cachedHtmlElementRecord.HtmlElementSequence)
            {
                htmlElementRecordStepNeedsRerender = true;
            }

            _previousHtmlElementSequence = _cachedHtmlElementRecord.HtmlElementSequence;
            
            // Get GridTabContainerRecord
            var gridTabContainerRecordStepNeedsRerender = false;
            
            _cachedGridTabContainer = GridItemRecordsState.Value
                .LookupGridTabContainer(GridItemRecord.GridItemRecordKey);

            if (_previousGridTabContainerSequence is null ||
                _previousGridTabContainerSequence.Value != _cachedGridTabContainer.GridTabContainerRecordSequence)
            {
                gridTabContainerRecordStepNeedsRerender = true;
            }

            shouldRender = htmlElementRecordStepNeedsRerender || gridTabContainerRecordStepNeedsRerender;
        }
        catch (KeyNotFoundException)
        {
            shouldRender = false;
        }

        return shouldRender;
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
        var unregisterHtmlElementAction = new UnregisterHtmlElementAction(GridItemRecord.HtmlElementRecordKey);

        Dispatcher.Dispatch(unregisterHtmlElementAction);

        base.Dispose(disposing);
    }
}