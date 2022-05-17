using BlazorWindowManager.ClassLibrary.ConstructorAction;
using BlazorWindowManager.ClassLibrary.Dimension;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Grid;

public record GridRecord
{
    public readonly List<List<GridWindowRecord>> GridWindowRecords = new();

    public GridRecord()
    {
    }

    public GridRecord(GridRecord otherGridRecord, ConstructorActionKind constructActionKind, params GridWindowRecord[] gridWindowRecords)
    {
        GridWindowRecords = new List<List<GridWindowRecord>>(otherGridRecord.GridWindowRecords);

        foreach (var gridWindowRecord in gridWindowRecords)
        {
            switch (constructActionKind)
            {
                case ConstructorActionKind.Add:
                    AddGridWindowRecord(gridWindowRecord);
                    break;
                case ConstructorActionKind.Replace:
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
        GridWindowRecords = new List<List<GridWindowRecord>>(otherGridRecord.GridWindowRecords);

        // TODO: short circuit the loops when the corresponding GridWindow is found
        foreach (var gridWindowRecordId in gridWindowRecordIds)
        {
            foreach (var row in GridWindowRecords)
            {
                foreach (var column in row)
                {
                    if (column.GridWindowRecordId == gridWindowRecordId)
                    {
                        row.Remove(column);
                    }
                }
            }
        }
    }

    public Guid GridRecordId { get; init; } = Guid.NewGuid();

    private void ReplaceGridWindowRecord(GridWindowRecord gridWindowRecord)
    {
        for (int i = 0; i < GridWindowRecords.Count; i++)
        {
            List<GridWindowRecord>? row = GridWindowRecords[i];
            for (int i1 = 0; i1 < row.Count; i1++)
            {
                GridWindowRecord? column = row[i1];
                if (column.GridWindowRecordId == gridWindowRecord.GridWindowRecordId)
                {
                    row[i1] = gridWindowRecord;
                }
            }
        }
    }

    private void AddGridWindowRecord(GridWindowRecord gridWindowRecord)
    {
        GridWindowRecords.First().Add(gridWindowRecord);
    }

    private ImmutableArray<ImmutableArray<GridWindowRecord>> GetGridWindowRecords()
    {
        List<ImmutableArray<GridWindowRecord>> temporaryGridWindowRecords = new();

        foreach (var row in GridWindowRecords)
        {
            var temporaryRow = new List<GridWindowRecord>();

            foreach (var column in row)
            {
                temporaryRow.Add(column);
            }

            temporaryGridWindowRecords.Add(temporaryRow.ToImmutableArray());
        }

        return temporaryGridWindowRecords.ToImmutableArray();
    }

    public GridWindowRecord? FindGridWindowRecordById(Guid gridWindowRecordId)
    {
        foreach (var row in GridWindowRecords)
        {
            foreach (var column in row)
            {
                if (column.GridWindowRecordId == gridWindowRecordId)
                {
                    return column;
                }
            }
        }

        return null;
    }
}
