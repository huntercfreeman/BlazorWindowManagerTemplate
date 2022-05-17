using BlazorWindowManager.ClassLibrary.ConstructorAction;
using System.Collections.Immutable;

namespace BlazorWindowManager.ClassLibrary.Grid;

public record GridWindowRecord
{
    private readonly Dictionary<Guid, GridWindowTabRecord> _gridWindowTabRecordMap;

    public Guid GridWindowRecordId { get; } = Guid.NewGuid();

    public GridWindowRecord(string gridWindowDisplayName, Type renderedContentType)
    {
        GridWindowDisplayName = gridWindowDisplayName;
        RenderedContentType = renderedContentType;
    }

    public GridWindowRecord(GridWindowRecord otherGridWindowRecord, ConstructActionKind constructActionKind, params GridWindowTabRecord[] gridWindowTabRecords)
    {
        _gridWindowTabRecordMap = new Dictionary<Guid, GridWindowTabRecord>(otherGridWindowRecord._gridWindowTabRecordMap);

        foreach (var gridWindowTabRecord in gridWindowTabRecords)
        {
            switch (constructActionKind)
            {
                case ConstructActionKind.Add:
                    AddGridWindowTabRecord(gridWindowTabRecord);
                    break;
                case ConstructActionKind.Replace:
                    ReplaceGridWindowTabRecord(gridWindowTabRecord);
                    break;
                default:
                    throw new ApplicationException($"The {nameof(constructActionKind)} with value: " +
                        $"'{constructActionKind}' is not " +
                        $"currently supported for constructing a {nameof(GridRecord)}");
            }
        }
    }

    public GridWindowRecord(GridWindowRecord otherGridWindowRecord, params Guid[] gridWindowRecordIds)
    {
        _gridWindowTabRecordMap = new Dictionary<Guid, GridWindowTabRecord>(otherGridWindowRecord._gridWindowTabRecordMap);

        foreach (var gridWindowRecord in gridWindowRecordIds)
        {
            _gridWindowTabRecordMap.Remove(gridWindowRecord);
        }
    }

    private void AddGridWindowTabRecord(GridWindowTabRecord gridWindowTabRecord)
    {
        _gridWindowTabRecordMap.Add(gridWindowTabRecord.GridRecordId, gridWindowTabRecord);
    }

    private void ReplaceGridWindowTabRecord(GridWindowTabRecord gridWindowTabRecord)
    {
        _gridWindowTabRecordMap[gridWindowTabRecord.GridRecordId] = gridWindowTabRecord;
    }

    public ImmutableArray<GridWindowTabRecord> GridWindowTabRecords => _gridWindowTabRecordMap.Values
        .ToImmutableArray();

    public string GridWindowDisplayName { get; }
    public Type RenderedContentType { get; }

    public GridWindowTabRecord LookupGridWindowRecordById(Guid gridWindowTabRecordId) =>
        _gridWindowTabRecordMap[gridWindowTabRecordId];
}
