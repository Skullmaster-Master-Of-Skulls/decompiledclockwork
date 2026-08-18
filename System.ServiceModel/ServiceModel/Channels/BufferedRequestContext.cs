using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200099B RID: 2459
	internal class BufferedRequestContext : RequestContext
	{
		// Token: 0x06005FF6 RID: 24566 RVA: 0x001661F0 File Offset: 0x001643F0
		public BufferedRequestContext(RequestContext requestContext)
		{
			this.innerRequestContext = requestContext;
			this.thisLock = new object();
		}

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x06005FF7 RID: 24567 RVA: 0x0016620A File Offset: 0x0016440A
		public override Message RequestMessage
		{
			get
			{
				return this.innerRequestContext.RequestMessage;
			}
		}

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x06005FF8 RID: 24568 RVA: 0x00166217 File Offset: 0x00164417
		public RequestContext InnerRequestContext
		{
			get
			{
				return this.innerRequestContext;
			}
		}

		// Token: 0x06005FF9 RID: 24569 RVA: 0x00166220 File Offset: 0x00164420
		public void DelayClose(bool delay)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.delayClose = delay;
			}
		}

		// Token: 0x06005FFA RID: 24570 RVA: 0x00166264 File Offset: 0x00164464
		public void ReInitialize(Message requestMessage)
		{
			RequestContextBase requestContextBase = this.innerRequestContext as RequestContextBase;
			if (requestContextBase != null)
			{
				requestContextBase.ReInitialize(requestMessage);
			}
		}

		// Token: 0x06005FFB RID: 24571 RVA: 0x00166288 File Offset: 0x00164488
		public override void Abort()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.delayClose)
				{
					this.delayClose = false;
					return;
				}
			}
			this.innerRequestContext.Abort();
		}

		// Token: 0x06005FFC RID: 24572 RVA: 0x001662E0 File Offset: 0x001644E0
		public override void Close()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.delayClose)
				{
					this.delayClose = false;
					return;
				}
			}
			this.innerRequestContext.Close();
		}

		// Token: 0x06005FFD RID: 24573 RVA: 0x00166338 File Offset: 0x00164538
		public override void Close(TimeSpan timeout)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.delayClose)
				{
					this.delayClose = false;
					return;
				}
			}
			this.innerRequestContext.Close(timeout);
		}

		// Token: 0x06005FFE RID: 24574 RVA: 0x00166390 File Offset: 0x00164590
		public override void Reply(Message message)
		{
			this.innerRequestContext.Reply(message);
		}

		// Token: 0x06005FFF RID: 24575 RVA: 0x0016639E File Offset: 0x0016459E
		public override void Reply(Message message, TimeSpan timeout)
		{
			this.innerRequestContext.Reply(message, timeout);
		}

		// Token: 0x06006000 RID: 24576 RVA: 0x001663AD File Offset: 0x001645AD
		public override IAsyncResult BeginReply(Message message, AsyncCallback callback, object state)
		{
			return this.innerRequestContext.BeginReply(message, callback, state);
		}

		// Token: 0x06006001 RID: 24577 RVA: 0x001663BD File Offset: 0x001645BD
		public override IAsyncResult BeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerRequestContext.BeginReply(message, timeout, callback, state);
		}

		// Token: 0x06006002 RID: 24578 RVA: 0x001663CF File Offset: 0x001645CF
		public override void EndReply(IAsyncResult result)
		{
			this.innerRequestContext.EndReply(result);
		}

		// Token: 0x04003867 RID: 14439
		private bool delayClose;

		// Token: 0x04003868 RID: 14440
		private object thisLock;

		// Token: 0x04003869 RID: 14441
		private RequestContext innerRequestContext;
	}
}
