using System;

namespace System.Web.Compilation
{
	// Token: 0x0200080C RID: 2060
	[Flags]
	public enum BuildProviderAppliesTo
	{
		// Token: 0x04003343 RID: 13123
		Web = 1,
		// Token: 0x04003344 RID: 13124
		Code = 2,
		// Token: 0x04003345 RID: 13125
		Resources = 4,
		// Token: 0x04003346 RID: 13126
		All = 7
	}
}
