using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000944 RID: 2372
	internal class ReliableBinderSendAsyncResult : ReliableOutputAsyncResult
	{
		// Token: 0x06005B35 RID: 23349 RVA: 0x0014E832 File Offset: 0x0014CA32
		public ReliableBinderSendAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
		}

		// Token: 0x06005B36 RID: 23350 RVA: 0x0014E83C File Offset: 0x0014CA3C
		public static void End(IAsyncResult result)
		{
			Exception ex;
			ReliableBinderSendAsyncResult.End(result, out ex);
		}

		// Token: 0x06005B37 RID: 23351 RVA: 0x0014E854 File Offset: 0x0014CA54
		public static void End(IAsyncResult result, out Exception handledException)
		{
			ReliableBinderSendAsyncResult reliableBinderSendAsyncResult = AsyncResult.End<ReliableBinderSendAsyncResult>(result);
			handledException = reliableBinderSendAsyncResult.HandledException;
		}

		// Token: 0x06005B38 RID: 23352 RVA: 0x0014E870 File Offset: 0x0014CA70
		protected override IAsyncResult BeginOperation(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.Binder.BeginSend(base.Message, timeout, base.MaskingMode, callback, state);
		}

		// Token: 0x06005B39 RID: 23353 RVA: 0x0014E88C File Offset: 0x0014CA8C
		protected override void EndOperation(IAsyncResult result)
		{
			base.Binder.EndSend(result);
		}
	}
}
