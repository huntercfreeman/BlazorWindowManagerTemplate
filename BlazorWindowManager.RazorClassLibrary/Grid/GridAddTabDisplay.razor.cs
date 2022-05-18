using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using Fluxor;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using BlazorWindowManager.ClassLibrary.Grid;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridAddTabDisplay : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [CascadingParameter, EditorRequired]
    public GridRecordKey GridRecordKey { get; set; } = null!;
    
    [Parameter, EditorRequired]
    public int NextAvailableTabIndex { get; set; }

    private void DispatchAddGridTabAction()
    {
        var addGridTabAction = new AddGridTabAction(GridRecordKey, new GridTabRecord(Guid.NewGuid(), 
            typeof(GridAddTabFormDisplay),
            "Empty"),
            NextAvailableTabIndex);

        Dispatcher.Dispatch(addGridTabAction);
    }
}