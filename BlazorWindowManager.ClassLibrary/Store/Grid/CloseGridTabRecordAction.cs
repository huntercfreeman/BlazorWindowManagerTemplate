using BlazorWindowManager.ClassLibrary.Grid;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public record CloseGridTabRecordAction(GridItemRecordKey GridItemRecordKey,
    GridTabRecordKey GridTabRecordKey,
    int? TabToSetAsActive);