using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net.WebSockets
{
	// Token: 0x0200022C RID: 556
	internal sealed class InternalClientWebSocket : WebSocketBase
	{
		// Token: 0x060014A1 RID: 5281 RVA: 0x0006C850 File Offset: 0x0006AA50
		public InternalClientWebSocket(Stream innerStream, string subProtocol, int receiveBufferSize, int sendBufferSize, TimeSpan keepAliveInterval, bool useZeroMaskingKey, ArraySegment<byte> internalBuffer) : base(innerStream, subProtocol, keepAliveInterval, WebSocketBuffer.CreateClientBuffer(internalBuffer, receiveBufferSize, sendBufferSize))
		{
			this.m_Properties = base.InternalBuffer.CreateProperties(useZeroMaskingKey);
			this.m_SessionHandle = this.CreateWebSocketHandle();
			if (this.m_SessionHandle == null || this.m_SessionHandle.IsInvalid)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			base.StartKeepAliveTimer();
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0006C8B0 File Offset: 0x0006AAB0
		internal override SafeHandle SessionHandle
		{
			get
			{
				return this.m_SessionHandle;
			}
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0006C8B8 File Offset: 0x0006AAB8
		private SafeHandle CreateWebSocketHandle()
		{
			SafeWebSocketHandle result;
			WebSocketProtocolComponent.WebSocketCreateClientHandle(this.m_Properties, out result);
			return result;
		}

		// Token: 0x04001648 RID: 5704
		private readonly SafeHandle m_SessionHandle;

		// Token: 0x04001649 RID: 5705
		private readonly WebSocketProtocolComponent.Property[] m_Properties;
	}
}
