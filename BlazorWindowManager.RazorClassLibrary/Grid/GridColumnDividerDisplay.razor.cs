using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Button;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.Element;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridColumnDividerDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public GridModel LeftElementModel { get; set; } = null!;
    [Parameter, EditorRequired]
    public GridModel RightElementModel { get; set; } = null!;
    [Parameter, EditorRequired]
    public EventCallback<(GridModel LeftElementModel, GridModel RightElementModel)> OnMouseDownEventCallback { get; set; }

    private void FireOnMouseDownEventCallback()
    {
        if (OnMouseDownEventCallback.HasDelegate)
            OnMouseDownEventCallback.InvokeAsync((LeftElementModel, RightElementModel));
    }
}