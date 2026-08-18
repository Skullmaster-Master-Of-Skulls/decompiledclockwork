using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;

namespace System.Security.Cryptography
{
	// Token: 0x0200000D RID: 13
	internal sealed class BigInt
	{
		// Token: 0x0600003E RID: 62 RVA: 0x0000318C File Offset: 0x0000138C
		internal BigInt()
		{
			this.m_elements = new byte[128];
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000031A4 File Offset: 0x000013A4
		internal BigInt(byte b)
		{
			this.m_elements = new byte[128];
			this.SetDigit(0, b);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000031C4 File Offset: 0x000013C4
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000031CC File Offset: 0x000013CC
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

		// Token: 0x06000042 RID: 66 RVA: 0x000031F3 File Offset: 0x000013F3
		internal byte GetDigit(int index)
		{
			if (index < 0 || index >= this.m_size)
			{
				return 0;
			}
			return this.m_elements[index];
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000320C File Offset: 0x0000140C
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

		// Token: 0x06000044 RID: 68 RVA: 0x0000325F File Offset: 0x0000145F
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

		// Token: 0x06000045 RID: 69 RVA: 0x00003294 File Offset: 0x00001494
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

		// Token: 0x06000046 RID: 70 RVA: 0x000032FC File Offset: 0x000014FC
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

		// Token: 0x06000047 RID: 71 RVA: 0x00003364 File Offset: 0x00001564
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

		// Token: 0x06000048 RID: 72 RVA: 0x000033B6 File Offset: 0x000015B6
		public static bool operator !=(BigInt value1, BigInt value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000033C2 File Offset: 0x000015C2
		public override bool Equals(object obj)
		{
			return obj is BigInt && this == (BigInt)obj;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000033DC File Offset: 0x000015DC
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.m_size; i++)
			{
				num += (int)this.GetDigit(i);
			}
			return num;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003408 File Offset: 0x00001608
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

		// Token: 0x0600004C RID: 76 RVA: 0x00003478 File Offset: 0x00001678
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

		// Token: 0x0600004D RID: 77 RVA: 0x00003508 File Offset: 0x00001708
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

		// Token: 0x0600004E RID: 78 RVA: 0x00003588 File Offset: 0x00001788
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

		// Token: 0x0600004F RID: 79 RVA: 0x0000361C File Offset: 0x0000181C
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

		// Token: 0x06000050 RID: 80 RVA: 0x000036B4 File Offset: 0x000018B4
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

		// Token: 0x06000051 RID: 81 RVA: 0x00003700 File Offset: 0x00001900
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

		// Token: 0x06000052 RID: 82 RVA: 0x0000388A File Offset: 0x00001A8A
		internal void CopyFrom(BigInt a)
		{
			Array.Copy(a.m_elements, this.m_elements, 128);
			this.m_size = a.m_size;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000038B0 File Offset: 0x00001AB0
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

		// Token: 0x06000054 RID: 84 RVA: 0x000038DC File Offset: 0x00001ADC
		internal byte[] ToByteArray()
		{
			byte[] array = new byte[this.Size];
			Array.Copy(this.m_elements, array, this.Size);
			return array;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003908 File Offset: 0x00001B08
		internal void Clear()
		{
			this.m_size = 0;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003914 File Offset: 0x00001B14
		internal void FromHexadecimal(string hexNum)
		{
			byte[] array = X509Utils.DecodeHexString(hexNum);
			Array.Reverse(array);
			int hexArraySize = Utils.GetHexArraySize(array);
			Array.Copy(array, this.m_elements, hexArraySize);
			this.Size = hexArraySize;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000394C File Offset: 0x00001B4C
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

		// Token: 0x06000058 RID: 88 RVA: 0x000039B4 File Offset: 0x00001BB4
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

		// Token: 0x0400007F RID: 127
		private byte[] m_elements;

		// Token: 0x04000080 RID: 128
		private const int m_maxbytes = 128;

		// Token: 0x04000081 RID: 129
		private const int m_base = 256;

		// Token: 0x04000082 RID: 130
		private int m_size;

		// Token: 0x04000083 RID: 131
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
