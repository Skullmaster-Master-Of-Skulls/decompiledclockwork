using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Web.Http.Routing;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000022 RID: 34
	public class HttpRequestContext
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004B6A File Offset: 0x00002D6A
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00004B72 File Offset: 0x00002D72
		public virtual X509Certificate2 ClientCertificate { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004B7B File Offset: 0x00002D7B
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00004B83 File Offset: 0x00002D83
		public virtual HttpConfiguration Configuration { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004B8C File Offset: 0x00002D8C
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00004B94 File Offset: 0x00002D94
		public virtual bool IncludeErrorDetail { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004B9D File Offset: 0x00002D9D
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00004BA5 File Offset: 0x00002DA5
		public virtual bool IsLocal { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004BAE File Offset: 0x00002DAE
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00004BB6 File Offset: 0x00002DB6
		public virtual IPrincipal Principal { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004BBF File Offset: 0x00002DBF
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00004BC7 File Offset: 0x00002DC7
		public virtual IHttpRouteData RouteData { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004BD0 File Offset: 0x00002DD0
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00004BD8 File Offset: 0x00002DD8
		public virtual UrlHelper Url { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004BE1 File Offset: 0x00002DE1
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004BE9 File Offset: 0x00002DE9
		public virtual string VirtualPathRoot { get; set; }
	}
}
