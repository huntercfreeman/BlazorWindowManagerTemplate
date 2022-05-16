using BlazorWindowManager.ClassLibrary.Theme;

namespace BlazorWindowManager.ClassLibrary.Theme;

public static class ColorKindConverter
{
    public static string ToCssClassString(this ColorKind colorKind) => colorKind switch
    {
        ColorKind.Primary => "bwmt_primary",
        ColorKind.Secondary => "bwmt_secondary",
        ColorKind.Success => "bwmt_success",
        ColorKind.Danger => "bwmt_danger",
        ColorKind.Warning => "bwmt_warning",
        ColorKind.Info => "bwmt_info",
        _ => ""
    };
}