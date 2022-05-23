using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.TaskManager;

namespace BlazorWindowManager.RazorClassLibrary.TaskManager;

public partial class EnqueuedTaskDisplay
{
    [Parameter, EditorRequired]
    public EnqueuedTaskRecord EnqueuedTaskRecord { get; set; } = null!;
}