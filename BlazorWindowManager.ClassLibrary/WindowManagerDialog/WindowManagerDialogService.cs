using BlazorWindowManager.ClassLibrary.Store.WindowManagerDialog;
using Fluxor;

namespace BlazorWindowManager.ClassLibrary.WindowManagerDialog;

public class WindowManagerDialogService : IWindowManagerDialogService
{
    private readonly IDispatcher _dispatcher;

    public WindowManagerDialogService(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public void AddWindowManagerDialogRecord(WindowManagerDialogRecord windowManagerDialogRecord)
    {
        var action = new AddWindowManagerDialogRecordAction(windowManagerDialogRecord);

        _dispatcher.Dispatch(action);
    }

    public void ClearWindowManagerDialogRecord()
    {
        var action = new ClearWindowManagerDialogStateAction();

        _dispatcher.Dispatch(action);
    }

    public void PersistWindowManagerDialogRecord(WindowManagerDialogRecord windowManagerDialogRecord)
    {
        var action = new PersistWindowManagerDialogRecordStateAction(windowManagerDialogRecord);

        _dispatcher.Dispatch(action);
    }

    public void RemoveWindowManagerDialogRecord(Guid windowManagerDialogRecordId)
    {
        var action = new RemoveWindowManagerDialogRecordAction(windowManagerDialogRecordId);

        _dispatcher.Dispatch(action);
    }
}
