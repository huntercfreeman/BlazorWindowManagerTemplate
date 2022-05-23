using BlazorWindowManager.ClassLibrary.TaskManager;
using Fluxor;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Store.TaskManager;

[FeatureState]
public record TaskManagerState
{
    private readonly ConcurrentDictionary<Guid, EnqueuedTaskRecord> _activeTasksDictionary;
    private readonly ConcurrentDictionary<Guid, EnqueuedTaskRecord> _successfulTasksDictionary;
    private readonly ConcurrentDictionary<Guid, EnqueuedTaskRecord> _failedTasksDictionary;

    public TaskManagerState()
    {
        _activeTasksDictionary = new();
        _successfulTasksDictionary = new();
        _failedTasksDictionary = new();
    }

    public TaskManagerState(TaskManagerState previousTaskManagerState, TaskRecord taskRecord)
    {
        _activeTasksDictionary = new ConcurrentDictionary<Guid, EnqueuedTaskRecord>(
            previousTaskManagerState._activeTasksDictionary);
        _successfulTasksDictionary = new ConcurrentDictionary<Guid, EnqueuedTaskRecord>(
            previousTaskManagerState._successfulTasksDictionary);
        _failedTasksDictionary = new ConcurrentDictionary<Guid, EnqueuedTaskRecord>(
            previousTaskManagerState._failedTasksDictionary);

        var task = taskRecord.TaskFunc(taskRecord.CancellationToken);

        _activeTasksDictionary.TryAdd(taskRecord.TaskRecordId,
            new EnqueuedTaskRecord(taskRecord, task));

        _ = Task.Run(async () =>
        {
            await task;

            if(_activeTasksDictionary.Remove(taskRecord.TaskRecordId, out var enqueuedTaskRecord))
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    _failedTasksDictionary.TryAdd(taskRecord.TaskRecordId, enqueuedTaskRecord);
                }
                else
                {
                    _successfulTasksDictionary.TryAdd(taskRecord.TaskRecordId, enqueuedTaskRecord);
                }
            }
        });
    }

    public ImmutableArray<EnqueuedTaskRecord> ActiveTasks => _activeTasksDictionary.Values
        .ToImmutableArray();

    public ImmutableArray<EnqueuedTaskRecord> SuccessfulTasks => _successfulTasksDictionary.Values
        .ToImmutableArray();

    public ImmutableArray<EnqueuedTaskRecord> FailedTasks => _failedTasksDictionary.Values
        .ToImmutableArray();
}
