using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200059D RID: 1437
	internal class RequestChannelBinder : IChannelBinder
	{
		// Token: 0x060037BC RID: 14268 RVA: 0x000D6CE5 File Offset: 0x000D4EE5
		internal RequestChannelBinder(IRequestChannel channel)
		{
			if (channel == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channel");
			}
			this.channel = channel;
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x060037BD RID: 14269 RVA: 0x000D6D07 File Offset: 0x000D4F07
		public IChannel Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060037BE RID: 14270 RVA: 0x000D6D0F File Offset: 0x000D4F0F
		public bool HasSession
		{
			get
			{
				return this.channel is ISessionChannel<IOutputSession>;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x000D6D1F File Offset: 0x000D4F1F
		public Uri ListenUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x060037C0 RID: 14272 RVA: 0x000D6D22 File Offset: 0x000D4F22
		public EndpointAddress LocalAddress
		{
			get
			{
				return EndpointAddress.AnonymousAddress;
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x060037C1 RID: 14273 RVA: 0x000D6D29 File Offset: 0x000D4F29
		public EndpointAddress RemoteAddress
		{
			get
			{
				return this.channel.RemoteAddress;
			}
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x000D6D36 File Offset: 0x000D4F36
		public void Abort()
		{
			this.channel.Abort();
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000D6D43 File Offset: 0x000D4F43
		public void CloseAfterFault(TimeSpan timeout)
		{
			this.channel.Close(timeout);
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x000D6D51 File Offset: 0x000D4F51
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x000D6D62 File Offset: 0x000D4F62
		public bool EndTryReceive(IAsyncResult result, out RequestContext requestContext)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x000D6D73 File Offset: 0x000D4F73
		public RequestContext CreateRequestContext(Message message)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000D6D84 File Offset: 0x000D4F84
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channel.BeginRequest(message, timeout, callback, state);
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000D6D96 File Offset: 0x000D4F96
		public void EndSend(IAsyncResult result)
		{
			this.ValidateNullReply(this.channel.EndRequest(result));
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000D6DAA File Offset: 0x000D4FAA
		public void Send(Message message, TimeSpan timeout)
		{
			this.ValidateNullReply(this.channel.Request(message, timeout));
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000D6DBF File Offset: 0x000D4FBF
		public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channel.BeginRequest(message, timeout, callback, state);
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000D6DD1 File Offset: 0x000D4FD1
		public Message EndRequest(IAsyncResult result)
		{
			return this.channel.EndRequest(result);
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000D6DDF File Offset: 0x000D4FDF
		public bool TryReceive(TimeSpan timeout, out RequestContext requestContext)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000D6DF0 File Offset: 0x000D4FF0
		public Message Request(Message message, TimeSpan timeout)
		{
			return this.channel.Request(message, timeout);
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000D6E00 File Offset: 0x000D5000
		private void ValidateNullReply(Message message)
		{
			if (message != null && !(message is NullMessage))
			{
				ProtocolException exception = ProtocolException.OneWayOperationReturnedNonNull(message);
				throw TraceUtility.ThrowHelperError(exception, message);
			}
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x000D6E27 File Offset: 0x000D5027
		public bool WaitForMessage(TimeSpan timeout)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000D6E38 File Offset: 0x000D5038
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000D6E49 File Offset: 0x000D5049
		public bool EndWaitForMessage(IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x04002967 RID: 10599
		private IRequestChannel channel;
	}
}
