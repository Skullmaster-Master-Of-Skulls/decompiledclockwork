using System;

namespace System.Data
{
	// Token: 0x02000060 RID: 96
	[Flags]
	public enum ConnectionState
	{
		// Token: 0x040006CD RID: 1741
		Closed = 0,
		// Token: 0x040006CE RID: 1742
		Open = 1,
		// Token: 0x040006CF RID: 1743
		Connecting = 2,
		// Token: 0x040006D0 RID: 1744
		Executing = 4,
		// Token: 0x040006D1 RID: 1745
		Fetching = 8,
		// Token: 0x040006D2 RID: 1746
		Broken = 16
	}
}
