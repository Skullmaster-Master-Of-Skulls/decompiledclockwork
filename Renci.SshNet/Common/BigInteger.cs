using System;
using System.Collections.Generic;
using System.Globalization;
using Renci.SshNet.Abstractions;

namespace Renci.SshNet.Common
{
	// Token: 0x020000EB RID: 235
	public struct BigInteger : IComparable, IFormattable, IComparable<BigInteger>, IEquatable<BigInteger>
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x000206A0 File Offset: 0x0001E8A0
		public int BitLength
		{
			get
			{
				if (this._sign == 0)
				{
					return 0;
				}
				int num = this._data.Length - 1;
				while (this._data[num] == 0U)
				{
					num--;
				}
				int num2 = BigInteger.BitScanBackward(this._data[num]) + 1;
				return num * 4 * 8 + num2 + ((this._sign > 0) ? 0 : 1);
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000206F8 File Offset: 0x0001E8F8
		public static BigInteger ModInverse(BigInteger bi, BigInteger modulus)
		{
			BigInteger bigInteger = modulus;
			BigInteger bigInteger2 = bi % modulus;
			BigInteger bigInteger3 = 0;
			BigInteger bigInteger4 = 1;
			while (!bigInteger2.IsZero)
			{
				if (bigInteger2.IsOne)
				{
					return bigInteger4;
				}
				bigInteger3 += bigInteger / bigInteger2 * bigInteger4;
				bigInteger %= bigInteger2;
				if (bigInteger.IsZero)
				{
					break;
				}
				if (bigInteger.IsOne)
				{
					return modulus - bigInteger3;
				}
				bigInteger4 += bigInteger2 / bigInteger * bigInteger3;
				bigInteger2 %= bigInteger;
			}
			return 0;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002078C File Offset: 0x0001E98C
		public static BigInteger PositiveMod(BigInteger dividend, BigInteger divisor)
		{
			BigInteger bigInteger = dividend % divisor;
			if (bigInteger < 0L)
			{
				bigInteger += divisor;
			}
			return bigInteger;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000207B4 File Offset: 0x0001E9B4
		public static BigInteger Random(int bitLength)
		{
			byte[] array = new byte[bitLength / 8 + ((bitLength % 8 > 0) ? 1 : 0)];
			CryptoAbstraction.GenerateRandom(array);
			array[array.Length - 1] = (array[array.Length - 1] & 127);
			return new BigInteger(array);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x000207F3 File Offset: 0x0001E9F3
		private BigInteger(short sign, uint[] data)
		{
			this._sign = sign;
			this._data = data;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00020804 File Offset: 0x0001EA04
		public BigInteger(int value)
		{
			if (value == 0)
			{
				this._sign = 0;
				this._data = null;
				return;
			}
			if (value > 0)
			{
				this._sign = 1;
				this._data = new uint[]
				{
					(uint)value
				};
				return;
			}
			this._sign = -1;
			this._data = new uint[]
			{
				(uint)(-(uint)value)
			};
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00020857 File Offset: 0x0001EA57
		[CLSCompliant(false)]
		public BigInteger(uint value)
		{
			if (value == 0U)
			{
				this._sign = 0;
				this._data = null;
				return;
			}
			this._sign = 1;
			this._data = new uint[]
			{
				value
			};
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00020884 File Offset: 0x0001EA84
		public BigInteger(long value)
		{
			if (value == 0L)
			{
				this._sign = 0;
				this._data = null;
				return;
			}
			if (value > 0L)
			{
				this._sign = 1;
				uint num = (uint)value;
				uint num2 = (uint)(value >> 32);
				this._data = new uint[(num2 != 0U) ? 2 : 1];
				this._data[0] = num;
				if (num2 != 0U)
				{
					this._data[1] = num2;
					return;
				}
			}
			else
			{
				this._sign = -1;
				value = -value;
				uint num3 = (uint)value;
				uint num4 = (uint)((ulong)value >> 32);
				this._data = new uint[(num4 != 0U) ? 2 : 1];
				this._data[0] = num3;
				if (num4 != 0U)
				{
					this._data[1] = num4;
				}
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002091C File Offset: 0x0001EB1C
		[CLSCompliant(false)]
		public BigInteger(ulong value)
		{
			if (value == 0UL)
			{
				this._sign = 0;
				this._data = null;
				return;
			}
			this._sign = 1;
			uint num = (uint)value;
			uint num2 = (uint)(value >> 32);
			this._data = new uint[(num2 != 0U) ? 2 : 1];
			this._data[0] = num;
			if (num2 != 0U)
			{
				this._data[1] = num2;
			}
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00020972 File Offset: 0x0001EB72
		private static bool Negative(byte[] v)
		{
			return (v[7] & 128) > 0;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00020980 File Offset: 0x0001EB80
		private static ushort Exponent(byte[] v)
		{
			return (ushort)((int)((ushort)(v[7] & 127)) << 4 | (ushort)(v[6] & 240) >> 4);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002099C File Offset: 0x0001EB9C
		private static ulong Mantissa(byte[] v)
		{
			ulong num = (ulong)((int)v[0] | (int)v[1] << 8 | (int)v[2] << 16 | (int)v[3] << 24);
			uint num2 = (uint)((int)v[4] | (int)v[5] << 8 | (int)(v[6] & 15) << 16);
			return num | (ulong)num2 << 32;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000209DC File Offset: 0x0001EBDC
		public BigInteger(double value)
		{
			if (double.IsNaN(value) || double.IsInfinity(value))
			{
				throw new OverflowException();
			}
			byte[] bytes = BitConverter.GetBytes(value);
			ulong num = BigInteger.Mantissa(bytes);
			if (num != 0UL)
			{
				int num2 = (int)BigInteger.Exponent(bytes);
				num |= 4503599627370496UL;
				BigInteger bigInteger = num;
				bigInteger = ((num2 > 1075) ? (bigInteger << num2 - 1075) : (bigInteger >> 1075 - num2));
				this._sign = (BigInteger.Negative(bytes) ? -1 : 1);
				this._data = bigInteger._data;
				return;
			}
			int num3 = (int)BigInteger.Exponent(bytes);
			if (num3 == 0)
			{
				this._sign = 0;
				this._data = null;
				return;
			}
			BigInteger bigInteger2 = BigInteger.Negative(bytes) ? BigInteger.MinusOne : BigInteger.One;
			bigInteger2 <<= num3 - 1023;
			this._sign = bigInteger2._sign;
			this._data = bigInteger2._data;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00020ACC File Offset: 0x0001ECCC
		public BigInteger(float value)
		{
			this = new BigInteger((double)value);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00020AD8 File Offset: 0x0001ECD8
		public BigInteger(decimal value)
		{
			int[] bits = decimal.GetBits(decimal.Truncate(value));
			int num = 3;
			while (num > 0 && bits[num - 1] == 0)
			{
				num--;
			}
			if (num == 0)
			{
				this._sign = 0;
				this._data = null;
				return;
			}
			this._sign = (((bits[3] & int.MinValue) != 0) ? -1 : 1);
			this._data = new uint[num];
			this._data[0] = (uint)bits[0];
			if (num > 1)
			{
				this._data[1] = (uint)bits[1];
			}
			if (num > 2)
			{
				this._data[2] = (uint)bits[2];
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00020B64 File Offset: 0x0001ED64
		[CLSCompliant(false)]
		public BigInteger(byte[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int num = value.Length;
			if (num == 0 || (num == 1 && value[0] == 0))
			{
				this._sign = 0;
				this._data = null;
				return;
			}
			if ((value[num - 1] & 128) != 0)
			{
				this._sign = -1;
			}
			else
			{
				this._sign = 1;
			}
			if (this._sign == 1)
			{
				while (value[num - 1] == 0)
				{
					if (--num == 0)
					{
						this._sign = 0;
						this._data = null;
						return;
					}
				}
				int num3;
				int num2 = num3 = num / 4;
				if ((num & 3) != 0)
				{
					num2++;
				}
				this._data = new uint[num2];
				int num4 = 0;
				for (int i = 0; i < num3; i++)
				{
					this._data[i] = (uint)((int)value[num4++] | (int)value[num4++] << 8 | (int)value[num4++] << 16 | (int)value[num4++] << 24);
				}
				num2 = (num & 3);
				if (num2 > 0)
				{
					int num5 = this._data.Length - 1;
					for (int j = 0; j < num2; j++)
					{
						this._data[num5] |= (uint)((uint)value[num4++] << j * 8);
					}
					return;
				}
			}
			else
			{
				int num7;
				int num6 = num7 = num / 4;
				if ((num & 3) != 0)
				{
					num6++;
				}
				this._data = new uint[num6];
				uint num8 = 1U;
				int num9 = 0;
				for (int k = 0; k < num7; k++)
				{
					uint num10 = (uint)((int)value[num9++] | (int)value[num9++] << 8 | (int)value[num9++] << 16 | (int)value[num9++] << 24);
					ulong num11 = (ulong)num10 - (ulong)num8;
					num10 = (uint)num11;
					num8 = ((uint)(num11 >> 32) & 1U);
					this._data[k] = ~num10;
				}
				num6 = (num & 3);
				if (num6 > 0)
				{
					uint num10 = 0U;
					uint num12 = 0U;
					for (int l = 0; l < num6; l++)
					{
						num10 |= (uint)((uint)value[num9++] << l * 8);
						num12 = (num12 << 8 | 255U);
					}
					ulong num13 = (ulong)(num10 - num8);
					num10 = (uint)num13;
					num8 = ((uint)(num13 >> 32) & 1U);
					if ((~(num10 != 0U) & num12) == 0U)
					{
						Array.Resize<uint>(ref this._data, this._data.Length - 1);
					}
					else
					{
						this._data[this._data.Length - 1] = (~num10 & num12);
					}
				}
				if (num8 != 0U)
				{
					throw new Exception("non zero final carry");
				}
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00020DAF File Offset: 0x0001EFAF
		public bool IsEven
		{
			get
			{
				return this._sign == 0 || (this._data[0] & 1U) == 0U;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00020DC8 File Offset: 0x0001EFC8
		public bool IsOne
		{
			get
			{
				return this._sign == 1 && this._data.Length == 1 && this._data[0] == 1U;
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00020DEC File Offset: 0x0001EFEC
		private static int PopulationCount(uint x)
		{
			x -= (x >> 1 & 1431655765U);
			x = (x & 858993459U) + (x >> 2 & 858993459U);
			x = (x + (x >> 4) & 252645135U);
			x += x >> 8;
			x += x >> 16;
			return (int)(x & 63U);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00020E3C File Offset: 0x0001F03C
		private static int PopulationCount(ulong x)
		{
			x -= (x >> 1 & 6148914691236517205UL);
			x = (x & 3689348814741910323UL) + (x >> 2 & 3689348814741910323UL);
			x = (x + (x >> 4) & 1085102592571150095UL);
			return (int)(x * 72340172838076673UL >> 56);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00020E95 File Offset: 0x0001F095
		private static int LeadingZeroCount(uint value)
		{
			value |= value >> 1;
			value |= value >> 2;
			value |= value >> 4;
			value |= value >> 8;
			value |= value >> 16;
			return 32 - BigInteger.PopulationCount(value);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00020EC4 File Offset: 0x0001F0C4
		private static int LeadingZeroCount(ulong value)
		{
			value |= value >> 1;
			value |= value >> 2;
			value |= value >> 4;
			value |= value >> 8;
			value |= value >> 16;
			value |= value >> 32;
			return 64 - BigInteger.PopulationCount(value);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00020EFC File Offset: 0x0001F0FC
		private static double BuildDouble(int sign, ulong mantissa, int exponent)
		{
			if (sign == 0 || mantissa == 0UL)
			{
				return 0.0;
			}
			exponent += 1075;
			int num = BigInteger.LeadingZeroCount(mantissa) - 11;
			if (exponent - num > 2046)
			{
				if (sign <= 0)
				{
					return double.NegativeInfinity;
				}
				return double.PositiveInfinity;
			}
			else
			{
				if (num < 0)
				{
					mantissa >>= -num;
					exponent += -num;
				}
				else if (num >= exponent)
				{
					mantissa <<= exponent - 1;
					exponent = 0;
				}
				else
				{
					mantissa <<= num;
					exponent -= num;
				}
				mantissa &= 4503599627370495UL;
				if (((long)exponent & 2047L) == (long)exponent)
				{
					ulong num2 = mantissa | (ulong)((ulong)((long)exponent) << 52);
					if (sign < 0)
					{
						num2 |= 9223372036854775808UL;
					}
					return BitConverter.Int64BitsToDouble((long)num2);
				}
				if (sign <= 0)
				{
					return double.NegativeInfinity;
				}
				return double.PositiveInfinity;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00020FD4 File Offset: 0x0001F1D4
		public bool IsPowerOfTwo
		{
			get
			{
				bool flag = false;
				if (this._sign != 1)
				{
					return false;
				}
				uint[] data = this._data;
				for (int i = 0; i < data.Length; i++)
				{
					int num = BigInteger.PopulationCount(data[i]);
					if (num > 0)
					{
						if (num > 1 || flag)
						{
							return false;
						}
						flag = true;
					}
				}
				return flag;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0002101D File Offset: 0x0001F21D
		public bool IsZero
		{
			get
			{
				return this._sign == 0;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00021028 File Offset: 0x0001F228
		public int Sign
		{
			get
			{
				return (int)this._sign;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x00021030 File Offset: 0x0001F230
		public static BigInteger MinusOne
		{
			get
			{
				return BigInteger.MinusOneSingleton;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00021037 File Offset: 0x0001F237
		public static BigInteger One
		{
			get
			{
				return BigInteger.OneSingleton;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0002103E File Offset: 0x0001F23E
		public static BigInteger Zero
		{
			get
			{
				return BigInteger.ZeroSingleton;
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00021048 File Offset: 0x0001F248
		public static explicit operator int(BigInteger value)
		{
			if (value._data == null)
			{
				return 0;
			}
			if (value._data.Length > 1)
			{
				throw new OverflowException();
			}
			uint num = value._data[0];
			if (value._sign == 1)
			{
				if (num > 2147483647U)
				{
					throw new OverflowException();
				}
				return (int)num;
			}
			else
			{
				if (value._sign != -1)
				{
					return 0;
				}
				if (num > 2147483648U)
				{
					throw new OverflowException();
				}
				return (int)(-(int)num);
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000210AD File Offset: 0x0001F2AD
		[CLSCompliant(false)]
		public static explicit operator uint(BigInteger value)
		{
			if (value._data == null)
			{
				return 0U;
			}
			if (value._data.Length > 1 || value._sign == -1)
			{
				throw new OverflowException();
			}
			return value._data[0];
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x000210DC File Offset: 0x0001F2DC
		public static explicit operator short(BigInteger value)
		{
			int num = (int)value;
			if (num < -32768 || num > 32767)
			{
				throw new OverflowException();
			}
			return (short)num;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00021108 File Offset: 0x0001F308
		[CLSCompliant(false)]
		public static explicit operator ushort(BigInteger value)
		{
			uint num = (uint)value;
			if (num > 65535U)
			{
				throw new OverflowException();
			}
			return (ushort)num;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002111F File Offset: 0x0001F31F
		public static explicit operator byte(BigInteger value)
		{
			uint num = (uint)value;
			if (num > 255U)
			{
				throw new OverflowException();
			}
			return (byte)num;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00021138 File Offset: 0x0001F338
		[CLSCompliant(false)]
		public static explicit operator sbyte(BigInteger value)
		{
			int num = (int)value;
			if (num < -128 || num > 127)
			{
				throw new OverflowException();
			}
			return (sbyte)num;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00021160 File Offset: 0x0001F360
		public static explicit operator long(BigInteger value)
		{
			if (value._data == null)
			{
				return 0L;
			}
			if (value._data.Length > 2)
			{
				throw new OverflowException();
			}
			uint num = value._data[0];
			if (value._data.Length == 1)
			{
				if (value._sign == 1)
				{
					return (long)((ulong)num);
				}
				return (long)(-(long)((ulong)num));
			}
			else
			{
				uint num2 = value._data[1];
				if (value._sign == 1)
				{
					if (num2 >= 2147483648U)
					{
						throw new OverflowException();
					}
					return (long)((ulong)num2 << 32 | (ulong)num);
				}
				else
				{
					ulong num3 = -((ulong)num2 << 32 | (ulong)num);
					if (num3 > 0UL)
					{
						throw new OverflowException();
					}
					return (long)num3;
				}
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000211EC File Offset: 0x0001F3EC
		[CLSCompliant(false)]
		public static explicit operator ulong(BigInteger value)
		{
			if (value._data == null)
			{
				return 0UL;
			}
			if (value._data.Length > 2 || value._sign == -1)
			{
				throw new OverflowException();
			}
			uint num = value._data[0];
			if (value._data.Length == 1)
			{
				return (ulong)num;
			}
			return (ulong)value._data[1] << 32 | (ulong)num;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00021244 File Offset: 0x0001F444
		public static explicit operator double(BigInteger value)
		{
			if (value._data == null)
			{
				return 0.0;
			}
			int num = value._data.Length;
			if (num == 1)
			{
				return BigInteger.BuildDouble((int)value._sign, (ulong)value._data[0], 0);
			}
			if (num != 2)
			{
				int num2 = value._data.Length - 1;
				uint num3 = value._data[num2];
				ulong num4 = (ulong)num3 << 32 | (ulong)value._data[num2 - 1];
				int num5 = BigInteger.LeadingZeroCount(num3) - 11;
				if (num5 > 0)
				{
					num4 = (num4 << num5 | (ulong)(value._data[num2 - 2] >> 32 - num5));
				}
				else
				{
					num4 >>= -num5;
				}
				return BigInteger.BuildDouble((int)value._sign, num4, (value._data.Length - 2) * 32 - num5);
			}
			return BigInteger.BuildDouble((int)value._sign, (ulong)value._data[1] << 32 | (ulong)value._data[0], 0);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00021321 File Offset: 0x0001F521
		public static explicit operator float(BigInteger value)
		{
			return (float)((double)value);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002132C File Offset: 0x0001F52C
		public static explicit operator decimal(BigInteger value)
		{
			if (value._data == null)
			{
				return 0m;
			}
			uint[] data = value._data;
			if (data.Length > 3)
			{
				throw new OverflowException();
			}
			int lo = 0;
			int mid = 0;
			int hi = 0;
			if (data.Length > 2)
			{
				hi = (int)data[2];
			}
			if (data.Length > 1)
			{
				mid = (int)data[1];
			}
			if (data.Length != 0)
			{
				lo = (int)data[0];
			}
			return new decimal(lo, mid, hi, value._sign < 0, 0);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002138E File Offset: 0x0001F58E
		public static implicit operator BigInteger(int value)
		{
			return new BigInteger(value);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00021396 File Offset: 0x0001F596
		[CLSCompliant(false)]
		public static implicit operator BigInteger(uint value)
		{
			return new BigInteger(value);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002138E File Offset: 0x0001F58E
		public static implicit operator BigInteger(short value)
		{
			return new BigInteger((int)value);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002138E File Offset: 0x0001F58E
		[CLSCompliant(false)]
		public static implicit operator BigInteger(ushort value)
		{
			return new BigInteger((int)value);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0002138E File Offset: 0x0001F58E
		public static implicit operator BigInteger(byte value)
		{
			return new BigInteger((int)value);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002138E File Offset: 0x0001F58E
		[CLSCompliant(false)]
		public static implicit operator BigInteger(sbyte value)
		{
			return new BigInteger((int)value);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002139E File Offset: 0x0001F59E
		public static implicit operator BigInteger(long value)
		{
			return new BigInteger(value);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x000213A6 File Offset: 0x0001F5A6
		[CLSCompliant(false)]
		public static implicit operator BigInteger(ulong value)
		{
			return new BigInteger(value);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000213AE File Offset: 0x0001F5AE
		public static explicit operator BigInteger(double value)
		{
			return new BigInteger(value);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x000213B6 File Offset: 0x0001F5B6
		public static explicit operator BigInteger(float value)
		{
			return new BigInteger(value);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000213BE File Offset: 0x0001F5BE
		public static explicit operator BigInteger(decimal value)
		{
			return new BigInteger(value);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000213C8 File Offset: 0x0001F5C8
		public static BigInteger operator +(BigInteger left, BigInteger right)
		{
			if (left._sign == 0)
			{
				return right;
			}
			if (right._sign == 0)
			{
				return left;
			}
			if (left._sign == right._sign)
			{
				return new BigInteger(left._sign, BigInteger.CoreAdd(left._data, right._data));
			}
			int num = BigInteger.CoreCompare(left._data, right._data);
			if (num == 0)
			{
				return BigInteger.Zero;
			}
			if (num > 0)
			{
				return new BigInteger(left._sign, BigInteger.CoreSub(left._data, right._data));
			}
			return new BigInteger(right._sign, BigInteger.CoreSub(right._data, left._data));
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002146C File Offset: 0x0001F66C
		public static BigInteger operator -(BigInteger left, BigInteger right)
		{
			if (right._sign == 0)
			{
				return left;
			}
			if (left._sign == 0)
			{
				return new BigInteger(-right._sign, right._data);
			}
			if (left._sign != right._sign)
			{
				return new BigInteger(left._sign, BigInteger.CoreAdd(left._data, right._data));
			}
			int num = BigInteger.CoreCompare(left._data, right._data);
			if (num == 0)
			{
				return BigInteger.Zero;
			}
			if (num > 0)
			{
				return new BigInteger(left._sign, BigInteger.CoreSub(left._data, right._data));
			}
			return new BigInteger(-right._sign, BigInteger.CoreSub(right._data, left._data));
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00021524 File Offset: 0x0001F724
		public static BigInteger operator *(BigInteger left, BigInteger right)
		{
			if (left._sign == 0 || right._sign == 0)
			{
				return BigInteger.Zero;
			}
			if (left._data[0] == 1U && left._data.Length == 1)
			{
				if (left._sign == 1)
				{
					return right;
				}
				return new BigInteger(-right._sign, right._data);
			}
			else
			{
				if (right._data[0] != 1U || right._data.Length != 1)
				{
					uint[] data = left._data;
					uint[] data2 = right._data;
					uint[] array = new uint[data.Length + data2.Length];
					for (int i = 0; i < data.Length; i++)
					{
						uint num = data[i];
						int num2 = i;
						ulong num3 = 0UL;
						for (int j = 0; j < data2.Length; j++)
						{
							num3 = num3 + (ulong)num * (ulong)data2[j] + (ulong)array[num2];
							array[num2++] = (uint)num3;
							num3 >>= 32;
						}
						while (num3 != 0UL)
						{
							num3 += (ulong)array[num2];
							array[num2++] = (uint)num3;
							num3 >>= 32;
						}
					}
					int num4 = array.Length - 1;
					while (num4 >= 0 && array[num4] == 0U)
					{
						num4--;
					}
					if (num4 < array.Length - 1)
					{
						Array.Resize<uint>(ref array, num4 + 1);
					}
					return new BigInteger(left._sign * right._sign, array);
				}
				if (right._sign == 1)
				{
					return left;
				}
				return new BigInteger(-left._sign, left._data);
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00021684 File Offset: 0x0001F884
		public static BigInteger operator /(BigInteger dividend, BigInteger divisor)
		{
			if (divisor._sign == 0)
			{
				throw new DivideByZeroException();
			}
			if (dividend._sign == 0)
			{
				return dividend;
			}
			uint[] array;
			uint[] array2;
			BigInteger.DivModUnsigned(dividend._data, divisor._data, out array, out array2);
			int num = array.Length - 1;
			while (num >= 0 && array[num] == 0U)
			{
				num--;
			}
			if (num == -1)
			{
				return BigInteger.Zero;
			}
			if (num < array.Length - 1)
			{
				Array.Resize<uint>(ref array, num + 1);
			}
			return new BigInteger(dividend._sign * divisor._sign, array);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00021704 File Offset: 0x0001F904
		public static BigInteger operator %(BigInteger dividend, BigInteger divisor)
		{
			if (divisor._sign == 0)
			{
				throw new DivideByZeroException();
			}
			if (dividend._sign == 0)
			{
				return dividend;
			}
			uint[] array;
			uint[] array2;
			BigInteger.DivModUnsigned(dividend._data, divisor._data, out array, out array2);
			int num = array2.Length - 1;
			while (num >= 0 && array2[num] == 0U)
			{
				num--;
			}
			if (num == -1)
			{
				return BigInteger.Zero;
			}
			if (num < array2.Length - 1)
			{
				Array.Resize<uint>(ref array2, num + 1);
			}
			return new BigInteger(dividend._sign, array2);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0002177B File Offset: 0x0001F97B
		public static BigInteger operator -(BigInteger value)
		{
			if (value._data == null)
			{
				return value;
			}
			return new BigInteger(-value._sign, value._data);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0002179A File Offset: 0x0001F99A
		public static BigInteger operator +(BigInteger value)
		{
			return value;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000217A0 File Offset: 0x0001F9A0
		public static BigInteger operator ++(BigInteger value)
		{
			if (value._data == null)
			{
				return BigInteger.One;
			}
			short sign = value._sign;
			uint[] array = value._data;
			if (array.Length == 1)
			{
				if (sign == -1 && array[0] == 1U)
				{
					return BigInteger.Zero;
				}
				if (sign == 0)
				{
					return BigInteger.One;
				}
			}
			array = ((sign == -1) ? BigInteger.CoreSub(array, 1U) : BigInteger.CoreAdd(array, 1U));
			return new BigInteger(sign, array);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00021804 File Offset: 0x0001FA04
		public static BigInteger operator --(BigInteger value)
		{
			if (value._data == null)
			{
				return BigInteger.MinusOne;
			}
			short sign = value._sign;
			uint[] array = value._data;
			if (array.Length == 1)
			{
				if (sign == 1 && array[0] == 1U)
				{
					return BigInteger.Zero;
				}
				if (sign == 0)
				{
					return BigInteger.MinusOne;
				}
			}
			array = ((sign == -1) ? BigInteger.CoreAdd(array, 1U) : BigInteger.CoreSub(array, 1U));
			return new BigInteger(sign, array);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00021868 File Offset: 0x0001FA68
		public static BigInteger operator &(BigInteger left, BigInteger right)
		{
			if (left._sign == 0)
			{
				return left;
			}
			if (right._sign == 0)
			{
				return right;
			}
			uint[] data = left._data;
			uint[] data2 = right._data;
			int sign = (int)left._sign;
			int sign2 = (int)right._sign;
			bool flag = sign == sign2 && sign == -1;
			uint[] array = new uint[Math.Max(data.Length, data2.Length)];
			ulong num = 1UL;
			ulong num2 = 1UL;
			ulong num3 = 1UL;
			int i;
			for (i = 0; i < array.Length; i++)
			{
				uint num4 = 0U;
				if (i < data.Length)
				{
					num4 = data[i];
				}
				if (sign == -1)
				{
					num = (ulong)(~(ulong)num4) + num;
					num4 = (uint)num;
					num = (ulong)((uint)(num >> 32));
				}
				uint num5 = 0U;
				if (i < data2.Length)
				{
					num5 = data2[i];
				}
				if (sign2 == -1)
				{
					num2 = (ulong)(~(ulong)num5) + num2;
					num5 = (uint)num2;
					num2 = (ulong)((uint)(num2 >> 32));
				}
				uint num6 = num4 & num5;
				if (flag)
				{
					num3 = (ulong)num6 - num3;
					num6 = ~(uint)num3;
					num3 = (ulong)((uint)(num3 >> 32) & 1U);
				}
				array[i] = num6;
			}
			i = array.Length - 1;
			while (i >= 0 && array[i] == 0U)
			{
				i--;
			}
			if (i == -1)
			{
				return BigInteger.Zero;
			}
			if (i < array.Length - 1)
			{
				Array.Resize<uint>(ref array, i + 1);
			}
			return new BigInteger(flag ? -1 : 1, array);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000219B8 File Offset: 0x0001FBB8
		public static BigInteger operator |(BigInteger left, BigInteger right)
		{
			if (left._sign == 0)
			{
				return right;
			}
			if (right._sign == 0)
			{
				return left;
			}
			uint[] data = left._data;
			uint[] data2 = right._data;
			int sign = (int)left._sign;
			int sign2 = (int)right._sign;
			bool flag = sign == -1 || sign2 == -1;
			uint[] array = new uint[Math.Max(data.Length, data2.Length)];
			ulong num = 1UL;
			ulong num2 = 1UL;
			ulong num3 = 1UL;
			int i;
			for (i = 0; i < array.Length; i++)
			{
				uint num4 = 0U;
				if (i < data.Length)
				{
					num4 = data[i];
				}
				if (sign == -1)
				{
					num = (ulong)(~(ulong)num4) + num;
					num4 = (uint)num;
					num = (ulong)((uint)(num >> 32));
				}
				uint num5 = 0U;
				if (i < data2.Length)
				{
					num5 = data2[i];
				}
				if (sign2 == -1)
				{
					num2 = (ulong)(~(ulong)num5) + num2;
					num5 = (uint)num2;
					num2 = (ulong)((uint)(num2 >> 32));
				}
				uint num6 = num4 | num5;
				if (flag)
				{
					num3 = (ulong)num6 - num3;
					num6 = ~(uint)num3;
					num3 = (ulong)((uint)(num3 >> 32) & 1U);
				}
				array[i] = num6;
			}
			i = array.Length - 1;
			while (i >= 0 && array[i] == 0U)
			{
				i--;
			}
			if (i == -1)
			{
				return BigInteger.Zero;
			}
			if (i < array.Length - 1)
			{
				Array.Resize<uint>(ref array, i + 1);
			}
			return new BigInteger(flag ? -1 : 1, array);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00021B08 File Offset: 0x0001FD08
		public static BigInteger operator ^(BigInteger left, BigInteger right)
		{
			if (left._sign == 0)
			{
				return right;
			}
			if (right._sign == 0)
			{
				return left;
			}
			uint[] data = left._data;
			uint[] data2 = right._data;
			int sign = (int)left._sign;
			int sign2 = (int)right._sign;
			bool flag = sign == -1 ^ sign2 == -1;
			uint[] array = new uint[Math.Max(data.Length, data2.Length)];
			ulong num = 1UL;
			ulong num2 = 1UL;
			ulong num3 = 1UL;
			int i;
			for (i = 0; i < array.Length; i++)
			{
				uint num4 = 0U;
				if (i < data.Length)
				{
					num4 = data[i];
				}
				if (sign == -1)
				{
					num = (ulong)(~(ulong)num4) + num;
					num4 = (uint)num;
					num = (ulong)((uint)(num >> 32));
				}
				uint num5 = 0U;
				if (i < data2.Length)
				{
					num5 = data2[i];
				}
				if (sign2 == -1)
				{
					num2 = (ulong)(~(ulong)num5) + num2;
					num5 = (uint)num2;
					num2 = (ulong)((uint)(num2 >> 32));
				}
				uint num6 = num4 ^ num5;
				if (flag)
				{
					num3 = (ulong)num6 - num3;
					num6 = ~(uint)num3;
					num3 = (ulong)((uint)(num3 >> 32) & 1U);
				}
				array[i] = num6;
			}
			i = array.Length - 1;
			while (i >= 0 && array[i] == 0U)
			{
				i--;
			}
			if (i == -1)
			{
				return BigInteger.Zero;
			}
			if (i < array.Length - 1)
			{
				Array.Resize<uint>(ref array, i + 1);
			}
			return new BigInteger(flag ? -1 : 1, array);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00021C54 File Offset: 0x0001FE54
		public static BigInteger operator ~(BigInteger value)
		{
			if (value._data == null)
			{
				return BigInteger.MinusOne;
			}
			uint[] data = value._data;
			int sign = (int)value._sign;
			bool flag = sign == 1;
			uint[] array = new uint[data.Length];
			ulong num = 1UL;
			ulong num2 = 1UL;
			int i;
			for (i = 0; i < array.Length; i++)
			{
				uint num3 = data[i];
				if (sign == -1)
				{
					num = (ulong)(~(ulong)num3) + num;
					num3 = (uint)num;
					num = (ulong)((uint)(num >> 32));
				}
				num3 = ~num3;
				if (flag)
				{
					num2 = (ulong)num3 - num2;
					num3 = ~(uint)num2;
					num2 = (ulong)((uint)(num2 >> 32) & 1U);
				}
				array[i] = num3;
			}
			i = array.Length - 1;
			while (i >= 0 && array[i] == 0U)
			{
				i--;
			}
			if (i == -1)
			{
				return BigInteger.Zero;
			}
			if (i < array.Length - 1)
			{
				Array.Resize<uint>(ref array, i + 1);
			}
			return new BigInteger(flag ? -1 : 1, array);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00021D34 File Offset: 0x0001FF34
		private static int BitScanBackward(uint word)
		{
			for (int i = 31; i >= 0; i--)
			{
				uint num = 1U << i;
				if ((word & num) == num)
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00021D60 File Offset: 0x0001FF60
		public static BigInteger operator <<(BigInteger value, int shift)
		{
			if (shift == 0 || value._data == null)
			{
				return value;
			}
			if (shift < 0)
			{
				return value >> -shift;
			}
			uint[] data = value._data;
			int sign = (int)value._sign;
			int num = BigInteger.BitScanBackward(data[data.Length - 1]);
			int num2 = shift - (31 - num);
			int num3 = (num2 >> 5) + (((num2 & 31) != 0) ? 1 : 0);
			uint[] array = new uint[data.Length + num3];
			int num4 = shift >> 5;
			int num5 = shift & 31;
			int num6 = 32 - num5;
			if (num6 == 32)
			{
				for (int i = 0; i < data.Length; i++)
				{
					uint num7 = data[i];
					array[i + num4] |= num7 << num5;
				}
			}
			else
			{
				for (int j = 0; j < data.Length; j++)
				{
					uint num8 = data[j];
					array[j + num4] |= num8 << num5;
					if (j + num4 + 1 < array.Length)
					{
						array[j + num4 + 1] = num8 >> num6;
					}
				}
			}
			return new BigInteger((short)sign, array);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00021E68 File Offset: 0x00020068
		public static BigInteger operator >>(BigInteger value, int shift)
		{
			if (shift == 0 || value._sign == 0)
			{
				return value;
			}
			if (shift < 0)
			{
				return value << -shift;
			}
			uint[] data = value._data;
			int sign = (int)value._sign;
			int num = BigInteger.BitScanBackward(data[data.Length - 1]);
			int num2 = shift >> 5;
			int num3 = shift & 31;
			int num4 = num2;
			if (num3 > num)
			{
				num4++;
			}
			int num5 = data.Length - num4;
			if (num5 > 0)
			{
				uint[] array = new uint[num5];
				int num6 = 32 - num3;
				if (num6 == 32)
				{
					for (int i = data.Length - 1; i >= num2; i--)
					{
						uint num7 = data[i];
						if (i - num2 < array.Length)
						{
							array[i - num2] |= num7 >> num3;
						}
					}
				}
				else
				{
					for (int j = data.Length - 1; j >= num2; j--)
					{
						uint num8 = data[j];
						if (j - num2 < array.Length)
						{
							array[j - num2] |= num8 >> num3;
						}
						if (j - num2 - 1 >= 0)
						{
							array[j - num2 - 1] = num8 << num6;
						}
					}
				}
				if (sign == -1)
				{
					for (int k = 0; k < num2; k++)
					{
						if (data[k] != 0U)
						{
							return --new BigInteger((short)sign, array);
						}
					}
					if (num3 > 0 && data[num2] << num6 != 0U)
					{
						return --new BigInteger((short)sign, array);
					}
				}
				return new BigInteger((short)sign, array);
			}
			if (sign == 1)
			{
				return BigInteger.Zero;
			}
			return BigInteger.MinusOne;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00021FD8 File Offset: 0x000201D8
		public static bool operator <(BigInteger left, BigInteger right)
		{
			return BigInteger.Compare(left, right) < 0;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00021FE4 File Offset: 0x000201E4
		public static bool operator <(BigInteger left, long right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00021FF1 File Offset: 0x000201F1
		public static bool operator <(long left, BigInteger right)
		{
			return right.CompareTo(left) > 0;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00021FFE File Offset: 0x000201FE
		[CLSCompliant(false)]
		public static bool operator <(BigInteger left, ulong right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002200B File Offset: 0x0002020B
		[CLSCompliant(false)]
		public static bool operator <(ulong left, BigInteger right)
		{
			return right.CompareTo(left) > 0;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00022018 File Offset: 0x00020218
		public static bool operator <=(BigInteger left, BigInteger right)
		{
			return BigInteger.Compare(left, right) <= 0;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00022027 File Offset: 0x00020227
		public static bool operator <=(BigInteger left, long right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00022037 File Offset: 0x00020237
		public static bool operator <=(long left, BigInteger right)
		{
			return right.CompareTo(left) >= 0;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00022047 File Offset: 0x00020247
		[CLSCompliant(false)]
		public static bool operator <=(BigInteger left, ulong right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00022057 File Offset: 0x00020257
		[CLSCompliant(false)]
		public static bool operator <=(ulong left, BigInteger right)
		{
			return right.CompareTo(left) >= 0;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00022067 File Offset: 0x00020267
		public static bool operator >(BigInteger left, BigInteger right)
		{
			return BigInteger.Compare(left, right) > 0;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00022073 File Offset: 0x00020273
		public static bool operator >(BigInteger left, long right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00022080 File Offset: 0x00020280
		public static bool operator >(long left, BigInteger right)
		{
			return right.CompareTo(left) < 0;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002208D File Offset: 0x0002028D
		[CLSCompliant(false)]
		public static bool operator >(BigInteger left, ulong right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002209A File Offset: 0x0002029A
		[CLSCompliant(false)]
		public static bool operator >(ulong left, BigInteger right)
		{
			return right.CompareTo(left) < 0;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x000220A7 File Offset: 0x000202A7
		public static bool operator >=(BigInteger left, BigInteger right)
		{
			return BigInteger.Compare(left, right) >= 0;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x000220B6 File Offset: 0x000202B6
		public static bool operator >=(BigInteger left, long right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000220C6 File Offset: 0x000202C6
		public static bool operator >=(long left, BigInteger right)
		{
			return right.CompareTo(left) <= 0;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x000220D6 File Offset: 0x000202D6
		[CLSCompliant(false)]
		public static bool operator >=(BigInteger left, ulong right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000220E6 File Offset: 0x000202E6
		[CLSCompliant(false)]
		public static bool operator >=(ulong left, BigInteger right)
		{
			return right.CompareTo(left) <= 0;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000220F6 File Offset: 0x000202F6
		public static bool operator ==(BigInteger left, BigInteger right)
		{
			return BigInteger.Compare(left, right) == 0;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00022102 File Offset: 0x00020302
		public static bool operator ==(BigInteger left, long right)
		{
			return left.CompareTo(right) == 0;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002210F File Offset: 0x0002030F
		public static bool operator ==(long left, BigInteger right)
		{
			return right.CompareTo(left) == 0;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002211C File Offset: 0x0002031C
		[CLSCompliant(false)]
		public static bool operator ==(BigInteger left, ulong right)
		{
			return left.CompareTo(right) == 0;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00022129 File Offset: 0x00020329
		[CLSCompliant(false)]
		public static bool operator ==(ulong left, BigInteger right)
		{
			return right.CompareTo(left) == 0;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00022136 File Offset: 0x00020336
		public static bool operator !=(BigInteger left, BigInteger right)
		{
			return BigInteger.Compare(left, right) != 0;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00022142 File Offset: 0x00020342
		public static bool operator !=(BigInteger left, long right)
		{
			return left.CompareTo(right) != 0;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002214F File Offset: 0x0002034F
		public static bool operator !=(long left, BigInteger right)
		{
			return right.CompareTo(left) != 0;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002215C File Offset: 0x0002035C
		[CLSCompliant(false)]
		public static bool operator !=(BigInteger left, ulong right)
		{
			return left.CompareTo(right) != 0;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00022169 File Offset: 0x00020369
		[CLSCompliant(false)]
		public static bool operator !=(ulong left, BigInteger right)
		{
			return right.CompareTo(left) != 0;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00022176 File Offset: 0x00020376
		public override bool Equals(object obj)
		{
			return obj is BigInteger && this.Equals((BigInteger)obj);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00022190 File Offset: 0x00020390
		public bool Equals(BigInteger other)
		{
			if (this._sign != other._sign)
			{
				return false;
			}
			int num = (this._data != null) ? this._data.Length : 0;
			int num2 = (other._data != null) ? other._data.Length : 0;
			if (num != num2)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (this._data[i] != other._data[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000221FC File Offset: 0x000203FC
		public bool Equals(long other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00022208 File Offset: 0x00020408
		public override string ToString()
		{
			return this.ToString(10U, null);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00022214 File Offset: 0x00020414
		private string ToStringWithPadding(string format, uint radix, IFormatProvider provider)
		{
			if (format.Length <= 1)
			{
				return this.ToString(radix, provider);
			}
			int num = Convert.ToInt32(format.Substring(1), CultureInfo.InvariantCulture.NumberFormat);
			string text = this.ToString(radix, provider);
			if (text.Length >= num)
			{
				return text;
			}
			string text2 = new string('0', num - text.Length);
			if (text[0] != '-')
			{
				return text2 + text;
			}
			return "-" + text2 + text.Substring(1);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00022293 File Offset: 0x00020493
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002229D File Offset: 0x0002049D
		public string ToString(IFormatProvider provider)
		{
			return this.ToString(null, provider);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000222A8 File Offset: 0x000204A8
		public string ToString(string format, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty(format))
			{
				return this.ToString(10U, provider);
			}
			char c = format[0];
			if (c <= 'X')
			{
				if (c <= 'G')
				{
					if (c != 'D' && c != 'G')
					{
						goto IL_6F;
					}
				}
				else if (c != 'R')
				{
					if (c != 'X')
					{
						goto IL_6F;
					}
					goto IL_64;
				}
			}
			else if (c <= 'g')
			{
				if (c != 'd' && c != 'g')
				{
					goto IL_6F;
				}
			}
			else if (c != 'r')
			{
				if (c != 'x')
				{
					goto IL_6F;
				}
				goto IL_64;
			}
			return this.ToStringWithPadding(format, 10U, provider);
			IL_64:
			return this.ToStringWithPadding(format, 16U, null);
			IL_6F:
			throw new FormatException(string.Format("format '{0}' not implemented", format));
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00022334 File Offset: 0x00020534
		private static uint[] MakeTwoComplement(uint[] v)
		{
			uint[] array = new uint[v.Length];
			ulong num = 1UL;
			for (int i = 0; i < v.Length; i++)
			{
				uint num2 = v[i];
				num = (ulong)(~(ulong)num2) + num;
				num2 = (uint)num;
				num = (ulong)((uint)(num >> 32));
				array[i] = num2;
			}
			uint num3 = array[array.Length - 1];
			int num4 = BigInteger.FirstNonFfByte(num3);
			uint num5 = 255U;
			for (int j = 1; j < num4; j++)
			{
				num5 = (num5 << 8 | 255U);
			}
			array[array.Length - 1] = (num3 & num5);
			return array;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000223BC File Offset: 0x000205BC
		private string ToString(uint radix, IFormatProvider provider)
		{
			if ((long)"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Length < (long)((ulong)radix))
			{
				throw new ArgumentException("charSet length less than radix", "characterSet");
			}
			if (radix == 1U)
			{
				throw new ArgumentException("There is no such thing as radix one notation", "radix");
			}
			if (this._sign == 0)
			{
				return "0";
			}
			if (this._data.Length != 1 || this._data[0] != 1U)
			{
				List<char> list = new List<char>(1 + this._data.Length * 3 / 10);
				BigInteger bigInteger;
				if (this._sign == 1)
				{
					bigInteger = this;
				}
				else
				{
					uint[] array = this._data;
					if (radix > 10U)
					{
						array = BigInteger.MakeTwoComplement(array);
					}
					bigInteger = new BigInteger(1, array);
				}
				while (bigInteger != 0L)
				{
					BigInteger value;
					bigInteger = BigInteger.DivRem(bigInteger, radix, out value);
					list.Add("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[(int)value]);
				}
				if (this._sign == -1 && radix == 10U)
				{
					NumberFormatInfo numberFormatInfo = null;
					if (provider != null)
					{
						numberFormatInfo = (provider.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo);
					}
					if (numberFormatInfo != null)
					{
						string negativeSign = numberFormatInfo.NegativeSign;
						for (int i = negativeSign.Length - 1; i >= 0; i--)
						{
							list.Add(negativeSign[i]);
						}
					}
					else
					{
						list.Add('-');
					}
				}
				char c = list[list.Count - 1];
				if (this._sign == 1 && radix > 10U && (c < '0' || c > '9'))
				{
					list.Add('0');
				}
				list.Reverse();
				return new string(list.ToArray());
			}
			if (this._sign != 1)
			{
				return "-1";
			}
			return "1";
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00022554 File Offset: 0x00020754
		public static BigInteger Parse(string value)
		{
			BigInteger result;
			Exception ex;
			if (!BigInteger.Parse(value, false, out result, out ex))
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00022571 File Offset: 0x00020771
		public static BigInteger Parse(string value, NumberStyles style)
		{
			return BigInteger.Parse(value, style, null);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002257B File Offset: 0x0002077B
		public static BigInteger Parse(string value, IFormatProvider provider)
		{
			return BigInteger.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00022588 File Offset: 0x00020788
		public static BigInteger Parse(string value, NumberStyles style, IFormatProvider provider)
		{
			BigInteger result;
			Exception ex;
			if (!BigInteger.Parse(value, style, provider, false, out result, out ex))
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000225A8 File Offset: 0x000207A8
		public static bool TryParse(string value, out BigInteger result)
		{
			Exception ex;
			return BigInteger.Parse(value, true, out result, out ex);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x000225C0 File Offset: 0x000207C0
		public static bool TryParse(string value, NumberStyles style, IFormatProvider provider, out BigInteger result)
		{
			Exception ex;
			if (!BigInteger.Parse(value, style, provider, true, out result, out ex))
			{
				result = BigInteger.Zero;
				return false;
			}
			return true;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000225EC File Offset: 0x000207EC
		private static bool Parse(string value, NumberStyles style, IFormatProvider fp, bool tryParse, out BigInteger result, out Exception exc)
		{
			result = BigInteger.Zero;
			exc = null;
			if (value == null)
			{
				if (!tryParse)
				{
					exc = new ArgumentNullException("value");
				}
				return false;
			}
			if (value.Length == 0)
			{
				if (!tryParse)
				{
					exc = BigInteger.GetFormatException();
				}
				return false;
			}
			NumberFormatInfo numberFormatInfo = null;
			if (fp != null)
			{
				Type typeFromHandle = typeof(NumberFormatInfo);
				numberFormatInfo = (NumberFormatInfo)fp.GetFormat(typeFromHandle);
			}
			if (numberFormatInfo == null)
			{
				numberFormatInfo = NumberFormatInfo.CurrentInfo;
			}
			if (!BigInteger.CheckStyle(style, tryParse, ref exc))
			{
				return false;
			}
			bool flag = (style & NumberStyles.AllowCurrencySymbol) > NumberStyles.None;
			bool flag2 = (style & NumberStyles.AllowHexSpecifier) > NumberStyles.None;
			bool flag3 = (style & NumberStyles.AllowThousands) > NumberStyles.None;
			bool flag4 = (style & NumberStyles.AllowDecimalPoint) > NumberStyles.None;
			bool flag5 = (style & NumberStyles.AllowParentheses) > NumberStyles.None;
			bool flag6 = (style & NumberStyles.AllowTrailingSign) > NumberStyles.None;
			bool flag7 = (style & NumberStyles.AllowLeadingSign) > NumberStyles.None;
			bool flag8 = (style & NumberStyles.AllowTrailingWhite) > NumberStyles.None;
			bool flag9 = (style & NumberStyles.AllowLeadingWhite) > NumberStyles.None;
			bool flag10 = (style & NumberStyles.AllowExponent) > NumberStyles.None;
			int i = 0;
			if (flag9 && !BigInteger.JumpOverWhitespace(ref i, value, true, tryParse, ref exc))
			{
				return false;
			}
			bool flag11 = false;
			bool flag12 = false;
			bool flag13 = false;
			bool flag14 = false;
			if (flag5 && value[i] == '(')
			{
				flag11 = true;
				flag13 = true;
				flag12 = true;
				i++;
				if (flag9 && !BigInteger.JumpOverWhitespace(ref i, value, true, tryParse, ref exc))
				{
					return false;
				}
				if (value.Substring(i, numberFormatInfo.NegativeSign.Length) == numberFormatInfo.NegativeSign)
				{
					if (!tryParse)
					{
						exc = BigInteger.GetFormatException();
					}
					return false;
				}
				if (value.Substring(i, numberFormatInfo.PositiveSign.Length) == numberFormatInfo.PositiveSign)
				{
					if (!tryParse)
					{
						exc = BigInteger.GetFormatException();
					}
					return false;
				}
			}
			if (flag7 && !flag13)
			{
				BigInteger.FindSign(ref i, value, numberFormatInfo, ref flag13, ref flag12);
				if (flag13)
				{
					if (flag9 && !BigInteger.JumpOverWhitespace(ref i, value, true, tryParse, ref exc))
					{
						return false;
					}
					if (flag)
					{
						BigInteger.FindCurrency(ref i, value, numberFormatInfo, ref flag14);
						if (flag14 && flag9 && !BigInteger.JumpOverWhitespace(ref i, value, true, tryParse, ref exc))
						{
							return false;
						}
					}
				}
			}
			if (flag && !flag14)
			{
				BigInteger.FindCurrency(ref i, value, numberFormatInfo, ref flag14);
				if (flag14)
				{
					if (flag9 && !BigInteger.JumpOverWhitespace(ref i, value, true, tryParse, ref exc))
					{
						return false;
					}
					if (flag14 && (!flag13 && flag7))
					{
						BigInteger.FindSign(ref i, value, numberFormatInfo, ref flag13, ref flag12);
						if (flag13 && flag9 && !BigInteger.JumpOverWhitespace(ref i, value, true, tryParse, ref exc))
						{
							return false;
						}
					}
				}
			}
			BigInteger bigInteger = BigInteger.Zero;
			int num = 0;
			int num2 = -1;
			bool flag15 = true;
			while (i < value.Length)
			{
				if (!BigInteger.ValidDigit(value[i], flag2))
				{
					if (!flag3 || (!BigInteger.FindOther(ref i, value, numberFormatInfo.NumberGroupSeparator) && !BigInteger.FindOther(ref i, value, numberFormatInfo.CurrencyGroupSeparator)))
					{
						if (!flag4 || num2 >= 0 || (!BigInteger.FindOther(ref i, value, numberFormatInfo.NumberDecimalSeparator) && !BigInteger.FindOther(ref i, value, numberFormatInfo.CurrencyDecimalSeparator)))
						{
							break;
						}
						num2 = num;
					}
				}
				else
				{
					num++;
					if (flag2)
					{
						char c = value[i++];
						byte b;
						if (char.IsDigit(c))
						{
							b = (byte)(c - '0');
						}
						else if (char.IsLower(c))
						{
							b = (byte)(c - 'a' + '\n');
						}
						else
						{
							b = (byte)(c - 'A' + '\n');
						}
						if (flag15 && b >= 8)
						{
							flag12 = true;
						}
						bigInteger = bigInteger * 16 + b;
						flag15 = false;
					}
					else
					{
						bigInteger = bigInteger * 10 + (byte)(value[i++] - '0');
					}
				}
			}
			if (num == 0)
			{
				if (!tryParse)
				{
					exc = BigInteger.GetFormatException();
				}
				return false;
			}
			if (flag2 && flag12)
			{
				BigInteger right = BigInteger.Pow(16, num) - 1;
				bigInteger = (bigInteger ^ right) + 1;
			}
			int num3 = 0;
			if (flag10 && BigInteger.FindExponent(ref i, value, ref num3, tryParse, ref exc) && exc != null)
			{
				return false;
			}
			if (flag6 && !flag13)
			{
				BigInteger.FindSign(ref i, value, numberFormatInfo, ref flag13, ref flag12);
				if (flag13 && i < value.Length && flag8 && !BigInteger.JumpOverWhitespace(ref i, value, true, tryParse, ref exc))
				{
					return false;
				}
			}
			if (flag && !flag14)
			{
				if (flag8 && i < value.Length && !BigInteger.JumpOverWhitespace(ref i, value, false, tryParse, ref exc))
				{
					return false;
				}
				BigInteger.FindCurrency(ref i, value, numberFormatInfo, ref flag14);
				if (flag14 && i < value.Length)
				{
					if (flag8 && !BigInteger.JumpOverWhitespace(ref i, value, true, tryParse, ref exc))
					{
						return false;
					}
					if (!flag13 && flag6)
					{
						BigInteger.FindSign(ref i, value, numberFormatInfo, ref flag13, ref flag12);
					}
				}
			}
			if (flag8 && i < value.Length && !BigInteger.JumpOverWhitespace(ref i, value, false, tryParse, ref exc))
			{
				return false;
			}
			if (flag11)
			{
				if (i >= value.Length || value[i++] != ')')
				{
					if (!tryParse)
					{
						exc = BigInteger.GetFormatException();
					}
					return false;
				}
				if (flag8 && i < value.Length && !BigInteger.JumpOverWhitespace(ref i, value, false, tryParse, ref exc))
				{
					return false;
				}
			}
			if (i < value.Length && value[i] != '\0')
			{
				if (!tryParse)
				{
					exc = BigInteger.GetFormatException();
				}
				return false;
			}
			if (num2 >= 0)
			{
				num3 = num3 - num + num2;
			}
			if (num3 < 0)
			{
				BigInteger bigInteger2;
				bigInteger = BigInteger.DivRem(bigInteger, BigInteger.Pow(10, -num3), out bigInteger2);
				if (!bigInteger2.IsZero)
				{
					if (!tryParse)
					{
						exc = new OverflowException(string.Concat(new object[]
						{
							"Value too large or too small. exp=",
							num3,
							" rem = ",
							bigInteger2,
							" pow = ",
							BigInteger.Pow(10, -num3)
						}));
					}
					return false;
				}
			}
			else if (num3 > 0)
			{
				bigInteger = BigInteger.Pow(10, num3) * bigInteger;
			}
			if (bigInteger._sign == 0)
			{
				result = bigInteger;
			}
			else if (flag12)
			{
				result = new BigInteger(-1, bigInteger._data);
			}
			else
			{
				result = new BigInteger(1, bigInteger._data);
			}
			return true;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00022BE0 File Offset: 0x00020DE0
		private static bool CheckStyle(NumberStyles style, bool tryParse, ref Exception exc)
		{
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				NumberStyles numberStyles = style ^ NumberStyles.AllowHexSpecifier;
				if ((numberStyles & NumberStyles.AllowLeadingWhite) != NumberStyles.None)
				{
					numberStyles ^= NumberStyles.AllowLeadingWhite;
				}
				if ((numberStyles & NumberStyles.AllowTrailingWhite) != NumberStyles.None)
				{
					numberStyles ^= NumberStyles.AllowTrailingWhite;
				}
				if (numberStyles != NumberStyles.None)
				{
					if (!tryParse)
					{
						exc = new ArgumentException("With AllowHexSpecifier only AllowLeadingWhite and AllowTrailingWhite are permitted.");
					}
					return false;
				}
			}
			else if (style > NumberStyles.Any)
			{
				if (!tryParse)
				{
					exc = new ArgumentException("Not a valid number style");
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00022C3E File Offset: 0x00020E3E
		private static bool JumpOverWhitespace(ref int pos, string s, bool reportError, bool tryParse, ref Exception exc)
		{
			while (pos < s.Length && char.IsWhiteSpace(s[pos]))
			{
				pos++;
			}
			if (reportError && pos >= s.Length)
			{
				if (!tryParse)
				{
					exc = BigInteger.GetFormatException();
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00022C7C File Offset: 0x00020E7C
		private static void FindSign(ref int pos, string s, NumberFormatInfo nfi, ref bool foundSign, ref bool negative)
		{
			if (pos + nfi.NegativeSign.Length <= s.Length && string.CompareOrdinal(s, pos, nfi.NegativeSign, 0, nfi.NegativeSign.Length) == 0)
			{
				negative = true;
				foundSign = true;
				pos += nfi.NegativeSign.Length;
				return;
			}
			if (pos + nfi.PositiveSign.Length <= s.Length && string.CompareOrdinal(s, pos, nfi.PositiveSign, 0, nfi.PositiveSign.Length) == 0)
			{
				negative = false;
				pos += nfi.PositiveSign.Length;
				foundSign = true;
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00022D1C File Offset: 0x00020F1C
		private static void FindCurrency(ref int pos, string s, NumberFormatInfo nfi, ref bool foundCurrency)
		{
			if (pos + nfi.CurrencySymbol.Length <= s.Length && s.Substring(pos, nfi.CurrencySymbol.Length) == nfi.CurrencySymbol)
			{
				foundCurrency = true;
				pos += nfi.CurrencySymbol.Length;
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00022D74 File Offset: 0x00020F74
		private static bool FindExponent(ref int pos, string s, ref int exponent, bool tryParse, ref Exception exc)
		{
			exponent = 0;
			if (pos >= s.Length || (s[pos] != 'e' && s[pos] != 'E'))
			{
				exc = null;
				return false;
			}
			int i = pos + 1;
			if (i == s.Length)
			{
				exc = (tryParse ? null : BigInteger.GetFormatException());
				return true;
			}
			bool flag = false;
			if (s[i] == '-')
			{
				flag = true;
				if (++i == s.Length)
				{
					exc = (tryParse ? null : BigInteger.GetFormatException());
					return true;
				}
			}
			if (s[i] == '+' && ++i == s.Length)
			{
				exc = (tryParse ? null : BigInteger.GetFormatException());
				return true;
			}
			long num = 0L;
			while (i < s.Length)
			{
				if (!char.IsDigit(s[i]))
				{
					exc = (tryParse ? null : BigInteger.GetFormatException());
					return true;
				}
				num = checked(num * 10L - unchecked((long)(checked(s[i] - '0'))));
				if (num < -2147483648L || num > 2147483647L)
				{
					exc = (tryParse ? null : new OverflowException("Value too large or too small."));
					return true;
				}
				i++;
			}
			if (!flag)
			{
				num = -num;
			}
			exc = null;
			exponent = (int)num;
			pos = i;
			return true;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00022E99 File Offset: 0x00021099
		private static bool FindOther(ref int pos, string s, string other)
		{
			if (pos + other.Length <= s.Length && s.Substring(pos, other.Length) == other)
			{
				pos += other.Length;
				return true;
			}
			return false;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00022ED0 File Offset: 0x000210D0
		private static bool ValidDigit(char e, bool allowHex)
		{
			if (allowHex)
			{
				return char.IsDigit(e) || (e >= 'A' && e <= 'F') || (e >= 'a' && e <= 'f');
			}
			return char.IsDigit(e);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00022EFF File Offset: 0x000210FF
		private static Exception GetFormatException()
		{
			return new FormatException("Input string was not in the correct format");
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00022F0C File Offset: 0x0002110C
		private static bool ProcessTrailingWhitespace(bool tryParse, string s, int position, ref Exception exc)
		{
			int length = s.Length;
			for (int i = position; i < length; i++)
			{
				char c = s[i];
				if (c != '\0' && !char.IsWhiteSpace(c))
				{
					if (!tryParse)
					{
						exc = BigInteger.GetFormatException();
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00022F4C File Offset: 0x0002114C
		private static bool Parse(string value, bool tryParse, out BigInteger result, out Exception exc)
		{
			int num = 1;
			bool flag = false;
			result = BigInteger.Zero;
			exc = null;
			if (value == null)
			{
				if (!tryParse)
				{
					exc = new ArgumentNullException("value");
				}
				return false;
			}
			int length = value.Length;
			int i;
			for (i = 0; i < length; i++)
			{
				char c = value[i];
				if (!char.IsWhiteSpace(c))
				{
					break;
				}
			}
			if (i == length)
			{
				if (!tryParse)
				{
					exc = BigInteger.GetFormatException();
				}
				return false;
			}
			NumberFormatInfo currentInfo = NumberFormatInfo.CurrentInfo;
			string negativeSign = currentInfo.NegativeSign;
			string positiveSign = currentInfo.PositiveSign;
			if (string.CompareOrdinal(value, i, positiveSign, 0, positiveSign.Length) == 0)
			{
				i += positiveSign.Length;
			}
			else if (string.CompareOrdinal(value, i, negativeSign, 0, negativeSign.Length) == 0)
			{
				num = -1;
				i += negativeSign.Length;
			}
			BigInteger bigInteger = BigInteger.Zero;
			while (i < length)
			{
				char c = value[i];
				if (c == '\0')
				{
					i = length;
				}
				else if (c >= '0' && c <= '9')
				{
					byte value2 = (byte)(c - '0');
					bigInteger = bigInteger * 10 + value2;
					flag = true;
				}
				else if (!BigInteger.ProcessTrailingWhitespace(tryParse, value, i, ref exc))
				{
					return false;
				}
				i++;
			}
			if (!flag)
			{
				if (!tryParse)
				{
					exc = BigInteger.GetFormatException();
				}
				return false;
			}
			if (bigInteger._sign == 0)
			{
				result = bigInteger;
			}
			else if (num == -1)
			{
				result = new BigInteger(-1, bigInteger._data);
			}
			else
			{
				result = new BigInteger(1, bigInteger._data);
			}
			return true;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x000230B8 File Offset: 0x000212B8
		public static BigInteger Min(BigInteger left, BigInteger right)
		{
			int sign = (int)left._sign;
			int sign2 = (int)right._sign;
			if (sign < sign2)
			{
				return left;
			}
			if (sign2 < sign)
			{
				return right;
			}
			int num = BigInteger.CoreCompare(left._data, right._data);
			if (sign == -1)
			{
				num = -num;
			}
			if (num <= 0)
			{
				return left;
			}
			return right;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00023100 File Offset: 0x00021300
		public static BigInteger Max(BigInteger left, BigInteger right)
		{
			int sign = (int)left._sign;
			int sign2 = (int)right._sign;
			if (sign > sign2)
			{
				return left;
			}
			if (sign2 > sign)
			{
				return right;
			}
			int num = BigInteger.CoreCompare(left._data, right._data);
			if (sign == -1)
			{
				num = -num;
			}
			if (num >= 0)
			{
				return left;
			}
			return right;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00023147 File Offset: 0x00021347
		public static BigInteger Abs(BigInteger value)
		{
			return new BigInteger(Math.Abs(value._sign), value._data);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00023160 File Offset: 0x00021360
		public static BigInteger DivRem(BigInteger dividend, BigInteger divisor, out BigInteger remainder)
		{
			if (divisor._sign == 0)
			{
				throw new DivideByZeroException();
			}
			if (dividend._sign == 0)
			{
				remainder = dividend;
				return dividend;
			}
			uint[] array;
			uint[] array2;
			BigInteger.DivModUnsigned(dividend._data, divisor._data, out array, out array2);
			int num = array2.Length - 1;
			while (num >= 0 && array2[num] == 0U)
			{
				num--;
			}
			if (num == -1)
			{
				remainder = BigInteger.Zero;
			}
			else
			{
				if (num < array2.Length - 1)
				{
					Array.Resize<uint>(ref array2, num + 1);
				}
				remainder = new BigInteger(dividend._sign, array2);
			}
			num = array.Length - 1;
			while (num >= 0 && array[num] == 0U)
			{
				num--;
			}
			if (num == -1)
			{
				return BigInteger.Zero;
			}
			if (num < array.Length - 1)
			{
				Array.Resize<uint>(ref array, num + 1);
			}
			return new BigInteger(dividend._sign * divisor._sign, array);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00023230 File Offset: 0x00021430
		public static BigInteger Pow(BigInteger value, int exponent)
		{
			if (exponent < 0)
			{
				throw new ArgumentOutOfRangeException("exponent", "exp must be >= 0");
			}
			if (exponent == 0)
			{
				return BigInteger.One;
			}
			if (exponent == 1)
			{
				return value;
			}
			BigInteger bigInteger = BigInteger.One;
			while (exponent != 0)
			{
				if ((exponent & 1) != 0)
				{
					bigInteger *= value;
				}
				if (exponent == 1)
				{
					break;
				}
				value *= value;
				exponent >>= 1;
			}
			return bigInteger;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002328C File Offset: 0x0002148C
		public static BigInteger ModPow(BigInteger value, BigInteger exponent, BigInteger modulus)
		{
			if (exponent._sign == -1)
			{
				throw new ArgumentOutOfRangeException("exponent", "power must be >= 0");
			}
			if (modulus._sign == 0)
			{
				throw new DivideByZeroException();
			}
			BigInteger bigInteger = BigInteger.One % modulus;
			while (exponent._sign != 0)
			{
				if (!exponent.IsEven)
				{
					bigInteger *= value;
					bigInteger %= modulus;
				}
				if (exponent.IsOne)
				{
					break;
				}
				value *= value;
				value %= modulus;
				exponent >>= 1;
			}
			return bigInteger;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00023314 File Offset: 0x00021514
		public static BigInteger GreatestCommonDivisor(BigInteger left, BigInteger right)
		{
			if (left._sign != 0 && left._data.Length == 1 && left._data[0] == 1U)
			{
				return BigInteger.One;
			}
			if (right._sign != 0 && right._data.Length == 1 && right._data[0] == 1U)
			{
				return BigInteger.One;
			}
			if (left.IsZero)
			{
				return BigInteger.Abs(right);
			}
			if (right.IsZero)
			{
				return BigInteger.Abs(left);
			}
			BigInteger bigInteger = new BigInteger(1, left._data);
			BigInteger bigInteger2 = new BigInteger(1, right._data);
			BigInteger bigInteger3 = bigInteger2;
			while (bigInteger._data.Length > 1)
			{
				bigInteger3 = bigInteger;
				bigInteger = bigInteger2 % bigInteger;
				bigInteger2 = bigInteger3;
			}
			if (bigInteger.IsZero)
			{
				return bigInteger3;
			}
			uint num = bigInteger._data[0];
			uint num2 = (uint)(bigInteger2 % num);
			int num3 = 0;
			while (((num2 | num) & 1U) == 0U)
			{
				num2 >>= 1;
				num >>= 1;
				num3++;
			}
			while (num2 != 0U)
			{
				while ((num2 & 1U) == 0U)
				{
					num2 >>= 1;
				}
				while ((num & 1U) == 0U)
				{
					num >>= 1;
				}
				if (num2 >= num)
				{
					num2 = num2 - num >> 1;
				}
				else
				{
					num = num - num2 >> 1;
				}
			}
			return num << num3;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00023444 File Offset: 0x00021644
		public static double Log(BigInteger value, double baseValue)
		{
			if (value._sign == -1 || baseValue == 1.0 || baseValue == -1.0 || baseValue == double.NegativeInfinity || double.IsNaN(baseValue))
			{
				return double.NaN;
			}
			if (baseValue == 0.0 || baseValue == double.PositiveInfinity)
			{
				if (!value.IsOne)
				{
					return double.NaN;
				}
				return 0.0;
			}
			else
			{
				if (value._data == null)
				{
					return double.NegativeInfinity;
				}
				int num = value._data.Length - 1;
				int num2 = -1;
				for (int i = 31; i >= 0; i--)
				{
					if (((ulong)value._data[num] & (ulong)(1L << (i & 31))) != 0UL)
					{
						num2 = i + num * 32;
						break;
					}
				}
				long num3 = (long)num2;
				double num4 = 0.0;
				double num5 = 1.0;
				BigInteger bigInteger = BigInteger.One;
				long num6;
				for (num6 = num3; num6 > 2147483647L; num6 -= 2147483647L)
				{
					bigInteger <<= int.MaxValue;
				}
				bigInteger <<= (int)num6;
				for (long num7 = num3; num7 >= 0L; num7 -= 1L)
				{
					if ((value & bigInteger)._sign != 0)
					{
						num4 += num5;
					}
					num5 *= 0.5;
					bigInteger >>= 1;
				}
				return (Math.Log(num4) + Math.Log(2.0) * (double)num3) / Math.Log(baseValue);
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000235C3 File Offset: 0x000217C3
		public static double Log(BigInteger value)
		{
			return BigInteger.Log(value, 2.718281828459045);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000235D4 File Offset: 0x000217D4
		public static double Log10(BigInteger value)
		{
			return BigInteger.Log(value, 10.0);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000235E5 File Offset: 0x000217E5
		[CLSCompliant(false)]
		public bool Equals(ulong other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000235F4 File Offset: 0x000217F4
		public override int GetHashCode()
		{
			uint num = (uint)((long)this._sign * 16843009L);
			if (this._data != null)
			{
				foreach (uint num2 in this._data)
				{
					num ^= num2;
				}
			}
			return (int)num;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00023637 File Offset: 0x00021837
		public static BigInteger Add(BigInteger left, BigInteger right)
		{
			return left + right;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00023640 File Offset: 0x00021840
		public static BigInteger Subtract(BigInteger left, BigInteger right)
		{
			return left - right;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00023649 File Offset: 0x00021849
		public static BigInteger Multiply(BigInteger left, BigInteger right)
		{
			return left * right;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00023652 File Offset: 0x00021852
		public static BigInteger Divide(BigInteger dividend, BigInteger divisor)
		{
			return dividend / divisor;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002365B File Offset: 0x0002185B
		public static BigInteger Remainder(BigInteger dividend, BigInteger divisor)
		{
			return dividend % divisor;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00023664 File Offset: 0x00021864
		public static BigInteger Negate(BigInteger value)
		{
			return -value;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002366C File Offset: 0x0002186C
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is BigInteger))
			{
				return -1;
			}
			return BigInteger.Compare(this, (BigInteger)obj);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002368E File Offset: 0x0002188E
		public int CompareTo(BigInteger other)
		{
			return BigInteger.Compare(this, other);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002369C File Offset: 0x0002189C
		[CLSCompliant(false)]
		public int CompareTo(ulong other)
		{
			if (this._sign < 0)
			{
				return -1;
			}
			if (this._sign == 0)
			{
				if (other != 0UL)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (this._data.Length > 2)
				{
					return 1;
				}
				uint high = (uint)(other >> 32);
				uint low = (uint)other;
				return this.LongCompare(low, high);
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x000236E4 File Offset: 0x000218E4
		private int LongCompare(uint low, uint high)
		{
			uint num = 0U;
			if (this._data.Length > 1)
			{
				num = this._data[1];
			}
			if (num > high)
			{
				return 1;
			}
			if (num < high)
			{
				return -1;
			}
			uint num2 = this._data[0];
			if (num2 > low)
			{
				return 1;
			}
			if (num2 < low)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002372C File Offset: 0x0002192C
		public int CompareTo(long other)
		{
			int sign = (int)this._sign;
			int num = Math.Sign(other);
			if (sign != num)
			{
				if (sign <= num)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				if (sign == 0)
				{
					return 0;
				}
				if (this._data.Length > 2)
				{
					return (int)this._sign;
				}
				if (other < 0L)
				{
					other = -other;
				}
				uint low = (uint)other;
				uint high = (uint)((ulong)other >> 32);
				int num2 = this.LongCompare(low, high);
				if (sign == -1)
				{
					num2 = -num2;
				}
				return num2;
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00023794 File Offset: 0x00021994
		public static int Compare(BigInteger left, BigInteger right)
		{
			int sign = (int)left._sign;
			int sign2 = (int)right._sign;
			if (sign == sign2)
			{
				int num = BigInteger.CoreCompare(left._data, right._data);
				if (sign < 0)
				{
					num = -num;
				}
				return num;
			}
			if (sign <= sign2)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000237D5 File Offset: 0x000219D5
		private static int TopByte(uint x)
		{
			if ((x & 4294901760U) != 0U)
			{
				if ((x & 4278190080U) != 0U)
				{
					return 4;
				}
				return 3;
			}
			else
			{
				if ((x & 65280U) != 0U)
				{
					return 2;
				}
				return 1;
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000237F9 File Offset: 0x000219F9
		private static int FirstNonFfByte(uint word)
		{
			if ((word & 4278190080U) != 4278190080U)
			{
				return 4;
			}
			if ((word & 16711680U) != 16711680U)
			{
				return 3;
			}
			if ((word & 65280U) != 65280U)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002382C File Offset: 0x00021A2C
		public byte[] ToByteArray()
		{
			if (this._sign == 0)
			{
				return new byte[1];
			}
			int num = (this._data.Length - 1) * 4;
			bool flag = false;
			uint num2 = this._data[this._data.Length - 1];
			int num3;
			if (this._sign == 1)
			{
				num3 = BigInteger.TopByte(num2);
				uint num4 = 128U << (num3 - 1) * 8;
				if ((num2 & num4) != 0U)
				{
					flag = true;
				}
			}
			else
			{
				num3 = BigInteger.TopByte(num2);
			}
			byte[] array = new byte[num + num3 + (flag ? 1 : 0)];
			if (this._sign == 1)
			{
				int num5 = 0;
				int num6 = this._data.Length - 1;
				for (int i = 0; i < num6; i++)
				{
					uint num7 = this._data[i];
					array[num5++] = (byte)num7;
					array[num5++] = (byte)(num7 >> 8);
					array[num5++] = (byte)(num7 >> 16);
					array[num5++] = (byte)(num7 >> 24);
				}
				while (num3-- > 0)
				{
					array[num5++] = (byte)num2;
					num2 >>= 8;
				}
			}
			else
			{
				int num8 = 0;
				int num9 = this._data.Length - 1;
				uint num10 = 1U;
				uint num11;
				for (int j = 0; j < num9; j++)
				{
					num11 = this._data[j];
					ulong num12 = (ulong)(~(ulong)num11) + (ulong)num10;
					num11 = (uint)num12;
					num10 = (uint)(num12 >> 32);
					array[num8++] = (byte)num11;
					array[num8++] = (byte)(num11 >> 8);
					array[num8++] = (byte)(num11 >> 16);
					array[num8++] = (byte)(num11 >> 24);
				}
				ulong num13 = (ulong)(~(ulong)num2) + (ulong)num10;
				num11 = (uint)num13;
				if ((uint)(num13 >> 32) == 0U)
				{
					int num14 = BigInteger.FirstNonFfByte(num11);
					bool flag2 = ((ulong)num11 & (ulong)(1L << (num14 * 8 - 1 & 31))) == 0UL;
					int num15 = num14 + (flag2 ? 1 : 0);
					if (num15 != num3)
					{
						Array.Resize<byte>(ref array, num + num15);
					}
					while (num14-- > 0)
					{
						array[num8++] = (byte)num11;
						num11 >>= 8;
					}
					if (flag2)
					{
						array[num8++] = byte.MaxValue;
					}
				}
				else
				{
					Array.Resize<byte>(ref array, num + 5);
					array[num8++] = (byte)num11;
					array[num8++] = (byte)(num11 >> 8);
					array[num8++] = (byte)(num11 >> 16);
					array[num8++] = (byte)(num11 >> 24);
					array[num8++] = byte.MaxValue;
				}
			}
			return array;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00023A98 File Offset: 0x00021C98
		private static uint[] CoreAdd(uint[] a, uint[] b)
		{
			if (a.Length < b.Length)
			{
				uint[] array = a;
				a = b;
				b = array;
			}
			int num = a.Length;
			int num2 = b.Length;
			uint[] array2 = new uint[num];
			ulong num3 = 0UL;
			int i;
			for (i = 0; i < num2; i++)
			{
				num3 = num3 + (ulong)a[i] + (ulong)b[i];
				array2[i] = (uint)num3;
				num3 >>= 32;
			}
			while (i < num)
			{
				num3 += (ulong)a[i];
				array2[i] = (uint)num3;
				num3 >>= 32;
				i++;
			}
			if (num3 != 0UL)
			{
				Array.Resize<uint>(ref array2, num + 1);
				array2[i] = (uint)num3;
			}
			return array2;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00023B24 File Offset: 0x00021D24
		private static uint[] CoreSub(uint[] a, uint[] b)
		{
			int num = a.Length;
			int num2 = b.Length;
			uint[] array = new uint[num];
			ulong num3 = 0UL;
			int i;
			for (i = 0; i < num2; i++)
			{
				num3 = (ulong)a[i] - (ulong)b[i] - num3;
				array[i] = (uint)num3;
				num3 = (num3 >> 32 & 1UL);
			}
			while (i < num)
			{
				num3 = (ulong)a[i] - num3;
				array[i] = (uint)num3;
				num3 = (num3 >> 32 & 1UL);
				i++;
			}
			i = num - 1;
			while (i >= 0 && array[i] == 0U)
			{
				i--;
			}
			if (i < num - 1)
			{
				Array.Resize<uint>(ref array, i + 1);
			}
			return array;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00023BC0 File Offset: 0x00021DC0
		private static uint[] CoreAdd(uint[] a, uint b)
		{
			int num = a.Length;
			uint[] array = new uint[num];
			ulong num2 = (ulong)b;
			int i;
			for (i = 0; i < num; i++)
			{
				num2 += (ulong)a[i];
				array[i] = (uint)num2;
				num2 >>= 32;
			}
			if (num2 != 0UL)
			{
				Array.Resize<uint>(ref array, num + 1);
				array[i] = (uint)num2;
			}
			return array;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00023C0C File Offset: 0x00021E0C
		private static uint[] CoreSub(uint[] a, uint b)
		{
			int num = a.Length;
			uint[] array = new uint[num];
			ulong num2 = (ulong)b;
			int i;
			for (i = 0; i < num; i++)
			{
				num2 = (ulong)a[i] - num2;
				array[i] = (uint)num2;
				num2 = (num2 >> 32 & 1UL);
			}
			i = num - 1;
			while (i >= 0 && array[i] == 0U)
			{
				i--;
			}
			if (i < num - 1)
			{
				Array.Resize<uint>(ref array, i + 1);
			}
			return array;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00023C6C File Offset: 0x00021E6C
		private static int CoreCompare(uint[] a, uint[] b)
		{
			int num = (a != null) ? a.Length : 0;
			int num2 = (b != null) ? b.Length : 0;
			if (num > num2)
			{
				return 1;
			}
			if (num2 > num)
			{
				return -1;
			}
			for (int i = num - 1; i >= 0; i--)
			{
				uint num3 = a[i];
				uint num4 = b[i];
				if (num3 > num4)
				{
					return 1;
				}
				if (num3 < num4)
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00023CC0 File Offset: 0x00021EC0
		private static int GetNormalizeShift(uint value)
		{
			int num = 0;
			if ((value & 4294901760U) == 0U)
			{
				value <<= 16;
				num += 16;
			}
			if ((value & 4278190080U) == 0U)
			{
				value <<= 8;
				num += 8;
			}
			if ((value & 4026531840U) == 0U)
			{
				value <<= 4;
				num += 4;
			}
			if ((value & 3221225472U) == 0U)
			{
				value <<= 2;
				num += 2;
			}
			if ((value & 2147483648U) == 0U)
			{
				value <<= 1;
				num++;
			}
			return num;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00023D2C File Offset: 0x00021F2C
		private static void Normalize(uint[] u, int l, uint[] un, int shift)
		{
			uint num = 0U;
			int i;
			if (shift > 0)
			{
				int num2 = 32 - shift;
				for (i = 0; i < l; i++)
				{
					uint num3 = u[i];
					un[i] = (num3 << shift | num);
					num = num3 >> num2;
				}
			}
			else
			{
				for (i = 0; i < l; i++)
				{
					un[i] = u[i];
				}
			}
			while (i < un.Length)
			{
				un[i++] = 0U;
			}
			if (num != 0U)
			{
				un[l] = num;
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00023D94 File Offset: 0x00021F94
		private static void Unnormalize(uint[] un, out uint[] r, int shift)
		{
			int num = un.Length;
			r = new uint[num];
			if (shift > 0)
			{
				int num2 = 32 - shift;
				uint num3 = 0U;
				for (int i = num - 1; i >= 0; i--)
				{
					uint num4 = un[i];
					r[i] = (num4 >> shift | num3);
					num3 = num4 << num2;
				}
				return;
			}
			for (int j = 0; j < num; j++)
			{
				r[j] = un[j];
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00023DFC File Offset: 0x00021FFC
		private static void DivModUnsigned(uint[] u, uint[] v, out uint[] q, out uint[] r)
		{
			int num = u.Length;
			int num2 = v.Length;
			if (num2 <= 1)
			{
				ulong num3 = 0UL;
				uint num4 = v[0];
				q = new uint[num];
				r = new uint[1];
				for (int i = num - 1; i >= 0; i--)
				{
					num3 *= 4294967296UL;
					num3 += (ulong)u[i];
					ulong num5 = num3 / (ulong)num4;
					num3 -= num5 * (ulong)num4;
					q[i] = (uint)num5;
				}
				r[0] = (uint)num3;
				return;
			}
			if (num >= num2)
			{
				int normalizeShift = BigInteger.GetNormalizeShift(v[num2 - 1]);
				uint[] array = new uint[num + 1];
				uint[] array2 = new uint[num2];
				BigInteger.Normalize(u, num, array, normalizeShift);
				BigInteger.Normalize(v, num2, array2, normalizeShift);
				q = new uint[num - num2 + 1];
				r = null;
				for (int j = num - num2; j >= 0; j--)
				{
					ulong num6 = 4294967296UL * (ulong)array[j + num2] + (ulong)array[j + num2 - 1];
					ulong num7 = num6 / (ulong)array2[num2 - 1];
					num6 -= num7 * (ulong)array2[num2 - 1];
					while (num7 >= 4294967296UL || num7 * (ulong)array2[num2 - 2] > num6 * 4294967296UL + (ulong)array[j + num2 - 2])
					{
						num7 -= 1UL;
						num6 += (ulong)array2[num2 - 1];
						if (num6 >= 4294967296UL)
						{
							break;
						}
					}
					long num8 = 0L;
					long num10;
					for (int k = 0; k < num2; k++)
					{
						ulong num9 = (ulong)array2[k] * num7;
						num10 = (long)((ulong)array[k + j] - (ulong)((uint)num9) - (ulong)num8);
						array[k + j] = (uint)num10;
						num9 >>= 32;
						num10 >>= 32;
						num8 = (long)(num9 - (ulong)num10);
					}
					num10 = (long)((ulong)array[j + num2] - (ulong)num8);
					array[j + num2] = (uint)num10;
					q[j] = (uint)num7;
					if (num10 < 0L)
					{
						q[j] -= 1U;
						ulong num11 = 0UL;
						for (int k = 0; k < num2; k++)
						{
							num11 = (ulong)array2[k] + (ulong)array[j + k] + num11;
							array[j + k] = (uint)num11;
							num11 >>= 32;
						}
						num11 += (ulong)array[j + num2];
						array[j + num2] = (uint)num11;
					}
				}
				BigInteger.Unnormalize(array, out r, normalizeShift);
				return;
			}
			q = new uint[1];
			r = u;
		}

		// Token: 0x040003DA RID: 986
		private static readonly BigInteger ZeroSingleton = new BigInteger(0);

		// Token: 0x040003DB RID: 987
		private static readonly BigInteger OneSingleton = new BigInteger(1);

		// Token: 0x040003DC RID: 988
		private static readonly BigInteger MinusOneSingleton = new BigInteger(-1);

		// Token: 0x040003DD RID: 989
		private const ulong Base = 4294967296UL;

		// Token: 0x040003DE RID: 990
		private const int Bias = 1075;

		// Token: 0x040003DF RID: 991
		private const int DecimalSignMask = -2147483648;

		// Token: 0x040003E0 RID: 992
		private readonly uint[] _data;

		// Token: 0x040003E1 RID: 993
		private readonly short _sign;
	}
}
