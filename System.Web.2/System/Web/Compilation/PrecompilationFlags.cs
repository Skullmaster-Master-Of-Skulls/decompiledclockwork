using System;

namespace System.Web.Compilation
{
	// Token: 0x02000828 RID: 2088
	[Flags]
	public enum PrecompilationFlags
	{
		// Token: 0x0400339A RID: 13210
		Default = 0,
		// Token: 0x0400339B RID: 13211
		Updatable = 1,
		// Token: 0x0400339C RID: 13212
		OverwriteTarget = 2,
		// Token: 0x0400339D RID: 13213
		ForceDebug = 4,
		// Token: 0x0400339E RID: 13214
		Clean = 8,
		// Token: 0x0400339F RID: 13215
		CodeAnalysis = 16,
		// Token: 0x040033A0 RID: 13216
		AllowPartiallyTrustedCallers = 32,
		// Token: 0x040033A1 RID: 13217
		DelaySign = 64,
		// Token: 0x040033A2 RID: 13218
		FixedNames = 128,
		// Token: 0x040033A3 RID: 13219
		IgnoreBadImageFormatException = 256
	}
}
