using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000945 RID: 2373
	internal class ReliableBinderRequestAsyncResult : ReliableOutputAsyncResult
	{
		// Token: 0x06005B3A RID: 23354 RVA: 0x0014E89A File Offset: 0x0014CA9A
		public ReliableBinderRequestAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
		}

		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06005B3B RID: 23355 RVA: 0x0014E8A4 File Offset: 0x0014CAA4
		protected IClientReliableChannelBinder ClientBinder
		{
			get
			{
				return (IClientReliableChannelBinder)base.Binder;
			}
		}

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06005B3C RID: 23356 RVA: 0x0014E8B1 File Offset: 0x0014CAB1
		protected Message Reply
		{
			get
			{
				return this.reply;
			}
		}

		// Token: 0x06005B3D RID: 23357 RVA: 0x0014E8BC File Offset: 0x0014CABC
		public static Message End(IAsyncResult result)
		{
			Exception ex;
			return ReliableBinderRequestAsyncResult.End(result, out ex);
		}

		// Token: 0x06005B3E RID: 23358 RVA: 0x0014E8D4 File Offset: 0x0014CAD4
		public static Message End(IAsyncResult result, out Exception handledException)
		{
			ReliableBinderRequestAsyncResult reliableBinderRequestAsyncResult = AsyncResult.End<ReliableBinderRequestAsyncResult>(result);
			handledException = reliableBinderRequestAsyncResult.HandledException;
			return reliableBinderRequestAsyncResult.reply;
		}

		// Token: 0x06005B3F RID: 23359 RVA: 0x0014E8F6 File Offset: 0x0014CAF6
		protected override IAsyncResult BeginOperation(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.ClientBinder.BeginRequest(base.Message, timeout, base.MaskingMode, callback, state);
		}

		// Token: 0x06005B40 RID: 23360 RVA: 0x0014E912 File Offset: 0x0014CB12
		protected override void EndOperation(IAsyncResult result)
		{
			this.reply = this.ClientBinder.EndRequest(result);
		}

		// Token: 0x040036E2 RID: 14050
		private Message reply;
	}
}
