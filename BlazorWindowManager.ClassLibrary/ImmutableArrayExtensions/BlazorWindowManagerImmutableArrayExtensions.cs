using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.ImmutableArrayExtensions;

public static class BlazorWindowManagerImmutableArrayExtensions
{
    public static ImmutableArray<ImmutableArray<T>> ConvertToImmutable<T>(IEnumerable<IEnumerable<T>> items)
    {
        List<ImmutableArray<T>> temporaryRows = new();

        foreach (var row in items)
        {
            List<T> temporaryRow = new();

            foreach (var gridItemRecord in row)
            {
                temporaryRow.Add(gridItemRecord);
            }

            temporaryRows.Add(temporaryRow.ToImmutableArray());
        }

        return temporaryRows.ToImmutableArray();
    }
}
