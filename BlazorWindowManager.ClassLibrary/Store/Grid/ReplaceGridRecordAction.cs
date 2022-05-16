using BlazorWindowManager.ClassLibrary.Element;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public record ReplaceGridModelAction(GridModel GridModel, object? GridModelChildComponentState);
