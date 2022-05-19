using System.Text;
using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Grid;
using BlazorWindowManager.ClassLibrary.Html;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using BlazorWindowManager.ClassLibrary.Store.Html;
using BlazorWindowManager.ClassLibrary.Store.Theme;
using BlazorWindowManager.ClassLibrary.Theme;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

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
    public RenderFragment ChooseGridTabRecordRenderFragment { get; set; } = null!;
    [Parameter]
    public DimensionsRecord? InitialDimensionsRecord { get; set; }

    public const string ON_CHOSE_GRID_TAB_RECORD_ACTION_PARAMETER_NAME = "OnChoseGridTabRecordAction";
    
    private GridBoard? _cachedGridBoard;
    private Guid? _previousGridBoardSequence;
    
    private Guid? _previousHtmlElementSequence;
    private HtmlElementRecord? _cachedHtmlElementRecord;

    protected override void OnInitialized()
    {
        var registerHtmlElementAction = new RegisterHtmlElementAction(GridRecord.HtmlElementRecordKey,
            InitialDimensionsRecord ?? DimensionsRecord.FromPixelUnits(400, 400, 0, 0),
            new ZIndexRecord(1));

        Dispatcher.Dispatch(registerHtmlElementAction);
        
        var registerGridRecordAction = new RegisterGridRecordAction(GridRecord.GridRecordKey);

        Dispatcher.Dispatch(registerGridRecordAction);

        ShouldRender();

        base.OnInitialized();
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

            // Get GridBoardRecord
            var gridBoardRecordStepNeedsRerender = false;

            _cachedGridBoard = GridRecordsState.Value
                .LookupGridBoard(GridRecord.GridRecordKey);

            if (_previousGridBoardSequence is null ||
                _previousGridBoardSequence.Value != _cachedGridBoard.GridBoardSequence)
            {
                gridBoardRecordStepNeedsRerender = true;
            }

            _previousGridBoardSequence = _cachedGridBoard.GridBoardSequence;

            shouldRender = htmlElementRecordStepNeedsRerender || gridBoardRecordStepNeedsRerender;
        }
        catch (KeyNotFoundException)
        {
            shouldRender = false;
        }

        return shouldRender;
    }

    private void AddGridItemRecordOnClick()
    {
        var addGridItemRecordAction = new AddGridItemRecordAction(GridRecord.GridRecordKey,
            new GridItemRecord(new GridItemRecordKey(Guid.NewGuid()),
                               new HtmlElementRecordKey(Guid.NewGuid())));

        Dispatcher.Dispatch(addGridItemRecordAction);
    }
    
    private string GetCssClasses()
    {
        var classBuilder = new StringBuilder();

        classBuilder.Append(ThemeState.Value.BlazorWindowManagerThemeKind.ConvertToCssClass());

        if (!string.IsNullOrWhiteSpace(ThemeState.Value.CssClassForOverridingColors))
            classBuilder.Append(ThemeState.Value.CssClassForOverridingColors);

        return classBuilder.ToString();
    }
    
    protected override void Dispose(bool disposing)
    {
        var unregisterHtmlElementAction = new UnregisterHtmlElementAction(GridRecord.HtmlElementRecordKey);

        Dispatcher.Dispatch(unregisterHtmlElementAction);

        base.Dispose(disposing);
    }
}