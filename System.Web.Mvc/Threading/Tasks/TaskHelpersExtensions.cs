using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000014 RID: 20
	internal static class TaskHelpersExtensions
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003A14 File Offset: 0x00001C14
		internal static async Task<object> CastToObject(this Task task)
		{
			await task;
			return null;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003B30 File Offset: 0x00001D30
		internal static async Task<object> CastToObject<T>(this Task<T> task)
		{
			return await task;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003B78 File Offset: 0x00001D78
		internal static void ThrowIfFaulted(this Task task)
		{
			task.GetAwaiter().GetResult();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003B93 File Offset: 0x00001D93
		internal static bool TryGetResult<TResult>(this Task<TResult> task, out TResult result)
		{
			if (task.Status == TaskStatus.RanToCompletion)
			{
				result = task.Result;
				return true;
			}
			result = default(TResult);
			return false;
		}
	}
}
