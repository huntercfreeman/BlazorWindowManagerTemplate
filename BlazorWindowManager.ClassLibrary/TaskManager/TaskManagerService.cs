using BlazorWindowManager.ClassLibrary.Store.TaskManager;
using Fluxor;

namespace BlazorWindowManager.ClassLibrary.TaskManager;

public class TaskManagerService : ITaskManagerService
{
    private readonly IDispatcher _dispatcher;

    public TaskManagerService(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public void EnqueueTaskRecord(TaskRecord taskRecord)
    {
        var action = new EnqueueTaskRecordAction(taskRecord);

        _dispatcher.Dispatch(action);
    }

    public void NotifyStateHasChanged()
    {
        var action = new TaskManagerServiceStateHasChangedAction();

        _dispatcher.Dispatch(action);
    }
}
