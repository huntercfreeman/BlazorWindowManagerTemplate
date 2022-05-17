using BlazorWindowManager.ClassLibrary.ConstructorAction;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Grid;

public record GridRecord(Guid GridRecordId, List<List<GridWindowRecord>> GridWindowRecords)
{
    private readonly Dictionary<Guid, GridWindowRecord> _gridWindowRecordMap;

    public GridRecord(GridRecord otherGridRecord, ConstructActionKind constructActionKind, GridWindowRecord[] gridWindowRecords)
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
                        $"currently supported for constructing a {nameof(GridStates)}");
            }
        }
    }

    private void ReplaceGridWindowRecord(GridWindowRecord gridWindowRecord)
    {
        throw new NotImplementedException();
    }

    private void AddGridWindowRecord(GridWindowRecord gridWindowRecord)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<GridWindowRecord> GridWindowRecords => _gridWindowRecordMap.Values
        .ToImmutableArray();
}
