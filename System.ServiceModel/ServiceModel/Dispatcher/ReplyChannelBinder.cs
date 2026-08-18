using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200059C RID: 1436
	internal class ReplyChannelBinder : IChannelBinder
	{
		// Token: 0x060037A7 RID: 14247 RVA: 0x000D6B9B File Offset: 0x000D4D9B
		internal ReplyChannelBinder(IReplyChannel channel, Uri listenUri)
		{
			if (channel == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channel");
			}
			this.channel = channel;
			this.listenUri = listenUri;
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x000D6BC4 File Offset: 0x000D4DC4
		public IChannel Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x060037A9 RID: 14249 RVA: 0x000D6BCC File Offset: 0x000D4DCC
		public bool HasSession
		{
			get
			{
				return this.channel is ISessionChannel<IInputSession>;
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x000D6BDC File Offset: 0x000D4DDC
		public Uri ListenUri
		{
			get
			{
				return this.listenUri;
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x060037AB RID: 14251 RVA: 0x000D6BE4 File Offset: 0x000D4DE4
		public EndpointAddress LocalAddress
		{
			get
			{
				return this.channel.LocalAddress;
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x060037AC RID: 14252 RVA: 0x000D6BF1 File Offset: 0x000D4DF1
		public EndpointAddress RemoteAddress
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000D6C02 File Offset: 0x000D4E02
		public void Abort()
		{
			this.channel.Abort();
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000D6C0F File Offset: 0x000D4E0F
		public void CloseAfterFault(TimeSpan timeout)
		{
			this.channel.Close(timeout);
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000D6C1D File Offset: 0x000D4E1D
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channel.BeginTryReceiveRequest(timeout, callback, state);
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000D6C2D File Offset: 0x000D4E2D
		public bool EndTryReceive(IAsyncResult result, out RequestContext requestContext)
		{
			return this.channel.EndTryReceiveRequest(result, out requestContext);
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000D6C3C File Offset: 0x000D4E3C
		public RequestContext CreateRequestContext(Message message)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000D6C4D File Offset: 0x000D4E4D
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw TraceUtility.ThrowHelperError(new NotImplementedException(), message);
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000D6C5A File Offset: 0x000D4E5A
		public void EndSend(IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000D6C6B File Offset: 0x000D4E6B
		public void Send(Message message, TimeSpan timeout)
		{
			throw TraceUtility.ThrowHelperError(new NotImplementedException(), message);
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000D6C78 File Offset: 0x000D4E78
		public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw TraceUtility.ThrowHelperError(new NotImplementedException(), message);
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000D6C85 File Offset: 0x000D4E85
		public Message EndRequest(IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000D6C96 File Offset: 0x000D4E96
		public bool TryReceive(TimeSpan timeout, out RequestContext requestContext)
		{
			return this.channel.TryReceiveRequest(timeout, out requestContext);
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000D6CA5 File Offset: 0x000D4EA5
		public Message Request(Message message, TimeSpan timeout)
		{
			throw TraceUtility.ThrowHelperError(new NotImplementedException(), message);
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000D6CB2 File Offset: 0x000D4EB2
		public bool WaitForMessage(TimeSpan timeout)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x000D6CC3 File Offset: 0x000D4EC3
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000D6CD4 File Offset: 0x000D4ED4
		public bool EndWaitForMessage(IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x04002965 RID: 10597
		private IReplyChannel channel;

		// Token: 0x04002966 RID: 10598
		private Uri listenUri;
	}
}
