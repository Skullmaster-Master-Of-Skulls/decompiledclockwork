using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000A6 RID: 166
	public enum FailoverEvent
	{
		// Token: 0x040004C1 RID: 1217
		End = 1,
		// Token: 0x040004C2 RID: 1218
		Abort,
		// Token: 0x040004C3 RID: 1219
		Reauth = 4,
		// Token: 0x040004C4 RID: 1220
		Begin = 8,
		// Token: 0x040004C5 RID: 1221
		Error = 16
	}
}
