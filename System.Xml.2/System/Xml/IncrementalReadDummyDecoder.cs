using System;

namespace System.Xml
{
	// Token: 0x020000AF RID: 175
	internal class IncrementalReadDummyDecoder : IncrementalReadDecoder
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00016222 File Offset: 0x00014422
		internal override int DecodedCount
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00016225 File Offset: 0x00014425
		internal override bool IsFull
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00016228 File Offset: 0x00014428
		internal override void SetNextOutputBuffer(Array array, int offset, int len)
		{
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001622A File Offset: 0x0001442A
		internal override int Decode(char[] chars, int startPos, int len)
		{
			return len;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001622D File Offset: 0x0001442D
		internal override int Decode(string str, int startPos, int len)
		{
			return len;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00016230 File Offset: 0x00014430
		internal override void Reset()
		{
		}
	}
}
