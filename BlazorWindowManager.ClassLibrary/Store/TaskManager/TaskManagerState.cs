using BlazorWindowManager.ClassLibrary.TaskManager;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Store.TaskManager;

[FeatureState]
public record TaskManagerState
{
    private readonly Dictionary<Guid, EnqueuedTaskRecord> _activeTasksDictionary;

    public TaskManagerState()
    {
        _activeTasksDictionary = new();
    }

    public TaskManagerState(TaskManagerState previousTaskManagerState, TaskRecord taskRecord)
    {
        _activeTasksDictionary = new Dictionary<Guid, EnqueuedTaskRecord>(
            previousTaskManagerState._activeTasksDictionary);

        var task = taskRecord.TaskFunc(taskRecord.CancellationToken);

        _activeTasksDictionary.Add(taskRecord.TaskRecordId,
            new EnqueuedTaskRecord(taskRecord, task));

        _ = Task.Run(async () =>
        {
            await task;
        });
    }

    public ImmutableArray<EnqueuedTaskRecord> ActiveTasks => _activeTasksDictionary.Values
        .ToImmutableArray();
}
