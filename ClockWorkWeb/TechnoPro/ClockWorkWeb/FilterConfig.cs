using System;
using System.Web.Mvc;
using TechnoPro.Common.UI.ClientManager.Web.Auth.Authentication;

namespace TechnoPro.ClockWorkWeb
{
	// Token: 0x02000015 RID: 21
	public class FilterConfig
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00003CBE File Offset: 0x00001EBE
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new CustomAuthorizeAttribute());
			filters.Add(new HandleErrorAttribute());
		}
	}
}
