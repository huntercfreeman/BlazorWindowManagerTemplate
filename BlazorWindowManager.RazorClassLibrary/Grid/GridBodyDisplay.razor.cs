using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridBodyDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public Type RenderedContentType { get; set; } = null!;
}