using System;

namespace System.Net
{
	// Token: 0x02000212 RID: 530
	[Flags]
	internal enum Alg
	{
		// Token: 0x0400159C RID: 5532
		Any = 0,
		// Token: 0x0400159D RID: 5533
		ClassSignture = 8192,
		// Token: 0x0400159E RID: 5534
		ClassEncrypt = 24576,
		// Token: 0x0400159F RID: 5535
		ClassHash = 32768,
		// Token: 0x040015A0 RID: 5536
		ClassKeyXch = 40960,
		// Token: 0x040015A1 RID: 5537
		TypeRSA = 1024,
		// Token: 0x040015A2 RID: 5538
		TypeBlock = 1536,
		// Token: 0x040015A3 RID: 5539
		TypeStream = 2048,
		// Token: 0x040015A4 RID: 5540
		TypeDH = 2560,
		// Token: 0x040015A5 RID: 5541
		NameDES = 1,
		// Token: 0x040015A6 RID: 5542
		NameRC2 = 2,
		// Token: 0x040015A7 RID: 5543
		Name3DES = 3,
		// Token: 0x040015A8 RID: 5544
		NameAES_128 = 14,
		// Token: 0x040015A9 RID: 5545
		NameAES_192 = 15,
		// Token: 0x040015AA RID: 5546
		NameAES_256 = 16,
		// Token: 0x040015AB RID: 5547
		NameAES = 17,
		// Token: 0x040015AC RID: 5548
		NameRC4 = 1,
		// Token: 0x040015AD RID: 5549
		NameMD5 = 3,
		// Token: 0x040015AE RID: 5550
		NameSHA = 4,
		// Token: 0x040015AF RID: 5551
		NameSHA256 = 12,
		// Token: 0x040015B0 RID: 5552
		NameSHA384 = 13,
		// Token: 0x040015B1 RID: 5553
		NameSHA512 = 14,
		// Token: 0x040015B2 RID: 5554
		NameDH_Ephem = 2
	}
}
