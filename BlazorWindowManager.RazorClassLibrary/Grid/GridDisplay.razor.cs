using Microsoft.AspNetCore.Components;
using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Html;
using BlazorWindowManager.ClassLibrary.Store.Html;
using BlazorWindowManager.RazorClassLibrary.Transformative;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using BlazorWindowManager.ClassLibrary.Grid;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using BlazorWindowManager.ClassLibrary.Store.Theme;
using System.Text;
using BlazorWindowManager.ClassLibrary.Theme;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridDisplay : FluxorComponent
{
    [Inject]
    private IState<HtmlElementRecordsState> HtmlElementRecordsState { get; set; } = null!;
    [Inject]
    private IState<GridRecordsState> GridRecordsState { get; set; } = null!;
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public GridRecord GridRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public GridTabContainerRecord GridTabContainerRecord { get; set; } = null!;
    [Parameter]
    public DimensionsRecord? InitialDimensionsRecord { get; set; }

    private TransformativeDisplay _transformativeDisplay = null!;
    private Guid? _previousHtmlElementSequence;
    private Guid? _previousGridTabContainerSequence;
    private HtmlElementRecord? _cachedHtmlElementRecord;
    private GridTabContainerRecord? _cachedGridTabContainerRecord;
    private int _renderCount;

    protected override async Task OnInitializedAsync()
    {
        var registerHtmlElemementAction = new RegisterHtmlElemementAction(GridRecord.HtmlElementRecordKey,
            InitialDimensionsRecord ?? DimensionsRecord.FromPixelUnits(400, 400, 0, 0),
            new ZIndexRecord(1));

        Dispatcher.Dispatch(registerHtmlElemementAction);

        var registerGridTabContainerRecordAction = new RegisterGridTabContainerRecordAction(GridRecord.GridRecordKey,
            GridTabContainerRecord);

        Dispatcher.Dispatch(registerGridTabContainerRecordAction);

        await base.OnInitializedAsync();
    }

    protected override bool ShouldRender()
    {
        bool shouldRender;

        try
        {
            // Get HtmlElementRecord
            var htmlElementRecordStepNeedsRerender = false;

            _cachedHtmlElementRecord = HtmlElementRecordsState.Value
                .LookupHtmlElementRecord(GridRecord.HtmlElementRecordKey);

            if (_previousHtmlElementSequence is null ||
                _previousHtmlElementSequence.Value != _cachedHtmlElementRecord.HtmlElementSequence)
            {
                htmlElementRecordStepNeedsRerender = true;
            }

            _previousHtmlElementSequence = _cachedHtmlElementRecord.HtmlElementSequence;

            // Get GridTabContainerRecord
            var gridTabContainerRecordStepNeedsRerender = false;

            _cachedGridTabContainerRecord = GridRecordsState.Value
                .LookupHtmlElementRecord(GridRecord.GridRecordKey);

            if (_previousGridTabContainerSequence is null ||
                _previousGridTabContainerSequence.Value != _cachedGridTabContainerRecord.GridTabContainerSequence)
            {
                gridTabContainerRecordStepNeedsRerender = true;
            }

            _previousGridTabContainerSequence = _cachedGridTabContainerRecord.GridTabContainerSequence;

            shouldRender = htmlElementRecordStepNeedsRerender || gridTabContainerRecordStepNeedsRerender;
        }
        catch (KeyNotFoundException)
        {
            shouldRender = false;
        }

        return shouldRender;
    }

    private string GetCssClasses()
    {
        var classBuilder = new StringBuilder();

        classBuilder.Append(ThemeState.Value.BlazorWindowManagerThemeKind.ConvertToCssClass());

        if (!string.IsNullOrWhiteSpace(ThemeState.Value.CssClassForOverridingColors))
            classBuilder.Append(ThemeState.Value.CssClassForOverridingColors);

        return classBuilder.ToString();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        _renderCount++;

        base.OnAfterRender(firstRender);
    }
}