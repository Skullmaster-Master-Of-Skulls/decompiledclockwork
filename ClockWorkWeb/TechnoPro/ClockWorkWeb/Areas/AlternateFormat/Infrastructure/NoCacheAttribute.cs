using System;
using System.Web;
using System.Web.Mvc;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure
{
	// Token: 0x02000184 RID: 388
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public sealed class NoCacheAttribute : ActionFilterAttribute
	{
		// Token: 0x06000B7B RID: 2939 RVA: 0x00049970 File Offset: 0x00047B70
		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1.0));
			filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
			filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
			filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			filterContext.HttpContext.Response.Cache.SetNoStore();
			base.OnResultExecuting(filterContext);
		}
	}
}
