using System;
using System.Web.Mvc;

namespace TechnoPro.ClockWorkWeb.Areas.Loa
{
	// Token: 0x0200015F RID: 351
	public class LoaAreaRegistration : AreaRegistration
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00048C7F File Offset: 0x00046E7F
		public override string AreaName
		{
			get
			{
				return "Loa";
			}
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00048C86 File Offset: 0x00046E86
		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute("Loa_default", "Loa/{controller}/{action}/{id}", new
			{
				action = "Index",
				id = UrlParameter.Optional
			});
		}
	}
}
