using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.Grid;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using Fluxor;
using Dispatcher = Microsoft.AspNetCore.Components.Dispatcher;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridTabDisplay : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [CascadingParameter]
    public GridItemRecordKey GridItemRecordKey { get; set; } = null!;
    
    [Parameter, EditorRequired]
    public GridTabRecord GridTabRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public int MyTabIndex { get; set; }
    [Parameter, EditorRequired]
    public int ActiveTabIndex { get; set; }
    
    private void OnGridTabRecordChosenAction((Type renderedContentType, string renderedContentTabDisplayName) argumentTuple)
    {
        var addGridTabRecordAction = new AddGridTabRecordAction(GridItemRecordKey,
            new GridTabRecord(new GridTabRecordKey(Guid.NewGuid()), argumentTuple.renderedContentType,
                argumentTuple.renderedContentTabDisplayName),
            0);

        Dispatcher.Dispatch(addGridTabRecordAction);
    }
    
    private string IsActiveCssClass => ActiveTabIndex == MyTabIndex
        ? "bwmt_active"
        : string.Empty;

    private void DispatchCloseGridTabActionOnClick()
    {
        var closeGridTabAction = new CloseGridTabRecordAction(GridItemRecordKey, 
            GridTabRecord.GridTabRecordKey, 
            null);

        Dispatcher.Dispatch(closeGridTabAction);
    }
    
    private void DispatchSetActiveGridTabActionOnClick()
    {
        var setActiveGridTabAction = new SetActiveGridTabRecordAction(GridItemRecordKey, 
            GridTabRecord.GridTabRecordKey,
            MyTabIndex);

        Dispatcher.Dispatch(setActiveGridTabAction);
    }
}