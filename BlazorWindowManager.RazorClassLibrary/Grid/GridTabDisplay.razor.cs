using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.Grid;
using System.Collections.Immutable;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridTabDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public GridTabRecord GridTabRecord { get; set; } = null!;
}