using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	// Token: 0x02000450 RID: 1104
	internal sealed class BigInt
	{
		// Token: 0x060028E5 RID: 10469 RVA: 0x000BB17F File Offset: 0x000B937F
		internal BigInt()
		{
			this.m_elements = new byte[128];
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000BB197 File Offset: 0x000B9397
		internal BigInt(byte b)
		{
			this.m_elements = new byte[128];
			this.SetDigit(0, b);
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x000BB1B7 File Offset: 0x000B93B7
		// (set) Token: 0x060028E8 RID: 10472 RVA: 0x000BB1BF File Offset: 0x000B93BF
		internal int Size
		{
			get
			{
				return this.m_size;
			}
			set
			{
				if (value > 128)
				{
					this.m_size = 128;
				}
				if (value < 0)
				{
					this.m_size = 0;
				}
				this.m_size = value;
			}
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000BB1E6 File Offset: 0x000B93E6
		internal byte GetDigit(int index)
		{
			if (index < 0 || index >= this.m_size)
			{
				return 0;
			}
			return this.m_elements[index];
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000BB200 File Offset: 0x000B9400
		internal void SetDigit(int index, byte digit)
		{
			if (index >= 0 && index < 128)
			{
				this.m_elements[index] = digit;
				if (index >= this.m_size && digit != 0)
				{
					this.m_size = index + 1;
				}
				if (index == this.m_size - 1 && digit == 0)
				{
					this.m_size--;
				}
			}
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x000BB253 File Offset: 0x000B9453
		internal void SetDigit(int index, byte digit, ref int size)
		{
			if (index >= 0 && index < 128)
			{
				this.m_elements[index] = digit;
				if (index >= size && digit != 0)
				{
					size = index + 1;
				}
				if (index == size - 1 && digit == 0)
				{
					size--;
				}
			}
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x000BB288 File Offset: 0x000B9488
		public static bool operator <(BigInt value1, BigInt value2)
		{
			if (value1 == null)
			{
				return true;
			}
			if (value2 == null)
			{
				return false;
			}
			int size = value1.Size;
			int size2 = value2.Size;
			if (size != size2)
			{
				return size < size2;
			}
			while (size-- > 0)
			{
				if (value1.m_elements[size] != value2.m_elements[size])
				{
					return value1.m_elements[size] < value2.m_elements[size];
				}
			}
			return false;
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000BB2F0 File Offset: 0x000B94F0
		public static bool operator >(BigInt value1, BigInt value2)
		{
			if (value1 == null)
			{
				return false;
			}
			if (value2 == null)
			{
				return true;
			}
			int size = value1.Size;
			int size2 = value2.Size;
			if (size != size2)
			{
				return size > size2;
			}
			while (size-- > 0)
			{
				if (value1.m_elements[size] != value2.m_elements[size])
				{
					return value1.m_elements[size] > value2.m_elements[size];
				}
			}
			return false;
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000BB358 File Offset: 0x000B9558
		public static bool operator ==(BigInt value1, BigInt value2)
		{
			if (value1 == null)
			{
				return value2 == null;
			}
			if (value2 == null)
			{
				return value1 == null;
			}
			int size = value1.Size;
			int size2 = value2.Size;
			if (size != size2)
			{
				return false;
			}
			for (int i = 0; i < size; i++)
			{
				if (value1.m_elements[i] != value2.m_elements[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000BB3AA File Offset: 0x000B95AA
		public static bool operator !=(BigInt value1, BigInt value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000BB3B6 File Offset: 0x000B95B6
		public override bool Equals(object obj)
		{
			return obj is BigInt && this == (BigInt)obj;
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000BB3D0 File Offset: 0x000B95D0
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.m_size; i++)
			{
				num += (int)this.GetDigit(i);
			}
			return num;
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000BB3FC File Offset: 0x000B95FC
		internal static void Add(BigInt a, byte b, ref BigInt c)
		{
			byte b2 = b;
			int size = a.Size;
			int size2 = 0;
			for (int i = 0; i < size; i++)
			{
				int num = (int)(a.GetDigit(i) + b2);
				c.SetDigit(i, (byte)(num & 255), ref size2);
				b2 = (byte)(num >> 8 & 255);
			}
			if (b2 != 0)
			{
				c.SetDigit(a.Size, b2, ref size2);
			}
			c.Size = size2;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000BB46C File Offset: 0x000B966C
		internal static void Negate(ref BigInt a)
		{
			int size = 0;
			for (int i = 0; i < 128; i++)
			{
				a.SetDigit(i, ~a.GetDigit(i) & byte.MaxValue, ref size);
			}
			for (int j = 0; j < 128; j++)
			{
				a.SetDigit(j, a.GetDigit(j) + 1, ref size);
				if ((a.GetDigit(j) & 255) != 0)
				{
					break;
				}
				a.SetDigit(j, a.GetDigit(j) & byte.MaxValue, ref size);
			}
			a.Size = size;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000BB4FC File Offset: 0x000B96FC
		internal static void Subtract(BigInt a, BigInt b, ref BigInt c)
		{
			byte b2 = 0;
			if (a < b)
			{
				BigInt.Subtract(b, a, ref c);
				BigInt.Negate(ref c);
				return;
			}
			int size = a.Size;
			int size2 = 0;
			for (int i = 0; i < size; i++)
			{
				int num = (int)(a.GetDigit(i) - b.GetDigit(i) - b2);
				b2 = 0;
				if (num < 0)
				{
					num += 256;
					b2 = 1;
				}
				c.SetDigit(i, (byte)(num & 255), ref size2);
			}
			c.Size = size2;
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000BB57C File Offset: 0x000B977C
		private void Multiply(int b)
		{
			if (b == 0)
			{
				this.Clear();
				return;
			}
			int num = 0;
			int size = this.Size;
			int size2 = 0;
			for (int i = 0; i < size; i++)
			{
				int num2 = b * (int)this.GetDigit(i) + num;
				num = num2 / 256;
				this.SetDigit(i, (byte)(num2 % 256), ref size2);
			}
			if (num != 0)
			{
				byte[] bytes = BitConverter.GetBytes(num);
				for (int j = 0; j < bytes.Length; j++)
				{
					this.SetDigit(size + j, bytes[j], ref size2);
				}
			}
			this.Size = size2;
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000BB610 File Offset: 0x000B9810
		private static void Multiply(BigInt a, int b, ref BigInt c)
		{
			if (b == 0)
			{
				c.Clear();
				return;
			}
			int num = 0;
			int size = a.Size;
			int size2 = 0;
			for (int i = 0; i < size; i++)
			{
				int num2 = b * (int)a.GetDigit(i) + num;
				num = num2 / 256;
				c.SetDigit(i, (byte)(num2 % 256), ref size2);
			}
			if (num != 0)
			{
				byte[] bytes = BitConverter.GetBytes(num);
				for (int j = 0; j < bytes.Length; j++)
				{
					c.SetDigit(size + j, bytes[j], ref size2);
				}
			}
			c.Size = size2;
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000BB6A8 File Offset: 0x000B98A8
		private void Divide(int b)
		{
			int num = 0;
			int size = this.Size;
			int size2 = 0;
			while (size-- > 0)
			{
				int num2 = 256 * num + (int)this.GetDigit(size);
				num = num2 % b;
				this.SetDigit(size, (byte)(num2 / b), ref size2);
			}
			this.Size = size2;
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x000BB6F4 File Offset: 0x000B98F4
		internal static void Divide(BigInt numerator, BigInt denominator, ref BigInt quotient, ref BigInt remainder)
		{
			if (numerator < denominator)
			{
				quotient.Clear();
				remainder.CopyFrom(numerator);
				return;
			}
			if (numerator == denominator)
			{
				quotient.Clear();
				quotient.SetDigit(0, 1);
				remainder.Clear();
				return;
			}
			BigInt bigInt = new BigInt();
			bigInt.CopyFrom(numerator);
			BigInt bigInt2 = new BigInt();
			bigInt2.CopyFrom(denominator);
			uint num = 0U;
			while (bigInt2.Size < bigInt.Size)
			{
				bigInt2.Multiply(256);
				num += 1U;
			}
			if (bigInt2 > bigInt)
			{
				bigInt2.Divide(256);
				num -= 1U;
			}
			BigInt bigInt3 = new BigInt();
			quotient.Clear();
			int num2 = 0;
			while ((long)num2 <= (long)((ulong)num))
			{
				int num3 = (bigInt.Size == bigInt2.Size) ? ((int)bigInt.GetDigit(bigInt.Size - 1)) : (256 * (int)bigInt.GetDigit(bigInt.Size - 1) + (int)bigInt.GetDigit(bigInt.Size - 2));
				int digit = (int)bigInt2.GetDigit(bigInt2.Size - 1);
				int num4 = num3 / digit;
				if (num4 >= 256)
				{
					num4 = 255;
				}
				BigInt.Multiply(bigInt2, num4, ref bigInt3);
				while (bigInt3 > bigInt)
				{
					num4--;
					BigInt.Multiply(bigInt2, num4, ref bigInt3);
				}
				quotient.Multiply(256);
				BigInt.Add(quotient, (byte)num4, ref quotient);
				BigInt.Subtract(bigInt, bigInt3, ref bigInt);
				bigInt2.Divide(256);
				num2++;
			}
			remainder.CopyFrom(bigInt);
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000BB87E File Offset: 0x000B9A7E
		internal void CopyFrom(BigInt a)
		{
			Array.Copy(a.m_elements, this.m_elements, 128);
			this.m_size = a.m_size;
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x000BB8A4 File Offset: 0x000B9AA4
		internal bool IsZero()
		{
			for (int i = 0; i < this.m_size; i++)
			{
				if (this.m_elements[i] != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000BB8D0 File Offset: 0x000B9AD0
		internal byte[] ToByteArray()
		{
			byte[] array = new byte[this.Size];
			Array.Copy(this.m_elements, array, this.Size);
			return array;
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000BB8FC File Offset: 0x000B9AFC
		internal void Clear()
		{
			this.m_size = 0;
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000BB908 File Offset: 0x000B9B08
		internal void FromHexadecimal(string hexNum)
		{
			byte[] array = X509Utils.DecodeHexString(hexNum);
			Array.Reverse(array);
			int hexArraySize = X509Utils.GetHexArraySize(array);
			Array.Copy(array, this.m_elements, hexArraySize);
			this.Size = hexArraySize;
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x000BB940 File Offset: 0x000B9B40
		internal void FromDecimal(string decNum)
		{
			BigInt a = new BigInt();
			BigInt a2 = new BigInt();
			int length = decNum.Length;
			for (int i = 0; i < length; i++)
			{
				if (decNum[i] <= '9' && decNum[i] >= '0')
				{
					BigInt.Multiply(a, 10, ref a2);
					BigInt.Add(a2, (byte)(decNum[i] - '0'), ref a);
				}
			}
			this.CopyFrom(a);
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x000BB9A8 File Offset: 0x000B9BA8
		internal string ToDecimal()
		{
			if (this.IsZero())
			{
				return "0";
			}
			BigInt denominator = new BigInt(10);
			BigInt bigInt = new BigInt();
			BigInt bigInt2 = new BigInt();
			BigInt bigInt3 = new BigInt();
			bigInt.CopyFrom(this);
			char[] array = new char[(int)Math.Ceiling((double)(this.m_size * 2) * 1.21)];
			int length = 0;
			do
			{
				BigInt.Divide(bigInt, denominator, ref bigInt2, ref bigInt3);
				array[length++] = BigInt.decValues[(int)(bigInt3.IsZero() ? 0 : bigInt3.m_elements[0])];
				bigInt.CopyFrom(bigInt2);
			}
			while (!bigInt2.IsZero());
			Array.Reverse(array, 0, length);
			return new string(array, 0, length);
		}

		// Token: 0x04002288 RID: 8840
		private byte[] m_elements;

		// Token: 0x04002289 RID: 8841
		private const int m_maxbytes = 128;

		// Token: 0x0400228A RID: 8842
		private const int m_base = 256;

		// Token: 0x0400228B RID: 8843
		private int m_size;

		// Token: 0x0400228C RID: 8844
		private static readonly char[] decValues = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9'
		};
	}
}
