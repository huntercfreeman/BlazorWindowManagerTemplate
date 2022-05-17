using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.Dropdown;

namespace BlazorWindowManager.RazorClassLibrary.Dropdown;

public partial class DropdownGroupDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public DropdownGroupRecord DropdownGroupRecord { get; set; } = null!;
}