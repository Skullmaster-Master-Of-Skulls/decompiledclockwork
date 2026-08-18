using System;

namespace System.Windows.Forms
{
	// Token: 0x02000237 RID: 567
	[Flags]
	public enum DragDropEffects
	{
		// Token: 0x04000F1D RID: 3869
		None = 0,
		// Token: 0x04000F1E RID: 3870
		Copy = 1,
		// Token: 0x04000F1F RID: 3871
		Move = 2,
		// Token: 0x04000F20 RID: 3872
		Link = 4,
		// Token: 0x04000F21 RID: 3873
		Scroll = -2147483648,
		// Token: 0x04000F22 RID: 3874
		All = -2147483645
	}
}
