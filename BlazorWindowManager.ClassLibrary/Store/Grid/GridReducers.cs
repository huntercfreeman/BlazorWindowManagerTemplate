//using Fluxor;
//using BlazorWindowManager.ClassLibrary.Element;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BlazorWindowManager.ClassLibrary.Store.Grid;

//public class GridReducers
//{
//[ReducerMethod]
//public static GridState ReduceAddGridStateAction(GridState previousGridState,
//    AddGridModelAction addGridStateAction)
//{
//    // Replace GridModels Lists
//    var nextRowsList = new List<List<GridModel>>();

//    foreach (var row in previousGridState.GridModels)
//    {
//        nextRowsList.Add(new List<GridModel>(row));
//    }

//    switch (addGridStateAction.ArgumentTuple.CardinalDirectionKind)
//    {
//        case Direction.CardinalDirectionKind.North:
//            nextRowsList.Insert(addGridStateAction.ArgumentTuple.GridRowIndex, new());

//            nextRowsList[addGridStateAction.ArgumentTuple.GridRowIndex].Add(addGridStateAction.GridModel);
//            break;
//        case Direction.CardinalDirectionKind.East:
//            nextRowsList[addGridStateAction.ArgumentTuple.GridRowIndex].Insert(addGridStateAction.ArgumentTuple.GridColumnIndex + 1, addGridStateAction.GridModel);
//            break;
//        case Direction.CardinalDirectionKind.South:
//            nextRowsList.Insert(addGridStateAction.ArgumentTuple.GridRowIndex + 1, new());

//            nextRowsList[addGridStateAction.ArgumentTuple.GridRowIndex + 1].Add(addGridStateAction.GridModel);
//            break;
//        case Direction.CardinalDirectionKind.West:
//            nextRowsList[addGridStateAction.ArgumentTuple.GridRowIndex]
//                .Insert(addGridStateAction.ArgumentTuple.GridColumnIndex,
//                    addGridStateAction.GridModel);
//            break;
//    }

//    // Replace GridModelMap
//    var nextGridModelMap = new Dictionary<Guid, object?>(previousGridState.GridModelMap);

//    nextGridModelMap.Add(addGridStateAction.GridModel.GridModelId, addGridStateAction.GridModelChildComponentState);

//    return new GridState(nextGridModelMap, nextRowsList);
//}

//    [ReducerMethod]
//    public static GridState ReduceReplaceGridModelAction(GridState previousGridState,
//        ReplaceGridModelAction replaceGridModelAction)
//    {
//        // Replace GridModelMap
//        var nextGridModelMap = new Dictionary<Guid, object?>(previousGridState.GridModelMap);

//        nextGridModelMap.Remove(replaceGridModelAction.GridModel.GridModelId);

//        nextGridModelMap.Add(replaceGridModelAction.GridModel.GridModelId, replaceGridModelAction.GridModelChildComponentState);

//        return previousGridState with
//        {
//            GridModelMap = nextGridModelMap
//        };
//    }
//}
