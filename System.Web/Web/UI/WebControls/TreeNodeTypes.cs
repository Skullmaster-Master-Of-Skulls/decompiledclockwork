using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000674 RID: 1652
	[Flags]
	public enum TreeNodeTypes
	{
		// Token: 0x04002D45 RID: 11589
		None = 0,
		// Token: 0x04002D46 RID: 11590
		Root = 1,
		// Token: 0x04002D47 RID: 11591
		Parent = 2,
		// Token: 0x04002D48 RID: 11592
		Leaf = 4,
		// Token: 0x04002D49 RID: 11593
		All = 7
	}
}
