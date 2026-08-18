using System;

namespace System.Drawing.Drawing2D
{
	// Token: 0x020000CE RID: 206
	public enum PathPointType
	{
		// Token: 0x040009F5 RID: 2549
		Start,
		// Token: 0x040009F6 RID: 2550
		Line,
		// Token: 0x040009F7 RID: 2551
		Bezier = 3,
		// Token: 0x040009F8 RID: 2552
		PathTypeMask = 7,
		// Token: 0x040009F9 RID: 2553
		DashMode = 16,
		// Token: 0x040009FA RID: 2554
		PathMarker = 32,
		// Token: 0x040009FB RID: 2555
		CloseSubpath = 128,
		// Token: 0x040009FC RID: 2556
		Bezier3 = 3
	}
}
