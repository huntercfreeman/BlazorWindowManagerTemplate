using BlazorWindowManager.ClassLibrary.WindowManagerDialog;
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
        ClearWindowManagerDialogStateAction clearWindowManagerDialogStateAction)
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
            nextWindowManagerDialogState.WindowManagerDialogRecordMap[persistWindowManagerDialogRecordStateAction.WindowManagerDialogRecord.WindowManagerDialogRecordId] = 
                persistWindowManagerDialogRecordStateAction.WindowManagerDialogRecord;
        }

        return nextWindowManagerDialogState;
    }

    [ReducerMethod]
    public static WindowManagerDialogWrapperState ReduceReplaceDialogDimensionsRecordAction(WindowManagerDialogWrapperState previousWindowManagerDialogWrapperState,
        ReplaceWindowManagerDialogRecordAction replaceWindowManagerDialogRecordAction)
    {
        var nextWindowManagerDialogWrapperState = new WindowManagerDialogWrapperState(previousWindowManagerDialogWrapperState);

        var nextWindowManagerDialogRecord = replaceWindowManagerDialogRecordAction.WindowManagerDialogRecord with
        {
            DimensionsRecord = replaceWindowManagerDialogRecordAction.ReplacementDimensionsRecord
        };

        nextWindowManagerDialogWrapperState.WindowManagerDialogRecordMap.Remove(replaceWindowManagerDialogRecordAction.WindowManagerDialogRecord.WindowManagerDialogRecordId);

        nextWindowManagerDialogWrapperState.WindowManagerDialogRecordMap.Add(nextWindowManagerDialogRecord.WindowManagerDialogRecordId,
            nextWindowManagerDialogRecord);

        return nextWindowManagerDialogWrapperState;
    }
}