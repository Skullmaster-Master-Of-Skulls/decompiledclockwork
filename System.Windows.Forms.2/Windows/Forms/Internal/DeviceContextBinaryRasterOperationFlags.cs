using System;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004D8 RID: 1240
	[Flags]
	internal enum DeviceContextBinaryRasterOperationFlags
	{
		// Token: 0x04003521 RID: 13601
		Black = 1,
		// Token: 0x04003522 RID: 13602
		NotMergePen = 2,
		// Token: 0x04003523 RID: 13603
		MaskNotPen = 3,
		// Token: 0x04003524 RID: 13604
		NotCopyPen = 4,
		// Token: 0x04003525 RID: 13605
		MaskPenNot = 5,
		// Token: 0x04003526 RID: 13606
		Not = 6,
		// Token: 0x04003527 RID: 13607
		XorPen = 7,
		// Token: 0x04003528 RID: 13608
		NotMaskPen = 8,
		// Token: 0x04003529 RID: 13609
		MaskPen = 9,
		// Token: 0x0400352A RID: 13610
		NotXorPen = 10,
		// Token: 0x0400352B RID: 13611
		Nop = 11,
		// Token: 0x0400352C RID: 13612
		MergeNotPen = 12,
		// Token: 0x0400352D RID: 13613
		CopyPen = 13,
		// Token: 0x0400352E RID: 13614
		MergePenNot = 14,
		// Token: 0x0400352F RID: 13615
		MergePen = 15,
		// Token: 0x04003530 RID: 13616
		White = 16
	}
}
