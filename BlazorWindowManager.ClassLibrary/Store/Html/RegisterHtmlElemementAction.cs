using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Html;

namespace BlazorWindowManager.ClassLibrary.Store.Html;

public record RegisterHtmlElemementAction(HtmlElementRecordKey HtmlElementRecordKey,
        DimensionsRecord DimensionsRecord,
        ZIndexRecord ZIndexRecord);
