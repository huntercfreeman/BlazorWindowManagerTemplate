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
        int? tabToSetAsActive)
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
        
        ActiveTabIndex = tabToSetAsActive ?? _gridTabRecordMap.Values.ToList().IndexOf(gridTabRecord);
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

                ActiveTabIndex = tabToSetAsActive ?? previousGridTabContainerRecord.ActiveTabIndex - 1;

                if (ActiveTabIndex < 0)
                {
                    if (_gridTabRecordMap.Any())
                    {
                        ActiveTabIndex = 0;
                    }
                    else
                    {
                        ActiveTabIndex = null;
                    }
                }

                break;
            case ConstructorActionKind.Replace:
                ActiveTabIndex = tabToSetAsActive ?? 0;
                break;
        }
    }

    public int? ActiveTabIndex { get; }
    public Guid GridTabContainerRecordSequence { get; }
    public ImmutableArray<GridTabRecord> GridTabRecords => _gridTabRecordMap.Values
        .ToImmutableArray();
}
