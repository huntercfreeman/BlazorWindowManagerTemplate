using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.Dropdown;

public class DropdownReducer
{
    [ReducerMethod]
    public static DropdownState ReduceSetDropdownStateAction(DropdownState previousDropdownState,
        SetDropdownStateAction setDropdownStateAction)
    {
        return new DropdownState(setDropdownStateAction.DropdownGroupRecord);
    }
}