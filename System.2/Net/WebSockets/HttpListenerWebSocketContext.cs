using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;

namespace System.Net.WebSockets
{
	// Token: 0x0200022D RID: 557
	public class HttpListenerWebSocketContext : WebSocketContext
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x0006C8D4 File Offset: 0x0006AAD4
		internal HttpListenerWebSocketContext(Uri requestUri, NameValueCollection headers, CookieCollection cookieCollection, IPrincipal user, bool isAuthenticated, bool isLocal, bool isSecureConnection, string origin, IEnumerable<string> secWebSocketProtocols, string secWebSocketVersion, string secWebSocketKey, WebSocket webSocket)
		{
			this.m_CookieCollection = new CookieCollection();
			this.m_CookieCollection.Add(cookieCollection);
			this.m_Headers = new NameValueCollection(headers);
			this.m_User = HttpListenerWebSocketContext.CopyPrincipal(user);
			this.m_RequestUri = requestUri;
			this.m_IsAuthenticated = isAuthenticated;
			this.m_IsLocal = isLocal;
			this.m_IsSecureConnection = isSecureConnection;
			this.m_Origin = origin;
			this.m_SecWebSocketProtocols = secWebSocketProtocols;
			this.m_SecWebSocketVersion = secWebSocketVersion;
			this.m_SecWebSocketKey = secWebSocketKey;
			this.m_WebSocket = webSocket;
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x0006C95E File Offset: 0x0006AB5E
		public override Uri RequestUri
		{
			get
			{
				return this.m_RequestUri;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0006C966 File Offset: 0x0006AB66
		public override NameValueCollection Headers
		{
			get
			{
				return this.m_Headers;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0006C96E File Offset: 0x0006AB6E
		public override string Origin
		{
			get
			{
				return this.m_Origin;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x0006C976 File Offset: 0x0006AB76
		public override IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				return this.m_SecWebSocketProtocols;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0006C97E File Offset: 0x0006AB7E
		public override string SecWebSocketVersion
		{
			get
			{
				return this.m_SecWebSocketVersion;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x0006C986 File Offset: 0x0006AB86
		public override string SecWebSocketKey
		{
			get
			{
				return this.m_SecWebSocketKey;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0006C98E File Offset: 0x0006AB8E
		public override CookieCollection CookieCollection
		{
			get
			{
				return this.m_CookieCollection;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0006C996 File Offset: 0x0006AB96
		public override IPrincipal User
		{
			get
			{
				return this.m_User;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x0006C99E File Offset: 0x0006AB9E
		public override bool IsAuthenticated
		{
			get
			{
				return this.m_IsAuthenticated;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x0006C9A6 File Offset: 0x0006ABA6
		public override bool IsLocal
		{
			get
			{
				return this.m_IsLocal;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x0006C9AE File Offset: 0x0006ABAE
		public override bool IsSecureConnection
		{
			get
			{
				return this.m_IsSecureConnection;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0006C9B6 File Offset: 0x0006ABB6
		public override WebSocket WebSocket
		{
			get
			{
				return this.m_WebSocket;
			}
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0006C9C0 File Offset: 0x0006ABC0
		private static IPrincipal CopyPrincipal(IPrincipal user)
		{
			IPrincipal result = null;
			if (user != null)
			{
				if (!(user is WindowsPrincipal))
				{
					HttpListenerBasicIdentity httpListenerBasicIdentity = user.Identity as HttpListenerBasicIdentity;
					if (httpListenerBasicIdentity != null)
					{
						result = new GenericPrincipal(new HttpListenerBasicIdentity(httpListenerBasicIdentity.Name, httpListenerBasicIdentity.Password), null);
					}
				}
				else
				{
					WindowsIdentity windowsIdentity = (WindowsIdentity)user.Identity;
					result = new WindowsPrincipal(HttpListener.CreateWindowsIdentity(windowsIdentity.Token, windowsIdentity.AuthenticationType, WindowsAccountType.Normal, true));
				}
			}
			return result;
		}

		// Token: 0x0400164A RID: 5706
		private Uri m_RequestUri;

		// Token: 0x0400164B RID: 5707
		private NameValueCollection m_Headers;

		// Token: 0x0400164C RID: 5708
		private CookieCollection m_CookieCollection;

		// Token: 0x0400164D RID: 5709
		private IPrincipal m_User;

		// Token: 0x0400164E RID: 5710
		private bool m_IsAuthenticated;

		// Token: 0x0400164F RID: 5711
		private bool m_IsLocal;

		// Token: 0x04001650 RID: 5712
		private bool m_IsSecureConnection;

		// Token: 0x04001651 RID: 5713
		private string m_Origin;

		// Token: 0x04001652 RID: 5714
		private IEnumerable<string> m_SecWebSocketProtocols;

		// Token: 0x04001653 RID: 5715
		private string m_SecWebSocketVersion;

		// Token: 0x04001654 RID: 5716
		private string m_SecWebSocketKey;

		// Token: 0x04001655 RID: 5717
		private WebSocket m_WebSocket;
	}
}
