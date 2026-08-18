using System;

namespace System.Drawing.Imaging
{
	// Token: 0x020000A2 RID: 162
	[Flags]
	public enum ImageFlags
	{
		// Token: 0x040008CF RID: 2255
		None = 0,
		// Token: 0x040008D0 RID: 2256
		Scalable = 1,
		// Token: 0x040008D1 RID: 2257
		HasAlpha = 2,
		// Token: 0x040008D2 RID: 2258
		HasTranslucent = 4,
		// Token: 0x040008D3 RID: 2259
		PartiallyScalable = 8,
		// Token: 0x040008D4 RID: 2260
		ColorSpaceRgb = 16,
		// Token: 0x040008D5 RID: 2261
		ColorSpaceCmyk = 32,
		// Token: 0x040008D6 RID: 2262
		ColorSpaceGray = 64,
		// Token: 0x040008D7 RID: 2263
		ColorSpaceYcbcr = 128,
		// Token: 0x040008D8 RID: 2264
		ColorSpaceYcck = 256,
		// Token: 0x040008D9 RID: 2265
		HasRealDpi = 4096,
		// Token: 0x040008DA RID: 2266
		HasRealPixelSize = 8192,
		// Token: 0x040008DB RID: 2267
		ReadOnly = 65536,
		// Token: 0x040008DC RID: 2268
		Caching = 131072
	}
}
