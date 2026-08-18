using System;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000045 RID: 69
	internal class DecimalInfo
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0002728C File Offset: 0x0002628C
		private DecimalInfo()
		{
		}

		// Token: 0x04000253 RID: 595
		internal const byte ExponentIndex = 3;

		// Token: 0x04000254 RID: 596
		internal const byte ScaleByteIndex = 2;

		// Token: 0x04000255 RID: 597
		internal const byte SignByteIndex = 3;

		// Token: 0x04000256 RID: 598
		internal const byte SignBitIndex = 31;

		// Token: 0x04000257 RID: 599
		internal const byte MaxPrecision = 29;
	}
}
