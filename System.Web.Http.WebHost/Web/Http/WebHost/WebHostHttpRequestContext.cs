using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace System.Web.Http.WebHost
{
	// Token: 0x0200002A RID: 42
	internal class WebHostHttpRequestContext : HttpRequestContext
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00006CEC File Offset: 0x00004EEC
		public WebHostHttpRequestContext(HttpContextBase contextBase, HttpRequestBase requestBase, HttpRequestMessage request)
		{
			this._contextBase = contextBase;
			this._requestBase = requestBase;
			this._request = request;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006D09 File Offset: 0x00004F09
		public HttpContextBase Context
		{
			get
			{
				return this._contextBase;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00006D11 File Offset: 0x00004F11
		public HttpRequestBase WebRequest
		{
			get
			{
				return this._requestBase;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006D19 File Offset: 0x00004F19
		public HttpRequestMessage Request
		{
			get
			{
				return this._request;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006D24 File Offset: 0x00004F24
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00006D9B File Offset: 0x00004F9B
		public override X509Certificate2 ClientCertificate
		{
			get
			{
				if (!this._clientCertificateSet)
				{
					X509Certificate2 clientCertificate;
					if (this._requestBase.ClientCertificate != null && this._requestBase.ClientCertificate.Certificate != null && this._requestBase.ClientCertificate.Certificate.Length > 0)
					{
						clientCertificate = new X509Certificate2(this._requestBase.ClientCertificate.Certificate);
					}
					else
					{
						clientCertificate = null;
					}
					this._clientCertificate = clientCertificate;
					this._clientCertificateSet = true;
				}
				return this._clientCertificate;
			}
			set
			{
				this._clientCertificate = value;
				this._clientCertificateSet = true;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006DAB File Offset: 0x00004FAB
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00006DCD File Offset: 0x00004FCD
		public override HttpConfiguration Configuration
		{
			get
			{
				if (!this._configurationSet)
				{
					this._configuration = GlobalConfiguration.Configuration;
					this._configurationSet = true;
				}
				return this._configuration;
			}
			set
			{
				this._configuration = value;
				this._configurationSet = true;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006DE0 File Offset: 0x00004FE0
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00006E5B File Offset: 0x0000505B
		public override bool IncludeErrorDetail
		{
			get
			{
				if (!this._includeErrorDetailSet)
				{
					IncludeErrorDetailPolicy includeErrorDetailPolicy;
					if (this._configuration != null)
					{
						includeErrorDetailPolicy = this._configuration.IncludeErrorDetailPolicy;
					}
					else
					{
						includeErrorDetailPolicy = IncludeErrorDetailPolicy.Default;
					}
					bool includeErrorDetail;
					switch (includeErrorDetailPolicy)
					{
					case IncludeErrorDetailPolicy.Default:
						includeErrorDetail = !this._contextBase.IsCustomErrorEnabled;
						goto IL_5A;
					case IncludeErrorDetailPolicy.LocalOnly:
						includeErrorDetail = this.IsLocal;
						goto IL_5A;
					case IncludeErrorDetailPolicy.Always:
						includeErrorDetail = true;
						goto IL_5A;
					}
					includeErrorDetail = false;
					IL_5A:
					this._includeErrorDetail = includeErrorDetail;
					this._includeErrorDetailSet = true;
				}
				return this._includeErrorDetail;
			}
			set
			{
				this._includeErrorDetail = value;
				this._includeErrorDetailSet = true;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00006E6B File Offset: 0x0000506B
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00006E93 File Offset: 0x00005093
		public override bool IsLocal
		{
			get
			{
				if (!this._isLocalSet)
				{
					this._isLocal = this._requestBase.IsLocal;
					this._isLocalSet = true;
				}
				return this._isLocal;
			}
			set
			{
				this._isLocal = value;
				this._isLocalSet = true;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006EA3 File Offset: 0x000050A3
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00006EB0 File Offset: 0x000050B0
		public override IPrincipal Principal
		{
			get
			{
				return this._contextBase.User;
			}
			set
			{
				this._contextBase.User = value;
				Thread.CurrentPrincipal = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00006EC4 File Offset: 0x000050C4
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00006EEC File Offset: 0x000050EC
		public override UrlHelper Url
		{
			get
			{
				if (!this._urlSet)
				{
					this._url = new UrlHelper(this._request);
					this._urlSet = true;
				}
				return this._url;
			}
			set
			{
				this._url = value;
				this._urlSet = true;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006EFC File Offset: 0x000050FC
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00006F3D File Offset: 0x0000513D
		public override string VirtualPathRoot
		{
			get
			{
				if (!this._virtualPathRootSet)
				{
					string virtualPathRoot;
					if (this._configuration != null)
					{
						virtualPathRoot = this._configuration.VirtualPathRoot;
					}
					else
					{
						virtualPathRoot = null;
					}
					this._virtualPathRoot = virtualPathRoot;
					this._virtualPathRootSet = true;
				}
				return this._virtualPathRoot;
			}
			set
			{
				this._virtualPathRoot = value;
				this._virtualPathRootSet = true;
			}
		}

		// Token: 0x04000051 RID: 81
		private readonly HttpContextBase _contextBase;

		// Token: 0x04000052 RID: 82
		private readonly HttpRequestBase _requestBase;

		// Token: 0x04000053 RID: 83
		private readonly HttpRequestMessage _request;

		// Token: 0x04000054 RID: 84
		private X509Certificate2 _clientCertificate;

		// Token: 0x04000055 RID: 85
		private bool _clientCertificateSet;

		// Token: 0x04000056 RID: 86
		private HttpConfiguration _configuration;

		// Token: 0x04000057 RID: 87
		private bool _configurationSet;

		// Token: 0x04000058 RID: 88
		private bool _includeErrorDetail;

		// Token: 0x04000059 RID: 89
		private bool _includeErrorDetailSet;

		// Token: 0x0400005A RID: 90
		private bool _isLocal;

		// Token: 0x0400005B RID: 91
		private bool _isLocalSet;

		// Token: 0x0400005C RID: 92
		private UrlHelper _url;

		// Token: 0x0400005D RID: 93
		private bool _urlSet;

		// Token: 0x0400005E RID: 94
		private string _virtualPathRoot;

		// Token: 0x0400005F RID: 95
		private bool _virtualPathRootSet;
	}
}
