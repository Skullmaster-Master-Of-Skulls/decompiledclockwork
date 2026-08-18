using System;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200048E RID: 1166
	[Flags]
	public enum ToolStripItemDesignerAvailability
	{
		// Token: 0x040033FE RID: 13310
		None = 0,
		// Token: 0x040033FF RID: 13311
		ToolStrip = 1,
		// Token: 0x04003400 RID: 13312
		MenuStrip = 2,
		// Token: 0x04003401 RID: 13313
		ContextMenuStrip = 4,
		// Token: 0x04003402 RID: 13314
		StatusStrip = 8,
		// Token: 0x04003403 RID: 13315
		All = 15
	}
}
