using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000590 RID: 1424
	internal class OutputChannelBinder : IChannelBinder
	{
		// Token: 0x060036FD RID: 14077 RVA: 0x000D4121 File Offset: 0x000D2321
		internal OutputChannelBinder(IOutputChannel channel)
		{
			if (channel == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channel");
			}
			this.channel = channel;
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x000D4143 File Offset: 0x000D2343
		public IChannel Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x060036FF RID: 14079 RVA: 0x000D414B File Offset: 0x000D234B
		public bool HasSession
		{
			get
			{
				return this.channel is ISessionChannel<IOutputSession>;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06003700 RID: 14080 RVA: 0x000D415B File Offset: 0x000D235B
		public Uri ListenUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06003701 RID: 14081 RVA: 0x000D415E File Offset: 0x000D235E
		public EndpointAddress LocalAddress
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06003702 RID: 14082 RVA: 0x000D416F File Offset: 0x000D236F
		public EndpointAddress RemoteAddress
		{
			get
			{
				return this.channel.RemoteAddress;
			}
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x000D417C File Offset: 0x000D237C
		public void Abort()
		{
			this.channel.Abort();
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x000D4189 File Offset: 0x000D2389
		public void CloseAfterFault(TimeSpan timeout)
		{
			this.channel.Close(timeout);
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x000D4197 File Offset: 0x000D2397
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x000D41A8 File Offset: 0x000D23A8
		public bool EndTryReceive(IAsyncResult result, out RequestContext requestContext)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x000D41B9 File Offset: 0x000D23B9
		public RequestContext CreateRequestContext(Message message)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x000D41CA File Offset: 0x000D23CA
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channel.BeginSend(message, timeout, callback, state);
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x000D41DC File Offset: 0x000D23DC
		public void EndSend(IAsyncResult result)
		{
			this.channel.EndSend(result);
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x000D41EA File Offset: 0x000D23EA
		public void Send(Message message, TimeSpan timeout)
		{
			this.channel.Send(message, timeout);
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x000D41F9 File Offset: 0x000D23F9
		public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw TraceUtility.ThrowHelperError(new NotImplementedException(), message);
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x000D4206 File Offset: 0x000D2406
		public Message EndRequest(IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000D4217 File Offset: 0x000D2417
		public bool TryReceive(TimeSpan timeout, out RequestContext requestContext)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000D4228 File Offset: 0x000D2428
		public Message Request(Message message, TimeSpan timeout)
		{
			throw TraceUtility.ThrowHelperError(new NotImplementedException(), message);
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000D4235 File Offset: 0x000D2435
		public bool WaitForMessage(TimeSpan timeout)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000D4246 File Offset: 0x000D2446
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000D4257 File Offset: 0x000D2457
		public bool EndWaitForMessage(IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x04002900 RID: 10496
		private IOutputChannel channel;
	}
}
