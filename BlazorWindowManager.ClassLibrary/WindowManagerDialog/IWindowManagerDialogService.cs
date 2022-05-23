namespace BlazorWindowManager.ClassLibrary.WindowManagerDialog;

public interface IWindowManagerDialogService
{
    public void AddWindowManagerDialogRecord(WindowManagerDialogRecord windowManagerDialogRecord);
    public void ClearWindowManagerDialogRecord();
    public void PersistWindowManagerDialogRecord(WindowManagerDialogRecord windowManagerDialogRecord);
    public void RemoveWindowManagerDialogRecord(Guid windowManagerDialogRecordId);
}
