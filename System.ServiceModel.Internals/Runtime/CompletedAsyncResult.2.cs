using System;

namespace System.Runtime
{
	// Token: 0x02000011 RID: 17
	internal class CompletedAsyncResult<T> : AsyncResult
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00003644 File Offset: 0x00001844
		public CompletedAsyncResult(T data, AsyncCallback callback, object state) : base(callback, state)
		{
			this.data = data;
			base.Complete(true);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000365C File Offset: 0x0000185C
		public static T End(IAsyncResult result)
		{
			Fx.AssertAndThrowFatal(result.IsCompleted, "CompletedAsyncResult<T> was not completed!");
			CompletedAsyncResult<T> completedAsyncResult = AsyncResult.End<CompletedAsyncResult<T>>(result);
			return completedAsyncResult.data;
		}

		// Token: 0x0400003D RID: 61
		private T data;
	}
}
