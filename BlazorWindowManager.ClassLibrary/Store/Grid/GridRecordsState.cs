using BlazorWindowManager.ClassLibrary.ConstructorAction;
using BlazorWindowManager.ClassLibrary.Grid;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

[FeatureState]
public record GridTabContainerRecordsState
{
    private Dictionary<GridRecordKey, GridTabContainerRecord> _gridTabContainerRecordMap;

    public GridTabContainerRecordsState()
    {
        _gridTabContainerRecordMap = new();
    }

    public GridTabContainerRecordsState(GridTabContainerRecordsState otherGridRecordsState,
        ConstructorActionKind constructorActionKind,
        GridTabContainerRecord gridTabContainerRecord)
    {
        _gridTabContainerRecordMap = new(otherGridRecordsState._gridTabContainerRecordMap);

        switch (constructorActionKind)
        {
            case ConstructorActionKind.Add:
                PerformAdd(gridTabContainerRecord);
                break;
            case ConstructorActionKind.Replace:
                PerformReplace(gridTabContainerRecord);
                break;
        }
    }

    private void PerformAdd(GridTabContainerRecord gridTabContainerRecord)
    {
        _gridTabContainerRecordMap.Add(gridTabContainerRecord.GridRecordKey, gridTabContainerRecord);
    }

    public ImmutableArray<GridTabContainerRecord> HtmlElementRecords => _gridTabContainerRecordMap.Values
        .ToImmutableArray();

    private void PerformReplace(GridTabContainerRecord gridTabContainerRecord)
    {
        _gridTabContainerRecordMap[gridTabContainerRecord.GridRecordKey] = gridTabContainerRecord;
    }

    public GridTabContainerRecord LookupGridTabContainerRecord(GridRecordKey gridRecordKey)
    {
        return _gridTabContainerRecordMap[gridRecordKey];
    }
}

[FeatureState]
public record GridRecordsState
{
    private Dictionary<GridRecordKey, GridTabContainerRecord> _gridTabContainerRecordMap;

    public GridRecordsState()
    {
        _gridTabContainerRecordMap = new();
    }

    public GridRecordsState(GridTabContainerRecordsState otherGridRecordsState,
        ConstructorActionKind constructorActionKind,
        GridTabContainerRecord gridTabContainerRecord)
    {
        _gridTabContainerRecordMap = new(otherGridRecordsState._gridTabContainerRecordMap);

        switch (constructorActionKind)
        {
            case ConstructorActionKind.Add:
                PerformAdd(gridTabContainerRecord);
                break;
            case ConstructorActionKind.Replace:
                PerformReplace(gridTabContainerRecord);
                break;
        }
    }

    private void PerformAdd(GridTabContainerRecord gridTabContainerRecord)
    {
        _gridTabContainerRecordMap.Add(gridTabContainerRecord.GridRecordKey, gridTabContainerRecord);
    }

    public ImmutableArray<GridTabContainerRecord> HtmlElementRecords => _gridTabContainerRecordMap.Values
        .ToImmutableArray();

    private void PerformReplace(GridTabContainerRecord gridTabContainerRecord)
    {
        _gridTabContainerRecordMap[gridTabContainerRecord.GridRecordKey] = gridTabContainerRecord;
    }

    public GridTabContainerRecord LookupGridTabContainerRecord(GridRecordKey gridRecordKey)
    {
        return _gridTabContainerRecordMap[gridRecordKey];
    }
}
