using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using System.Collections.Immutable;
using BlazorWindowManager.ClassLibrary.TaskManager;

namespace BlazorWindowManager.RazorClassLibrary.TaskManager;

public partial class TaskManagerListDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public ImmutableArray<EnqueuedTaskRecord> EnqueuedTaskRecords { get; set; }
    [Parameter, EditorRequired]
    public string Class { get; set; } = null!;
}