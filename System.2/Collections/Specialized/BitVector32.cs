using System;
using System.Text;

namespace System.Collections.Specialized
{
	// Token: 0x020003A7 RID: 935
	public struct BitVector32
	{
		// Token: 0x060022EA RID: 8938 RVA: 0x000A62F4 File Offset: 0x000A44F4
		public BitVector32(int data)
		{
			this.data = (uint)data;
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000A62FD File Offset: 0x000A44FD
		public BitVector32(BitVector32 value)
		{
			this.data = value.data;
		}

		// Token: 0x170008DB RID: 2267
		public bool this[int bit]
		{
			get
			{
				return ((ulong)this.data & (ulong)((long)bit)) == (ulong)bit;
			}
			set
			{
				if (value)
				{
					this.data |= (uint)bit;
					return;
				}
				this.data &= (uint)(~(uint)bit);
			}
		}

		// Token: 0x170008DC RID: 2268
		public int this[BitVector32.Section section]
		{
			get
			{
				return (int)((this.data & (uint)((uint)section.Mask << (int)section.Offset)) >> (int)section.Offset);
			}
			set
			{
				value <<= (int)section.Offset;
				int num = (65535 & (int)section.Mask) << (int)section.Offset;
				this.data = ((this.data & (uint)(~(uint)num)) | (uint)(value & num));
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x000A63AB File Offset: 0x000A45AB
		public int Data
		{
			get
			{
				return (int)this.data;
			}
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000A63B4 File Offset: 0x000A45B4
		private static short CountBitsSet(short mask)
		{
			short num = 0;
			while ((mask & 1) != 0)
			{
				num += 1;
				mask = (short)(mask >> 1);
			}
			return num;
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000A63D6 File Offset: 0x000A45D6
		public static int CreateMask()
		{
			return BitVector32.CreateMask(0);
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x000A63DE File Offset: 0x000A45DE
		public static int CreateMask(int previous)
		{
			if (previous == 0)
			{
				return 1;
			}
			if (previous == -2147483648)
			{
				throw new InvalidOperationException(SR.GetString("BitVectorFull"));
			}
			return previous << 1;
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000A6400 File Offset: 0x000A4600
		private static short CreateMaskFromHighValue(short highValue)
		{
			short num = 16;
			while (((int)highValue & 32768) == 0)
			{
				num -= 1;
				highValue = (short)(highValue << 1);
			}
			ushort num2 = 0;
			while (num > 0)
			{
				num -= 1;
				num2 = (ushort)(num2 << 1);
				num2 |= 1;
			}
			return (short)num2;
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000A643F File Offset: 0x000A463F
		public static BitVector32.Section CreateSection(short maxValue)
		{
			return BitVector32.CreateSectionHelper(maxValue, 0, 0);
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x000A6449 File Offset: 0x000A4649
		public static BitVector32.Section CreateSection(short maxValue, BitVector32.Section previous)
		{
			return BitVector32.CreateSectionHelper(maxValue, previous.Mask, previous.Offset);
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000A6460 File Offset: 0x000A4660
		private static BitVector32.Section CreateSectionHelper(short maxValue, short priorMask, short priorOffset)
		{
			if (maxValue < 1)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidValue", new object[]
				{
					"maxValue",
					0
				}), "maxValue");
			}
			short num = priorOffset + BitVector32.CountBitsSet(priorMask);
			if (num >= 32)
			{
				throw new InvalidOperationException(SR.GetString("BitVectorFull"));
			}
			return new BitVector32.Section(BitVector32.CreateMaskFromHighValue(maxValue), num);
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000A64C8 File Offset: 0x000A46C8
		public override bool Equals(object o)
		{
			return o is BitVector32 && this.data == ((BitVector32)o).data;
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000A64E7 File Offset: 0x000A46E7
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000A64FC File Offset: 0x000A46FC
		public static string ToString(BitVector32 value)
		{
			StringBuilder stringBuilder = new StringBuilder(45);
			stringBuilder.Append("BitVector32{");
			int num = (int)value.data;
			for (int i = 0; i < 32; i++)
			{
				if (((long)num & (long)((ulong)-2147483648)) != 0L)
				{
					stringBuilder.Append("1");
				}
				else
				{
					stringBuilder.Append("0");
				}
				num <<= 1;
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x000A656C File Offset: 0x000A476C
		public override string ToString()
		{
			return BitVector32.ToString(this);
		}

		// Token: 0x04001FBB RID: 8123
		private uint data;

		// Token: 0x020007E7 RID: 2023
		public struct Section
		{
			// Token: 0x060043E4 RID: 17380 RVA: 0x0011DD0D File Offset: 0x0011BF0D
			internal Section(short mask, short offset)
			{
				this.mask = mask;
				this.offset = offset;
			}

			// Token: 0x17000F5B RID: 3931
			// (get) Token: 0x060043E5 RID: 17381 RVA: 0x0011DD1D File Offset: 0x0011BF1D
			public short Mask
			{
				get
				{
					return this.mask;
				}
			}

			// Token: 0x17000F5C RID: 3932
			// (get) Token: 0x060043E6 RID: 17382 RVA: 0x0011DD25 File Offset: 0x0011BF25
			public short Offset
			{
				get
				{
					return this.offset;
				}
			}

			// Token: 0x060043E7 RID: 17383 RVA: 0x0011DD2D File Offset: 0x0011BF2D
			public override bool Equals(object o)
			{
				return o is BitVector32.Section && this.Equals((BitVector32.Section)o);
			}

			// Token: 0x060043E8 RID: 17384 RVA: 0x0011DD45 File Offset: 0x0011BF45
			public bool Equals(BitVector32.Section obj)
			{
				return obj.mask == this.mask && obj.offset == this.offset;
			}

			// Token: 0x060043E9 RID: 17385 RVA: 0x0011DD65 File Offset: 0x0011BF65
			public static bool operator ==(BitVector32.Section a, BitVector32.Section b)
			{
				return a.Equals(b);
			}

			// Token: 0x060043EA RID: 17386 RVA: 0x0011DD6F File Offset: 0x0011BF6F
			public static bool operator !=(BitVector32.Section a, BitVector32.Section b)
			{
				return !(a == b);
			}

			// Token: 0x060043EB RID: 17387 RVA: 0x0011DD7B File Offset: 0x0011BF7B
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x060043EC RID: 17388 RVA: 0x0011DD90 File Offset: 0x0011BF90
			public static string ToString(BitVector32.Section value)
			{
				return string.Concat(new string[]
				{
					"Section{0x",
					Convert.ToString(value.Mask, 16),
					", 0x",
					Convert.ToString(value.Offset, 16),
					"}"
				});
			}

			// Token: 0x060043ED RID: 17389 RVA: 0x0011DDE2 File Offset: 0x0011BFE2
			public override string ToString()
			{
				return BitVector32.Section.ToString(this);
			}

			// Token: 0x040034FA RID: 13562
			private readonly short mask;

			// Token: 0x040034FB RID: 13563
			private readonly short offset;
		}
	}
}
