using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Html;
using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.Html;

public class HtmlElementReducer
{
    [ReducerMethod]
    public static HtmlElementRecordsState ReduceZIndexResponseAction(HtmlElementRecordsState previousHtmlElementRecordsState,
        ZIndexResponseAction zIndexResponseAction)
    {
        var previousHtmlElementRecord = previousHtmlElementRecordsState.LookupHtmlElementRecord(zIndexResponseAction.HtmlElementRecordKey);

        var nextHtmlElementRecord = previousHtmlElementRecord with
        {
            ZIndexRecord = zIndexResponseAction.ZIndexRecord
        };

        return new HtmlElementRecordsState(previousHtmlElementRecordsState, 
            ConstructorAction.ConstructorActionKind.Replace, 
            nextHtmlElementRecord);
    }
    
    [ReducerMethod]
    public static HtmlElementRecordsState ReduceReplaceHtmlElementDimensionsRecordAction(HtmlElementRecordsState previousHtmlElementRecordsState,
        ReplaceHtmlElementDimensionsRecordAction replaceHtmlElementDimensionsRecordAction)
    {
        var previousHtmlElementRecord = previousHtmlElementRecordsState.LookupHtmlElementRecord(replaceHtmlElementDimensionsRecordAction.HtmlElementRecordKey);

        var nextHtmlElementRecord = previousHtmlElementRecord with
        {
            DimensionsRecord = replaceHtmlElementDimensionsRecordAction.DimensionsRecord
        };

        return new HtmlElementRecordsState(previousHtmlElementRecordsState, 
            ConstructorAction.ConstructorActionKind.Replace, 
            nextHtmlElementRecord);
    }
    
    [ReducerMethod]
    public static HtmlElementRecordsState ReduceRegisterHtmlElemementAction(HtmlElementRecordsState previousHtmlElementRecordsState,
        RegisterHtmlElemementAction registerHtmlElemementAction)
    {
        var htmlElementRecord = new HtmlElementRecord(registerHtmlElemementAction.HtmlElementRecordKey,
            registerHtmlElemementAction.DimensionsRecord,
            registerHtmlElemementAction.ZIndexRecord);

        return new HtmlElementRecordsState(previousHtmlElementRecordsState,
            ConstructorAction.ConstructorActionKind.Add,
            htmlElementRecord);
    }
    
    [ReducerMethod]
    public static HtmlElementRecordsState ReduceUnregisterHtmlElemementAction(HtmlElementRecordsState previousHtmlElementRecordsState,
        UnregisterHtmlElemementAction unregisterHtmlElemementAction)
    {
        return previousHtmlElementRecordsState;
    }
}
