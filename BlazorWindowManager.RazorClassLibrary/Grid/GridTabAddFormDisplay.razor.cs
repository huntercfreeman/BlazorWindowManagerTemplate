using BlazorWindowManager.ClassLibrary.Direction;
using BlazorWindowManager.ClassLibrary.Grid;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridTabAddFormDisplay : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [CascadingParameter]
    public RenderFragment EmptyGridTabContainerRenderFragment { get; set; } = null!;
    [CascadingParameter]
    public Guid? ActiveGridTabId { get; set; } = null!;
    [CascadingParameter]
    public int? ActiveGridTabIndex { get; set; } = null!;
    [CascadingParameter]
    public GridItemRecordKey GridItemRecordKey { get; set; } = null!;

    private CardinalDirectionKind _selectedCardinalDirectionKind = CardinalDirectionKind.CurrentPosition;

    private void OnTypeToRenderSelectedAction((Type renderedContentType, string renderedContentTabDisplayName) argumentTuple)
    {
        var guidId = ActiveGridTabId ?? Guid.NewGuid();

        var replaceGridTabAction = new ReplaceGridTabRecordAction(GridItemRecordKey,
            new GridTabRecord(new GridTabRecordKey(guidId), argumentTuple.renderedContentType, argumentTuple.renderedContentTabDisplayName),
            ActiveGridTabIndex ?? 0);

        Dispatcher.Dispatch(replaceGridTabAction);
    }

    private void OnCardinalDirectionKindSelectedEventCallback(CardinalDirectionKind cardinalDirectionKind)
    {
        _selectedCardinalDirectionKind = cardinalDirectionKind;
    }
}