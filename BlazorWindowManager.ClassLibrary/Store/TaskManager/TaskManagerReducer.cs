using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.TaskManager;

public class TaskManagerReducer
{
    [ReducerMethod]
    public static TaskManagerState ReduceEnqueueTaskRecordAction(TaskManagerState previousTaskManagerState,
        EnqueueTaskRecordAction enqueueTaskRecordAction)
    {
        return new TaskManagerState(previousTaskManagerState, 
            enqueueTaskRecordAction.TaskRecord);
    }
    
    [ReducerMethod]
    public static TaskManagerState ReduceEnqueueTaskRecordAction(TaskManagerState previousTaskManagerState,
        TaskManagerServiceStateHasChangedAction taskManagerServiceStateHasChangedAction)
    {
        return previousTaskManagerState;
    }
}