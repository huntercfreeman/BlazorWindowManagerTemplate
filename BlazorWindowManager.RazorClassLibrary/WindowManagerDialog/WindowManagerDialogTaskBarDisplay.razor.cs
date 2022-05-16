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
using Fluxor.Blazor.Web.Components;

namespace BlazorWindowManager.RazorClassLibrary.WindowManagerDialog;

public partial class WindowManagerDialogTaskBarDisplay : FluxorComponent
{
    [Inject]
    private IState<WindowManagerDialogWrapperState> WindowManagerDialogWrapperState { get; set; } = null!;
}