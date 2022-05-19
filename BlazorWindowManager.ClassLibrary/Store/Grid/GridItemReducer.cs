using BlazorWindowManager.ClassLibrary.Grid;
using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public class GridItemReducer
{
    [ReducerMethod]
    public static GridItemRecordsState ReduceAddGridTabRecordAction(GridItemRecordsState previousGridItemRecordsState,
        RegisterGridTabContainerRecordAction registerGridTabContainerRecordAction)
    {
        var nextGridItemRecordsState = new GridItemRecordsState(previousGridItemRecordsState,
            registerGridTabContainerRecordAction.GridItemRecordKey);

        return nextGridItemRecordsState;
    }
    
    [ReducerMethod]
    public static GridItemRecordsState ReduceAddGridTabRecordAction(GridItemRecordsState previousGridItemRecordsState,
        AddGridTabRecordAction addGridTabRecordAction)
    {
        var nextGridItemRecordsState = new GridItemRecordsState(previousGridItemRecordsState,
            addGridTabRecordAction.GridItemRecordKey,
            addGridTabRecordAction.GridTabRecord);

        return nextGridItemRecordsState;
    }
}
