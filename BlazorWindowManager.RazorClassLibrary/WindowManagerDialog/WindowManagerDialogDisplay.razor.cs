using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Store.WindowManagerDialog;
using BlazorWindowManager.ClassLibrary.WindowManagerDialog;
using BlazorWindowManager.RazorClassLibrary.Transformative;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorWindowManager.RazorClassLibrary.WindowManagerDialog;

public partial class WindowManagerDialogDisplay : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public WindowManagerDialogRecord WindowManagerDialogRecord { get; set; } = null!;

    private TransformativeDisplay _transformativeDisplay = null!;

    private void FireSubscribeToDragEventWithMoveHandle()
    {
        _transformativeDisplay.SubscribeToDragEventWithMoveHandle();
    }

    private void OnDimensionsRecordChangedEventCallback(DimensionsRecord replacementDimensionsRecord)
    {
        var action = new ReplaceWindowManagerDialogRecordAction(WindowManagerDialogRecord, replacementDimensionsRecord);

        Dispatcher.Dispatch(action);
    }

    private void MinimizeDialogOnClick()
    {
        var action = new SetIsMinimizedDialogAction(WindowManagerDialogRecord, true);

        Dispatcher.Dispatch(action);
    }

    private void CloseDialogOnClick()
    {
        var action = new RemoveWindowManagerDialogRecordAction(WindowManagerDialogRecord.WindowManagerDialogRecordId);

        Dispatcher.Dispatch(action);
    }
}