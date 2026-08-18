using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000302 RID: 770
	[Flags]
	[ComVisible(true)]
	public enum MouseButtons
	{
		// Token: 0x04001449 RID: 5193
		Left = 1048576,
		// Token: 0x0400144A RID: 5194
		None = 0,
		// Token: 0x0400144B RID: 5195
		Right = 2097152,
		// Token: 0x0400144C RID: 5196
		Middle = 4194304,
		// Token: 0x0400144D RID: 5197
		XButton1 = 8388608,
		// Token: 0x0400144E RID: 5198
		XButton2 = 16777216
	}
}
