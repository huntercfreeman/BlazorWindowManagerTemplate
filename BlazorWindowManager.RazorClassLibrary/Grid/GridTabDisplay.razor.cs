using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.Grid;
using System.Collections.Immutable;
using Fluxor;
using BlazorWindowManager.ClassLibrary.Store.Grid;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridTabDisplay : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public GridRecordKey GridRecordKey { get; set; } = null!;
    [Parameter, EditorRequired]
    public GridTabRecord GridTabRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public int ActiveTabIndex { get; set; }
    [Parameter, EditorRequired]
    public int MyTabIndex { get; set; }

    private string IsActiveCssClass => ActiveTabIndex == MyTabIndex
        ? "bwmt_active"
        : string.Empty;

    private void DispatchCloseGridTabActionOnClick()
    {
        var closeGridTabAction = new CloseGridTabAction(GridRecordKey, GridTabRecord.GridTabRecordId, MyTabIndex);

        Dispatcher.Dispatch(closeGridTabAction);
    }
}