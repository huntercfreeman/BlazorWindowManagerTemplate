using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.Grid;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using BlazorWindowManager.RazorClassLibrary.Html;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridItemDisplay : FluxorComponent
{
    [Inject]
    private IState<GridItemRecordsState> GridItemRecordsState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;
    
    [CascadingParameter]
    public RenderFragment ChooseGridTabRecordRenderFragment { get; set; } = null!;
    
    [Parameter, EditorRequired]
    public GridItemRecord GridItemRecord { get; set; } = null!;
    
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
}