using BlazorWindowManager.ClassLibrary.ConstructorAction;
using BlazorWindowManager.ClassLibrary.Grid;
using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public class GridReducer
{
    [ReducerMethod]
    public static GridRecordsState ReduceRegisterGridRecordTabContainerAction(GridRecordsState previousGridRecordsState,
        RegisterGridTabContainerRecordAction registerGridTabContainerRecordAction)
    {
        var nextGridRecordsState = new GridRecordsState(previousGridRecordsState,
            ConstructorActionKind.Add,
            registerGridTabContainerRecordAction.GridTabContainerRecord);

        return nextGridRecordsState;
    }
    
    [ReducerMethod]
    public static GridRecordsState ReduceCloseGridTabAction(GridRecordsState previousGridRecordsState,
        CloseGridTabAction closeGridTabAction)
    {
        var previousGridTabContainerRecord = previousGridRecordsState.LookupGridTabContainerRecord(closeGridTabAction.GridRecordKey);

        var nextGridTabContainerRecord = new GridTabContainerRecord(previousGridTabContainerRecord,
            Guid.NewGuid(),
            closeGridTabAction.GridTabRecordId);

        var nextGridRecordsState = new GridRecordsState(previousGridRecordsState,
            ConstructorActionKind.Replace,
            nextGridTabContainerRecord);

        return nextGridRecordsState;
    }
    
    [ReducerMethod]
    public static GridRecordsState ReduceAddGridTabAction(GridRecordsState previousGridRecordsState,
        AddGridTabAction addGridTabAction)
    {
        var previousGridTabContainerRecord = previousGridRecordsState.LookupGridTabContainerRecord(addGridTabAction.GridRecordKey);

        var nextGridTabContainerRecord = new GridTabContainerRecord(previousGridTabContainerRecord,
            previousGridTabContainerRecord.ActiveTabIndex ?? 0,
            Guid.NewGuid(),
            ConstructorActionKind.Add,
            addGridTabAction.GridTabRecord);

        var nextGridRecordsState = new GridRecordsState(previousGridRecordsState,
            ConstructorActionKind.Replace,
            nextGridTabContainerRecord);

        return nextGridRecordsState;
    }
    
    [ReducerMethod]
    public static GridRecordsState ReduceSetActiveGridTabAction(GridRecordsState previousGridRecordsState,
        SetActiveGridTabAction setActiveGridTabAction)
    {
        var previousGridTabContainerRecord = previousGridRecordsState.LookupGridTabContainerRecord(setActiveGridTabAction.GridRecordKey);

        var nextGridTabContainerRecord = new GridTabContainerRecord(previousGridTabContainerRecord,
            setActiveGridTabAction.ToBeActiveTabIndex,
            Guid.NewGuid());

        var nextGridRecordsState = new GridRecordsState(previousGridRecordsState,
            ConstructorActionKind.Replace,
            nextGridTabContainerRecord);

        return nextGridRecordsState;
    }
}
