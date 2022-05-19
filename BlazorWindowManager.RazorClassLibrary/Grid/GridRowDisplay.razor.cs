using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using System.Collections.Immutable;
using BlazorWindowManager.ClassLibrary.Grid;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridRowDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public ImmutableArray<GridItemRecord> Row { get; set; }
}