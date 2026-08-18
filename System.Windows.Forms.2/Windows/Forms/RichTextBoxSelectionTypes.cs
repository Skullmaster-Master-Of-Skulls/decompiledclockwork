using System;

namespace System.Windows.Forms
{
	// Token: 0x0200034D RID: 845
	[Flags]
	public enum RichTextBoxSelectionTypes
	{
		// Token: 0x0400214D RID: 8525
		Empty = 0,
		// Token: 0x0400214E RID: 8526
		Text = 1,
		// Token: 0x0400214F RID: 8527
		Object = 2,
		// Token: 0x04002150 RID: 8528
		MultiChar = 4,
		// Token: 0x04002151 RID: 8529
		MultiObject = 8
	}
}
