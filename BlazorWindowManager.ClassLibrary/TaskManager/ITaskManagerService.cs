namespace BlazorWindowManager.ClassLibrary.TaskManager;

public interface ITaskManagerService
{
    public void EnqueueTaskRecord(TaskRecord taskRecord);
}