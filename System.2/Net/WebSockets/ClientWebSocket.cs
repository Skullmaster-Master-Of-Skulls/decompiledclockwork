using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x0200022A RID: 554
	public sealed class ClientWebSocket : WebSocket
	{
		// Token: 0x06001474 RID: 5236 RVA: 0x0006BEA6 File Offset: 0x0006A0A6
		static ClientWebSocket()
		{
			WebSocket.RegisterPrefixes();
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0006BEB0 File Offset: 0x0006A0B0
		public ClientWebSocket()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.WebSockets, this, ".ctor", null);
			}
			if (!WebSocketProtocolComponent.IsSupported)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			this.state = 0;
			this.options = new ClientWebSocketOptions();
			this.cts = new CancellationTokenSource();
			if (Logging.On)
			{
				Logging.Exit(Logging.WebSockets, this, ".ctor", null);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0006BF1C File Offset: 0x0006A11C
		public ClientWebSocketOptions Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0006BF24 File Offset: 0x0006A124
		public override WebSocketCloseStatus? CloseStatus
		{
			get
			{
				if (this.innerWebSocket != null)
				{
					return this.innerWebSocket.CloseStatus;
				}
				return null;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x0006BF4E File Offset: 0x0006A14E
		public override string CloseStatusDescription
		{
			get
			{
				if (this.innerWebSocket != null)
				{
					return this.innerWebSocket.CloseStatusDescription;
				}
				return null;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0006BF65 File Offset: 0x0006A165
		public override string SubProtocol
		{
			get
			{
				if (this.innerWebSocket != null)
				{
					return this.innerWebSocket.SubProtocol;
				}
				return null;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x0006BF7C File Offset: 0x0006A17C
		public override WebSocketState State
		{
			get
			{
				if (this.innerWebSocket != null)
				{
					return this.innerWebSocket.State;
				}
				switch (this.state)
				{
				case 0:
					return WebSocketState.None;
				case 1:
					return WebSocketState.Connecting;
				case 3:
					return WebSocketState.Closed;
				}
				return WebSocketState.Closed;
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0006BFC4 File Offset: 0x0006A1C4
		public Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (!uri.IsAbsoluteUri)
			{
				throw new ArgumentException(SR.GetString("net_uri_NotAbsolute"), "uri");
			}
			if (uri.Scheme != Uri.UriSchemeWs && uri.Scheme != Uri.UriSchemeWss)
			{
				throw new ArgumentException(SR.GetString("net_WebSockets_Scheme"), "uri");
			}
			int num = Interlocked.CompareExchange(ref this.state, 1, 0);
			if (num == 3)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (num != 0)
			{
				throw new InvalidOperationException(SR.GetString("net_WebSockets_AlreadyStarted"));
			}
			this.options.SetToReadOnly();
			return this.ConnectAsyncCore(uri, cancellationToken);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0006C084 File Offset: 0x0006A284
		private Task ConnectAsyncCore(Uri uri, CancellationToken cancellationToken)
		{
			ClientWebSocket.<ConnectAsyncCore>d__21 <ConnectAsyncCore>d__;
			<ConnectAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ConnectAsyncCore>d__.<>4__this = this;
			<ConnectAsyncCore>d__.uri = uri;
			<ConnectAsyncCore>d__.cancellationToken = cancellationToken;
			<ConnectAsyncCore>d__.<>1__state = -1;
			<ConnectAsyncCore>d__.<>t__builder.Start<ClientWebSocket.<ConnectAsyncCore>d__21>(ref <ConnectAsyncCore>d__);
			return <ConnectAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0006C0D7 File Offset: 0x0006A2D7
		private void ConnectExceptionCleanup(HttpWebResponse response)
		{
			this.Dispose();
			if (response != null)
			{
				response.Dispose();
			}
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0006C0E8 File Offset: 0x0006A2E8
		private HttpWebRequest CreateAndConfigureRequest(Uri uri)
		{
			HttpWebRequest httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
			if (httpWebRequest == null)
			{
				throw new InvalidOperationException(SR.GetString("net_WebSockets_InvalidRegistration"));
			}
			foreach (object obj in this.options.RequestHeaders.Keys)
			{
				string name = (string)obj;
				httpWebRequest.Headers.Add(name, this.options.RequestHeaders[name]);
			}
			if (this.options.RequestedSubProtocols.Count > 0)
			{
				httpWebRequest.Headers.Add("Sec-WebSocket-Protocol", string.Join(", ", this.options.RequestedSubProtocols));
			}
			if (this.options.UseDefaultCredentials)
			{
				httpWebRequest.UseDefaultCredentials = true;
			}
			else if (this.options.Credentials != null)
			{
				httpWebRequest.Credentials = this.options.Credentials;
			}
			if (this.options.InternalClientCertificates != null)
			{
				httpWebRequest.ClientCertificates = this.options.InternalClientCertificates;
			}
			httpWebRequest.Proxy = this.options.Proxy;
			httpWebRequest.CookieContainer = this.options.Cookies;
			this.cts.Token.Register(new Action<object>(this.AbortRequest), httpWebRequest, false);
			return httpWebRequest;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0006C250 File Offset: 0x0006A450
		private string ValidateResponse(HttpWebRequest request, HttpWebResponse response)
		{
			if (response.StatusCode != HttpStatusCode.SwitchingProtocols)
			{
				throw new WebSocketException(SR.GetString("net_WebSockets_Connect101Expected", new object[]
				{
					(int)response.StatusCode
				}));
			}
			string text = response.Headers["Upgrade"];
			if (!string.Equals(text, "websocket", StringComparison.OrdinalIgnoreCase))
			{
				throw new WebSocketException(SR.GetString("net_WebSockets_InvalidResponseHeader", new object[]
				{
					"Upgrade",
					text
				}));
			}
			string text2 = response.Headers["Connection"];
			if (!string.Equals(text2, "Upgrade", StringComparison.OrdinalIgnoreCase))
			{
				throw new WebSocketException(SR.GetString("net_WebSockets_InvalidResponseHeader", new object[]
				{
					"Connection",
					text2
				}));
			}
			string text3 = response.Headers["Sec-WebSocket-Accept"];
			string secWebSocketAcceptString = WebSocketHelpers.GetSecWebSocketAcceptString(request.Headers["Sec-WebSocket-Key"]);
			if (!string.Equals(text3, secWebSocketAcceptString, StringComparison.OrdinalIgnoreCase))
			{
				throw new WebSocketException(SR.GetString("net_WebSockets_InvalidResponseHeader", new object[]
				{
					"Sec-WebSocket-Accept",
					text3
				}));
			}
			string text4 = response.Headers["Sec-WebSocket-Protocol"];
			if (!string.IsNullOrWhiteSpace(text4) && this.options.RequestedSubProtocols.Count > 0)
			{
				bool flag = false;
				foreach (string a in this.options.RequestedSubProtocols)
				{
					if (string.Equals(a, text4, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw new WebSocketException(SR.GetString("net_WebSockets_AcceptUnsupportedProtocol", new object[]
					{
						string.Join(", ", this.options.RequestedSubProtocols),
						text4
					}));
				}
			}
			if (!string.IsNullOrWhiteSpace(text4))
			{
				return text4;
			}
			return null;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0006C430 File Offset: 0x0006A630
		public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this.innerWebSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0006C448 File Offset: 0x0006A648
		public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this.innerWebSocket.ReceiveAsync(buffer, cancellationToken);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0006C45D File Offset: 0x0006A65D
		public override Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this.innerWebSocket.CloseAsync(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0006C473 File Offset: 0x0006A673
		public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this.innerWebSocket.CloseOutputAsync(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0006C489 File Offset: 0x0006A689
		public override void Abort()
		{
			if (this.state == 3)
			{
				return;
			}
			if (this.innerWebSocket != null)
			{
				this.innerWebSocket.Abort();
			}
			this.Dispose();
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0006C4B0 File Offset: 0x0006A6B0
		private void AbortRequest(object obj)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)obj;
			httpWebRequest.Abort();
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0006C4CC File Offset: 0x0006A6CC
		public override void Dispose()
		{
			int num = Interlocked.Exchange(ref this.state, 3);
			if (num == 3)
			{
				return;
			}
			this.cts.Cancel(false);
			this.cts.Dispose();
			if (this.innerWebSocket != null)
			{
				this.innerWebSocket.Dispose();
			}
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0006C515 File Offset: 0x0006A715
		private void ThrowIfNotConnected()
		{
			if (this.state == 3)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.state != 2)
			{
				throw new InvalidOperationException(SR.GetString("net_WebSockets_NotConnected"));
			}
		}

		// Token: 0x04001634 RID: 5684
		private readonly ClientWebSocketOptions options;

		// Token: 0x04001635 RID: 5685
		private WebSocket innerWebSocket;

		// Token: 0x04001636 RID: 5686
		private readonly CancellationTokenSource cts;

		// Token: 0x04001637 RID: 5687
		private int state;

		// Token: 0x04001638 RID: 5688
		private const int created = 0;

		// Token: 0x04001639 RID: 5689
		private const int connecting = 1;

		// Token: 0x0400163A RID: 5690
		private const int connected = 2;

		// Token: 0x0400163B RID: 5691
		private const int disposed = 3;
	}
}
