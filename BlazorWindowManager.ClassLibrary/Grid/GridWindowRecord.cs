namespace BlazorWindowManager.ClassLibrary.Grid;

public record GridWindowRecord
{
    private readonly Dictionary<Guid, GridWindowTabRecord> _gridWindowTabRecordMap;

    public Guid GridWindowRecordId { get; } = Guid.NewGuid();


}
