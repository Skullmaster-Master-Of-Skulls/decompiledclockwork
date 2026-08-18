using System;
using System.Web.Mvc;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat
{
	// Token: 0x02000165 RID: 357
	public class AlternateFormatAreaRegistration : AreaRegistration
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00049010 File Offset: 0x00047210
		public override string AreaName
		{
			get
			{
				return "AlternateFormat";
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00049027 File Offset: 0x00047227
		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute("AlternateFormat_default", "AlternateFormat/{controller}/{action}/{id}", new
			{
				action = "Index",
				id = UrlParameter.Optional
			});
		}
	}
}
