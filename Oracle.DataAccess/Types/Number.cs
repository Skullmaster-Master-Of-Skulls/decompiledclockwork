using System;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000044 RID: 68
	internal class Number
	{
		// Token: 0x06000328 RID: 808 RVA: 0x00027284 File Offset: 0x00026284
		private Number()
		{
		}

		// Token: 0x0400023E RID: 574
		internal const byte ExpOffset = 65;

		// Token: 0x0400023F RID: 575
		internal const byte MaxByteValue = 255;

		// Token: 0x04000240 RID: 576
		internal const byte HighBitValue = 128;

		// Token: 0x04000241 RID: 577
		internal const byte PosExpOffset = 193;

		// Token: 0x04000242 RID: 578
		internal const byte NegExpOffset = 62;

		// Token: 0x04000243 RID: 579
		internal const byte PosDigitOffset = 1;

		// Token: 0x04000244 RID: 580
		internal const byte NegDigitOffset = 101;

		// Token: 0x04000245 RID: 581
		internal const byte NegTrailingByte = 102;

		// Token: 0x04000246 RID: 582
		internal const byte ZeroBytesLen = 1;

		// Token: 0x04000247 RID: 583
		internal const byte ZeroBytesExp = 128;

		// Token: 0x04000248 RID: 584
		internal const byte NumLen = 22;

		// Token: 0x04000249 RID: 585
		internal const byte LENINDEX = 0;

		// Token: 0x0400024A RID: 586
		internal const byte EXPINDEX = 1;

		// Token: 0x0400024B RID: 587
		internal const byte DIGITINDEX = 2;

		// Token: 0x0400024C RID: 588
		internal const byte Base = 100;

		// Token: 0x0400024D RID: 589
		internal const byte ExpBase = 2;

		// Token: 0x0400024E RID: 590
		internal const byte MaxDigitsLen = 20;

		// Token: 0x0400024F RID: 591
		internal const byte MaxScale = 127;

		// Token: 0x04000250 RID: 592
		internal const short MinScale = -84;

		// Token: 0x04000251 RID: 593
		internal const byte MaxPrecision = 38;

		// Token: 0x04000252 RID: 594
		internal const byte BinaryFloatLength = 4;
	}
}
