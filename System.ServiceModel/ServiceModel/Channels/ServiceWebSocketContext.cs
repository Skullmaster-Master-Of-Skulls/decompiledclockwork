using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.WebSockets;
using System.Security.Principal;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200087B RID: 2171
	internal class ServiceWebSocketContext : WebSocketContext
	{
		// Token: 0x06005238 RID: 21048 RVA: 0x0012F327 File Offset: 0x0012D527
		public ServiceWebSocketContext(WebSocketContext context, IPrincipal user)
		{
			this.context = context;
			this.user = user;
		}

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x06005239 RID: 21049 RVA: 0x0012F33D File Offset: 0x0012D53D
		public override CookieCollection CookieCollection
		{
			get
			{
				return this.context.CookieCollection;
			}
		}

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x0600523A RID: 21050 RVA: 0x0012F34A File Offset: 0x0012D54A
		public override NameValueCollection Headers
		{
			get
			{
				return this.context.Headers;
			}
		}

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x0600523B RID: 21051 RVA: 0x0012F357 File Offset: 0x0012D557
		public override bool IsAuthenticated
		{
			get
			{
				if (this.user == null)
				{
					return this.context.IsAuthenticated;
				}
				return this.user.Identity != null && this.user.Identity.IsAuthenticated;
			}
		}

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x0600523C RID: 21052 RVA: 0x0012F38C File Offset: 0x0012D58C
		public override bool IsLocal
		{
			get
			{
				return this.context.IsLocal;
			}
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x0600523D RID: 21053 RVA: 0x0012F399 File Offset: 0x0012D599
		public override bool IsSecureConnection
		{
			get
			{
				return this.context.IsSecureConnection;
			}
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x0600523E RID: 21054 RVA: 0x0012F3A6 File Offset: 0x0012D5A6
		public override Uri RequestUri
		{
			get
			{
				return this.context.RequestUri;
			}
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x0600523F RID: 21055 RVA: 0x0012F3B3 File Offset: 0x0012D5B3
		public override string SecWebSocketKey
		{
			get
			{
				return this.context.SecWebSocketKey;
			}
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x06005240 RID: 21056 RVA: 0x0012F3C0 File Offset: 0x0012D5C0
		public override string Origin
		{
			get
			{
				return this.context.Origin;
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x06005241 RID: 21057 RVA: 0x0012F3CD File Offset: 0x0012D5CD
		public override IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				return this.context.SecWebSocketProtocols;
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x06005242 RID: 21058 RVA: 0x0012F3DA File Offset: 0x0012D5DA
		public override string SecWebSocketVersion
		{
			get
			{
				return this.context.SecWebSocketVersion;
			}
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x06005243 RID: 21059 RVA: 0x0012F3E7 File Offset: 0x0012D5E7
		public override IPrincipal User
		{
			get
			{
				if (this.user == null)
				{
					return this.context.User;
				}
				return this.user;
			}
		}

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x06005244 RID: 21060 RVA: 0x0012F403 File Offset: 0x0012D603
		public override WebSocket WebSocket
		{
			get
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("WebSocketContextWebSocketCannotBeAccessedError")));
			}
		}

		// Token: 0x0400324E RID: 12878
		private WebSocketContext context;

		// Token: 0x0400324F RID: 12879
		private IPrincipal user;
	}
}
