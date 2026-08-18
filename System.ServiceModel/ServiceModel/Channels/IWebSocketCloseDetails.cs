using System;
using System.Net.WebSockets;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200087A RID: 2170
	public interface IWebSocketCloseDetails
	{
		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x06005235 RID: 21045
		WebSocketCloseStatus? InputCloseStatus { get; }

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x06005236 RID: 21046
		string InputCloseStatusDescription { get; }

		// Token: 0x06005237 RID: 21047
		void SetOutputCloseStatus(WebSocketCloseStatus closeStatus, string closeStatusDescription);
	}
}
