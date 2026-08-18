using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004BF RID: 1215
	internal enum QueryNodeType : byte
	{
		// Token: 0x0400250D RID: 9485
		Any,
		// Token: 0x0400250E RID: 9486
		Root,
		// Token: 0x0400250F RID: 9487
		Attribute,
		// Token: 0x04002510 RID: 9488
		Element = 4,
		// Token: 0x04002511 RID: 9489
		Text = 8,
		// Token: 0x04002512 RID: 9490
		Comment = 16,
		// Token: 0x04002513 RID: 9491
		Processing = 32,
		// Token: 0x04002514 RID: 9492
		Namespace = 64,
		// Token: 0x04002515 RID: 9493
		Multiple = 128,
		// Token: 0x04002516 RID: 9494
		ChildNodes = 188,
		// Token: 0x04002517 RID: 9495
		Ancestor = 133,
		// Token: 0x04002518 RID: 9496
		All = 255
	}
}
