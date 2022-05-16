using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.WindowManagerDialog;

public record WindowManagerDialogRecord(Guid WindowManagerDialogRecordId,
    Type RenderedContentType,
    Dictionary<string, object>? RenderedContentParameters)
{
}
