using System;

namespace System.Reflection.Metadata.Decoding
{
	// Token: 0x0200014E RID: 334
	internal enum PrimitiveTypeCode : byte
	{
		// Token: 0x040008DA RID: 2266
		Boolean = 2,
		// Token: 0x040008DB RID: 2267
		Byte = 5,
		// Token: 0x040008DC RID: 2268
		SByte = 4,
		// Token: 0x040008DD RID: 2269
		Char = 3,
		// Token: 0x040008DE RID: 2270
		Single = 12,
		// Token: 0x040008DF RID: 2271
		Double,
		// Token: 0x040008E0 RID: 2272
		Int16 = 6,
		// Token: 0x040008E1 RID: 2273
		Int32 = 8,
		// Token: 0x040008E2 RID: 2274
		Int64 = 10,
		// Token: 0x040008E3 RID: 2275
		UInt16 = 7,
		// Token: 0x040008E4 RID: 2276
		UInt32 = 9,
		// Token: 0x040008E5 RID: 2277
		UInt64 = 11,
		// Token: 0x040008E6 RID: 2278
		IntPtr = 24,
		// Token: 0x040008E7 RID: 2279
		UIntPtr,
		// Token: 0x040008E8 RID: 2280
		Object = 28,
		// Token: 0x040008E9 RID: 2281
		String = 14,
		// Token: 0x040008EA RID: 2282
		TypedReference = 22,
		// Token: 0x040008EB RID: 2283
		Void = 1
	}
}
