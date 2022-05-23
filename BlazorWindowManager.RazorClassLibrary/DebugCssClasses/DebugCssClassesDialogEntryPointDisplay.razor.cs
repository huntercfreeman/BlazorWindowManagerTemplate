using System.Text;
using BlazorWindowManager.ClassLibrary.Html;
using BlazorWindowManager.ClassLibrary.Store.Theme;
using BlazorWindowManager.ClassLibrary.Store.WindowManagerDialog;
using BlazorWindowManager.ClassLibrary.Theme;
using BlazorWindowManager.ClassLibrary.WindowManagerDialog;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorWindowManager.RazorClassLibrary.DebugCssClasses;

public partial class DebugCssClassesDialogEntryPointDisplay : ComponentBase
{
    [Inject]
    private IState<WindowManagerDialogWrapperState> WindowManagerDialogWrapperState { get; set; }  = null!;
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; }  = null!;

    private static Guid _dialogId = Guid.NewGuid();
    
    private void RenderDialogOnClick()
    {
        if (!WindowManagerDialogWrapperState.Value.WindowManagerDialogRecordMap.ContainsKey(_dialogId))
        {
            var windowManagerDialogRecord = new WindowManagerDialogRecord(Guid.NewGuid(),
                $"{nameof(DebugCssClassToggleStateDisplay)} Dialog",
                typeof(DebugCssClassToggleStateDisplay),
                null,
                new HtmlElementRecordKey(Guid.NewGuid()),
                IsMinimized: false
            );
            
            var action = new AddWindowManagerDialogRecordAction(windowManagerDialogRecord);

            Dispatcher.Dispatch(action);
        }
    }

    private string GetCssClasses()
    {
        var classBuilder = new StringBuilder();

        classBuilder.Append(ThemeState.Value.BlazorWindowManagerThemeKind.ConvertToCssClass());

        if(!string.IsNullOrWhiteSpace(ThemeState.Value.CssClassForOverridingColors))
            classBuilder.Append(ThemeState.Value.CssClassForOverridingColors);

        return classBuilder.ToString();
    }
}