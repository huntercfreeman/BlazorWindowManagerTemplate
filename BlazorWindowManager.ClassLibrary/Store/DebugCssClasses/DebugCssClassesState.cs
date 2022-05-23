using BlazorWindowManager.ClassLibrary.DebugCssClasses;
using Fluxor;

namespace BlazorWindowManager.ClassLibrary.Store.DebugCssClasses;

[FeatureState]
public record DebugCssClassesState
{
    private readonly Dictionary<Guid, DebugCssClassSection> _debugCssClassSectionMap;

    public DebugCssClassesState()
    {
        _debugCssClassSectionMap = new();

        foreach (var debugCssClassSection in DebugCssClassInitialStates.DebugCssClassSectionInitalStates)
        {
            _debugCssClassSectionMap.Add(debugCssClassSection.DebugCssClassSectionId, debugCssClassSection);
        }
    }
    
    public DebugCssClassesState(DebugCssClassesState otherDebugCssClassesState, 
        Guid debugCssClassSectionId,
        Guid debugCssClassId)
    {
        _debugCssClassSectionMap = new(otherDebugCssClassesState._debugCssClassSectionMap);

        var previousDebugCssClassSection = _debugCssClassSectionMap[debugCssClassSectionId];

        _debugCssClassSectionMap[debugCssClassSectionId] =
            new DebugCssClassSection(previousDebugCssClassSection,
                debugCssClassId);
    }

    public DebugCssClassSection LookUpDebugCssClassSection(Guid debugCssClassSectionId)
    {
        return _debugCssClassSectionMap[debugCssClassSectionId];
    }
}