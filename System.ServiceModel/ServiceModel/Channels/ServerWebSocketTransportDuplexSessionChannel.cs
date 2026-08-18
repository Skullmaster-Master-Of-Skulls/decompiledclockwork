using System;
using System.Net.Http;
using System.Net.WebSockets;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200087C RID: 2172
	internal class ServerWebSocketTransportDuplexSessionChannel : WebSocketTransportDuplexSessionChannel
	{
		// Token: 0x06005245 RID: 21061 RVA: 0x0012F41E File Offset: 0x0012D61E
		public ServerWebSocketTransportDuplexSessionChannel(HttpChannelListener channelListener, EndpointAddress localAddress, Uri localVia, ConnectionBufferPool bufferPool, HttpRequestContext httpRequestContext, HttpPipeline httpPipeline, HttpResponseMessage httpResponseMessage, string subProtocol) : base(channelListener, localAddress, localVia, bufferPool)
		{
			this.httpRequestContext = httpRequestContext;
			this.httpPipeline = httpPipeline;
			this.httpResponseMessage = httpResponseMessage;
			this.subProtocol = subProtocol;
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x06005246 RID: 21062 RVA: 0x0012F44B File Offset: 0x0012D64B
		protected override bool IsStreamedOutput
		{
			get
			{
				return TransferModeHelper.IsResponseStreamed(base.TransferMode);
			}
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x0012F458 File Offset: 0x0012D658
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(SessionOpenNotification))
			{
				if (this.sessionOpenNotification == null)
				{
					this.sessionOpenNotification = new ServerWebSocketTransportDuplexSessionChannel.SessionOpenNotificationHelper(this);
				}
				return (T)((object)this.sessionOpenNotification);
			}
			return base.GetProperty<T>();
		}

		// Token: 0x06005248 RID: 21064 RVA: 0x0012F4A8 File Offset: 0x0012D6A8
		internal void SetWebSocketInfo(WebSocketContext webSocketContext, RemoteEndpointMessageProperty remoteEndpointMessageProperty, SecurityMessageProperty handshakeSecurityMessageProperty, byte[] innerBuffer, bool shouldDisposeWebSocketAfterClosed, HttpRequestMessage requestMessage)
		{
			base.ShouldDisposeWebSocketAfterClosed = shouldDisposeWebSocketAfterClosed;
			this.webSocketContext = webSocketContext;
			base.WebSocket = webSocketContext.WebSocket;
			base.InternalBuffer = innerBuffer;
			if (handshakeSecurityMessageProperty != null)
			{
				base.RemoteSecurity = handshakeSecurityMessageProperty;
			}
			bool isStreamed = TransferModeHelper.IsRequestStreamed(base.TransferMode);
			this.webSocketMessageSource = new WebSocketTransportDuplexSessionChannel.WebSocketMessageSource(this, this.webSocketContext, isStreamed, remoteEndpointMessageProperty, this, requestMessage);
			base.SetMessageSource(this.webSocketMessageSource);
		}

		// Token: 0x06005249 RID: 21065 RVA: 0x0012F511 File Offset: 0x0012D711
		protected override void OnClosed()
		{
			base.OnClosed();
			((IDisposable)this.httpRequestContext).Dispose();
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x0012F524 File Offset: 0x0012D724
		protected override void OnOpen(TimeSpan timeout)
		{
			if (TD.WebSocketConnectionAcceptStartIsEnabled())
			{
				TD.WebSocketConnectionAcceptStart(this.httpRequestContext.EventTraceActivity);
			}
			this.httpRequestContext.AcceptWebSocket(this.httpResponseMessage, this.subProtocol, timeout);
			if (TD.WebSocketConnectionAcceptedIsEnabled())
			{
				TD.WebSocketConnectionAccepted(this.httpRequestContext.EventTraceActivity, (base.WebSocket != null) ? base.WebSocket.GetHashCode() : -1);
			}
		}

		// Token: 0x0600524B RID: 21067 RVA: 0x0012F58D File Offset: 0x0012D78D
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (TD.WebSocketConnectionAcceptStartIsEnabled())
			{
				TD.WebSocketConnectionAcceptStart(this.httpRequestContext.EventTraceActivity);
			}
			return this.httpRequestContext.BeginAcceptWebSocket(this.httpResponseMessage, this.subProtocol, callback, state);
		}

		// Token: 0x0600524C RID: 21068 RVA: 0x0012F5BF File Offset: 0x0012D7BF
		protected override void OnEndOpen(IAsyncResult result)
		{
			this.httpRequestContext.EndAcceptWebSocket(result);
			if (TD.WebSocketConnectionAcceptedIsEnabled())
			{
				TD.WebSocketConnectionAccepted(this.httpRequestContext.EventTraceActivity, (base.WebSocket != null) ? base.WebSocket.GetHashCode() : -1);
			}
		}

		// Token: 0x0600524D RID: 21069 RVA: 0x0012F5FA File Offset: 0x0012D7FA
		protected override void OnOpened()
		{
			base.OnOpened();
			this.httpPipeline.Close();
		}

		// Token: 0x04003250 RID: 12880
		private WebSocketContext webSocketContext;

		// Token: 0x04003251 RID: 12881
		private HttpRequestContext httpRequestContext;

		// Token: 0x04003252 RID: 12882
		private HttpPipeline httpPipeline;

		// Token: 0x04003253 RID: 12883
		private HttpResponseMessage httpResponseMessage;

		// Token: 0x04003254 RID: 12884
		private string subProtocol;

		// Token: 0x04003255 RID: 12885
		private WebSocketTransportDuplexSessionChannel.WebSocketMessageSource webSocketMessageSource;

		// Token: 0x04003256 RID: 12886
		private SessionOpenNotification sessionOpenNotification;

		// Token: 0x02000D5C RID: 3420
		private class SessionOpenNotificationHelper : SessionOpenNotification
		{
			// Token: 0x06007D5D RID: 32093 RVA: 0x001D49DB File Offset: 0x001D2BDB
			public SessionOpenNotificationHelper(ServerWebSocketTransportDuplexSessionChannel channel)
			{
				this.channel = channel;
			}

			// Token: 0x17001C04 RID: 7172
			// (get) Token: 0x06007D5E RID: 32094 RVA: 0x001D49EA File Offset: 0x001D2BEA
			public override bool IsEnabled
			{
				get
				{
					return this.channel.WebSocketSettings.CreateNotificationOnConnection;
				}
			}

			// Token: 0x06007D5F RID: 32095 RVA: 0x001D49FC File Offset: 0x001D2BFC
			public override void UpdateMessageProperties(MessageProperties inboundMessageProperties)
			{
				this.channel.webSocketMessageSource.UpdateOpenNotificationMessageProperties(inboundMessageProperties);
			}

			// Token: 0x040047FC RID: 18428
			private readonly ServerWebSocketTransportDuplexSessionChannel channel;
		}
	}
}
