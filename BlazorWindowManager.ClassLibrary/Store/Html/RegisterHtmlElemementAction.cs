using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Html;

namespace BlazorWindowManager.ClassLibrary.Store.Html;

public record RegisterHtmlElementAction(HtmlElementRecordKey HtmlElementRecordKey,
        DimensionsRecord DimensionsRecord,
        ZIndexRecord ZIndexRecord);
