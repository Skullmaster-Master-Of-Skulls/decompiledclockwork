using System;

namespace System.Data
{
	// Token: 0x0200009C RID: 156
	[Flags]
	public enum ConnectionState
	{
		// Token: 0x040002D7 RID: 727
		Closed = 0,
		// Token: 0x040002D8 RID: 728
		Open = 1,
		// Token: 0x040002D9 RID: 729
		Connecting = 2,
		// Token: 0x040002DA RID: 730
		Executing = 4,
		// Token: 0x040002DB RID: 731
		Fetching = 8,
		// Token: 0x040002DC RID: 732
		Broken = 16
	}
}
