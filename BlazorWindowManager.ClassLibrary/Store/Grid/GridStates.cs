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
public record GridStates
{
    private readonly Dictionary<Guid, GridRecord> _gridRecordMap;

    public GridStates()
    {
        _gridRecordMap = new Dictionary<Guid, GridRecord>();
    }
    
    public GridStates(GridStates otherGridStates, ConstructActionKind constructActionKind, params GridRecord[] gridRecords)
    {
        _gridRecordMap = new Dictionary<Guid, GridRecord>(otherGridStates._gridRecordMap);

        foreach(var gridRecord in gridRecords)
        {
            switch (constructActionKind)
            {
                case ConstructActionKind.Add:
                    AddGridRecord(gridRecord);
                    break;
                case ConstructActionKind.Replace:
                    ReplaceGridRecord(gridRecord);
                    break;
                default:
                    throw new ApplicationException($"The {nameof(constructActionKind)} with value: " +
                        $"'{constructActionKind}' is not " +
                        $"currently supported for constructing a {nameof(GridStates)}");
            }
        }
    }

    public GridStates(GridStates otherGridStates, params Guid[] toBeRemovedGridRecordIds)
    {
        _gridRecordMap = new Dictionary<Guid, GridRecord>(otherGridStates._gridRecordMap);

        foreach (var gridRecordId in toBeRemovedGridRecordIds)
        {
            _gridRecordMap.Remove(gridRecordId);
        }
    }

    private void AddGridRecord(GridRecord gridRecord)
    {
        _gridRecordMap.Add(gridRecord.GridRecordId, gridRecord);
    }
    
    private void ReplaceGridRecord(GridRecord gridRecord)
    {
        try
        {
            _gridRecordMap[gridRecord.GridRecordId] = gridRecord;
        }
        catch (KeyNotFoundException)
        {
            // TODO: Should I just throw the exception in this case?
            _gridRecordMap.Add(gridRecord.GridRecordId, gridRecord);
        }
    }
    
    public ImmutableArray<GridRecord> GridRecords => _gridRecordMap.Values
        .ToImmutableArray();

    public GridRecord LookupGridRecordById(Guid gridRecordId)
    {
        return _gridRecordMap[gridRecordId];
    }
}