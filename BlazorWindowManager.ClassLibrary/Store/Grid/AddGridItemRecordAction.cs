using BlazorWindowManager.ClassLibrary.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorWindowManager.ClassLibrary.Direction;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public record AddGridItemRecordAction(GridRecordKey GridRecordKey,
    GridItemRecord GridItemRecord,
    CardinalDirectionKind CardinalDirectionKind,
    int? RowIndex,
    int? ActiveGridItemRecordIndex);

