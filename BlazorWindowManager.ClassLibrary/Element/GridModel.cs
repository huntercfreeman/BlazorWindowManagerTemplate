using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Direction;

namespace BlazorWindowManager.ClassLibrary.Element;

public class GridModel
{
    public GridModel(Guid gridModelId, Type renderedContentType, List<List<GridModel>> gridModels)
    {
        GridModelId = gridModelId;
        RenderedContentType = renderedContentType;
        GridModels = gridModels;
    }

    public Guid GridModelId { get; }
    public Type RenderedContentType { get; }
    public List<List<GridModel>> GridModels { get; }

    public void AddGridModel((CardinalDirectionKind CardinalDirectionKind, int GridColumnIndex, int GridRowIndex) argumentTuple,
        GridModel gridModel)
    {
        switch (argumentTuple.CardinalDirectionKind)
        {
            case Direction.CardinalDirectionKind.North:
                GridModels.Insert(argumentTuple.GridRowIndex, new());

                GridModels[argumentTuple.GridRowIndex].Add(gridModel);
                break;
            case Direction.CardinalDirectionKind.East:
                GridModels[argumentTuple.GridRowIndex].Insert(argumentTuple.GridColumnIndex + 1, gridModel);
                break;
            case Direction.CardinalDirectionKind.South:
                GridModels.Insert(argumentTuple.GridRowIndex + 1, new());

                GridModels[argumentTuple.GridRowIndex + 1].Add(gridModel);
                break;
            case Direction.CardinalDirectionKind.West:
                GridModels[argumentTuple.GridRowIndex]
                    .Insert(argumentTuple.GridColumnIndex,
                        gridModel);
                break;
        }
    }
}