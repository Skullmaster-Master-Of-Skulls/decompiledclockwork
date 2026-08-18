using System;
using System.Data;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000046 RID: 70
	internal class DecimalConv
	{
		// Token: 0x0600032A RID: 810 RVA: 0x00027294 File Offset: 0x00026294
		private DecimalConv()
		{
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0002729C File Offset: 0x0002629C
		internal unsafe static decimal GetDecimal(IntPtr numCtx)
		{
			byte* ptr = (byte*)((void*)numCtx);
			if ((ptr[1] & 128) != 0)
			{
				int num = (int)(*ptr);
				int num2 = (int)(ptr[1] - 193);
				if (num == 1)
				{
					return 0m;
				}
				if (ptr[1] == 255 && ptr[2] == 101)
				{
					throw new OverflowException();
				}
				if (num2 > 14 || (num2 == 14 && ptr[2] > 8) || num2 - (num - 2) < -14)
				{
					throw new OverflowException();
				}
				int num3 = (num - 1) * 2;
				if (num3 >= 32)
				{
					throw new OverflowException();
				}
				byte b = 0;
				if (num3 == 30)
				{
					if (ptr[2] - 1 < 10)
					{
						num3--;
					}
					if ((ptr[*ptr] - 1) % 10 == 0)
					{
						num3--;
						b = (ptr[*ptr] - 1) / 10;
					}
					if (num3 > 29)
					{
						throw new OverflowException();
					}
				}
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				int num8 = 1;
				int num9 = 1;
				int num10 = 1;
				int num11 = 1;
				int num12 = num;
				int num13;
				if (b > 0)
				{
					num13 = 2 * (num2 - (num - 2)) + 1;
				}
				else
				{
					num13 = 2 * (num2 - (num - 2));
				}
				if (num13 > 1)
				{
					num12 = num2 + 2;
				}
				int i = num12;
				int num14 = num12;
				if (num12 - 1 > 12)
				{
					if (b > 0)
					{
						num4 = (int)b;
						i--;
						num8 = 10;
					}
					while (i >= num14 - 3)
					{
						int num15;
						if (i <= num)
						{
							num15 = (int)(ptr[i] - 1) * num8;
						}
						else
						{
							num15 = 0;
						}
						num8 *= 100;
						num4 += num15;
						i--;
					}
				}
				if (num12 - 1 > 8)
				{
					num14 = i;
					while (i >= num14 - 3)
					{
						int num15;
						if (i <= num)
						{
							num15 = (int)(ptr[i] - 1) * num9;
						}
						else
						{
							num15 = 0;
						}
						num9 *= 100;
						num5 += num15;
						i--;
					}
				}
				if (num12 - 1 > 4)
				{
					num14 = i;
					while (i >= num14 - 3)
					{
						int num15;
						if (i <= num)
						{
							num15 = (int)(ptr[i] - 1) * num10;
						}
						else
						{
							num15 = 0;
						}
						num10 *= 100;
						num6 += num15;
						i--;
					}
				}
				while (i >= 2)
				{
					int num15;
					if (i <= num)
					{
						num15 = (int)(ptr[i] - 1) * num11;
					}
					else
					{
						num15 = 0;
					}
					num11 *= 100;
					num7 += num15;
					i--;
				}
				decimal num16;
				if (num12 - 1 > 12)
				{
					num16 = ((long)num7 * (long)num10 + (long)num6) * ((long)num8 * (long)num9) + ((long)num5 * (long)num8 + (long)num4);
				}
				else if (num12 - 1 > 8)
				{
					num16 = ((long)num7 * (long)num10 + (long)num6) * num9 + num5;
				}
				else if (num12 - 1 > 4)
				{
					num16 = (long)num7 * (long)num10 + (long)num6;
				}
				else
				{
					num16 = num7;
				}
				if (num13 < 0)
				{
					int[] bits = decimal.GetBits(num16);
					num16 = new decimal(bits[0], bits[1], bits[2], false, (byte)(-(byte)num13));
				}
				else if (num13 == 1)
				{
					num16 *= 10m;
				}
				return num16;
			}
			else
			{
				int num17 = (int)(*ptr);
				int num18 = (int)(62 - ptr[1]);
				if (num17 == 1)
				{
					throw new OverflowException();
				}
				if (num18 > 14 || (num18 == 14 && ptr[2] < 94) || num18 - (num17 - 3) < -14)
				{
					throw new OverflowException();
				}
				int num19 = (num17 - 2) * 2;
				if (num19 >= 32)
				{
					throw new OverflowException();
				}
				byte b2 = 0;
				if (num19 == 30)
				{
					if (101 - ptr[2] < 10)
					{
						num19--;
					}
					if ((101 - ptr[*ptr - 1]) % 10 == 0)
					{
						num19--;
						b2 = (101 - ptr[*ptr - 1]) / 10;
					}
					if (num19 > 29)
					{
						throw new OverflowException();
					}
				}
				int num20 = 0;
				int num21 = 0;
				int num22 = 0;
				int num23 = 0;
				int num24 = 1;
				int num25 = 1;
				int num26 = 1;
				int num27 = 1;
				int num28 = num17;
				int num29;
				if (b2 > 0)
				{
					num29 = 2 * (num18 - (num17 - 3)) + 1;
				}
				else
				{
					num29 = 2 * (num18 - (num17 - 3));
				}
				if (num29 > 1)
				{
					num28 = num18 + 3;
				}
				int j = num28 - 1;
				int num30 = num28 - 1;
				if (num28 - 2 > 12)
				{
					if (b2 > 0)
					{
						num20 = (int)b2;
						j--;
						num24 = 10;
					}
					while (j >= num30 - 3)
					{
						int num31;
						if (j < num17)
						{
							num31 = (int)(101 - ptr[j]) * num24;
						}
						else
						{
							num31 = 0;
						}
						num24 *= 100;
						num20 += num31;
						j--;
					}
				}
				if (num28 - 2 > 8)
				{
					num30 = j;
					while (j >= num30 - 3)
					{
						int num31;
						if (j < num17)
						{
							num31 = (int)(101 - ptr[j]) * num25;
						}
						else
						{
							num31 = 0;
						}
						num25 *= 100;
						num21 += num31;
						j--;
					}
				}
				if (num28 - 2 > 4)
				{
					num30 = j;
					while (j >= num30 - 3)
					{
						int num31;
						if (j < num17)
						{
							num31 = (int)(101 - ptr[j]) * num26;
						}
						else
						{
							num31 = 0;
						}
						num26 *= 100;
						num22 += num31;
						j--;
					}
				}
				while (j >= 2)
				{
					int num31;
					if (j < num17)
					{
						num31 = (int)(101 - ptr[j]) * num27;
					}
					else
					{
						num31 = 0;
					}
					num27 *= 100;
					num23 += num31;
					j--;
				}
				decimal num32;
				if (num28 - 2 > 12)
				{
					num32 = ((long)num23 * (long)num26 + (long)num22) * ((long)num24 * (long)num25) + ((long)num21 * (long)num24 + (long)num20);
				}
				else if (num28 - 2 > 8)
				{
					num32 = ((long)num23 * (long)num26 + (long)num22) * num25 + num21;
				}
				else if (num28 - 2 > 4)
				{
					num32 = (long)num23 * (long)num26 + (long)num22;
				}
				else
				{
					num32 = num23;
				}
				int[] bits2 = decimal.GetBits(num32);
				if (num29 < 0)
				{
					num32 = new decimal(bits2[0], bits2[1], bits2[2], true, (byte)(-(byte)num29));
				}
				else
				{
					bits2[3] = (bits2[3] | int.MinValue);
					num32 = new decimal(bits2);
					if (num29 == 1)
					{
						num32 *= 10m;
					}
				}
				return num32;
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00027888 File Offset: 0x00026888
		internal unsafe static ValueType GetNum(IntPtr numCtx, DbType dbType)
		{
			int num = 0;
			ValueType result = 0;
			switch (dbType)
			{
			case DbType.Byte:
			{
				byte b = 0;
				try
				{
					num = OpsDec.ToByte(numCtx, &b);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				result = b;
				break;
			}
			case DbType.Boolean:
			case DbType.Currency:
			case DbType.Date:
			case DbType.DateTime:
			case DbType.Guid:
				break;
			case DbType.Decimal:
				result = DecimalConv.GetDecimal(numCtx);
				num = 0;
				break;
			case DbType.Double:
			{
				double num2 = 0.0;
				try
				{
					num = OpsDec.ToReal(numCtx, (void*)(&num2), 8);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					throw;
				}
				result = num2;
				break;
			}
			case DbType.Int16:
			{
				short num3 = 0;
				try
				{
					num = OpsDec.ToInteger(numCtx, (void*)(&num3), 2);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					throw;
				}
				result = num3;
				break;
			}
			case DbType.Int32:
			{
				int num4 = 0;
				try
				{
					num = OpsDec.ToInteger(numCtx, (void*)(&num4), 4);
				}
				catch (Exception ex4)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex4);
					}
					throw;
				}
				result = num4;
				break;
			}
			case DbType.Int64:
			{
				long num5 = 0L;
				try
				{
					num = OpsDec.ToInteger(numCtx, (void*)(&num5), 8);
				}
				catch (Exception ex5)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex5);
					}
					throw;
				}
				result = num5;
				break;
			}
			default:
				if (dbType == DbType.Single)
				{
					float num6 = 0f;
					try
					{
						num = OpsDec.ToReal(numCtx, (void*)(&num6), 4);
					}
					catch (Exception ex6)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex6);
						}
						throw;
					}
					result = num6;
				}
				break;
			}
			if (num == 0)
			{
				return result;
			}
			if (num == 22053 || num == 22054)
			{
				throw new OverflowException(OracleTypeException.GetTypeMsg(num, new object[0]));
			}
			throw new OracleTypeException(num, new object[0]);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00027A8C File Offset: 0x00026A8C
		public unsafe static void GetBytes(decimal dec, IntPtr numCtx)
		{
			byte* ptr = (byte*)((void*)numCtx);
			if (dec == 0m)
			{
				*ptr = 1;
				ptr[1] = 128;
				return;
			}
			int[] bits = decimal.GetBits(dec);
			int num = bits[3];
			bool flag = false;
			if (((long)num & (long)((ulong)-2147483648)) == (long)((ulong)-2147483648))
			{
				flag = true;
			}
			int num2 = (int)((byte)((num & 16711680) >> 16));
			decimal d = 10000000000000000m;
			int num3 = 100000000;
			decimal num4 = 0m;
			int i = 0;
			int j = 0;
			int k = 0;
			int l = 0;
			if (num2 == 0)
			{
				long num5 = (long)(dec % d);
				long num6 = (long)(dec / d);
				i = (int)(num5 % (long)num3);
				j = (int)(num5 / (long)num3);
				if (num6 != 0L)
				{
					k = (int)(num6 % (long)num3);
					l = (int)(num6 / (long)num3);
				}
				if (flag)
				{
					if (i < 0)
					{
						i = -i;
					}
					if (j < 0)
					{
						j = -j;
					}
					if (k < 0)
					{
						k = -k;
					}
					if (l < 0)
					{
						l = -l;
					}
				}
			}
			else
			{
				num4 = new decimal(bits[0], bits[1], bits[2], flag, 0) / (decimal)Math.Pow(10.0, (double)num2);
				bits = decimal.GetBits(num4);
				num = bits[3];
				num2 = (int)((byte)((num & 16711680) >> 16));
				num4 = new decimal(bits[0], bits[1], bits[2], false, 0);
				if (num2 == 0)
				{
					long num5 = (long)(num4 % d);
					long num6 = (long)(num4 / d);
					i = (int)(num5 % (long)num3);
					j = (int)(num5 / (long)num3);
					if (num6 > 0L)
					{
						k = (int)(num6 % (long)num3);
						l = (int)(num6 / (long)num3);
					}
				}
			}
			byte b = 100;
			byte b2 = 0;
			byte b3 = 0;
			byte b4;
			if (num2 == 0)
			{
				if (i > 0)
				{
					b4 = (byte)(i % (int)b);
					i /= (int)b;
					while (b4 == 0)
					{
						b4 = (byte)(i % (int)b);
						i /= (int)b;
						b2 += 1;
					}
				}
				else if (j > 0)
				{
					b4 = (byte)(j % (int)b);
					j /= (int)b;
					b2 = 4;
					while (b4 == 0)
					{
						b4 = (byte)(j % (int)b);
						j /= (int)b;
						b2 += 1;
					}
				}
				else if (k > 0)
				{
					b4 = (byte)(k % (int)b);
					k /= (int)b;
					b2 = 8;
					while (b4 == 0)
					{
						b4 = (byte)(k % (int)b);
						k /= (int)b;
						b2 += 1;
					}
				}
				else
				{
					b4 = (byte)(l % (int)b);
					l /= (int)b;
					b2 = 12;
					while (b4 == 0)
					{
						b4 = (byte)(l % (int)b);
						l /= (int)b;
						b2 += 1;
					}
				}
			}
			else if (num2 % 2 == 0)
			{
				long num5 = (long)(num4 % d);
				long num6 = (long)(num4 / d);
				i = (int)(num5 % (long)num3);
				j = (int)(num5 / (long)num3);
				if (num6 > 0L)
				{
					k = (int)(num6 % (long)num3);
					l = (int)(num6 / (long)num3);
				}
				b4 = (byte)(i % (int)b);
				i /= (int)b;
				b3 = (byte)(num2 / 2);
			}
			else
			{
				decimal d2 = 1000000000000000m;
				long num7 = 10000000L;
				long num5 = (long)(num4 % d2);
				long num6 = (long)(num4 / d2);
				i = (int)(num5 % num7);
				j = (int)(num5 / num7);
				if (num6 > 0L)
				{
					k = (int)(num6 % (long)num3);
					l = (int)(num6 / (long)num3);
				}
				b4 = (byte)(i % 10 * 10);
				i /= 10;
				b3 = (byte)(num2 / 2 + 1);
			}
			int m = 0;
			byte[] array = new byte[22];
			if (flag)
			{
				array[m] = 101 - b4;
			}
			else
			{
				array[m] = b4 + 1;
			}
			m++;
			while (i > 0)
			{
				b4 = (byte)(i % (int)b);
				i /= (int)b;
				if (flag)
				{
					array[m] = 101 - b4;
				}
				else
				{
					array[m] = b4 + 1;
				}
				m++;
				b2 += 1;
			}
			if (j > 0)
			{
				while (b2 < 3)
				{
					if (flag)
					{
						array[m] = 101;
					}
					else
					{
						array[m] = 1;
					}
					m++;
					b2 += 1;
				}
			}
			while (j > 0)
			{
				b4 = (byte)(j % (int)b);
				j /= (int)b;
				if (flag)
				{
					array[m] = 101 - b4;
				}
				else
				{
					array[m] = b4 + 1;
				}
				m++;
				b2 += 1;
			}
			if (k > 0)
			{
				while (b2 < 7)
				{
					if (flag)
					{
						array[m] = 101;
					}
					else
					{
						array[m] = 1;
					}
					m++;
					b2 += 1;
				}
			}
			while (k > 0)
			{
				b4 = (byte)(k % (int)b);
				k /= (int)b;
				if (flag)
				{
					array[m] = 101 - b4;
				}
				else
				{
					array[m] = b4 + 1;
				}
				m++;
				b2 += 1;
			}
			if (l > 0)
			{
				while (b2 < 11)
				{
					if (flag)
					{
						array[m] = 101;
					}
					else
					{
						array[m] = 1;
					}
					m++;
					b2 += 1;
				}
			}
			while (l > 0)
			{
				b4 = (byte)(l % (int)b);
				l /= (int)b;
				if (flag)
				{
					array[m] = 101 - b4;
				}
				else
				{
					array[m] = b4 + 1;
				}
				m++;
				b2 += 1;
			}
			if (flag)
			{
				b2 = 62 - b2 + b3;
			}
			else
			{
				b2 = b2 + 193 - b3;
			}
			if (flag)
			{
				*ptr = (byte)(m + 2);
			}
			else
			{
				*ptr = (byte)(m + 1);
			}
			ptr[1] = b2;
			int num8 = 2;
			while (m > 0)
			{
				ptr[num8] = array[m - 1];
				num8++;
				m--;
			}
			if (flag)
			{
				ptr[num8] = 102;
			}
		}
	}
}
