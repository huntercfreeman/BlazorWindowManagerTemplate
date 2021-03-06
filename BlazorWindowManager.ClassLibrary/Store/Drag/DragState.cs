using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Store.Drag;

[FeatureState]
public record DragState(double DeltaX, double DeltaY, MouseEventArgs? MouseEventArgs)
{
    public DragState() : this(0, 0, null)
    {

    }
}
