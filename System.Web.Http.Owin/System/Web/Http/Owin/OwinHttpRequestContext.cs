using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using Microsoft.Owin;

namespace System.Web.Http.Owin
{
	// Token: 0x02000015 RID: 21
	internal class OwinHttpRequestContext : HttpRequestContext
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00004C29 File Offset: 0x00002E29
		public OwinHttpRequestContext(IOwinContext context, HttpRequestMessage request)
		{
			this._context = context;
			this._request = request;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004C3F File Offset: 0x00002E3F
		public IOwinContext Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004C47 File Offset: 0x00002E47
		public HttpRequestMessage Request
		{
			get
			{
				return this._request;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004C4F File Offset: 0x00002E4F
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00004C7C File Offset: 0x00002E7C
		public override X509Certificate2 ClientCertificate
		{
			get
			{
				if (!this._clientCertificateSet)
				{
					this._clientCertificate = this._context.Get<X509Certificate2>("ssl.ClientCertificate");
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004C8C File Offset: 0x00002E8C
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00004CF3 File Offset: 0x00002EF3
		public override bool IncludeErrorDetail
		{
			get
			{
				if (!this._includeErrorDetailSet)
				{
					HttpConfiguration configuration = this.Configuration;
					IncludeErrorDetailPolicy includeErrorDetailPolicy;
					if (configuration != null)
					{
						includeErrorDetailPolicy = configuration.IncludeErrorDetailPolicy;
					}
					else
					{
						includeErrorDetailPolicy = IncludeErrorDetailPolicy.Default;
					}
					bool includeErrorDetail;
					switch (includeErrorDetailPolicy)
					{
					case IncludeErrorDetailPolicy.Default:
					case IncludeErrorDetailPolicy.LocalOnly:
						includeErrorDetail = this.IsLocal;
						goto IL_46;
					case IncludeErrorDetailPolicy.Always:
						includeErrorDetail = true;
						goto IL_46;
					}
					includeErrorDetail = false;
					IL_46:
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004D03 File Offset: 0x00002F03
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00004D30 File Offset: 0x00002F30
		public override bool IsLocal
		{
			get
			{
				if (!this._isLocalSet)
				{
					this._isLocal = this._context.Get<bool>("server.IsLocal");
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004D40 File Offset: 0x00002F40
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00004D52 File Offset: 0x00002F52
		public override IPrincipal Principal
		{
			get
			{
				return this._context.Request.User;
			}
			set
			{
				this._context.Request.User = value;
				Thread.CurrentPrincipal = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004D6B File Offset: 0x00002F6B
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00004D93 File Offset: 0x00002F93
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004DA4 File Offset: 0x00002FA4
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004DFB File Offset: 0x00002FFB
		public override string VirtualPathRoot
		{
			get
			{
				if (!this._virtualPathRootSet)
				{
					string text = this._context.Request.PathBase.ToString();
					this._virtualPathRoot = (string.IsNullOrEmpty(text) ? "/" : text);
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

		// Token: 0x0400001E RID: 30
		private readonly IOwinContext _context;

		// Token: 0x0400001F RID: 31
		private readonly HttpRequestMessage _request;

		// Token: 0x04000020 RID: 32
		private X509Certificate2 _clientCertificate;

		// Token: 0x04000021 RID: 33
		private bool _clientCertificateSet;

		// Token: 0x04000022 RID: 34
		private bool _includeErrorDetail;

		// Token: 0x04000023 RID: 35
		private bool _includeErrorDetailSet;

		// Token: 0x04000024 RID: 36
		private bool _isLocal;

		// Token: 0x04000025 RID: 37
		private bool _isLocalSet;

		// Token: 0x04000026 RID: 38
		private UrlHelper _url;

		// Token: 0x04000027 RID: 39
		private bool _urlSet;

		// Token: 0x04000028 RID: 40
		private string _virtualPathRoot;

		// Token: 0x04000029 RID: 41
		private bool _virtualPathRootSet;
	}
}
