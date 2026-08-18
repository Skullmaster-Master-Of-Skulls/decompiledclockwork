using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000017 RID: 23
	internal static class TaskHelpers
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00003F4A File Offset: 0x0000214A
		internal static Task Canceled()
		{
			return TaskHelpers.CancelCache<TaskHelpers.AsyncVoid>.Canceled;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003F51 File Offset: 0x00002151
		internal static Task<TResult> Canceled<TResult>()
		{
			return TaskHelpers.CancelCache<TResult>.Canceled;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003F58 File Offset: 0x00002158
		internal static Task Completed()
		{
			return TaskHelpers._defaultCompleted;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003F5F File Offset: 0x0000215F
		internal static Task FromError(Exception exception)
		{
			return TaskHelpers.FromError<TaskHelpers.AsyncVoid>(exception);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003F68 File Offset: 0x00002168
		internal static Task<TResult> FromError<TResult>(Exception exception)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			taskCompletionSource.SetException(exception);
			return taskCompletionSource.Task;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003F88 File Offset: 0x00002188
		internal static Task<object> NullResult()
		{
			return TaskHelpers._completedTaskReturningNull;
		}

		// Token: 0x04000025 RID: 37
		private static readonly Task _defaultCompleted = Task.FromResult<TaskHelpers.AsyncVoid>(default(TaskHelpers.AsyncVoid));

		// Token: 0x04000026 RID: 38
		private static readonly Task<object> _completedTaskReturningNull = Task.FromResult<object>(null);

		// Token: 0x02000018 RID: 24
		private struct AsyncVoid
		{
		}

		// Token: 0x02000019 RID: 25
		private static class CancelCache<TResult>
		{
			// Token: 0x060000A2 RID: 162 RVA: 0x00003FBC File Offset: 0x000021BC
			private static Task<TResult> GetCancelledTask()
			{
				TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
				taskCompletionSource.SetCanceled();
				return taskCompletionSource.Task;
			}

			// Token: 0x04000027 RID: 39
			public static readonly Task<TResult> Canceled = TaskHelpers.CancelCache<TResult>.GetCancelledTask();
		}
	}
}
