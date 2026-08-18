using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000049 RID: 73
	[Flags]
	internal enum SectionCharacteristics : uint
	{
		// Token: 0x04000273 RID: 627
		TypeReg = 0U,
		// Token: 0x04000274 RID: 628
		TypeDSect = 1U,
		// Token: 0x04000275 RID: 629
		TypeNoLoad = 2U,
		// Token: 0x04000276 RID: 630
		TypeGroup = 4U,
		// Token: 0x04000277 RID: 631
		TypeNoPad = 8U,
		// Token: 0x04000278 RID: 632
		TypeCopy = 16U,
		// Token: 0x04000279 RID: 633
		ContainsCode = 32U,
		// Token: 0x0400027A RID: 634
		ContainsInitializedData = 64U,
		// Token: 0x0400027B RID: 635
		ContainsUninitializedData = 128U,
		// Token: 0x0400027C RID: 636
		LinkerOther = 256U,
		// Token: 0x0400027D RID: 637
		LinkerInfo = 512U,
		// Token: 0x0400027E RID: 638
		TypeOver = 1024U,
		// Token: 0x0400027F RID: 639
		LinkerRemove = 2048U,
		// Token: 0x04000280 RID: 640
		LinkerComdat = 4096U,
		// Token: 0x04000281 RID: 641
		MemProtected = 16384U,
		// Token: 0x04000282 RID: 642
		NoDeferSpecExc = 16384U,
		// Token: 0x04000283 RID: 643
		GPRel = 32768U,
		// Token: 0x04000284 RID: 644
		MemFardata = 32768U,
		// Token: 0x04000285 RID: 645
		MemSysheap = 65536U,
		// Token: 0x04000286 RID: 646
		MemPurgeable = 131072U,
		// Token: 0x04000287 RID: 647
		Mem16Bit = 131072U,
		// Token: 0x04000288 RID: 648
		MemLocked = 262144U,
		// Token: 0x04000289 RID: 649
		MemPreload = 524288U,
		// Token: 0x0400028A RID: 650
		Align1Bytes = 1048576U,
		// Token: 0x0400028B RID: 651
		Align2Bytes = 2097152U,
		// Token: 0x0400028C RID: 652
		Align4Bytes = 3145728U,
		// Token: 0x0400028D RID: 653
		Align8Bytes = 4194304U,
		// Token: 0x0400028E RID: 654
		Align16Bytes = 5242880U,
		// Token: 0x0400028F RID: 655
		Align32Bytes = 6291456U,
		// Token: 0x04000290 RID: 656
		Align64Bytes = 7340032U,
		// Token: 0x04000291 RID: 657
		Align128Bytes = 8388608U,
		// Token: 0x04000292 RID: 658
		Align256Bytes = 9437184U,
		// Token: 0x04000293 RID: 659
		Align512Bytes = 10485760U,
		// Token: 0x04000294 RID: 660
		Align1024Bytes = 11534336U,
		// Token: 0x04000295 RID: 661
		Align2048Bytes = 12582912U,
		// Token: 0x04000296 RID: 662
		Align4096Bytes = 13631488U,
		// Token: 0x04000297 RID: 663
		Align8192Bytes = 14680064U,
		// Token: 0x04000298 RID: 664
		AlignMask = 15728640U,
		// Token: 0x04000299 RID: 665
		LinkerNRelocOvfl = 16777216U,
		// Token: 0x0400029A RID: 666
		MemDiscardable = 33554432U,
		// Token: 0x0400029B RID: 667
		MemNotCached = 67108864U,
		// Token: 0x0400029C RID: 668
		MemNotPaged = 134217728U,
		// Token: 0x0400029D RID: 669
		MemShared = 268435456U,
		// Token: 0x0400029E RID: 670
		MemExecute = 536870912U,
		// Token: 0x0400029F RID: 671
		MemRead = 1073741824U,
		// Token: 0x040002A0 RID: 672
		MemWrite = 2147483648U
	}
}
