using System;

namespace iTextSharp.text.pdf.codec
{
	// Token: 0x02000532 RID: 1330
	public class TIFFField : IComparable<TIFFField>
	{
		// Token: 0x06002DA0 RID: 11680 RVA: 0x00116855 File Offset: 0x00115855
		internal TIFFField()
		{
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x0011685D File Offset: 0x0011585D
		public TIFFField(int tag, int type, int count, object data)
		{
			this.tag = tag;
			this.type = type;
			this.count = count;
			this.data = data;
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x00116882 File Offset: 0x00115882
		public int GetTag()
		{
			return this.tag;
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x0011688A File Offset: 0x0011588A
		public new int GetType()
		{
			return this.type;
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x00116892 File Offset: 0x00115892
		public int GetCount()
		{
			return this.count;
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x0011689A File Offset: 0x0011589A
		public byte[] GetAsBytes()
		{
			return (byte[])this.data;
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x001168A7 File Offset: 0x001158A7
		public char[] GetAsChars()
		{
			return (char[])this.data;
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x001168B4 File Offset: 0x001158B4
		public short[] GetAsShorts()
		{
			return (short[])this.data;
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x001168C1 File Offset: 0x001158C1
		public int[] GetAsInts()
		{
			return (int[])this.data;
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x001168CE File Offset: 0x001158CE
		public long[] GetAsLongs()
		{
			return (long[])this.data;
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x001168DB File Offset: 0x001158DB
		public float[] GetAsFloats()
		{
			return (float[])this.data;
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x001168E8 File Offset: 0x001158E8
		public double[] GetAsDoubles()
		{
			return (double[])this.data;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x001168F5 File Offset: 0x001158F5
		public int[][] GetAsSRationals()
		{
			return (int[][])this.data;
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x00116902 File Offset: 0x00115902
		public long[][] GetAsRationals()
		{
			return (long[][])this.data;
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x00116910 File Offset: 0x00115910
		public int GetAsInt(int index)
		{
			switch (this.type)
			{
			case 1:
			case 7:
				return (int)(((byte[])this.data)[index] & byte.MaxValue);
			case 3:
				return (int)(((char[])this.data)[index] & char.MaxValue);
			case 6:
				return (int)((byte[])this.data)[index];
			case 8:
				return (int)((short[])this.data)[index];
			case 9:
				return ((int[])this.data)[index];
			}
			throw new InvalidCastException();
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x001169AC File Offset: 0x001159AC
		public long GetAsLong(int index)
		{
			switch (this.type)
			{
			case 1:
			case 7:
				return (long)(((byte[])this.data)[index] & byte.MaxValue);
			case 3:
				return (long)(((char[])this.data)[index] & char.MaxValue);
			case 4:
				return ((long[])this.data)[index];
			case 6:
				return (long)((ulong)((byte[])this.data)[index]);
			case 8:
				return (long)((short[])this.data)[index];
			case 9:
				return (long)((int[])this.data)[index];
			}
			throw new InvalidCastException();
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x00116A58 File Offset: 0x00115A58
		public float GetAsFloat(int index)
		{
			switch (this.type)
			{
			case 1:
				return (float)(((byte[])this.data)[index] & byte.MaxValue);
			case 3:
				return (float)(((char[])this.data)[index] & char.MaxValue);
			case 4:
				return (float)((long[])this.data)[index];
			case 5:
			{
				long[] asRational = this.GetAsRational(index);
				return (float)((double)asRational[0] / (double)asRational[1]);
			}
			case 6:
				return (float)((byte[])this.data)[index];
			case 8:
				return (float)((short[])this.data)[index];
			case 9:
				return (float)((int[])this.data)[index];
			case 10:
			{
				int[] asSRational = this.GetAsSRational(index);
				return (float)((double)asSRational[0] / (double)asSRational[1]);
			}
			case 11:
				return ((float[])this.data)[index];
			case 12:
				return (float)((double[])this.data)[index];
			}
			throw new InvalidCastException();
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x00116B58 File Offset: 0x00115B58
		public double GetAsDouble(int index)
		{
			switch (this.type)
			{
			case 1:
				return (double)(((byte[])this.data)[index] & byte.MaxValue);
			case 3:
				return (double)(((char[])this.data)[index] & char.MaxValue);
			case 4:
				return (double)((long[])this.data)[index];
			case 5:
			{
				long[] asRational = this.GetAsRational(index);
				return (double)asRational[0] / (double)asRational[1];
			}
			case 6:
				return (double)((byte[])this.data)[index];
			case 8:
				return (double)((short[])this.data)[index];
			case 9:
				return (double)((int[])this.data)[index];
			case 10:
			{
				int[] asSRational = this.GetAsSRational(index);
				return (double)asSRational[0] / (double)asSRational[1];
			}
			case 11:
				return (double)((float[])this.data)[index];
			case 12:
				return ((double[])this.data)[index];
			}
			throw new InvalidCastException();
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x00116C55 File Offset: 0x00115C55
		public string GetAsString(int index)
		{
			return ((string[])this.data)[index];
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x00116C64 File Offset: 0x00115C64
		public int[] GetAsSRational(int index)
		{
			return ((int[][])this.data)[index];
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x00116C73 File Offset: 0x00115C73
		public long[] GetAsRational(int index)
		{
			if (this.type == 4)
			{
				return this.GetAsLongs();
			}
			return ((long[][])this.data)[index];
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x00116C94 File Offset: 0x00115C94
		public int CompareTo(TIFFField o)
		{
			if (o == null)
			{
				throw new ArgumentException();
			}
			int num = o.GetTag();
			if (this.tag < num)
			{
				return -1;
			}
			if (this.tag > num)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04001F6E RID: 8046
		public const int TIFF_BYTE = 1;

		// Token: 0x04001F6F RID: 8047
		public const int TIFF_ASCII = 2;

		// Token: 0x04001F70 RID: 8048
		public const int TIFF_SHORT = 3;

		// Token: 0x04001F71 RID: 8049
		public const int TIFF_LONG = 4;

		// Token: 0x04001F72 RID: 8050
		public const int TIFF_RATIONAL = 5;

		// Token: 0x04001F73 RID: 8051
		public const int TIFF_SBYTE = 6;

		// Token: 0x04001F74 RID: 8052
		public const int TIFF_UNDEFINED = 7;

		// Token: 0x04001F75 RID: 8053
		public const int TIFF_SSHORT = 8;

		// Token: 0x04001F76 RID: 8054
		public const int TIFF_SLONG = 9;

		// Token: 0x04001F77 RID: 8055
		public const int TIFF_SRATIONAL = 10;

		// Token: 0x04001F78 RID: 8056
		public const int TIFF_FLOAT = 11;

		// Token: 0x04001F79 RID: 8057
		public const int TIFF_DOUBLE = 12;

		// Token: 0x04001F7A RID: 8058
		private int tag;

		// Token: 0x04001F7B RID: 8059
		private int type;

		// Token: 0x04001F7C RID: 8060
		private int count;

		// Token: 0x04001F7D RID: 8061
		private object data;
	}
}
