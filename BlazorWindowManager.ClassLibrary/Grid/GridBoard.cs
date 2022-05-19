using BlazorWindowManager.ClassLibrary.Direction;
using BlazorWindowManager.ClassLibrary.ImmutableArrayExtensions;
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
        GridBoardSequence = Guid.NewGuid();
    }

    public GridBoard(GridBoard previousGridBoard, GridItemRecord gridItemRecord)
    {
        _gridItemRecords = new(previousGridBoard._gridItemRecords);

        if (!_gridItemRecords.Any())
            _gridItemRecords.Add(new());

        _gridItemRecords.First().Add(gridItemRecord);

        GridBoardSequence = Guid.NewGuid();
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

        GridBoardSequence = Guid.NewGuid();
    }

    public ImmutableArray<ImmutableArray<GridItemRecord>> GridItemRecords => BlazorWindowManagerImmutableArrayExtensions.ConvertToImmutable(_gridItemRecords);

    public Guid GridBoardSequence { get; }
}
