using BlazorWindowManager.ClassLibrary.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public record GridChildComponentPersistedState(Dictionary<GridModel, object?> GridChildComponentPersistedStateMap)
{
    public GridChildComponentPersistedState() : this(new Dictionary<GridModel, object?>())
    {

    }
}
