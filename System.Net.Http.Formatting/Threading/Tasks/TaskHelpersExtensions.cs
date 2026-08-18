using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000008 RID: 8
	internal static class TaskHelpersExtensions
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002774 File Offset: 0x00000974
		internal static async Task<object> CastToObject(this Task task)
		{
			await task;
			return null;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002890 File Offset: 0x00000A90
		internal static async Task<object> CastToObject<T>(this Task<T> task)
		{
			return await task;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028D8 File Offset: 0x00000AD8
		internal static void ThrowIfFaulted(this Task task)
		{
			task.GetAwaiter().GetResult();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028F3 File Offset: 0x00000AF3
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
