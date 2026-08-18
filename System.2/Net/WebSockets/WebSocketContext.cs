using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;

namespace System.Net.WebSockets
{
	// Token: 0x02000234 RID: 564
	public abstract class WebSocketContext
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600152E RID: 5422
		public abstract Uri RequestUri { get; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600152F RID: 5423
		public abstract NameValueCollection Headers { get; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001530 RID: 5424
		public abstract string Origin { get; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001531 RID: 5425
		public abstract IEnumerable<string> SecWebSocketProtocols { get; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001532 RID: 5426
		public abstract string SecWebSocketVersion { get; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001533 RID: 5427
		public abstract string SecWebSocketKey { get; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001534 RID: 5428
		public abstract CookieCollection CookieCollection { get; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001535 RID: 5429
		public abstract IPrincipal User { get; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001536 RID: 5430
		public abstract bool IsAuthenticated { get; }

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001537 RID: 5431
		public abstract bool IsLocal { get; }

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001538 RID: 5432
		public abstract bool IsSecureConnection { get; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001539 RID: 5433
		public abstract WebSocket WebSocket { get; }
	}
}
