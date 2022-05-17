using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.Grid;
using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Store.WindowManagerDialog;
using BlazorWindowManager.ClassLibrary.WindowManagerDialog;
using Fluxor;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridDisplay : ComponentBase
{
    [Inject]
    private IViewportDimensionsService ViewportDimensionsService { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public GridRecord GridRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public DimensionsRecord InitialDimensionsRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public Type AddWindowToGridRenderedType { get; set; } = null!;
    [Parameter, EditorRequired]
    public Dictionary<string, object> AddWindowToGridRenderedTypeParameters { get; set; } = null!;
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

    private async Task OnAddWindowToGridOnClickAsync()
    {
        var dimensionsRecordForDialog = await WindowManagerDialogRecord.ConstructDefaultDimensionsRecord(ViewportDimensionsService);

        var windowManagerDialogRecord = new WindowManagerDialogRecord(Guid.NewGuid(),
            "Add Window To Grid",
            AddWindowToGridRenderedType,
            AddWindowToGridRenderedTypeParameters,
            dimensionsRecordForDialog);

        var action = new AddWindowManagerDialogRecordAction(windowManagerDialogRecord);

        Dispatcher.Dispatch(action);
    }
}