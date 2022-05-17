using BlazorWindowManager.ClassLibrary.ConstructorAction;
using BlazorWindowManager.ClassLibrary.Grid;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

[FeatureState]
public record GridStates
{
    private readonly Dictionary<Guid, GridRecord> _gridRecordMap;

    public GridStates()
    {
        _gridRecordMap = new Dictionary<Guid, GridRecord>();
    }
    
    public GridStates(GridStates otherGridStates, ConstructActionKind constructActionKind, params GridRecord[] gridRecords)
    {
        _gridRecordMap = new Dictionary<Guid, GridRecord>(otherGridStates._gridRecordMap);

        foreach(var gridRecord in gridRecords)
        {
            switch (constructActionKind)
            {
                case ConstructActionKind.Add:
                    AddGridRecord(gridRecord);
                    break;
                case ConstructActionKind.Replace:
                    ReplaceGridRecord(gridRecord);
                    break;
                default:
                    throw new ApplicationException($"The {nameof(constructActionKind)} with value: " +
                        $"'{constructActionKind}' is not " +
                        $"currently supported for constructing a {nameof(GridStates)}");
            }
        }
    }

    public GridStates(GridStates otherGridStates, params Guid[] toBeRemovedGridRecordIds)
    {
        _gridRecordMap = new Dictionary<Guid, GridRecord>(otherGridStates._gridRecordMap);

        foreach (var gridRecordId in toBeRemovedGridRecordIds)
        {
            _gridRecordMap.Remove(gridRecordId);
        }
    }

    public GridStates(GridStates otherGridStates, ConstructActionKind constructActionKind, Guid gridRecordId, params GridWindowRecord[] gridWindowRecords)
    {
        _gridRecordMap = new Dictionary<Guid, GridRecord>(otherGridStates._gridRecordMap);

        var nextGridRecord = new GridRecord(_gridRecordMap[gridRecordId], constructActionKind, gridWindowRecords);
    }

    private void AddGridRecord(GridRecord gridRecord)
    {
        _gridRecordMap.Add(gridRecord., gridRecord);
    }
    
    private void ReplaceGridRecord(GridRecord gridRecord)
    {
        try
        {
            _gridRecordMap[gridRecord.GridRecordId] = gridRecord;
        }
        catch (KeyNotFoundException)
        {
            // TODO: Should I just throw the exception in this case?
            _gridRecordMap.Add(gridRecord.GridRecordId, gridRecord);
        }
    }
    
    public ImmutableArray<GridRecord> GridRecords => _gridRecordMap.Values
        .ToImmutableArray();

    public GridRecord LookupGridRecordById(Guid gridRecordId)
    {
        return _gridRecordMap[gridRecordId];
    }
}


// DragEvents when dragging to resize the grid windows????

/*
 * # Side Comments
 *     []GridRecords when empty renders a UI to allow user to select a blazor 
 *         component type to be used to make a new GridWindowRecord
 *         (like typeof(Counter) where Counter.razor is a blazor component)
 *     
 *     []GridRecords ALWAYS display the "X" however, "X" will unregister the GridRecord
 *          if there are no GridWindows, otherwise, solely remove that active GridWindow.
 *     
 *     []GridWindow will display the position window up arrow right down arrow left arrow,
 *         and center button when dragging a different GridWindow
 *         allowing for a "DropZone" to move that GridWindow being Dragged.
 *         
 *     []GridWindowTabRecord dispatches Action to update active tab on the GridWindow
 *     
 *     []Perhaps allow user to set ShouldRender => PredicateParameter / ShouldRender => OnlyInitially
 *         as when dragging the GridWindow it will cause the inner component to constantly rerender.
 */