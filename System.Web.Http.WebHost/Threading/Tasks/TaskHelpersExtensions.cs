using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000009 RID: 9
	internal static class TaskHelpersExtensions
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002AF8 File Offset: 0x00000CF8
		internal static async Task<object> CastToObject(this Task task)
		{
			await task;
			return null;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C14 File Offset: 0x00000E14
		internal static async Task<object> CastToObject<T>(this Task<T> task)
		{
			return await task;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C5C File Offset: 0x00000E5C
		internal static void ThrowIfFaulted(this Task task)
		{
			task.GetAwaiter().GetResult();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C77 File Offset: 0x00000E77
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
