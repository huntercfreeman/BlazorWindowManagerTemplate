namespace BlazorWindowManager.ClassLibrary.Grid;

public record GridTabContainerRecord
{
    private Dictionary<GridTabRecordKey, GridTabRecord> _gridTabRecordMap;

    public GridTabContainerRecord()
    {
        _gridTabRecordMap = new();
    }

    public GridTabContainerRecord(GridTabContainerRecord previousGridTabContainerRecord, 
        GridTabRecord gridTabRecord)
    {
        GridTabContainerRecordSequence = new();

        _gridTabRecordMap = new(previousGridTabContainerRecord._gridTabRecordMap);

        _gridTabRecordMap.Add(gridTabRecord.GridTabRecordKey, gridTabRecord);
    }

    public Guid GridTabContainerRecordSequence { get; }
}
