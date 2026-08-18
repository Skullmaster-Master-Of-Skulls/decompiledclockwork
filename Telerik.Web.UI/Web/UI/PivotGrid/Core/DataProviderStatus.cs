using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C8E RID: 3214
	public enum DataProviderStatus
	{
		// Token: 0x040020F9 RID: 8441
		Uninitialized,
		// Token: 0x040020FA RID: 8442
		Initializing,
		// Token: 0x040020FB RID: 8443
		Ready,
		// Token: 0x040020FC RID: 8444
		RetrievingData,
		// Token: 0x040020FD RID: 8445
		Canceled,
		// Token: 0x040020FE RID: 8446
		Faulted
	}
}
