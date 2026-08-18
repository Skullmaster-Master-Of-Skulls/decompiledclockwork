using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net.WebSockets
{
	// Token: 0x0200022E RID: 558
	internal sealed class ServerWebSocket : WebSocketBase
	{
		// Token: 0x060014B2 RID: 5298 RVA: 0x0006CA2C File Offset: 0x0006AC2C
		public ServerWebSocket(Stream innerStream, string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval, ArraySegment<byte> internalBuffer) : base(innerStream, subProtocol, keepAliveInterval, WebSocketBuffer.CreateServerBuffer(internalBuffer, receiveBufferSize))
		{
			this.m_Properties = base.InternalBuffer.CreateProperties(false);
			this.m_SessionHandle = this.CreateWebSocketHandle();
			if (this.m_SessionHandle == null || this.m_SessionHandle.IsInvalid)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			base.StartKeepAliveTimer();
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0006CA89 File Offset: 0x0006AC89
		internal override SafeHandle SessionHandle
		{
			get
			{
				return this.m_SessionHandle;
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0006CA94 File Offset: 0x0006AC94
		private SafeHandle CreateWebSocketHandle()
		{
			SafeWebSocketHandle result;
			WebSocketProtocolComponent.WebSocketCreateServerHandle(this.m_Properties, this.m_Properties.Length, out result);
			return result;
		}

		// Token: 0x04001656 RID: 5718
		private readonly SafeHandle m_SessionHandle;

		// Token: 0x04001657 RID: 5719
		private readonly WebSocketProtocolComponent.Property[] m_Properties;
	}
}
