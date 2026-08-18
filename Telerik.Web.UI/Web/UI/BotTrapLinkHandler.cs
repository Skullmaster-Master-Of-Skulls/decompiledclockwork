using System;
using System.Web;
using System.Web.Caching;

namespace Telerik.Web.UI
{
	// Token: 0x020016C3 RID: 5827
	internal class BotTrapLinkHandler : IHttpHandler
	{
		// Token: 0x170044EF RID: 17647
		// (get) Token: 0x0600E0E7 RID: 57575 RVA: 0x0031F8DB File Offset: 0x0031DADB
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600E0E8 RID: 57576 RVA: 0x0031F8E0 File Offset: 0x0031DAE0
		public void ProcessRequest(HttpContext context)
		{
			HttpApplication applicationInstance = context.ApplicationInstance;
			string text = applicationInstance.Request.QueryString["guid"];
			if (!string.IsNullOrEmpty(text))
			{
				HttpRuntime.Cache.Add(text, "BotGuid", null, DateTime.Now.AddMinutes(5.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
			}
			applicationInstance.Response.StatusCode = 200;
			context.ApplicationInstance.CompleteRequest();
		}
	}
}
