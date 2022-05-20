using BlazorWindowManager.ClassLibrary.Direction;
using BlazorWindowManager.ClassLibrary.Grid;
using BlazorWindowManager.ClassLibrary.Html;
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
    [CascadingParameter(Name="ActiveGridTabIndex")]
    public int? ActiveGridTabIndex { get; set; }
    [CascadingParameter(Name="ActiveRowIndex")]
    public int? ActiveRowIndex { get; set; }
    [CascadingParameter(Name="ActiveRowIndex")]
    public int? ActiveGridItemRecordIndex { get; set; }
    [CascadingParameter]
    public GridItemRecordKey GridItemRecordKey { get; set; } = null!;
    [CascadingParameter]
    public GridRecordKey GridRecordKey { get; set; } = null!;

    private CardinalDirectionKind _selectedCardinalDirectionKind = CardinalDirectionKind.CurrentPosition;

    private void OnTypeToRenderSelectedAction((Type renderedContentType, string renderedContentTabDisplayName) argumentTuple)
    {
        if (_selectedCardinalDirectionKind == CardinalDirectionKind.CurrentPosition)
        {
            var guidId = ActiveGridTabId ?? Guid.NewGuid();
            
            var replaceGridTabAction = new ReplaceGridTabRecordAction(GridItemRecordKey,
                new GridTabRecord(new GridTabRecordKey(guidId), argumentTuple.renderedContentType, argumentTuple.renderedContentTabDisplayName),
                ActiveGridTabIndex ?? 0);

            Dispatcher.Dispatch(replaceGridTabAction);
        }
        else
        {
            var addGridItemRecordAction = new AddGridItemRecordAction(GridRecordKey,
                new GridItemRecord(new GridItemRecordKey(Guid.NewGuid()),
                    new HtmlElementRecordKey(Guid.NewGuid())),
                _selectedCardinalDirectionKind,
                ActiveRowIndex,
                ActiveGridItemRecordIndex);

            Dispatcher.Dispatch(addGridItemRecordAction);
        }
    }

    private void OnCardinalDirectionKindSelectedEventCallback(CardinalDirectionKind cardinalDirectionKind)
    {
        _selectedCardinalDirectionKind = cardinalDirectionKind;
    }
}