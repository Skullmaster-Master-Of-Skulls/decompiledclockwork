using System;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x02000047 RID: 71
	[Flags]
	internal enum EsInkFlags
	{
		// Token: 0x04000348 RID: 840
		RenderInk = 1,
		// Token: 0x04000349 RID: 841
		RenderShape = 2,
		// Token: 0x0400034A RID: 842
		HitTestInk = 4,
		// Token: 0x0400034B RID: 843
		InkAnnotation = 8,
		// Token: 0x0400034C RID: 844
		UseRenderInk = 65536,
		// Token: 0x0400034D RID: 845
		UseRenderShape = 131072,
		// Token: 0x0400034E RID: 846
		UseHitTestInk = 262144,
		// Token: 0x0400034F RID: 847
		UseInkAnnotation = 524288
	}
}
