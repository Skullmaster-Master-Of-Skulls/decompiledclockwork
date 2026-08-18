using System;
using System.Text;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.Core;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.Common
{
	// Token: 0x0200009B RID: 155
	internal class HelperClass
	{
		// Token: 0x060006A1 RID: 1697 RVA: 0x0003A82C File Offset: 0x00038A2C
		internal static int URShift(int number, int bits)
		{
			if (number >= 0)
			{
				return number >> bits;
			}
			return (number >> bits) + (2 << ~bits);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0003A848 File Offset: 0x00038A48
		internal static long URShift(long number, int bits)
		{
			if (number >= 0L)
			{
				return number >> bits;
			}
			return (number >> bits) + (2L << ~bits);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0003A868 File Offset: 0x00038A68
		internal static int CompareBytes(byte[] first, byte[] second)
		{
			if (first == null && second == null)
			{
				return 0;
			}
			if (first == null || second == null)
			{
				if (first == null)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				int num = first.Length;
				int num2 = second.Length;
				if (num == num2)
				{
					int i = 0;
					while (i < num)
					{
						int num3 = (int)(first[i] & byte.MaxValue);
						int num4 = (int)(second[i] & byte.MaxValue);
						if (num3 != num4)
						{
							if (num3 < num4)
							{
								return -1;
							}
							return 1;
						}
						else
						{
							i++;
						}
					}
					return 0;
				}
				if (num >= num2)
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0003A8D4 File Offset: 0x00038AD4
		internal static double GetDouble(byte[] bytes, int offset, int len)
		{
			byte b = bytes[offset];
			int num = offset + 1;
			int num2 = len - 1;
			bool flag = true;
			if (OracleNumberCore.IsNaN(bytes, offset, len))
			{
				return double.NaN;
			}
			sbyte b2;
			bool flag2;
			int num3;
			if ((b & 128) != 0)
			{
				if (b == 128 && len == 1)
				{
					return 0.0;
				}
				if (len == 2 && b == 255 && bytes[offset + 1] == 101)
				{
					return double.PositiveInfinity;
				}
				b2 = (sbyte)(((int)b & -129) - 65);
				flag2 = ((bytes[num + num2 - 1] - 1) % 10 == 0);
				num3 = (int)(bytes[num] - 1);
			}
			else
			{
				flag = false;
				if (b == 0 && len == 1)
				{
					return double.NegativeInfinity;
				}
				b2 = (sbyte)(((int)(~(int)b) & -129) - 65);
				if (num2 != 20 || bytes[offset + num2] == 102)
				{
					num2--;
				}
				flag2 = ((101 - bytes[num + num2 - 1]) % 10 == 0);
				num3 = (int)(101 - bytes[num]);
			}
			int num4 = num2 << 1;
			if (flag2)
			{
				num4--;
			}
			int num5 = ((int)(b2 + 1) << 1) - num4;
			if (num3 < 10)
			{
				num4--;
			}
			double num16;
			if (num4 <= 15 && ((num5 >= 0 && num5 <= 37 - num4) || (num5 < 0 && num5 >= -22)))
			{
				int num6 = 0;
				int num7 = 0;
				int num8 = 0;
				int num9 = 0;
				int num10 = 0;
				int num11 = 0;
				int num12 = 0;
				if (flag)
				{
					switch (num2)
					{
					case 1:
						break;
					case 2:
						num6 = (int)(bytes[num + 1] - 1);
						break;
					case 3:
						num7 = (int)(bytes[num + 2] - 1);
						num6 = (int)(bytes[num + 1] - 1);
						break;
					case 4:
						num8 = (int)(bytes[num + 3] - 1);
						num7 = (int)(bytes[num + 2] - 1);
						num6 = (int)(bytes[num + 1] - 1);
						break;
					case 5:
						num9 = (int)(bytes[num + 4] - 1);
						num8 = (int)(bytes[num + 3] - 1);
						num7 = (int)(bytes[num + 2] - 1);
						num6 = (int)(bytes[num + 1] - 1);
						break;
					case 6:
						num10 = (int)(bytes[num + 5] - 1);
						num9 = (int)(bytes[num + 4] - 1);
						num8 = (int)(bytes[num + 3] - 1);
						num7 = (int)(bytes[num + 2] - 1);
						num6 = (int)(bytes[num + 1] - 1);
						break;
					case 7:
						num11 = (int)(bytes[num + 6] - 1);
						num10 = (int)(bytes[num + 5] - 1);
						num9 = (int)(bytes[num + 4] - 1);
						num8 = (int)(bytes[num + 3] - 1);
						num7 = (int)(bytes[num + 2] - 1);
						num6 = (int)(bytes[num + 1] - 1);
						break;
					default:
						num12 = (int)(bytes[num + 7] - 1);
						num11 = (int)(bytes[num + 6] - 1);
						num10 = (int)(bytes[num + 5] - 1);
						num9 = (int)(bytes[num + 4] - 1);
						num8 = (int)(bytes[num + 3] - 1);
						num7 = (int)(bytes[num + 2] - 1);
						num6 = (int)(bytes[num + 1] - 1);
						break;
					}
				}
				else
				{
					switch (num2)
					{
					case 1:
						break;
					case 2:
						num6 = (int)(101 - bytes[num + 1]);
						break;
					case 3:
						num7 = (int)(101 - bytes[num + 2]);
						num6 = (int)(101 - bytes[num + 1]);
						break;
					case 4:
						num8 = (int)(101 - bytes[num + 3]);
						num7 = (int)(101 - bytes[num + 2]);
						num6 = (int)(101 - bytes[num + 1]);
						break;
					case 5:
						num9 = (int)(101 - bytes[num + 4]);
						num8 = (int)(101 - bytes[num + 3]);
						num7 = (int)(101 - bytes[num + 2]);
						num6 = (int)(101 - bytes[num + 1]);
						break;
					case 6:
						num10 = (int)(101 - bytes[num + 5]);
						num9 = (int)(101 - bytes[num + 4]);
						num8 = (int)(101 - bytes[num + 3]);
						num7 = (int)(101 - bytes[num + 2]);
						num6 = (int)(101 - bytes[num + 1]);
						break;
					case 7:
						num11 = (int)(101 - bytes[num + 6]);
						num10 = (int)(101 - bytes[num + 5]);
						num9 = (int)(101 - bytes[num + 4]);
						num8 = (int)(101 - bytes[num + 3]);
						num7 = (int)(101 - bytes[num + 2]);
						num6 = (int)(101 - bytes[num + 1]);
						break;
					default:
						num12 = (int)(101 - bytes[num + 7]);
						num11 = (int)(101 - bytes[num + 6]);
						num10 = (int)(101 - bytes[num + 5]);
						num9 = (int)(101 - bytes[num + 4]);
						num8 = (int)(101 - bytes[num + 3]);
						num7 = (int)(101 - bytes[num + 2]);
						num6 = (int)(101 - bytes[num + 1]);
						break;
					}
				}
				double num13;
				if (flag2)
				{
					switch (num2)
					{
					case 2:
						num13 = (double)(num3 * 10 + num6 / 10);
						break;
					case 3:
						num13 = (double)(num3 * 1000 + num6 * 10 + num7 / 10);
						break;
					case 4:
						num13 = (double)(num3 * 100000 + num6 * 1000 + num7 * 10 + num8 / 10);
						break;
					case 5:
						num13 = (double)(num3 * 10000000 + num6 * 100000 + num7 * 1000 + num8 * 10 + num9 / 10);
						break;
					case 6:
					{
						int num14 = num6 * 10000000 + num7 * 100000 + num8 * 1000 + num9 * 10 + num10 / 10;
						num13 = (double)((long)num3 * 1000000000L + (long)num14);
						break;
					}
					case 7:
					{
						int num14 = num7 * 10000000 + num8 * 100000 + num9 * 1000 + num10 * 10 + num11 / 10;
						int num15 = num3 * 100 + num6;
						num13 = (double)((long)num15 * 1000000000L + (long)num14);
						break;
					}
					case 8:
					{
						int num14 = num8 * 10000000 + num9 * 100000 + num10 * 1000 + num11 * 10 + num12 / 10;
						int num15 = num3 * 10000 + num6 * 100 + num7;
						num13 = (double)((long)num15 * 1000000000L + (long)num14);
						break;
					}
					default:
						num13 = (double)(num3 / 10);
						break;
					}
				}
				else
				{
					switch (num2)
					{
					case 2:
						num13 = (double)(num3 * 100 + num6);
						break;
					case 3:
						num13 = (double)(num3 * 10000 + num6 * 100 + num7);
						break;
					case 4:
						num13 = (double)(num3 * 1000000 + num6 * 10000 + num7 * 100 + num8);
						break;
					case 5:
					{
						int num14 = num6 * 1000000 + num7 * 10000 + num8 * 100 + num9;
						num13 = (double)((long)num3 * 100000000L + (long)num14);
						break;
					}
					case 6:
					{
						int num14 = num7 * 1000000 + num8 * 10000 + num9 * 100 + num10;
						int num15 = num3 * 100 + num6;
						num13 = (double)((long)num15 * 100000000L + (long)num14);
						break;
					}
					case 7:
					{
						int num14 = num8 * 1000000 + num9 * 10000 + num10 * 100 + num11;
						int num15 = num3 * 10000 + num6 * 100 + num7;
						num13 = (double)((long)num15 * 100000000L + (long)num14);
						break;
					}
					case 8:
					{
						int num14 = num9 * 1000000 + num10 * 10000 + num11 * 100 + num12;
						int num15 = num3 * 1000000 + num6 * 10000 + num7 * 100 + num8;
						num13 = (double)((long)num15 * 100000000L + (long)num14);
						break;
					}
					default:
						num13 = (double)num3;
						break;
					}
				}
				if (num5 == 0 || num13 == 0.0)
				{
					num16 = num13;
				}
				else if (num5 >= 0)
				{
					if (num5 <= 22)
					{
						num16 = num13 * HelperClass.small10pow[num5];
					}
					else
					{
						int num17 = 15 - num4;
						num13 *= HelperClass.small10pow[num17];
						num16 = num13 * HelperClass.small10pow[num5 - num17];
					}
				}
				else
				{
					num16 = num13 / HelperClass.small10pow[-num5];
				}
			}
			else
			{
				int num18 = 0;
				int num19 = 0;
				int num20 = 0;
				int num21 = 0;
				int num22 = 0;
				int num23 = 0;
				int num24 = 0;
				int num25 = 0;
				int num26 = 0;
				int num27 = 0;
				int num28 = 0;
				int num29 = 0;
				int num30 = 0;
				int num31 = 0;
				bool flag3 = false;
				int num32;
				if (flag)
				{
					int i;
					if ((num2 & 1) != 0)
					{
						i = 2;
						num32 = num3;
					}
					else
					{
						i = 3;
						num32 = num3 * 100 + (int)(bytes[num + 1] - 1);
					}
					while (i < num2)
					{
						int num33 = (int)((bytes[num + i - 1] - 1) * 100 + (bytes[num + i] - 1)) + num32 * 10000;
						switch (num26)
						{
						case 0:
							num32 = (num33 & 65535);
							break;
						case 1:
							num32 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num18 * 10000;
							num18 = (num33 & 65535);
							break;
						case 2:
							num32 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num18 * 10000;
							num18 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num19 * 10000;
							num19 = (num33 & 65535);
							break;
						case 3:
							num32 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num18 * 10000;
							num18 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num19 * 10000;
							num19 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num20 * 10000;
							num20 = (num33 & 65535);
							break;
						case 4:
							num32 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num18 * 10000;
							num18 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num19 * 10000;
							num19 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num20 * 10000;
							num20 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num21 * 10000;
							num21 = (num33 & 65535);
							break;
						case 5:
							num32 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num18 * 10000;
							num18 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num19 * 10000;
							num19 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num20 * 10000;
							num20 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num21 * 10000;
							num21 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num22 * 10000;
							num22 = (num33 & 65535);
							break;
						case 6:
							num32 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num18 * 10000;
							num18 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num19 * 10000;
							num19 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num20 * 10000;
							num20 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num21 * 10000;
							num21 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num22 * 10000;
							num22 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num23 * 10000;
							num23 = (num33 & 65535);
							break;
						case 7:
							num32 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num18 * 10000;
							num18 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num19 * 10000;
							num19 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num20 * 10000;
							num20 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num21 * 10000;
							num21 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num22 * 10000;
							num22 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num23 * 10000;
							num23 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num24 * 10000;
							num24 = (num33 & 65535);
							break;
						default:
							num32 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num18 * 10000;
							num18 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num19 * 10000;
							num19 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num20 * 10000;
							num20 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num21 * 10000;
							num21 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num22 * 10000;
							num22 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num23 * 10000;
							num23 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num24 * 10000;
							num24 = (num33 & 65535);
							num33 = (num33 >> 16 & 65535) + num25 * 10000;
							num25 = (num33 & 65535);
							break;
						}
						num33 = (num33 >> 16 & 65535);
						if (num33 != 0)
						{
							num26++;
							switch (num26)
							{
							case 1:
								num18 = num33;
								break;
							case 2:
								num19 = num33;
								break;
							case 3:
								num20 = num33;
								break;
							case 4:
								num21 = num33;
								break;
							case 5:
								num22 = num33;
								break;
							case 6:
								num23 = num33;
								break;
							case 7:
								num24 = num33;
								break;
							case 8:
								num25 = num33;
								break;
							}
						}
						i += 2;
					}
				}
				else
				{
					int i;
					if ((num2 & 1) != 0)
					{
						i = 2;
						num32 = num3;
					}
					else
					{
						i = 3;
						num32 = num3 * 100 + (int)(101 - bytes[num + 1]);
					}
					while (i < num2)
					{
						int num34 = (int)((101 - bytes[num + i - 1]) * 100 + (101 - bytes[num + i])) + num32 * 10000;
						switch (num26)
						{
						case 0:
							num32 = (num34 & 65535);
							break;
						case 1:
							num32 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num18 * 10000;
							num18 = (num34 & 65535);
							break;
						case 2:
							num32 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num18 * 10000;
							num18 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num19 * 10000;
							num19 = (num34 & 65535);
							break;
						case 3:
							num32 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num18 * 10000;
							num18 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num19 * 10000;
							num19 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num20 * 10000;
							num20 = (num34 & 65535);
							break;
						case 4:
							num32 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num18 * 10000;
							num18 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num19 * 10000;
							num19 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num20 * 10000;
							num20 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num21 * 10000;
							num21 = (num34 & 65535);
							break;
						case 5:
							num32 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num18 * 10000;
							num18 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num19 * 10000;
							num19 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num20 * 10000;
							num20 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num21 * 10000;
							num21 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num22 * 10000;
							num22 = (num34 & 65535);
							break;
						case 6:
							num32 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num18 * 10000;
							num18 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num19 * 10000;
							num19 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num20 * 10000;
							num20 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num21 * 10000;
							num21 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num22 * 10000;
							num22 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num23 * 10000;
							num23 = (num34 & 65535);
							break;
						case 7:
							num32 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num18 * 10000;
							num18 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num19 * 10000;
							num19 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num20 * 10000;
							num20 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num21 * 10000;
							num21 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num22 * 10000;
							num22 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num23 * 10000;
							num23 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num24 * 10000;
							num24 = (num34 & 65535);
							break;
						default:
							num32 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num18 * 10000;
							num18 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num19 * 10000;
							num19 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num20 * 10000;
							num20 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num21 * 10000;
							num21 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num22 * 10000;
							num22 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num23 * 10000;
							num23 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num24 * 10000;
							num24 = (num34 & 65535);
							num34 = (num34 >> 16 & 65535) + num25 * 10000;
							num25 = (num34 & 65535);
							break;
						}
						num34 = (num34 >> 16 & 65535);
						if (num34 != 0)
						{
							num26++;
							switch (num26)
							{
							case 1:
								num18 = num34;
								break;
							case 2:
								num19 = num34;
								break;
							case 3:
								num20 = num34;
								break;
							case 4:
								num21 = num34;
								break;
							case 5:
								num22 = num34;
								break;
							case 6:
								num23 = num34;
								break;
							case 7:
								num24 = num34;
								break;
							case 8:
								num25 = num34;
								break;
							}
						}
						i += 2;
					}
				}
				int num35 = num26;
				num26++;
				int num36 = (int)(62 - b2) + num2;
				int num37 = HelperClass.nexpdigstable[num36];
				int[] array = HelperClass.expdigstable[num36];
				int num38 = num26 + 5;
				int num39 = 0;
				if (num37 > num38)
				{
					num39 = num37 - num38;
					num37 = num38;
				}
				int num40 = 0;
				int num41 = 0;
				int num42 = num37 - 1 + (num26 - 1) - 4;
				int j;
				for (j = 0; j < num42; j++)
				{
					int num43 = num41 & 65535;
					num41 = (num41 >> 16 & 65535);
					int num44 = (num26 < j + 1) ? num26 : (j + 1);
					for (int k = (j - num37 + 1 > 0) ? (j - num37 + 1) : 0; k < num44; k++)
					{
						int num45 = num39 + j - k;
						int num46;
						switch (k)
						{
						case 1:
							num46 = num18 * array[num45];
							break;
						case 2:
							num46 = num19 * array[num45];
							break;
						case 3:
							num46 = num20 * array[num45];
							break;
						case 4:
							num46 = num21 * array[num45];
							break;
						case 5:
							num46 = num22 * array[num45];
							break;
						case 6:
							num46 = num23 * array[num45];
							break;
						case 7:
							num46 = num24 * array[num45];
							break;
						case 8:
							num46 = num25 * array[num45];
							break;
						default:
							num46 = num32 * array[num45];
							break;
						}
						num43 += (num46 & 65535);
						num41 += (num46 >> 16 & 65535);
					}
					flag3 = (flag3 || (num43 & 65535) != 0);
					num41 += (num43 >> 16 & 65535);
				}
				num42 += 5;
				while (j < num42)
				{
					int num47 = num41 & 65535;
					num41 = (num41 >> 16 & 65535);
					int num48 = (num26 < j + 1) ? num26 : (j + 1);
					for (int k = (j - num37 + 1 > 0) ? (j - num37 + 1) : 0; k < num48; k++)
					{
						int num49 = num39 + j - k;
						int num50;
						switch (k)
						{
						case 1:
							num50 = num18 * array[num49];
							break;
						case 2:
							num50 = num19 * array[num49];
							break;
						case 3:
							num50 = num20 * array[num49];
							break;
						case 4:
							num50 = num21 * array[num49];
							break;
						case 5:
							num50 = num22 * array[num49];
							break;
						case 6:
							num50 = num23 * array[num49];
							break;
						case 7:
							num50 = num24 * array[num49];
							break;
						case 8:
							num50 = num25 * array[num49];
							break;
						default:
							num50 = num32 * array[num49];
							break;
						}
						num47 += (num50 & 65535);
						num41 += (num50 >> 16 & 65535);
					}
					switch (num40++)
					{
					case 1:
						num28 = (num47 & 65535);
						break;
					case 2:
						num29 = (num47 & 65535);
						break;
					case 3:
						num30 = (num47 & 65535);
						break;
					case 4:
						num31 = (num47 & 65535);
						break;
					default:
						num27 = (num47 & 65535);
						break;
					}
					num41 += (num47 >> 16 & 65535);
					j++;
				}
				while (num41 != 0)
				{
					if (num40 < 5)
					{
						switch (num40++)
						{
						case 1:
							num28 = (num41 & 65535);
							break;
						case 2:
							num29 = (num41 & 65535);
							break;
						case 3:
							num30 = (num41 & 65535);
							break;
						case 4:
							num31 = (num41 & 65535);
							break;
						default:
							num27 = (num41 & 65535);
							break;
						}
					}
					else
					{
						flag3 = (flag3 || num27 != 0);
						num27 = num28;
						num28 = num29;
						num29 = num30;
						num30 = num31;
						num31 = (num41 & 65535);
					}
					num41 = (num41 >> 16 & 65535);
					num35++;
				}
				int num51 = (HelperClass.binexpstable[num36] + num35) * 16 - 1;
				int num52;
				switch (num40)
				{
				case 2:
					num52 = num28;
					break;
				case 3:
					num52 = num29;
					break;
				case 4:
					num52 = num30;
					break;
				case 5:
					num52 = num31;
					break;
				default:
					num52 = num27;
					break;
				}
				for (int num53 = num52 >> 1; num53 != 0; num53 >>= 1)
				{
					num51++;
				}
				int num54 = 5;
				int num55 = num52 << 5;
				int num56 = 0;
				num41 = 0;
				while ((num55 & 1048576) == 0)
				{
					num55 <<= 1;
					num54++;
				}
				switch (num40)
				{
				case 2:
					if (num54 > 16)
					{
						num55 |= num27 << num54 - 16;
					}
					else if (num54 == 16)
					{
						num55 |= num27;
					}
					else
					{
						num55 |= num27 >> 16 - num54;
						num56 = num27 << 16 + num54;
					}
					break;
				case 3:
					if (num54 > 16)
					{
						num55 |= (num28 << num54 - 16 | num27 >> 32 - num54);
						num56 = num27 << num54;
					}
					else if (num54 == 16)
					{
						num55 |= num28;
						num56 = num27 << 16;
					}
					else
					{
						num55 |= num28 >> 16 - num54;
						num56 = num28 << 16 + num54;
						num56 |= num27 << num54;
					}
					break;
				case 4:
					if (num54 > 16)
					{
						num55 |= (num29 << num54 - 16 | num28 >> 32 - num54);
						num56 = (num28 << num54 | num27 << num54 - 16);
					}
					else if (num54 == 16)
					{
						num55 |= num29;
						num56 = (num28 << 16 | num27);
					}
					else
					{
						num55 |= num29 >> 16 - num54;
						num56 = (num29 << 16 + num54 | num28 << num54 | num27 >> 16 - num54);
						num41 = (num27 & 1 << 15 - num54);
						if (num54 < 15)
						{
							flag3 = (flag3 || num27 << num54 + 17 != 0);
						}
					}
					break;
				case 5:
					if (num54 > 16)
					{
						num55 |= (num30 << num54 - 16 | num29 >> 32 - num54);
						num56 = (num29 << num54 | num28 << num54 - 16 | num27 >> 32 - num54);
						num41 = (num27 & 1 << 31 - num54);
						flag3 = (flag3 || num27 << num54 + 1 != 0);
					}
					else if (num54 == 16)
					{
						num55 |= num30;
						num56 = (num29 << 16 | num28);
						num41 = (num27 & 32768);
						flag3 = (flag3 || (num27 & 32767) != 0);
					}
					else
					{
						num55 |= num30 >> 16 - num54;
						num56 = (num30 << 16 + num54 | num29 << num54 | num28 >> 16 - num54);
						num41 = (num28 & 1 << 15 - num54);
						if (num54 < 15)
						{
							flag3 = (flag3 || num28 << num54 + 17 != 0);
						}
						flag3 = (flag3 || num27 != 0);
					}
					break;
				}
				if (num41 != 0 && (flag3 || (num56 & 1) != 0))
				{
					if ((long)num56 == (long)((ulong)-1))
					{
						num56 = 0;
						num55++;
						if ((num55 & 2097152) != 0)
						{
							num56 = (num56 >> 1 | num55 << 31);
							num55 >>= 1;
							num51++;
						}
					}
					else
					{
						num56++;
					}
				}
				long value = (long)num51 << 52 | (long)(num55 & 1048575) << 32 | ((long)num56 & (long)((ulong)-1));
				num16 = BitConverter.Int64BitsToDouble(value);
			}
			return flag ? num16 : (-num16);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0003C47C File Offset: 0x0003A67C
		internal static float GetFloat(OraType oraType, byte[] bytes, int offset, int len)
		{
			if (oraType != OraType.ORA_IBFLOAT)
			{
				return HelperClass.GetFloat(bytes, offset, len);
			}
			return TTCBinaryFloatAccessor.GetFloatFromByteArray(bytes, offset);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0003C494 File Offset: 0x0003A694
		internal static double GetDouble(OraType oraType, byte[] bytes, int offset, int len)
		{
			if (oraType != OraType.ORA_IBDOUBLE)
			{
				return HelperClass.GetDouble(bytes, offset, len);
			}
			return TTCBinaryDoubleAccessor.GetDoubleFromByteArray(bytes, offset);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0003C4AC File Offset: 0x0003A6AC
		internal static OracleDecimal GetOracleDecimal(OraType oraType, byte[] bytes, int offset)
		{
			OracleDecimal @null = OracleDecimal.Null;
			if (bytes != null)
			{
				if (oraType == OraType.ORA_IBDOUBLE)
				{
					@null = new OracleDecimal(TTCBinaryDoubleAccessor.GetDoubleFromByteArray(bytes, offset));
				}
				else if (oraType == OraType.ORA_IBFLOAT)
				{
					@null = new OracleDecimal(TTCBinaryFloatAccessor.GetFloatFromByteArray(bytes, offset));
				}
				else
				{
					@null = new OracleDecimal(bytes, false);
				}
			}
			return @null;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0003C4F8 File Offset: 0x0003A6F8
		internal static float GetFloat(byte[] bytes, int offset, int len)
		{
			byte b = bytes[offset];
			int num = offset + 1;
			if (OracleNumberCore.IsNaN(bytes, offset, len))
			{
				return float.NaN;
			}
			double num4;
			if ((b & 128) != 0)
			{
				if (b == 128 && len == 1)
				{
					return 0f;
				}
				if (len == 2 && b == 255 && bytes[offset + 1] == 101)
				{
					return float.PositiveInfinity;
				}
				sbyte b2 = (sbyte)(((int)b & -129) - 65);
				int num2 = len - 1;
				while (bytes[num] == 1 && num2 > 0)
				{
					num++;
					num2--;
					b2 -= 1;
				}
				int num3 = (int)(HelperClass.tablemaxexponent - (double)b2);
				switch (num2)
				{
				case 1:
					num4 = (double)(bytes[num] - 1) * HelperClass.factorTable[num3];
					break;
				case 2:
					num4 = (double)((bytes[num] - 1) * 100 + (bytes[num + 1] - 1)) * HelperClass.factorTable[num3 + 1];
					break;
				case 3:
					num4 = (double)((int)(bytes[num] - 1) * 10000 + (int)((bytes[num + 1] - 1) * 100) + (int)(bytes[num + 2] - 1)) * HelperClass.factorTable[num3 + 2];
					break;
				case 4:
					num4 = (double)((int)(bytes[num] - 1) * 1000000 + (int)(bytes[num + 1] - 1) * 10000 + (int)((bytes[num + 2] - 1) * 100) + (int)(bytes[num + 3] - 1)) * HelperClass.factorTable[num3 + 3];
					break;
				case 5:
					num4 = (double)((int)(bytes[num + 1] - 1) * 1000000 + (int)(bytes[num + 2] - 1) * 10000 + (int)((bytes[num + 3] - 1) * 100) + (int)(bytes[num + 4] - 1)) * HelperClass.factorTable[num3 + 4] + (double)(bytes[num] - 1) * HelperClass.factorTable[num3];
					break;
				case 6:
					num4 = (double)((int)(bytes[num + 2] - 1) * 1000000 + (int)(bytes[num + 3] - 1) * 10000 + (int)((bytes[num + 4] - 1) * 100) + (int)(bytes[num + 5] - 1)) * HelperClass.factorTable[num3 + 5] + (double)((bytes[num] - 1) * 100 + (bytes[num + 1] - 1)) * HelperClass.factorTable[num3 + 1];
					break;
				default:
					num4 = (double)((int)(bytes[num + 3] - 1) * 1000000 + (int)(bytes[num + 4] - 1) * 10000 + (int)((bytes[num + 5] - 1) * 100) + (int)(bytes[num + 6] - 1)) * HelperClass.factorTable[num3 + 6] + (double)((int)(bytes[num] - 1) * 10000 + (int)((bytes[num + 1] - 1) * 100) + (int)(bytes[num + 2] - 1)) * HelperClass.factorTable[num3 + 2];
					break;
				}
			}
			else
			{
				if (b == 0 && len == 1)
				{
					return float.NegativeInfinity;
				}
				sbyte b2 = (sbyte)(((int)(~(int)b) & -129) - 65);
				int num2 = len - 1;
				if (num2 != 20 || bytes[offset + num2] == 102)
				{
					num2--;
				}
				while (bytes[num] == 1 && num2 > 0)
				{
					num++;
					num2--;
					b2 -= 1;
				}
				int num3 = (int)(HelperClass.tablemaxexponent - (double)b2);
				switch (num2)
				{
				case 1:
					num4 = (double)(-(double)(101 - bytes[num])) * HelperClass.factorTable[num3];
					break;
				case 2:
					num4 = (double)(-(double)((101 - bytes[num]) * 100 + (101 - bytes[num + 1]))) * HelperClass.factorTable[num3 + 1];
					break;
				case 3:
					num4 = (double)(-(double)((int)(101 - bytes[num]) * 10000 + (int)((101 - bytes[num + 1]) * 100) + (int)(101 - bytes[num + 2]))) * HelperClass.factorTable[num3 + 2];
					break;
				case 4:
					num4 = (double)(-(double)((int)(101 - bytes[num]) * 1000000 + (int)(101 - bytes[num + 1]) * 10000 + (int)((101 - bytes[num + 2]) * 100) + (int)(101 - bytes[num + 3]))) * HelperClass.factorTable[num3 + 3];
					break;
				case 5:
					num4 = -((double)((int)(101 - bytes[num + 1]) * 1000000 + (int)(101 - bytes[num + 2]) * 10000 + (int)((101 - bytes[num + 3]) * 100) + (int)(101 - bytes[num + 4])) * HelperClass.factorTable[num3 + 4] + (double)(101 - bytes[num]) * HelperClass.factorTable[num3]);
					break;
				case 6:
					num4 = -((double)((int)(101 - bytes[num + 2]) * 1000000 + (int)(101 - bytes[num + 3]) * 10000 + (int)((101 - bytes[num + 4]) * 100) + (int)(101 - bytes[num + 5])) * HelperClass.factorTable[num3 + 5] + (double)((101 - bytes[num]) * 100 + (101 - bytes[num + 1])) * HelperClass.factorTable[num3 + 1]);
					break;
				default:
					num4 = -((double)((int)(101 - bytes[num + 3]) * 1000000 + (int)(101 - bytes[num + 4]) * 10000 + (int)((101 - bytes[num + 5]) * 100) + (int)(101 - bytes[num + 6])) * HelperClass.factorTable[num3 + 6] + (double)((int)(101 - bytes[num]) * 10000 + (int)((101 - bytes[num + 1]) * 100) + (int)(101 - bytes[num + 2])) * HelperClass.factorTable[num3 + 2]);
					break;
				}
			}
			return (float)num4;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0003CA08 File Offset: 0x0003AC08
		internal static int GetInt(byte[] bytes, int offset, int length)
		{
			if (bytes == null)
			{
				return 0;
			}
			byte b = bytes[offset];
			int num = 0;
			byte b2 = (byte)(length - 1);
			if ((b & 128) != 0)
			{
				sbyte b3 = (sbyte)(((int)b & -129) - 65);
				int num2 = (int)((b2 > (byte)(b3 + 1)) ? (b3 + 2) : ((sbyte)(b2 + 1)));
				int num3 = num2 + offset;
				if (b3 >= 4)
				{
					if (b3 > 4)
					{
						throw new OverflowException();
					}
					long num4 = 0L;
					if (num2 > 1)
					{
						num4 = (long)(bytes[offset + 1] - 1);
						for (int i = 2 + offset; i < num3; i++)
						{
							num4 = num4 * 100L + (long)(bytes[i] - 1);
						}
					}
					for (int j = (int)(b3 - (sbyte)b2); j >= 0; j--)
					{
						num4 *= 100L;
					}
					if (num4 > 2147483647L)
					{
						throw new OverflowException();
					}
					num = (int)num4;
				}
				else
				{
					if (num2 > 1)
					{
						num = (int)(bytes[offset + 1] - 1);
						for (int k = 2 + offset; k < num3; k++)
						{
							num = num * 100 + (int)(bytes[k] - 1);
						}
					}
					for (int l = (int)(b3 - (sbyte)b2); l >= 0; l--)
					{
						num *= 100;
					}
				}
			}
			else
			{
				sbyte b3 = (sbyte)(((int)(~(int)b) & -129) - 65);
				if (b2 != 20 || bytes[offset + (int)b2] == 102)
				{
					b2 -= 1;
				}
				int num5 = (int)((b2 > (byte)(b3 + 1)) ? (b3 + 2) : ((sbyte)(b2 + 1)));
				int num6 = num5 + offset;
				if (b3 >= 4)
				{
					if (b3 > 4)
					{
						throw new OverflowException();
					}
					long num7 = 0L;
					if (num5 > 1)
					{
						num7 = (long)(101 - bytes[offset + 1]);
						for (int m = 2 + offset; m < num6; m++)
						{
							num7 = num7 * 100L + (long)(101 - bytes[m]);
						}
					}
					for (int n = (int)(b3 - (sbyte)b2); n >= 0; n--)
					{
						num7 *= 100L;
					}
					num7 = -num7;
					if (num7 < -2147483648L)
					{
						throw new OverflowException();
					}
					num = (int)num7;
				}
				else
				{
					if (num5 > 1)
					{
						num = (int)(101 - bytes[offset + 1]);
						for (int num8 = 2 + offset; num8 < num6; num8++)
						{
							num = num * 100 + (int)(101 - bytes[num8]);
						}
					}
					for (int num9 = (int)(b3 - (sbyte)b2); num9 >= 0; num9--)
					{
						num *= 100;
					}
					num = -num;
				}
			}
			return num;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0003CC1C File Offset: 0x0003AE1C
		internal static long GetLong(byte[] bytes, int offset, int length)
		{
			byte b = bytes[offset];
			long num = 0L;
			if ((b & 128) != 0)
			{
				if (b == 128 && length == 1)
				{
					return 0L;
				}
				byte b2 = (byte)(((int)b & -129) - 65);
				if (b2 > 9)
				{
					throw new OverflowException();
				}
				if (b2 == 9)
				{
					int i = 1;
					int num2 = length;
					if (length > 11)
					{
						num2 = 11;
					}
					while (i < num2)
					{
						int num3 = (int)(bytes[offset + i] & byte.MaxValue);
						int num4 = HelperClass.MAX_LONG[i];
						if (num3 != num4)
						{
							if (num3 >= num4)
							{
								throw new OverflowException();
							}
							break;
						}
						else
						{
							i++;
						}
					}
					if (i == num2 && length > 11)
					{
						throw new OverflowException();
					}
				}
				byte b3 = (byte)(length - 1);
				int num5 = (int)((b3 > b2 + 1) ? (b2 + 2) : (b3 + 1));
				int num6 = num5 + offset;
				if (num5 > 1)
				{
					num = (long)(bytes[offset + 1] - 1);
					for (int j = 2 + offset; j < num6; j++)
					{
						num = num * 100L + (long)(bytes[j] - 1);
					}
				}
				for (int k = (int)(b2 - b3); k >= 0; k--)
				{
					num *= 100L;
				}
			}
			else
			{
				byte b2 = (byte)(((int)(~(int)b) & -129) - 65);
				if (b2 > 9)
				{
					throw new OverflowException();
				}
				if (b2 == 9)
				{
					int l = 1;
					int num7 = length;
					if (length > 12)
					{
						num7 = 12;
					}
					while (l < num7)
					{
						int num8 = (int)(bytes[offset + l] & byte.MaxValue);
						int num9 = HelperClass.MIN_LONG[l];
						if (num8 != num9)
						{
							if (num8 <= num9)
							{
								throw new OverflowException();
							}
							break;
						}
						else
						{
							l++;
						}
					}
					if (l == num7 && length < 12)
					{
						throw new OverflowException();
					}
				}
				byte b3 = (byte)(length - 1);
				if (b3 != 20 || bytes[offset + (int)b3] == 102)
				{
					b3 -= 1;
				}
				int num10 = (int)((b3 > b2 + 1) ? (b2 + 2) : (b3 + 1));
				int num11 = num10 + offset;
				if (num10 > 1)
				{
					num = (long)(101 - bytes[offset + 1]);
					for (int m = 2 + offset; m < num11; m++)
					{
						num = num * 100L + (long)(101 - bytes[m]);
					}
				}
				for (int n = (int)(b2 - b3); n >= 0; n--)
				{
					num *= 100L;
				}
				num = -num;
			}
			return num;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0003CE38 File Offset: 0x0003B038
		internal static string GetZeros(int count)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (count-- > 0)
			{
				stringBuilder.Append("0");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0003CE68 File Offset: 0x0003B068
		internal static string RemoveSingleQuotes(string strInput)
		{
			if (string.IsNullOrEmpty(strInput))
			{
				return strInput;
			}
			if (strInput.StartsWith("'") && strInput.EndsWith("'"))
			{
				strInput = strInput.Substring(1, strInput.Length - 2);
			}
			return strInput;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0003CEA0 File Offset: 0x0003B0A0
		internal static string RemoveDoubleQuotes(string strInput)
		{
			if (string.IsNullOrEmpty(strInput))
			{
				return strInput;
			}
			if (strInput.StartsWith("\"") && strInput.EndsWith("\""))
			{
				strInput = strInput.Substring(1, strInput.Length - 2);
			}
			return strInput;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0003CED8 File Offset: 0x0003B0D8
		internal static string RemoveSingleAndDoubleQuotes(string strInput)
		{
			if (string.IsNullOrEmpty(strInput))
			{
				return strInput;
			}
			if (strInput.StartsWith("'") && strInput.EndsWith("'"))
			{
				strInput = strInput.Substring(1, strInput.Length - 2);
			}
			if (strInput.StartsWith("\"") && strInput.EndsWith("\""))
			{
				strInput = strInput.Substring(1, strInput.Length - 2);
			}
			return strInput;
		}

		// Token: 0x0400084E RID: 2126
		internal const string SELECT_CLAUSE = "SELECT";

		// Token: 0x0400084F RID: 2127
		internal const string INSERT_CLAUSE = "INSERT";

		// Token: 0x04000850 RID: 2128
		internal const string UPDATE_CLAUSE = "UPDATE";

		// Token: 0x04000851 RID: 2129
		internal const string DELETE_CLAUSE = "DELETE";

		// Token: 0x04000852 RID: 2130
		internal const string MERGE_CLAUSE = "MERGE";

		// Token: 0x04000853 RID: 2131
		internal const string WITH_CLAUSE = "WITH";

		// Token: 0x04000854 RID: 2132
		internal const string RETURNING_PATTERN = "\\bRETURNING\\b | \\bRETURN\\b ";

		// Token: 0x04000855 RID: 2133
		private const int LNXSGNBT = 128;

		// Token: 0x04000856 RID: 2134
		private const byte LNXDIGS = 20;

		// Token: 0x04000857 RID: 2135
		private const byte LNXEXPBS = 64;

		// Token: 0x04000858 RID: 2136
		private const byte MAX_INT_EXPONENT = 4;

		// Token: 0x04000859 RID: 2137
		private const byte MIN_INT_EXPONENT = 4;

		// Token: 0x0400085A RID: 2138
		private const byte MAX_LONG_EXPONENT = 9;

		// Token: 0x0400085B RID: 2139
		private const byte MIN_LONG_EXPONENT = 9;

		// Token: 0x0400085C RID: 2140
		private const int MAX_LONG_length = 11;

		// Token: 0x0400085D RID: 2141
		private const int MIN_LONG_length = 12;

		// Token: 0x0400085E RID: 2142
		private const bool GET_XXX_ROUNDS = false;

		// Token: 0x0400085F RID: 2143
		private const int LNXM_NUM = 22;

		// Token: 0x04000860 RID: 2144
		private const byte LENINDEX = 0;

		// Token: 0x04000861 RID: 2145
		private const byte EXPINDEX = 1;

		// Token: 0x04000862 RID: 2146
		private const byte DIGITINDEX = 2;

		// Token: 0x04000863 RID: 2147
		private const byte POSEXPOFFSET = 193;

		// Token: 0x04000864 RID: 2148
		private const byte POSDIGITOFFSET = 1;

		// Token: 0x04000865 RID: 2149
		private const byte DEC_MAX_PRECISION = 29;

		// Token: 0x04000866 RID: 2150
		private const byte NEGDIGITOFFSET = 101;

		// Token: 0x04000867 RID: 2151
		private const byte NEGEXPOFFSET = 62;

		// Token: 0x04000868 RID: 2152
		private const int LNXEXPMX = 127;

		// Token: 0x04000869 RID: 2153
		internal static char[] WHITE_SPACE_DELIMS = new char[]
		{
			' ',
			'\t',
			'\r',
			'\n'
		};

		// Token: 0x0400086A RID: 2154
		private static int[] MAX_LONG = new int[]
		{
			202,
			10,
			23,
			34,
			73,
			4,
			69,
			55,
			78,
			59,
			8
		};

		// Token: 0x0400086B RID: 2155
		private static int[] MIN_LONG = new int[]
		{
			53,
			92,
			79,
			68,
			29,
			98,
			33,
			47,
			24,
			43,
			93,
			102
		};

		// Token: 0x0400086C RID: 2156
		private static double tablemaxexponent = 127.0;

		// Token: 0x0400086D RID: 2157
		private static double[] factorTable = new double[]
		{
			1E+254,
			1E+252,
			1E+250,
			1E+248,
			1E+246,
			1E+244,
			1E+242,
			1E+240,
			1E+238,
			1E+236,
			1E+234,
			1E+232,
			1E+230,
			1E+228,
			1E+226,
			1E+224,
			1E+222,
			1E+220,
			1E+218,
			1E+216,
			1E+214,
			1E+212,
			1E+210,
			1E+208,
			1E+206,
			1E+204,
			1E+202,
			1E+200,
			1E+198,
			1E+196,
			1E+194,
			1E+192,
			1E+190,
			1E+188,
			1E+186,
			1E+184,
			1E+182,
			1E+180,
			1E+178,
			1E+176,
			1E+174,
			1E+172,
			1E+170,
			1E+168,
			1E+166,
			1E+164,
			1E+162,
			1E+160,
			1E+158,
			1E+156,
			1E+154,
			1E+152,
			1E+150,
			1E+148,
			1E+146,
			1E+144,
			1E+142,
			1E+140,
			1E+138,
			1E+136,
			1E+134,
			1E+132,
			1E+130,
			1E+128,
			1.0000000000000001E+126,
			1E+124,
			1E+122,
			1E+120,
			1E+118,
			1E+116,
			1E+114,
			1E+112,
			1E+110,
			1E+108,
			1E+106,
			1E+104,
			1E+102,
			1E+100,
			1E+98,
			1E+96,
			1E+94,
			1E+92,
			1E+90,
			1E+88,
			1E+86,
			1E+84,
			1E+82,
			1E+80,
			1E+78,
			1E+76,
			1E+74,
			1E+72,
			1E+70,
			1E+68,
			1E+66,
			1E+64,
			1E+62,
			1E+60,
			1E+58,
			1E+56,
			1E+54,
			1E+52,
			1E+50,
			1E+48,
			1E+46,
			1E+44,
			1E+42,
			1E+40,
			1E+38,
			1E+36,
			1E+34,
			1E+32,
			1E+30,
			1E+28,
			1E+26,
			1E+24,
			1E+22,
			1E+20,
			1E+18,
			10000000000000000.0,
			100000000000000.0,
			1000000000000.0,
			10000000000.0,
			100000000.0,
			1000000.0,
			10000.0,
			100.0,
			1.0,
			0.01,
			0.0001,
			1E-06,
			1E-08,
			1E-10,
			1E-12,
			1E-14,
			1E-16,
			1E-18,
			1E-20,
			1E-22,
			1E-24,
			1E-26,
			1E-28,
			1E-30,
			1E-32,
			1E-34,
			1E-36,
			1E-38,
			1E-40,
			1E-42,
			1E-44,
			1E-46,
			1E-48,
			1E-50,
			1E-52,
			1E-54,
			1E-56,
			1E-58,
			1E-60,
			1E-62,
			1E-64,
			1E-66,
			1E-68,
			1E-70,
			1E-72,
			1E-74,
			1E-76,
			1E-78,
			1E-80,
			1E-82,
			1E-84,
			1E-86,
			1E-88,
			1E-90,
			1E-92,
			1E-94,
			1E-96,
			1E-98,
			1E-100,
			1E-102,
			1E-104,
			1E-106,
			1E-108,
			1E-110,
			1E-112,
			1E-114,
			1E-116,
			1E-118,
			1E-120,
			1E-122,
			1E-124,
			1E-126,
			1E-128,
			1E-130,
			1E-132,
			1E-134,
			1E-136,
			1E-138,
			1E-140,
			1E-142,
			1E-144,
			1E-146,
			1E-148,
			1E-150,
			1E-152,
			1E-154,
			1E-156,
			1E-158,
			1E-160,
			1E-162,
			1E-164,
			1E-166,
			1E-168,
			1E-170,
			1E-172,
			1E-174,
			1E-176,
			1E-178,
			1E-180,
			1E-182,
			1E-184,
			1E-186,
			1E-188,
			1E-190,
			1E-192,
			1E-194,
			1E-196,
			1E-198,
			1E-200,
			1E-202,
			1E-204,
			1E-206,
			1E-208,
			1E-210,
			1E-212,
			1E-214,
			1E-216,
			1E-218,
			1E-220,
			1E-222,
			1E-224,
			1E-226,
			1E-228,
			1E-230,
			1E-232,
			1E-234,
			1E-236,
			1E-238,
			1E-240,
			1E-242,
			1E-244,
			1E-246,
			1E-248,
			1E-250,
			1E-252,
			1E-254
		};

		// Token: 0x0400086E RID: 2158
		private static double[] small10pow = new double[]
		{
			1.0,
			10.0,
			100.0,
			1000.0,
			10000.0,
			100000.0,
			1000000.0,
			10000000.0,
			100000000.0,
			1000000000.0,
			10000000000.0,
			100000000000.0,
			1000000000000.0,
			10000000000000.0,
			100000000000000.0,
			1000000000000000.0,
			10000000000000000.0,
			1E+17,
			1E+18,
			1E+19,
			1E+20,
			1E+21,
			1E+22
		};

		// Token: 0x0400086F RID: 2159
		private static int[] expdigs0 = new int[]
		{
			25597,
			55634,
			18440,
			18324,
			42485,
			50370,
			56862,
			11593,
			45703,
			57341,
			10255,
			12549,
			59579,
			5
		};

		// Token: 0x04000870 RID: 2160
		private static int[] expdigs1 = new int[]
		{
			50890,
			19916,
			24149,
			23777,
			11324,
			41057,
			14921,
			56274,
			30917,
			19462,
			54968,
			47943,
			38791,
			3872
		};

		// Token: 0x04000871 RID: 2161
		private static int[] expdigs2 = new int[]
		{
			24101,
			29690,
			40218,
			29073,
			29604,
			22037,
			27674,
			9082,
			56670,
			55244,
			20865,
			54874,
			47573,
			38
		};

		// Token: 0x04000872 RID: 2162
		private static int[] expdigs3 = new int[]
		{
			22191,
			40873,
			1607,
			45622,
			23883,
			24544,
			32988,
			43530,
			61694,
			55616,
			43150,
			32976,
			27418,
			25379
		};

		// Token: 0x04000873 RID: 2163
		private static int[] expdigs4 = new int[]
		{
			55927,
			44317,
			6569,
			54851,
			238,
			63160,
			51447,
			12231,
			55667,
			25459,
			5674,
			40962,
			52047,
			253
		};

		// Token: 0x04000874 RID: 2164
		private static int[] expdigs5 = new int[]
		{
			56264,
			8962,
			51839,
			64773,
			39323,
			49783,
			15587,
			30924,
			36601,
			56615,
			27581,
			36454,
			35254,
			2
		};

		// Token: 0x04000875 RID: 2165
		private static int[] expdigs6 = new int[]
		{
			21545,
			25466,
			59727,
			37873,
			13099,
			7602,
			15571,
			49963,
			37664,
			46896,
			14328,
			59258,
			17403,
			1663
		};

		// Token: 0x04000876 RID: 2166
		private static int[] expdigs7 = new int[]
		{
			12011,
			4842,
			3874,
			57395,
			38141,
			46606,
			49307,
			60792,
			31833,
			21440,
			9318,
			47123,
			41461,
			16
		};

		// Token: 0x04000877 RID: 2167
		private static int[] expdigs8 = new int[]
		{
			52383,
			25023,
			56409,
			43947,
			51036,
			17420,
			62725,
			5735,
			53692,
			44882,
			64439,
			36137,
			24719,
			10900
		};

		// Token: 0x04000878 RID: 2168
		private static int[] expdigs9 = new int[]
		{
			65404,
			27119,
			57580,
			26653,
			42453,
			19179,
			26186,
			42000,
			1847,
			62708,
			14406,
			12813,
			247,
			109
		};

		// Token: 0x04000879 RID: 2169
		private static int[] expdigs10 = new int[]
		{
			36698,
			50078,
			40552,
			35000,
			49576,
			56552,
			261,
			49572,
			31475,
			59609,
			45363,
			46658,
			5900,
			1
		};

		// Token: 0x0400087A RID: 2170
		private static int[] expdigs11 = new int[]
		{
			33321,
			54106,
			42443,
			60698,
			47535,
			24088,
			45785,
			18352,
			47026,
			40291,
			5183,
			35843,
			24059,
			714
		};

		// Token: 0x0400087B RID: 2171
		private static int[] expdigs12 = new int[]
		{
			12129,
			44450,
			22706,
			34030,
			37175,
			8760,
			31915,
			56544,
			23407,
			52176,
			7260,
			41646,
			9415,
			7
		};

		// Token: 0x0400087C RID: 2172
		private static int[] expdigs13 = new int[]
		{
			43054,
			17160,
			43698,
			6780,
			36385,
			52800,
			62346,
			52747,
			33988,
			2855,
			31979,
			38083,
			44325,
			4681
		};

		// Token: 0x0400087D RID: 2173
		private static int[] expdigs14 = new int[]
		{
			60723,
			40803,
			16165,
			19073,
			2985,
			9703,
			41911,
			37227,
			41627,
			1994,
			38986,
			27250,
			53527,
			46
		};

		// Token: 0x0400087E RID: 2174
		private static int[] expdigs15 = new int[]
		{
			36481,
			57623,
			45627,
			58488,
			53274,
			7238,
			2063,
			31221,
			62631,
			25319,
			35409,
			25293,
			54667,
			30681
		};

		// Token: 0x0400087F RID: 2175
		private static int[] expdigs16 = new int[]
		{
			52138,
			47106,
			3077,
			4517,
			41165,
			38738,
			39997,
			10142,
			13078,
			16637,
			53438,
			54647,
			53630,
			306
		};

		// Token: 0x04000880 RID: 2176
		private static int[] expdigs17 = new int[]
		{
			25425,
			24719,
			55736,
			8564,
			12208,
			3664,
			51518,
			17140,
			61079,
			30312,
			2500,
			30693,
			4468,
			3
		};

		// Token: 0x04000881 RID: 2177
		private static int[] expdigs18 = new int[]
		{
			58368,
			65134,
			52675,
			3178,
			26300,
			7986,
			11833,
			515,
			23109,
			63525,
			29138,
			19030,
			50114,
			2010
		};

		// Token: 0x04000882 RID: 2178
		private static int[] expdigs19 = new int[]
		{
			41216,
			15724,
			12323,
			26246,
			59245,
			58406,
			46648,
			13767,
			11372,
			15053,
			61895,
			48686,
			7054,
			20
		};

		// Token: 0x04000883 RID: 2179
		private static int[] expdigs20 = new int[]
		{
			0,
			29248,
			62416,
			1433,
			14025,
			43846,
			39905,
			44375,
			137,
			47955,
			62409,
			33386,
			48983,
			13177
		};

		// Token: 0x04000884 RID: 2180
		private static int[] expdigs21 = new int[]
		{
			0,
			21264,
			53708,
			60962,
			25043,
			64008,
			31200,
			50906,
			9831,
			56185,
			43877,
			36378,
			50952,
			131
		};

		// Token: 0x04000885 RID: 2181
		private static int[] expdigs22 = new int[]
		{
			0,
			50020,
			25440,
			60247,
			44814,
			39961,
			6865,
			26068,
			34832,
			9081,
			17478,
			44928,
			20825,
			1
		};

		// Token: 0x04000886 RID: 2182
		private static int[] expdigs23 = new int[]
		{
			0,
			0,
			52929,
			10084,
			25506,
			6346,
			61348,
			31525,
			52689,
			61296,
			27615,
			15903,
			40426,
			863
		};

		// Token: 0x04000887 RID: 2183
		private static int[] expdigs24 = new int[]
		{
			0,
			16384,
			24122,
			53840,
			43508,
			13170,
			51076,
			37670,
			58198,
			31414,
			57292,
			61762,
			41691,
			8
		};

		// Token: 0x04000888 RID: 2184
		private static int[] expdigs25 = new int[]
		{
			0,
			0,
			4096,
			29077,
			42481,
			30581,
			10617,
			59493,
			46251,
			1892,
			5557,
			4505,
			52391,
			5659
		};

		// Token: 0x04000889 RID: 2185
		private static int[] expdigs26 = new int[]
		{
			0,
			0,
			58368,
			11431,
			1080,
			29797,
			47947,
			36639,
			42405,
			50481,
			29546,
			9875,
			39190,
			56
		};

		// Token: 0x0400088A RID: 2186
		private static int[] expdigs27 = new int[]
		{
			0,
			0,
			0,
			57600,
			63028,
			53094,
			12749,
			18174,
			21993,
			48265,
			14922,
			59933,
			4030,
			37092
		};

		// Token: 0x0400088B RID: 2187
		private static int[] expdigs28 = new int[]
		{
			0,
			0,
			0,
			576,
			1941,
			35265,
			9302,
			42780,
			50682,
			28007,
			29640,
			28124,
			60333,
			370
		};

		// Token: 0x0400088C RID: 2188
		private static int[] expdigs29 = new int[]
		{
			0,
			0,
			0,
			5904,
			8539,
			12149,
			36793,
			43681,
			12958,
			60573,
			21267,
			35015,
			46478,
			3
		};

		// Token: 0x0400088D RID: 2189
		private static int[] expdigs30 = new int[]
		{
			0,
			0,
			0,
			0,
			7268,
			50548,
			47962,
			3644,
			22719,
			26999,
			41893,
			7421,
			56711,
			2430
		};

		// Token: 0x0400088E RID: 2190
		private static int[] expdigs31 = new int[]
		{
			0,
			0,
			0,
			0,
			7937,
			49002,
			60772,
			28216,
			38893,
			55975,
			63988,
			59711,
			20227,
			24
		};

		// Token: 0x0400088F RID: 2191
		private static int[] expdigs32 = new int[]
		{
			0,
			0,
			0,
			16384,
			38090,
			63404,
			55657,
			8801,
			62648,
			13666,
			57656,
			60234,
			15930
		};

		// Token: 0x04000890 RID: 2192
		private static int[] expdigs33 = new int[]
		{
			0,
			0,
			0,
			4096,
			37081,
			37989,
			16940,
			55138,
			17665,
			39458,
			9751,
			20263,
			159
		};

		// Token: 0x04000891 RID: 2193
		private static int[] expdigs34 = new int[]
		{
			0,
			0,
			0,
			58368,
			35104,
			16108,
			61773,
			14313,
			30323,
			54789,
			57113,
			38868,
			1
		};

		// Token: 0x04000892 RID: 2194
		private static int[] expdigs35 = new int[]
		{
			0,
			0,
			0,
			8448,
			18701,
			29652,
			51080,
			65023,
			27172,
			37903,
			3192,
			1044
		};

		// Token: 0x04000893 RID: 2195
		private static int[] expdigs36 = new int[]
		{
			0,
			0,
			0,
			37440,
			63101,
			2917,
			39177,
			50457,
			25830,
			50186,
			28867,
			10
		};

		// Token: 0x04000894 RID: 2196
		private static int[] expdigs37 = new int[]
		{
			0,
			0,
			0,
			56080,
			45850,
			37384,
			3668,
			12301,
			38269,
			18196,
			6842
		};

		// Token: 0x04000895 RID: 2197
		private static int[] expdigs38 = new int[]
		{
			0,
			0,
			0,
			46436,
			13565,
			50181,
			34770,
			37478,
			5625,
			27707,
			68
		};

		// Token: 0x04000896 RID: 2198
		private static int[] expdigs39 = new int[]
		{
			0,
			0,
			0,
			32577,
			45355,
			38512,
			38358,
			3651,
			36101,
			44841
		};

		// Token: 0x04000897 RID: 2199
		private static int[] expdigs40 = new int[]
		{
			0,
			0,
			16384,
			28506,
			5696,
			56746,
			15456,
			50499,
			27230,
			448
		};

		// Token: 0x04000898 RID: 2200
		private static int[] expdigs41 = new int[]
		{
			0,
			0,
			4096,
			285,
			9232,
			58239,
			57170,
			38515,
			31729,
			4
		};

		// Token: 0x04000899 RID: 2201
		private static int[] expdigs42 = new int[]
		{
			0,
			0,
			58368,
			41945,
			57108,
			12378,
			28752,
			48226,
			2938
		};

		// Token: 0x0400089A RID: 2202
		private static int[] expdigs43 = new int[]
		{
			0,
			0,
			24832,
			47605,
			49067,
			23716,
			61891,
			25385,
			29
		};

		// Token: 0x0400089B RID: 2203
		private static int[] expdigs44 = new int[]
		{
			0,
			0,
			8768,
			2442,
			50298,
			23174,
			19624,
			19259
		};

		// Token: 0x0400089C RID: 2204
		private static int[] expdigs45 = new int[]
		{
			0,
			0,
			40720,
			45899,
			1813,
			31689,
			38862,
			192
		};

		// Token: 0x0400089D RID: 2205
		private static int[] expdigs46 = new int[]
		{
			0,
			0,
			36452,
			14221,
			34752,
			48813,
			60681,
			1
		};

		// Token: 0x0400089E RID: 2206
		private static int[] expdigs47 = new int[]
		{
			0,
			0,
			61313,
			34220,
			16731,
			11629,
			1262
		};

		// Token: 0x0400089F RID: 2207
		private static int[] expdigs48 = new int[]
		{
			0,
			16384,
			60906,
			18036,
			40144,
			40748,
			12
		};

		// Token: 0x040008A0 RID: 2208
		private static int[] expdigs49 = new int[]
		{
			0,
			4096,
			609,
			15909,
			52830,
			8271
		};

		// Token: 0x040008A1 RID: 2209
		private static int[] expdigs50 = new int[]
		{
			0,
			58368,
			3282,
			56520,
			47058,
			82
		};

		// Token: 0x040008A2 RID: 2210
		private static int[] expdigs51 = new int[]
		{
			0,
			41216,
			52461,
			7118,
			54210
		};

		// Token: 0x040008A3 RID: 2211
		private static int[] expdigs52 = new int[]
		{
			0,
			45632,
			51642,
			6624,
			542
		};

		// Token: 0x040008A4 RID: 2212
		private static int[] expdigs53 = new int[]
		{
			0,
			25360,
			24109,
			27591,
			5
		};

		// Token: 0x040008A5 RID: 2213
		private static int[] expdigs54 = new int[]
		{
			0,
			42852,
			46771,
			3552
		};

		// Token: 0x040008A6 RID: 2214
		private static int[] expdigs55 = new int[]
		{
			0,
			28609,
			34546,
			35
		};

		// Token: 0x040008A7 RID: 2215
		private static int[] expdigs56 = new int[]
		{
			16384,
			4218,
			23283
		};

		// Token: 0x040008A8 RID: 2216
		private static int[] expdigs57 = new int[]
		{
			4096,
			54437,
			232
		};

		// Token: 0x040008A9 RID: 2217
		private static int[] expdigs58 = new int[]
		{
			58368,
			21515,
			2
		};

		// Token: 0x040008AA RID: 2218
		private static int[] expdigs59 = new int[]
		{
			57600,
			1525
		};

		// Token: 0x040008AB RID: 2219
		private static int[] expdigs60 = new int[]
		{
			16960,
			15
		};

		// Token: 0x040008AC RID: 2220
		private static int[] expdigs61 = new int[]
		{
			10000
		};

		// Token: 0x040008AD RID: 2221
		private static int[] expdigs62 = new int[]
		{
			100
		};

		// Token: 0x040008AE RID: 2222
		private static int[] expdigs63 = new int[]
		{
			1
		};

		// Token: 0x040008AF RID: 2223
		private static int[] expdigs64 = new int[]
		{
			36700,
			62914,
			23592,
			49807,
			10485,
			36700,
			62914,
			23592,
			49807,
			10485,
			36700,
			62914,
			23592,
			655
		};

		// Token: 0x040008B0 RID: 2224
		private static int[] expdigs65 = new int[]
		{
			14784,
			18979,
			33659,
			19503,
			2726,
			9542,
			629,
			2202,
			40475,
			10590,
			4299,
			47815,
			36280,
			6
		};

		// Token: 0x040008B1 RID: 2225
		private static int[] expdigs66 = new int[]
		{
			16332,
			9978,
			33613,
			31138,
			35584,
			64252,
			13857,
			14424,
			62281,
			46279,
			36150,
			46573,
			63392,
			4294
		};

		// Token: 0x040008B2 RID: 2226
		private static int[] expdigs67 = new int[]
		{
			6716,
			24348,
			22618,
			23904,
			21327,
			3919,
			44703,
			19149,
			28803,
			48959,
			6259,
			50273,
			62237,
			42
		};

		// Token: 0x040008B3 RID: 2227
		private static int[] expdigs68 = new int[]
		{
			8471,
			23660,
			38254,
			26440,
			33662,
			38879,
			9869,
			11588,
			41479,
			23225,
			60127,
			24310,
			32615,
			28147
		};

		// Token: 0x040008B4 RID: 2228
		private static int[] expdigs69 = new int[]
		{
			13191,
			6790,
			63297,
			30410,
			12788,
			42987,
			23691,
			28296,
			32527,
			38898,
			41233,
			4830,
			31128,
			281
		};

		// Token: 0x040008B5 RID: 2229
		private static int[] expdigs70 = new int[]
		{
			4064,
			53152,
			62236,
			29139,
			46658,
			12881,
			31694,
			4870,
			19986,
			24637,
			9587,
			28884,
			53395,
			2
		};

		// Token: 0x040008B6 RID: 2230
		private static int[] expdigs71 = new int[]
		{
			26266,
			10526,
			16260,
			55017,
			35680,
			40443,
			19789,
			17356,
			30195,
			55905,
			28426,
			63010,
			44197,
			1844
		};

		// Token: 0x040008B7 RID: 2231
		private static int[] expdigs72 = new int[]
		{
			38273,
			7969,
			37518,
			26764,
			23294,
			63974,
			18547,
			17868,
			24550,
			41191,
			17323,
			53714,
			29277,
			18
		};

		// Token: 0x040008B8 RID: 2232
		private static int[] expdigs73 = new int[]
		{
			16739,
			37738,
			38090,
			26589,
			43521,
			1543,
			15713,
			10671,
			11975,
			41533,
			18106,
			9348,
			16921,
			12089
		};

		// Token: 0x040008B9 RID: 2233
		private static int[] expdigs74 = new int[]
		{
			14585,
			61981,
			58707,
			16649,
			25994,
			39992,
			28337,
			17801,
			37475,
			22697,
			31638,
			16477,
			58496,
			120
		};

		// Token: 0x040008BA RID: 2234
		private static int[] expdigs75 = new int[]
		{
			58472,
			2585,
			40564,
			27691,
			44824,
			27269,
			58610,
			54572,
			35108,
			30373,
			35050,
			10650,
			13692,
			1
		};

		// Token: 0x040008BB RID: 2235
		private static int[] expdigs76 = new int[]
		{
			50392,
			58911,
			41968,
			49557,
			29112,
			29939,
			43526,
			63500,
			55595,
			27220,
			25207,
			38361,
			18456,
			792
		};

		// Token: 0x040008BC RID: 2236
		private static int[] expdigs77 = new int[]
		{
			26062,
			32046,
			3696,
			45060,
			46821,
			40931,
			50242,
			60272,
			24148,
			20588,
			6150,
			44948,
			60477,
			7
		};

		// Token: 0x040008BD RID: 2237
		private static int[] expdigs78 = new int[]
		{
			12430,
			30407,
			320,
			41980,
			58777,
			41755,
			41041,
			13609,
			45167,
			13348,
			40838,
			60354,
			19454,
			5192
		};

		// Token: 0x040008BE RID: 2238
		private static int[] expdigs79 = new int[]
		{
			30926,
			26518,
			13110,
			43018,
			54982,
			48258,
			24658,
			15209,
			63366,
			11929,
			20069,
			43857,
			60487,
			51
		};

		// Token: 0x040008BF RID: 2239
		private static int[] expdigs80 = new int[]
		{
			51263,
			54048,
			48761,
			48627,
			30576,
			49046,
			4414,
			61195,
			61755,
			48474,
			19124,
			55906,
			15511,
			34028
		};

		// Token: 0x040008C0 RID: 2240
		private static int[] expdigs81 = new int[]
		{
			39834,
			11681,
			47018,
			3107,
			64531,
			54229,
			41331,
			41899,
			51735,
			42427,
			59173,
			13010,
			18505,
			340
		};

		// Token: 0x040008C1 RID: 2241
		private static int[] expdigs82 = new int[]
		{
			27268,
			6670,
			31272,
			9861,
			45865,
			10372,
			12865,
			62678,
			23454,
			35158,
			20252,
			29621,
			26399,
			3
		};

		// Token: 0x040008C2 RID: 2242
		private static int[] expdigs83 = new int[]
		{
			57738,
			46147,
			66,
			48154,
			11239,
			21430,
			55809,
			46003,
			15044,
			25138,
			52780,
			48043,
			4883,
			2230
		};

		// Token: 0x040008C3 RID: 2243
		private static int[] expdigs84 = new int[]
		{
			20893,
			62065,
			64225,
			52254,
			59094,
			55919,
			60195,
			5702,
			48647,
			50058,
			7736,
			41768,
			19709,
			22
		};

		// Token: 0x040008C4 RID: 2244
		private static int[] expdigs85 = new int[]
		{
			37714,
			32321,
			45840,
			36031,
			33290,
			47121,
			5146,
			28127,
			9887,
			25390,
			52929,
			2698,
			1073,
			14615
		};

		// Token: 0x040008C5 RID: 2245
		private static int[] expdigs86 = new int[]
		{
			35111,
			8187,
			18153,
			56721,
			40309,
			59453,
			51824,
			4868,
			45974,
			3530,
			43783,
			8546,
			9841,
			146
		};

		// Token: 0x040008C6 RID: 2246
		private static int[] expdigs87 = new int[]
		{
			23288,
			61030,
			42779,
			19572,
			29894,
			47780,
			45082,
			32816,
			43713,
			33458,
			25341,
			63655,
			30244,
			1
		};

		// Token: 0x040008C7 RID: 2247
		private static int[] expdigs88 = new int[]
		{
			58138,
			33000,
			62869,
			37127,
			61799,
			298,
			46353,
			5693,
			63898,
			62040,
			989,
			23191,
			53065,
			957
		};

		// Token: 0x040008C8 RID: 2248
		private static int[] expdigs89 = new int[]
		{
			42524,
			32442,
			36673,
			15444,
			22900,
			658,
			61412,
			32824,
			21610,
			64190,
			1975,
			11373,
			37886,
			9
		};

		// Token: 0x040008C9 RID: 2249
		private static int[] expdigs90 = new int[]
		{
			26492,
			4357,
			32437,
			10852,
			34233,
			53968,
			55056,
			34692,
			64553,
			38226,
			41929,
			21646,
			6667,
			6277
		};

		// Token: 0x040008CA RID: 2250
		private static int[] expdigs91 = new int[]
		{
			61213,
			698,
			16053,
			50571,
			2963,
			50347,
			13657,
			48188,
			46520,
			19387,
			33187,
			25775,
			50529,
			62
		};

		// Token: 0x040008CB RID: 2251
		private static int[] expdigs92 = new int[]
		{
			42864,
			54351,
			45226,
			20476,
			23443,
			17724,
			3780,
			44701,
			52910,
			23402,
			28374,
			46862,
			40234,
			41137
		};

		// Token: 0x040008CC RID: 2252
		private static int[] expdigs93 = new int[]
		{
			23366,
			62147,
			58123,
			44113,
			55284,
			39498,
			3314,
			9622,
			9704,
			27759,
			25187,
			43722,
			24650,
			411
		};

		// Token: 0x040008CD RID: 2253
		private static int[] expdigs94 = new int[]
		{
			38899,
			44530,
			19586,
			37141,
			1863,
			9570,
			32801,
			31553,
			51870,
			62536,
			51369,
			30583,
			7455,
			4
		};

		// Token: 0x040008CE RID: 2254
		private static int[] expdigs95 = new int[]
		{
			10421,
			4321,
			43699,
			3472,
			65252,
			17057,
			13858,
			29819,
			14733,
			21490,
			40602,
			31315,
			65186,
			2695
		};

		// Token: 0x040008CF RID: 2255
		private static int[] expdigs96 = new int[]
		{
			6002,
			54438,
			29272,
			34113,
			17036,
			25074,
			36183,
			953,
			25051,
			12011,
			20722,
			4245,
			62911,
			26
		};

		// Token: 0x040008D0 RID: 2256
		private static int[] expdigs97 = new int[]
		{
			14718,
			45935,
			8408,
			42891,
			21312,
			56531,
			44159,
			45581,
			20325,
			36295,
			35509,
			24455,
			30844,
			17668
		};

		// Token: 0x040008D1 RID: 2257
		private static int[] expdigs98 = new int[]
		{
			54542,
			45023,
			23021,
			3050,
			31015,
			20881,
			50904,
			40432,
			33626,
			14125,
			44264,
			60537,
			44872,
			176
		};

		// Token: 0x040008D2 RID: 2258
		private static int[] expdigs99 = new int[]
		{
			60183,
			8969,
			14648,
			17725,
			11451,
			50016,
			34587,
			46279,
			19341,
			42084,
			16826,
			5848,
			50256,
			1
		};

		// Token: 0x040008D3 RID: 2259
		private static int[] expdigs100 = new int[]
		{
			64999,
			53685,
			60382,
			19151,
			25736,
			5357,
			31302,
			23283,
			14225,
			52622,
			56781,
			39489,
			60351,
			1157
		};

		// Token: 0x040008D4 RID: 2260
		private static int[] expdigs101 = new int[]
		{
			1305,
			4469,
			39270,
			18541,
			63827,
			59035,
			54707,
			16616,
			32910,
			48367,
			64137,
			2360,
			37959,
			11
		};

		// Token: 0x040008D5 RID: 2261
		private static int[] expdigs102 = new int[]
		{
			45449,
			32125,
			19705,
			56098,
			51958,
			5225,
			18285,
			13654,
			9341,
			25888,
			50946,
			26855,
			36068,
			7588
		};

		// Token: 0x040008D6 RID: 2262
		private static int[] expdigs103 = new int[]
		{
			27324,
			53405,
			43450,
			25464,
			3796,
			3329,
			46058,
			53220,
			26307,
			53998,
			33932,
			23861,
			58032,
			75
		};

		// Token: 0x040008D7 RID: 2263
		private static int[] expdigs104 = new int[]
		{
			63080,
			50735,
			1844,
			21406,
			57926,
			63607,
			24936,
			52889,
			23469,
			64488,
			539,
			8859,
			21210,
			49732
		};

		// Token: 0x040008D8 RID: 2264
		private static int[] expdigs105 = new int[]
		{
			62890,
			39828,
			3950,
			32982,
			39245,
			21607,
			40226,
			50991,
			18584,
			10475,
			59643,
			40720,
			21183,
			497
		};

		// Token: 0x040008D9 RID: 2265
		private static int[] expdigs106 = new int[]
		{
			37329,
			64623,
			11835,
			985,
			46923,
			48712,
			28582,
			21481,
			28366,
			41392,
			13703,
			49559,
			63781,
			4
		};

		// Token: 0x040008DA RID: 2266
		private static int[] expdigs107 = new int[]
		{
			3316,
			60011,
			41933,
			47959,
			54404,
			39790,
			12283,
			941,
			46090,
			42226,
			18108,
			38803,
			16879,
			3259
		};

		// Token: 0x040008DB RID: 2267
		private static int[] expdigs108 = new int[]
		{
			46563,
			56305,
			5006,
			45044,
			49040,
			12849,
			778,
			6563,
			46336,
			3043,
			7390,
			2354,
			38835,
			32
		};

		// Token: 0x040008DC RID: 2268
		private static int[] expdigs109 = new int[]
		{
			28653,
			3742,
			33331,
			2671,
			39772,
			29981,
			56489,
			1973,
			26280,
			26022,
			56391,
			56434,
			57039,
			21359
		};

		// Token: 0x040008DD RID: 2269
		private static int[] expdigs110 = new int[]
		{
			9461,
			17732,
			7542,
			26241,
			8917,
			24548,
			61513,
			13126,
			59245,
			41547,
			1874,
			41852,
			39236,
			213
		};

		// Token: 0x040008DE RID: 2270
		private static int[] expdigs111 = new int[]
		{
			36794,
			22459,
			63645,
			14024,
			42032,
			53329,
			25518,
			11272,
			18287,
			20076,
			62933,
			3039,
			8912,
			2
		};

		// Token: 0x040008DF RID: 2271
		private static int[] expdigs112 = new int[]
		{
			14926,
			15441,
			32337,
			42579,
			26354,
			35154,
			22815,
			36955,
			12564,
			8047,
			856,
			41917,
			55080,
			1399
		};

		// Token: 0x040008E0 RID: 2272
		private static int[] expdigs113 = new int[]
		{
			8668,
			50617,
			10153,
			17465,
			1574,
			28532,
			15301,
			58041,
			38791,
			60373,
			663,
			29255,
			65431,
			13
		};

		// Token: 0x040008E1 RID: 2273
		private static int[] expdigs114 = new int[]
		{
			21589,
			32199,
			24754,
			45321,
			9349,
			26230,
			35019,
			37508,
			20896,
			42986,
			31405,
			12458,
			65173,
			9173
		};

		// Token: 0x040008E2 RID: 2274
		private static int[] expdigs115 = new int[]
		{
			46746,
			1632,
			61196,
			50915,
			64318,
			41549,
			2971,
			23968,
			59191,
			58756,
			61917,
			779,
			48493,
			91
		};

		// Token: 0x040008E3 RID: 2275
		private static int[] expdigs116 = new int[]
		{
			1609,
			63382,
			15744,
			15685,
			51627,
			56348,
			33838,
			52458,
			44148,
			11077,
			56293,
			41906,
			45227,
			60122
		};

		// Token: 0x040008E4 RID: 2276
		private static int[] expdigs117 = new int[]
		{
			19676,
			45198,
			6055,
			38823,
			8380,
			49060,
			17377,
			58196,
			43039,
			21737,
			59545,
			12870,
			14870,
			601
		};

		// Token: 0x040008E5 RID: 2277
		private static int[] expdigs118 = new int[]
		{
			4128,
			2418,
			28241,
			13495,
			26298,
			3767,
			31631,
			5169,
			8950,
			27087,
			56956,
			4060,
			804,
			6
		};

		// Token: 0x040008E6 RID: 2278
		private static int[] expdigs119 = new int[]
		{
			39930,
			40673,
			19029,
			54677,
			38145,
			23200,
			41325,
			24564,
			24955,
			54484,
			23863,
			52998,
			13147,
			3940
		};

		// Token: 0x040008E7 RID: 2279
		private static int[] expdigs120 = new int[]
		{
			3676,
			24655,
			34924,
			27416,
			23974,
			887,
			10899,
			4833,
			21221,
			28725,
			19899,
			57546,
			26345,
			39
		};

		// Token: 0x040008E8 RID: 2280
		private static int[] expdigs121 = new int[]
		{
			28904,
			41324,
			18596,
			42292,
			12070,
			52013,
			30810,
			61057,
			55753,
			32324,
			38953,
			6752,
			32688,
			25822
		};

		// Token: 0x040008E9 RID: 2281
		private static int[] expdigs122 = new int[]
		{
			42232,
			26627,
			2807,
			27948,
			50583,
			49016,
			32420,
			64180,
			3178,
			3600,
			21361,
			52496,
			14744,
			258
		};

		// Token: 0x040008EA RID: 2282
		private static int[] expdigs123 = new int[]
		{
			2388,
			59904,
			28863,
			7488,
			31963,
			8354,
			47510,
			15059,
			2653,
			58363,
			31670,
			21496,
			38158,
			2
		};

		// Token: 0x040008EB RID: 2283
		private static int[] expdigs124 = new int[]
		{
			50070,
			5266,
			26158,
			10774,
			15148,
			6873,
			30230,
			33898,
			63720,
			51799,
			4515,
			50124,
			19875,
			1692
		};

		// Token: 0x040008EC RID: 2284
		private static int[] expdigs125 = new int[]
		{
			54240,
			3984,
			12058,
			2729,
			13914,
			11865,
			38313,
			39660,
			10467,
			20834,
			36745,
			57517,
			60491,
			16
		};

		// Token: 0x040008ED RID: 2285
		private static int[] expdigs126 = new int[]
		{
			5387,
			58214,
			9214,
			13883,
			14445,
			34873,
			21745,
			13490,
			23334,
			25008,
			58535,
			19372,
			44484,
			11090
		};

		// Token: 0x040008EE RID: 2286
		private static int[] expdigs127 = new int[]
		{
			27578,
			64807,
			12543,
			794,
			13907,
			61297,
			12013,
			64360,
			15961,
			20566,
			24178,
			15922,
			59427,
			110
		};

		// Token: 0x040008EF RID: 2287
		private static int[] expdigs128 = new int[]
		{
			49427,
			41935,
			46000,
			59645,
			45358,
			51075,
			15848,
			32756,
			38170,
			14623,
			35631,
			57175,
			7147,
			1
		};

		// Token: 0x040008F0 RID: 2288
		private static int[] expdigs129 = new int[]
		{
			33941,
			39160,
			55469,
			45679,
			22878,
			60091,
			37210,
			18508,
			1638,
			57398,
			65026,
			41643,
			54966,
			726
		};

		// Token: 0x040008F1 RID: 2289
		private static int[] expdigs130 = new int[]
		{
			60632,
			24639,
			41842,
			62060,
			20544,
			59583,
			52800,
			1495,
			48513,
			43827,
			10480,
			1727,
			17589,
			7
		};

		// Token: 0x040008F2 RID: 2290
		private static int[] expdigs131 = new int[]
		{
			5590,
			60244,
			53985,
			26632,
			53049,
			33628,
			58267,
			54922,
			21641,
			62744,
			58109,
			2070,
			26887,
			4763
		};

		// Token: 0x040008F3 RID: 2291
		private static int[] expdigs132 = new int[]
		{
			62970,
			37957,
			34618,
			29757,
			24123,
			2302,
			17622,
			58876,
			44780,
			6525,
			33349,
			36065,
			41556,
			47
		};

		// Token: 0x040008F4 RID: 2292
		private static int[] expdigs133 = new int[]
		{
			1615,
			24878,
			20040,
			11487,
			23235,
			27766,
			59005,
			57847,
			60881,
			11588,
			63635,
			61281,
			31817,
			31217
		};

		// Token: 0x040008F5 RID: 2293
		private static int[] expdigs134 = new int[]
		{
			14434,
			2870,
			65081,
			44023,
			40864,
			40254,
			47120,
			6476,
			32066,
			23053,
			17020,
			19618,
			11459,
			312
		};

		// Token: 0x040008F6 RID: 2294
		private static int[] expdigs135 = new int[]
		{
			43398,
			40005,
			36695,
			8304,
			12205,
			16131,
			42414,
			38075,
			63890,
			2851,
			61774,
			59833,
			7978,
			3
		};

		// Token: 0x040008F7 RID: 2295
		private static int[] expdigs136 = new int[]
		{
			56426,
			22060,
			15473,
			31824,
			19088,
			38788,
			64386,
			12875,
			35770,
			65519,
			11824,
			19623,
			56959,
			2045
		};

		// Token: 0x040008F8 RID: 2296
		private static int[] expdigs137 = new int[]
		{
			16292,
			32333,
			10640,
			47504,
			29026,
			30534,
			23581,
			6682,
			10188,
			24248,
			44027,
			51969,
			30060,
			20
		};

		// Token: 0x040008F9 RID: 2297
		private static int[] expdigs138 = new int[]
		{
			29432,
			37518,
			55373,
			2727,
			33243,
			22572,
			16689,
			35625,
			34145,
			15830,
			59880,
			32552,
			52948,
			13407
		};

		// Token: 0x040008FA RID: 2298
		private static int[] expdigs139 = new int[]
		{
			61898,
			27244,
			41841,
			33450,
			18682,
			13988,
			24415,
			11497,
			1652,
			34237,
			34677,
			325,
			5117,
			134
		};

		// Token: 0x040008FB RID: 2299
		private static int[] expdigs140 = new int[]
		{
			16347,
			3549,
			48915,
			22616,
			21158,
			51913,
			32356,
			21086,
			3293,
			8862,
			1002,
			26873,
			22333,
			1
		};

		// Token: 0x040008FC RID: 2300
		private static int[] expdigs141 = new int[]
		{
			25966,
			63733,
			28215,
			31946,
			40858,
			58538,
			11004,
			6877,
			6109,
			3965,
			35478,
			37365,
			45488,
			878
		};

		// Token: 0x040008FD RID: 2301
		private static int[] expdigs142 = new int[]
		{
			45479,
			34060,
			17321,
			19980,
			1719,
			16314,
			29601,
			8588,
			58388,
			22321,
			14117,
			63288,
			51572,
			8
		};

		// Token: 0x040008FE RID: 2302
		private static int[] expdigs143 = new int[]
		{
			46861,
			47640,
			11481,
			23766,
			46730,
			53756,
			8682,
			60589,
			42028,
			27453,
			29714,
			31598,
			39954,
			5758
		};

		// Token: 0x040008FF RID: 2303
		private static int[] expdigs144 = new int[]
		{
			29304,
			58803,
			51232,
			27762,
			60760,
			17576,
			19092,
			26820,
			11561,
			48771,
			6850,
			27841,
			38410,
			57
		};

		// Token: 0x04000900 RID: 2304
		private static int[] expdigs145 = new int[]
		{
			2916,
			49445,
			34666,
			46387,
			18627,
			58279,
			60468,
			190,
			3545,
			51889,
			51605,
			47909,
			40910,
			37739
		};

		// Token: 0x04000901 RID: 2305
		private static int[] expdigs146 = new int[]
		{
			19034,
			62098,
			15419,
			33887,
			38852,
			53011,
			28129,
			37357,
			11176,
			48360,
			9035,
			9654,
			25968,
			377
		};

		// Token: 0x04000902 RID: 2306
		private static int[] expdigs147 = new int[]
		{
			25094,
			10451,
			7363,
			55389,
			57404,
			27399,
			11422,
			39695,
			28947,
			12935,
			61694,
			26310,
			50722,
			3
		};

		// Token: 0x04000903 RID: 2307
		private static int[][] expdigstable = new int[][]
		{
			HelperClass.expdigs0,
			HelperClass.expdigs1,
			HelperClass.expdigs2,
			HelperClass.expdigs3,
			HelperClass.expdigs4,
			HelperClass.expdigs5,
			HelperClass.expdigs6,
			HelperClass.expdigs7,
			HelperClass.expdigs8,
			HelperClass.expdigs9,
			HelperClass.expdigs10,
			HelperClass.expdigs11,
			HelperClass.expdigs12,
			HelperClass.expdigs13,
			HelperClass.expdigs14,
			HelperClass.expdigs15,
			HelperClass.expdigs16,
			HelperClass.expdigs17,
			HelperClass.expdigs18,
			HelperClass.expdigs19,
			HelperClass.expdigs20,
			HelperClass.expdigs21,
			HelperClass.expdigs22,
			HelperClass.expdigs23,
			HelperClass.expdigs24,
			HelperClass.expdigs25,
			HelperClass.expdigs26,
			HelperClass.expdigs27,
			HelperClass.expdigs28,
			HelperClass.expdigs29,
			HelperClass.expdigs30,
			HelperClass.expdigs31,
			HelperClass.expdigs32,
			HelperClass.expdigs33,
			HelperClass.expdigs34,
			HelperClass.expdigs35,
			HelperClass.expdigs36,
			HelperClass.expdigs37,
			HelperClass.expdigs38,
			HelperClass.expdigs39,
			HelperClass.expdigs40,
			HelperClass.expdigs41,
			HelperClass.expdigs42,
			HelperClass.expdigs43,
			HelperClass.expdigs44,
			HelperClass.expdigs45,
			HelperClass.expdigs46,
			HelperClass.expdigs47,
			HelperClass.expdigs48,
			HelperClass.expdigs49,
			HelperClass.expdigs50,
			HelperClass.expdigs51,
			HelperClass.expdigs52,
			HelperClass.expdigs53,
			HelperClass.expdigs54,
			HelperClass.expdigs55,
			HelperClass.expdigs56,
			HelperClass.expdigs57,
			HelperClass.expdigs58,
			HelperClass.expdigs59,
			HelperClass.expdigs60,
			HelperClass.expdigs61,
			HelperClass.expdigs62,
			HelperClass.expdigs63,
			HelperClass.expdigs64,
			HelperClass.expdigs65,
			HelperClass.expdigs66,
			HelperClass.expdigs67,
			HelperClass.expdigs68,
			HelperClass.expdigs69,
			HelperClass.expdigs70,
			HelperClass.expdigs71,
			HelperClass.expdigs72,
			HelperClass.expdigs73,
			HelperClass.expdigs74,
			HelperClass.expdigs75,
			HelperClass.expdigs76,
			HelperClass.expdigs77,
			HelperClass.expdigs78,
			HelperClass.expdigs79,
			HelperClass.expdigs80,
			HelperClass.expdigs81,
			HelperClass.expdigs82,
			HelperClass.expdigs83,
			HelperClass.expdigs84,
			HelperClass.expdigs85,
			HelperClass.expdigs86,
			HelperClass.expdigs87,
			HelperClass.expdigs88,
			HelperClass.expdigs89,
			HelperClass.expdigs90,
			HelperClass.expdigs91,
			HelperClass.expdigs92,
			HelperClass.expdigs93,
			HelperClass.expdigs94,
			HelperClass.expdigs95,
			HelperClass.expdigs96,
			HelperClass.expdigs97,
			HelperClass.expdigs98,
			HelperClass.expdigs99,
			HelperClass.expdigs100,
			HelperClass.expdigs101,
			HelperClass.expdigs102,
			HelperClass.expdigs103,
			HelperClass.expdigs104,
			HelperClass.expdigs105,
			HelperClass.expdigs106,
			HelperClass.expdigs107,
			HelperClass.expdigs108,
			HelperClass.expdigs109,
			HelperClass.expdigs110,
			HelperClass.expdigs111,
			HelperClass.expdigs112,
			HelperClass.expdigs113,
			HelperClass.expdigs114,
			HelperClass.expdigs115,
			HelperClass.expdigs116,
			HelperClass.expdigs117,
			HelperClass.expdigs118,
			HelperClass.expdigs119,
			HelperClass.expdigs120,
			HelperClass.expdigs121,
			HelperClass.expdigs122,
			HelperClass.expdigs123,
			HelperClass.expdigs124,
			HelperClass.expdigs125,
			HelperClass.expdigs126,
			HelperClass.expdigs127,
			HelperClass.expdigs128,
			HelperClass.expdigs129,
			HelperClass.expdigs130,
			HelperClass.expdigs131,
			HelperClass.expdigs132,
			HelperClass.expdigs133,
			HelperClass.expdigs134,
			HelperClass.expdigs135,
			HelperClass.expdigs136,
			HelperClass.expdigs137,
			HelperClass.expdigs138,
			HelperClass.expdigs139,
			HelperClass.expdigs140,
			HelperClass.expdigs141,
			HelperClass.expdigs142,
			HelperClass.expdigs143,
			HelperClass.expdigs144,
			HelperClass.expdigs145,
			HelperClass.expdigs146,
			HelperClass.expdigs147
		};

		// Token: 0x04000904 RID: 2308
		private static int[] nexpdigstable = new int[]
		{
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			13,
			13,
			13,
			12,
			12,
			11,
			11,
			10,
			10,
			10,
			9,
			9,
			8,
			8,
			8,
			7,
			7,
			6,
			6,
			5,
			5,
			5,
			4,
			4,
			3,
			3,
			3,
			2,
			2,
			1,
			1,
			1,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14
		};

		// Token: 0x04000905 RID: 2309
		private static int[] binexpstable = new int[]
		{
			90,
			89,
			89,
			88,
			88,
			88,
			87,
			87,
			86,
			86,
			86,
			85,
			85,
			84,
			84,
			83,
			83,
			83,
			82,
			82,
			81,
			81,
			81,
			80,
			80,
			79,
			79,
			78,
			78,
			78,
			77,
			77,
			76,
			76,
			76,
			75,
			75,
			74,
			74,
			73,
			73,
			73,
			72,
			72,
			71,
			71,
			71,
			70,
			70,
			69,
			69,
			68,
			68,
			68,
			67,
			67,
			66,
			66,
			66,
			65,
			65,
			64,
			64,
			64,
			63,
			63,
			62,
			62,
			61,
			61,
			61,
			60,
			60,
			59,
			59,
			59,
			58,
			58,
			57,
			57,
			56,
			56,
			56,
			55,
			55,
			54,
			54,
			54,
			53,
			53,
			52,
			52,
			51,
			51,
			51,
			50,
			50,
			49,
			49,
			49,
			48,
			48,
			47,
			47,
			46,
			46,
			46,
			45,
			45,
			44,
			44,
			44,
			43,
			43,
			42,
			42,
			41,
			41,
			41,
			40,
			40,
			39,
			39,
			39,
			38,
			38,
			37,
			37,
			37,
			36,
			36,
			35,
			35,
			34,
			34,
			34,
			33,
			33,
			32,
			32,
			32,
			31,
			31,
			30,
			30,
			29,
			29,
			29
		};
	}
}
