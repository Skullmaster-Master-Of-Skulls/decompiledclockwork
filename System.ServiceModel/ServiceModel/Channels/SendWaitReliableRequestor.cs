using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000942 RID: 2370
	internal sealed class SendWaitReliableRequestor : ReliableRequestor
	{
		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06005B15 RID: 23317 RVA: 0x0014E3F7 File Offset: 0x0014C5F7
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06005B16 RID: 23318 RVA: 0x0014E3FF File Offset: 0x0014C5FF
		public override void Fault(CommunicationObject communicationObject)
		{
			this.replied = true;
			this.replyHandle.Fault(communicationObject);
			base.Fault(communicationObject);
		}

		// Token: 0x06005B17 RID: 23319 RVA: 0x0014E41B File Offset: 0x0014C61B
		public override WsrmMessageInfo GetInfo()
		{
			return this.replyInfo;
		}

		// Token: 0x06005B18 RID: 23320 RVA: 0x0014E424 File Offset: 0x0014C624
		private Message GetReply(bool last)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.replyInfo != null)
				{
					this.replied = true;
					return this.replyInfo.Message;
				}
				if (last)
				{
					this.replied = true;
				}
			}
			return null;
		}

		// Token: 0x06005B19 RID: 23321 RVA: 0x0014E488 File Offset: 0x0014C688
		private TimeSpan GetWaitTimeout(TimeSpan timeoutRemaining)
		{
			if (timeoutRemaining < ReliableMessagingConstants.RequestorReceiveTime)
			{
				return timeoutRemaining;
			}
			return ReliableMessagingConstants.RequestorReceiveTime;
		}

		// Token: 0x06005B1A RID: 23322 RVA: 0x0014E4A0 File Offset: 0x0014C6A0
		protected override Message OnRequest(Message request, TimeSpan timeout, bool last)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.Binder.Send(request, timeoutHelper.RemainingTime(), MaskingMode.None);
			TimeSpan waitTimeout = this.GetWaitTimeout(timeoutHelper.RemainingTime());
			this.replyHandle.Wait(waitTimeout);
			return this.GetReply(last);
		}

		// Token: 0x06005B1B RID: 23323 RVA: 0x0014E4EC File Offset: 0x0014C6EC
		protected override IAsyncResult OnBeginRequest(Message request, TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.request = request;
			return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, new OperationWithTimeoutBeginCallback[]
			{
				new OperationWithTimeoutBeginCallback(this.BeginSend),
				new OperationWithTimeoutBeginCallback(this.BeginWait)
			}, new OperationEndCallback[]
			{
				new OperationEndCallback(this.EndSend),
				new OperationEndCallback(this.EndWait)
			}, callback, state);
		}

		// Token: 0x06005B1C RID: 23324 RVA: 0x0014E551 File Offset: 0x0014C751
		protected override Message OnEndRequest(bool last, IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
			return this.GetReply(last);
		}

		// Token: 0x06005B1D RID: 23325 RVA: 0x0014E560 File Offset: 0x0014C760
		private IAsyncResult BeginSend(TimeSpan timeout, AsyncCallback callback, object state)
		{
			IAsyncResult result;
			try
			{
				result = base.Binder.BeginSend(this.request, timeout, MaskingMode.None, callback, state);
			}
			finally
			{
				this.request = null;
			}
			return result;
		}

		// Token: 0x06005B1E RID: 23326 RVA: 0x0014E5A0 File Offset: 0x0014C7A0
		private void EndSend(IAsyncResult result)
		{
			base.Binder.EndSend(result);
		}

		// Token: 0x06005B1F RID: 23327 RVA: 0x0014E5B0 File Offset: 0x0014C7B0
		public override void SetInfo(WsrmMessageInfo info)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.replied || this.replyInfo != null)
				{
					return;
				}
				this.replyInfo = info;
			}
			this.replyHandle.Set();
		}

		// Token: 0x06005B20 RID: 23328 RVA: 0x0014E610 File Offset: 0x0014C810
		private IAsyncResult BeginWait(TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeSpan waitTimeout = this.GetWaitTimeout(timeout);
			return this.replyHandle.BeginWait(waitTimeout, callback, state);
		}

		// Token: 0x06005B21 RID: 23329 RVA: 0x0014E633 File Offset: 0x0014C833
		private void EndWait(IAsyncResult result)
		{
			this.replyHandle.EndWait(result);
		}

		// Token: 0x040036D7 RID: 14039
		private bool replied;

		// Token: 0x040036D8 RID: 14040
		private InterruptibleWaitObject replyHandle = new InterruptibleWaitObject(false, true);

		// Token: 0x040036D9 RID: 14041
		private WsrmMessageInfo replyInfo;

		// Token: 0x040036DA RID: 14042
		private Message request;

		// Token: 0x040036DB RID: 14043
		private object thisLock = new object();
	}
}
