using System;

namespace System.Windows.Forms
{
	// Token: 0x0200042D RID: 1069
	[Flags]
	public enum ValidationConstraints
	{
		// Token: 0x040027CC RID: 10188
		None = 0,
		// Token: 0x040027CD RID: 10189
		Selectable = 1,
		// Token: 0x040027CE RID: 10190
		Enabled = 2,
		// Token: 0x040027CF RID: 10191
		Visible = 4,
		// Token: 0x040027D0 RID: 10192
		TabStop = 8,
		// Token: 0x040027D1 RID: 10193
		ImmediateChildren = 16
	}
}
