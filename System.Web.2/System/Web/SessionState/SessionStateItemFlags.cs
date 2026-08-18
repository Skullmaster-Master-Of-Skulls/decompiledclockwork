using System;

namespace System.Web.SessionState
{
	// Token: 0x02000121 RID: 289
	[Flags]
	internal enum SessionStateItemFlags
	{
		// Token: 0x040013E6 RID: 5094
		None = 0,
		// Token: 0x040013E7 RID: 5095
		Uninitialized = 1,
		// Token: 0x040013E8 RID: 5096
		IgnoreCacheItemRemoved = 2
	}
}
