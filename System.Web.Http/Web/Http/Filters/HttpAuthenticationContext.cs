using System;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x02000070 RID: 112
	public class HttpAuthenticationContext
	{
		// Token: 0x0600030D RID: 781 RVA: 0x00009FBA File Offset: 0x000081BA
		public HttpAuthenticationContext(HttpActionContext actionContext, IPrincipal principal)
		{
			if (actionContext == null)
			{
				throw new ArgumentNullException("actionContext");
			}
			this.ActionContext = actionContext;
			this.Principal = principal;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00009FDE File Offset: 0x000081DE
		// (set) Token: 0x0600030F RID: 783 RVA: 0x00009FE6 File Offset: 0x000081E6
		public HttpActionContext ActionContext { get; private set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00009FEF File Offset: 0x000081EF
		// (set) Token: 0x06000311 RID: 785 RVA: 0x00009FF7 File Offset: 0x000081F7
		public IPrincipal Principal { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000A000 File Offset: 0x00008200
		// (set) Token: 0x06000313 RID: 787 RVA: 0x0000A008 File Offset: 0x00008208
		public IHttpActionResult ErrorResult { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000A011 File Offset: 0x00008211
		public HttpRequestMessage Request
		{
			get
			{
				return this.ActionContext.Request;
			}
		}
	}
}
