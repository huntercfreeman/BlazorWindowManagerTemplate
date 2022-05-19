using BlazorWindowManager.ClassLibrary.Grid;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public record AddGridTabRecordAction(GridItemRecordKey GridItemRecordKey,
    GridTabRecord GridTabRecord,
    int TabToSetAsActive);