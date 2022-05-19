using BlazorWindowManager.ClassLibrary.Grid;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridTabAddButtonDisplay : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [CascadingParameter]
    public GridItemRecordKey GridItemRecordKey { get; set; } = null!;
    
    [Parameter, EditorRequired]
    public int NextAvailableTabIndex { get; set; }

    private void DispatchAddGridTabAction()
    {
        var addGridTabAction = new AddGridTabRecordAction(GridItemRecordKey, new GridTabRecord(new GridTabRecordKey(Guid.NewGuid()), 
                typeof(GridTabAddFormDisplay),
                "Empty"),
            NextAvailableTabIndex);

        Dispatcher.Dispatch(addGridTabAction);
    }
}