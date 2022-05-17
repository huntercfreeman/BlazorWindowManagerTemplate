using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using Fluxor;
using BlazorWindowManager.ClassLibrary.Store.Html;
using BlazorWindowManager.ClassLibrary.Html;
using BlazorWindowManager.ClassLibrary.Dimension;

namespace BlazorWindowManager.RazorClassLibrary.Html;

public partial class HtmlElementExampleWrapperDisplay
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    private List<HtmlElementRecordKey> _htmlElementRecordKeys = new();

    private void AddHtmlElementOnClick()
    {
        var htmlElementKey = new HtmlElementRecordKey(Guid.NewGuid());
        
        _htmlElementRecordKeys.Add(htmlElementKey);

        var registerHtmlElemementAction = new RegisterHtmlElemementAction(htmlElementKey,
            DimensionsRecord.FromPixelUnits(400, 400, 0, 0),
            new ZIndexRecord(1));

        Dispatcher.Dispatch(registerHtmlElemementAction);
    }
}