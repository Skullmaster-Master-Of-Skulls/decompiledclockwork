using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000503 RID: 1283
	[Flags]
	public enum TreeNodeTypes
	{
		// Token: 0x0400247A RID: 9338
		None = 0,
		// Token: 0x0400247B RID: 9339
		Root = 1,
		// Token: 0x0400247C RID: 9340
		Parent = 2,
		// Token: 0x0400247D RID: 9341
		Leaf = 4,
		// Token: 0x0400247E RID: 9342
		All = 7
	}
}
