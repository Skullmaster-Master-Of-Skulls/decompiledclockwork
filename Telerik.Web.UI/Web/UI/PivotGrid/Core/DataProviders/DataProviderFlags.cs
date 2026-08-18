using System;

namespace Telerik.Web.UI.PivotGrid.Core.DataProviders
{
	// Token: 0x02000691 RID: 1681
	[Flags]
	internal enum DataProviderFlags
	{
		// Token: 0x0400105A RID: 4186
		None = 0,
		// Token: 0x0400105B RID: 4187
		NeedsRefresh = 1,
		// Token: 0x0400105C RID: 4188
		ForceRefresh = 2,
		// Token: 0x0400105D RID: 4189
		ResetStatus = 4,
		// Token: 0x0400105E RID: 4190
		All = 7
	}
}
