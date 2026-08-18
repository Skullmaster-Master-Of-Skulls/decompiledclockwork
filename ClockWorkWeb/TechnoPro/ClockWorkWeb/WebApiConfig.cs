using System;
using System.Web.Http;

namespace TechnoPro.ClockWorkWeb
{
	// Token: 0x02000017 RID: 23
	public static class WebApiConfig
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00003D0D File Offset: 0x00001F0D
		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new
			{
				id = RouteParameter.Optional
			});
		}
	}
}
