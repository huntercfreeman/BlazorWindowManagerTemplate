using BlazorWindowManager.ClassLibrary.WindowManagerDialog;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Store.WindowManagerDialog;

[FeatureState]
public record WindowManagerDialogWrapperState
{
    public WindowManagerDialogWrapperState()
    {
        WindowManagerDialogRecordMap = new Dictionary<Guid, WindowManagerDialogRecord>();
    }
    
    public WindowManagerDialogWrapperState(WindowManagerDialogWrapperState otherWindowManagerDialogState)
    {
        WindowManagerDialogRecordMap = 
            new Dictionary<Guid, WindowManagerDialogRecord>(
                otherWindowManagerDialogState.WindowManagerDialogRecordMap);
    }

    public Dictionary<Guid, WindowManagerDialogRecord> WindowManagerDialogRecordMap { get; init; }
}
