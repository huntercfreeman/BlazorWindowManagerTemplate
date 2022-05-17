using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.Html;
using Fluxor;
using BlazorWindowManager.ClassLibrary.Store.Html;

namespace BlazorWindowManager.RazorClassLibrary.Html;

public partial class HtmlElementExampleDisplay : ComponentBase
{
    [Inject]
    private IState<HtmlElementRecordsState> HtmlElementRecordsState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public HtmlElementRecordKey HtmlElementRecordKey { get; set; } = null!;

    private void RequestZIndexOnClick()
    {
        var zIndexRequestAction = new ZIndexRequestAction(HtmlElementRecordKey);

        Dispatcher.Dispatch(zIndexRequestAction);
    }

    private void OnDimensionsRecordChangedEventCallback()
    {

    }
}