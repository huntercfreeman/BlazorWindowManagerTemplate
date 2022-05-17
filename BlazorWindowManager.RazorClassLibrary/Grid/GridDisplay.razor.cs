using Microsoft.AspNetCore.Components;
using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Html;
using BlazorWindowManager.ClassLibrary.Store.Html;
using BlazorWindowManager.RazorClassLibrary.Transformative;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using BlazorWindowManager.ClassLibrary.Grid;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridDisplay : FluxorComponent
{
    [Inject]
    private IViewportDimensionsService ViewportDimensionsService { get; set; } = null!;
    [Inject]
    private IState<HtmlElementRecordsState> HtmlElementRecordsState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public GridRecord GridRecord { get; set; } = null!;
    [Parameter]
    public DimensionsRecord? InitialDimensionsRecord { get; set; }

    private TransformativeDisplay _transformativeDisplay = null!;
    private Guid? _previousHtmlElementSequence;
    private HtmlElementRecord? _cachedHtmlElementRecord;
    private int _renderCount;

    protected override async Task OnInitializedAsync()
    {
        var registerHtmlElemementAction = new RegisterHtmlElemementAction(GridRecord.HtmlElementRecordKey,
            InitialDimensionsRecord ?? DimensionsRecord.FromPixelUnits(400, 400, 0, 0),
            new ZIndexRecord(1));

        Dispatcher.Dispatch(registerHtmlElemementAction);

        var registerHtmlElemementAction = new RegisterHtmlElemementAction(GridRecord.HtmlElementRecordKey,
            InitialDimensionsRecord ?? DimensionsRecord.FromPixelUnits(400, 400, 0, 0),
            new ZIndexRecord(1));

        Dispatcher.Dispatch(registerHtmlElemementAction);

        await base.OnInitializedAsync();
    }

    protected override bool ShouldRender()
    {
        bool shouldRender;

        try
        {
            _cachedHtmlElementRecord = HtmlElementRecordsState.Value
                .LookupHtmlElementRecord(GridRecord.HtmlElementRecordKey);

            if (_previousHtmlElementSequence is null ||
                _previousHtmlElementSequence.Value != _cachedHtmlElementRecord.HtmlElementSequence)
            {
                shouldRender = true;
            }
            else
            {
                shouldRender = false;
            }

            _previousHtmlElementSequence = _cachedHtmlElementRecord.HtmlElementSequence;
        }
        catch (KeyNotFoundException)
        {
            shouldRender = false;
        }

        return shouldRender;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        _renderCount++;

        base.OnAfterRender(firstRender);
    }
}