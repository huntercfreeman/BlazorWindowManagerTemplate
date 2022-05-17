using System.Collections.Immutable;

namespace BlazorWindowManager.ClassLibrary.Grid;

public record GridTabContainerRecord(GridRecordKey GridRecordKey, 
    int ActiveTab, 
    ImmutableArray<GridTabRecord> GridTabRecords,
    Guid HtmlElementSequence);
