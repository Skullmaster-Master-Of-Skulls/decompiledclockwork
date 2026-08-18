using System;

namespace System.Runtime
{
	// Token: 0x02000012 RID: 18
	internal class CompletedAsyncResult<TResult, TParameter> : AsyncResult
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003686 File Offset: 0x00001886
		public CompletedAsyncResult(TResult resultData, TParameter parameter, AsyncCallback callback, object state) : base(callback, state)
		{
			this.resultData = resultData;
			this.parameter = parameter;
			base.Complete(true);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000036A8 File Offset: 0x000018A8
		public static TResult End(IAsyncResult result, out TParameter parameter)
		{
			Fx.AssertAndThrowFatal(result.IsCompleted, "CompletedAsyncResult<T> was not completed!");
			CompletedAsyncResult<TResult, TParameter> completedAsyncResult = AsyncResult.End<CompletedAsyncResult<TResult, TParameter>>(result);
			parameter = completedAsyncResult.parameter;
			return completedAsyncResult.resultData;
		}

		// Token: 0x0400003E RID: 62
		private TResult resultData;

		// Token: 0x0400003F RID: 63
		private TParameter parameter;
	}
}
