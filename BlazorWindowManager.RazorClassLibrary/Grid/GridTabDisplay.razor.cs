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
    public GridRecordKey GridRecordKey { get; set; } = null!;
    [CascadingParameter]
    public GridItemRecordKey GridItemRecordKey { get; set; } = null!;
    [CascadingParameter(Name="ActiveGridTabIndex")]
    public int? ActiveGridTabIndex { get; set; }
    [CascadingParameter(Name="RowIndex")]
    public int RowIndex { get; set; }
    [CascadingParameter(Name="ActiveGridItemRecordIndex")]
    public int ActiveGridItemRecordIndex { get; set; }
    [CascadingParameter(Name="TotalGridItemCountInRow")]
    public int TotalGridItemCountInRow { get; set; }
    
    [Parameter, EditorRequired]
    public GridTabRecord GridTabRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public int MyGridTabIndex { get; set; }
    [Parameter, EditorRequired]
    public int TotalCountOfGridTabs { get; set; }
    
    private void OnGridTabRecordChosenAction((Type renderedContentType, string renderedContentTabDisplayName) argumentTuple)
    {
        var addGridTabRecordAction = new AddGridTabRecordAction(GridItemRecordKey,
            new GridTabRecord(new GridTabRecordKey(Guid.NewGuid()), argumentTuple.renderedContentType,
                argumentTuple.renderedContentTabDisplayName),
            0);

        Dispatcher.Dispatch(addGridTabRecordAction);
    }
    
    private string IsActiveCssClass => ActiveGridTabIndex == MyGridTabIndex
        ? "bwmt_active"
        : string.Empty;

    private void DispatchCloseGridTabActionOnClick()
    {
        var closeGridTabAction = new CloseGridTabRecordAction(GridItemRecordKey, 
            GridTabRecord.GridTabRecordKey, 
            null);

        Dispatcher.Dispatch(closeGridTabAction);
        
        // Ensure the GridItem has active tabs
        if (TotalCountOfGridTabs == 1)
        {
            var removeGridItemRecordAction = new RemoveGridItemRecordAction(GridRecordKey,
                RowIndex,
                ActiveGridItemRecordIndex);

            Dispatcher.Dispatch(removeGridItemRecordAction);

            // Ensure the GridRow has GridItems remaining.
            if (TotalGridItemCountInRow == 1)
            {
                var removeGridRowRecordAction = new RemoveGridRowRecordAction(GridRecordKey,
                    RowIndex);

                Dispatcher.Dispatch(removeGridRowRecordAction);
            }
        }
    }
    
    private void DispatchSetActiveGridTabActionOnClick()
    {
        var setActiveGridTabAction = new SetActiveGridTabRecordAction(GridItemRecordKey, 
            GridTabRecord.GridTabRecordKey,
            MyGridTabIndex);

        Dispatcher.Dispatch(setActiveGridTabAction);
    }
}