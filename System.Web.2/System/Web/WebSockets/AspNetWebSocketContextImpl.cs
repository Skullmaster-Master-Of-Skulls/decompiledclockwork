using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.WebSockets;
using System.Security.Principal;
using System.Web.Caching;
using System.Web.Profile;

namespace System.Web.WebSockets
{
	// Token: 0x020001BA RID: 442
	internal sealed class AspNetWebSocketContextImpl : AspNetWebSocketContext
	{
		// Token: 0x060016D3 RID: 5843 RVA: 0x00047F8D File Offset: 0x0004618D
		public AspNetWebSocketContextImpl(HttpContextBase httpContext = null, HttpWorkerRequest workerRequest = null, AspNetWebSocket webSocket = null)
		{
			this._httpContext = httpContext;
			this._workerRequest = workerRequest;
			this._webSocket = webSocket;
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x00047FAA File Offset: 0x000461AA
		public override string AnonymousID
		{
			get
			{
				return this._httpContext.Request.AnonymousID;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x00047FBC File Offset: 0x000461BC
		public override HttpApplicationStateBase Application
		{
			get
			{
				return this._httpContext.Application;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x00047FC9 File Offset: 0x000461C9
		public override string ApplicationPath
		{
			get
			{
				return this._httpContext.Request.ApplicationPath;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x00047FDB File Offset: 0x000461DB
		public override Cache Cache
		{
			get
			{
				return this._httpContext.Cache;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x00047FE8 File Offset: 0x000461E8
		public override HttpClientCertificate ClientCertificate
		{
			get
			{
				return this._httpContext.Request.ClientCertificate;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x00047FFC File Offset: 0x000461FC
		public override CookieCollection CookieCollection
		{
			get
			{
				if (this._cookieCollection == null)
				{
					CookieCollection cookieCollection = new CookieCollection();
					HttpCookieCollection cookies = this.Cookies;
					for (int i = 0; i < cookies.Count; i++)
					{
						HttpCookie httpCookie = cookies.Get(i);
						cookieCollection.Add(new Cookie
						{
							Name = httpCookie.Name,
							Value = httpCookie.Value,
							HttpOnly = httpCookie.HttpOnly,
							Path = httpCookie.Path,
							Secure = httpCookie.Secure,
							Domain = httpCookie.Domain,
							Expires = httpCookie.Expires
						});
					}
					this._cookieCollection = cookieCollection;
				}
				return this._cookieCollection;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x000480A6 File Offset: 0x000462A6
		public override HttpCookieCollection Cookies
		{
			get
			{
				return this._httpContext.Request.Cookies;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x000480B8 File Offset: 0x000462B8
		public override string FilePath
		{
			get
			{
				return this._httpContext.Request.FilePath;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x000480CA File Offset: 0x000462CA
		public override NameValueCollection Headers
		{
			get
			{
				return this._httpContext.Request.Headers;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x000480DC File Offset: 0x000462DC
		public override bool IsAuthenticated
		{
			get
			{
				return this._httpContext.Request.IsAuthenticated;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x000480EE File Offset: 0x000462EE
		public override bool IsClientConnected
		{
			get
			{
				return this._workerRequest.IsClientConnected();
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x000480FB File Offset: 0x000462FB
		public override bool IsDebuggingEnabled
		{
			get
			{
				return this._httpContext.IsDebuggingEnabled;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x00048108 File Offset: 0x00046308
		public override bool IsLocal
		{
			get
			{
				return this._httpContext.Request.IsLocal;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x0004811A File Offset: 0x0004631A
		public override bool IsSecureConnection
		{
			get
			{
				return this._httpContext.Request.IsSecureConnection;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0004812C File Offset: 0x0004632C
		public override IDictionary Items
		{
			get
			{
				return this._httpContext.Items;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00048139 File Offset: 0x00046339
		public override WindowsIdentity LogonUserIdentity
		{
			get
			{
				return this._httpContext.Request.LogonUserIdentity;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x0004814B File Offset: 0x0004634B
		public override string Origin
		{
			get
			{
				return this.Headers["Origin"];
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x0004815D File Offset: 0x0004635D
		public override string Path
		{
			get
			{
				return this._httpContext.Request.Path;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x0004816F File Offset: 0x0004636F
		public override string PathInfo
		{
			get
			{
				return this._httpContext.Request.PathInfo;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x00048181 File Offset: 0x00046381
		public override ProfileBase Profile
		{
			get
			{
				return this._httpContext.Profile;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0004818E File Offset: 0x0004638E
		public override NameValueCollection QueryString
		{
			get
			{
				return this._httpContext.Request.QueryString;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x000481A0 File Offset: 0x000463A0
		public override string RawUrl
		{
			get
			{
				return this._httpContext.Request.RawUrl;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x000481B2 File Offset: 0x000463B2
		public override Uri RequestUri
		{
			get
			{
				return this._httpContext.Request.Url;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x000481C4 File Offset: 0x000463C4
		public override string SecWebSocketKey
		{
			get
			{
				return this.Headers["Sec-WebSocket-Key"];
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x000481D6 File Offset: 0x000463D6
		public override IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				return this._httpContext.WebSocketRequestedProtocols;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x000481E3 File Offset: 0x000463E3
		public override string SecWebSocketVersion
		{
			get
			{
				return this.Headers["Sec-WebSocket-Version"];
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x000481F5 File Offset: 0x000463F5
		public override HttpServerUtilityBase Server
		{
			get
			{
				return this._httpContext.Server;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x00048202 File Offset: 0x00046402
		public override NameValueCollection ServerVariables
		{
			get
			{
				return this._httpContext.Request.ServerVariables;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x00048214 File Offset: 0x00046414
		public override DateTime Timestamp
		{
			get
			{
				return this._httpContext.Timestamp;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x00048221 File Offset: 0x00046421
		public override UnvalidatedRequestValuesBase Unvalidated
		{
			get
			{
				return this._httpContext.Request.Unvalidated;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x00048233 File Offset: 0x00046433
		public override Uri UrlReferrer
		{
			get
			{
				return this._httpContext.Request.UrlReferrer;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00048245 File Offset: 0x00046445
		public override IPrincipal User
		{
			get
			{
				return this._httpContext.User;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x00048252 File Offset: 0x00046452
		public override string UserAgent
		{
			get
			{
				return this._httpContext.Request.UserAgent;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00048264 File Offset: 0x00046464
		public override string UserHostAddress
		{
			get
			{
				return this._httpContext.Request.UserHostAddress;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x00048276 File Offset: 0x00046476
		public override string UserHostName
		{
			get
			{
				return this._httpContext.Request.UserHostName;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x00048288 File Offset: 0x00046488
		public override string[] UserLanguages
		{
			get
			{
				return this._httpContext.Request.UserLanguages;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x0004829A File Offset: 0x0004649A
		public override WebSocket WebSocket
		{
			get
			{
				return this._webSocket;
			}
		}

		// Token: 0x040016BD RID: 5821
		private readonly HttpContextBase _httpContext;

		// Token: 0x040016BE RID: 5822
		private readonly HttpWorkerRequest _workerRequest;

		// Token: 0x040016BF RID: 5823
		private readonly AspNetWebSocket _webSocket;

		// Token: 0x040016C0 RID: 5824
		private CookieCollection _cookieCollection;
	}
}
