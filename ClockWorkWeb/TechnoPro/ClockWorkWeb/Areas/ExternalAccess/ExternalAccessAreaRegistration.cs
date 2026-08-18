using System;
using System.Web.Mvc;

namespace TechnoPro.ClockWorkWeb.Areas.ExternalAccess
{
	// Token: 0x02000162 RID: 354
	public class ExternalAccessAreaRegistration : AreaRegistration
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x00048FBC File Offset: 0x000471BC
		public override string AreaName
		{
			get
			{
				return "ExternalAccess";
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00048FD3 File Offset: 0x000471D3
		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute("ExternalAccess_default", "ExternalAccess/{controller}/{action}/{id}", new
			{
				action = "Index",
				id = UrlParameter.Optional
			});
		}
	}
}
