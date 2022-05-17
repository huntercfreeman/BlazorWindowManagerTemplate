using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.ContextMenu;

public record ContextMenuGroupRecord(Guid ContextMenuGroupId);
public record ContextMenuDropdownRecord(Guid ContextMenuGroupId);
