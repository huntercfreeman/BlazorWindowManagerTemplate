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
    private IState<ThemeState> ThemeState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;
    
    [CascadingParameter]
    public RenderFragment ChooseGridTabRecordRenderFragment { get; set; } = null!;
    
    [Parameter, EditorRequired]
    public GridItemRecord GridItemRecord { get; set; } = null!;
    [Parameter]
    public DimensionsRecord? InitialDimensionsRecord { get; set; }
    
    private GridTabContainerRecord? _cachedGridTabContainer;
    private Guid? _previousGridTabContainerSequence;
    
    protected override void OnInitialized()
    {
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
            _cachedGridTabContainer = GridItemRecordsState.Value
                .LookupGridTabContainer(GridItemRecord.GridItemRecordKey);

            if (_previousGridTabContainerSequence is null ||
                _previousGridTabContainerSequence.Value != _cachedGridTabContainer.GridTabContainerRecordSequence)
            {
                shouldRender = true;
            }
            else
            {
                shouldRender = false;
            }

            _previousGridTabContainerSequence = _cachedGridTabContainer.GridTabContainerRecordSequence;
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
}