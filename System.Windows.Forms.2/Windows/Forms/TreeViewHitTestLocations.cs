using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000420 RID: 1056
	[Flags]
	[ComVisible(true)]
	public enum TreeViewHitTestLocations
	{
		// Token: 0x040027A8 RID: 10152
		None = 1,
		// Token: 0x040027A9 RID: 10153
		Image = 2,
		// Token: 0x040027AA RID: 10154
		Label = 4,
		// Token: 0x040027AB RID: 10155
		Indent = 8,
		// Token: 0x040027AC RID: 10156
		AboveClientArea = 256,
		// Token: 0x040027AD RID: 10157
		BelowClientArea = 512,
		// Token: 0x040027AE RID: 10158
		LeftOfClientArea = 2048,
		// Token: 0x040027AF RID: 10159
		RightOfClientArea = 1024,
		// Token: 0x040027B0 RID: 10160
		RightOfLabel = 32,
		// Token: 0x040027B1 RID: 10161
		StateImage = 64,
		// Token: 0x040027B2 RID: 10162
		PlusMinus = 16
	}
}
