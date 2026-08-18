using System;

namespace System.Xml
{
	// Token: 0x020000AE RID: 174
	internal abstract class IncrementalReadDecoder
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600060E RID: 1550
		internal abstract int DecodedCount { get; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600060F RID: 1551
		internal abstract bool IsFull { get; }

		// Token: 0x06000610 RID: 1552
		internal abstract void SetNextOutputBuffer(Array array, int offset, int len);

		// Token: 0x06000611 RID: 1553
		internal abstract int Decode(char[] chars, int startPos, int len);

		// Token: 0x06000612 RID: 1554
		internal abstract int Decode(string str, int startPos, int len);

		// Token: 0x06000613 RID: 1555
		internal abstract void Reset();
	}
}
