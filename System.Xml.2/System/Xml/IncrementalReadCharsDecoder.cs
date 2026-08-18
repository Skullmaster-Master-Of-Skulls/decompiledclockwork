using System;

namespace System.Xml
{
	// Token: 0x020000B0 RID: 176
	internal class IncrementalReadCharsDecoder : IncrementalReadDecoder
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x0001623A File Offset: 0x0001443A
		internal IncrementalReadCharsDecoder()
		{
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00016242 File Offset: 0x00014442
		internal override int DecodedCount
		{
			get
			{
				return this.curIndex - this.startIndex;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00016251 File Offset: 0x00014451
		internal override bool IsFull
		{
			get
			{
				return this.curIndex == this.endIndex;
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00016264 File Offset: 0x00014464
		internal override int Decode(char[] chars, int startPos, int len)
		{
			int num = this.endIndex - this.curIndex;
			if (num > len)
			{
				num = len;
			}
			Buffer.BlockCopy(chars, startPos * 2, this.buffer, this.curIndex * 2, num * 2);
			this.curIndex += num;
			return num;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000162B0 File Offset: 0x000144B0
		internal override int Decode(string str, int startPos, int len)
		{
			int num = this.endIndex - this.curIndex;
			if (num > len)
			{
				num = len;
			}
			str.CopyTo(startPos, this.buffer, this.curIndex, num);
			this.curIndex += num;
			return num;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000162F4 File Offset: 0x000144F4
		internal override void Reset()
		{
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000162F6 File Offset: 0x000144F6
		internal override void SetNextOutputBuffer(Array buffer, int index, int count)
		{
			this.buffer = (char[])buffer;
			this.startIndex = index;
			this.curIndex = index;
			this.endIndex = index + count;
		}

		// Token: 0x0400027C RID: 636
		private char[] buffer;

		// Token: 0x0400027D RID: 637
		private int startIndex;

		// Token: 0x0400027E RID: 638
		private int curIndex;

		// Token: 0x0400027F RID: 639
		private int endIndex;
	}
}
