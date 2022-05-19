using BlazorWindowManager.ClassLibrary.Direction;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Grid;

public record GridBoard
{
    private List<List<GridItemRecord>> _gridItemRecords;

    public GridBoard()
    {
        _gridItemRecords = new();
    }
    
    public GridBoard(GridBoard otherGridBoard, 
        GridItemRecord gridItemRecord,
        CardinalDirectionKind? cardinalDirectionKind,
        int? rowIndexRelativeTo,
        int? columnIndexRelativeTo)
    {
        _gridItemRecords = new(otherGridBoard._gridItemRecords);

        if(!_gridItemRecords.Any())
        {
            _gridItemRecords.Add(new List<GridItemRecord>());
        }

        _gridItemRecords.First().Add(gridItemRecord);
    }

    public ImmutableArray<ImmutableArray<GridItemRecord>> GridItemRecords => GetGridItemRecords();

    public Guid GridBoardSequence { get; set; }

    private ImmutableArray<ImmutableArray<GridItemRecord>> GetGridItemRecords()
    {
        List<ImmutableArray<GridItemRecord>> temporaryRows = new();

        foreach(var row in _gridItemRecords)
        {
            List<GridItemRecord> temporaryRow = new();

            foreach(var gridItemRecord in row)
            {
                temporaryRow.Add(gridItemRecord);
            }

            temporaryRows.Add(temporaryRow.ToImmutableArray());
        }

        return temporaryRows.ToImmutableArray();
    }
}
