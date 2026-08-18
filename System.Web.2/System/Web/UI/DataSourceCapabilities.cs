using System;

namespace System.Web.UI
{
	// Token: 0x02000279 RID: 633
	[Flags]
	public enum DataSourceCapabilities
	{
		// Token: 0x04001978 RID: 6520
		None = 0,
		// Token: 0x04001979 RID: 6521
		Sort = 1,
		// Token: 0x0400197A RID: 6522
		Page = 2,
		// Token: 0x0400197B RID: 6523
		RetrieveTotalRowCount = 4
	}
}
