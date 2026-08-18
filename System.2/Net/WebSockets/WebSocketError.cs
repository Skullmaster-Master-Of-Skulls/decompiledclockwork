using System;

namespace System.Net.WebSockets
{
	// Token: 0x02000235 RID: 565
	public enum WebSocketError
	{
		// Token: 0x040016A3 RID: 5795
		Success,
		// Token: 0x040016A4 RID: 5796
		InvalidMessageType,
		// Token: 0x040016A5 RID: 5797
		Faulted,
		// Token: 0x040016A6 RID: 5798
		NativeError,
		// Token: 0x040016A7 RID: 5799
		NotAWebSocket,
		// Token: 0x040016A8 RID: 5800
		UnsupportedVersion,
		// Token: 0x040016A9 RID: 5801
		UnsupportedProtocol,
		// Token: 0x040016AA RID: 5802
		HeaderError,
		// Token: 0x040016AB RID: 5803
		ConnectionClosedPrematurely,
		// Token: 0x040016AC RID: 5804
		InvalidState
	}
}
