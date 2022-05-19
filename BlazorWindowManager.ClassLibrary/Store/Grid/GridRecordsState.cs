using BlazorWindowManager.ClassLibrary.ConstructorAction;
using BlazorWindowManager.ClassLibrary.Direction;
using BlazorWindowManager.ClassLibrary.Grid;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

[FeatureState]
public record GridRecordsState
{
    private Dictionary<GridRecordKey, GridBoard> _gridRecordItemContainerMap;

    public GridRecordsState()
    {
        _gridRecordItemContainerMap = new();
    }
    
    public GridRecordsState(GridRecordsState otherGridRecordsState,
        GridRecordKey gridRecordKey,
        GridItemRecord gridItemRecord,
        CardinalDirectionKind? cardinalDirectionKind,
        int? rowIndexRelativeTo,
        int? columnIndexRelativeTo)
    {
        _gridRecordItemContainerMap = new(otherGridRecordsState._gridRecordItemContainerMap);

        var nextGridBoard = new GridBoard(_gridRecordItemContainerMap[gridRecordKey],
            gridItemRecord,
            cardinalDirectionKind,
            rowIndexRelativeTo,
            columnIndexRelativeTo);

        _gridRecordItemContainerMap[gridRecordKey] = nextGridBoard;
    }

    public GridBoard LookupGridBoard(GridRecordKey gridRecordKey) =>
        _gridRecordItemContainerMap[gridRecordKey];
}
