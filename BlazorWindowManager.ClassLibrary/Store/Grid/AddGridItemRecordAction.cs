using BlazorWindowManager.ClassLibrary.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public record AddGridItemRecordAction(GridRecordKey GridRecordKey,
    GridItemRecord GridItemRecord);

