using System;

namespace System.Xml
{
	// Token: 0x02000059 RID: 89
	internal class IncrementalReadDummyDecoder : IncrementalReadDecoder
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00010E12 File Offset: 0x0000FE12
		internal override int DecodedCount
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00010E15 File Offset: 0x0000FE15
		internal override bool IsFull
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00010E18 File Offset: 0x0000FE18
		internal override void SetNextOutputBuffer(Array array, int offset, int len)
		{
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00010E1A File Offset: 0x0000FE1A
		internal override int Decode(char[] chars, int startPos, int len)
		{
			return len;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00010E1D File Offset: 0x0000FE1D
		internal override int Decode(string str, int startPos, int len)
		{
			return len;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010E20 File Offset: 0x0000FE20
		internal override void Reset()
		{
		}
	}
}
