using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000007 RID: 7
	internal static class TaskHelpers
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002C3C File Offset: 0x00000E3C
		internal static Task Canceled()
		{
			return TaskHelpers.CancelCache<TaskHelpers.AsyncVoid>.Canceled;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C43 File Offset: 0x00000E43
		internal static Task<TResult> Canceled<TResult>()
		{
			return TaskHelpers.CancelCache<TResult>.Canceled;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C4A File Offset: 0x00000E4A
		internal static Task Completed()
		{
			return TaskHelpers._defaultCompleted;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C51 File Offset: 0x00000E51
		internal static Task FromError(Exception exception)
		{
			return TaskHelpers.FromError<TaskHelpers.AsyncVoid>(exception);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C5C File Offset: 0x00000E5C
		internal static Task<TResult> FromError<TResult>(Exception exception)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			taskCompletionSource.SetException(exception);
			return taskCompletionSource.Task;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C7C File Offset: 0x00000E7C
		internal static Task<object> NullResult()
		{
			return TaskHelpers._completedTaskReturningNull;
		}

		// Token: 0x04000006 RID: 6
		private static readonly Task _defaultCompleted = Task.FromResult<TaskHelpers.AsyncVoid>(default(TaskHelpers.AsyncVoid));

		// Token: 0x04000007 RID: 7
		private static readonly Task<object> _completedTaskReturningNull = Task.FromResult<object>(null);

		// Token: 0x02000008 RID: 8
		private struct AsyncVoid
		{
		}

		// Token: 0x02000009 RID: 9
		private static class CancelCache<TResult>
		{
			// Token: 0x06000056 RID: 86 RVA: 0x00002CB0 File Offset: 0x00000EB0
			private static Task<TResult> GetCancelledTask()
			{
				TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
				taskCompletionSource.SetCanceled();
				return taskCompletionSource.Task;
			}

			// Token: 0x04000008 RID: 8
			public static readonly Task<TResult> Canceled = TaskHelpers.CancelCache<TResult>.GetCancelledTask();
		}
	}
}
