using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000011 RID: 17
	internal static class TaskHelpers
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000038A6 File Offset: 0x00001AA6
		internal static Task Canceled()
		{
			return TaskHelpers.CancelCache<TaskHelpers.AsyncVoid>.Canceled;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000038AD File Offset: 0x00001AAD
		internal static Task<TResult> Canceled<TResult>()
		{
			return TaskHelpers.CancelCache<TResult>.Canceled;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000038B4 File Offset: 0x00001AB4
		internal static Task Completed()
		{
			return TaskHelpers._defaultCompleted;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000038BB File Offset: 0x00001ABB
		internal static Task FromError(Exception exception)
		{
			return TaskHelpers.FromError<TaskHelpers.AsyncVoid>(exception);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000038C4 File Offset: 0x00001AC4
		internal static Task<TResult> FromError<TResult>(Exception exception)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			taskCompletionSource.SetException(exception);
			return taskCompletionSource.Task;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000038E4 File Offset: 0x00001AE4
		internal static Task<object> NullResult()
		{
			return TaskHelpers._completedTaskReturningNull;
		}

		// Token: 0x0400001E RID: 30
		private static readonly Task _defaultCompleted = Task.FromResult<TaskHelpers.AsyncVoid>(default(TaskHelpers.AsyncVoid));

		// Token: 0x0400001F RID: 31
		private static readonly Task<object> _completedTaskReturningNull = Task.FromResult<object>(null);

		// Token: 0x02000012 RID: 18
		private struct AsyncVoid
		{
		}

		// Token: 0x02000013 RID: 19
		private static class CancelCache<TResult>
		{
			// Token: 0x0600007A RID: 122 RVA: 0x00003918 File Offset: 0x00001B18
			private static Task<TResult> GetCancelledTask()
			{
				TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
				taskCompletionSource.SetCanceled();
				return taskCompletionSource.Task;
			}

			// Token: 0x04000020 RID: 32
			public static readonly Task<TResult> Canceled = TaskHelpers.CancelCache<TResult>.GetCancelledTask();
		}
	}
}
