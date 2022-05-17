namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public record UnregisterGridWindowRecordAction(Guid GridRecordId, Guid GridWindowRecordId);


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