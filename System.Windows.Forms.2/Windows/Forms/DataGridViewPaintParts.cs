using System;

namespace System.Windows.Forms
{
	// Token: 0x02000207 RID: 519
	[Flags]
	public enum DataGridViewPaintParts
	{
		// Token: 0x04000E1C RID: 3612
		None = 0,
		// Token: 0x04000E1D RID: 3613
		All = 127,
		// Token: 0x04000E1E RID: 3614
		Background = 1,
		// Token: 0x04000E1F RID: 3615
		Border = 2,
		// Token: 0x04000E20 RID: 3616
		ContentBackground = 4,
		// Token: 0x04000E21 RID: 3617
		ContentForeground = 8,
		// Token: 0x04000E22 RID: 3618
		ErrorIcon = 16,
		// Token: 0x04000E23 RID: 3619
		Focus = 32,
		// Token: 0x04000E24 RID: 3620
		SelectionBackground = 64
	}
}
