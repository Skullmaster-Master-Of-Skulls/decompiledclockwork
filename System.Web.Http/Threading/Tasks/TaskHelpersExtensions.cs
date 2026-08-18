using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200001A RID: 26
	internal static class TaskHelpersExtensions
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x000040B8 File Offset: 0x000022B8
		internal static async Task<object> CastToObject(this Task task)
		{
			await task;
			return null;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000041D4 File Offset: 0x000023D4
		internal static async Task<object> CastToObject<T>(this Task<T> task)
		{
			return await task;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000421C File Offset: 0x0000241C
		internal static void ThrowIfFaulted(this Task task)
		{
			task.GetAwaiter().GetResult();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004237 File Offset: 0x00002437
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
