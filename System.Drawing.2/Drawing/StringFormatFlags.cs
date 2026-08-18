using System;

namespace System.Drawing
{
	// Token: 0x02000048 RID: 72
	[Flags]
	public enum StringFormatFlags
	{
		// Token: 0x04000586 RID: 1414
		DirectionRightToLeft = 1,
		// Token: 0x04000587 RID: 1415
		DirectionVertical = 2,
		// Token: 0x04000588 RID: 1416
		FitBlackBox = 4,
		// Token: 0x04000589 RID: 1417
		DisplayFormatControl = 32,
		// Token: 0x0400058A RID: 1418
		NoFontFallback = 1024,
		// Token: 0x0400058B RID: 1419
		MeasureTrailingSpaces = 2048,
		// Token: 0x0400058C RID: 1420
		NoWrap = 4096,
		// Token: 0x0400058D RID: 1421
		LineLimit = 8192,
		// Token: 0x0400058E RID: 1422
		NoClip = 16384
	}
}
