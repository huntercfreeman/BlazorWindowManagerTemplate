using BlazorWindowManager.ClassLibrary.ConstructorAction;
using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public class GridReducer
{
    [ReducerMethod]
    public static GridRecordsState ReduceRegisterGridRecordAction(GridRecordsState previousGridRecordsState,
        RegisterGridRecordAction registerGridRecordAction)
    {
        return new GridRecordsState(previousGridRecordsState,
            registerGridRecordAction.GridRecordKey,
            registerGridRecordAction.GridItemRecord,
            registerGridRecordAction.CardinalDirectionKind,
            registerGridRecordAction.RowIndexRelativeTo,
            registerGridRecordAction.ColumnIndexRelativeTo);
    }
}
