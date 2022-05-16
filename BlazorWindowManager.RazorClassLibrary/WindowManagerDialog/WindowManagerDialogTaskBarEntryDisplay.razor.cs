using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Button;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.WindowManagerDialog;
using BlazorWindowManager.ClassLibrary.Store.WindowManagerDialog;
using Fluxor;

namespace BlazorWindowManager.RazorClassLibrary.WindowManagerDialog;

public partial class WindowManagerDialogTaskBarEntryDisplay : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public WindowManagerDialogRecord WindowManagerDialogRecord { get; set; } = null!;

    private void ToggleIsMinizedDialogOnClick()
    {
        var action = new SetIsMinimizedDialogAction(WindowManagerDialogRecord, !WindowManagerDialogRecord.IsMinimized);

        Dispatcher.Dispatch(action);
    }

    private void CloseDialogOnClick()
    {
        var action = new RemoveWindowManagerDialogRecordAction(WindowManagerDialogRecord.WindowManagerDialogRecordId);

        Dispatcher.Dispatch(action);
    }
}