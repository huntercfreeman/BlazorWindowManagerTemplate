using BlazorWindowManager.ClassLibrary.Direction;
using BlazorWindowManager.ClassLibrary.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Store.Grid;

public record AddGridModelAction((CardinalDirectionKind CardinalDirectionKind, int GridColumnIndex, int GridRowIndex) ArgumentTuple, 
    GridModel GridModel);
