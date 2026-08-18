using System;

namespace System.Web.Mvc
{
	// Token: 0x020000C9 RID: 201
	public static class GlobalFilters
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000EC0D File Offset: 0x0000CE0D
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0000EC14 File Offset: 0x0000CE14
		public static GlobalFilterCollection Filters { get; private set; } = new GlobalFilterCollection();
	}
}
