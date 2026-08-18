using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000091 RID: 145
	[__DynamicallyInvokable]
	public static class TaskExtensions
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x0000A088 File Offset: 0x00008288
		[__DynamicallyInvokable]
		public static Task Unwrap(this Task<Task> task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			return Task.CreateUnwrapPromise<TaskExtensions.VoidResult>(task, false);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000A0AC File Offset: 0x000082AC
		[__DynamicallyInvokable]
		public static Task<TResult> Unwrap<TResult>(this Task<Task<TResult>> task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			return Task.CreateUnwrapPromise<TResult>(task, false);
		}

		// Token: 0x02000306 RID: 774
		private struct VoidResult
		{
		}
	}
}
