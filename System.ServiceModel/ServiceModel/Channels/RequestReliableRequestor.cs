using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000940 RID: 2368
	internal sealed class RequestReliableRequestor : ReliableRequestor
	{
		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x06005B04 RID: 23300 RVA: 0x0014E208 File Offset: 0x0014C408
		private IClientReliableChannelBinder ClientBinder
		{
			get
			{
				return (IClientReliableChannelBinder)base.Binder;
			}
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x06005B05 RID: 23301 RVA: 0x0014E215 File Offset: 0x0014C415
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06005B06 RID: 23302 RVA: 0x0014E21D File Offset: 0x0014C41D
		public override WsrmMessageInfo GetInfo()
		{
			return this.replyInfo;
		}

		// Token: 0x06005B07 RID: 23303 RVA: 0x0014E228 File Offset: 0x0014C428
		private Message GetReply(Message reply, bool last)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (reply != null && this.replyInfo != null)
				{
					this.replyInfo = null;
				}
				else if (reply == null && this.replyInfo != null)
				{
					reply = this.replyInfo.Message;
				}
				if (reply != null || last)
				{
					this.replied = true;
				}
			}
			return reply;
		}

		// Token: 0x06005B08 RID: 23304 RVA: 0x0014E2A0 File Offset: 0x0014C4A0
		protected override Message OnRequest(Message request, TimeSpan timeout, bool last)
		{
			return this.GetReply(this.ClientBinder.Request(request, timeout, MaskingMode.None), last);
		}

		// Token: 0x06005B09 RID: 23305 RVA: 0x0014E2B7 File Offset: 0x0014C4B7
		protected override IAsyncResult OnBeginRequest(Message request, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.ClientBinder.BeginRequest(request, timeout, MaskingMode.None, callback, state);
		}

		// Token: 0x06005B0A RID: 23306 RVA: 0x0014E2CA File Offset: 0x0014C4CA
		protected override Message OnEndRequest(bool last, IAsyncResult result)
		{
			return this.GetReply(this.ClientBinder.EndRequest(result), last);
		}

		// Token: 0x06005B0B RID: 23307 RVA: 0x0014E2E0 File Offset: 0x0014C4E0
		public override void SetInfo(WsrmMessageInfo info)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (!this.replied && this.replyInfo == null)
				{
					this.replyInfo = info;
				}
			}
		}

		// Token: 0x040036D3 RID: 14035
		private bool replied;

		// Token: 0x040036D4 RID: 14036
		private WsrmMessageInfo replyInfo;

		// Token: 0x040036D5 RID: 14037
		private object thisLock = new object();
	}
}
