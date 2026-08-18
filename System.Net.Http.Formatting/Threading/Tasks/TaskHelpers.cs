using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000005 RID: 5
	internal static class TaskHelpers
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002605 File Offset: 0x00000805
		internal static Task Canceled()
		{
			return TaskHelpers.CancelCache<TaskHelpers.AsyncVoid>.Canceled;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000260C File Offset: 0x0000080C
		internal static Task<TResult> Canceled<TResult>()
		{
			return TaskHelpers.CancelCache<TResult>.Canceled;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002613 File Offset: 0x00000813
		internal static Task Completed()
		{
			return TaskHelpers._defaultCompleted;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000261A File Offset: 0x0000081A
		internal static Task FromError(Exception exception)
		{
			return TaskHelpers.FromError<TaskHelpers.AsyncVoid>(exception);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002624 File Offset: 0x00000824
		internal static Task<TResult> FromError<TResult>(Exception exception)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			taskCompletionSource.SetException(exception);
			return taskCompletionSource.Task;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002644 File Offset: 0x00000844
		internal static Task<object> NullResult()
		{
			return TaskHelpers._completedTaskReturningNull;
		}

		// Token: 0x04000004 RID: 4
		private static readonly Task _defaultCompleted = Task.FromResult<TaskHelpers.AsyncVoid>(default(TaskHelpers.AsyncVoid));

		// Token: 0x04000005 RID: 5
		private static readonly Task<object> _completedTaskReturningNull = Task.FromResult<object>(null);

		// Token: 0x02000006 RID: 6
		private struct AsyncVoid
		{
		}

		// Token: 0x02000007 RID: 7
		private static class CancelCache<TResult>
		{
			// Token: 0x0600002E RID: 46 RVA: 0x00002678 File Offset: 0x00000878
			private static Task<TResult> GetCancelledTask()
			{
				TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
				taskCompletionSource.SetCanceled();
				return taskCompletionSource.Task;
			}

			// Token: 0x04000006 RID: 6
			public static readonly Task<TResult> Canceled = TaskHelpers.CancelCache<TResult>.GetCancelledTask();
		}
	}
}
