using System;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x0200023D RID: 573
	internal class Number
	{
		// Token: 0x0400195B RID: 6491
		internal const byte ExpOffset = 65;

		// Token: 0x0400195C RID: 6492
		internal const byte MaxByteValue = 255;

		// Token: 0x0400195D RID: 6493
		internal const byte HighBitValue = 128;

		// Token: 0x0400195E RID: 6494
		internal const byte PosExpOffset = 193;

		// Token: 0x0400195F RID: 6495
		internal const byte NegExpOffset = 62;

		// Token: 0x04001960 RID: 6496
		internal const byte PosDigitOffset = 1;

		// Token: 0x04001961 RID: 6497
		internal const byte NegDigitOffset = 101;

		// Token: 0x04001962 RID: 6498
		internal const byte NegTrailingByte = 102;

		// Token: 0x04001963 RID: 6499
		internal const byte ZeroBytesLen = 1;

		// Token: 0x04001964 RID: 6500
		internal const byte ZeroBytesExp = 128;

		// Token: 0x04001965 RID: 6501
		internal const byte NumLen = 22;

		// Token: 0x04001966 RID: 6502
		internal const byte LENINDEX = 0;

		// Token: 0x04001967 RID: 6503
		internal const byte EXPINDEX = 1;

		// Token: 0x04001968 RID: 6504
		internal const byte DIGITINDEX = 2;

		// Token: 0x04001969 RID: 6505
		internal const byte Base = 100;

		// Token: 0x0400196A RID: 6506
		internal const byte ExpBase = 2;

		// Token: 0x0400196B RID: 6507
		internal const byte MaxDigitsLen = 20;

		// Token: 0x0400196C RID: 6508
		internal const byte MaxScale = 127;

		// Token: 0x0400196D RID: 6509
		internal const short MinScale = -84;

		// Token: 0x0400196E RID: 6510
		internal const byte MaxPrecision = 38;

		// Token: 0x0400196F RID: 6511
		internal const byte BinaryFloatLength = 4;

		// Token: 0x04001970 RID: 6512
		internal const byte BinaryDoubleLength = 8;

		// Token: 0x04001971 RID: 6513
		internal const byte NEGATIVE_BYTE_TERMINATOR = 102;
	}
}
