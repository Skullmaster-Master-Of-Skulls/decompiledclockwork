using System;

namespace NLog.Targets
{
	// Token: 0x02000172 RID: 370
	[Flags]
	public enum Win32FileAttributes
	{
		// Token: 0x040003DB RID: 987
		ReadOnly = 1,
		// Token: 0x040003DC RID: 988
		Hidden = 2,
		// Token: 0x040003DD RID: 989
		System = 4,
		// Token: 0x040003DE RID: 990
		Archive = 32,
		// Token: 0x040003DF RID: 991
		Device = 64,
		// Token: 0x040003E0 RID: 992
		Normal = 128,
		// Token: 0x040003E1 RID: 993
		Temporary = 256,
		// Token: 0x040003E2 RID: 994
		SparseFile = 512,
		// Token: 0x040003E3 RID: 995
		ReparsePoint = 1024,
		// Token: 0x040003E4 RID: 996
		Compressed = 2048,
		// Token: 0x040003E5 RID: 997
		NotContentIndexed = 8192,
		// Token: 0x040003E6 RID: 998
		Encrypted = 16384,
		// Token: 0x040003E7 RID: 999
		WriteThrough = -2147483648,
		// Token: 0x040003E8 RID: 1000
		NoBuffering = 536870912,
		// Token: 0x040003E9 RID: 1001
		DeleteOnClose = 67108864,
		// Token: 0x040003EA RID: 1002
		PosixSemantics = 16777216
	}
}
