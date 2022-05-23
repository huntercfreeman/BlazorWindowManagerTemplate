using BlazorWindowManager.ClassLibrary.TaskManager;
using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.TaskManager;

public record EnqueueTaskRecordAction(TaskRecord TaskRecord);

public class TaskManagerReducer
{
    [ReducerMethod]
    public static TaskManagerState ReduceEnqueueTaskRecordAction(TaskManagerState previousTaskManagerState,
        EnqueueTaskRecordAction enqueueTaskRecordAction)
    {
        return new TaskManagerState(previousTaskManagerState, 
            enqueueTaskRecordAction.TaskRecord);
    }
}