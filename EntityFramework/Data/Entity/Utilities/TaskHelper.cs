using System;
using System.Threading.Tasks;

namespace System.Data.Entity.Utilities
{
	// Token: 0x020006EE RID: 1774
	internal static class TaskHelper
	{
		// Token: 0x06004727 RID: 18215 RVA: 0x00150CD0 File Offset: 0x0014EED0
		internal static Task<T> FromException<T>(Exception ex)
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			taskCompletionSource.SetException(ex);
			return taskCompletionSource.Task;
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x00150CF0 File Offset: 0x0014EEF0
		internal static Task<T> FromCancellation<T>()
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			taskCompletionSource.SetCanceled();
			return taskCompletionSource.Task;
		}
	}
}
