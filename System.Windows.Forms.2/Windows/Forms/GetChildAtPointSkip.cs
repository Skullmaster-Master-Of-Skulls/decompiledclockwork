using System;

namespace System.Windows.Forms
{
	// Token: 0x0200026F RID: 623
	[Flags]
	public enum GetChildAtPointSkip
	{
		// Token: 0x0400106A RID: 4202
		None = 0,
		// Token: 0x0400106B RID: 4203
		Invisible = 1,
		// Token: 0x0400106C RID: 4204
		Disabled = 2,
		// Token: 0x0400106D RID: 4205
		Transparent = 4
	}
}
