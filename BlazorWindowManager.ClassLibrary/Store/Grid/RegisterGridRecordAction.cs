using BlazorWindowManager.ClassLibrary.Direction;
using BlazorWindowManager.ClassLibrary.Grid;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public record RegisterGridRecordAction(GridRecordKey GridRecordKey, 
    GridItemRecord? GridItemRecord,
    CardinalDirectionKind? CardinalDirectionKind,
    int? RowIndexRelativeTo,
    int? ColumnIndexRelativeTo);
