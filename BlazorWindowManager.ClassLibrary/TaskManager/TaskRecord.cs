using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWindowManager.ClassLibrary.TaskManager;

public record TaskRecord(Guid TaskRecordId, 
    string TaskName,
    Func<CancellationToken, Task> TaskFunc,
    CancellationToken CancellationToken,
    Action CancelCancellationTokenSourceAction);
