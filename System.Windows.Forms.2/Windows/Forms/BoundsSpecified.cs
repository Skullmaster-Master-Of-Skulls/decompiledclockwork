using System;

namespace System.Windows.Forms
{
	// Token: 0x02000142 RID: 322
	[Flags]
	public enum BoundsSpecified
	{
		// Token: 0x04000727 RID: 1831
		X = 1,
		// Token: 0x04000728 RID: 1832
		Y = 2,
		// Token: 0x04000729 RID: 1833
		Width = 4,
		// Token: 0x0400072A RID: 1834
		Height = 8,
		// Token: 0x0400072B RID: 1835
		Location = 3,
		// Token: 0x0400072C RID: 1836
		Size = 12,
		// Token: 0x0400072D RID: 1837
		All = 15,
		// Token: 0x0400072E RID: 1838
		None = 0
	}
}
