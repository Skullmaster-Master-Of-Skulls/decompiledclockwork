using System;
using System.Data.Common;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x0200015A RID: 346
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlDecimal : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x060014C7 RID: 5319 RVA: 0x0009EB64 File Offset: 0x0009DF64
		private byte CalculatePrecision()
		{
			int num;
			uint[] array;
			uint num2;
			if (this.m_data4 != 0U)
			{
				num = 33;
				array = SqlDecimal.DecimalHelpersHiHi;
				num2 = this.m_data4;
			}
			else if (this.m_data3 != 0U)
			{
				num = 24;
				array = SqlDecimal.DecimalHelpersHi;
				num2 = this.m_data3;
			}
			else if (this.m_data2 != 0U)
			{
				num = 15;
				array = SqlDecimal.DecimalHelpersMid;
				num2 = this.m_data2;
			}
			else
			{
				num = 5;
				array = SqlDecimal.DecimalHelpersLo;
				num2 = this.m_data1;
			}
			if (num2 < array[num])
			{
				num -= 2;
				if (num2 < array[num])
				{
					num -= 2;
					if (num2 < array[num])
					{
						num--;
					}
					else
					{
						num++;
					}
				}
				else
				{
					num++;
				}
			}
			else
			{
				num += 2;
				if (num2 < array[num])
				{
					num--;
				}
				else
				{
					num++;
				}
			}
			if (num2 >= array[num])
			{
				num++;
				if (num == 37 && num2 >= array[num])
				{
					num++;
				}
			}
			byte b = (byte)(num + 1);
			if (b > 1 && this.VerifyPrecision(b - 1))
			{
				b -= 1;
			}
			return Math.Max(b, this.m_bScale);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0009EC50 File Offset: 0x0009E050
		private bool VerifyPrecision(byte precision)
		{
			int num = (int)(checked(precision - 1));
			if (this.m_data4 < SqlDecimal.DecimalHelpersHiHi[num])
			{
				return true;
			}
			if (this.m_data4 == SqlDecimal.DecimalHelpersHiHi[num])
			{
				if (this.m_data3 < SqlDecimal.DecimalHelpersHi[num])
				{
					return true;
				}
				if (this.m_data3 == SqlDecimal.DecimalHelpersHi[num])
				{
					if (this.m_data2 < SqlDecimal.DecimalHelpersMid[num])
					{
						return true;
					}
					if (this.m_data2 == SqlDecimal.DecimalHelpersMid[num] && this.m_data1 < SqlDecimal.DecimalHelpersLo[num])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0009ECD4 File Offset: 0x0009E0D4
		private SqlDecimal(bool fNull)
		{
			this.m_bLen = (this.m_bPrec = (this.m_bScale = 0));
			this.m_bStatus = 0;
			this.m_data1 = (this.m_data2 = (this.m_data3 = (this.m_data4 = 0U)));
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0009ED24 File Offset: 0x0009E124
		public SqlDecimal(decimal value)
		{
			this.m_bStatus = 1;
			int[] bits = decimal.GetBits(value);
			uint num = (uint)bits[3];
			this.m_data1 = (uint)bits[0];
			this.m_data2 = (uint)bits[1];
			this.m_data3 = (uint)bits[2];
			this.m_data4 = 0U;
			this.m_bStatus |= (((num & 2147483648U) == 2147483648U) ? 2 : 0);
			if (this.m_data3 != 0U)
			{
				this.m_bLen = 3;
			}
			else if (this.m_data2 != 0U)
			{
				this.m_bLen = 2;
			}
			else
			{
				this.m_bLen = 1;
			}
			this.m_bScale = (byte)((int)(num & 16711680U) >> 16);
			this.m_bPrec = 0;
			this.m_bPrec = this.CalculatePrecision();
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0009EDD4 File Offset: 0x0009E1D4
		public SqlDecimal(int value)
		{
			this.m_bStatus = 1;
			uint data = (uint)value;
			if (value < 0)
			{
				this.m_bStatus |= 2;
				if (value != -2147483648)
				{
					data = (uint)(-(uint)value);
				}
			}
			this.m_data1 = data;
			this.m_data2 = (this.m_data3 = (this.m_data4 = 0U));
			this.m_bLen = 1;
			this.m_bPrec = SqlDecimal.BGetPrecUI4(this.m_data1);
			this.m_bScale = 0;
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0009EE48 File Offset: 0x0009E248
		public SqlDecimal(long value)
		{
			this.m_bStatus = 1;
			ulong num = (ulong)value;
			if (value < 0L)
			{
				this.m_bStatus |= 2;
				if (value != -9223372036854775808L)
				{
					num = (ulong)(-(ulong)value);
				}
			}
			this.m_data1 = (uint)num;
			this.m_data2 = (uint)(num >> 32);
			this.m_data3 = (this.m_data4 = 0U);
			this.m_bLen = ((this.m_data2 == 0U) ? 1 : 2);
			this.m_bPrec = SqlDecimal.BGetPrecUI8(num);
			this.m_bScale = 0;
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0009EECC File Offset: 0x0009E2CC
		public SqlDecimal(byte bPrecision, byte bScale, bool fPositive, int[] bits)
		{
			SqlDecimal.CheckValidPrecScale(bPrecision, bScale);
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			if (bits.Length != 4)
			{
				throw new ArgumentException(SQLResource.InvalidArraySizeMessage, "bits");
			}
			this.m_bPrec = bPrecision;
			this.m_bScale = bScale;
			this.m_data1 = (uint)bits[0];
			this.m_data2 = (uint)bits[1];
			this.m_data3 = (uint)bits[2];
			this.m_data4 = (uint)bits[3];
			this.m_bLen = 1;
			for (int i = 3; i >= 0; i--)
			{
				if (bits[i] != 0)
				{
					this.m_bLen = (byte)(i + 1);
					break;
				}
			}
			this.m_bStatus = 1;
			if (!fPositive)
			{
				this.m_bStatus |= 2;
			}
			if (this.FZero())
			{
				this.SetPositive();
			}
			if (bPrecision < this.CalculatePrecision())
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0009EF9C File Offset: 0x0009E39C
		public SqlDecimal(byte bPrecision, byte bScale, bool fPositive, int data1, int data2, int data3, int data4)
		{
			SqlDecimal.CheckValidPrecScale(bPrecision, bScale);
			this.m_bPrec = bPrecision;
			this.m_bScale = bScale;
			this.m_data1 = (uint)data1;
			this.m_data2 = (uint)data2;
			this.m_data3 = (uint)data3;
			this.m_data4 = (uint)data4;
			this.m_bLen = 1;
			if (data4 == 0)
			{
				if (data3 == 0)
				{
					if (data2 == 0)
					{
						this.m_bLen = 1;
					}
					else
					{
						this.m_bLen = 2;
					}
				}
				else
				{
					this.m_bLen = 3;
				}
			}
			else
			{
				this.m_bLen = 4;
			}
			this.m_bStatus = 1;
			if (!fPositive)
			{
				this.m_bStatus |= 2;
			}
			if (this.FZero())
			{
				this.SetPositive();
			}
			if (bPrecision < this.CalculatePrecision())
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0009F050 File Offset: 0x0009E450
		public SqlDecimal(double dVal)
		{
			this = new SqlDecimal(false);
			this.m_bStatus = 1;
			if (dVal < 0.0)
			{
				dVal = -dVal;
				this.m_bStatus |= 2;
			}
			if (dVal >= 1E+38)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			double num = Math.Floor(dVal);
			double num2 = dVal - num;
			this.m_bPrec = 38;
			this.m_bLen = 1;
			if (num > 0.0)
			{
				dVal = Math.Floor(num / 4294967296.0);
				this.m_data1 = (uint)(num - dVal * 4294967296.0);
				num = dVal;
				if (num > 0.0)
				{
					dVal = Math.Floor(num / 4294967296.0);
					this.m_data2 = (uint)(num - dVal * 4294967296.0);
					num = dVal;
					this.m_bLen += 1;
					if (num > 0.0)
					{
						dVal = Math.Floor(num / 4294967296.0);
						this.m_data3 = (uint)(num - dVal * 4294967296.0);
						num = dVal;
						this.m_bLen += 1;
						if (num > 0.0)
						{
							dVal = Math.Floor(num / 4294967296.0);
							this.m_data4 = (uint)(num - dVal * 4294967296.0);
							this.m_bLen += 1;
						}
					}
				}
			}
			uint num3 = (uint)(this.FZero() ? 0 : this.CalculatePrecision());
			if (num3 > 17U)
			{
				uint num4 = num3 - 17U;
				uint num5;
				do
				{
					num5 = this.DivByULong(10U);
					num4 -= 1U;
				}
				while (num4 > 0U);
				num4 = num3 - 17U;
				if (num5 >= 5U)
				{
					this.AddULong(1U);
					num3 = (uint)this.CalculatePrecision() + num4;
				}
				do
				{
					this.MultByULong(10U);
					num4 -= 1U;
				}
				while (num4 > 0U);
			}
			this.m_bScale = (byte)((num3 < 17U) ? (17U - num3) : 0U);
			this.m_bPrec = (byte)(num3 + (uint)this.m_bScale);
			if (this.m_bScale > 0)
			{
				num3 = (uint)this.m_bScale;
				do
				{
					uint num6 = (num3 >= 9U) ? 9U : num3;
					num2 *= SqlDecimal.x_rgulShiftBase[(int)(num6 - 1U)];
					num3 -= num6;
					this.MultByULong(SqlDecimal.x_rgulShiftBase[(int)(num6 - 1U)]);
					this.AddULong((uint)num2);
					num2 -= Math.Floor(num2);
				}
				while (num3 > 0U);
			}
			if (num2 >= 0.5)
			{
				this.AddULong(1U);
			}
			if (this.FZero())
			{
				this.SetPositive();
			}
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0009F2B4 File Offset: 0x0009E6B4
		private SqlDecimal(uint[] rglData, byte bLen, byte bPrec, byte bScale, bool fPositive)
		{
			SqlDecimal.CheckValidPrecScale(bPrec, bScale);
			this.m_bLen = bLen;
			this.m_bPrec = bPrec;
			this.m_bScale = bScale;
			this.m_data1 = rglData[0];
			this.m_data2 = rglData[1];
			this.m_data3 = rglData[2];
			this.m_data4 = rglData[3];
			this.m_bStatus = 1;
			if (!fPositive)
			{
				this.m_bStatus |= 2;
			}
			if (this.FZero())
			{
				this.SetPositive();
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x0009F32C File Offset: 0x0009E72C
		public bool IsNull
		{
			get
			{
				return (this.m_bStatus & 1) == 0;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x0009F344 File Offset: 0x0009E744
		public decimal Value
		{
			get
			{
				return this.ToDecimal();
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x0009F358 File Offset: 0x0009E758
		public bool IsPositive
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return (this.m_bStatus & 2) == 0;
			}
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0009F380 File Offset: 0x0009E780
		private void SetPositive()
		{
			this.m_bStatus &= 253;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0009F3A0 File Offset: 0x0009E7A0
		private void SetSignBit(bool fPositive)
		{
			this.m_bStatus = (fPositive ? (this.m_bStatus & 253) : (this.m_bStatus | 2));
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x0009F3D0 File Offset: 0x0009E7D0
		public byte Precision
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return this.m_bPrec;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x0009F3F4 File Offset: 0x0009E7F4
		public byte Scale
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return this.m_bScale;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x0009F418 File Offset: 0x0009E818
		public int[] Data
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return new int[]
				{
					(int)this.m_data1,
					(int)this.m_data2,
					(int)this.m_data3,
					(int)this.m_data4
				};
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0009F460 File Offset: 0x0009E860
		public byte[] BinData
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				int num = (int)this.m_data1;
				int num2 = (int)this.m_data2;
				int num3 = (int)this.m_data3;
				int num4 = (int)this.m_data4;
				byte[] array = new byte[16];
				array[0] = (byte)(num & 255);
				num >>= 8;
				array[1] = (byte)(num & 255);
				num >>= 8;
				array[2] = (byte)(num & 255);
				num >>= 8;
				array[3] = (byte)(num & 255);
				array[4] = (byte)(num2 & 255);
				num2 >>= 8;
				array[5] = (byte)(num2 & 255);
				num2 >>= 8;
				array[6] = (byte)(num2 & 255);
				num2 >>= 8;
				array[7] = (byte)(num2 & 255);
				array[8] = (byte)(num3 & 255);
				num3 >>= 8;
				array[9] = (byte)(num3 & 255);
				num3 >>= 8;
				array[10] = (byte)(num3 & 255);
				num3 >>= 8;
				array[11] = (byte)(num3 & 255);
				array[12] = (byte)(num4 & 255);
				num4 >>= 8;
				array[13] = (byte)(num4 & 255);
				num4 >>= 8;
				array[14] = (byte)(num4 & 255);
				num4 >>= 8;
				array[15] = (byte)(num4 & 255);
				return array;
			}
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0009F594 File Offset: 0x0009E994
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			int bLen = (int)this.m_bLen;
			char[] array2 = new char[39];
			int i = 0;
			while (bLen > 1 || array[0] != 0U)
			{
				uint uiDigit;
				SqlDecimal.MpDiv1(array, ref bLen, 10U, out uiDigit);
				array2[i++] = SqlDecimal.ChFromDigit(uiDigit);
			}
			while (i <= (int)this.m_bScale)
			{
				array2[i++] = SqlDecimal.ChFromDigit(0U);
			}
			int num = 0;
			int num2 = 0;
			if (this.m_bScale > 0)
			{
				num = 1;
			}
			char[] array3;
			if (this.IsPositive)
			{
				array3 = new char[num + i];
			}
			else
			{
				array3 = new char[num + i + 1];
				array3[num2++] = '-';
			}
			while (i > 0)
			{
				if (i-- == (int)this.m_bScale)
				{
					array3[num2++] = '.';
				}
				array3[num2++] = array2[i];
			}
			return new string(array3);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0009F694 File Offset: 0x0009EA94
		public static SqlDecimal Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s == SQLResource.NullString)
			{
				return SqlDecimal.Null;
			}
			SqlDecimal @null = SqlDecimal.Null;
			char[] array = s.ToCharArray();
			int num = array.Length;
			int num2 = -1;
			int num3 = 0;
			@null.m_bPrec = 1;
			@null.m_bScale = 0;
			@null.SetToZero();
			while (num != 0 && array[num - 1] == ' ')
			{
				num--;
			}
			if (num == 0)
			{
				throw new FormatException(SQLResource.FormatMessage);
			}
			while (array[num3] == ' ')
			{
				num3++;
				num--;
			}
			if (array[num3] == '-')
			{
				@null.SetSignBit(false);
				num3++;
				num--;
			}
			else
			{
				@null.SetSignBit(true);
				if (array[num3] == '+')
				{
					num3++;
					num--;
				}
			}
			while (num > 2 && array[num3] == '0')
			{
				num3++;
				num--;
			}
			if (2 == num && '0' == array[num3] && '.' == array[num3 + 1])
			{
				array[num3] = '.';
				array[num3 + 1] = '0';
			}
			if (num == 0 || num > 39)
			{
				throw new FormatException(SQLResource.FormatMessage);
			}
			while (num > 1 && array[num3] == '0')
			{
				num3++;
				num--;
			}
			int i;
			for (i = 0; i < num; i++)
			{
				char c = array[num3];
				num3++;
				if (c >= '0' && c <= '9')
				{
					c -= '0';
					@null.MultByULong(10U);
					@null.AddULong((uint)c);
				}
				else
				{
					if (c != '.' || num2 >= 0)
					{
						throw new FormatException(SQLResource.FormatMessage);
					}
					num2 = i;
				}
			}
			if (num2 < 0)
			{
				@null.m_bPrec = (byte)i;
				@null.m_bScale = 0;
			}
			else
			{
				@null.m_bPrec = (byte)(i - 1);
				@null.m_bScale = (byte)((int)@null.m_bPrec - num2);
			}
			if (@null.m_bPrec > 38)
			{
				throw new FormatException(SQLResource.FormatMessage);
			}
			if (@null.m_bPrec == 0)
			{
				throw new FormatException(SQLResource.FormatMessage);
			}
			if (@null.FZero())
			{
				@null.SetPositive();
			}
			return @null;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0009F874 File Offset: 0x0009EC74
		public double ToDouble()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			double num = this.m_data4;
			num = num * 4294967296.0 + this.m_data3;
			num = num * 4294967296.0 + this.m_data2;
			num = num * 4294967296.0 + this.m_data1;
			num /= Math.Pow(10.0, (double)this.m_bScale);
			if (!this.IsPositive)
			{
				return -num;
			}
			return num;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0009F908 File Offset: 0x0009ED08
		private decimal ToDecimal()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			if (this.m_data4 != 0U || this.m_bScale > 28)
			{
				throw new OverflowException(SQLResource.ConversionOverflowMessage);
			}
			return new decimal((int)this.m_data1, (int)this.m_data2, (int)this.m_data3, !this.IsPositive, this.m_bScale);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0009F968 File Offset: 0x0009ED68
		public static implicit operator SqlDecimal(decimal x)
		{
			return new SqlDecimal(x);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0009F97C File Offset: 0x0009ED7C
		public static explicit operator SqlDecimal(double x)
		{
			return new SqlDecimal(x);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0009F990 File Offset: 0x0009ED90
		public static implicit operator SqlDecimal(long x)
		{
			return new SqlDecimal(new decimal(x));
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0009F9A8 File Offset: 0x0009EDA8
		public static explicit operator decimal(SqlDecimal x)
		{
			return x.Value;
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0009F9BC File Offset: 0x0009EDBC
		public static SqlDecimal operator -(SqlDecimal x)
		{
			if (x.IsNull)
			{
				return SqlDecimal.Null;
			}
			SqlDecimal result = x;
			if (result.FZero())
			{
				result.SetPositive();
			}
			else
			{
				result.SetSignBit(!result.IsPositive);
			}
			return result;
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0009FA00 File Offset: 0x0009EE00
		public static SqlDecimal operator +(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDecimal.Null;
			}
			bool flag = true;
			bool flag2 = x.IsPositive;
			bool flag3 = y.IsPositive;
			int bScale = (int)x.m_bScale;
			int bScale2 = (int)y.m_bScale;
			int num = Math.Max((int)x.m_bPrec - bScale, (int)y.m_bPrec - bScale2);
			int num2 = Math.Max(bScale, bScale2);
			int num3 = num + num2 + 1;
			num3 = Math.Min((int)SqlDecimal.MaxPrecision, num3);
			if (num3 - num < num2)
			{
				num2 = num3 - num;
			}
			if (bScale != num2)
			{
				x.AdjustScale(num2 - bScale, true);
			}
			if (bScale2 != num2)
			{
				y.AdjustScale(num2 - bScale2, true);
			}
			if (!flag2)
			{
				flag2 = !flag2;
				flag3 = !flag3;
				flag = !flag;
			}
			int num4 = (int)x.m_bLen;
			int bLen = (int)y.m_bLen;
			uint[] array = new uint[]
			{
				x.m_data1,
				x.m_data2,
				x.m_data3,
				x.m_data4
			};
			uint[] array2 = new uint[]
			{
				y.m_data1,
				y.m_data2,
				y.m_data3,
				y.m_data4
			};
			byte bLen2;
			if (flag3)
			{
				ulong num5 = 0UL;
				int num6 = 0;
				while (num6 < num4 || num6 < bLen)
				{
					if (num6 < num4)
					{
						num5 += (ulong)array[num6];
					}
					if (num6 < bLen)
					{
						num5 += (ulong)array2[num6];
					}
					array[num6] = (uint)num5;
					num5 >>= 32;
					num6++;
				}
				if (num5 != 0UL)
				{
					if (num6 == 4)
					{
						throw new OverflowException(SQLResource.ArithOverflowMessage);
					}
					array[num6] = (uint)num5;
					num6++;
				}
				bLen2 = (byte)num6;
			}
			else
			{
				int num7 = 0;
				if (x.LAbsCmp(y) < 0)
				{
					flag = !flag;
					uint[] array3 = array2;
					array2 = array;
					array = array3;
					num4 = bLen;
					bLen = (int)x.m_bLen;
				}
				ulong num5 = 4294967296UL;
				int num6 = 0;
				while (num6 < num4 || num6 < bLen)
				{
					if (num6 < num4)
					{
						num5 += (ulong)array[num6];
					}
					if (num6 < bLen)
					{
						num5 -= (ulong)array2[num6];
					}
					array[num6] = (uint)num5;
					if (array[num6] != 0U)
					{
						num7 = num6;
					}
					num5 >>= 32;
					num5 += (ulong)-1;
					num6++;
				}
				bLen2 = (byte)(num7 + 1);
			}
			SqlDecimal result = new SqlDecimal(array, bLen2, (byte)num3, (byte)num2, flag);
			if (result.FGt10_38() || result.CalculatePrecision() > 38)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (result.FZero())
			{
				result.SetPositive();
			}
			return result;
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0009FC54 File Offset: 0x0009F054
		public static SqlDecimal operator -(SqlDecimal x, SqlDecimal y)
		{
			return x + -y;
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0009FC70 File Offset: 0x0009F070
		public static SqlDecimal operator *(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDecimal.Null;
			}
			int bLen = (int)y.m_bLen;
			int num = (int)(x.m_bScale + y.m_bScale);
			int num2 = num;
			int num3 = (int)(x.m_bPrec - x.m_bScale + (y.m_bPrec - y.m_bScale) + 1);
			int num4 = num2 + num3;
			if (num4 > 38)
			{
				num4 = 38;
			}
			if (num2 > 38)
			{
				num2 = 38;
			}
			num2 = Math.Min(num4 - num3, num2);
			num2 = Math.Max(num2, Math.Min(num, 6));
			int num5 = num2 - num;
			bool fPositive = x.IsPositive == y.IsPositive;
			uint[] array = new uint[]
			{
				x.m_data1,
				x.m_data2,
				x.m_data3,
				x.m_data4
			};
			uint[] array2 = new uint[]
			{
				y.m_data1,
				y.m_data2,
				y.m_data3,
				y.m_data4
			};
			uint[] array3 = new uint[9];
			int i = 0;
			for (int j = 0; j < (int)x.m_bLen; j++)
			{
				uint num6 = array[j];
				ulong num7 = 0UL;
				i = j;
				for (int k = 0; k < bLen; k++)
				{
					ulong num8 = num7 + (ulong)array3[i];
					ulong num9 = (ulong)array2[k];
					num7 = (ulong)num6 * num9;
					num7 += num8;
					if (num7 < num8)
					{
						num8 = 4294967296UL;
					}
					else
					{
						num8 = 0UL;
					}
					array3[i++] = (uint)num7;
					num7 = (num7 >> 32) + num8;
				}
				if (num7 != 0UL)
				{
					array3[i++] = (uint)num7;
				}
			}
			while (array3[i] == 0U && i > 0)
			{
				i--;
			}
			int num10 = i + 1;
			if (num5 != 0)
			{
				if (num5 < 0)
				{
					uint num11;
					uint num12;
					do
					{
						if (num5 <= -9)
						{
							num11 = SqlDecimal.x_rgulShiftBase[8];
							num5 += 9;
						}
						else
						{
							num11 = SqlDecimal.x_rgulShiftBase[-num5 - 1];
							num5 = 0;
						}
						SqlDecimal.MpDiv1(array3, ref num10, num11, out num12);
					}
					while (num5 != 0);
					if (num10 > 4)
					{
						throw new OverflowException(SQLResource.ArithOverflowMessage);
					}
					for (i = num10; i < 4; i++)
					{
						array3[i] = 0U;
					}
					SqlDecimal result = new SqlDecimal(array3, (byte)num10, (byte)num4, (byte)num2, fPositive);
					if (result.FGt10_38())
					{
						throw new OverflowException(SQLResource.ArithOverflowMessage);
					}
					if (num12 >= num11 / 2U)
					{
						result.AddULong(1U);
					}
					if (result.FZero())
					{
						result.SetPositive();
					}
					return result;
				}
				else
				{
					if (num10 > 4)
					{
						throw new OverflowException(SQLResource.ArithOverflowMessage);
					}
					for (i = num10; i < 4; i++)
					{
						array3[i] = 0U;
					}
					SqlDecimal result = new SqlDecimal(array3, (byte)num10, (byte)num4, (byte)num, fPositive);
					if (result.FZero())
					{
						result.SetPositive();
					}
					result.AdjustScale(num5, true);
					return result;
				}
			}
			else
			{
				if (num10 > 4)
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
				for (i = num10; i < 4; i++)
				{
					array3[i] = 0U;
				}
				SqlDecimal result = new SqlDecimal(array3, (byte)num10, (byte)num4, (byte)num2, fPositive);
				if (result.FGt10_38())
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
				if (result.FZero())
				{
					result.SetPositive();
				}
				return result;
			}
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0009FF6C File Offset: 0x0009F36C
		public static SqlDecimal operator /(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDecimal.Null;
			}
			if (y.FZero())
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			bool fPositive = x.IsPositive == y.IsPositive;
			int bScale = (int)x.m_bScale;
			int bPrec = (int)x.m_bPrec;
			int num = Math.Max((int)(x.m_bScale + y.m_bPrec + 1), 6);
			int num2 = (int)(x.m_bPrec - x.m_bScale + y.m_bScale);
			int num3 = num + (int)x.m_bPrec + (int)y.m_bPrec + 1;
			int val = Math.Min(num, 6);
			num2 = Math.Min(num2, 38);
			num3 = num2 + num;
			if (num3 > 38)
			{
				num3 = 38;
			}
			num = Math.Min(num3 - num2, num);
			num = Math.Max(num, val);
			int digits = num - (int)x.m_bScale + (int)y.m_bScale;
			x.AdjustScale(digits, true);
			uint[] rgulU = new uint[]
			{
				x.m_data1,
				x.m_data2,
				x.m_data3,
				x.m_data4
			};
			uint[] rgulD = new uint[]
			{
				y.m_data1,
				y.m_data2,
				y.m_data3,
				y.m_data4
			};
			uint[] rgulR = new uint[5];
			uint[] array = new uint[4];
			int num4;
			int num5;
			SqlDecimal.MpDiv(rgulU, (int)x.m_bLen, rgulD, (int)y.m_bLen, array, out num4, rgulR, out num5);
			SqlDecimal.ZeroToMaxLen(array, num4);
			SqlDecimal result = new SqlDecimal(array, (byte)num4, (byte)num3, (byte)num, fPositive);
			if (result.FZero())
			{
				result.SetPositive();
			}
			return result;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x000A0100 File Offset: 0x0009F500
		public static explicit operator SqlDecimal(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal((int)x.ByteValue);
			}
			return SqlDecimal.Null;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x000A0128 File Offset: 0x0009F528
		public static implicit operator SqlDecimal(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal((int)x.Value);
			}
			return SqlDecimal.Null;
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x000A0150 File Offset: 0x0009F550
		public static implicit operator SqlDecimal(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal((int)x.Value);
			}
			return SqlDecimal.Null;
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x000A0178 File Offset: 0x0009F578
		public static implicit operator SqlDecimal(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal(x.Value);
			}
			return SqlDecimal.Null;
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x000A01A0 File Offset: 0x0009F5A0
		public static implicit operator SqlDecimal(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal(x.Value);
			}
			return SqlDecimal.Null;
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x000A01C8 File Offset: 0x0009F5C8
		public static implicit operator SqlDecimal(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal(x.ToDecimal());
			}
			return SqlDecimal.Null;
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x000A01F0 File Offset: 0x0009F5F0
		public static explicit operator SqlDecimal(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal((double)x.Value);
			}
			return SqlDecimal.Null;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x000A021C File Offset: 0x0009F61C
		public static explicit operator SqlDecimal(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal(x.Value);
			}
			return SqlDecimal.Null;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x000A0244 File Offset: 0x0009F644
		public static explicit operator SqlDecimal(SqlString x)
		{
			if (!x.IsNull)
			{
				return SqlDecimal.Parse(x.Value);
			}
			return SqlDecimal.Null;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x000A026C File Offset: 0x0009F66C
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			if (this.IsNull)
			{
				return;
			}
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			uint num = array[(int)(this.m_bLen - 1)];
			for (int i = (int)this.m_bLen; i < 4; i++)
			{
			}
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x000A02CC File Offset: 0x0009F6CC
		private static void ZeroToMaxLen(uint[] rgulData, int cUI4sCur)
		{
			switch (cUI4sCur)
			{
			case 1:
				rgulData[1] = (rgulData[2] = (rgulData[3] = 0U));
				return;
			case 2:
				rgulData[2] = (rgulData[3] = 0U);
				return;
			case 3:
				rgulData[3] = 0U;
				return;
			default:
				return;
			}
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x000A0310 File Offset: 0x0009F710
		private static byte CLenFromPrec(byte bPrec)
		{
			return SqlDecimal.rgCLenFromPrec[(int)(bPrec - 1)];
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x000A0328 File Offset: 0x0009F728
		private bool FZero()
		{
			return this.m_data1 == 0U && this.m_bLen <= 1;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x000A034C File Offset: 0x0009F74C
		private bool FGt10_38()
		{
			return (ulong)this.m_data4 >= 1262177448UL && this.m_bLen == 4 && ((ulong)this.m_data4 > 1262177448UL || (ulong)this.m_data3 > 1518781562UL || ((ulong)this.m_data3 == 1518781562UL && (ulong)this.m_data2 >= 160047680UL));
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x000A03B8 File Offset: 0x0009F7B8
		private bool FGt10_38(uint[] rglData)
		{
			return (ulong)rglData[3] >= 1262177448UL && ((ulong)rglData[3] > 1262177448UL || (ulong)rglData[2] > 1518781562UL || ((ulong)rglData[2] == 1518781562UL && (ulong)rglData[1] >= 160047680UL));
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x000A040C File Offset: 0x0009F80C
		private static byte BGetPrecUI4(uint value)
		{
			int num;
			if (value < 10000U)
			{
				if (value < 100U)
				{
					num = ((value >= 10U) ? 2 : 1);
				}
				else
				{
					num = ((value >= 1000U) ? 4 : 3);
				}
			}
			else if (value < 100000000U)
			{
				if (value < 1000000U)
				{
					num = ((value >= 100000U) ? 6 : 5);
				}
				else
				{
					num = ((value >= 10000000U) ? 8 : 7);
				}
			}
			else
			{
				num = ((value >= 1000000000U) ? 10 : 9);
			}
			return (byte)num;
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x000A0480 File Offset: 0x0009F880
		private static byte BGetPrecUI8(uint ulU0, uint ulU1)
		{
			ulong dwlVal = (ulong)ulU0 + ((ulong)ulU1 << 32);
			return SqlDecimal.BGetPrecUI8(dwlVal);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x000A049C File Offset: 0x0009F89C
		private static byte BGetPrecUI8(ulong dwlVal)
		{
			int num2;
			if (dwlVal < 100000000UL)
			{
				uint num = (uint)dwlVal;
				if (num < 10000U)
				{
					if (num < 100U)
					{
						num2 = ((num >= 10U) ? 2 : 1);
					}
					else
					{
						num2 = ((num >= 1000U) ? 4 : 3);
					}
				}
				else if (num < 1000000U)
				{
					num2 = ((num >= 100000U) ? 6 : 5);
				}
				else
				{
					num2 = ((num >= 10000000U) ? 8 : 7);
				}
			}
			else if (dwlVal < 10000000000000000UL)
			{
				if (dwlVal < 1000000000000UL)
				{
					if (dwlVal < 10000000000UL)
					{
						num2 = ((dwlVal >= 1000000000UL) ? 10 : 9);
					}
					else
					{
						num2 = ((dwlVal >= 100000000000UL) ? 12 : 11);
					}
				}
				else if (dwlVal < 100000000000000UL)
				{
					num2 = ((dwlVal >= 10000000000000UL) ? 14 : 13);
				}
				else
				{
					num2 = ((dwlVal >= 1000000000000000UL) ? 16 : 15);
				}
			}
			else if (dwlVal < 1000000000000000000UL)
			{
				num2 = ((dwlVal >= 100000000000000000UL) ? 18 : 17);
			}
			else
			{
				num2 = ((dwlVal >= 10000000000000000000UL) ? 20 : 19);
			}
			return (byte)num2;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x000A05CC File Offset: 0x0009F9CC
		private void AddULong(uint ulAdd)
		{
			ulong num = (ulong)ulAdd;
			int bLen = (int)this.m_bLen;
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			int num2 = 0;
			for (;;)
			{
				num += (ulong)array[num2];
				array[num2] = (uint)num;
				num >>= 32;
				if (num == 0UL)
				{
					break;
				}
				num2++;
				if (num2 >= bLen)
				{
					goto Block_2;
				}
			}
			this.StoreFromWorkingArray(array);
			return;
			Block_2:
			if (num2 == 4)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			array[num2] = (uint)num;
			this.m_bLen += 1;
			if (this.FGt10_38(array))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this.StoreFromWorkingArray(array);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x000A0674 File Offset: 0x0009FA74
		private void MultByULong(uint uiMultiplier)
		{
			int bLen = (int)this.m_bLen;
			ulong num = 0UL;
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			for (int i = 0; i < bLen; i++)
			{
				ulong num2 = (ulong)array[i];
				ulong num3 = num2 * (ulong)uiMultiplier;
				num += num3;
				if (num < num3)
				{
					num3 = 4294967296UL;
				}
				else
				{
					num3 = 0UL;
				}
				array[i] = (uint)num;
				num = (num >> 32) + num3;
			}
			if (num != 0UL)
			{
				if (bLen == 4)
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
				array[bLen] = (uint)num;
				this.m_bLen += 1;
			}
			if (this.FGt10_38(array))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this.StoreFromWorkingArray(array);
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x000A073C File Offset: 0x0009FB3C
		private uint DivByULong(uint iDivisor)
		{
			ulong num = (ulong)iDivisor;
			ulong num2 = 0UL;
			bool flag = true;
			if (num == 0UL)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			for (int i = (int)this.m_bLen; i > 0; i--)
			{
				num2 = (num2 << 32) + (ulong)array[i - 1];
				uint num3 = (uint)(num2 / num);
				array[i - 1] = num3;
				num2 %= num;
				if (flag && num3 == 0U)
				{
					this.m_bLen -= 1;
				}
				else
				{
					flag = false;
				}
			}
			this.StoreFromWorkingArray(array);
			if (flag)
			{
				this.m_bLen = 1;
			}
			return (uint)num2;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x000A07EC File Offset: 0x0009FBEC
		internal void AdjustScale(int digits, bool fRound)
		{
			bool flag = false;
			int i = digits;
			if (i + (int)this.m_bScale < 0)
			{
				throw new SqlTruncateException();
			}
			if (i + (int)this.m_bScale > 38)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			byte bScale = (byte)(i + (int)this.m_bScale);
			byte bPrec = (byte)Math.Min(38, Math.Max(1, i + (int)this.m_bPrec));
			if (i > 0)
			{
				this.m_bScale = bScale;
				this.m_bPrec = bPrec;
				while (i > 0)
				{
					uint num;
					if (i >= 9)
					{
						num = SqlDecimal.x_rgulShiftBase[8];
						i -= 9;
					}
					else
					{
						num = SqlDecimal.x_rgulShiftBase[i - 1];
						i = 0;
					}
					this.MultByULong(num);
				}
			}
			else if (i < 0)
			{
				uint num;
				uint num2;
				do
				{
					if (i <= -9)
					{
						num = SqlDecimal.x_rgulShiftBase[8];
						i += 9;
					}
					else
					{
						num = SqlDecimal.x_rgulShiftBase[-i - 1];
						i = 0;
					}
					num2 = this.DivByULong(num);
				}
				while (i < 0);
				flag = (num2 >= num / 2U);
				this.m_bScale = bScale;
				this.m_bPrec = bPrec;
			}
			if (flag && fRound)
			{
				this.AddULong(1U);
				return;
			}
			if (this.FZero())
			{
				this.SetPositive();
			}
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x000A08F0 File Offset: 0x0009FCF0
		public static SqlDecimal AdjustScale(SqlDecimal n, int digits, bool fRound)
		{
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			SqlDecimal result = n;
			result.AdjustScale(digits, fRound);
			return result;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x000A0918 File Offset: 0x0009FD18
		public static SqlDecimal ConvertToPrecScale(SqlDecimal n, int precision, int scale)
		{
			SqlDecimal.CheckValidPrecScale(precision, scale);
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			SqlDecimal sqlDecimal = n;
			int num = precision - (int)sqlDecimal.m_bPrec;
			int digits = scale - (int)sqlDecimal.m_bScale;
			sqlDecimal.AdjustScale(digits, true);
			byte b = SqlDecimal.CLenFromPrec((byte)precision);
			if (b < sqlDecimal.m_bLen)
			{
				throw new SqlTruncateException();
			}
			if (b == sqlDecimal.m_bLen && precision < (int)sqlDecimal.CalculatePrecision())
			{
				throw new SqlTruncateException();
			}
			sqlDecimal.m_bPrec = (byte)precision;
			return sqlDecimal;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x000A0994 File Offset: 0x0009FD94
		private int LAbsCmp(SqlDecimal snumOp)
		{
			int bLen = (int)snumOp.m_bLen;
			int bLen2 = (int)this.m_bLen;
			if (bLen != bLen2)
			{
				if (bLen2 <= bLen)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				uint[] array = new uint[]
				{
					this.m_data1,
					this.m_data2,
					this.m_data3,
					this.m_data4
				};
				uint[] array2 = new uint[]
				{
					snumOp.m_data1,
					snumOp.m_data2,
					snumOp.m_data3,
					snumOp.m_data4
				};
				int num = bLen - 1;
				while (array[num] == array2[num])
				{
					num--;
					if (num < 0)
					{
						return 0;
					}
				}
				if (array[num] <= array2[num])
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x000A0A38 File Offset: 0x0009FE38
		private static void MpMove(uint[] rgulS, int ciulS, uint[] rgulD, out int ciulD)
		{
			ciulD = ciulS;
			for (int i = 0; i < ciulS; i++)
			{
				rgulD[i] = rgulS[i];
			}
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x000A0A5C File Offset: 0x0009FE5C
		private static void MpSet(uint[] rgulD, out int ciulD, uint iulN)
		{
			ciulD = 1;
			rgulD[0] = iulN;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x000A0A70 File Offset: 0x0009FE70
		private static void MpNormalize(uint[] rgulU, ref int ciulU)
		{
			while (ciulU > 1 && rgulU[ciulU - 1] == 0U)
			{
				ciulU--;
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x000A0A94 File Offset: 0x0009FE94
		private static void MpMul1(uint[] piulD, ref int ciulD, uint iulX)
		{
			uint num = 0U;
			int i;
			for (i = 0; i < ciulD; i++)
			{
				ulong num2 = (ulong)piulD[i];
				ulong x = (ulong)num + num2 * (ulong)iulX;
				num = SqlDecimal.HI(x);
				piulD[i] = SqlDecimal.LO(x);
			}
			if (num != 0U)
			{
				piulD[i] = num;
				ciulD++;
			}
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x000A0ADC File Offset: 0x0009FEDC
		private static void MpDiv1(uint[] rgulU, ref int ciulU, uint iulD, out uint iulR)
		{
			uint num = 0U;
			ulong num2 = (ulong)iulD;
			int i = ciulU;
			while (i > 0)
			{
				i--;
				ulong num3 = ((ulong)num << 32) + (ulong)rgulU[i];
				rgulU[i] = (uint)(num3 / num2);
				num = (uint)(num3 - (ulong)rgulU[i] * num2);
			}
			iulR = num;
			SqlDecimal.MpNormalize(rgulU, ref ciulU);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x000A0B24 File Offset: 0x0009FF24
		internal static ulong DWL(uint lo, uint hi)
		{
			return (ulong)lo + ((ulong)hi << 32);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x000A0B3C File Offset: 0x0009FF3C
		private static uint HI(ulong x)
		{
			return (uint)(x >> 32);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x000A0B50 File Offset: 0x0009FF50
		private static uint LO(ulong x)
		{
			return (uint)x;
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x000A0B60 File Offset: 0x0009FF60
		private static void MpDiv(uint[] rgulU, int ciulU, uint[] rgulD, int ciulD, uint[] rgulQ, out int ciulQ, uint[] rgulR, out int ciulR)
		{
			if (ciulD == 1 && rgulD[0] == 0U)
			{
				ciulQ = (ciulR = 0);
				return;
			}
			if (ciulU == 1 && ciulD == 1)
			{
				SqlDecimal.MpSet(rgulQ, out ciulQ, rgulU[0] / rgulD[0]);
				SqlDecimal.MpSet(rgulR, out ciulR, rgulU[0] % rgulD[0]);
				return;
			}
			if (ciulD > ciulU)
			{
				SqlDecimal.MpMove(rgulU, ciulU, rgulR, out ciulR);
				SqlDecimal.MpSet(rgulQ, out ciulQ, 0U);
				return;
			}
			if (ciulU <= 2)
			{
				ulong num = SqlDecimal.DWL(rgulU[0], rgulU[1]);
				ulong num2 = (ulong)rgulD[0];
				if (ciulD > 1)
				{
					num2 += (ulong)rgulD[1] << 32;
				}
				ulong x = num / num2;
				rgulQ[0] = SqlDecimal.LO(x);
				rgulQ[1] = SqlDecimal.HI(x);
				ciulQ = ((SqlDecimal.HI(x) != 0U) ? 2 : 1);
				x = num % num2;
				rgulR[0] = SqlDecimal.LO(x);
				rgulR[1] = SqlDecimal.HI(x);
				ciulR = ((SqlDecimal.HI(x) != 0U) ? 2 : 1);
				return;
			}
			if (ciulD == 1)
			{
				SqlDecimal.MpMove(rgulU, ciulU, rgulQ, out ciulQ);
				uint num3;
				SqlDecimal.MpDiv1(rgulQ, ref ciulQ, rgulD[0], out num3);
				rgulR[0] = num3;
				ciulR = 1;
				return;
			}
			ciulQ = (ciulR = 0);
			if (rgulU != rgulR)
			{
				SqlDecimal.MpMove(rgulU, ciulU, rgulR, out ciulR);
			}
			ciulQ = ciulU - ciulD + 1;
			uint num4 = rgulD[ciulD - 1];
			rgulR[ciulU] = 0U;
			int num5 = ciulU;
			uint num6 = (uint)(4294967296UL / ((ulong)num4 + 1UL));
			if (num6 > 1U)
			{
				SqlDecimal.MpMul1(rgulD, ref ciulD, num6);
				num4 = rgulD[ciulD - 1];
				SqlDecimal.MpMul1(rgulR, ref ciulR, num6);
			}
			uint num7 = rgulD[ciulD - 2];
			do
			{
				ulong num8 = SqlDecimal.DWL(rgulR[num5 - 1], rgulR[num5]);
				uint num9;
				if (num4 == rgulR[num5])
				{
					num9 = uint.MaxValue;
				}
				else
				{
					num9 = (uint)(num8 / (ulong)num4);
				}
				ulong num10 = (ulong)num9;
				uint num11 = (uint)(num8 - num10 * (ulong)num4);
				while ((ulong)num7 * num10 > SqlDecimal.DWL(rgulR[num5 - 2], num11))
				{
					num9 -= 1U;
					if (num11 >= -num4)
					{
						break;
					}
					num11 += num4;
					num10 = (ulong)num9;
				}
				num8 = 4294967296UL;
				ulong num12 = 0UL;
				int i = 0;
				int num13 = num5 - ciulD;
				while (i < ciulD)
				{
					ulong num14 = (ulong)rgulD[i];
					num12 += (ulong)num9 * num14;
					num8 += (ulong)rgulR[num13] - (ulong)SqlDecimal.LO(num12);
					num12 = (ulong)SqlDecimal.HI(num12);
					rgulR[num13] = SqlDecimal.LO(num8);
					num8 = (ulong)SqlDecimal.HI(num8) + 4294967296UL - 1UL;
					i++;
					num13++;
				}
				num8 += (ulong)rgulR[num13] - num12;
				rgulR[num13] = SqlDecimal.LO(num8);
				rgulQ[num5 - ciulD] = num9;
				if (SqlDecimal.HI(num8) == 0U)
				{
					rgulQ[num5 - ciulD] = num9 - 1U;
					uint num15 = 0U;
					i = 0;
					num13 = num5 - ciulD;
					while (i < ciulD)
					{
						num8 = (ulong)rgulD[i] + (ulong)rgulR[num13] + (ulong)num15;
						num15 = SqlDecimal.HI(num8);
						rgulR[num13] = SqlDecimal.LO(num8);
						i++;
						num13++;
					}
					rgulR[num13] += num15;
				}
				num5--;
			}
			while (num5 >= ciulD);
			SqlDecimal.MpNormalize(rgulQ, ref ciulQ);
			ciulR = ciulD;
			SqlDecimal.MpNormalize(rgulR, ref ciulR);
			if (num6 > 1U)
			{
				uint num16;
				SqlDecimal.MpDiv1(rgulD, ref ciulD, num6, out num16);
				SqlDecimal.MpDiv1(rgulR, ref ciulR, num6, out num16);
			}
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x000A0E68 File Offset: 0x000A0268
		private EComparison CompareNm(SqlDecimal snumOp)
		{
			int num = this.IsPositive ? 1 : -1;
			int num2 = snumOp.IsPositive ? 1 : -1;
			if (num == num2)
			{
				SqlDecimal sqlDecimal = this;
				SqlDecimal snumOp2 = snumOp;
				int num3 = (int)(this.m_bScale - snumOp.m_bScale);
				if (num3 < 0)
				{
					try
					{
						sqlDecimal.AdjustScale(-num3, true);
						goto IL_79;
					}
					catch (OverflowException)
					{
						return (num > 0) ? EComparison.GT : EComparison.LT;
					}
				}
				if (num3 > 0)
				{
					try
					{
						snumOp2.AdjustScale(num3, true);
					}
					catch (OverflowException)
					{
						return (num > 0) ? EComparison.LT : EComparison.GT;
					}
				}
				IL_79:
				int num4 = sqlDecimal.LAbsCmp(snumOp2);
				if (num4 == 0)
				{
					return EComparison.EQ;
				}
				int num5 = num * num4;
				if (num5 < 0)
				{
					return EComparison.LT;
				}
				return EComparison.GT;
			}
			if (num != 1)
			{
				return EComparison.LT;
			}
			return EComparison.GT;
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x000A0F40 File Offset: 0x000A0340
		private static void CheckValidPrecScale(byte bPrec, byte bScale)
		{
			if (bPrec < 1 || bPrec > SqlDecimal.MaxPrecision || bScale < 0 || bScale > SqlDecimal.MaxScale || bScale > bPrec)
			{
				throw new SqlTypeException(SQLResource.InvalidPrecScaleMessage);
			}
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x000A0F74 File Offset: 0x000A0374
		private static void CheckValidPrecScale(int iPrec, int iScale)
		{
			if (iPrec < 1 || iPrec > (int)SqlDecimal.MaxPrecision || iScale < 0 || iScale > (int)SqlDecimal.MaxScale || iScale > iPrec)
			{
				throw new SqlTypeException(SQLResource.InvalidPrecScaleMessage);
			}
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x000A0FA8 File Offset: 0x000A03A8
		public static SqlBoolean operator ==(SqlDecimal x, SqlDecimal y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.CompareNm(y) == EComparison.EQ);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x000A0FE0 File Offset: 0x000A03E0
		public static SqlBoolean operator !=(SqlDecimal x, SqlDecimal y)
		{
			return !(x == y);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x000A0FFC File Offset: 0x000A03FC
		public static SqlBoolean operator <(SqlDecimal x, SqlDecimal y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.CompareNm(y) == EComparison.LT);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x000A1034 File Offset: 0x000A0434
		public static SqlBoolean operator >(SqlDecimal x, SqlDecimal y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.CompareNm(y) == EComparison.GT);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x000A106C File Offset: 0x000A046C
		public static SqlBoolean operator <=(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			EComparison ecomparison = x.CompareNm(y);
			return new SqlBoolean(ecomparison == EComparison.LT || ecomparison == EComparison.EQ);
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x000A10AC File Offset: 0x000A04AC
		public static SqlBoolean operator >=(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			EComparison ecomparison = x.CompareNm(y);
			return new SqlBoolean(ecomparison == EComparison.GT || ecomparison == EComparison.EQ);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x000A10EC File Offset: 0x000A04EC
		public static SqlDecimal Add(SqlDecimal x, SqlDecimal y)
		{
			return x + y;
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x000A1100 File Offset: 0x000A0500
		public static SqlDecimal Subtract(SqlDecimal x, SqlDecimal y)
		{
			return x - y;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x000A1114 File Offset: 0x000A0514
		public static SqlDecimal Multiply(SqlDecimal x, SqlDecimal y)
		{
			return x * y;
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x000A1128 File Offset: 0x000A0528
		public static SqlDecimal Divide(SqlDecimal x, SqlDecimal y)
		{
			return x / y;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x000A113C File Offset: 0x000A053C
		public static SqlBoolean Equals(SqlDecimal x, SqlDecimal y)
		{
			return x == y;
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x000A1150 File Offset: 0x000A0550
		public static SqlBoolean NotEquals(SqlDecimal x, SqlDecimal y)
		{
			return x != y;
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x000A1164 File Offset: 0x000A0564
		public static SqlBoolean LessThan(SqlDecimal x, SqlDecimal y)
		{
			return x < y;
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x000A1178 File Offset: 0x000A0578
		public static SqlBoolean GreaterThan(SqlDecimal x, SqlDecimal y)
		{
			return x > y;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x000A118C File Offset: 0x000A058C
		public static SqlBoolean LessThanOrEqual(SqlDecimal x, SqlDecimal y)
		{
			return x <= y;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x000A11A0 File Offset: 0x000A05A0
		public static SqlBoolean GreaterThanOrEqual(SqlDecimal x, SqlDecimal y)
		{
			return x >= y;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x000A11B4 File Offset: 0x000A05B4
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x000A11CC File Offset: 0x000A05CC
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x000A11E4 File Offset: 0x000A05E4
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x000A11FC File Offset: 0x000A05FC
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x000A1214 File Offset: 0x000A0614
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x000A122C File Offset: 0x000A062C
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x000A1244 File Offset: 0x000A0644
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x000A125C File Offset: 0x000A065C
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x000A1274 File Offset: 0x000A0674
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x000A128C File Offset: 0x000A068C
		private static char ChFromDigit(uint uiDigit)
		{
			return (char)(uiDigit + 48U);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x000A12A0 File Offset: 0x000A06A0
		private void StoreFromWorkingArray(uint[] rguiData)
		{
			this.m_data1 = rguiData[0];
			this.m_data2 = rguiData[1];
			this.m_data3 = rguiData[2];
			this.m_data4 = rguiData[3];
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x000A12D4 File Offset: 0x000A06D4
		private void SetToZero()
		{
			this.m_bLen = 1;
			this.m_data1 = (this.m_data2 = (this.m_data3 = (this.m_data4 = 0U)));
			this.m_bStatus = 1;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x000A1314 File Offset: 0x000A0714
		private void MakeInteger(out bool fFraction)
		{
			int i = (int)this.m_bScale;
			fFraction = false;
			while (i > 0)
			{
				uint num;
				if (i >= 9)
				{
					num = this.DivByULong(SqlDecimal.x_rgulShiftBase[8]);
					i -= 9;
				}
				else
				{
					num = this.DivByULong(SqlDecimal.x_rgulShiftBase[i - 1]);
					i = 0;
				}
				if (num != 0U)
				{
					fFraction = true;
				}
			}
			this.m_bScale = 0;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x000A136C File Offset: 0x000A076C
		public static SqlDecimal Abs(SqlDecimal n)
		{
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			n.SetPositive();
			return n;
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x000A1390 File Offset: 0x000A0790
		public static SqlDecimal Ceiling(SqlDecimal n)
		{
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			if (n.m_bScale == 0)
			{
				return n;
			}
			bool flag;
			n.MakeInteger(out flag);
			if (flag && n.IsPositive)
			{
				n.AddULong(1U);
			}
			if (n.FZero())
			{
				n.SetPositive();
			}
			return n;
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x000A13E4 File Offset: 0x000A07E4
		public static SqlDecimal Floor(SqlDecimal n)
		{
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			if (n.m_bScale == 0)
			{
				return n;
			}
			bool flag;
			n.MakeInteger(out flag);
			if (flag && !n.IsPositive)
			{
				n.AddULong(1U);
			}
			if (n.FZero())
			{
				n.SetPositive();
			}
			return n;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x000A1438 File Offset: 0x000A0838
		public static SqlInt32 Sign(SqlDecimal n)
		{
			if (n.IsNull)
			{
				return SqlInt32.Null;
			}
			if (n == new SqlDecimal(0))
			{
				return SqlInt32.Zero;
			}
			if (n.IsNull)
			{
				return SqlInt32.Null;
			}
			if (!n.IsPositive)
			{
				return new SqlInt32(-1);
			}
			return new SqlInt32(1);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x000A1494 File Offset: 0x000A0894
		private static SqlDecimal Round(SqlDecimal n, int lPosition, bool fTruncate)
		{
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			if (lPosition >= 0)
			{
				lPosition = Math.Min(38, lPosition);
				if (lPosition >= (int)n.m_bScale)
				{
					return n;
				}
			}
			else
			{
				lPosition = Math.Max(-38, lPosition);
				if (lPosition < (int)(n.m_bScale - n.m_bPrec))
				{
					n.SetToZero();
					return n;
				}
			}
			uint num = 0U;
			int i = Math.Abs(lPosition - (int)n.m_bScale);
			uint num2 = 1U;
			while (i > 0)
			{
				if (i >= 9)
				{
					num = n.DivByULong(SqlDecimal.x_rgulShiftBase[8]);
					num2 = SqlDecimal.x_rgulShiftBase[8];
					i -= 9;
				}
				else
				{
					num = n.DivByULong(SqlDecimal.x_rgulShiftBase[i - 1]);
					num2 = SqlDecimal.x_rgulShiftBase[i - 1];
					i = 0;
				}
			}
			if (num2 > 1U)
			{
				num /= num2 / 10U;
			}
			if (n.FZero() && (fTruncate || num < 5U))
			{
				n.SetPositive();
				return n;
			}
			if (num >= 5U && !fTruncate)
			{
				n.AddULong(1U);
			}
			i = Math.Abs(lPosition - (int)n.m_bScale);
			while (i-- > 0)
			{
				n.MultByULong(10U);
			}
			return n;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x000A159C File Offset: 0x000A099C
		public static SqlDecimal Round(SqlDecimal n, int position)
		{
			return SqlDecimal.Round(n, position, false);
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x000A15B4 File Offset: 0x000A09B4
		public static SqlDecimal Truncate(SqlDecimal n, int position)
		{
			return SqlDecimal.Round(n, position, true);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x000A15CC File Offset: 0x000A09CC
		public static SqlDecimal Power(SqlDecimal n, double exp)
		{
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			byte precision = n.Precision;
			int scale = (int)n.Scale;
			double x = n.ToDouble();
			n = new SqlDecimal(Math.Pow(x, exp));
			n.AdjustScale(scale - (int)n.Scale, true);
			n.m_bPrec = SqlDecimal.MaxPrecision;
			return n;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x000A162C File Offset: 0x000A0A2C
		public int CompareTo(object value)
		{
			if (value is SqlDecimal)
			{
				SqlDecimal value2 = (SqlDecimal)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlDecimal));
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x000A1668 File Offset: 0x000A0A68
		public int CompareTo(SqlDecimal value)
		{
			if (this.IsNull)
			{
				if (!value.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (value.IsNull)
				{
					return 1;
				}
				if (this < value)
				{
					return -1;
				}
				if (this > value)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x000A16C0 File Offset: 0x000A0AC0
		public override bool Equals(object value)
		{
			if (!(value is SqlDecimal))
			{
				return false;
			}
			SqlDecimal y = (SqlDecimal)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x000A1718 File Offset: 0x000A0B18
		public override int GetHashCode()
		{
			if (this.IsNull)
			{
				return 0;
			}
			SqlDecimal sqlDecimal = this;
			int num = (int)sqlDecimal.CalculatePrecision();
			sqlDecimal.AdjustScale(38 - num, true);
			int bLen = (int)sqlDecimal.m_bLen;
			int num2 = 0;
			int[] data = sqlDecimal.Data;
			for (int i = 0; i < bLen; i++)
			{
				int num3 = num2 >> 28 & 255;
				num2 <<= 4;
				num2 = (num2 ^ data[i] ^ num3);
			}
			return num2;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x000A1788 File Offset: 0x000A0B88
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x000A1798 File Offset: 0x000A0B98
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_bStatus = (254 & this.m_bStatus);
				return;
			}
			SqlDecimal sqlDecimal = SqlDecimal.Parse(reader.ReadElementString());
			this.m_bStatus = sqlDecimal.m_bStatus;
			this.m_bLen = sqlDecimal.m_bLen;
			this.m_bPrec = sqlDecimal.m_bPrec;
			this.m_bScale = sqlDecimal.m_bScale;
			this.m_data1 = sqlDecimal.m_data1;
			this.m_data2 = sqlDecimal.m_data2;
			this.m_data3 = sqlDecimal.m_data3;
			this.m_data4 = sqlDecimal.m_data4;
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x000A1848 File Offset: 0x000A0C48
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(this.ToString());
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x000A188C File Offset: 0x000A0C8C
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000D90 RID: 3472
		internal byte m_bStatus;

		// Token: 0x04000D91 RID: 3473
		internal byte m_bLen;

		// Token: 0x04000D92 RID: 3474
		internal byte m_bPrec;

		// Token: 0x04000D93 RID: 3475
		internal byte m_bScale;

		// Token: 0x04000D94 RID: 3476
		internal uint m_data1;

		// Token: 0x04000D95 RID: 3477
		internal uint m_data2;

		// Token: 0x04000D96 RID: 3478
		internal uint m_data3;

		// Token: 0x04000D97 RID: 3479
		internal uint m_data4;

		// Token: 0x04000D98 RID: 3480
		private const byte NUMERIC_MAX_PRECISION = 38;

		// Token: 0x04000D99 RID: 3481
		public static readonly byte MaxPrecision = 38;

		// Token: 0x04000D9A RID: 3482
		public static readonly byte MaxScale = 38;

		// Token: 0x04000D9B RID: 3483
		private const byte x_bNullMask = 1;

		// Token: 0x04000D9C RID: 3484
		private const byte x_bIsNull = 0;

		// Token: 0x04000D9D RID: 3485
		private const byte x_bNotNull = 1;

		// Token: 0x04000D9E RID: 3486
		private const byte x_bReverseNullMask = 254;

		// Token: 0x04000D9F RID: 3487
		private const byte x_bSignMask = 2;

		// Token: 0x04000DA0 RID: 3488
		private const byte x_bPositive = 0;

		// Token: 0x04000DA1 RID: 3489
		private const byte x_bNegative = 2;

		// Token: 0x04000DA2 RID: 3490
		private const byte x_bReverseSignMask = 253;

		// Token: 0x04000DA3 RID: 3491
		private const uint x_uiZero = 0U;

		// Token: 0x04000DA4 RID: 3492
		private const int x_cNumeMax = 4;

		// Token: 0x04000DA5 RID: 3493
		private const long x_lInt32Base = 4294967296L;

		// Token: 0x04000DA6 RID: 3494
		private const ulong x_ulInt32Base = 4294967296UL;

		// Token: 0x04000DA7 RID: 3495
		private const ulong x_ulInt32BaseForMod = 4294967295UL;

		// Token: 0x04000DA8 RID: 3496
		internal const ulong x_llMax = 9223372036854775807UL;

		// Token: 0x04000DA9 RID: 3497
		private const uint x_ulBase10 = 10U;

		// Token: 0x04000DAA RID: 3498
		private const double DUINT_BASE = 4294967296.0;

		// Token: 0x04000DAB RID: 3499
		private const double DUINT_BASE2 = 1.8446744073709552E+19;

		// Token: 0x04000DAC RID: 3500
		private const double DUINT_BASE3 = 7.922816251426434E+28;

		// Token: 0x04000DAD RID: 3501
		private const double DMAX_NUME = 1E+38;

		// Token: 0x04000DAE RID: 3502
		private const uint DBL_DIG = 17U;

		// Token: 0x04000DAF RID: 3503
		private const byte x_cNumeDivScaleMin = 6;

		// Token: 0x04000DB0 RID: 3504
		private static readonly uint[] x_rgulShiftBase = new uint[]
		{
			10U,
			100U,
			1000U,
			10000U,
			100000U,
			1000000U,
			10000000U,
			100000000U,
			1000000000U
		};

		// Token: 0x04000DB1 RID: 3505
		private static readonly uint[] DecimalHelpersLo = new uint[]
		{
			10U,
			100U,
			1000U,
			10000U,
			100000U,
			1000000U,
			10000000U,
			100000000U,
			1000000000U,
			1410065408U,
			1215752192U,
			3567587328U,
			1316134912U,
			276447232U,
			2764472320U,
			1874919424U,
			1569325056U,
			2808348672U,
			2313682944U,
			1661992960U,
			3735027712U,
			2990538752U,
			4135583744U,
			2701131776U,
			1241513984U,
			3825205248U,
			3892314112U,
			268435456U,
			2684354560U,
			1073741824U,
			2147483648U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U
		};

		// Token: 0x04000DB2 RID: 3506
		private static readonly uint[] DecimalHelpersMid = new uint[]
		{
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			2U,
			23U,
			232U,
			2328U,
			23283U,
			232830U,
			2328306U,
			23283064U,
			232830643U,
			2328306436U,
			1808227885U,
			902409669U,
			434162106U,
			46653770U,
			466537709U,
			370409800U,
			3704098002U,
			2681241660U,
			1042612833U,
			1836193738U,
			1182068202U,
			3230747430U,
			2242703233U,
			952195850U,
			932023908U,
			730304488U,
			3008077584U,
			16004768U,
			160047680U
		};

		// Token: 0x04000DB3 RID: 3507
		private static readonly uint[] DecimalHelpersHi = new uint[]
		{
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			5U,
			54U,
			542U,
			5421U,
			54210U,
			542101U,
			5421010U,
			54210108U,
			542101086U,
			1126043566U,
			2670501072U,
			935206946U,
			762134875U,
			3326381459U,
			3199043520U,
			1925664130U,
			2076772117U,
			3587851993U,
			1518781562U
		};

		// Token: 0x04000DB4 RID: 3508
		private static readonly uint[] DecimalHelpersHiHi = new uint[]
		{
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			1U,
			12U,
			126U,
			1262U,
			12621U,
			126217U,
			1262177U,
			12621774U,
			126217744U,
			1262177448U
		};

		// Token: 0x04000DB5 RID: 3509
		private const int HelperTableStartIndexLo = 5;

		// Token: 0x04000DB6 RID: 3510
		private const int HelperTableStartIndexMid = 15;

		// Token: 0x04000DB7 RID: 3511
		private const int HelperTableStartIndexHi = 24;

		// Token: 0x04000DB8 RID: 3512
		private const int HelperTableStartIndexHiHi = 33;

		// Token: 0x04000DB9 RID: 3513
		private static readonly byte[] rgCLenFromPrec = new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4
		};

		// Token: 0x04000DBA RID: 3514
		private const uint x_ulT1 = 10U;

		// Token: 0x04000DBB RID: 3515
		private const uint x_ulT2 = 100U;

		// Token: 0x04000DBC RID: 3516
		private const uint x_ulT3 = 1000U;

		// Token: 0x04000DBD RID: 3517
		private const uint x_ulT4 = 10000U;

		// Token: 0x04000DBE RID: 3518
		private const uint x_ulT5 = 100000U;

		// Token: 0x04000DBF RID: 3519
		private const uint x_ulT6 = 1000000U;

		// Token: 0x04000DC0 RID: 3520
		private const uint x_ulT7 = 10000000U;

		// Token: 0x04000DC1 RID: 3521
		private const uint x_ulT8 = 100000000U;

		// Token: 0x04000DC2 RID: 3522
		private const uint x_ulT9 = 1000000000U;

		// Token: 0x04000DC3 RID: 3523
		private const ulong x_dwlT10 = 10000000000UL;

		// Token: 0x04000DC4 RID: 3524
		private const ulong x_dwlT11 = 100000000000UL;

		// Token: 0x04000DC5 RID: 3525
		private const ulong x_dwlT12 = 1000000000000UL;

		// Token: 0x04000DC6 RID: 3526
		private const ulong x_dwlT13 = 10000000000000UL;

		// Token: 0x04000DC7 RID: 3527
		private const ulong x_dwlT14 = 100000000000000UL;

		// Token: 0x04000DC8 RID: 3528
		private const ulong x_dwlT15 = 1000000000000000UL;

		// Token: 0x04000DC9 RID: 3529
		private const ulong x_dwlT16 = 10000000000000000UL;

		// Token: 0x04000DCA RID: 3530
		private const ulong x_dwlT17 = 100000000000000000UL;

		// Token: 0x04000DCB RID: 3531
		private const ulong x_dwlT18 = 1000000000000000000UL;

		// Token: 0x04000DCC RID: 3532
		private const ulong x_dwlT19 = 10000000000000000000UL;

		// Token: 0x04000DCD RID: 3533
		public static readonly SqlDecimal Null = new SqlDecimal(true);

		// Token: 0x04000DCE RID: 3534
		public static readonly SqlDecimal MinValue = SqlDecimal.Parse("-99999999999999999999999999999999999999");

		// Token: 0x04000DCF RID: 3535
		public static readonly SqlDecimal MaxValue = SqlDecimal.Parse("99999999999999999999999999999999999999");
	}
}
