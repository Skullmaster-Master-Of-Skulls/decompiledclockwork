using System;

namespace System.Security.Permissions
{
	// Token: 0x020000D1 RID: 209
	[Flags]
	[Serializable]
	public enum DataProtectionPermissionFlags
	{
		// Token: 0x040005E0 RID: 1504
		NoFlags = 0,
		// Token: 0x040005E1 RID: 1505
		ProtectData = 1,
		// Token: 0x040005E2 RID: 1506
		UnprotectData = 2,
		// Token: 0x040005E3 RID: 1507
		ProtectMemory = 4,
		// Token: 0x040005E4 RID: 1508
		UnprotectMemory = 8,
		// Token: 0x040005E5 RID: 1509
		AllFlags = 15
	}
}
