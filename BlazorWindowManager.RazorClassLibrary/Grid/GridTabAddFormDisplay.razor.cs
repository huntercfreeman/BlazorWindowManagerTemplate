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
            // TODO: The contents of this else seem unsafe as you cannot await an event. Maybe Effects would help?
            
            var gridItemRecord = new GridItemRecord(new GridItemRecordKey(Guid.NewGuid()),
                new HtmlElementRecordKey(Guid.NewGuid()));

            var registerGridTabContainerRecordAction = 
                new RegisterGridTabContainerRecordAction(gridItemRecord.GridItemRecordKey);
            
            Dispatcher.Dispatch(registerGridTabContainerRecordAction);
            
            var addGridTabRecordAction = new AddGridTabRecordAction(gridItemRecord.GridItemRecordKey,
                new GridTabRecord(new GridTabRecordKey(Guid.NewGuid()), 
                    argumentTuple.renderedContentType, 
                    argumentTuple.renderedContentTabDisplayName),
                0);
            
            Dispatcher.Dispatch(addGridTabRecordAction);
            
            var addGridItemRecordAction = new AddGridItemRecordAction(GridRecordKey,
                gridItemRecord,
                _selectedCardinalDirectionKind,
                ActiveRowIndex,
                ActiveGridItemRecordIndex);

            Dispatcher.Dispatch(addGridItemRecordAction);
            
            if (ActiveGridTabId is not null)
            {
                var closeGridTabRecordAction = new CloseGridTabRecordAction(GridItemRecordKey,
                    new GridTabRecordKey(ActiveGridTabId.Value),
                    null
                );

                Dispatcher.Dispatch(closeGridTabRecordAction);
            }
        }
    }

    private void OnCardinalDirectionKindSelectedEventCallback(CardinalDirectionKind cardinalDirectionKind)
    {
        _selectedCardinalDirectionKind = cardinalDirectionKind;
    }
}