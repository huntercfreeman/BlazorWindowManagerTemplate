using BlazorWindowManager.ClassLibrary.Dimension;
using BlazorWindowManager.ClassLibrary.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.WindowManagerDialog;

public record WindowManagerDialogRecord(Guid WindowManagerDialogRecordId,
    string WindowManagerDialogRecordDisplayName,
    Type RenderedContentType,
    Dictionary<string, object>? RenderedContentParameters, 
    HtmlElementRecordKey HtmlElementRecordKey, 
    bool IsMinimized = false)
{
    public const double DEFAULT_WIDTH_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL = 0.7;
    public const double DEFAULT_HEIGHT_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL = 0.7;

    public const double DEFAULT_LEFT_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL = 0.15;
    public const double DEFAULT_TOP_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL = 0.15;

    public static async Task<DimensionsRecord> ConstructDefaultDimensionsRecord(IViewportDimensionsService viewportDimensionsService)
    {
        var viewportDimensions = await viewportDimensionsService.GetViewportDimensionsAsync();

        DimensionValuedUnit widthInPixels =
            new DimensionValuedUnit(DEFAULT_WIDTH_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL * viewportDimensions.WidthInPixels,
                DimensionUnitKind.Pixels);

        DimensionValuedUnit heightInPixels =
            new DimensionValuedUnit(DEFAULT_HEIGHT_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL * viewportDimensions.HeightInPixels,
                DimensionUnitKind.Pixels);

        DimensionValuedUnit leftInPixels =
            new DimensionValuedUnit(DEFAULT_LEFT_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL * viewportDimensions.WidthInPixels,
                DimensionUnitKind.Pixels);

        DimensionValuedUnit topInPixels =
            new DimensionValuedUnit(DEFAULT_TOP_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL * viewportDimensions.HeightInPixels,
                DimensionUnitKind.Pixels);

        return new DimensionsRecord(widthInPixels, heightInPixels, leftInPixels, topInPixels);
    }
}
