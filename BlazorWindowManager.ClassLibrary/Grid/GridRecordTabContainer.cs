using BlazorWindowManager.ClassLibrary.ConstructorAction;
using System.Collections.Immutable;

namespace BlazorWindowManager.ClassLibrary.Grid;

public record GridTabContainerRecord
{
    private Dictionary<Guid, GridTabRecord> _gridTabRecordMap;

    public GridTabContainerRecord(GridRecordKey gridRecordKey,
        int activeTabIndex,
        Guid gridTabContainerSequence, 
        params GridTabRecord[] gridTabRecords)
    {
        _gridTabRecordMap = new();

        GridRecordKey = gridRecordKey;
        ActiveTabIndex = activeTabIndex;
        GridTabContainerSequence = gridTabContainerSequence;

        foreach (var gridTabRecord in gridTabRecords)
        {
            _gridTabRecordMap.Add(gridTabRecord.GridTabRecordId, gridTabRecord);
        }
    }
    
    public GridTabContainerRecord(GridTabContainerRecord otherGridTabContainerRecord,
        int toBeActiveTabIndex,
        Guid gridTabContainerSequence)
    {
        _gridTabRecordMap = new Dictionary<Guid, GridTabRecord>(otherGridTabContainerRecord._gridTabRecordMap);

        GridRecordKey = otherGridTabContainerRecord.GridRecordKey;
        ActiveTabIndex = toBeActiveTabIndex;
        GridTabContainerSequence = gridTabContainerSequence;
    }

    public GridTabContainerRecord(GridTabContainerRecord otherGridTabContainerRecord,
        int activeTabIndex,
        Guid gridTabContainerSequence,
        ConstructorActionKind constructorActionKind,
        params GridTabRecord[] gridTabRecords)
    {
        _gridTabRecordMap = new Dictionary<Guid, GridTabRecord>(otherGridTabContainerRecord._gridTabRecordMap);

        GridRecordKey = otherGridTabContainerRecord.GridRecordKey;
        ActiveTabIndex = activeTabIndex;
        GridTabContainerSequence = gridTabContainerSequence;

        foreach (var gridTabRecord in gridTabRecords)
        {
            switch(constructorActionKind)
            {
                case ConstructorActionKind.Add:
                    PerformAdd(gridTabRecord);
                    break;
                case ConstructorActionKind.Replace:
                    PerformReplace(gridTabRecord);
                    break;
            }
        }
    }
    
    public GridTabContainerRecord(GridTabContainerRecord otherGridTabContainerRecord,
        Guid gridTabContainerSequence,
        params Guid[] gridTabRecordIds)
    {
        _gridTabRecordMap = new Dictionary<Guid, GridTabRecord>(otherGridTabContainerRecord._gridTabRecordMap);

        int nextActiveTabIndex = otherGridTabContainerRecord.ActiveTabIndex;

        if (nextActiveTabIndex == otherGridTabContainerRecord.GridTabRecords.Length - 1)
            nextActiveTabIndex--; 
        
        GridRecordKey = otherGridTabContainerRecord.GridRecordKey;
        ActiveTabIndex = nextActiveTabIndex;
        GridTabContainerSequence = gridTabContainerSequence;

        foreach (var gridTabRecordId in gridTabRecordIds)
        {
            _gridTabRecordMap.Remove(gridTabRecordId);
        }
    }
    
    private void PerformAdd(GridTabRecord gridTabRecord)
    {
        _gridTabRecordMap.Add(gridTabRecord.GridTabRecordId, gridTabRecord);
    }

    private void PerformReplace(GridTabRecord gridTabRecord)
    {
        _gridTabRecordMap[gridTabRecord.GridTabRecordId] = gridTabRecord;
    }

    public ImmutableArray<GridTabRecord> GridTabRecords => _gridTabRecordMap.Values
        .ToImmutableArray();

    public GridRecordKey GridRecordKey { get; }
    public int ActiveTabIndex { get; }
    public Guid GridTabContainerSequence { get; }
}
