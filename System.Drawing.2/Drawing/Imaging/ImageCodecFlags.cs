using System;

namespace System.Drawing.Imaging
{
	// Token: 0x0200009F RID: 159
	[Flags]
	public enum ImageCodecFlags
	{
		// Token: 0x040008AD RID: 2221
		Encoder = 1,
		// Token: 0x040008AE RID: 2222
		Decoder = 2,
		// Token: 0x040008AF RID: 2223
		SupportBitmap = 4,
		// Token: 0x040008B0 RID: 2224
		SupportVector = 8,
		// Token: 0x040008B1 RID: 2225
		SeekableEncode = 16,
		// Token: 0x040008B2 RID: 2226
		BlockingDecode = 32,
		// Token: 0x040008B3 RID: 2227
		Builtin = 65536,
		// Token: 0x040008B4 RID: 2228
		System = 131072,
		// Token: 0x040008B5 RID: 2229
		User = 262144
	}
}
