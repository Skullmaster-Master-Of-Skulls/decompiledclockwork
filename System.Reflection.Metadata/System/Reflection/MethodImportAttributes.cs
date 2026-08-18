using System;

namespace System.Reflection
{
	// Token: 0x0200000C RID: 12
	[Flags]
	public enum MethodImportAttributes : short
	{
		// Token: 0x04000028 RID: 40
		None = 0,
		// Token: 0x04000029 RID: 41
		ExactSpelling = 1,
		// Token: 0x0400002A RID: 42
		BestFitMappingDisable = 32,
		// Token: 0x0400002B RID: 43
		BestFitMappingEnable = 16,
		// Token: 0x0400002C RID: 44
		BestFitMappingMask = 48,
		// Token: 0x0400002D RID: 45
		CharSetAnsi = 2,
		// Token: 0x0400002E RID: 46
		CharSetUnicode = 4,
		// Token: 0x0400002F RID: 47
		CharSetAuto = 6,
		// Token: 0x04000030 RID: 48
		CharSetMask = 6,
		// Token: 0x04000031 RID: 49
		ThrowOnUnmappableCharEnable = 4096,
		// Token: 0x04000032 RID: 50
		ThrowOnUnmappableCharDisable = 8192,
		// Token: 0x04000033 RID: 51
		ThrowOnUnmappableCharMask = 12288,
		// Token: 0x04000034 RID: 52
		SetLastError = 64,
		// Token: 0x04000035 RID: 53
		CallingConventionWinApi = 256,
		// Token: 0x04000036 RID: 54
		CallingConventionCDecl = 512,
		// Token: 0x04000037 RID: 55
		CallingConventionStdCall = 768,
		// Token: 0x04000038 RID: 56
		CallingConventionThisCall = 1024,
		// Token: 0x04000039 RID: 57
		CallingConventionFastCall = 1280,
		// Token: 0x0400003A RID: 58
		CallingConventionMask = 1792
	}
}
