using BlazorWindowManager.ClassLibrary.Grid;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridDisplay : FluxorComponent
{
    [Inject]
    private IState<GridRecordsState> GridRecordsState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null;

    [Parameter, EditorRequired]
    public GridRecord GridRecord { get; set; } = null!;
    [Parameter]
    public IEnumerable<IEnumerable<GridItemRecord>>? InitialGridItemRecords { get; set; }

    private GridBoard? _cachedGridBoard;
    private Guid? _previousGridBoardSequence;

    protected override void OnInitialized()
    {
        var registerGridRecordAction = new RegisterGridRecordAction(GridRecord.GridRecordKey,
            null,
            null,
            null,
            null);

        Dispatcher.Dispatch(registerGridRecordAction);

        base.OnInitialized();
    }

    protected override bool ShouldRender()
    {
        bool shouldRender;

        try
        {
            _cachedGridBoard = GridRecordsState.Value
                .LookupGridBoard(GridRecord.GridRecordKey);

            if (_previousGridBoardSequence is null ||
                _previousGridBoardSequence.Value != _cachedGridBoard.GridBoardSequence)
            {
                shouldRender = true;
            }
            else
            {
                shouldRender = false;
            }

            _previousGridBoardSequence = _cachedGridBoard.GridBoardSequence;
        }
        catch (KeyNotFoundException)
        {
            shouldRender = false;
        }

        return shouldRender;
    }
}