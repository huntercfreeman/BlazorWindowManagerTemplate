using BlazorWindowManager.ClassLibrary.Grid;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public record RemoveGridRowRecordAction(GridRecordKey GridRecordKey,
    int RowIndex);