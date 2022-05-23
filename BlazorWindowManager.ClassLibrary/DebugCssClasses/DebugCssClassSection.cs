using System.Collections.Immutable;

namespace BlazorWindowManager.ClassLibrary.DebugCssClasses;

public record DebugCssClassSection
{
    private Dictionary<Guid, DebugCssClass> _debugCssClassMap;

    public DebugCssClassSection(string displayName, 
        string description,
        ImmutableArray<DebugCssClass> debugCssClasses)
    {
        DisplayName = displayName;
        Description = description;
        
        _debugCssClassMap = debugCssClasses
            .ToDictionary(x => x.DebugCssClassId, x => x);
    }
    
    public DebugCssClassSection(DebugCssClassSection otherDebugCssClassSection,
        Guid debugCssClassId)
    {
        DebugCssClassSectionId = otherDebugCssClassSection.DebugCssClassSectionId;
        
        DisplayName = otherDebugCssClassSection.DisplayName;
        Description = otherDebugCssClassSection.Description;
        
        _debugCssClassMap = new(otherDebugCssClassSection._debugCssClassMap);

        var previousDebugCssClass = _debugCssClassMap[debugCssClassId];

        _debugCssClassMap[debugCssClassId] = previousDebugCssClass with
        {
            IsActive = !previousDebugCssClass.IsActive
        };
    }
    
    public Guid DebugCssClassSectionId { get; } = Guid.NewGuid();
    public string DisplayName { get; }
    public string Description { get; }
    public ImmutableArray<DebugCssClass> DebugCssClasses => _debugCssClassMap.Values.ToImmutableArray();
}   
