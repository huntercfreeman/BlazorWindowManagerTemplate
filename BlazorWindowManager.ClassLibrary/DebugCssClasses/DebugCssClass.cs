namespace BlazorWindowManager.ClassLibrary.DebugCssClasses;

public record DebugCssClass(Guid DebugCssClassId,
    string CssClassName,
    string DisplayName,
    string Description,
    bool IsActive)
{
}