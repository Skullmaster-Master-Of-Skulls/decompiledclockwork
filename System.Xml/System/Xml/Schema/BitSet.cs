using System;

namespace System.Xml.Schema
{
	// Token: 0x02000186 RID: 390
	internal sealed class BitSet
	{
		// Token: 0x060014AA RID: 5290 RVA: 0x00058372 File Offset: 0x00057372
		private BitSet()
		{
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0005837A File Offset: 0x0005737A
		public BitSet(int count)
		{
			this.count = count;
			this.bits = new uint[this.Subscript(count + 31)];
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0005839E File Offset: 0x0005739E
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000500 RID: 1280
		public bool this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x000583B0 File Offset: 0x000573B0
		public void Clear()
		{
			int num = this.bits.Length;
			int num2 = num;
			while (num2-- > 0)
			{
				this.bits[num2] = 0U;
			}
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x000583DC File Offset: 0x000573DC
		public void Clear(int index)
		{
			int num = this.Subscript(index);
			this.EnsureLength(num + 1);
			this.bits[num] &= ~(1U << index);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0005841C File Offset: 0x0005741C
		public void Set(int index)
		{
			int num = this.Subscript(index);
			this.EnsureLength(num + 1);
			this.bits[num] |= 1U << index;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0005845C File Offset: 0x0005745C
		public bool Get(int index)
		{
			bool result = false;
			if (index < this.count)
			{
				int num = this.Subscript(index);
				result = (((ulong)this.bits[num] & (ulong)(1L << (index & 31 & 31))) != 0UL);
			}
			return result;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0005849C File Offset: 0x0005749C
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

		// Token: 0x060014B3 RID: 5299 RVA: 0x00058508 File Offset: 0x00057508
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

		// Token: 0x060014B4 RID: 5300 RVA: 0x00058574 File Offset: 0x00057574
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

		// Token: 0x060014B5 RID: 5301 RVA: 0x000585C4 File Offset: 0x000575C4
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

		// Token: 0x060014B6 RID: 5302 RVA: 0x000585FC File Offset: 0x000575FC
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

		// Token: 0x060014B7 RID: 5303 RVA: 0x000586A0 File Offset: 0x000576A0
		public BitSet Clone()
		{
			return new BitSet
			{
				count = this.count,
				bits = (uint[])this.bits.Clone()
			};
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x000586D8 File Offset: 0x000576D8
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

		// Token: 0x060014B9 RID: 5305 RVA: 0x0005870C File Offset: 0x0005770C
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

		// Token: 0x060014BA RID: 5306 RVA: 0x0005874F File Offset: 0x0005774F
		private int Subscript(int bitIndex)
		{
			return bitIndex >> 5;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00058754 File Offset: 0x00057754
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

		// Token: 0x04000C80 RID: 3200
		private const int bitSlotShift = 5;

		// Token: 0x04000C81 RID: 3201
		private const int bitSlotMask = 31;

		// Token: 0x04000C82 RID: 3202
		private int count;

		// Token: 0x04000C83 RID: 3203
		private uint[] bits;
	}
}
