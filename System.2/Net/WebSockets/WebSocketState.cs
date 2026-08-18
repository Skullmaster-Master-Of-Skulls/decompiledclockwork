using System;

namespace System.Net.WebSockets
{
	// Token: 0x0200023C RID: 572
	public enum WebSocketState
	{
		// Token: 0x040016DB RID: 5851
		None,
		// Token: 0x040016DC RID: 5852
		Connecting,
		// Token: 0x040016DD RID: 5853
		Open,
		// Token: 0x040016DE RID: 5854
		CloseSent,
		// Token: 0x040016DF RID: 5855
		CloseReceived,
		// Token: 0x040016E0 RID: 5856
		Closed,
		// Token: 0x040016E1 RID: 5857
		Aborted
	}
}
