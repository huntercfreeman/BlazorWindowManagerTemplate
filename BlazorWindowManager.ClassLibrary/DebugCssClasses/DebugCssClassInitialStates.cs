using System.Collections.Immutable;
using BlazorWindowManager.ClassLibrary.Store.Drag;

namespace BlazorWindowManager.ClassLibrary.DebugCssClasses;

public static class DebugCssClassInitialStates
{
    public static readonly DebugCssClassSection DragDebugCssClassSectionInitialState = new DebugCssClassSection(
        $"{nameof(DragState)}",
        $"{nameof(DragState)} related toggleable css classes for debugging.",
        new DebugCssClass[]
        {
            new DebugCssClass(Guid.NewGuid(),
                "bwmt_debug-drag-event-provider-display",
                $"{nameof(DragState)} css classes for debugging", 
                $"{nameof(DragState)} css classes for debugging",
                false)
        }.ToImmutableArray());

    public static readonly ImmutableArray<DebugCssClassSection> DebugCssClassSectionInitalStates = new DebugCssClassSection[]
    {
        DragDebugCssClassSectionInitialState
    }.ToImmutableArray();
}