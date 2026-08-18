using System;

namespace System.Web.Mvc
{
	// Token: 0x020000C6 RID: 198
	public static class FilterProviders
	{
		// Token: 0x06000533 RID: 1331 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
		static FilterProviders()
		{
			FilterProviders.Providers.Add(GlobalFilters.Filters);
			FilterProviders.Providers.Add(new FilterAttributeFilterProvider());
			FilterProviders.Providers.Add(new ControllerInstanceFilterProvider());
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0000EA0D File Offset: 0x0000CC0D
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x0000EA14 File Offset: 0x0000CC14
		public static FilterProviderCollection Providers { get; private set; } = new FilterProviderCollection();
	}
}
