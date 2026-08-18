using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200000A RID: 10
	internal static class TaskHelpersExtensions
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002DAC File Offset: 0x00000FAC
		internal static async Task<object> CastToObject(this Task task)
		{
			await task;
			return null;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002EC8 File Offset: 0x000010C8
		internal static async Task<object> CastToObject<T>(this Task<T> task)
		{
			return await task;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F10 File Offset: 0x00001110
		internal static void ThrowIfFaulted(this Task task)
		{
			task.GetAwaiter().GetResult();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002F2B File Offset: 0x0000112B
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
