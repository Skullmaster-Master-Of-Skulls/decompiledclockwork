using System;
using System.Net.WebSockets;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200079C RID: 1948
	internal static class WebSocketDefaults
	{
		// Token: 0x04002EC1 RID: 11969
		internal const WebSocketTransportUsage TransportUsage = WebSocketTransportUsage.Never;

		// Token: 0x04002EC2 RID: 11970
		internal const bool CreateNotificationOnConnection = false;

		// Token: 0x04002EC3 RID: 11971
		internal const string DefaultKeepAliveIntervalString = "00:00:00";

		// Token: 0x04002EC4 RID: 11972
		internal static readonly TimeSpan DefaultKeepAliveInterval = TimeSpanHelper.FromSeconds(0, "00:00:00");

		// Token: 0x04002EC5 RID: 11973
		internal const int BufferSize = 16384;

		// Token: 0x04002EC6 RID: 11974
		internal const int MinReceiveBufferSize = 256;

		// Token: 0x04002EC7 RID: 11975
		internal const int MinSendBufferSize = 16;

		// Token: 0x04002EC8 RID: 11976
		internal const bool DisablePayloadMasking = false;

		// Token: 0x04002EC9 RID: 11977
		internal const WebSocketMessageType DefaultWebSocketMessageType = WebSocketMessageType.Binary;

		// Token: 0x04002ECA RID: 11978
		internal const string SubProtocol = null;

		// Token: 0x04002ECB RID: 11979
		internal const int DefaultMaxPendingConnections = 0;

		// Token: 0x04002ECC RID: 11980
		internal static readonly int MaxPendingConnectionsCpuCount = ServiceThrottle.DefaultMaxConcurrentSessionsCpuCount;

		// Token: 0x04002ECD RID: 11981
		internal const string WebSocketConnectionHeaderValue = "Upgrade";

		// Token: 0x04002ECE RID: 11982
		internal const string WebSocketUpgradeHeaderValue = "websocket";
	}
}
