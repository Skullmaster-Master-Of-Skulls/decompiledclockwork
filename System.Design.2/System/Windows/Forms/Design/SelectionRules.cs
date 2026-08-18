using System;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200032B RID: 811
	[Flags]
	public enum SelectionRules
	{
		// Token: 0x0400189D RID: 6301
		None = 0,
		// Token: 0x0400189E RID: 6302
		Moveable = 268435456,
		// Token: 0x0400189F RID: 6303
		Visible = 1073741824,
		// Token: 0x040018A0 RID: 6304
		Locked = -2147483648,
		// Token: 0x040018A1 RID: 6305
		TopSizeable = 1,
		// Token: 0x040018A2 RID: 6306
		BottomSizeable = 2,
		// Token: 0x040018A3 RID: 6307
		LeftSizeable = 4,
		// Token: 0x040018A4 RID: 6308
		RightSizeable = 8,
		// Token: 0x040018A5 RID: 6309
		AllSizeable = 15
	}
}
