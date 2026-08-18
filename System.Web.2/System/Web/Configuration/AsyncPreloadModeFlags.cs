using System;

namespace System.Web.Configuration
{
	// Token: 0x0200069C RID: 1692
	[Flags]
	public enum AsyncPreloadModeFlags
	{
		// Token: 0x04002B00 RID: 11008
		None = 0,
		// Token: 0x04002B01 RID: 11009
		Form = 1,
		// Token: 0x04002B02 RID: 11010
		FormMultiPart = 2,
		// Token: 0x04002B03 RID: 11011
		NonForm = 4,
		// Token: 0x04002B04 RID: 11012
		AllFormTypes = 3,
		// Token: 0x04002B05 RID: 11013
		All = 7
	}
}
