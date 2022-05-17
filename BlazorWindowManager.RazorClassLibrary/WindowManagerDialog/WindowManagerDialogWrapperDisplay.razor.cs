using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorWindowManager.ClassLibrary.Store.Html;
using BlazorWindowManager.ClassLibrary.Store.WindowManagerDialog;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorWindowManager.RazorClassLibrary.WindowManagerDialog;

public partial class WindowManagerDialogWrapperDisplay : FluxorComponent
{
    [Inject]
    private IState<WindowManagerDialogWrapperState> WindowManagerDialogState { get; set; } = null!;
}