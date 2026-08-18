using System;
using System.Diagnostics;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000759 RID: 1881
	internal abstract class OutputChannel : ChannelBase, IOutputChannel, IChannel, ICommunicationObject
	{
		// Token: 0x060047D6 RID: 18390 RVA: 0x0010A4B0 File Offset: 0x001086B0
		protected OutputChannel(ChannelManagerBase manager) : base(manager)
		{
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x060047D7 RID: 18391
		public abstract EndpointAddress RemoteAddress { get; }

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x060047D8 RID: 18392
		public abstract Uri Via { get; }

		// Token: 0x060047D9 RID: 18393 RVA: 0x0010A4B9 File Offset: 0x001086B9
		public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
		{
			return this.BeginSend(message, base.DefaultSendTimeout, callback, state);
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x0010A4CC File Offset: 0x001086CC
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowIfDisposedOrNotOpen();
			this.AddHeadersTo(message);
			this.EmitTrace(message);
			return this.OnBeginSend(message, timeout, callback, state);
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x0010A53D File Offset: 0x0010873D
		public void EndSend(IAsyncResult result)
		{
			this.OnEndSend(result);
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x0010A548 File Offset: 0x00108748
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IOutputChannel))
			{
				return (T)((object)this);
			}
			T property = base.GetProperty<T>();
			if (property != null)
			{
				return property;
			}
			return default(T);
		}

		// Token: 0x060047DD RID: 18397
		protected abstract void OnSend(Message message, TimeSpan timeout);

		// Token: 0x060047DE RID: 18398
		protected abstract IAsyncResult OnBeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060047DF RID: 18399
		protected abstract void OnEndSend(IAsyncResult result);

		// Token: 0x060047E0 RID: 18400 RVA: 0x0010A591 File Offset: 0x00108791
		public void Send(Message message)
		{
			this.Send(message, base.DefaultSendTimeout);
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x0010A5A0 File Offset: 0x001087A0
		public void Send(Message message, TimeSpan timeout)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowIfDisposedOrNotOpen();
			this.AddHeadersTo(message);
			this.EmitTrace(message);
			this.OnSend(message, timeout);
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x0010A60E File Offset: 0x0010880E
		protected virtual TraceRecord CreateSendTrace(Message message)
		{
			return MessageTransmitTraceRecord.CreateSendTraceRecord(message, this.RemoteAddress);
		}

		// Token: 0x060047E3 RID: 18403 RVA: 0x0010A61C File Offset: 0x0010881C
		private void EmitTrace(Message message)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262164, SR.GetString("TraceCodeMessageSent"), this.CreateSendTrace(message), this, null);
			}
		}

		// Token: 0x060047E4 RID: 18404 RVA: 0x0010A643 File Offset: 0x00108843
		protected virtual void AddHeadersTo(Message message)
		{
		}
	}
}
