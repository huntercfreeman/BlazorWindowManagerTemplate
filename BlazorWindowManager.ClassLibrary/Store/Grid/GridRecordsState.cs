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
        ConstructorActionKind constructorActionKind,
        CardinalDirectionKind? cardinalDirectionKind,
        int? rowIndexRelativeTo,
        int? columnIndexRelativeTo)
    {
        _gridRecordItemContainerMap = new(otherGridRecordsState._gridRecordItemContainerMap);

        switch (constructorActionKind)
        {
            case ConstructorActionKind.Add:
                PerformAdd(gridRecordKey,
                    gridItemRecord,
                    constructorActionKind,
                    cardinalDirectionKind,
                    rowIndexRelativeTo,
                    columnIndexRelativeTo);
                break;
            case ConstructorActionKind.Replace:
                PerformReplace(gridRecordKey,
                    gridItemRecord,
                    constructorActionKind,
                    cardinalDirectionKind,
                    rowIndexRelativeTo,
                    columnIndexRelativeTo);
                break;
        }

        var nextGridBoard = new GridBoard(_gridRecordItemContainerMap[gridRecordKey],
            gridItemRecord,
            cardinalDirectionKind,
            rowIndexRelativeTo,
            columnIndexRelativeTo);

        _gridRecordItemContainerMap[gridRecordKey] = nextGridBoard;
    }

    private void PerformAdd(GridRecordKey gridRecordKey,
        GridItemRecord gridItemRecord,
        ConstructorActionKind constructorActionKind,
        CardinalDirectionKind? cardinalDirectionKind,
        int? rowIndexRelativeTo,
        int? columnIndexRelativeTo)
    {
        var gridBoard = new GridBoard();

        _gridRecordItemContainerMap.Add(gridRecordKey, gridBoard);
    }

    private void PerformReplace(GridRecordKey gridRecordKey,
        GridItemRecord gridItemRecord,
        ConstructorActionKind constructorActionKind,
        CardinalDirectionKind? cardinalDirectionKind,
        int? rowIndexRelativeTo,
        int? columnIndexRelativeTo)
    {
        throw new NotImplementedException();
    }

    public GridBoard LookupGridBoard(GridRecordKey gridRecordKey) =>
        _gridRecordItemContainerMap[gridRecordKey];
}
