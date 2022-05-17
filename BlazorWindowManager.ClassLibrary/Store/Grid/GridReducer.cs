using BlazorWindowManager.ClassLibrary.ConstructorAction;
using BlazorWindowManager.ClassLibrary.Grid;
using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public class GridReducer
{
    [ReducerMethod]
    public static GridStates ReduceRegisterGridRecordAction(GridStates previousGridStates,
        RegisterGridRecordAction registerGridRecordAction)
    {
        return new GridStates(previousGridStates, ConstructActionKind.Add, registerGridRecordAction.GridRecord);
    }

    [ReducerMethod]
    public static GridStates ReduceUnregisterGridRecordAction(GridStates previousGridStates,
        UnregisterGridRecordAction unregisterGridRecordAction)
    {
        return new GridStates(previousGridStates, unregisterGridRecordAction.GridRecordId);
    }

    [ReducerMethod]
    public static GridStates ReduceRegisterGridWindowRecordAction(GridStates previousGridStates,
        RegisterGridWindowRecordAction registerGridWindowRecordAction)
    {
        var gridWindowRecord = previousGridStates.LookupGridRecordById(registerGridWindowRecordAction.GridRecordId);

        gridWindowRecord.GridWindowRecords.First().Add(registerGridWindowRecordAction.GridWindowRecord);

        return previousGridStates;
    }

    [ReducerMethod]
    public static GridStates ReduceUnregisterGridWindowRecordAction(GridStates previousGridStates,
        UnregisterGridWindowRecordAction unregisterGridWindowRecordAction)
    {

        var previousGridRecordFromId = previousGridStates
            .LookupGridRecordById(unregisterGridWindowRecordAction.GridWindowRecordId);

        var nextGridRecord = new GridRecord(previousGridRecordFromId,
            unregisterGridWindowRecordAction.GridWindowRecordId);

        return new GridStates(previousGridStates, ConstructActionKind.Replace, nextGridRecord);
    }

    [ReducerMethod]
    public static GridStates ReduceRegisterGridWindowTabRecordAction(GridStates previousGridStates,
        RegisterGridWindowTabRecordAction registerGridWindowTabRecordAction)
    {
        var previousGridRecord = previousGridStates
            .LookupGridRecordById(registerGridWindowTabRecordAction.GridRecordId);

        var previousGridWindowRecord = previousGridRecord
            .FindGridWindowRecordById(registerGridWindowTabRecordAction.GridWindowRecordId);

        var nextGridWindowRecord = new GridWindowRecord(previousGridWindowRecord,
            ConstructActionKind.Add,
            registerGridWindowTabRecordAction.GridWindowTabRecord);

        var nextGridRecord = new GridRecord(previousGridRecord, 
            ConstructActionKind.Replace, 
            nextGridWindowRecord);

        return new GridStates(previousGridStates, ConstructActionKind.Replace, nextGridRecord);
    }

    [ReducerMethod]
    public static GridStates ReduceUnregisterGridWindowTabRecordAction(GridStates previousGridStates,
        UnregisterGridWindowTabRecordAction unregisterGridWindowTabRecordAction)
    {
        var previousGridRecord = previousGridStates
            .LookupGridRecordById(unregisterGridWindowTabRecordAction.GridRecordId);

        var previousGridWindowRecord = previousGridRecord
            .FindGridWindowRecordById(unregisterGridWindowTabRecordAction.GridWindowRecordId);

        var nextGridWindowRecord = new GridWindowRecord(previousGridWindowRecord,
            unregisterGridWindowTabRecordAction.GridWindowRecordTabId);

        var nextGridRecord = new GridRecord(previousGridRecord,
            ConstructActionKind.Replace,
            nextGridWindowRecord);

        return new GridStates(previousGridStates, ConstructActionKind.Replace, nextGridRecord);
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