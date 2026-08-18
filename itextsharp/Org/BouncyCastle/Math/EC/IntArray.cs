using System;
using System.Text;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000547 RID: 1351
	internal class IntArray : ICloneable
	{
		// Token: 0x06002E73 RID: 11891 RVA: 0x0011F1D2 File Offset: 0x0011E1D2
		public IntArray(int intLen)
		{
			this.m_ints = new int[intLen];
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x0011F1E6 File Offset: 0x0011E1E6
		private IntArray(int[] ints)
		{
			this.m_ints = ints;
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x0011F1F5 File Offset: 0x0011E1F5
		public IntArray(BigInteger bigInt) : this(bigInt, 0)
		{
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x0011F200 File Offset: 0x0011E200
		public IntArray(BigInteger bigInt, int minIntLen)
		{
			if (bigInt.SignValue == -1)
			{
				throw new ArgumentException("Only positive Integers allowed", "bigint");
			}
			if (bigInt.SignValue == 0)
			{
				int[] ints = new int[1];
				this.m_ints = ints;
				return;
			}
			byte[] array = bigInt.ToByteArrayUnsigned();
			int num = array.Length;
			int i = (num + 3) / 4;
			this.m_ints = new int[Math.Max(i, minIntLen)];
			int num2 = num % 4;
			int j = 0;
			if (0 < num2)
			{
				int num3 = (int)array[j++];
				while (j < num2)
				{
					num3 = (num3 << 8 | (int)array[j++]);
				}
				this.m_ints[--i] = num3;
			}
			while (i > 0)
			{
				int num4 = (int)array[j++];
				for (int k = 1; k < 4; k++)
				{
					num4 = (num4 << 8 | (int)array[j++]);
				}
				this.m_ints[--i] = num4;
			}
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x0011F2E4 File Offset: 0x0011E2E4
		public int GetUsedLength()
		{
			int num = this.m_ints.Length;
			if (num < 1)
			{
				return 0;
			}
			if (this.m_ints[0] != 0)
			{
				while (this.m_ints[--num] == 0)
				{
				}
				return num + 1;
			}
			while (this.m_ints[--num] == 0)
			{
				if (num <= 0)
				{
					return 0;
				}
			}
			return num + 1;
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x0011F334 File Offset: 0x0011E334
		public int BitLength
		{
			get
			{
				int usedLength = this.GetUsedLength();
				if (usedLength == 0)
				{
					return 0;
				}
				int num = usedLength - 1;
				uint num2 = (uint)this.m_ints[num];
				int num3 = (num << 5) + 1;
				if (num2 > 65535U)
				{
					if (num2 > 16777215U)
					{
						num3 += 24;
						num2 >>= 24;
					}
					else
					{
						num3 += 16;
						num2 >>= 16;
					}
				}
				else if (num2 > 255U)
				{
					num3 += 8;
					num2 >>= 8;
				}
				while (num2 > 1U)
				{
					num3++;
					num2 >>= 1;
				}
				return num3;
			}
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x0011F3A8 File Offset: 0x0011E3A8
		private int[] resizedInts(int newLen)
		{
			int[] array = new int[newLen];
			int num = this.m_ints.Length;
			int length = (num < newLen) ? num : newLen;
			Array.Copy(this.m_ints, 0, array, 0, length);
			return array;
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x0011F3E0 File Offset: 0x0011E3E0
		public BigInteger ToBigInteger()
		{
			int usedLength = this.GetUsedLength();
			if (usedLength == 0)
			{
				return BigInteger.Zero;
			}
			int num = this.m_ints[usedLength - 1];
			byte[] array = new byte[4];
			int num2 = 0;
			bool flag = false;
			for (int i = 3; i >= 0; i--)
			{
				byte b = (byte)((uint)num >> 8 * i);
				if (flag || b != 0)
				{
					flag = true;
					array[num2++] = b;
				}
			}
			int num3 = 4 * (usedLength - 1) + num2;
			byte[] array2 = new byte[num3];
			for (int j = 0; j < num2; j++)
			{
				array2[j] = array[j];
			}
			for (int k = usedLength - 2; k >= 0; k--)
			{
				for (int l = 3; l >= 0; l--)
				{
					array2[num2++] = (byte)((uint)this.m_ints[k] >> 8 * l);
				}
			}
			return new BigInteger(1, array2);
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x0011F4B4 File Offset: 0x0011E4B4
		public void ShiftLeft()
		{
			int num = this.GetUsedLength();
			if (num == 0)
			{
				return;
			}
			if (this.m_ints[num - 1] < 0)
			{
				num++;
				if (num > this.m_ints.Length)
				{
					this.m_ints = this.resizedInts(this.m_ints.Length + 1);
				}
			}
			bool flag = false;
			for (int i = 0; i < num; i++)
			{
				bool flag2 = this.m_ints[i] < 0;
				this.m_ints[i] <<= 1;
				if (flag)
				{
					this.m_ints[i] |= 1;
				}
				flag = flag2;
			}
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x0011F550 File Offset: 0x0011E550
		public IntArray ShiftLeft(int n)
		{
			int usedLength = this.GetUsedLength();
			if (usedLength == 0)
			{
				return this;
			}
			if (n == 0)
			{
				return this;
			}
			if (n > 31)
			{
				throw new ArgumentException("shiftLeft() for max 31 bits , " + n + "bit shift is not possible", "n");
			}
			int[] array = new int[usedLength + 1];
			int num = 32 - n;
			array[0] = this.m_ints[0] << n;
			for (int i = 1; i < usedLength; i++)
			{
				array[i] = (this.m_ints[i] << n | (int)((uint)this.m_ints[i - 1] >> num));
			}
			array[usedLength] = (int)((uint)this.m_ints[usedLength - 1] >> num);
			return new IntArray(array);
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x0011F5F8 File Offset: 0x0011E5F8
		public void AddShifted(IntArray other, int shift)
		{
			int usedLength = other.GetUsedLength();
			int num = usedLength + shift;
			if (num > this.m_ints.Length)
			{
				this.m_ints = this.resizedInts(num);
			}
			for (int i = 0; i < usedLength; i++)
			{
				this.m_ints[i + shift] ^= other.m_ints[i];
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06002E7E RID: 11902 RVA: 0x0011F656 File Offset: 0x0011E656
		public int Length
		{
			get
			{
				return this.m_ints.Length;
			}
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x0011F660 File Offset: 0x0011E660
		public bool TestBit(int n)
		{
			int num = n >> 5;
			int num2 = n & 31;
			int num3 = 1 << num2;
			return (this.m_ints[num] & num3) != 0;
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x0011F690 File Offset: 0x0011E690
		public void FlipBit(int n)
		{
			int num = n >> 5;
			int num2 = n & 31;
			int num3 = 1 << num2;
			this.m_ints[num] ^= num3;
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x0011F6C8 File Offset: 0x0011E6C8
		public void SetBit(int n)
		{
			int num = n >> 5;
			int num2 = n & 31;
			int num3 = 1 << num2;
			this.m_ints[num] |= num3;
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x0011F700 File Offset: 0x0011E700
		public IntArray Multiply(IntArray other, int m)
		{
			int num = m + 31 >> 5;
			if (this.m_ints.Length < num)
			{
				this.m_ints = this.resizedInts(num);
			}
			IntArray intArray = new IntArray(other.resizedInts(other.Length + 1));
			IntArray intArray2 = new IntArray(m + m + 31 >> 5);
			int num2 = 1;
			for (int i = 0; i < 32; i++)
			{
				for (int j = 0; j < num; j++)
				{
					if ((this.m_ints[j] & num2) != 0)
					{
						intArray2.AddShifted(intArray, j);
					}
				}
				num2 <<= 1;
				intArray.ShiftLeft();
			}
			return intArray2;
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x0011F794 File Offset: 0x0011E794
		public void Reduce(int m, int[] redPol)
		{
			for (int i = m + m - 2; i >= m; i--)
			{
				if (this.TestBit(i))
				{
					int num = i - m;
					this.FlipBit(num);
					this.FlipBit(i);
					int num2 = redPol.Length;
					while (--num2 >= 0)
					{
						this.FlipBit(redPol[num2] + num);
					}
				}
			}
			this.m_ints = this.resizedInts(m + 31 >> 5);
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x0011F838 File Offset: 0x0011E838
		public IntArray Square(int m)
		{
			int[] array = new int[]
			{
				0,
				1,
				4,
				5,
				16,
				17,
				20,
				21,
				64,
				65,
				68,
				69,
				80,
				81,
				84,
				85
			};
			int num = m + 31 >> 5;
			if (this.m_ints.Length < num)
			{
				this.m_ints = this.resizedInts(num);
			}
			IntArray intArray = new IntArray(num + num);
			for (int i = 0; i < num; i++)
			{
				int num2 = 0;
				for (int j = 0; j < 4; j++)
				{
					num2 = (int)((uint)num2 >> 8);
					int num3 = (int)((uint)this.m_ints[i] >> j * 4 & 15U);
					int num4 = array[num3] << 24;
					num2 |= num4;
				}
				intArray.m_ints[i + i] = num2;
				num2 = 0;
				int num5 = (int)((uint)this.m_ints[i] >> 16);
				for (int k = 0; k < 4; k++)
				{
					num2 = (int)((uint)num2 >> 8);
					int num6 = (int)((uint)num5 >> k * 4 & 15U);
					int num7 = array[num6] << 24;
					num2 |= num7;
				}
				intArray.m_ints[i + i + 1] = num2;
			}
			return intArray;
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x0011F930 File Offset: 0x0011E930
		public override bool Equals(object o)
		{
			if (!(o is IntArray))
			{
				return false;
			}
			IntArray intArray = (IntArray)o;
			int usedLength = this.GetUsedLength();
			if (intArray.GetUsedLength() != usedLength)
			{
				return false;
			}
			for (int i = 0; i < usedLength; i++)
			{
				if (this.m_ints[i] != intArray.m_ints[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x0011F984 File Offset: 0x0011E984
		public override int GetHashCode()
		{
			int num = this.GetUsedLength();
			int num2 = num;
			while (--num >= 0)
			{
				num2 *= 17;
				num2 ^= this.m_ints[num];
			}
			return num2;
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x0011F9B5 File Offset: 0x0011E9B5
		public object Clone()
		{
			return new IntArray((int[])this.m_ints.Clone());
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x0011F9CC File Offset: 0x0011E9CC
		public override string ToString()
		{
			int usedLength = this.GetUsedLength();
			if (usedLength == 0)
			{
				return "0";
			}
			StringBuilder stringBuilder = new StringBuilder(Convert.ToString(this.m_ints[usedLength - 1], 2));
			for (int i = usedLength - 2; i >= 0; i--)
			{
				string text = Convert.ToString(this.m_ints[i], 2);
				for (int j = text.Length; j < 8; j++)
				{
					text = "0" + text;
				}
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400200B RID: 8203
		private int[] m_ints;
	}
}
