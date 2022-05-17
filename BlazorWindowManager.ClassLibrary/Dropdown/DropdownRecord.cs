using System.Collections.Immutable;

namespace BlazorWindowManager.ClassLibrary.Dropdown;

public record DropdownRecord
{
    private readonly List<DropdownRecord> _childDropdownRecords;

    public DropdownRecord(bool shouldDisplayChildren,
        IEnumerable<DropdownRecord> childDropdownRecords,
        string dropdownRecordDisplayName)
    {
        _childDropdownRecords = childDropdownRecords.ToList();
        ShouldDisplayChildren = shouldDisplayChildren;
        DropdownRecordDisplayName = dropdownRecordDisplayName;
    }

    public ImmutableArray<DropdownRecord> ChildDropdownRecords => _childDropdownRecords
        .ToImmutableArray();

    public Guid DropdownRecordId { get; } = Guid.NewGuid();
    public bool ShouldDisplayChildren { get; }
    public string DropdownRecordDisplayName { get; }
}