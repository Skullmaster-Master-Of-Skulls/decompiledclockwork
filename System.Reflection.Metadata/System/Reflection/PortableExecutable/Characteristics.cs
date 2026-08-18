using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000021 RID: 33
	public enum Characteristics : ushort
	{
		// Token: 0x040000E1 RID: 225
		RelocsStripped = 1,
		// Token: 0x040000E2 RID: 226
		ExecutableImage,
		// Token: 0x040000E3 RID: 227
		LineNumsStripped = 4,
		// Token: 0x040000E4 RID: 228
		LocalSymsStripped = 8,
		// Token: 0x040000E5 RID: 229
		AggressiveWSTrim = 16,
		// Token: 0x040000E6 RID: 230
		LargeAddressAware = 32,
		// Token: 0x040000E7 RID: 231
		BytesReversedLo = 128,
		// Token: 0x040000E8 RID: 232
		Bit32Machine = 256,
		// Token: 0x040000E9 RID: 233
		DebugStripped = 512,
		// Token: 0x040000EA RID: 234
		RemovableRunFromSwap = 1024,
		// Token: 0x040000EB RID: 235
		NetRunFromSwap = 2048,
		// Token: 0x040000EC RID: 236
		System = 4096,
		// Token: 0x040000ED RID: 237
		Dll = 8192,
		// Token: 0x040000EE RID: 238
		UpSystemOnly = 16384,
		// Token: 0x040000EF RID: 239
		BytesReversedHi = 32768
	}
}
