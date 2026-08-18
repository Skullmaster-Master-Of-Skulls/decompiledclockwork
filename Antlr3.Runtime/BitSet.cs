using System;
using System.Collections.Generic;
using System.Text;

namespace Antlr.Runtime
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public sealed class BitSet : ICloneable
	{
		// Token: 0x06000083 RID: 131 RVA: 0x000033A2 File Offset: 0x000015A2
		public BitSet() : this(64)
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000033AC File Offset: 0x000015AC
		[CLSCompliant(false)]
		public BitSet(ulong[] bits)
		{
			this._bits = bits;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000033BC File Offset: 0x000015BC
		public BitSet(IEnumerable<int> items) : this()
		{
			foreach (int el in items)
			{
				this.Add(el);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000340C File Offset: 0x0000160C
		public BitSet(int nbits)
		{
			this._bits = new ulong[(nbits - 1 >> 6) + 1];
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003428 File Offset: 0x00001628
		public static BitSet Of(int el)
		{
			BitSet bitSet = new BitSet(el + 1);
			bitSet.Add(el);
			return bitSet;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003448 File Offset: 0x00001648
		public static BitSet Of(int a, int b)
		{
			BitSet bitSet = new BitSet(Math.Max(a, b) + 1);
			bitSet.Add(a);
			bitSet.Add(b);
			return bitSet;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003474 File Offset: 0x00001674
		public static BitSet Of(int a, int b, int c)
		{
			BitSet bitSet = new BitSet();
			bitSet.Add(a);
			bitSet.Add(b);
			bitSet.Add(c);
			return bitSet;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000034A0 File Offset: 0x000016A0
		public static BitSet Of(int a, int b, int c, int d)
		{
			BitSet bitSet = new BitSet();
			bitSet.Add(a);
			bitSet.Add(b);
			bitSet.Add(c);
			bitSet.Add(d);
			return bitSet;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000034D0 File Offset: 0x000016D0
		public BitSet Or(BitSet a)
		{
			if (a == null)
			{
				return this;
			}
			BitSet bitSet = (BitSet)this.Clone();
			bitSet.OrInPlace(a);
			return bitSet;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000034F8 File Offset: 0x000016F8
		public void Add(int el)
		{
			int num = BitSet.WordNumber(el);
			if (num >= this._bits.Length)
			{
				this.GrowToInclude(el);
			}
			this._bits[num] |= BitSet.BitMask(el);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000353C File Offset: 0x0000173C
		public void GrowToInclude(int bit)
		{
			int size = Math.Max(this._bits.Length << 1, BitSet.NumWordsToHold(bit));
			this.SetSize(size);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003568 File Offset: 0x00001768
		public void OrInPlace(BitSet a)
		{
			if (a == null)
			{
				return;
			}
			if (a._bits.Length > this._bits.Length)
			{
				this.SetSize(a._bits.Length);
			}
			int num = Math.Min(this._bits.Length, a._bits.Length);
			for (int i = num - 1; i >= 0; i--)
			{
				this._bits[i] |= a._bits[i];
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000035DD File Offset: 0x000017DD
		private void SetSize(int nwords)
		{
			Array.Resize<ulong>(ref this._bits, nwords);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000035EC File Offset: 0x000017EC
		private static ulong BitMask(int bitNumber)
		{
			int num = bitNumber & 63;
			return 1UL << num;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003605 File Offset: 0x00001805
		public object Clone()
		{
			return new BitSet((ulong[])this._bits.Clone());
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000361C File Offset: 0x0000181C
		public int Size()
		{
			int num = 0;
			for (int i = this._bits.Length - 1; i >= 0; i--)
			{
				ulong num2 = this._bits[i];
				if (num2 != 0UL)
				{
					for (int j = 63; j >= 0; j--)
					{
						if ((num2 & 1UL << j) != 0UL)
						{
							num++;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000366D File Offset: 0x0000186D
		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003674 File Offset: 0x00001874
		public override bool Equals(object other)
		{
			if (other == null || !(other is BitSet))
			{
				return false;
			}
			BitSet bitSet = (BitSet)other;
			int num = Math.Min(this._bits.Length, bitSet._bits.Length);
			for (int i = 0; i < num; i++)
			{
				if (this._bits[i] != bitSet._bits[i])
				{
					return false;
				}
			}
			if (this._bits.Length > num)
			{
				for (int j = num + 1; j < this._bits.Length; j++)
				{
					if (this._bits[j] != 0UL)
					{
						return false;
					}
				}
			}
			else if (bitSet._bits.Length > num)
			{
				for (int k = num + 1; k < bitSet._bits.Length; k++)
				{
					if (bitSet._bits[k] != 0UL)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003730 File Offset: 0x00001930
		public bool Member(int el)
		{
			if (el < 0)
			{
				return false;
			}
			int num = BitSet.WordNumber(el);
			return num < this._bits.Length && (this._bits[num] & BitSet.BitMask(el)) != 0UL;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003770 File Offset: 0x00001970
		public void Remove(int el)
		{
			int num = BitSet.WordNumber(el);
			if (num < this._bits.Length)
			{
				this._bits[num] &= ~BitSet.BitMask(el);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000037B0 File Offset: 0x000019B0
		public bool IsNil()
		{
			for (int i = this._bits.Length - 1; i >= 0; i--)
			{
				if (this._bits[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000037E1 File Offset: 0x000019E1
		private static int NumWordsToHold(int el)
		{
			return (el >> 6) + 1;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000037E8 File Offset: 0x000019E8
		public int NumBits()
		{
			return this._bits.Length << 6;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000037F4 File Offset: 0x000019F4
		public int LengthInLongWords()
		{
			return this._bits.Length;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003800 File Offset: 0x00001A00
		public int[] ToArray()
		{
			int[] array = new int[this.Size()];
			int num = 0;
			for (int i = 0; i < this._bits.Length << 6; i++)
			{
				if (this.Member(i))
				{
					array[num++] = i;
				}
			}
			return array;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003842 File Offset: 0x00001A42
		private static int WordNumber(int bit)
		{
			return bit >> 6;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003847 File Offset: 0x00001A47
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003850 File Offset: 0x00001A50
		public string ToString(string[] tokenNames)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string value = ",";
			bool flag = false;
			stringBuilder.Append('{');
			for (int i = 0; i < this._bits.Length << 6; i++)
			{
				if (this.Member(i))
				{
					if (i > 0 && flag)
					{
						stringBuilder.Append(value);
					}
					if (tokenNames != null)
					{
						stringBuilder.Append(tokenNames[i]);
					}
					else
					{
						stringBuilder.Append(i);
					}
					flag = true;
				}
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x04000018 RID: 24
		private const int BITS = 64;

		// Token: 0x04000019 RID: 25
		private const int LOG_BITS = 6;

		// Token: 0x0400001A RID: 26
		private const int MOD_MASK = 63;

		// Token: 0x0400001B RID: 27
		private ulong[] _bits;
	}
}
