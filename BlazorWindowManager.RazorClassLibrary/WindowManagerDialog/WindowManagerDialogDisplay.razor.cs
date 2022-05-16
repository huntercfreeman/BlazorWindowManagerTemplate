using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWindowManager.ClassLibrary.WindowManagerDialog;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorWindowManager.RazorClassLibrary.WindowManagerDialog;

public partial class WindowManagerDialogDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public WindowManagerDialogRecord WindowManagerDialogRecord { get; set; } = null!;
}