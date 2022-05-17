using BlazorWindowManager.ClassLibrary.Dimension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Html;

public record HtmlElementRecord(HtmlElementRecordKey HtmlElementRecordKey, 
    DimensionsRecord DimensionsRecord, 
    ZIndexRecord ZIndexRecord,
    Guid HtmlElementSequence)
{
}
