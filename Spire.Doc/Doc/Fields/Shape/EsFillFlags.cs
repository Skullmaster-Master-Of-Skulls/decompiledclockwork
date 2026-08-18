using System;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x02000071 RID: 113
	[Flags]
	internal enum EsFillFlags
	{
		// Token: 0x0400071E RID: 1822
		RecolorFillAsPicture = 64,
		// Token: 0x0400071F RID: 1823
		UseShapeAnchor = 32,
		// Token: 0x04000720 RID: 1824
		Filled = 16,
		// Token: 0x04000721 RID: 1825
		HitTestFill = 8,
		// Token: 0x04000722 RID: 1826
		FillShape = 4,
		// Token: 0x04000723 RID: 1827
		FillUseRect = 2,
		// Token: 0x04000724 RID: 1828
		NoFillHitTest = 1
	}
}
