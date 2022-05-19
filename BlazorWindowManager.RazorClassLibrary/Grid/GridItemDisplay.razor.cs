using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.Grid;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridItemDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public GridItemRecord GridItemRecord { get; set; } = null!;
}