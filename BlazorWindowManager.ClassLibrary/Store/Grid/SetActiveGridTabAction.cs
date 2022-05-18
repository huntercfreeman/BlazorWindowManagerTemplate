using BlazorWindowManager.ClassLibrary.Grid;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public record SetActiveGridTabAction(GridRecordKey GridRecordKey, int ToBeActiveTabIndex);
