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
using Fluxor.Blazor.Web.Components;

namespace BlazorWindowManager.RazorClassLibrary.Html;

public partial class HtmlElementExampleDisplay : FluxorComponent
{
    [Inject]
    private IState<HtmlElementRecordsState> HtmlElementRecordsState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public HtmlElementRecordKey HtmlElementRecordKey { get; set; } = null!;

    private Guid? _previousHtmlElementSequence;

    private int _renderedCount;

    private void RequestZIndexOnClick()
    {
        var zIndexRequestAction = new ZIndexRequestAction(HtmlElementRecordKey);

        Dispatcher.Dispatch(zIndexRequestAction);
    }

    private void OnDimensionsRecordChangedEventCallback()
    {

    }

    protected override bool ShouldRender()
    {
        var htmlElementRecord = HtmlElementRecordsState.Value.LookupHtmlElementRecord(HtmlElementRecordKey);

        bool shouldRender;

        if(_previousHtmlElementSequence is null ||
            _previousHtmlElementSequence.Value != htmlElementRecord.HtmlElementSequence)
        {
            shouldRender = true;
        }
        else
        {
            shouldRender = false;
        }

        _previousHtmlElementSequence = htmlElementRecord.HtmlElementSequence;

        return shouldRender;
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        _renderedCount++;

        return base.OnAfterRenderAsync(firstRender);
    }
}