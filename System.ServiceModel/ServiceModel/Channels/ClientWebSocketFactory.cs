using System;
using System.IO;
using System.Net.WebSockets;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000884 RID: 2180
	public abstract class ClientWebSocketFactory
	{
		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x060052CF RID: 21199
		public abstract string WebSocketVersion { get; }

		// Token: 0x060052D0 RID: 21200
		public abstract WebSocket CreateWebSocket(Stream connection, WebSocketTransportSettings settings);
	}
}
