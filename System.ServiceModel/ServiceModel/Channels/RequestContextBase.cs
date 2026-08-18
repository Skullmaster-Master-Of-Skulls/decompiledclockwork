using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200076B RID: 1899
	internal abstract class RequestContextBase : RequestContext
	{
		// Token: 0x0600487B RID: 18555 RVA: 0x0010BDE7 File Offset: 0x00109FE7
		protected RequestContextBase(Message requestMessage, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout)
		{
			this.defaultSendTimeout = defaultSendTimeout;
			this.defaultCloseTimeout = defaultCloseTimeout;
			this.requestMessage = requestMessage;
		}

		// Token: 0x0600487C RID: 18556 RVA: 0x0010BE16 File Offset: 0x0010A016
		public void ReInitialize(Message requestMessage)
		{
			this.state = CommunicationState.Opened;
			this.requestMessageException = null;
			this.replySent = false;
			this.replyInitiated = false;
			this.aborted = false;
			this.requestMessage = requestMessage;
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x0600487D RID: 18557 RVA: 0x0010BE42 File Offset: 0x0010A042
		public override Message RequestMessage
		{
			get
			{
				if (this.requestMessageException != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.requestMessageException);
				}
				return this.requestMessage;
			}
		}

		// Token: 0x0600487E RID: 18558 RVA: 0x0010BE63 File Offset: 0x0010A063
		protected void SetRequestMessage(Message requestMessage)
		{
			this.requestMessage = requestMessage;
		}

		// Token: 0x0600487F RID: 18559 RVA: 0x0010BE6C File Offset: 0x0010A06C
		protected void SetRequestMessage(Exception requestMessageException)
		{
			this.requestMessageException = requestMessageException;
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06004880 RID: 18560 RVA: 0x0010BE75 File Offset: 0x0010A075
		protected bool ReplyInitiated
		{
			get
			{
				return this.replyInitiated;
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06004881 RID: 18561 RVA: 0x0010BE7D File Offset: 0x0010A07D
		protected object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06004882 RID: 18562 RVA: 0x0010BE85 File Offset: 0x0010A085
		public bool Aborted
		{
			get
			{
				return this.aborted;
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06004883 RID: 18563 RVA: 0x0010BE8D File Offset: 0x0010A08D
		public TimeSpan DefaultCloseTimeout
		{
			get
			{
				return this.defaultCloseTimeout;
			}
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06004884 RID: 18564 RVA: 0x0010BE95 File Offset: 0x0010A095
		public TimeSpan DefaultSendTimeout
		{
			get
			{
				return this.defaultSendTimeout;
			}
		}

		// Token: 0x06004885 RID: 18565 RVA: 0x0010BEA0 File Offset: 0x0010A0A0
		public override void Abort()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.state == CommunicationState.Closed)
				{
					return;
				}
				this.state = CommunicationState.Closing;
				this.aborted = true;
			}
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 262174, SR.GetString("TraceCodeRequestContextAbort"), this);
			}
			try
			{
				this.OnAbort();
			}
			finally
			{
				this.state = CommunicationState.Closed;
			}
		}

		// Token: 0x06004886 RID: 18566 RVA: 0x0010BF30 File Offset: 0x0010A130
		public override void Close()
		{
			this.Close(this.defaultCloseTimeout);
		}

		// Token: 0x06004887 RID: 18567 RVA: 0x0010BF40 File Offset: 0x0010A140
		public override void Close(TimeSpan timeout)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("ValueMustBeNonNegative")));
			}
			bool flag = false;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.state != CommunicationState.Opened)
				{
					return;
				}
				if (this.TryInitiateReply())
				{
					flag = true;
				}
				this.state = CommunicationState.Closing;
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			bool flag3 = true;
			try
			{
				if (flag)
				{
					this.OnReply(null, timeoutHelper.RemainingTime());
				}
				this.OnClose(timeoutHelper.RemainingTime());
				this.state = CommunicationState.Closed;
				flag3 = false;
			}
			finally
			{
				if (flag3)
				{
					this.Abort();
				}
			}
		}

		// Token: 0x06004888 RID: 18568 RVA: 0x0010C018 File Offset: 0x0010A218
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (!disposing)
			{
				return;
			}
			if (this.replySent)
			{
				this.Close();
				return;
			}
			this.Abort();
		}

		// Token: 0x06004889 RID: 18569
		protected abstract void OnAbort();

		// Token: 0x0600488A RID: 18570
		protected abstract void OnClose(TimeSpan timeout);

		// Token: 0x0600488B RID: 18571
		protected abstract void OnReply(Message message, TimeSpan timeout);

		// Token: 0x0600488C RID: 18572
		protected abstract IAsyncResult OnBeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x0600488D RID: 18573
		protected abstract void OnEndReply(IAsyncResult result);

		// Token: 0x0600488E RID: 18574 RVA: 0x0010C03C File Offset: 0x0010A23C
		protected void ThrowIfInvalidReply()
		{
			if (this.state == CommunicationState.Closed || this.state == CommunicationState.Closing)
			{
				if (this.aborted)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationObjectAbortedException(SR.GetString("RequestContextAborted")));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName));
			}
			else
			{
				if (this.replyInitiated)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ReplyAlreadySent")));
				}
				return;
			}
		}

		// Token: 0x0600488F RID: 18575 RVA: 0x0010C0BC File Offset: 0x0010A2BC
		protected bool TryInitiateReply()
		{
			object obj = this.thisLock;
			bool result;
			lock (obj)
			{
				if (this.state != CommunicationState.Opened || this.replyInitiated)
				{
					result = false;
				}
				else
				{
					this.replyInitiated = true;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06004890 RID: 18576 RVA: 0x0010C118 File Offset: 0x0010A318
		public override IAsyncResult BeginReply(Message message, AsyncCallback callback, object state)
		{
			return this.BeginReply(message, this.defaultSendTimeout, callback, state);
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x0010C12C File Offset: 0x0010A32C
		public override IAsyncResult BeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.ThrowIfInvalidReply();
				this.replyInitiated = true;
			}
			return this.OnBeginReply(message, timeout, callback, state);
		}

		// Token: 0x06004892 RID: 18578 RVA: 0x0010C180 File Offset: 0x0010A380
		public override void EndReply(IAsyncResult result)
		{
			this.OnEndReply(result);
			this.replySent = true;
		}

		// Token: 0x06004893 RID: 18579 RVA: 0x0010C190 File Offset: 0x0010A390
		public override void Reply(Message message)
		{
			this.Reply(message, this.defaultSendTimeout);
		}

		// Token: 0x06004894 RID: 18580 RVA: 0x0010C1A0 File Offset: 0x0010A3A0
		public override void Reply(Message message, TimeSpan timeout)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.ThrowIfInvalidReply();
				this.replyInitiated = true;
			}
			this.OnReply(message, timeout);
			this.replySent = true;
		}

		// Token: 0x06004895 RID: 18581 RVA: 0x0010C1F8 File Offset: 0x0010A3F8
		protected void SetReplySent()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.ThrowIfInvalidReply();
				this.replyInitiated = true;
			}
			this.replySent = true;
		}

		// Token: 0x04002DEA RID: 11754
		private TimeSpan defaultSendTimeout;

		// Token: 0x04002DEB RID: 11755
		private TimeSpan defaultCloseTimeout;

		// Token: 0x04002DEC RID: 11756
		private CommunicationState state = CommunicationState.Opened;

		// Token: 0x04002DED RID: 11757
		private Message requestMessage;

		// Token: 0x04002DEE RID: 11758
		private Exception requestMessageException;

		// Token: 0x04002DEF RID: 11759
		private bool replySent;

		// Token: 0x04002DF0 RID: 11760
		private bool replyInitiated;

		// Token: 0x04002DF1 RID: 11761
		private bool aborted;

		// Token: 0x04002DF2 RID: 11762
		private object thisLock = new object();
	}
}
