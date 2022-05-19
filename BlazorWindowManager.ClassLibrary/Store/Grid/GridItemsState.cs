using BlazorWindowManager.ClassLibrary.Grid;
using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

[FeatureState]
public record GridItemsState
{
    private Dictionary<GridItemRecordKey, GridTabContainerRecord> _gridTabContainerRecordMap;

    public GridItemsState()
    {
        _gridTabContainerRecordMap = new();
    }

    public GridItemsState(GridItemsState otherGridItemsState,
        GridItemRecordKey gridItemRecordKey,
        GridTabRecord gridTabRecord)
    {
        _gridTabContainerRecordMap = new(otherGridItemsState._gridTabContainerRecordMap);

        var previousGridTabContainerRecord = _gridTabContainerRecordMap[gridItemRecordKey];

        var nextGridTabContainerRecord = new GridTabContainerRecord(previousGridTabContainerRecord,
            gridTabRecord);

        _gridTabContainerRecordMap[gridItemRecordKey] = nextGridTabContainerRecord;
    }
}
