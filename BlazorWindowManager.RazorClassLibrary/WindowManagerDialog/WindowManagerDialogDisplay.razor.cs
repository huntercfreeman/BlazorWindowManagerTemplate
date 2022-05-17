using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Html;
using BlazorWindowManager.ClassLibrary.Store.Html;
using BlazorWindowManager.ClassLibrary.Store.WindowManagerDialog;
using BlazorWindowManager.ClassLibrary.WindowManagerDialog;
using BlazorWindowManager.RazorClassLibrary.Transformative;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorWindowManager.RazorClassLibrary.WindowManagerDialog;

public partial class WindowManagerDialogDisplay : FluxorComponent
{
    [Inject]
    private IViewportDimensionsService ViewportDimensionsService { get; set; } = null!;
    [Inject]
    private IState<HtmlElementRecordsState> HtmlElementRecordsState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public WindowManagerDialogRecord WindowManagerDialogRecord { get; set; } = null!;
    [Parameter]
    public EventCallback<object> CompleteDialogInteractionEventCallback { get; set; }

    private TransformativeDisplay _transformativeDisplay = null!;
    private Guid? _previousHtmlElementSequence;
    private HtmlElementRecord? _cachedHtmlElementRecord;
    private int _renderCount;

    protected override async Task OnInitializedAsync()
    {
        var dimensionsRecordForDialog = await BlazorWindowManager.ClassLibrary.WindowManagerDialog.WindowManagerDialogRecord
            .ConstructDefaultDimensionsRecord(ViewportDimensionsService);
        
        var registerHtmlElemementAction = new RegisterHtmlElemementAction(WindowManagerDialogRecord.HtmlElementRecordKey,
            dimensionsRecordForDialog,
            new ZIndexRecord(1));

        Dispatcher.Dispatch(registerHtmlElemementAction);

        await base.OnInitializedAsync();
    }

    protected override bool ShouldRender()
    {
        bool shouldRender;

        try
        {
            _cachedHtmlElementRecord = HtmlElementRecordsState.Value
                .LookupHtmlElementRecord(WindowManagerDialogRecord.HtmlElementRecordKey);

            if (_previousHtmlElementSequence is null ||
                _previousHtmlElementSequence.Value != _cachedHtmlElementRecord.HtmlElementSequence)
            {
                shouldRender = true;
            }
            else
            {
                shouldRender = false;
            }

            _previousHtmlElementSequence = _cachedHtmlElementRecord.HtmlElementSequence;
        }
        catch (KeyNotFoundException)
        {
            shouldRender = false;
        }

        return shouldRender;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        _renderCount++;

        base.OnAfterRender(firstRender);
    }

    private void FireSubscribeToDragEventWithMoveHandle()
    {
        _transformativeDisplay.SubscribeToDragEventWithMoveHandle();
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