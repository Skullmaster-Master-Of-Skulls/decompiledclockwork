using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200058E RID: 1422
	internal class MessageRpcInvokeNotification : IInvokeReceivedNotification
	{
		// Token: 0x060036D5 RID: 14037 RVA: 0x000D358A File Offset: 0x000D178A
		public MessageRpcInvokeNotification(ServiceModelActivity activity, ChannelHandler handler)
		{
			this.activity = activity;
			this.handler = handler;
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x060036D6 RID: 14038 RVA: 0x000D35A0 File Offset: 0x000D17A0
		// (set) Token: 0x060036D7 RID: 14039 RVA: 0x000D35A8 File Offset: 0x000D17A8
		public bool DidInvokerEnsurePump { get; set; }

		// Token: 0x060036D8 RID: 14040 RVA: 0x000D35B4 File Offset: 0x000D17B4
		public void NotifyInvokeReceived()
		{
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				ChannelHandler.Register(this.handler);
			}
			this.DidInvokerEnsurePump = true;
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x000D35FC File Offset: 0x000D17FC
		public void NotifyInvokeReceived(RequestContext request)
		{
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				ChannelHandler.Register(this.handler, request);
			}
			this.DidInvokerEnsurePump = true;
		}

		// Token: 0x040028F4 RID: 10484
		private ServiceModelActivity activity;

		// Token: 0x040028F5 RID: 10485
		private ChannelHandler handler;
	}
}
