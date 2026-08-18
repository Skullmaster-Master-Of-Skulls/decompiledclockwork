using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace TechnoPro.ClockWorkWeb
{
	// Token: 0x02000016 RID: 22
	public class RouteConfig
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003CD9 File Offset: 0x00001ED9
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute("Default", "{controller}/{action}/{id}", new
			{
				controller = "Home",
				action = "Index",
				id = UrlParameter.Optional
			});
		}
	}
}
