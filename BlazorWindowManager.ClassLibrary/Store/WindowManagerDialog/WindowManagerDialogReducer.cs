using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.WindowManagerDialog;

public class WindowManagerDialogReducer
{
    [ReducerMethod]
    public WindowManagerDialogWrapperState ReduceAddWindowManagerDialogRecordAction(WindowManagerDialogWrapperState previousWindowDialogManagerState,
        AddWindowManagerDialogRecordAction addWindowManagerDialogRecordAction)
    {
        var nextWindowManagerDialogState = new WindowManagerDialogWrapperState(previousWindowDialogManagerState);

        nextWindowManagerDialogState.WindowManagerDialogRecordMap
            .Add(addWindowManagerDialogRecordAction.WindowManagerDialogRecord.WindowManagerDialogRecordId,
                    addWindowManagerDialogRecordAction.WindowManagerDialogRecord);

        return nextWindowManagerDialogState;
    }
    
    [ReducerMethod]
    public WindowManagerDialogWrapperState ReduceRemoveWindowManagerDialogRecordAction(WindowManagerDialogWrapperState previousWindowDialogManagerState,
        RemoveWindowManagerDialogRecordAction removeWindowManagerDialogRecordAction)
    {
        var nextWindowManagerDialogState = new WindowManagerDialogWrapperState(previousWindowDialogManagerState);

        nextWindowManagerDialogState.WindowManagerDialogRecordMap
            .Remove(removeWindowManagerDialogRecordAction.WindowManagerDialogRecordId);

        return nextWindowManagerDialogState;
    }
    
    [ReducerMethod]
    public WindowManagerDialogWrapperState ReduceClearWindowManagerDialogStateAction(WindowManagerDialogWrapperState previousWindowDialogManagerState,
        RemoveWindowManagerDialogRecordAction removeWindowManagerDialogRecordAction)
    {
        var nextWindowManagerDialogState = new WindowManagerDialogWrapperState();

        return nextWindowManagerDialogState;
    }
    
    [ReducerMethod]
    public WindowManagerDialogWrapperState ReducePersistWindowManagerDialogRecordStateAction(WindowManagerDialogWrapperState previousWindowDialogManagerState,
        PersistWindowManagerDialogRecordStateAction persistWindowManagerDialogRecordStateAction)
    {
        var nextWindowManagerDialogState = new WindowManagerDialogWrapperState(previousWindowDialogManagerState);

        if(!nextWindowManagerDialogState.WindowManagerDialogRecordMap
            .TryAdd(persistWindowManagerDialogRecordStateAction.WindowManagerDialogRecord.WindowManagerDialogRecordId,
                persistWindowManagerDialogRecordStateAction.WindowManagerDialogRecord))
        {
            nextWindowManagerDialogState.WindowManagerDialogRecordMap.Add(
                persistWindowManagerDialogRecordStateAction.WindowManagerDialogRecord.WindowManagerDialogRecordId,
                persistWindowManagerDialogRecordStateAction.WindowManagerDialogRecord);
        }

        return nextWindowManagerDialogState;
    }
}