using System;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000572 RID: 1394
	internal class InputChannelBinder : IChannelBinder
	{
		// Token: 0x0600361C RID: 13852 RVA: 0x000D193F File Offset: 0x000CFB3F
		internal InputChannelBinder(IInputChannel channel, Uri listenUri)
		{
			if (channel == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channel");
			}
			this.channel = channel;
			this.listenUri = listenUri;
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x0600361D RID: 13853 RVA: 0x000D1968 File Offset: 0x000CFB68
		public IChannel Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x0600361E RID: 13854 RVA: 0x000D1970 File Offset: 0x000CFB70
		public bool HasSession
		{
			get
			{
				return this.channel is ISessionChannel<IInputSession>;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x0600361F RID: 13855 RVA: 0x000D1980 File Offset: 0x000CFB80
		public Uri ListenUri
		{
			get
			{
				return this.listenUri;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06003620 RID: 13856 RVA: 0x000D1988 File Offset: 0x000CFB88
		public EndpointAddress LocalAddress
		{
			get
			{
				return this.channel.LocalAddress;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06003621 RID: 13857 RVA: 0x000D1995 File Offset: 0x000CFB95
		public EndpointAddress RemoteAddress
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000D19A6 File Offset: 0x000CFBA6
		public void Abort()
		{
			this.channel.Abort();
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x000D19B3 File Offset: 0x000CFBB3
		public void CloseAfterFault(TimeSpan timeout)
		{
			this.channel.Close(timeout);
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000D19C1 File Offset: 0x000CFBC1
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channel.BeginTryReceive(timeout, callback, state);
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x000D19D4 File Offset: 0x000CFBD4
		public bool EndTryReceive(IAsyncResult result, out RequestContext requestContext)
		{
			Message message;
			if (this.channel.EndTryReceive(result, out message))
			{
				requestContext = this.WrapMessage(message);
				return true;
			}
			requestContext = null;
			return false;
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x000D1A00 File Offset: 0x000CFC00
		public RequestContext CreateRequestContext(Message message)
		{
			return this.WrapMessage(message);
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x000D1A09 File Offset: 0x000CFC09
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw TraceUtility.ThrowHelperError(new NotImplementedException(), message);
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x000D1A16 File Offset: 0x000CFC16
		public void EndSend(IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000D1A27 File Offset: 0x000CFC27
		public void Send(Message message, TimeSpan timeout)
		{
			throw TraceUtility.ThrowHelperError(new NotImplementedException(), message);
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000D1A34 File Offset: 0x000CFC34
		public bool TryReceive(TimeSpan timeout, out RequestContext requestContext)
		{
			Message message;
			if (this.channel.TryReceive(timeout, out message))
			{
				requestContext = this.WrapMessage(message);
				return true;
			}
			requestContext = null;
			return false;
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000D1A60 File Offset: 0x000CFC60
		public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw TraceUtility.ThrowHelperError(new NotImplementedException(), message);
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000D1A6D File Offset: 0x000CFC6D
		public Message EndRequest(IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000D1A7E File Offset: 0x000CFC7E
		public Message Request(Message message, TimeSpan timeout)
		{
			throw TraceUtility.ThrowHelperError(new NotImplementedException(), message);
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000D1A8B File Offset: 0x000CFC8B
		public bool WaitForMessage(TimeSpan timeout)
		{
			return this.channel.WaitForMessage(timeout);
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000D1A99 File Offset: 0x000CFC99
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channel.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x000D1AA9 File Offset: 0x000CFCA9
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return this.channel.EndWaitForMessage(result);
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x000D1AB7 File Offset: 0x000CFCB7
		private RequestContext WrapMessage(Message message)
		{
			if (message == null)
			{
				return null;
			}
			return new InputChannelBinder.InputRequestContext(message, this);
		}

		// Token: 0x040028AA RID: 10410
		private IInputChannel channel;

		// Token: 0x040028AB RID: 10411
		private Uri listenUri;

		// Token: 0x02000C8B RID: 3211
		private class InputRequestContext : RequestContextBase
		{
			// Token: 0x060078A2 RID: 30882 RVA: 0x001C26AC File Offset: 0x001C08AC
			internal InputRequestContext(Message request, InputChannelBinder binder) : base(request, TimeSpan.Zero, TimeSpan.Zero)
			{
				this.binder = binder;
			}

			// Token: 0x060078A3 RID: 30883 RVA: 0x001C26C6 File Offset: 0x001C08C6
			protected override void OnAbort()
			{
			}

			// Token: 0x060078A4 RID: 30884 RVA: 0x001C26C8 File Offset: 0x001C08C8
			protected override void OnClose(TimeSpan timeout)
			{
			}

			// Token: 0x060078A5 RID: 30885 RVA: 0x001C26CA File Offset: 0x001C08CA
			protected override void OnReply(Message message, TimeSpan timeout)
			{
			}

			// Token: 0x060078A6 RID: 30886 RVA: 0x001C26CC File Offset: 0x001C08CC
			protected override IAsyncResult OnBeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x060078A7 RID: 30887 RVA: 0x001C26D6 File Offset: 0x001C08D6
			protected override void OnEndReply(IAsyncResult result)
			{
				CompletedAsyncResult.End(result);
			}

			// Token: 0x040044C3 RID: 17603
			private InputChannelBinder binder;
		}
	}
}
