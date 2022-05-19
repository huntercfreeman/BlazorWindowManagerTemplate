﻿using BlazorWindowManager.ClassLibrary.Grid;
using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

[FeatureState]
public record GridItemRecordsState
{
    private Dictionary<GridItemRecordKey, GridTabContainerRecord> _gridTabContainerRecordMap;

    public GridItemRecordsState()
    {
        _gridTabContainerRecordMap = new();
    }

    public GridItemRecordsState(GridItemRecordsState otherGridItemRecordsState, GridItemRecordKey gridItemRecordKey)
    {
        _gridTabContainerRecordMap = new(otherGridItemRecordsState._gridTabContainerRecordMap);

        _gridTabContainerRecordMap.Add(gridItemRecordKey, new());
    }

    public GridItemRecordsState(GridItemRecordsState otherGridItemsState,
        GridItemRecordKey gridItemRecordKey,
        GridTabRecord gridTabRecord)
    {
        _gridTabContainerRecordMap = new(otherGridItemsState._gridTabContainerRecordMap);

        GridTabContainerRecord previousGridTabContainerRecord;

        if(!_gridTabContainerRecordMap.TryGetValue(gridItemRecordKey, out previousGridTabContainerRecord!))
        {
            previousGridTabContainerRecord = new();
            _gridTabContainerRecordMap.Add(gridItemRecordKey, previousGridTabContainerRecord);
        }

        var nextGridTabContainerRecord = new GridTabContainerRecord(previousGridTabContainerRecord,
            gridTabRecord);

        _gridTabContainerRecordMap[gridItemRecordKey] = nextGridTabContainerRecord;
    }

    public GridTabContainerRecord LookupGridTabContainer(GridItemRecordKey gridItemRecordKey) =>
        _gridTabContainerRecordMap[gridItemRecordKey];
}
