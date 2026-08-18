using System;
using System.Threading.Tasks;

namespace System.Web
{
	// Token: 0x0200004C RID: 76
	internal static class TaskAsyncHelper
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x00007794 File Offset: 0x00005994
		internal static IAsyncResult BeginTask(Func<Task> taskFunc, AsyncCallback callback, object state)
		{
			Task task = taskFunc();
			if (task == null)
			{
				return null;
			}
			TaskWrapperAsyncResult resultToReturn = new TaskWrapperAsyncResult(task, state);
			bool isCompleted = task.IsCompleted;
			if (isCompleted)
			{
				resultToReturn.ForceCompletedSynchronously();
			}
			if (callback != null)
			{
				if (isCompleted)
				{
					callback(resultToReturn);
				}
				else
				{
					task.ContinueWith(delegate(Task _)
					{
						callback(resultToReturn);
					});
				}
			}
			return resultToReturn;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00007814 File Offset: 0x00005A14
		internal static void EndTask(IAsyncResult ar)
		{
			if (ar == null)
			{
				throw new ArgumentNullException("ar");
			}
			TaskWrapperAsyncResult taskWrapperAsyncResult = ar as TaskWrapperAsyncResult;
			if (taskWrapperAsyncResult == null)
			{
				throw new ArgumentException(SR.GetString("TaskAsyncHelper_ParameterInvalid"), "ar");
			}
			taskWrapperAsyncResult.Task.GetAwaiter().GetResult();
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00007861 File Offset: 0x00005A61
		internal static Task CompletedTask
		{
			get
			{
				return TaskAsyncHelper.s_completedTask;
			}
		}

		// Token: 0x0400014D RID: 333
		private static readonly Task s_completedTask = Task.FromResult<object>(null);
	}
}
