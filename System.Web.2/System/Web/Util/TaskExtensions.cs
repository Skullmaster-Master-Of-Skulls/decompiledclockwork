using System;
using System.Threading.Tasks;

namespace System.Web.Util
{
	// Token: 0x020001D0 RID: 464
	internal static class TaskExtensions
	{
		// Token: 0x06001775 RID: 6005 RVA: 0x000499CC File Offset: 0x00047BCC
		public static void ThrowIfFaulted(this Task task)
		{
			task.GetAwaiter().GetResult();
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x000499E7 File Offset: 0x00047BE7
		public static WithinCancellableCallbackTaskAwaitable WithinCancellableCallback(this Task task, HttpContext context)
		{
			return new WithinCancellableCallbackTaskAwaitable(context, task.GetAwaiter());
		}
	}
}
