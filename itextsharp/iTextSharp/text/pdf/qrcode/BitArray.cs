using System;
using System.Text;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x0200039C RID: 924
	public sealed class BitArray
	{
		// Token: 0x06001FF8 RID: 8184 RVA: 0x000BECFD File Offset: 0x000BDCFD
		public BitArray(int size)
		{
			if (size < 1)
			{
				throw new ArgumentException("size must be at least 1");
			}
			this.size = size;
			this.bits = BitArray.MakeArray(size);
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x000BED27 File Offset: 0x000BDD27
		public int GetSize()
		{
			return this.size;
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x000BED2F File Offset: 0x000BDD2F
		public bool Get(int i)
		{
			return (this.bits[i >> 5] & 1 << i) != 0;
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x000BED4B File Offset: 0x000BDD4B
		public void Set(int i)
		{
			this.bits[i >> 5] |= 1 << i;
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x000BED70 File Offset: 0x000BDD70
		public void Flip(int i)
		{
			this.bits[i >> 5] ^= 1 << i;
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x000BED95 File Offset: 0x000BDD95
		public void SetBulk(int i, int newBits)
		{
			this.bits[i >> 5] = newBits;
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x000BEDA4 File Offset: 0x000BDDA4
		public void Clear()
		{
			int num = this.bits.Length;
			for (int i = 0; i < num; i++)
			{
				this.bits[i] = 0;
			}
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x000BEDD0 File Offset: 0x000BDDD0
		public bool IsRange(int start, int end, bool value)
		{
			if (end < start)
			{
				throw new ArgumentException();
			}
			if (end == start)
			{
				return true;
			}
			end--;
			int num = start >> 5;
			int num2 = end >> 5;
			for (int i = num; i <= num2; i++)
			{
				int num3 = (i > num) ? 0 : (start & 31);
				int num4 = (i < num2) ? 31 : (end & 31);
				int num5;
				if (num3 == 0 && num4 == 31)
				{
					num5 = -1;
				}
				else
				{
					num5 = 0;
					for (int j = num3; j <= num4; j++)
					{
						num5 |= 1 << j;
					}
				}
				if ((this.bits[i] & num5) != (value ? num5 : 0))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x000BEE66 File Offset: 0x000BDE66
		public int[] GetBitArray()
		{
			return this.bits;
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000BEE70 File Offset: 0x000BDE70
		public void Reverse()
		{
			int[] array = new int[this.bits.Length];
			int num = this.size;
			for (int i = 0; i < num; i++)
			{
				if (this.Get(num - i - 1))
				{
					array[i >> 5] |= 1 << i;
				}
			}
			this.bits = array;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x000BEED0 File Offset: 0x000BDED0
		private static int[] MakeArray(int size)
		{
			int num = size >> 5;
			if ((size & 31) != 0)
			{
				num++;
			}
			return new int[num];
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x000BEEF4 File Offset: 0x000BDEF4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.size);
			for (int i = 0; i < this.size; i++)
			{
				if ((i & 7) == 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(this.Get(i) ? 'X' : '.');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001605 RID: 5637
		public int[] bits;

		// Token: 0x04001606 RID: 5638
		public int size;
	}
}
