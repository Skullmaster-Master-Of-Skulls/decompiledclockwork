using System;

namespace System.Web.Routing
{
	// Token: 0x02000152 RID: 338
	internal sealed class UrlAuthFailureHandler : IHttpHandler
	{
		// Token: 0x0600138A RID: 5002 RVA: 0x00003ABB File Offset: 0x00001CBB
		public void ProcessRequest(HttpContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x000097B7 File Offset: 0x000079B7
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}
