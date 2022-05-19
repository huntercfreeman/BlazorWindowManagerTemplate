using System.Collections.Immutable;

namespace BlazorWindowManager.ClassLibrary.Grid;

public record GridTabContainerRecord
{
    private Dictionary<GridTabRecordKey, GridTabRecord> _gridTabRecordMap;

    public GridTabContainerRecord()
    {
        GridTabContainerRecordSequence = Guid.NewGuid();

        _gridTabRecordMap = new();
    }

    public GridTabContainerRecord(GridTabContainerRecord previousGridTabContainerRecord, 
        GridTabRecord gridTabRecord)
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
    }

    public Guid GridTabContainerRecordSequence { get; }
    public ImmutableArray<GridTabRecord> GridTabRecords => _gridTabRecordMap.Values
        .ToImmutableArray();
}
