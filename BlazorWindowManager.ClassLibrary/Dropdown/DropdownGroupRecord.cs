using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.Dropdown;

public record DropdownGroupRecord(bool ShouldDisplayDropdownRecords)
{
    private readonly List<DropdownRecord> _dropdownRecords;

    public DropdownGroupRecord(bool shouldDisplayDropdownRecords,
        IEnumerable<DropdownRecord> dropdownRecords) 
            : this(shouldDisplayDropdownRecords)
    {
        _dropdownRecords = dropdownRecords.ToList();
    }

    public ImmutableArray<DropdownRecord> DropdownRecords => _dropdownRecords
        .ToImmutableArray();

    public Guid DropdownGroupRecordId { get; } = Guid.NewGuid();
}
