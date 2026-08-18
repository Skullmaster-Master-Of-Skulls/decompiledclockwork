using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000006 RID: 6
	internal static class TaskHelpers
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002988 File Offset: 0x00000B88
		internal static Task Canceled()
		{
			return TaskHelpers.CancelCache<TaskHelpers.AsyncVoid>.Canceled;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000298F File Offset: 0x00000B8F
		internal static Task<TResult> Canceled<TResult>()
		{
			return TaskHelpers.CancelCache<TResult>.Canceled;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002996 File Offset: 0x00000B96
		internal static Task Completed()
		{
			return TaskHelpers._defaultCompleted;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000299D File Offset: 0x00000B9D
		internal static Task FromError(Exception exception)
		{
			return TaskHelpers.FromError<TaskHelpers.AsyncVoid>(exception);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029A8 File Offset: 0x00000BA8
		internal static Task<TResult> FromError<TResult>(Exception exception)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			taskCompletionSource.SetException(exception);
			return taskCompletionSource.Task;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029C8 File Offset: 0x00000BC8
		internal static Task<object> NullResult()
		{
			return TaskHelpers._completedTaskReturningNull;
		}

		// Token: 0x04000005 RID: 5
		private static readonly Task _defaultCompleted = Task.FromResult<TaskHelpers.AsyncVoid>(default(TaskHelpers.AsyncVoid));

		// Token: 0x04000006 RID: 6
		private static readonly Task<object> _completedTaskReturningNull = Task.FromResult<object>(null);

		// Token: 0x02000007 RID: 7
		private struct AsyncVoid
		{
		}

		// Token: 0x02000008 RID: 8
		private static class CancelCache<TResult>
		{
			// Token: 0x06000047 RID: 71 RVA: 0x000029FC File Offset: 0x00000BFC
			private static Task<TResult> GetCancelledTask()
			{
				TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
				taskCompletionSource.SetCanceled();
				return taskCompletionSource.Task;
			}

			// Token: 0x04000007 RID: 7
			public static readonly Task<TResult> Canceled = TaskHelpers.CancelCache<TResult>.GetCancelledTask();
		}
	}
}
