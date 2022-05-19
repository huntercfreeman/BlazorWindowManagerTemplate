using System.Collections.Immutable;
using BlazorWindowManager.ClassLibrary.ConstructorAction;

namespace BlazorWindowManager.ClassLibrary.Grid;

public record GridTabContainerRecord
{
    private readonly Dictionary<GridTabRecordKey, GridTabRecord> _gridTabRecordMap;

    public GridTabContainerRecord()
    {
        GridTabContainerRecordSequence = Guid.NewGuid();

        _gridTabRecordMap = new();
    }

    public GridTabContainerRecord(GridTabContainerRecord previousGridTabContainerRecord, 
        GridTabRecord gridTabRecord,
        int? tabToSetAsActive,
        ConstructorActionKind constructorActionKind)
    {
        GridTabContainerRecordSequence = Guid.NewGuid();

        _gridTabRecordMap = new(previousGridTabContainerRecord._gridTabRecordMap);

        try
        {
            _gridTabRecordMap[gridTabRecord.GridTabRecordKey] = gridTabRecord;
        }
        catch (KeyNotFoundException)
        {
            _gridTabRecordMap.Add(gridTabRecord.GridTabRecordKey, gridTabRecord);
        }
        
        ActiveGridTabIndex = tabToSetAsActive ?? _gridTabRecordMap.Values.ToList().IndexOf(gridTabRecord);
    }
    
    public GridTabContainerRecord(GridTabContainerRecord previousGridTabContainerRecord, 
        GridTabRecordKey gridTabRecordKey,
        int? tabToSetAsActive,
        ConstructorActionKind constructorActionKind)
    {
        GridTabContainerRecordSequence = Guid.NewGuid();

        _gridTabRecordMap = new(previousGridTabContainerRecord._gridTabRecordMap);

        switch (constructorActionKind)
        {
            case ConstructorActionKind.Remove:
                _gridTabRecordMap.Remove(gridTabRecordKey);

                ActiveGridTabIndex = tabToSetAsActive ?? previousGridTabContainerRecord.ActiveGridTabIndex - 1;

                if (ActiveGridTabIndex < 0)
                {
                    if (_gridTabRecordMap.Any())
                    {
                        ActiveGridTabIndex = 0;
                    }
                    else
                    {
                        ActiveGridTabIndex = null;
                    }
                }

                break;
            case ConstructorActionKind.Replace:
                ActiveGridTabIndex = tabToSetAsActive ?? 0;
                break;
        }
    }

    public int? ActiveGridTabIndex { get; }
    public Guid GridTabContainerRecordSequence { get; }
    public ImmutableArray<GridTabRecord> GridTabRecords => _gridTabRecordMap.Values
        .ToImmutableArray();
}
