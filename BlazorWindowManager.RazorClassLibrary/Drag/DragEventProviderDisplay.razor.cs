using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Fluxor.Blazor.Web.Components;
using Fluxor;
using BlazorWindowManager.ClassLibrary.Store.Drag;

namespace BlazorWindowManager.RazorClassLibrary.Drag;

public partial class DragEventProviderDisplay : FluxorComponent
{
    [Inject]
    private IState<DragEventProviderState> DragEventProviderState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    private string IsActiveCssClass => DragEventProviderState.Value.OnDragEventSubscriptions.Any()
        ? "hfd_active"
        : string.Empty;

    private SemaphoreSlim _dragStateChangedSemaphoreSlim = new(1, 1);
    private Stack<MouseEventArgs> _dragStateChangedStack = new();
    private CancellationTokenSource _dragStateChangedCancellationTokenSource = new();
    private Task? _dragStateThrottlingTask;

    private async Task DispatchOnDragEventActionOnMouseMove(MouseEventArgs mouseEventArgs)
    {
        try
        {
            await _dragStateChangedSemaphoreSlim.WaitAsync();

            _dragStateChangedStack.Push(mouseEventArgs);
        }
        finally
        {
            _dragStateChangedSemaphoreSlim.Release();
        }

        if (_dragStateThrottlingTask is not null)
            await _dragStateThrottlingTask;

        try
        {
            await _dragStateChangedSemaphoreSlim.WaitAsync();

            if (_dragStateChangedStack.Any())
            {
                _dragStateChangedStack.Clear();

                var action = new DragEventAction(mouseEventArgs);

                Dispatcher.Dispatch(action);

                _dragStateThrottlingTask = Task.Run(async () =>
                {
                    await Task.Delay(25);
                }, _dragStateChangedCancellationTokenSource.Token);
            }
        }
        finally
        {
            _dragStateChangedSemaphoreSlim.Release();
        }
    }
    
    private void DispatchUnsubscribeActionOnMouseUp(MouseEventArgs mouseEventArgs)
    {
        var clearDragEventSubscriptionsAction = new ClearDragEventSubscriptionsAction();

        Dispatcher.Dispatch(clearDragEventSubscriptionsAction);

        var onDragEventAction = new DragEventAction(null);

        Dispatcher.Dispatch(onDragEventAction);
    }
}