using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000043 RID: 67
	internal enum Machine : ushort
	{
		// Token: 0x04000227 RID: 551
		Unknown,
		// Token: 0x04000228 RID: 552
		I386 = 332,
		// Token: 0x04000229 RID: 553
		WceMipsV2 = 361,
		// Token: 0x0400022A RID: 554
		Alpha = 388,
		// Token: 0x0400022B RID: 555
		SH3 = 418,
		// Token: 0x0400022C RID: 556
		SH3Dsp,
		// Token: 0x0400022D RID: 557
		SH3E,
		// Token: 0x0400022E RID: 558
		SH4 = 422,
		// Token: 0x0400022F RID: 559
		SH5 = 424,
		// Token: 0x04000230 RID: 560
		Arm = 448,
		// Token: 0x04000231 RID: 561
		Thumb = 450,
		// Token: 0x04000232 RID: 562
		ArmThumb2 = 452,
		// Token: 0x04000233 RID: 563
		AM33 = 467,
		// Token: 0x04000234 RID: 564
		PowerPC = 496,
		// Token: 0x04000235 RID: 565
		PowerPCFP,
		// Token: 0x04000236 RID: 566
		IA64 = 512,
		// Token: 0x04000237 RID: 567
		MIPS16 = 614,
		// Token: 0x04000238 RID: 568
		Alpha64 = 644,
		// Token: 0x04000239 RID: 569
		MipsFpu = 870,
		// Token: 0x0400023A RID: 570
		MipsFpu16 = 1126,
		// Token: 0x0400023B RID: 571
		Tricore = 1312,
		// Token: 0x0400023C RID: 572
		Ebc = 3772,
		// Token: 0x0400023D RID: 573
		Amd64 = 34404,
		// Token: 0x0400023E RID: 574
		M32R = 36929
	}
}
