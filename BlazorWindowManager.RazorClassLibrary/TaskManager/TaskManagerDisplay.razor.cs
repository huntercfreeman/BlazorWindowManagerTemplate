using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using Fluxor.Blazor.Web.Components;
using Fluxor;
using BlazorWindowManager.ClassLibrary.Store.TaskManager;

namespace BlazorWindowManager.RazorClassLibrary.TaskManager;

public partial class TaskManagerDisplay : FluxorComponent
{
    [Inject]
    private IState<TaskManagerState> TaskManagerState { get; set; } = null!;
}