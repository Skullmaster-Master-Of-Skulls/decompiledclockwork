using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x0200022F RID: 559
	public abstract class WebSocket : IDisposable
	{
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060014B5 RID: 5301
		public abstract WebSocketCloseStatus? CloseStatus { get; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060014B6 RID: 5302
		public abstract string CloseStatusDescription { get; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060014B7 RID: 5303
		public abstract string SubProtocol { get; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060014B8 RID: 5304
		public abstract WebSocketState State { get; }

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x0006CAB7 File Offset: 0x0006ACB7
		public static TimeSpan DefaultKeepAliveInterval
		{
			get
			{
				if (WebSocket.defaultKeepAliveInterval == null)
				{
					if (WebSocketProtocolComponent.IsSupported)
					{
						WebSocket.defaultKeepAliveInterval = new TimeSpan?(WebSocketProtocolComponent.WebSocketGetDefaultKeepAliveInterval());
					}
					else
					{
						WebSocket.defaultKeepAliveInterval = new TimeSpan?(Timeout.InfiniteTimeSpan);
					}
				}
				return WebSocket.defaultKeepAliveInterval.Value;
			}
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0006CAF6 File Offset: 0x0006ACF6
		public static ArraySegment<byte> CreateClientBuffer(int receiveBufferSize, int sendBufferSize)
		{
			WebSocketHelpers.ValidateBufferSizes(receiveBufferSize, sendBufferSize);
			return WebSocketBuffer.CreateInternalBufferArraySegment(receiveBufferSize, sendBufferSize, false);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0006CB07 File Offset: 0x0006AD07
		public static ArraySegment<byte> CreateServerBuffer(int receiveBufferSize)
		{
			WebSocketHelpers.ValidateBufferSizes(receiveBufferSize, 16);
			return WebSocketBuffer.CreateInternalBufferArraySegment(receiveBufferSize, 16, true);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0006CB1C File Offset: 0x0006AD1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static WebSocket CreateClientWebSocket(Stream innerStream, string subProtocol, int receiveBufferSize, int sendBufferSize, TimeSpan keepAliveInterval, bool useZeroMaskingKey, ArraySegment<byte> internalBuffer)
		{
			if (!WebSocketProtocolComponent.IsSupported)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			WebSocketHelpers.ValidateInnerStream(innerStream);
			WebSocketHelpers.ValidateOptions(subProtocol, receiveBufferSize, sendBufferSize, keepAliveInterval);
			WebSocketHelpers.ValidateArraySegment<byte>(internalBuffer, "internalBuffer");
			WebSocketBuffer.Validate(internalBuffer.Count, receiveBufferSize, sendBufferSize, false);
			return new InternalClientWebSocket(innerStream, subProtocol, receiveBufferSize, sendBufferSize, keepAliveInterval, useZeroMaskingKey, internalBuffer);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0006CB70 File Offset: 0x0006AD70
		internal static WebSocket CreateServerWebSocket(Stream innerStream, string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval, ArraySegment<byte> internalBuffer)
		{
			if (!WebSocketProtocolComponent.IsSupported)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			WebSocketHelpers.ValidateInnerStream(innerStream);
			WebSocketHelpers.ValidateOptions(subProtocol, receiveBufferSize, 16, keepAliveInterval);
			WebSocketHelpers.ValidateArraySegment<byte>(internalBuffer, "internalBuffer");
			WebSocketBuffer.Validate(internalBuffer.Count, receiveBufferSize, 16, true);
			return new ServerWebSocket(innerStream, subProtocol, receiveBufferSize, keepAliveInterval, internalBuffer);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0006CBC0 File Offset: 0x0006ADC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void RegisterPrefixes()
		{
			WebRequest.RegisterPrefix(Uri.UriSchemeWs + ":", new WebSocketHttpRequestCreator(false));
			WebRequest.RegisterPrefix(Uri.UriSchemeWss + ":", new WebSocketHttpRequestCreator(true));
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0006CBF8 File Offset: 0x0006ADF8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.")]
		public static bool IsApplicationTargeting45()
		{
			return BinaryCompatibility.TargetsAtLeast_Desktop_V4_5;
		}

		// Token: 0x060014C0 RID: 5312
		public abstract void Abort();

		// Token: 0x060014C1 RID: 5313
		public abstract Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken);

		// Token: 0x060014C2 RID: 5314
		public abstract Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken);

		// Token: 0x060014C3 RID: 5315
		public abstract void Dispose();

		// Token: 0x060014C4 RID: 5316
		public abstract Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken);

		// Token: 0x060014C5 RID: 5317
		public abstract Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken);

		// Token: 0x060014C6 RID: 5318 RVA: 0x0006CC00 File Offset: 0x0006AE00
		protected static void ThrowOnInvalidState(WebSocketState state, params WebSocketState[] validStates)
		{
			string text = string.Empty;
			if (validStates != null && validStates.Length != 0)
			{
				foreach (WebSocketState webSocketState in validStates)
				{
					if (state == webSocketState)
					{
						return;
					}
				}
				text = string.Join<WebSocketState>(", ", validStates);
			}
			throw new WebSocketException(SR.GetString("net_WebSockets_InvalidState", new object[]
			{
				state,
				text
			}));
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0006CC61 File Offset: 0x0006AE61
		protected static bool IsStateTerminal(WebSocketState state)
		{
			return state == WebSocketState.Closed || state == WebSocketState.Aborted;
		}

		// Token: 0x04001658 RID: 5720
		private static TimeSpan? defaultKeepAliveInterval;
	}
}
