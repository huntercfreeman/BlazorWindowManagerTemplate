using BlazorWindowManager.ClassLibrary.ConstructorAction;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Grid;

public record GridRecord
{
    private readonly Dictionary<Guid, GridWindowRecord> _gridWindowRecordMap;

    public GridRecord(GridRecord otherGridRecord, ConstructActionKind constructActionKind, params GridWindowRecord[] gridWindowRecords)
    {
        _gridWindowRecordMap = new Dictionary<Guid, GridWindowRecord>(otherGridRecord._gridWindowRecordMap);

        foreach (var gridWindowRecord in gridWindowRecords)
        {
            switch (constructActionKind)
            {
                case ConstructActionKind.Add:
                    AddGridWindowRecord(gridWindowRecord);
                    break;
                case ConstructActionKind.Replace:
                    ReplaceGridWindowRecord(gridWindowRecord);
                    break;
                default:
                    throw new ApplicationException($"The {nameof(constructActionKind)} with value: " +
                        $"'{constructActionKind}' is not " +
                        $"currently supported for constructing a {nameof(GridRecord)}");
            }
        }
    }
    
    public GridRecord(GridRecord otherGridRecord, params Guid[] gridWindowRecordIds)
    {
        _gridWindowRecordMap = new Dictionary<Guid, GridWindowRecord>(otherGridRecord._gridWindowRecordMap);

        foreach (var gridWindowRecord in gridWindowRecordIds)
        {
            _gridWindowRecordMap.Remove(gridWindowRecord);
        }
    }

    public Guid GridRecordId { get; } = Guid.NewGuid();

    private void ReplaceGridWindowRecord(GridWindowRecord gridWindowRecord)
    {
        _gridWindowRecordMap[gridWindowRecord.GridRecordId] = gridWindowRecord;
    }

    private void AddGridWindowRecord(GridWindowRecord gridWindowRecord)
    {
        _gridWindowRecordMap.Add(gridWindowRecord.GridRecordId, gridWindowRecord);
    }

    public ImmutableArray<GridWindowRecord> GridWindowRecords => _gridWindowRecordMap.Values
        .ToImmutableArray();
}
