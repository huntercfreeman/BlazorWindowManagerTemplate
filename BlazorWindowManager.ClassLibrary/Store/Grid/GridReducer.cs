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
}
