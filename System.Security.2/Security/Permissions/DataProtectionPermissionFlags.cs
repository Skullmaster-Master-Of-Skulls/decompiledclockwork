using System;

namespace System.Security.Permissions
{
	// Token: 0x0200000B RID: 11
	[Flags]
	[Serializable]
	public enum DataProtectionPermissionFlags
	{
		// Token: 0x04000061 RID: 97
		NoFlags = 0,
		// Token: 0x04000062 RID: 98
		ProtectData = 1,
		// Token: 0x04000063 RID: 99
		UnprotectData = 2,
		// Token: 0x04000064 RID: 100
		ProtectMemory = 4,
		// Token: 0x04000065 RID: 101
		UnprotectMemory = 8,
		// Token: 0x04000066 RID: 102
		AllFlags = 15
	}
}
