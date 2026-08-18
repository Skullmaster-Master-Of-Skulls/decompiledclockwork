using System;
using System.Collections.Generic;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.Core;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x0200023E RID: 574
	internal static class DecimalConv
	{
		// Token: 0x060014DE RID: 5342 RVA: 0x000E02A4 File Offset: 0x000DE4A4
		internal static decimal GetDecimal(byte[] bytes, int dataPos, int length)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			decimal result;
			try
			{
				int num = dataPos + 1;
				if ((bytes[dataPos] & 128) != 0)
				{
					int num2 = (int)(bytes[dataPos] - 193);
					if (length == 1)
					{
						result = 0m;
					}
					else
					{
						if (bytes[dataPos] == 255 && bytes[num] == 101)
						{
							throw new OverflowException();
						}
						if (num2 > 14 || (num2 == 14 && bytes[num] > 8) || num2 - (length - 2) < -14)
						{
							throw new OverflowException();
						}
						int num3 = (length - 1) * 2;
						if (num3 >= 32)
						{
							throw new OverflowException();
						}
						byte b = 0;
						if (num3 == 30)
						{
							if (bytes[num] - 1 < 10)
							{
								num3--;
							}
							if ((bytes[dataPos + length - 1] - 1) % 10 == 0)
							{
								num3--;
								b = (bytes[dataPos + length - 1] - 1) / 10;
							}
							if (num3 > 38)
							{
								throw new OverflowException();
							}
						}
						int[] array = new int[4];
						int num4 = 1;
						int num5 = 1;
						int num6 = 1;
						int num7 = 1;
						int num8 = length;
						int num9;
						if (b > 0)
						{
							num9 = 2 * (num2 - (length - 2)) + 1;
						}
						else
						{
							num9 = 2 * (num2 - (length - 2));
						}
						if (num9 > 1)
						{
							num8 = num2 + 2;
						}
						int i = num8;
						int num10 = num8;
						if (num8 - 1 > 12)
						{
							if (b > 0)
							{
								array[0] = (int)b;
								i--;
								num4 = 10;
							}
							while (i >= num10 - 3)
							{
								int num11;
								if (i <= length)
								{
									num11 = (int)(bytes[i + (dataPos - 1)] - 1) * num4;
								}
								else
								{
									num11 = 0;
								}
								num4 *= 100;
								array[0] += num11;
								i--;
							}
						}
						if (num8 - 1 > 8)
						{
							num10 = i;
							while (i >= num10 - 3)
							{
								int num11;
								if (i <= length)
								{
									num11 = (int)(bytes[i + (dataPos - 1)] - 1) * num5;
								}
								else
								{
									num11 = 0;
								}
								num5 *= 100;
								array[1] += num11;
								i--;
							}
						}
						if (num8 - 1 > 4)
						{
							num10 = i;
							while (i >= num10 - 3)
							{
								int num11;
								if (i <= length)
								{
									num11 = (int)(bytes[i + (dataPos - 1)] - 1) * num6;
								}
								else
								{
									num11 = 0;
								}
								num6 *= 100;
								array[2] += num11;
								i--;
							}
						}
						while (i >= 2)
						{
							int num11;
							if (i <= length)
							{
								num11 = (int)(bytes[i + (dataPos - 1)] - 1) * num7;
							}
							else
							{
								num11 = 0;
							}
							num7 *= 100;
							array[3] += num11;
							i--;
						}
						decimal num12;
						if (num8 - 1 > 12)
						{
							num12 = ((long)array[3] * (long)num6 + (long)array[2]) * ((long)num4 * (long)num5) + ((long)array[1] * (long)num4 + (long)array[0]);
						}
						else if (num8 - 1 > 8)
						{
							num12 = ((long)array[3] * (long)num6 + (long)array[2]) * num5 + array[1];
						}
						else if (num8 - 1 > 4)
						{
							num12 = (long)array[3] * (long)num6 + (long)array[2];
						}
						else
						{
							num12 = array[3];
						}
						if (num9 < 0)
						{
							int[] bits = decimal.GetBits(num12);
							num12 = new decimal(bits[0], bits[1], bits[2], false, (byte)(-(byte)num9));
						}
						result = num12;
					}
				}
				else
				{
					int num13 = (int)(62 - bytes[dataPos]);
					if (length == 1)
					{
						throw new OverflowException();
					}
					if (num13 > 14 || (num13 == 14 && bytes[num] < 94) || num13 - (length - 3) < -14)
					{
						throw new OverflowException();
					}
					int num14 = (length - 2) * 2;
					if (num14 >= 32)
					{
						throw new OverflowException();
					}
					byte b2 = 0;
					if (num14 == 30)
					{
						if (101 - bytes[num] < 10)
						{
							num14--;
						}
						if ((101 - bytes[dataPos + length - 1]) % 10 == 0)
						{
							num14--;
							b2 = (101 - bytes[dataPos + length - 1]) / 10;
						}
						if (num14 > 38)
						{
							throw new OverflowException();
						}
					}
					int[] array2 = new int[4];
					int num15 = 1;
					int num16 = 1;
					int num17 = 1;
					int num18 = 1;
					int num19 = length;
					int num20;
					if (b2 > 0)
					{
						num20 = 2 * (num13 - (length - 3)) + 1;
					}
					else
					{
						num20 = 2 * (num13 - (length - 3));
					}
					if (num20 > 1)
					{
						num19 = num13 + 3;
					}
					int j = num19 - 1;
					int num21 = num19 - 1;
					if (num19 - 2 > 12)
					{
						if (b2 > 0)
						{
							array2[0] = (int)b2;
							j--;
							num15 = 10;
						}
						while (j >= num21 - 3)
						{
							int num22;
							if (j < length)
							{
								num22 = (int)(101 - bytes[j + (dataPos - 1)]) * num15;
							}
							else
							{
								num22 = 0;
							}
							num15 *= 100;
							array2[0] += num22;
							j--;
						}
					}
					if (num19 - 2 > 8)
					{
						num21 = j;
						while (j >= num21 - 3)
						{
							int num22;
							if (j < length)
							{
								num22 = (int)(101 - bytes[j + (dataPos - 1)]) * num16;
							}
							else
							{
								num22 = 0;
							}
							num16 *= 100;
							array2[1] += num22;
							j--;
						}
					}
					if (num19 - 2 > 4)
					{
						num21 = j;
						while (j >= num21 - 3)
						{
							int num22;
							if (j < length)
							{
								num22 = (int)(101 - bytes[j + (dataPos - 1)]) * num17;
							}
							else
							{
								num22 = 0;
							}
							num17 *= 100;
							array2[2] += num22;
							j--;
						}
					}
					while (j >= 2)
					{
						int num22;
						if (j < length)
						{
							num22 = (int)(101 - bytes[j + (dataPos - 1)]) * num18;
						}
						else
						{
							num22 = 0;
						}
						num18 *= 100;
						array2[3] += num22;
						j--;
					}
					decimal num23;
					if (num19 - 2 > 12)
					{
						num23 = ((long)array2[3] * (long)num17 + (long)array2[2]) * ((long)num15 * (long)num16) + ((long)array2[1] * (long)num15 + (long)array2[0]);
					}
					else if (num19 - 2 > 8)
					{
						num23 = ((long)array2[3] * (long)num17 + (long)array2[2]) * num16 + array2[1];
					}
					else if (num19 - 2 > 4)
					{
						num23 = (long)array2[3] * (long)num17 + (long)array2[2];
					}
					else
					{
						num23 = array2[3];
					}
					int[] bits2 = decimal.GetBits(num23);
					if (num20 < 0)
					{
						num23 = new decimal(bits2[0], bits2[1], bits2[2], true, (byte)(-(byte)num20));
					}
					else
					{
						bits2[3] = (bits2[3] | int.MinValue);
						num23 = new decimal(bits2);
					}
					result = num23;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x000E0988 File Offset: 0x000DEB88
		internal static void GetBytes(decimal dec, out byte[] bytes)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				bytes = new byte[22];
				if (dec == 0m)
				{
					bytes[0] = 1;
					bytes[1] = 128;
				}
				else
				{
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
						bytes[0] = (byte)(m + 2);
					}
					else
					{
						bytes[0] = (byte)(m + 1);
					}
					bytes[1] = b2;
					int num8 = 2;
					while (m > 0)
					{
						bytes[num8] = array[m - 1];
						num8++;
						m--;
					}
					if (flag)
					{
						bytes[num8] = 102;
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x000E0F98 File Offset: 0x000DF198
		internal static byte[] FromString(string stringRep, out bool isPositive, out bool isZero, out bool isInfinity, out bool hasDecimalPoint)
		{
			isPositive = true;
			isZero = (isInfinity = (hasDecimalPoint = false));
			if (stringRep == null)
			{
				throw new ArgumentNullException();
			}
			string text = stringRep.Trim();
			if (text.Length == 0)
			{
				throw new FormatException();
			}
			if (isZero = text.Equals("0"))
			{
				return OracleNumberCore.GetZeroByteRep();
			}
			if (text[0] == '-' || text[0] == '+')
			{
				isPositive = (text[0] != '-');
				text = text.Substring(1).TrimStart(new char[0]);
			}
			if (isInfinity = text.Equals("~"))
			{
				if (!isPositive)
				{
					return OracleNumberCore.GetNegativeInfinityByteRep();
				}
				return OracleNumberCore.GetPositiveInfinityByteRep();
			}
			else
			{
				string[] array = text.Split(DecimalConv.exponent, 2);
				string text2 = array[0].TrimEnd(new char[0]);
				if (array.Length == 2)
				{
					throw new FormatException();
				}
				array = text2.Split(DecimalConv.dot);
				if (array.Length > 2)
				{
					throw new FormatException();
				}
				string text3 = array[0];
				if (!string.IsNullOrEmpty(text3))
				{
					text3 = text3.TrimStart(new char[]
					{
						"0"[0]
					});
				}
				int i = (text3 != null) ? text3.Length : 0;
				bool flag = i > 0;
				string text4 = null;
				int j = 0;
				bool flag2 = false;
				if (array.Length == 2)
				{
					text4 = array[1];
					if (!string.IsNullOrEmpty(text4))
					{
						text4 = text4.TrimEnd(new char[]
						{
							"0"[0]
						});
					}
					j = ((text4 != null) ? text4.Length : 0);
					flag2 = (hasDecimalPoint = (j > 0));
				}
				if (isZero = (!flag && !flag2))
				{
					return OracleNumberCore.GetZeroByteRep();
				}
				List<byte> list = new List<byte>(21);
				int num = 20;
				bool flag3 = false;
				bool flag4 = false;
				int num2 = 0;
				if (flag)
				{
					int num3 = 0;
					if (!flag2)
					{
						text3 = text3.TrimEnd(new char[]
						{
							"0"[0]
						});
						num2 = i - text3.Length;
						i = text3.Length;
						if (num2 % 2 != 0)
						{
							text3 += "0";
							i++;
							num2--;
						}
					}
					if (i >= 40)
					{
						string s = text3[40].ToString();
						byte b = byte.Parse(s);
						flag4 = (b >= 5);
						num2 += i - 40;
						i = 40;
						if (num2 % 2 != 0)
						{
							i--;
							num2++;
						}
						text3 = text3.Substring(0, i);
					}
					if (i % 2 != 0)
					{
						string s = text3[num3].ToString();
						byte b = byte.Parse(s);
						if (isPositive)
						{
							b += 1;
						}
						else
						{
							b = 101 - b;
						}
						list.Add(b);
						num--;
						num3 = 1;
					}
					while (i > num3)
					{
						if (--num < 0)
						{
							flag3 = true;
							break;
						}
						string s = text3.Substring(num3, 2);
						byte b = byte.Parse(s);
						if (isPositive)
						{
							b += 1;
						}
						else
						{
							b = 101 - b;
						}
						list.Add(b);
						num3 += 2;
					}
				}
				int count = list.Count;
				int num4 = 0;
				if (flag2 && !flag3)
				{
					int num3 = 0;
					if (!flag)
					{
						text4 = text4.TrimStart(new char[]
						{
							"0"[0]
						});
						num4 = j - text4.Length;
						j = text4.Length;
						if (num4 % 2 != 0)
						{
							text4 = "0" + text4;
							j++;
							num4--;
						}
						if (j >= 40)
						{
							string s = text4[40].ToString();
							byte b = byte.Parse(s);
							flag4 = (b >= 5);
						}
					}
					if (flag2 && flag && j + i >= 40)
					{
						string s = text4[40 - i].ToString();
						byte b = byte.Parse(s);
						flag4 = (b >= 5);
					}
					if (j % 2 != 0)
					{
						text4 += "0";
						j++;
					}
					while (j > num3)
					{
						if (--num < 0)
						{
							break;
						}
						string s = text4.Substring(num3, 2);
						byte b = byte.Parse(s);
						if (isPositive)
						{
							b += 1;
						}
						else
						{
							b = 101 - b;
						}
						list.Add(b);
						num3 += 2;
					}
				}
				bool flag5 = false;
				if (flag4)
				{
					byte[] array2 = list.ToArray();
					int num5 = 0;
					if (isPositive)
					{
						int k;
						for (k = array2.Length - 1; k >= 0; k--)
						{
							byte b = array2[k];
							num5 = (int)(b + 1);
							if (num5 <= 100)
							{
								list[k] = (byte)num5;
								break;
							}
							list.RemoveAt(k);
						}
						if (k < 0 && num5 > 100)
						{
							list.Add(2);
							flag5 = true;
						}
					}
					else
					{
						int k;
						for (k = array2.Length - 1; k >= 0; k--)
						{
							byte b = array2[k];
							num5 = (int)(b - 1);
							if (num5 >= 2)
							{
								list[k] = (byte)num5;
								break;
							}
							list.RemoveAt(k);
						}
						if (k < 0 && num5 < 2)
						{
							list.Add(100);
							flag5 = true;
						}
					}
				}
				if (!isPositive && list.Count < 20)
				{
					list.Add(102);
				}
				int num6 = count - 1;
				if (!flag2)
				{
					num6 += num2 / 2;
				}
				else if (!flag)
				{
					num6 -= num4 / 2;
				}
				if (flag5)
				{
					num6++;
				}
				if (num6 > 62)
				{
					throw new OverflowException();
				}
				if (num6 < -65)
				{
					return OracleNumberCore.GetZeroByteRep();
				}
				if (isPositive)
				{
					num6 += 193;
				}
				else
				{
					num6 = (int)((byte)(62 - num6));
				}
				list.Insert(0, (byte)num6);
				return list.ToArray();
			}
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x000E1548 File Offset: 0x000DF748
		internal static string ToString(byte[] bytes)
		{
			if (bytes == null || bytes.Length == 0 || bytes.Length > 21)
			{
				return null;
			}
			if (OracleNumberCore.IsZero(bytes))
			{
				return "0";
			}
			bool flag = !OracleNumberCore.IsPositive(bytes);
			if (!OracleNumberCore.IsInfinity(bytes))
			{
				int num = bytes.Length - 1;
				if (flag && bytes[num] == 102)
				{
					num--;
				}
				int num2 = 0;
				int num3 = 1;
				int num4 = num;
				int num5;
				if (flag)
				{
					num5 = (int)(62 - bytes[num2]);
				}
				else
				{
					num5 = (int)(bytes[num2] - 193);
				}
				bool flag2 = num5 < 0;
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = num3; i <= num4; i++)
				{
					int num6 = (int)bytes[i];
					if (flag)
					{
						num6 = 101 - num6;
					}
					else
					{
						num6--;
					}
					stringBuilder.Append(num6.ToString("D2"));
				}
				string text;
				if (flag2)
				{
					int num7 = (num5 + 1) * -1;
					int num8 = num7 * 2;
					if (num8 > 0)
					{
						stringBuilder.Insert(0, "0", num8);
					}
					stringBuilder.Insert(0, "0" + DecimalConv.dot[0]);
					text = stringBuilder.ToString().TrimEnd(new char[]
					{
						"0"[0]
					});
				}
				else
				{
					int num9 = num5 + 1;
					int num10 = num - num9;
					if (num10 > 0)
					{
						int num11 = num9 * 2;
						if (num11 < stringBuilder.Length)
						{
							stringBuilder.Insert(num11, DecimalConv.dot[0]);
						}
						text = stringBuilder.ToString().Trim(new char[]
						{
							"0"[0]
						});
					}
					else
					{
						if (num10 < 0)
						{
							int num12 = num10 * -1;
							int num13 = num12 * 2;
							if (num13 > 0)
							{
								stringBuilder.Append("0"[0], num13);
							}
						}
						text = stringBuilder.ToString().TrimStart(new char[]
						{
							"0"[0]
						});
					}
				}
				if (flag)
				{
					text = "-" + text;
				}
				return text;
			}
			if (!flag)
			{
				return "~";
			}
			return "-~";
		}

		// Token: 0x04001972 RID: 6514
		private const string ZERO = "0";

		// Token: 0x04001973 RID: 6515
		private const string INFINITY = "~";

		// Token: 0x04001974 RID: 6516
		internal const int MAX_BYTEREP_LENGTH = 21;

		// Token: 0x04001975 RID: 6517
		private const int POSITIVE_OFFSET = 1;

		// Token: 0x04001976 RID: 6518
		private const int NEGATIVE_OFFSET = 101;

		// Token: 0x04001977 RID: 6519
		private const byte NEGATIVE_BYTE_TERMINATOR = 102;

		// Token: 0x04001978 RID: 6520
		private const byte MAX_EXPONENT = 127;

		// Token: 0x04001979 RID: 6521
		private const byte BASE100EXP = 2;

		// Token: 0x0400197A RID: 6522
		private const int ORANUM_MAX_EXP = 62;

		// Token: 0x0400197B RID: 6523
		private const int ORANUM_MIN_EXP = -65;

		// Token: 0x0400197C RID: 6524
		private const int ORANUM_POS_BASE = 100;

		// Token: 0x0400197D RID: 6525
		private const int ORANUM_NEG_BASE = 2;

		// Token: 0x0400197E RID: 6526
		private const int ORANUM_MAX_DIGITS = 40;

		// Token: 0x0400197F RID: 6527
		private static char[] exponent = new char[]
		{
			'E',
			'e'
		};

		// Token: 0x04001980 RID: 6528
		private static char[] dot = new char[]
		{
			'.'
		};
	}
}
