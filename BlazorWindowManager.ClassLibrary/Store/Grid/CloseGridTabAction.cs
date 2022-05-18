using BlazorWindowManager.ClassLibrary.Grid;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public record CloseGridTabAction(GridRecordKey GridRecordKey, Guid GridTabRecordId, int ToBeClosedTabIndex);
