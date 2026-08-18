using System;

namespace Spire.Layouting
{
	// Token: 0x0200008B RID: 139
	[Flags]
	internal enum TextLineType
	{
		// Token: 0x04000920 RID: 2336
		None = 0,
		// Token: 0x04000921 RID: 2337
		NewLineBreak = 1,
		// Token: 0x04000922 RID: 2338
		LayoutBreak = 2,
		// Token: 0x04000923 RID: 2339
		FirstParagraphLine = 4,
		// Token: 0x04000924 RID: 2340
		LastParagraphLine = 8
	}
}
