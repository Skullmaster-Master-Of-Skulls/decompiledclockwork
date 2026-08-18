using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace System.Web.Http.Batch
{
	// Token: 0x02000023 RID: 35
	internal class BatchHttpRequestContext : HttpRequestContext
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00004BF2 File Offset: 0x00002DF2
		public BatchHttpRequestContext(HttpRequestContext batchContext)
		{
			if (batchContext == null)
			{
				throw new ArgumentNullException("batchContext");
			}
			this._batchContext = batchContext;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004C0F File Offset: 0x00002E0F
		public HttpRequestContext BatchContext
		{
			get
			{
				return this._batchContext;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004C17 File Offset: 0x00002E17
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004C24 File Offset: 0x00002E24
		public override X509Certificate2 ClientCertificate
		{
			get
			{
				return this._batchContext.ClientCertificate;
			}
			set
			{
				this._batchContext.ClientCertificate = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004C32 File Offset: 0x00002E32
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00004C3A File Offset: 0x00002E3A
		public override HttpConfiguration Configuration
		{
			get
			{
				return base.Configuration;
			}
			set
			{
				base.Configuration = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004C43 File Offset: 0x00002E43
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004C50 File Offset: 0x00002E50
		public override bool IncludeErrorDetail
		{
			get
			{
				return this._batchContext.IncludeErrorDetail;
			}
			set
			{
				this._batchContext.IncludeErrorDetail = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004C5E File Offset: 0x00002E5E
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004C6B File Offset: 0x00002E6B
		public override bool IsLocal
		{
			get
			{
				return this._batchContext.IsLocal;
			}
			set
			{
				this._batchContext.IsLocal = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004C79 File Offset: 0x00002E79
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004C86 File Offset: 0x00002E86
		public override IPrincipal Principal
		{
			get
			{
				return this._batchContext.Principal;
			}
			set
			{
				this._batchContext.Principal = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004C94 File Offset: 0x00002E94
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00004C9C File Offset: 0x00002E9C
		public override IHttpRouteData RouteData
		{
			get
			{
				return base.RouteData;
			}
			set
			{
				base.RouteData = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004CA5 File Offset: 0x00002EA5
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00004CAD File Offset: 0x00002EAD
		public override UrlHelper Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				base.Url = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004CB6 File Offset: 0x00002EB6
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004CC3 File Offset: 0x00002EC3
		public override string VirtualPathRoot
		{
			get
			{
				return this._batchContext.VirtualPathRoot;
			}
			set
			{
				this._batchContext.VirtualPathRoot = value;
			}
		}

		// Token: 0x04000044 RID: 68
		private readonly HttpRequestContext _batchContext;
	}
}
