using Fluxor;
using BlazorWindowManager.ClassLibrary.Element;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

[FeatureState]
public record GridState(Dictionary<Guid, object?> GridModelMap, List<List<GridModel>> GridModels)
{
    private GridState() : this(new Dictionary<Guid, object?>(), new List<List<GridModel>>())
    {
    }
}
