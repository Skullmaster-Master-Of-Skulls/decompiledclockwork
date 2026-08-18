using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000045 RID: 69
	internal enum Characteristics : ushort
	{
		// Token: 0x04000243 RID: 579
		RelocsStripped = 1,
		// Token: 0x04000244 RID: 580
		ExecutableImage,
		// Token: 0x04000245 RID: 581
		LineNumsStripped = 4,
		// Token: 0x04000246 RID: 582
		LocalSymsStripped = 8,
		// Token: 0x04000247 RID: 583
		AggressiveWSTrim = 16,
		// Token: 0x04000248 RID: 584
		LargeAddressAware = 32,
		// Token: 0x04000249 RID: 585
		BytesReversedLo = 128,
		// Token: 0x0400024A RID: 586
		Bit32Machine = 256,
		// Token: 0x0400024B RID: 587
		DebugStripped = 512,
		// Token: 0x0400024C RID: 588
		RemovableRunFromSwap = 1024,
		// Token: 0x0400024D RID: 589
		NetRunFromSwap = 2048,
		// Token: 0x0400024E RID: 590
		System = 4096,
		// Token: 0x0400024F RID: 591
		Dll = 8192,
		// Token: 0x04000250 RID: 592
		UpSystemOnly = 16384,
		// Token: 0x04000251 RID: 593
		BytesReversedHi = 32768
	}
}
