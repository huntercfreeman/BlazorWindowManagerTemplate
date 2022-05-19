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

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridItemDisplay : FluxorComponent
{
    [Inject]
    private IState<GridItemRecordsState> GridItemRecordsState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public GridItemRecord GridItemRecord { get; set; } = null!;

    private GridBoard? _cachedGridTabContainer;
    private Guid? _previousGridTabContainerSequence;

    protected override void OnInitialized()
    {
        var registerGridTabContainerRecordAction = new RegisterGridTabContainerRecordAction(GridItemRecord.GridItemRecordKey);

        Dispatcher.Dispatch(registerGridTabContainerRecordAction);

        base.OnInitialized();
    }

    protected override bool ShouldRender()
    {
        bool shouldRender;

        try
        {
            _cachedGridTabContainer = GridRecordsState.Value
                .LookupGridBoard(GridItemRecord.GridItemRecordKey);

            if (_previousGridBoardSequence is null ||
                _previousGridBoardSequence.Value != _cachedGridTabContainer.GridBoardSequence)
            {
                shouldRender = true;
            }
            else
            {
                shouldRender = false;
            }

            _previousGridBoardSequence = _cachedGridTabContainer.GridBoardSequence;
        }
        catch (KeyNotFoundException)
        {
            shouldRender = false;
        }

        return shouldRender;
    }

    private void AddGridItemRecordOnClick()
    {
        var addGridItemRecordAction = new AddGridItemRecordAction(GridRecord.GridRecordKey,
            new GridItemRecord(new GridItemRecordKey(Guid.NewGuid()),
                               new HtmlElementRecordKey(Guid.NewGuid())));

        Dispatcher.Dispatch(addGridItemRecordAction);
    }
}