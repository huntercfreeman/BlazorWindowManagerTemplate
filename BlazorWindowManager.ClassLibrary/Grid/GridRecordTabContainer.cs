using System.Collections.Immutable;

namespace BlazorWindowManager.ClassLibrary.Grid;

public record GridTabContainerRecord(GridRecordKey GridRecordKey, 
    int ActiveTabIndex, 
    ImmutableArray<GridTabRecord> GridTabRecords,
    Guid GridTabContainerSequence);
