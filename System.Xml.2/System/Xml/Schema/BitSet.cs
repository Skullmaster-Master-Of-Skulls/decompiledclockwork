using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E0 RID: 480
	internal sealed class BitSet
	{
		// Token: 0x06001FFA RID: 8186 RVA: 0x000AC5C4 File Offset: 0x000AA7C4
		private BitSet()
		{
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x000AC5CC File Offset: 0x000AA7CC
		public BitSet(int count)
		{
			this.count = count;
			this.bits = new uint[this.Subscript(count + 31)];
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001FFC RID: 8188 RVA: 0x000AC5F0 File Offset: 0x000AA7F0
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170006A4 RID: 1700
		public bool this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x000AC604 File Offset: 0x000AA804
		public void Clear()
		{
			int num = this.bits.Length;
			int num2 = num;
			while (num2-- > 0)
			{
				this.bits[num2] = 0U;
			}
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x000AC630 File Offset: 0x000AA830
		public void Clear(int index)
		{
			int num = this.Subscript(index);
			this.EnsureLength(num + 1);
			this.bits[num] &= ~(1U << index);
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x000AC668 File Offset: 0x000AA868
		public void Set(int index)
		{
			int num = this.Subscript(index);
			this.EnsureLength(num + 1);
			this.bits[num] |= 1U << index;
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000AC6A0 File Offset: 0x000AA8A0
		public bool Get(int index)
		{
			bool result = false;
			if (index < this.count)
			{
				int num = this.Subscript(index);
				result = (((ulong)this.bits[num] & (ulong)(1L << (index & 31 & 31))) > 0UL);
			}
			return result;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x000AC6DC File Offset: 0x000AA8DC
		public int NextSet(int startFrom)
		{
			int num = startFrom + 1;
			if (num == this.count)
			{
				return -1;
			}
			int num2 = this.Subscript(num);
			num &= 31;
			uint num3;
			for (num3 = this.bits[num2] >> num; num3 == 0U; num3 = this.bits[num2])
			{
				if (++num2 == this.bits.Length)
				{
					return -1;
				}
				num = 0;
			}
			while ((num3 & 1U) == 0U)
			{
				num3 >>= 1;
				num++;
			}
			return (num2 << 5) + num;
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x000AC748 File Offset: 0x000AA948
		public void And(BitSet other)
		{
			if (this == other)
			{
				return;
			}
			int num = this.bits.Length;
			int num2 = other.bits.Length;
			int i = (num > num2) ? num2 : num;
			int num3 = i;
			while (num3-- > 0)
			{
				this.bits[num3] &= other.bits[num3];
			}
			while (i < num)
			{
				this.bits[i] = 0U;
				i++;
			}
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x000AC7AC File Offset: 0x000AA9AC
		public void Or(BitSet other)
		{
			if (this == other)
			{
				return;
			}
			int num = other.bits.Length;
			this.EnsureLength(num);
			int num2 = num;
			while (num2-- > 0)
			{
				this.bits[num2] |= other.bits[num2];
			}
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x000AC7F4 File Offset: 0x000AA9F4
		public override int GetHashCode()
		{
			int num = 1234;
			int num2 = this.bits.Length;
			while (--num2 >= 0)
			{
				num ^= (int)(this.bits[num2] * (uint)(num2 + 1));
			}
			return num ^ num;
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x000AC82C File Offset: 0x000AAA2C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			BitSet bitSet = (BitSet)obj;
			int num = this.bits.Length;
			int num2 = bitSet.bits.Length;
			int num3 = (num > num2) ? num2 : num;
			int num4 = num3;
			while (num4-- > 0)
			{
				if (this.bits[num4] != bitSet.bits[num4])
				{
					return false;
				}
			}
			if (num > num3)
			{
				int num5 = num;
				while (num5-- > num3)
				{
					if (this.bits[num5] != 0U)
					{
						return false;
					}
				}
			}
			else
			{
				int num6 = num2;
				while (num6-- > num3)
				{
					if (bitSet.bits[num6] != 0U)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x000AC8D0 File Offset: 0x000AAAD0
		public BitSet Clone()
		{
			return new BitSet
			{
				count = this.count,
				bits = (uint[])this.bits.Clone()
			};
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x000AC908 File Offset: 0x000AAB08
		public bool IsEmpty
		{
			get
			{
				uint num = 0U;
				for (int i = 0; i < this.bits.Length; i++)
				{
					num |= this.bits[i];
				}
				return num == 0U;
			}
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x000AC93C File Offset: 0x000AAB3C
		public bool Intersects(BitSet other)
		{
			int num = Math.Min(this.bits.Length, other.bits.Length);
			while (--num >= 0)
			{
				if ((this.bits[num] & other.bits[num]) != 0U)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x000AC97F File Offset: 0x000AAB7F
		private int Subscript(int bitIndex)
		{
			return bitIndex >> 5;
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x000AC984 File Offset: 0x000AAB84
		private void EnsureLength(int nRequiredLength)
		{
			if (nRequiredLength > this.bits.Length)
			{
				int num = 2 * this.bits.Length;
				if (num < nRequiredLength)
				{
					num = nRequiredLength;
				}
				uint[] destinationArray = new uint[num];
				Array.Copy(this.bits, destinationArray, this.bits.Length);
				this.bits = destinationArray;
			}
		}

		// Token: 0x04000D7A RID: 3450
		private const int bitSlotShift = 5;

		// Token: 0x04000D7B RID: 3451
		private const int bitSlotMask = 31;

		// Token: 0x04000D7C RID: 3452
		private int count;

		// Token: 0x04000D7D RID: 3453
		private uint[] bits;
	}
}
