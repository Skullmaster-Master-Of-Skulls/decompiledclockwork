using System;

namespace System.Drawing.Printing
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	public enum PaperSourceKind
	{
		// Token: 0x040006A7 RID: 1703
		Upper = 1,
		// Token: 0x040006A8 RID: 1704
		Lower,
		// Token: 0x040006A9 RID: 1705
		Middle,
		// Token: 0x040006AA RID: 1706
		Manual,
		// Token: 0x040006AB RID: 1707
		Envelope,
		// Token: 0x040006AC RID: 1708
		ManualFeed,
		// Token: 0x040006AD RID: 1709
		AutomaticFeed,
		// Token: 0x040006AE RID: 1710
		TractorFeed,
		// Token: 0x040006AF RID: 1711
		SmallFormat,
		// Token: 0x040006B0 RID: 1712
		LargeFormat,
		// Token: 0x040006B1 RID: 1713
		LargeCapacity,
		// Token: 0x040006B2 RID: 1714
		Cassette = 14,
		// Token: 0x040006B3 RID: 1715
		FormSource,
		// Token: 0x040006B4 RID: 1716
		Custom = 257
	}
}
