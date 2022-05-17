using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.Grid;

namespace BlazorWindowManager.RazorClassLibrary.Grid.HelperComponents;

public partial class GridWindowDisplay : ComponentBase
{
    [Parameter]
    public GridWindowRecord GridWindowRecord { get; set; } = null!;

    private void CloseWindowTabOnClick()
    {

    }

    private void ShowContextMenuOnMouseDown()
    {

    }
}