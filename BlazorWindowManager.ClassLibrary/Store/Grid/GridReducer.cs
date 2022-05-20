using BlazorWindowManager.ClassLibrary.ConstructorAction;
using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public class GridReducer
{
    [ReducerMethod]
    public static GridRecordsState ReduceRegisterGridRecordAction(GridRecordsState previousGridRecordsState,
        RegisterGridRecordAction registerGridRecordAction)
    {
        return new GridRecordsState(previousGridRecordsState, registerGridRecordAction.GridRecordKey);
    }
    
    [ReducerMethod]
    public static GridRecordsState ReduceAddGridItemRecordAction(GridRecordsState previousGridRecordsState,
        AddGridItemRecordAction addGridItemRecordAction)
    {
        return new GridRecordsState(previousGridRecordsState,
            addGridItemRecordAction.GridRecordKey,
            addGridItemRecordAction.GridItemRecord,
            addGridItemRecordAction.CardinalDirectionKind,
            addGridItemRecordAction.RowIndex,
            addGridItemRecordAction.ActiveGridItemRecordIndex);
    }
}
