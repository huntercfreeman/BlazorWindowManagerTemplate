using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.Grid;
using BlazorWindowManager.ClassLibrary.Dimension;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public GridRecord GridRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public DimensionsRecord InitialDimensionsRecord { get; set; } = null!;
    [Parameter]
    public bool IsResizable { get; set; } = false;

    protected override void OnInitialized()
    {
        _dimensionsRecord = InitialDimensionsRecord;

        base.OnInitialized();
    }

    private DimensionsRecord _dimensionsRecord = null!;

    private void OnDimensionsRecordChangedEventCallback(DimensionsRecord dimensionsRecord)
    {
        _dimensionsRecord = dimensionsRecord;
    }
}