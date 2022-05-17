using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWindowManager.RazorClassLibrary.Icons.Codicon;
using BlazorWindowManager.ClassLibrary.Grid;
using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Store.WindowManagerDialog;
using BlazorWindowManager.ClassLibrary.WindowManagerDialog;
using Fluxor;
using BlazorWindowManager.ClassLibrary.Store.Grid;
using Fluxor.Blazor.Web.Components;
using BlazorWindowManager.ClassLibrary.Html;
using BlazorWindowManager.ClassLibrary.Store.Html;
using BlazorWindowManager.ClassLibrary.Store.Theme;
using System.Text;
using BlazorWindowManager.ClassLibrary.Theme;

namespace BlazorWindowManager.RazorClassLibrary.Grid;

public partial class GridDisplay : FluxorComponent
{
    [Inject]
    private IViewportDimensionsService ViewportDimensionsService { get; set; } = null!;
    [Inject]
    private IState<HtmlElementRecordsState> HtmlElementRecordsState { get; set; } = null!;
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public GridRecord GridRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public DimensionsRecord InitialDimensionsRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public Type AddWindowToGridRenderedType { get; set; } = null!;
    [Parameter, EditorRequired]
    public Dictionary<string, object> AddWindowToGridRenderedTypeParameters { get; set; } = null!;
    [Parameter]
    public bool IsResizable { get; set; } = false;

    private WindowManagerDialogRecord? _windowManagerDialogRecord;
    private Guid? _previousHtmlElementSequence;
    private HtmlElementRecord? _cachedHtmlElementRecord;
    private int _renderCount;

    protected override async Task OnInitializedAsync()
    {
        var dimensionsRecordForGrid = InitialDimensionsRecord ?? DimensionsRecord.FromPixelUnits(400, 400, 0, 0);
        var registerHtmlElemementAction = new RegisterHtmlElemementAction(GridRecord.HtmlElementRecordKey,
            dimensionsRecordForGrid,
            new ZIndexRecord(0));

        Dispatcher.Dispatch(registerHtmlElemementAction);

        await base.OnInitializedAsync();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        _renderCount++;

        base.OnAfterRender(firstRender);
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

    private async Task OnAddWindowToGridOnClickAsync()
    {
        if (_windowManagerDialogRecord is not null)
            return;

        var dimensionsRecordForDialog = await WindowManagerDialogRecord.ConstructDefaultDimensionsRecord(ViewportDimensionsService);

        var completeDialogInteractionEventCallback = new EventCallback<object>(this, OnCompletedDialogInteraction);

        var combinedParametersDictionary = new Dictionary<string, object>(AddWindowToGridRenderedTypeParameters ?? new())
        {
            { "CompleteDialogInteractionEventCallback", completeDialogInteractionEventCallback }
        };

        _windowManagerDialogRecord = new WindowManagerDialogRecord(Guid.NewGuid(),
            "Add Window To Grid",
            AddWindowToGridRenderedType,
            combinedParametersDictionary,
            new HtmlElementRecordKey(Guid.NewGuid()));

        var action = new AddWindowManagerDialogRecordAction(_windowManagerDialogRecord);

        Dispatcher.Dispatch(action);
    }

    private void OnCompletedDialogInteraction(object item)
    {
        var type = (Type)item;

        var gridWindowRecord = new GridWindowRecord("New Window", type);

        if(!GridRecord.GridWindowRecords.Any())
            GridRecord.GridWindowRecords.Add(new List<GridWindowRecord>());

        GridRecord.GridWindowRecords.First().Add(gridWindowRecord);

        if (_windowManagerDialogRecord is null)
            return;

        var action = new RemoveWindowManagerDialogRecordAction(_windowManagerDialogRecord.WindowManagerDialogRecordId);

        Dispatcher.Dispatch(action);
    }

    private string GetCssClasses()
    {
        var classBuilder = new StringBuilder();

        classBuilder.Append(ThemeState.Value.BlazorWindowManagerThemeKind.ConvertToCssClass());

        if (!string.IsNullOrWhiteSpace(ThemeState.Value.CssClassForOverridingColors))
            classBuilder.Append(ThemeState.Value.CssClassForOverridingColors);

        return classBuilder.ToString();
    }
}