namespace BlazorWindowManager.ClassLibrary.Grid;

// Is ShouldRerenderPredicate necessary?
public record GridWindowTabRecord(Guid GridRecordId, 
    Type RenderedContentType, 
    string GridWindowTabDisplayName,
    Func<bool> ShouldRerenderPredicate);