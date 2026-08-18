using System;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x02000042 RID: 66
	internal enum PathType
	{
		// Token: 0x0400030C RID: 780
		Unknown = -1,
		// Token: 0x0400030D RID: 781
		LineTo,
		// Token: 0x0400030E RID: 782
		CurveTo,
		// Token: 0x0400030F RID: 783
		MoveTo,
		// Token: 0x04000310 RID: 784
		Close,
		// Token: 0x04000311 RID: 785
		End,
		// Token: 0x04000312 RID: 786
		EscapeBase = 160,
		// Token: 0x04000313 RID: 787
		EscapeExtension = 160,
		// Token: 0x04000314 RID: 788
		AngleEllipseTo,
		// Token: 0x04000315 RID: 789
		AngleEllipse,
		// Token: 0x04000316 RID: 790
		ArcTo,
		// Token: 0x04000317 RID: 791
		Arc,
		// Token: 0x04000318 RID: 792
		ClockwiseArcTo,
		// Token: 0x04000319 RID: 793
		ClockwiseArc,
		// Token: 0x0400031A RID: 794
		EllipticalQuadrantX,
		// Token: 0x0400031B RID: 795
		EllipticalQuadrantY,
		// Token: 0x0400031C RID: 796
		QuadraticBezier,
		// Token: 0x0400031D RID: 797
		NoFill,
		// Token: 0x0400031E RID: 798
		NoLine,
		// Token: 0x0400031F RID: 799
		EscapeAutoLine,
		// Token: 0x04000320 RID: 800
		EscapeAutoCurve,
		// Token: 0x04000321 RID: 801
		EscapeCornerLine,
		// Token: 0x04000322 RID: 802
		EscapeCornerCurve,
		// Token: 0x04000323 RID: 803
		EscapeSmoothLine,
		// Token: 0x04000324 RID: 804
		EscapeSmoothCurve,
		// Token: 0x04000325 RID: 805
		EscapeSymmetricLine,
		// Token: 0x04000326 RID: 806
		EscapeSymmetricCurve,
		// Token: 0x04000327 RID: 807
		EscapeFreeForm,
		// Token: 0x04000328 RID: 808
		FillColor,
		// Token: 0x04000329 RID: 809
		LineColor
	}
}
