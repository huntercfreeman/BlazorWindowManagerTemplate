using BlazorWindowManager.ClassLibrary.ConstructorAction;
using BlazorWindowManager.ClassLibrary.Grid;
using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public class GridReducer
{
    [ReducerMethod]
    public static GridTabContainerRecordsState ReduceRegisterGridRecordTabContainerAction(GridTabContainerRecordsState previousGridRecordsState,
        RegisterGridTabContainerRecordAction registerGridTabContainerRecordAction)
    {
        var nextGridRecordsState = new GridTabContainerRecordsState(previousGridRecordsState,
            ConstructorActionKind.Add,
            registerGridTabContainerRecordAction.GridTabContainerRecord);

        return nextGridRecordsState;
    }
    
    [ReducerMethod]
    public static GridTabContainerRecordsState ReduceCloseGridTabAction(GridTabContainerRecordsState previousGridRecordsState,
        CloseGridTabAction closeGridTabAction)
    {
        var previousGridTabContainerRecord = previousGridRecordsState.LookupGridTabContainerRecord(closeGridTabAction.GridRecordKey);

        var nextGridTabContainerRecord = new GridTabContainerRecord(previousGridTabContainerRecord,
            Guid.NewGuid(),
            closeGridTabAction.GridTabRecordId);

        var nextGridRecordsState = new GridTabContainerRecordsState(previousGridRecordsState,
            ConstructorActionKind.Replace,
            nextGridTabContainerRecord);

        return nextGridRecordsState;
    }
    
    [ReducerMethod]
    public static GridTabContainerRecordsState ReduceAddGridTabAction(GridTabContainerRecordsState previousGridRecordsState,
        AddGridTabAction addGridTabAction)
    {
        var previousGridTabContainerRecord = previousGridRecordsState.LookupGridTabContainerRecord(addGridTabAction.GridRecordKey);

        var nextGridTabContainerRecord = new GridTabContainerRecord(previousGridTabContainerRecord,
           addGridTabAction.TabToSetAsActiveTab,
            Guid.NewGuid(),
            ConstructorActionKind.Add,
            addGridTabAction.GridTabRecord);

        var nextGridRecordsState = new GridTabContainerRecordsState(previousGridRecordsState,
            ConstructorActionKind.Replace,
            nextGridTabContainerRecord);

        return nextGridRecordsState;
    }
    
    [ReducerMethod]
    public static GridTabContainerRecordsState ReduceReplaceGridTabAction(GridTabContainerRecordsState previousGridRecordsState,
        ReplaceGridTabAction replaceGridTabAction)
    {
        var previousGridTabContainerRecord = previousGridRecordsState.LookupGridTabContainerRecord(replaceGridTabAction.GridRecordKey);

        var nextGridTabContainerRecord = new GridTabContainerRecord(previousGridTabContainerRecord,
           replaceGridTabAction.TabToSetAsActiveTab,
            Guid.NewGuid(),
            ConstructorActionKind.Replace,
            replaceGridTabAction.GridTabRecord);

        var nextGridRecordsState = new GridTabContainerRecordsState(previousGridRecordsState,
            ConstructorActionKind.Replace,
            nextGridTabContainerRecord);

        return nextGridRecordsState;
    }
    
    [ReducerMethod]
    public static GridTabContainerRecordsState ReduceSetActiveGridTabAction(GridTabContainerRecordsState previousGridRecordsState,
        SetActiveGridTabAction setActiveGridTabAction)
    {
        var previousGridTabContainerRecord = previousGridRecordsState.LookupGridTabContainerRecord(setActiveGridTabAction.GridRecordKey);

        var nextGridTabContainerRecord = new GridTabContainerRecord(previousGridTabContainerRecord,
            setActiveGridTabAction.ToBeActiveTabIndex,
            Guid.NewGuid());

        var nextGridRecordsState = new GridTabContainerRecordsState(previousGridRecordsState,
            ConstructorActionKind.Replace,
            nextGridTabContainerRecord);

        return nextGridRecordsState;
    }
}
