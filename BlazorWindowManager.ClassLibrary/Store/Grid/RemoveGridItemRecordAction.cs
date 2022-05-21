using BlazorWindowManager.ClassLibrary.Grid;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public record RemoveGridItemRecordAction(GridRecordKey GridRecordKey,
    int RowIndex,
    int GridItemIndex);