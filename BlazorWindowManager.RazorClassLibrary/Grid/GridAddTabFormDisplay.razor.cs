using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using BlazorWindowManager.ClassLibrary.Grid;
using Fluxor;
using BlazorWindowManager.ClassLibrary.Direction;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridAddTabFormDisplay : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [CascadingParameter, EditorRequired]
    public RenderFragment EmptyGridTabContainerRenderFragment { get; set; } = null!;
    [CascadingParameter, EditorRequired]
    public Guid? ActiveGridTabId { get; set; } = null!;
    [CascadingParameter, EditorRequired]
    public int? ActiveGridTabIndex { get; set; } = null!;
    [CascadingParameter, EditorRequired]
    public GridRecordKey GridRecordKey { get; set; } = null!;

    private CardinalDirectionKind _selectedCardinalDirectionKind = CardinalDirectionKind.CurrentPosition;

    private void OnTypeToRenderSelectedAction((Type renderedContentType, string renderedContentTabDisplayName) argumentTuple)
    {
        var guidId = ActiveGridTabId ?? Guid.NewGuid();

        if(_selectedCardinalDirectionKind == CardinalDirectionKind.CurrentPosition)
        {
            var replaceGridTabAction = new ReplaceGridTabAction(GridRecordKey,
                new GridTabRecord(guidId, argumentTuple.renderedContentType, argumentTuple.renderedContentTabDisplayName),
                ActiveGridTabIndex ?? 0);

            Dispatcher.Dispatch(replaceGridTabAction);
        }
        else
        {
            var replaceGridTabAction = new ReplaceGridTabAction(GridRecordKey,
                new GridTabRecord(guidId, argumentTuple.renderedContentType, argumentTuple.renderedContentTabDisplayName),
                ActiveGridTabIndex ?? 0);

            Dispatcher.Dispatch(replaceGridTabAction);
        }
    }

    private void OnCardinalDirectionKindSelectedEventCallback(CardinalDirectionKind cardinalDirectionKind)
    {
        _selectedCardinalDirectionKind = cardinalDirectionKind;
    }
}