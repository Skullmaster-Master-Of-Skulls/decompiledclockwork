using System;

namespace System.Runtime
{
	// Token: 0x02000010 RID: 16
	internal class CompletedAsyncResult : AsyncResult
	{
		// Token: 0x06000079 RID: 121 RVA: 0x0000361A File Offset: 0x0000181A
		public CompletedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
			base.Complete(true);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000362B File Offset: 0x0000182B
		public static void End(IAsyncResult result)
		{
			Fx.AssertAndThrowFatal(result.IsCompleted, "CompletedAsyncResult was not completed!");
			AsyncResult.End<CompletedAsyncResult>(result);
		}
	}
}
