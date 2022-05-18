using BlazorWindowManager.ClassLibrary.Grid;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public record ReplaceGridTabAction(GridRecordKey GridRecordKey, GridTabRecord GridTabRecord, int TabToSetAsActiveTab);
