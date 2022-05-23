# Goal
Add a Task Manager Service similar to the one found on a machine running the Windows operating system.

# Main Features
Display fire and forget tasks in separate lists. Those lists are:

- Active Tasks
- Seemingly Unresponsive Active Tasks
- Failed (Exception)
- Completed Successfully

Any Task that is still active is to have a way to
cancel that given task through the UI. Perhaps this comes in the form of a "Cancel" button for each active task displayed.

Any Task that is finished be it through failure or success is to have a way to requeue that given task through the UI. Perhaps this comes in the form of a "Requeue" button for each finished task displayed.

All lists containing Tasks must be entirely clearable of all entries. As well there is to be a clear button per task to remove only that singular task.

When enqueueing a task one is to be able to specify a period of time that specifies how long the task will remain in a list describing finished tasks.

When enqueueing a task one is to be able to specify a heartbeat function. That is to say, a function by which the task while executing can continually call the function to notify that a thread is not blocked unresponsively.

A Fire and Forget task throwining an exception is to NOT crash the application. It instead adds to a list of failed fire and forget tasks.

A Fire and Forget task should never block the UI thread. For example, if I purposefully enqueue an infinite loop as a EventCallback on a html element's onclick. The UI is to still function. Solely there will be an infinite loop in the background which the user will be able to identify through the task manager UI and cancel.

# Side Features
The Windows task manager is rather extensive relative to what is listed for this application "Main Features" section.

It is unlikely more than what is listed in "Main Features" will be added but it is acknowledged that more features could be added.