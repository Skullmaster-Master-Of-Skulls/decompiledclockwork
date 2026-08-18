using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web.Http.Routing;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200002F RID: 47
	internal sealed class RequestBackedHttpRequestContext : HttpRequestContext
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00006B36 File Offset: 0x00004D36
		public RequestBackedHttpRequestContext()
		{
			this.Principal = Thread.CurrentPrincipal;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006B49 File Offset: 0x00004D49
		public RequestBackedHttpRequestContext(HttpRequestMessage request) : this()
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this._request = request;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006B66 File Offset: 0x00004D66
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00006B6E File Offset: 0x00004D6E
		public HttpRequestMessage Request
		{
			get
			{
				return this._request;
			}
			set
			{
				this._request = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006B77 File Offset: 0x00004D77
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006B9D File Offset: 0x00004D9D
		public override X509Certificate2 ClientCertificate
		{
			get
			{
				if (this._certificateSet)
				{
					return this._certificate;
				}
				if (this._request != null)
				{
					return this._request.LegacyGetClientCertificate();
				}
				return null;
			}
			set
			{
				this._certificate = value;
				this._certificateSet = true;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006BAD File Offset: 0x00004DAD
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00006BD3 File Offset: 0x00004DD3
		public override HttpConfiguration Configuration
		{
			get
			{
				if (this._configurationSet)
				{
					return this._configuration;
				}
				if (this._request != null)
				{
					return this._request.LegacyGetConfiguration();
				}
				return null;
			}
			set
			{
				this._configuration = value;
				this._configurationSet = true;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00006BE3 File Offset: 0x00004DE3
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00006C09 File Offset: 0x00004E09
		public override bool IncludeErrorDetail
		{
			get
			{
				if (this._includeErrorDetailSet)
				{
					return this._includeErrorDetail;
				}
				return this._request != null && this._request.LegacyShouldIncludeErrorDetail();
			}
			set
			{
				this._includeErrorDetail = value;
				this._includeErrorDetailSet = true;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006C19 File Offset: 0x00004E19
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00006C3F File Offset: 0x00004E3F
		public override bool IsLocal
		{
			get
			{
				if (this._isLocalSet)
				{
					return this._isLocal;
				}
				return this._request != null && this._request.LegacyIsLocal();
			}
			set
			{
				this._isLocal = value;
				this._isLocalSet = true;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006C4F File Offset: 0x00004E4F
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00006C75 File Offset: 0x00004E75
		public override IHttpRouteData RouteData
		{
			get
			{
				if (this._routeDataSet)
				{
					return this._routeData;
				}
				if (this._request != null)
				{
					return this._request.LegacyGetRouteData();
				}
				return null;
			}
			set
			{
				this._routeData = value;
				this._routeDataSet = true;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00006C85 File Offset: 0x00004E85
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00006CAB File Offset: 0x00004EAB
		public override UrlHelper Url
		{
			get
			{
				if (this._urlSet)
				{
					return this._url;
				}
				if (this._request != null)
				{
					return new UrlHelper(this._request);
				}
				return null;
			}
			set
			{
				this._url = value;
				this._urlSet = true;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006CBC File Offset: 0x00004EBC
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00006CEA File Offset: 0x00004EEA
		public override string VirtualPathRoot
		{
			get
			{
				if (this._virtualPathRootSet)
				{
					return this._virtualPathRoot;
				}
				HttpConfiguration configuration = this.Configuration;
				if (configuration != null)
				{
					return configuration.VirtualPathRoot;
				}
				return null;
			}
			set
			{
				this._virtualPathRoot = value;
				this._virtualPathRootSet = true;
			}
		}

		// Token: 0x04000063 RID: 99
		private HttpRequestMessage _request;

		// Token: 0x04000064 RID: 100
		private X509Certificate2 _certificate;

		// Token: 0x04000065 RID: 101
		private bool _certificateSet;

		// Token: 0x04000066 RID: 102
		private HttpConfiguration _configuration;

		// Token: 0x04000067 RID: 103
		private bool _configurationSet;

		// Token: 0x04000068 RID: 104
		private bool _includeErrorDetail;

		// Token: 0x04000069 RID: 105
		private bool _includeErrorDetailSet;

		// Token: 0x0400006A RID: 106
		private bool _isLocal;

		// Token: 0x0400006B RID: 107
		private bool _isLocalSet;

		// Token: 0x0400006C RID: 108
		private IHttpRouteData _routeData;

		// Token: 0x0400006D RID: 109
		private bool _routeDataSet;

		// Token: 0x0400006E RID: 110
		private UrlHelper _url;

		// Token: 0x0400006F RID: 111
		private bool _urlSet;

		// Token: 0x04000070 RID: 112
		private string _virtualPathRoot;

		// Token: 0x04000071 RID: 113
		private bool _virtualPathRootSet;
	}
}
