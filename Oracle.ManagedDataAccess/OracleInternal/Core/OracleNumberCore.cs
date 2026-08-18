using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.Core
{
	// Token: 0x020000E2 RID: 226
	internal class OracleNumberCore
	{
		// Token: 0x060008C4 RID: 2244 RVA: 0x0005E7A8 File Offset: 0x0005C9A8
		private OracleNumberCore()
		{
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0005E7B0 File Offset: 0x0005C9B0
		internal static bool IsPositive(byte[] byteRep)
		{
			return (byteRep[0] & 128) != 0;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0005E7C0 File Offset: 0x0005C9C0
		internal static bool IsZero(byte[] byteRep)
		{
			return byteRep[0] == 128 && byteRep.Length == 1;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0005E7D8 File Offset: 0x0005C9D8
		internal static bool IsInfinity(byte[] byteRep)
		{
			return (byteRep.Length == 2 && byteRep[0] == byte.MaxValue && byteRep[1] == (byte)(OracleNumberCore.LNXBASE + 1)) || (byteRep[0] == 0 && byteRep.Length == 1);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0005E808 File Offset: 0x0005CA08
		internal static bool IsInt(byte[] byteRep)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (OracleNumberCore.IsZero(byteRep))
				{
					result = true;
				}
				else if (OracleNumberCore.IsInfinity(byteRep))
				{
					result = false;
				}
				else
				{
					sbyte[] array = OracleNumberCore.FromLnxFmt(byteRep);
					sbyte b = array[0];
					byte b2 = (byte)(array.Length - 1);
					if (b2 > (byte)(b + 1))
					{
						result = false;
					}
					else
					{
						result = true;
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
			return result;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0005E8B0 File Offset: 0x0005CAB0
		internal static bool IsPositiveInfinity(byte[] byteRep)
		{
			return byteRep.Length == 2 && byteRep[0] == byte.MaxValue && byteRep[1] == (byte)(OracleNumberCore.LNXBASE + 1);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0005E8D4 File Offset: 0x0005CAD4
		internal static bool IsNegativeInfinity(byte[] byteRep)
		{
			return byteRep.Length == 1 && byteRep[0] == 0;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0005E8E4 File Offset: 0x0005CAE4
		internal static bool IsNaN(byte[] byteRep, int offset = 0, int len = 0)
		{
			if (len == 0)
			{
				len = byteRep.Length;
			}
			return len == 8 && byteRep[offset] == OracleNumberCore.NANREPD[0] && byteRep[1 + offset] == OracleNumberCore.NANREPD[1] && byteRep[2 + offset] == OracleNumberCore.NANREPD[2] && byteRep[3 + offset] == OracleNumberCore.NANREPD[3] && byteRep[4 + offset] == OracleNumberCore.NANREPD[4] && byteRep[5 + offset] == OracleNumberCore.NANREPD[5] && byteRep[6 + offset] == OracleNumberCore.NANREPD[6] && byteRep[7 + offset] == OracleNumberCore.NANREPD[7];
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0005E980 File Offset: 0x0005CB80
		internal static byte[] GetPositiveInfinityByteRep()
		{
			return new byte[]
			{
				byte.MaxValue,
				(byte)(OracleNumberCore.LNXBASE + 1)
			};
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0005E9A8 File Offset: 0x0005CBA8
		internal static byte[] GetNegativeInfinityByteRep()
		{
			return new byte[]
			{
				0
			};
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0005E9C4 File Offset: 0x0005CBC4
		internal static byte[] GetZeroByteRep()
		{
			return new byte[]
			{
				128
			};
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0005E9E4 File Offset: 0x0005CBE4
		internal static byte[] GetByteRep(double doubleNum)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				if (double.IsNaN(doubleNum))
				{
					result = OracleNumberCore.NANREPD;
				}
				else if (doubleNum == 0.0)
				{
					result = OracleNumberCore.GetZeroByteRep();
				}
				else if (double.IsPositiveInfinity(doubleNum))
				{
					result = OracleNumberCore.GetPositiveInfinityByteRep();
				}
				else if (double.IsNegativeInfinity(doubleNum))
				{
					result = OracleNumberCore.GetNegativeInfinityByteRep();
				}
				else
				{
					result = OracleNumberCore.lnxren(doubleNum);
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

		// Token: 0x060008D0 RID: 2256 RVA: 0x0005EA9C File Offset: 0x0005CC9C
		public static bool isValid(byte[] num)
		{
			if (OracleNumberCore.IsNaN(num, 0, 0))
			{
				return true;
			}
			byte b = (byte)num.Length;
			if (OracleNumberCore.IsPositive(num))
			{
				if (b == 1)
				{
					return OracleNumberCore.IsZero(num);
				}
				if (num[0] == 255 && num[1] == (byte)(OracleNumberCore.LNXBASE + 1))
				{
					return b == 2;
				}
				if (b > 21)
				{
					return false;
				}
				if (num[1] < 2 || num[(int)(b - 1)] < 2)
				{
					return false;
				}
				for (int i = 1; i < (int)b; i++)
				{
					byte b2 = num[i];
					if (b2 < 1 || (int)b2 > OracleNumberCore.LNXBASE)
					{
						return false;
					}
				}
				return true;
			}
			else
			{
				if (b < 3)
				{
					return OracleNumberCore.IsNegativeInfinity(num);
				}
				if (b > 21)
				{
					return false;
				}
				if ((int)num[(int)(b - 1)] != OracleNumberCore.LNXBASE + 2)
				{
					if (b <= 20)
					{
						return false;
					}
				}
				else
				{
					b -= 1;
				}
				if ((int)num[1] > OracleNumberCore.LNXBASE || (int)num[(int)(b - 1)] > OracleNumberCore.LNXBASE)
				{
					return false;
				}
				for (int j = 1; j < (int)b; j++)
				{
					byte b2 = num[j];
					if (b2 < 2 || (int)b2 > OracleNumberCore.LNXBASE + 1)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0005EB88 File Offset: 0x0005CD88
		internal static sbyte[] FromLnxFmt(byte[] num)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			sbyte[] result;
			try
			{
				int num2 = num.Length;
				sbyte[] array;
				if (OracleNumberCore.IsPositive(num))
				{
					array = new sbyte[num2];
					array[0] = (sbyte)(((int)num[0] & -129) - 65);
					for (int i = 1; i < num2; i++)
					{
						array[i] = (sbyte)(num[i] - 1);
					}
				}
				else
				{
					if (num2 - 1 == 20 && num[num2 - 1] != (byte)((sbyte)(OracleNumberCore.LNXBASE + 2)))
					{
						array = new sbyte[num2];
					}
					else
					{
						array = new sbyte[num2 - 1];
					}
					array[0] = (sbyte)(((int)(~(int)num[0]) & -129) - 65);
					for (int j = 1; j < array.Length; j++)
					{
						array[j] = (sbyte)(OracleNumberCore.LNXBASE + 1 - (int)num[j]);
					}
				}
				result = array;
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

		// Token: 0x060008D2 RID: 2258 RVA: 0x0005EC8C File Offset: 0x0005CE8C
		internal static byte[] ToLnxFmt(byte[] num, bool pos)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				int num2 = num.Length;
				byte[] array;
				if (pos)
				{
					array = new byte[num2];
					array[0] = num[0] + 128 + 64 + 1;
					for (int i = 1; i < num2; i++)
					{
						array[i] = num[i] + 1;
					}
				}
				else
				{
					if (num2 - 1 < 20)
					{
						array = new byte[num2 + 1];
					}
					else
					{
						array = new byte[num2];
					}
					array[0] = ~(num[0] + 128 + 64 + 1);
					int i;
					for (i = 1; i < num2; i++)
					{
						array[i] = (byte)(OracleNumberCore.LNXBASE + 1 - (int)num[i]);
					}
					if (i <= 20)
					{
						array[i] = (byte)(OracleNumberCore.LNXBASE + 2);
					}
				}
				result = array;
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

		// Token: 0x060008D3 RID: 2259 RVA: 0x0005ED8C File Offset: 0x0005CF8C
		private static byte[] SetLength(byte[] oranum, int size)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				bool flag = OracleNumberCore.IsPositive(oranum);
				byte[] array;
				if (flag)
				{
					array = new byte[size];
				}
				else if (size <= 20 && (int)oranum[size - 1] != OracleNumberCore.LNXBASE + 2)
				{
					array = new byte[size + 1];
					array[size] = (byte)(OracleNumberCore.LNXBASE + 2);
				}
				else
				{
					array = new byte[size];
				}
				Array.Copy(oranum, 0, array, 0, size);
				result = array;
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

		// Token: 0x060008D4 RID: 2260 RVA: 0x0005EE48 File Offset: 0x0005D048
		internal static byte[] lnxmin(long longNum)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				byte[] array = new byte[20];
				byte[] array2 = new byte[20];
				byte b = 0;
				if (longNum == 0L)
				{
					result = OracleNumberCore.GetZeroByteRep();
				}
				else
				{
					bool pos = longNum >= 0L;
					int num = 0;
					while (longNum != 0L)
					{
						array[num] = (byte)Math.Abs(longNum % (long)OracleNumberCore.LNXBASE);
						longNum /= (long)OracleNumberCore.LNXBASE;
						num++;
					}
					byte b2 = (byte)(--num);
					int num2 = (int)b2;
					while (b <= b2)
					{
						array2[(int)b] = array[num2];
						b += 1;
						num2--;
					}
					while (num > 0 && array2[num--] == 0)
					{
						b -= 1;
					}
					byte[] array3 = new byte[(int)(b + 1)];
					array3[0] = b2;
					Array.Copy(array2, 0, array3, 1, (int)b);
					result = OracleNumberCore.ToLnxFmt(array3, pos);
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

		// Token: 0x060008D5 RID: 2261 RVA: 0x0005EF74 File Offset: 0x0005D174
		internal static byte[] lnxren(double doubleNum)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				byte[] array = new byte[20];
				int num = 0;
				bool pos = doubleNum >= 0.0;
				doubleNum = Math.Abs(doubleNum);
				if (doubleNum < 1.0)
				{
					for (int i = 0; i < 8; i++)
					{
						if (OracleNumberCore.powerTable[i][2] >= doubleNum)
						{
							num -= (int)OracleNumberCore.powerTable[i][0];
							doubleNum *= OracleNumberCore.powerTable[i][1];
						}
					}
					if (doubleNum < 1.0)
					{
						num--;
						doubleNum *= 100.0;
					}
				}
				else
				{
					for (int i = 0; i < 8; i++)
					{
						if (OracleNumberCore.powerTable[i][1] <= doubleNum)
						{
							num += (int)OracleNumberCore.powerTable[i][0];
							doubleNum *= OracleNumberCore.powerTable[i][2];
						}
					}
				}
				if (num > 62)
				{
					throw new OverflowException();
				}
				if (num < -65)
				{
					throw new OverflowException();
				}
				bool flag = doubleNum < 10.0;
				byte b = 8;
				int j = 0;
				byte b2 = (byte)doubleNum;
				while (j < (int)b)
				{
					array[j] = b2;
					doubleNum = (doubleNum - (double)b2) * 100.0;
					b2 = (byte)doubleNum;
					j++;
				}
				j = 7;
				if (flag)
				{
					if ((int)b2 >= OracleNumberCore.LNXBASE / 2)
					{
						byte[] array2 = array;
						int num2 = j;
						array2[num2] += 1;
					}
				}
				else if (num == 62 && (int)((array[j] + 5) / 10 * 10) == OracleNumberCore.LNXBASE)
				{
					array[j] = (array[j] - 5) / 10 * 10;
				}
				else
				{
					array[j] = (array[j] + 5) / 10 * 10;
				}
				while ((int)array[j] == OracleNumberCore.LNXBASE)
				{
					if (j == 0)
					{
						num++;
						array[j] = 1;
						break;
					}
					array[j] = 0;
					j--;
					byte[] array3 = array;
					int num3 = j;
					array3[num3] += 1;
				}
				j = 7;
				while (j != 0 && array[j] == 0)
				{
					b -= 1;
					j--;
				}
				byte[] array4 = new byte[(int)(b + 1)];
				array4[0] = (byte)num;
				Array.Copy(array, 0, array4, 1, (int)b);
				result = OracleNumberCore.ToLnxFmt(array4, pos);
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

		// Token: 0x060008D6 RID: 2262 RVA: 0x0005F200 File Offset: 0x0005D400
		internal static long lnxsni(byte[] num)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				long num2 = 0L;
				sbyte[] array = OracleNumberCore.FromLnxFmt(num);
				sbyte b = array[0];
				byte b2 = (byte)(array.Length - 1);
				if (OracleNumberCore.IsZero(num))
				{
					result = 0L;
				}
				else
				{
					if (OracleNumberCore.IsInfinity(num) || OracleNumberCore.compareBytes(num, OracleNumberCore.MAX_LONG) > 0 || OracleNumberCore.compareBytes(num, OracleNumberCore.MIN_LONG) < 0)
					{
						throw new OverflowException();
					}
					int num3 = (int)((b2 > (byte)(b + 1)) ? (b + 1) : ((sbyte)b2));
					for (int i = 0; i < num3; i++)
					{
						num2 = num2 * (long)OracleNumberCore.LNXBASE + (long)array[i + 1];
					}
					for (int j = (int)(b - (sbyte)b2); j >= 0; j--)
					{
						num2 *= (long)OracleNumberCore.LNXBASE;
					}
					result = (OracleNumberCore.IsPositive(num) ? num2 : (-num2));
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

		// Token: 0x060008D7 RID: 2263 RVA: 0x0005F31C File Offset: 0x0005D51C
		internal static double lnxnur(byte[] num)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			double result;
			try
			{
				double num2 = 0.0;
				int num3 = 1;
				bool flag = false;
				int num4 = OracleNumberCore.factorTable.Length;
				if (OracleNumberCore.IsZero(num))
				{
					result = num2;
				}
				else
				{
					if (num[0] == 0 || num[0] == 255)
					{
						if (OracleNumberCore.IsNegativeInfinity(num))
						{
							return double.NegativeInfinity;
						}
						if (OracleNumberCore.IsPositiveInfinity(num))
						{
							return double.PositiveInfinity;
						}
						if (OracleNumberCore.IsNaN(num, 0, 0))
						{
							return double.NaN;
						}
					}
					sbyte[] array = OracleNumberCore.FromLnxFmt(num);
					bool flag2 = array[1] < 10;
					double num5 = OracleNumberCore.factorTable[0][0];
					double num6 = OracleNumberCore.factorTable[0][0] - (double)(num4 - 20);
					int num7;
					int i;
					if ((double)array[0] > num5 || (double)array[0] < num6)
					{
						if ((double)array[0] > num5)
						{
							num7 = -1;
							i = (int)((double)array[0] - num5);
						}
						else
						{
							num7 = -1 + (num4 - 20);
							i = (int)((double)array[0] - num6);
						}
					}
					else
					{
						num7 = -1 + (int)(num5 - (double)array[0]);
						i = 0;
					}
					int j = array.Length - 1;
					if (flag2 ? (j > 8) : (j >= 8))
					{
						j = 8;
						flag = true;
					}
					switch (j % 4)
					{
					case 1:
					{
						int num8 = (int)array[1];
						num7++;
						double num9 = OracleNumberCore.factorTable[num7][1];
						if (num9 < 1.0)
						{
							num2 = (double)num8 / OracleNumberCore.factorTable[num7][2];
						}
						else
						{
							num2 = (double)num8 * OracleNumberCore.factorTable[num7][1];
						}
						num3++;
						j--;
						break;
					}
					case 2:
					{
						int num8 = (int)array[1] * OracleNumberCore.LNXBASE + (int)array[2];
						num7 += 2;
						double num9 = OracleNumberCore.factorTable[num7][1];
						if (num9 < 1.0)
						{
							num2 = (double)num8 / OracleNumberCore.factorTable[num7][2];
						}
						else
						{
							num2 = (double)num8 * OracleNumberCore.factorTable[num7][1];
						}
						num3 += 2;
						j -= 2;
						break;
					}
					case 3:
					{
						int num8 = ((int)array[1] * OracleNumberCore.LNXBASE + (int)array[2]) * OracleNumberCore.LNXBASE + (int)array[3];
						num7 += 3;
						double num9 = OracleNumberCore.factorTable[num7][1];
						if (num9 < 1.0)
						{
							num2 = (double)num8 / OracleNumberCore.factorTable[num7][2];
						}
						else
						{
							num2 = (double)num8 * OracleNumberCore.factorTable[num7][1];
						}
						num3 += 3;
						j -= 3;
						break;
					}
					default:
						num2 = 0.0;
						break;
					}
					while (j > 0)
					{
						int num8 = (((int)array[num3] * OracleNumberCore.LNXBASE + (int)array[num3 + 1]) * OracleNumberCore.LNXBASE + (int)array[num3 + 2]) * OracleNumberCore.LNXBASE + (int)array[num3 + 3];
						num7 += 4;
						double num9 = OracleNumberCore.factorTable[num7][1];
						if (num9 < 1.0)
						{
							num2 += (double)num8 / OracleNumberCore.factorTable[num7][2];
						}
						else
						{
							num2 += (double)num8 * OracleNumberCore.factorTable[num7][1];
						}
						num3 += 4;
						j -= 4;
					}
					if (flag)
					{
						if (flag2)
						{
							if ((int)array[num3] > OracleNumberCore.LNXBASE / 2)
							{
								int num8 = 1;
								num2 += (double)num8 * OracleNumberCore.factorTable[num7][1];
							}
						}
						else
						{
							num3--;
							int num8;
							if (array[num3] % 10 >= 5)
							{
								num8 = (int)((array[num3] / 10 + 1) * 10);
							}
							else
							{
								num8 = (int)(array[num3] / 10 * 10);
							}
							num8 -= (int)array[num3];
							num2 += (double)num8 * OracleNumberCore.factorTable[num7][1];
						}
					}
					if (i != 0)
					{
						int num10 = 0;
						while (i > 0)
						{
							if ((int)OracleNumberCore.powerTable[num10][0] <= i)
							{
								i -= (int)OracleNumberCore.powerTable[num10][0];
								num2 *= OracleNumberCore.powerTable[num10][1];
							}
							num10++;
						}
						while (i < 0)
						{
							if ((int)OracleNumberCore.powerTable[num10][0] <= -i)
							{
								i += (int)OracleNumberCore.powerTable[num10][0];
								num2 *= OracleNumberCore.powerTable[num10][2];
							}
							num10++;
						}
					}
					result = (OracleNumberCore.IsPositive(num) ? num2 : (-num2));
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

		// Token: 0x060008D8 RID: 2264 RVA: 0x0005F76C File Offset: 0x0005D96C
		internal static void NegateNumber(byte[] oranum)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				int num = oranum.Length;
				for (int i = num - 1; i > 0; i--)
				{
					oranum[i] = OracleNumberCore.LnxqNegate[(int)oranum[i]];
				}
				oranum[0] = ~oranum[0];
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

		// Token: 0x060008D9 RID: 2265 RVA: 0x0005F800 File Offset: 0x0005DA00
		internal static byte[] lnxabs(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				byte[] array = new byte[n.Length];
				if (OracleNumberCore.IsPositive(n))
				{
					Array.Copy(n, 0, array, 0, n.Length);
					result = array;
				}
				else if (OracleNumberCore.IsNegativeInfinity(n))
				{
					result = OracleNumberCore.GetPositiveInfinityByteRep();
				}
				else
				{
					int num = n.Length;
					if ((int)n[num - 1] == OracleNumberCore.LNXBASE + 2)
					{
						num--;
					}
					Array.Copy(n, 0, array, 0, num);
					OracleNumberCore.NegateNumber(array);
					result = OracleNumberCore.SetLength(array, num);
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

		// Token: 0x060008DA RID: 2266 RVA: 0x0005F8CC File Offset: 0x0005DACC
		internal static byte[] lnxneg(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				if (OracleNumberCore.IsZero(n))
				{
					result = OracleNumberCore.GetZeroByteRep();
				}
				else if (OracleNumberCore.IsPositiveInfinity(n))
				{
					result = OracleNumberCore.GetNegativeInfinityByteRep();
				}
				else if (OracleNumberCore.IsNegativeInfinity(n))
				{
					result = OracleNumberCore.GetPositiveInfinityByteRep();
				}
				else
				{
					int num = n.Length;
					if (!OracleNumberCore.IsPositive(n) && (int)n[num - 1] == OracleNumberCore.LNXBASE + 2)
					{
						num--;
					}
					byte[] array = new byte[num];
					Array.Copy(n, 0, array, 0, num);
					OracleNumberCore.NegateNumber(array);
					result = OracleNumberCore.SetLength(array, num);
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

		// Token: 0x060008DB RID: 2267 RVA: 0x0005F9AC File Offset: 0x0005DBAC
		internal static byte[] lnxadd(byte[] n1, byte[] n2)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				int num = n1.Length;
				int num2 = 0;
				int num3 = n2.Length;
				int num4 = 0;
				byte[] array = new byte[41];
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				int num8 = num5 + 1;
				bool flag = n1[0] >> 7 != 0;
				int num9 = (int)n1[0];
				if (!flag)
				{
					num9 = (int)((byte)(~(byte)num9));
					if ((int)n1[num - 1] == OracleNumberCore.LNXBASE + 2)
					{
						num--;
					}
				}
				int num10 = num - 1;
				bool flag2 = n2[0] >> 7 != 0;
				int num11 = (int)n2[0];
				if (!flag2)
				{
					num11 = (int)((byte)(~(byte)num11));
					if ((int)n2[num3 - 1] == OracleNumberCore.LNXBASE + 2)
					{
						num3--;
					}
				}
				int num12 = num3 - 1;
				if (num9 == 255 && (num10 == 0 || (int)n1[1] == OracleNumberCore.LNXBASE + 1))
				{
					bool flag3 = flag;
					if (flag3)
					{
						result = OracleNumberCore.GetPositiveInfinityByteRep();
					}
					else
					{
						result = OracleNumberCore.GetNegativeInfinityByteRep();
					}
				}
				else if (num11 == 255 && (num12 == 0 || (int)n2[1] == OracleNumberCore.LNXBASE + 1))
				{
					bool flag3 = flag2;
					if (flag3)
					{
						result = OracleNumberCore.GetPositiveInfinityByteRep();
					}
					else
					{
						result = OracleNumberCore.GetNegativeInfinityByteRep();
					}
				}
				else if (num9 == 128 && num10 == 0)
				{
					array = new byte[num3];
					Array.Copy(n2, 0, array, 0, num3);
					int num13 = num3;
					result = OracleNumberCore.SetLength(array, num13);
				}
				else if (num11 == 128 && num12 == 0)
				{
					array = new byte[num];
					Array.Copy(n1, 0, array, 0, num);
					int num13 = num;
					result = OracleNumberCore.SetLength(array, num13);
				}
				else
				{
					int num14 = num9 - num11;
					bool flag3;
					byte[][] array2;
					int num15;
					int num16;
					if (flag == flag2)
					{
						flag3 = flag;
						if (flag3)
						{
							array2 = OracleNumberCore.LnxqAdd_PPP;
							num15 = 1;
							num16 = 1;
						}
						else
						{
							array2 = OracleNumberCore.LnxqAdd_NNN;
							num15 = OracleNumberCore.LNXBASE + 1;
							num16 = -1;
						}
					}
					else
					{
						int num17 = num14;
						if (num17 == 0)
						{
							int num18 = num2 + 1;
							int num19 = num4 + 1;
							int num20 = num2 + ((num10 < num12) ? num10 : num12);
							while (num18 <= num20 && (int)((sbyte)n1[num18] + (sbyte)n2[num19]) == OracleNumberCore.LNXBASE + 2)
							{
								num18++;
								num19++;
							}
							if (num18 <= num20)
							{
								num17 = (flag ? ((int)((sbyte)n1[num18] + (sbyte)n2[num19]) - (OracleNumberCore.LNXBASE + 2)) : (OracleNumberCore.LNXBASE + 2 - (int)((sbyte)n1[num18] + (sbyte)n2[num19])));
							}
							else
							{
								num17 = num10 - num12;
							}
						}
						if (num17 == 0)
						{
							return OracleNumberCore.GetZeroByteRep();
						}
						flag3 = ((num17 > 0) ? flag : flag2);
						if (flag3)
						{
							array2 = OracleNumberCore.LnxqAdd_PNP;
							num15 = 1;
							num16 = -1;
						}
						else
						{
							array2 = OracleNumberCore.LnxqAdd_PNN;
							num15 = OracleNumberCore.LNXBASE + 1;
							num16 = 1;
						}
					}
					int num21;
					int num22;
					int num23;
					int num24;
					int num25;
					int num26;
					int num27;
					int num28;
					int num29;
					bool flag4;
					if (num14 >= 0)
					{
						num21 = num9;
						if (num14 + num12 <= num10)
						{
							num22 = num14;
							num23 = num12;
							num24 = num10 - (num14 + num12);
							num25 = num2 + num22;
							num26 = 1;
							num6 = num25 + num23;
							num7 = num4 + num12;
							num27 = num2 + num10;
							num28 = 1;
							num29 = num10;
							flag4 = (num24 != 0 && flag != flag3);
						}
						else if (num14 < num10)
						{
							num22 = num14;
							num23 = num10 - num14;
							num24 = num12 - num23;
							num25 = num2 + num22;
							num26 = 1;
							num6 = num2 + num10;
							num7 = num4 + num23;
							num27 = num4 + num12;
							num28 = 2;
							num29 = num14 + num12;
							flag4 = (flag2 != flag3);
						}
						else
						{
							num22 = num10;
							num23 = -(num14 - num10);
							num24 = num12;
							num25 = num2 + num10;
							num26 = 1;
							num27 = num4 + num12;
							num28 = 2;
							num29 = num14 + num12;
							flag4 = (flag2 != flag3);
						}
					}
					else
					{
						num21 = num11;
						num14 = -num14;
						if (num14 + num10 <= num12)
						{
							num22 = num14;
							num23 = num10;
							num24 = num12 - (num14 + num10);
							num25 = num4 + num22;
							num26 = 2;
							num6 = num2 + num10;
							num7 = num25 + num23;
							num27 = num4 + num12;
							num28 = 2;
							num29 = num12;
							flag4 = (num24 != 0 && flag2 != flag3);
						}
						else if (num14 < num12)
						{
							num22 = num14;
							num23 = num12 - num14;
							num24 = num10 - num23;
							num25 = num4 + num22;
							num26 = 2;
							num6 = num2 + num23;
							num7 = num4 + num12;
							num27 = num2 + num10;
							num28 = 1;
							num29 = num14 + num10;
							flag4 = (flag != flag3);
						}
						else
						{
							num22 = num12;
							num23 = -(num14 - num12);
							num24 = num10;
							num25 = num4 + num12;
							num26 = 2;
							num27 = num2 + num10;
							num28 = 1;
							num29 = num14 + num10;
							flag4 = (flag != flag3);
						}
					}
					if (num29 > 20)
					{
						if (num14 > 20)
						{
							num23 = 0;
							num24 = 0;
							num29 = num22;
							flag4 = false;
						}
						else
						{
							num8 = 1;
						}
					}
					int num30 = num8 + (num29 - 1);
					int i = num30;
					if (num24 != 0)
					{
						int num31 = i - num24;
						if (num28 == 1)
						{
							array[i] = n1[num27];
						}
						else
						{
							array[i] = n2[num27];
						}
						num27--;
						i--;
						if (flag4)
						{
							while (i > num31)
							{
								if (num28 == 1)
								{
									array[i] = (byte)((int)n1[num27] + num16);
								}
								else
								{
									array[i] = (byte)((int)n2[num27] + num16);
								}
								num27--;
								i--;
							}
						}
						else
						{
							while (i > num31)
							{
								if (num28 == 1)
								{
									array[i] = n1[num27];
								}
								else
								{
									array[i] = n2[num27];
								}
								num27--;
								i--;
							}
						}
					}
					if (num23 > 0)
					{
						int num31 = i - num23;
						int num32 = 0;
						int num33 = flag4 ? (num32 + 1) : num32;
						do
						{
							num33 = num32 + (int)((sbyte)n1[num6]) + (int)((sbyte)n2[num7]) + (int)array2[num33][1];
							array[i] = array2[num33][0];
							num6--;
							num7--;
							i--;
						}
						while (i > num31);
						flag4 = ((array2[num33][1] & 1) != 0);
					}
					else
					{
						int num34 = flag4 ? ((num16 == 1) ? 2 : OracleNumberCore.LNXBASE) : num15;
						int num31 = i + num23;
						while (i > num31)
						{
							array[i] = (byte)num34;
							i--;
						}
					}
					if (num22 != 0)
					{
						int num31 = i - num22;
						if (flag4)
						{
							int num35 = ((num16 == 1) ? OracleNumberCore.LNXBASE : 1) + (flag3 ? 0 : 1);
							int num36 = ((num16 == 1) ? 1 : OracleNumberCore.LNXBASE) + (flag3 ? 0 : 1);
							do
							{
								if (num26 == 1)
								{
									flag4 = ((int)n1[num25] == num35);
									array[i] = (byte)(flag4 ? num36 : ((int)n1[num25] + num16));
								}
								else
								{
									flag4 = ((int)n2[num25] == num35);
									array[i] = (byte)(flag4 ? num36 : ((int)n2[num25] + num16));
								}
								num25--;
								i--;
								if (!flag4)
								{
									break;
								}
							}
							while (i > num31);
						}
						while (i > num31)
						{
							if (num26 == 1)
							{
								array[i] = n1[num25];
							}
							else
							{
								array[i] = n2[num25];
							}
							num25--;
							i--;
						}
					}
					if (flag4)
					{
						if (num21 == 255)
						{
							if (flag3)
							{
								return OracleNumberCore.GetPositiveInfinityByteRep();
							}
							return OracleNumberCore.GetNegativeInfinityByteRep();
						}
						else
						{
							num8--;
							array[num8] = (byte)(flag3 ? 2 : OracleNumberCore.LNXBASE);
							num21++;
							num29++;
						}
					}
					if ((int)array[num8] == num15)
					{
						do
						{
							num8++;
							num21--;
							num29--;
						}
						while ((int)array[num8] == num15);
						if (num21 < 128)
						{
							return OracleNumberCore.GetZeroByteRep();
						}
					}
					if (num29 > 20)
					{
						num30 = num8 + 19;
						num29 = 20;
						if ((int)(flag3 ? array[num30 + 1] : OracleNumberCore.LnxqNegate[(int)array[num30 + 1]]) > OracleNumberCore.LNXBASE / 2)
						{
							int num37 = flag3 ? OracleNumberCore.LNXBASE : 2;
							if (!flag4)
							{
								array[num8 - 1] = (byte)num15;
							}
							while ((int)array[num30] == num37)
							{
								num30--;
								num29--;
							}
							if (num30 < num8)
							{
								if (num21 == 255)
								{
									if (flag3)
									{
										return OracleNumberCore.GetPositiveInfinityByteRep();
									}
									return OracleNumberCore.GetNegativeInfinityByteRep();
								}
								else
								{
									num8--;
									num21++;
									num29 = 1;
								}
							}
							byte[] array3 = array;
							int num38 = num30;
							array3[num38] += (byte)(flag3 ? 1 : -1);
						}
					}
					while ((int)array[num30] == num15)
					{
						num30--;
						num29--;
					}
					if (num8 != 1)
					{
						byte[] array4 = new byte[41];
						Array.Copy(array, num8, array4, 1, num29);
						Array.Copy(array4, 1, array, 1, num29);
					}
					int num13 = num29 + 1;
					if (!flag3 && num13 <= 20)
					{
						array[num13] = (byte)(OracleNumberCore.LNXBASE + 2);
						num13++;
					}
					array[num5] = (byte)(flag3 ? (num21 - 256) : (255 - num21));
					result = OracleNumberCore.SetLength(array, num13);
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

		// Token: 0x060008DC RID: 2268 RVA: 0x00060278 File Offset: 0x0005E478
		internal static byte[] lnxsub(byte[] n1, byte[] n2)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				result = OracleNumberCore.lnxadd(n1, OracleNumberCore.lnxneg(n2));
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

		// Token: 0x060008DD RID: 2269 RVA: 0x000602F4 File Offset: 0x0005E4F4
		internal static byte[] lnxmul(byte[] n1, byte[] n2)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				byte[] array = n1;
				int num = array.Length;
				byte[] array2 = n2;
				int num2 = array2.Length;
				byte[] array3 = new byte[22];
				int[] array4 = new int[10];
				int[] array5 = new int[10];
				byte[] array6 = new byte[41];
				int num3 = 0;
				bool flag = array[0] >> 7 != 0;
				byte b = array[0];
				if (!flag)
				{
					b = ~b;
					if ((int)array[num - 1] == OracleNumberCore.LNXBASE + 2)
					{
						num--;
					}
				}
				bool flag2 = array2[0] >> 7 != 0;
				byte b2 = array2[0];
				if (!flag2)
				{
					b2 = ~b2;
					if ((int)array2[num2 - 1] == OracleNumberCore.LNXBASE + 2)
					{
						num2--;
					}
				}
				if (b == 128 && num == 1)
				{
					array3 = OracleNumberCore.GetZeroByteRep();
					result = array3;
				}
				else if (b2 == 128 && num2 == 1)
				{
					array3 = OracleNumberCore.GetZeroByteRep();
					result = array3;
				}
				else if ((b & 255) == 255 && (num == 1 || (int)array[1] == OracleNumberCore.LNXBASE + 1))
				{
					if (flag == flag2)
					{
						array3 = OracleNumberCore.GetPositiveInfinityByteRep();
					}
					else
					{
						array3 = OracleNumberCore.GetNegativeInfinityByteRep();
					}
					result = array3;
				}
				else if ((b2 & 255) == 255 && (num2 == 1 || (int)array2[1] == OracleNumberCore.LNXBASE + 1))
				{
					if (flag == flag2)
					{
						array3 = OracleNumberCore.GetPositiveInfinityByteRep();
					}
					else
					{
						array3 = OracleNumberCore.GetNegativeInfinityByteRep();
					}
					result = array3;
				}
				else
				{
					if (num > num2)
					{
						byte[] array7 = array;
						array = array2;
						array2 = array7;
						int num4 = num;
						num = num2;
						num2 = num4;
						bool flag3 = flag;
						flag = flag2;
						flag2 = flag3;
					}
					int num5 = num / 2 - 1;
					int i = num5;
					int j = num - 2;
					if (flag)
					{
						if ((num & 1) == 0)
						{
							array4[i] = (int)array[j + 1] * OracleNumberCore.LNXBASE - OracleNumberCore.LNXBASE;
							j--;
							i--;
						}
						while (j > 0)
						{
							array4[i] = (int)array[j] * OracleNumberCore.LNXBASE + (int)array[j + 1] - (OracleNumberCore.LNXBASE + 1);
							j -= 2;
							i--;
						}
					}
					else
					{
						if ((num & 1) == 0)
						{
							array4[i] = (OracleNumberCore.LNXBASE + 1) * OracleNumberCore.LNXBASE - (int)array[j + 1] * OracleNumberCore.LNXBASE;
							j--;
							i--;
						}
						while (j > 0)
						{
							array4[i] = (OracleNumberCore.LNXBASE + 1) * (OracleNumberCore.LNXBASE + 1) - ((int)array[j] * OracleNumberCore.LNXBASE + (int)array[j + 1]);
							j -= 2;
							i--;
						}
					}
					int num6 = num2 / 2 - 1;
					int k = num6;
					int l = num2 - 2;
					if (flag2)
					{
						if ((num2 & 1) == 0)
						{
							array5[k] = (int)array2[l + 1] * OracleNumberCore.LNXBASE - OracleNumberCore.LNXBASE;
							l--;
							k--;
						}
						while (l > 0)
						{
							array5[k] = (int)array2[l] * OracleNumberCore.LNXBASE + (int)array2[l + 1] - (OracleNumberCore.LNXBASE + 1);
							l -= 2;
							k--;
						}
					}
					else
					{
						if ((num2 & 1) == 0)
						{
							array5[k] = (OracleNumberCore.LNXBASE + 1) * OracleNumberCore.LNXBASE - (int)array2[l + 1] * OracleNumberCore.LNXBASE;
							l--;
							k--;
						}
						while (l > 0)
						{
							array5[k] = (OracleNumberCore.LNXBASE + 1) * (OracleNumberCore.LNXBASE + 1) - ((int)array2[l] * OracleNumberCore.LNXBASE + (int)array2[l + 1]);
							l -= 2;
							k--;
						}
					}
					short num7;
					int num8;
					if (array4[0] * array5[0] < OracleNumberCore.LNXBASE * OracleNumberCore.LNXBASE * OracleNumberCore.LNXBASE)
					{
						num7 = (short)((b & byte.MaxValue) + (b2 & byte.MaxValue) - 193);
						num8 = (num & 254) + (num2 & 254);
					}
					else
					{
						num7 = (short)((b & byte.MaxValue) + (b2 & byte.MaxValue) - 192);
						num8 = (num & 254) + (num2 & 254) + 1;
					}
					int num9 = 1;
					int num10 = num8;
					if (num <= 3)
					{
						num3 = array4[0] * array5[num6];
						num3 = OracleNumberCore.LnxmulSetDigit1(array6, num10, num3);
						num10 -= 2;
						for (k = num6 - 1; k >= 0; k--)
						{
							num3 += array4[0] * array5[k];
							num3 = OracleNumberCore.LnxmulSetDigit1(array6, num10, num3);
							num10 -= 2;
						}
						OracleNumberCore.LnxmulSetDigit2(array6, num10, num3);
						num10 -= 2;
					}
					else
					{
						num3 += array4[num5] * array5[num6];
						num3 = OracleNumberCore.LnxmulSetDigit1(array6, num10, num3);
						num10 -= 2;
						for (k = num6 - 1; k > num6 - (num / 2 - 1); k--)
						{
							for (int m = num6 - k + 1; m > 0; m--)
							{
								num3 = OracleNumberCore.LnxmulSetSum(array4, array5, num5, k, m - 1, num3);
							}
							num3 = OracleNumberCore.LnxmulSetDigit1(array6, num10, num3);
							num10 -= 2;
						}
						do
						{
							for (int num11 = num / 2; num11 > 0; num11--)
							{
								num3 = OracleNumberCore.LnxmulSetSum(array4, array5, num5, k, num11 - 1, num3);
							}
							num3 = OracleNumberCore.LnxmulSetDigit1(array6, num10, num3);
							num10 -= 2;
							k--;
						}
						while (k >= 0);
						for (i = num5 - 1; i > 0; i--)
						{
							for (int num12 = i + 1; num12 > 0; num12--)
							{
								num3 = OracleNumberCore.LnxmulSetSum(array4, array5, i, 0, num12 - 1, num3);
							}
							num3 = OracleNumberCore.LnxmulSetDigit1(array6, num10, num3);
							num10 -= 2;
						}
						num3 += array4[0] * array5[0];
						num3 = OracleNumberCore.LnxmulSetDigit1(array6, num10, num3);
						num10 -= 2;
						OracleNumberCore.LnxmulSetDigit2(array6, num10, num3);
						num10 -= 2;
					}
					if ((num8 & 1) == 0 && array6[num10] != 1)
					{
						num7 += 1;
						num8++;
						num9--;
					}
					while (array6[num9 + num8 - 2] == 1)
					{
						num8--;
					}
					if (num8 > 21)
					{
						num10 = num9 + 19;
						num8 = 21;
						if ((int)array6[num10 + 1] > OracleNumberCore.LNXBASE / 2)
						{
							while ((int)array6[num10] == OracleNumberCore.LNXBASE)
							{
								num10--;
								num8--;
							}
							if (num10 < num9)
							{
								array6[num9] = 2;
								num7 += 1;
								num8++;
							}
							byte[] array8 = array6;
							int num13 = num10;
							array8[num13] += 1;
						}
						else
						{
							while (array6[num9 + num8 - 2] == 1)
							{
								num8--;
							}
						}
					}
					if (((int)num7 & 65535) > 255)
					{
						if (flag == flag2)
						{
							result = OracleNumberCore.GetPositiveInfinityByteRep();
						}
						else
						{
							result = OracleNumberCore.GetNegativeInfinityByteRep();
						}
					}
					else if (((int)num7 & 65535) < 128)
					{
						result = OracleNumberCore.GetZeroByteRep();
					}
					else
					{
						if (flag != flag2)
						{
							num8++;
							array3 = new byte[num8];
							array3[0] = (byte)(~(byte)num7);
							for (int num14 = 0; num14 < num8 - 1; num14++)
							{
								array3[num14 + 1] = (byte)(OracleNumberCore.LNXBASE + 2 - (int)array6[num9 + num14]);
							}
							array3[num8 - 1] = (byte)(OracleNumberCore.LNXBASE + 2);
						}
						else
						{
							array3 = new byte[num8];
							array3[0] = (byte)num7;
							for (int num14 = 0; num14 < num8 - 1; num14++)
							{
								array3[num14 + 1] = array6[num9 + num14];
							}
						}
						result = array3;
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
			return result;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00060A50 File Offset: 0x0005EC50
		internal static byte[] lnxdiv(byte[] n1, byte[] n2)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				int num = n1.Length;
				int num2 = n2.Length;
				byte[] array = new byte[22];
				int[] array2 = new int[22];
				int[] array3 = new int[10];
				int[] array4 = new int[13];
				bool flag = n1[0] >> 7 != 0;
				byte b = n1[0];
				if (!flag)
				{
					b = ~b;
					if ((int)n1[num - 1] == OracleNumberCore.LNXBASE + 2)
					{
						num--;
					}
				}
				bool flag2 = n2[0] >> 7 != 0;
				byte b2 = n2[0];
				if (!flag2)
				{
					b2 = ~b2;
					if ((int)n2[num2 - 1] == OracleNumberCore.LNXBASE + 2)
					{
						num2--;
					}
				}
				if ((b2 & 255) == 128 && num2 == 1)
				{
					if (flag == flag2)
					{
						result = OracleNumberCore.GetPositiveInfinityByteRep();
					}
					else
					{
						result = OracleNumberCore.GetNegativeInfinityByteRep();
					}
				}
				else if ((b & 255) == 128 && num == 1)
				{
					result = OracleNumberCore.GetZeroByteRep();
				}
				else
				{
					int num3;
					if (num == 1)
					{
						num3 = 0;
					}
					else
					{
						num3 = 1;
					}
					if (((b & 255) == 255 && (num == 2 || (int)n1[num3] == OracleNumberCore.LNXBASE + 1)) || (num == 1 && n1[0] == 0))
					{
						if (flag == flag2)
						{
							result = OracleNumberCore.GetPositiveInfinityByteRep();
						}
						else
						{
							result = OracleNumberCore.GetNegativeInfinityByteRep();
						}
					}
					else
					{
						if (num2 == 1)
						{
							num3 = 0;
						}
						else
						{
							num3 = 1;
						}
						if (((b2 & 255) == 255 && (num2 == 2 || (int)n2[num3] == OracleNumberCore.LNXBASE + 1)) || (num2 == 1 && n2[0] == 0))
						{
							result = OracleNumberCore.GetZeroByteRep();
						}
						else
						{
							int num4 = num / 2 - 1;
							int i = 21;
							int j = num - 2;
							while (i > num4)
							{
								array2[i] = 0;
								i--;
							}
							if (flag)
							{
								if ((num & 1) == 0)
								{
									array2[i] = (int)n1[j + 1] * OracleNumberCore.LNXBASE - OracleNumberCore.LNXBASE;
									j--;
									i--;
								}
								while (j > 0)
								{
									array2[i] = (int)n1[j] * OracleNumberCore.LNXBASE + (int)n1[j + 1] - (OracleNumberCore.LNXBASE + 1);
									j -= 2;
									i--;
								}
							}
							else
							{
								if ((num & 1) == 0)
								{
									array2[i] = (OracleNumberCore.LNXBASE + 1) * OracleNumberCore.LNXBASE - (int)n1[j + 1] * OracleNumberCore.LNXBASE;
									j--;
									i--;
								}
								while (j > 0)
								{
									array2[i] = (OracleNumberCore.LNXBASE + 1) * (OracleNumberCore.LNXBASE + 1) - ((int)n1[j] * OracleNumberCore.LNXBASE + (int)n1[j + 1]);
									j -= 2;
									i--;
								}
							}
							int num5 = num2 / 2 - 1;
							int num6 = num5;
							int k = num2 - 2;
							if (flag2)
							{
								if ((num2 & 1) == 0)
								{
									array3[num6] = (int)n2[k + 1] * OracleNumberCore.LNXBASE - OracleNumberCore.LNXBASE;
									k--;
									num6--;
								}
								while (k > 0)
								{
									array3[num6] = (int)n2[k] * OracleNumberCore.LNXBASE + (int)n2[k + 1] - (OracleNumberCore.LNXBASE + 1);
									k -= 2;
									num6--;
								}
							}
							else
							{
								if ((num2 & 1) == 0)
								{
									array3[num6] = (OracleNumberCore.LNXBASE + 1) * OracleNumberCore.LNXBASE - (int)n2[k + 1] * OracleNumberCore.LNXBASE;
									k--;
									num6--;
								}
								while (k > 0)
								{
									array3[num6] = (OracleNumberCore.LNXBASE + 1) * (OracleNumberCore.LNXBASE + 1) - ((int)n2[k] * OracleNumberCore.LNXBASE + (int)n2[k + 1]);
									k -= 2;
									num6--;
								}
							}
							int num7 = 0;
							int num8 = -1;
							if (num2 <= 3)
							{
								i = 0;
								int num9 = array2[0];
								int num10 = array3[0];
								do
								{
									int num11 = num9 / num10;
									i++;
									num9 -= num11 * num10;
									num9 = num9 * OracleNumberCore.LNXDIV_LNXBASE_SQUARED + array2[i];
									num8++;
									array4[num8] = num11;
									if (num9 == 0 && i >= num4)
									{
										break;
									}
								}
								while (num8 < 10 + ((array4[0] == 0) ? 2 : 1));
							}
							else
							{
								int num12 = 0;
								int num13 = num5;
								double num14 = (double)(array2[num12] * OracleNumberCore.LNXDIV_LNXBASE_SQUARED) + (double)array2[num12 + 1];
								double num15 = (double)(array3[0] * OracleNumberCore.LNXDIV_LNXBASE_SQUARED) + (double)array3[1];
								do
								{
									int l = (int)(num14 / num15);
									if (l != 0)
									{
										i = num12 + 2;
										num6 = 2;
										while (i <= num13)
										{
											array2[i] -= l * array3[num6];
											i++;
											num6++;
										}
									}
									num14 -= (double)l * num15;
									num14 = num14 * (double)OracleNumberCore.LNXDIV_LNXBASE_SQUARED + (double)array2[num12 + 2];
									if (l >= OracleNumberCore.LNXDIV_LNXBASE_SQUARED)
									{
										int num16 = num8;
										while (array4[num16] == OracleNumberCore.LNXDIV_LNXBASE_SQUARED - 1)
										{
											array4[num16] = 0;
											num16--;
										}
										array4[num16]++;
										l -= OracleNumberCore.LNXDIV_LNXBASE_SQUARED;
									}
									while (l < 0)
									{
										int num16 = num8;
										while (array4[num16] == 0)
										{
											array4[num16] = OracleNumberCore.LNXDIV_LNXBASE_SQUARED - 1;
											num16--;
										}
										array4[num16]--;
										l += OracleNumberCore.LNXDIV_LNXBASE_SQUARED;
									}
									num8++;
									array4[num8] = l;
									if (num12 >= num4 && ((num14 < 0.0) ? (-num14) : num14) < 0.1)
									{
										i = num12 + 2;
										while (i <= num13 && array2[i] == 0)
										{
											i++;
										}
										if (i > num13)
										{
											break;
										}
									}
									num12++;
									num13++;
								}
								while (num8 < 10 + ((array4[0] == 0) ? 2 : 1));
							}
							if (array4[0] == 0)
							{
								num7++;
							}
							while (array4[num8] == 0)
							{
								num8--;
							}
							int num17 = (array4[num7] >= OracleNumberCore.LNXBASE) ? 1 : 0;
							int num18 = (array4[num8] % OracleNumberCore.LNXBASE != 0) ? 1 : 0;
							int num19 = 2 * (num8 - num7) + num17 + num18;
							if (num19 > 20)
							{
								if (num17 > 0)
								{
									num8 = num7 + 9;
									array4[num8] += ((array4[num8 + 1] >= OracleNumberCore.LNXDIV_LNXBASE_SQUARED / 2) ? 1 : 0);
								}
								else
								{
									num8 = num7 + 10;
									array4[num8] = (array4[num8] + OracleNumberCore.LNXBASE / 2) / OracleNumberCore.LNXBASE * OracleNumberCore.LNXBASE;
								}
								if (array4[num8] == OracleNumberCore.LNXDIV_LNXBASE_SQUARED)
								{
									do
									{
										num8--;
									}
									while (array4[num8] == OracleNumberCore.LNXDIV_LNXBASE_SQUARED - 1);
									array4[num8]++;
								}
								if (array4[0] != 0)
								{
									num7 = 0;
								}
								while (array4[num8] == 0)
								{
									num8--;
								}
								num17 = ((array4[num7] >= OracleNumberCore.LNXBASE) ? 1 : 0);
								num18 = ((array4[num8] % OracleNumberCore.LNXBASE != 0) ? 1 : 0);
								num19 = 2 * (num8 - num7) + num17 + num18;
							}
							int num20 = (int)((b & byte.MaxValue) - (b2 & byte.MaxValue) - ((array4[0] == 0) ? 1 : 0) + 193);
							if (num20 > 255)
							{
								if (flag == flag2)
								{
									result = OracleNumberCore.GetPositiveInfinityByteRep();
								}
								else
								{
									result = OracleNumberCore.GetNegativeInfinityByteRep();
								}
							}
							else if (num20 < 128)
							{
								result = OracleNumberCore.GetZeroByteRep();
							}
							else
							{
								int num21 = num19 + 1;
								array = new byte[num21];
								int m = num19;
								int num16 = num8;
								if (num18 == 0)
								{
									array[m] = (byte)(array4[num16] / OracleNumberCore.LNXBASE + 1);
									m--;
									num16--;
								}
								while (m > 1)
								{
									int num22 = array4[num16] / OracleNumberCore.LNXBASE;
									int num23 = array4[num16] - num22 * OracleNumberCore.LNXBASE;
									array[m] = (byte)(num23 + 1);
									m--;
									array[m] = (byte)(num22 + 1);
									m--;
									num16--;
								}
								if (num17 == 0)
								{
									array[m] = (byte)(array4[num16] + 1);
								}
								array[0] = (byte)num20;
								if (flag != flag2)
								{
									num21++;
									byte[] array5;
									if (num21 > 20)
									{
										array5 = new byte[21];
										num21 = 21;
									}
									else
									{
										array5 = new byte[num21];
									}
									array5[0] = (byte)(~(byte)num20);
									int num24;
									for (num24 = 0; num24 < num21 - 2; num24++)
									{
										array5[num24 + 1] = (byte)(OracleNumberCore.LNXBASE + 2 - (int)array[num24 + 1]);
									}
									if (num21 <= 20)
									{
										array5[num21 - 1] = (byte)(OracleNumberCore.LNXBASE + 2);
									}
									else if (array.Length == 20)
									{
										array5[num21 - 1] = (byte)(OracleNumberCore.LNXBASE + 2);
									}
									else
									{
										array5[num24 + 1] = (byte)(OracleNumberCore.LNXBASE + 2 - (int)array[num24 + 1]);
									}
									result = array5;
								}
								else
								{
									result = array;
								}
							}
						}
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
			return result;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000612FC File Offset: 0x0005F4FC
		internal static byte[] lnxmod(byte[] n1, byte[] n2)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				byte[] array = OracleNumberCore.lnxdiv(n1, n2);
				byte[] n3 = OracleNumberCore.lnxtru(array, 0);
				array = OracleNumberCore.lnxmul(n2, n3);
				byte[] array2 = OracleNumberCore.lnxsub(n1, array);
				result = array2;
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

		// Token: 0x060008E0 RID: 2272 RVA: 0x00061390 File Offset: 0x0005F590
		internal static byte[] lnxsqr(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				int num = n.Length;
				int[] array = new int[29];
				int[] array2 = new int[29];
				if (!OracleNumberCore.IsPositive(n))
				{
					result = OracleNumberCore.GetNegativeInfinityByteRep();
				}
				else if (OracleNumberCore.IsPositiveInfinity(n))
				{
					result = OracleNumberCore.GetPositiveInfinityByteRep();
				}
				else if (OracleNumberCore.IsZero(n))
				{
					result = OracleNumberCore.GetZeroByteRep();
				}
				else
				{
					int num2 = (int)((n[0] & byte.MaxValue) - 193);
					for (int i = 1; i < num; i++)
					{
						array[i] = (int)(n[i] - 1);
					}
					int j = 1;
					int num3 = j + 20 + 3;
					int num4;
					if ((num2 + 128 & 1) != 0)
					{
						num4 = ((array[j] * OracleNumberCore.LNXBASE + array[j + 1]) * OracleNumberCore.LNXBASE + array[j + 2]) * OracleNumberCore.LNXBASE + array[j + 3];
						j += 3;
					}
					else
					{
						num4 = (array[j] * OracleNumberCore.LNXBASE + array[j + 1]) * OracleNumberCore.LNXBASE + array[j + 2];
						j += 2;
					}
					int num5 = (int)(Math.Sqrt((double)num4) * (double)OracleNumberCore.LNXBASE);
					array2[1] = num5 / (OracleNumberCore.LNXBASE * OracleNumberCore.LNXBASE);
					array2[2] = num5 / OracleNumberCore.LNXBASE % OracleNumberCore.LNXBASE;
					array2[3] = num5 % OracleNumberCore.LNXBASE;
					num4 -= array2[1] * num5;
					num4 = num4 * OracleNumberCore.LNXBASE + array[j + 1];
					num4 -= array2[2] * num5;
					num4 = num4 * OracleNumberCore.LNXBASE + array[j + 2];
					num4 -= array2[3] * num5;
					j += 3;
					num5 *= 2;
					int num6 = 3;
					int k = num6 + 1;
					int num8;
					while (j < num3)
					{
						num4 = num4 * OracleNumberCore.LNXBASE + array[j];
						int num7 = num4 / num5;
						num4 -= num7 * num5;
						array2[k] = num7;
						num8 = ((num6 + (num3 - j) < k) ? (num6 + (num3 - j)) : k);
						if (num7 != 0)
						{
							int num9 = j + 1;
							for (int l = num6 + 1; l < num8; l++)
							{
								array[num9] -= 2 * num7 * array2[l];
								num9++;
							}
							if (num9 < num3)
							{
								array[num9] -= num7 * num7;
							}
						}
						else if (num4 == 0)
						{
							int num9 = j + 1;
							while (num9 < num3 && array[num9] == 0)
							{
								num9++;
							}
							if (num9 == num3)
							{
								break;
							}
						}
						j++;
						k++;
					}
					num8 = k;
					k--;
					array2[0] = 0;
					while (k > 0)
					{
						while (array2[k] > OracleNumberCore.LNXBASE - 1)
						{
							array2[k] -= OracleNumberCore.LNXBASE;
							array2[k - 1]++;
						}
						while (array2[k] < 0)
						{
							array2[k] += OracleNumberCore.LNXBASE;
							array2[k - 1]--;
						}
						k--;
					}
					num2 = (num2 - (num2 + 128 & 1)) / 2 + 1;
					while (array2[k] == 0)
					{
						k++;
						num2--;
						if (num2 < -65)
						{
							return OracleNumberCore.GetZeroByteRep();
						}
					}
					do
					{
						num8--;
					}
					while (array2[num8] == 0);
					int num10 = num8 - k + 2;
					if (num10 > 21)
					{
						num8 = k + 20;
						if (array2[num8] >= OracleNumberCore.LNXBASE / 2)
						{
							do
							{
								num8--;
							}
							while (array2[num8] == OracleNumberCore.LNXBASE - 1);
							array2[num8]++;
						}
						else
						{
							do
							{
								num8--;
							}
							while (array2[num8] == 0);
						}
						if (num8 < k)
						{
							k = num8;
							num2++;
							if (num2 > 62)
							{
								return OracleNumberCore.GetPositiveInfinityByteRep();
							}
						}
						num10 = num8 - k + 2;
					}
					byte[] array3 = new byte[num10];
					array3[0] = (byte)(num2 - 63);
					for (int i = k; i <= num8; i++)
					{
						array3[i - (k - 1)] = (byte)(array2[i] + 1);
					}
					result = array3;
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

		// Token: 0x060008E1 RID: 2273 RVA: 0x0006180C File Offset: 0x0005FA0C
		internal static byte[] lnxceil(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				byte[] array = OracleNumberCore.lnxtru(n, 0);
				if (OracleNumberCore.compareBytes(array, n) != 0 && OracleNumberCore.IsPositive(n))
				{
					array = OracleNumberCore.lnxadd(array, OracleNumberCore.lnxqone);
				}
				result = array;
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

		// Token: 0x060008E2 RID: 2274 RVA: 0x000618A0 File Offset: 0x0005FAA0
		internal static byte[] lnxshift(byte[] n, int nDig)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				int num = n.Length;
				byte[] array = new byte[22];
				int i;
				if (num == 1)
				{
					i = 0;
				}
				else
				{
					i = 1;
				}
				if (((n[0] & 255) == 128 && num == 1) || (num == 2 && (n[0] & 255) == 255 && (int)n[i] == OracleNumberCore.LNXBASE + 1) || (num == 1 && n[0] == 0))
				{
					byte[] array2 = new byte[num];
					for (i = 0; i < num; i++)
					{
						array2[i] = n[i];
					}
					result = array2;
				}
				else
				{
					bool flag = n[0] >> 7 == 0;
					int num2 = (int)(flag ? (byte.MaxValue - n[0] & byte.MaxValue) : (n[0] & byte.MaxValue));
					int num3 = num;
					if ((nDig & 1) > 0)
					{
						byte[][] array3;
						byte[][] array4;
						byte b;
						if (flag)
						{
							if ((int)n[num3 - 1] == OracleNumberCore.LNXBASE + 2)
							{
								num3--;
							}
							array3 = OracleNumberCore.LnxqComponents_N;
							array4 = OracleNumberCore.LnxqDigit_N;
							b = (byte)(OracleNumberCore.LNXBASE + 1);
						}
						else
						{
							array3 = OracleNumberCore.LnxqComponents_P;
							array4 = OracleNumberCore.LnxqDigit_P;
							b = 1;
						}
						if (array3[(int)n[1]][0] != 0)
						{
							num2 = ((nDig >= 0) ? (num2 + (nDig / 2 + 1)) : (num2 - -nDig / 2));
							int j = num3 - 2;
							int k = num3 - 1;
							bool flag2;
							if (num3 > 20)
							{
								flag2 = (array3[(int)n[j + 1]][1] >= 5);
							}
							else
							{
								array[k + 1] = array4[(int)array3[(int)n[j + 1]][1]][0];
								num3++;
								flag2 = false;
							}
							while (j > 0)
							{
								array[k] = array4[(int)array3[(int)n[j]][1]][(int)array3[(int)n[j + 1]][0]];
								j--;
								k--;
							}
							array[1] = array4[0][(int)array3[(int)n[j + 1]][0]];
							if (flag2)
							{
								int num4 = flag ? 2 : OracleNumberCore.LNXBASE;
								int num5 = flag ? -1 : 1;
								k = 20;
								while ((int)array[k] == num4)
								{
									k--;
									num3--;
								}
								byte[] array5 = array;
								int num6 = k;
								array5[num6] += (byte)num5;
							}
						}
						else
						{
							num2 = ((nDig >= 0) ? (num2 + nDig / 2) : (num2 - (-nDig / 2 + 1)));
							int j = 1;
							int k;
							for (k = 1; k < num3 - 1; k++)
							{
								array[k] = array4[(int)array3[(int)n[j]][1]][(int)array3[(int)n[j + 1]][0]];
								j++;
							}
							array[k] = array4[(int)array3[(int)n[j]][1]][0];
						}
						while (array[num3 - 1] == b)
						{
							num3--;
						}
						if (flag)
						{
							num3++;
							array[num3 - 1] = (byte)(OracleNumberCore.LNXBASE + 2);
						}
					}
					else
					{
						num2 = ((nDig >= 0) ? (num2 + nDig / 2) : (num2 - -nDig / 2));
						for (i = 1; i < num3; i++)
						{
							array[i] = n[i];
						}
					}
					if (num2 > 255)
					{
						byte[] array2;
						if (flag)
						{
							array2 = new byte[]
							{
								0
							};
						}
						else
						{
							array2 = new byte[]
							{
								byte.MaxValue,
								(byte)(OracleNumberCore.LNXBASE + 1)
							};
						}
						result = array2;
					}
					else if (num2 < 128)
					{
						result = new byte[]
						{
							128
						};
					}
					else
					{
						array[0] = (flag ? ((byte)(255 - num2)) : ((byte)num2));
						byte[] array2 = new byte[num3];
						for (i = 0; i < num3; i++)
						{
							array2[i] = array[i];
						}
						result = array2;
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
			return result;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00061C74 File Offset: 0x0005FE74
		internal static byte[] lnxfpr(byte[] n, int precision)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				int num = n.Length;
				if (OracleNumberCore.IsZero(n))
				{
					result = OracleNumberCore.GetZeroByteRep();
				}
				else if (OracleNumberCore.IsNegativeInfinity(n))
				{
					result = OracleNumberCore.GetNegativeInfinityByteRep();
				}
				else if (OracleNumberCore.IsPositiveInfinity(n))
				{
					result = OracleNumberCore.GetPositiveInfinityByteRep();
				}
				else if (precision < 0)
				{
					result = OracleNumberCore.GetZeroByteRep();
				}
				else
				{
					bool flag;
					int num2;
					bool flag2;
					byte b;
					byte b2;
					int num3;
					if (flag = OracleNumberCore.IsPositive(n))
					{
						precision += (((n[1] & byte.MaxValue) < 11) ? 2 : 1);
						num2 = precision >> 1;
						flag2 = ((precision & 1) == 1);
						b = 1;
						b2 = (byte)OracleNumberCore.LNXBASE;
						num3 = 1;
					}
					else
					{
						precision += (((n[1] & byte.MaxValue) > 91) ? 2 : 1);
						num2 = precision >> 1;
						flag2 = ((precision & 1) == 1);
						b = (byte)(OracleNumberCore.LNXBASE + 1);
						b2 = 2;
						num3 = -1;
						num -= (((int)(n[num - 1] & byte.MaxValue) == OracleNumberCore.LNXBASE + 2) ? 1 : 0);
					}
					byte[] array = new byte[num];
					Array.Copy(n, 0, array, 0, num);
					if (num2 > num - 1 || (num2 == num - 1 && (flag2 || OracleNumberCore.LnxqFirstDigit[(int)n[num2]] == 1)))
					{
						result = OracleNumberCore.SetLength(n, num);
					}
					else if ((num2 == 0 && (!flag2 || (flag ? (n[1] < 51) : (n[1] > 51)))) || (num2 == 1 && !flag2 && (flag ? (n[1] < 6) : (n[1] > 96))))
					{
						result = OracleNumberCore.GetZeroByteRep();
					}
					else if (num2 == 0)
					{
						if (OracleNumberCore.IsInfinity(n))
						{
							if (flag)
							{
								result = OracleNumberCore.GetPositiveInfinityByteRep();
							}
							else
							{
								result = OracleNumberCore.GetNegativeInfinityByteRep();
							}
						}
						else
						{
							array[0] = (byte)((int)n[0] + num3);
							array[1] = (byte)((int)b + num3);
							result = OracleNumberCore.SetLength(array, 2);
						}
					}
					else
					{
						byte b4;
						byte b3 = b4 = (byte)num2;
						if (flag2)
						{
							if (flag ? (n[(int)(b3 + 1)] > 50) : (n[(int)(b3 + 1)] < 52))
							{
								array[(int)b4] = (byte)((int)n[(int)b3] + num3);
							}
							else
							{
								array[(int)b4] = n[(int)b3];
							}
						}
						else
						{
							array[(int)b4] = (flag ? OracleNumberCore.LnxqRound_P[(int)n[(int)b3]] : OracleNumberCore.LnxqRound_N[(int)n[(int)b3]]);
						}
						b3 -= 1;
						int size;
						if ((int)array[(int)b4] == (int)b2 + num3)
						{
							while (b3 > 0 && n[(int)b3] == b2)
							{
								b3 -= 1;
							}
							if (b3 == 0)
							{
								if (!OracleNumberCore.IsInfinity(n))
								{
									array[0] = (byte)((int)n[0] + num3);
									array[1] = (byte)((int)b + num3);
									return OracleNumberCore.SetLength(array, 2);
								}
								if (flag)
								{
									return OracleNumberCore.GetPositiveInfinityByteRep();
								}
								return OracleNumberCore.GetNegativeInfinityByteRep();
							}
							else
							{
								array[(int)b3] = (byte)((int)n[(int)b3] + num3);
								size = (int)(b3 + 1);
								b3 -= 1;
							}
						}
						else if (array[(int)b4] == b)
						{
							while (n[(int)b3] == b)
							{
								b3 -= 1;
							}
							size = (int)(b3 + 1);
						}
						else
						{
							size = num2 + 1;
						}
						result = OracleNumberCore.SetLength(array, size);
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
			return result;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00061FA4 File Offset: 0x000601A4
		internal static byte[] lnxflo(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				byte[] array = OracleNumberCore.lnxtru(n, 0);
				if (OracleNumberCore.compareBytes(array, n) != 0 && !OracleNumberCore.IsPositive(n))
				{
					array = OracleNumberCore.lnxsub(array, OracleNumberCore.lnxqone);
				}
				result = array;
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

		// Token: 0x060008E5 RID: 2277 RVA: 0x00062038 File Offset: 0x00060238
		internal static byte[] lnxrou(byte[] n, int decimal_place)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				int num = n.Length;
				byte b = 0;
				if (num == 1)
				{
					if (n[(int)b] == 128)
					{
						result = OracleNumberCore.GetZeroByteRep();
					}
					else
					{
						result = OracleNumberCore.GetNegativeInfinityByteRep();
					}
				}
				else if (num == 2 && n[0] == 255 && n[1] == (byte)(OracleNumberCore.LNXBASE + 1))
				{
					result = OracleNumberCore.GetPositiveInfinityByteRep();
				}
				else
				{
					int num2 = (n[0] < 0) ? (256 + (int)n[0]) : ((int)n[0]);
					bool flag;
					int num3;
					bool flag2;
					byte b2;
					byte b3;
					sbyte b4;
					if (flag = OracleNumberCore.IsPositive(n))
					{
						if (decimal_place >= 0)
						{
							num3 = num2 + (decimal_place + 1 >> 1) - 192;
							flag2 = ((decimal_place & 1) != 0);
						}
						else
						{
							decimal_place = -decimal_place;
							num3 = num2 - (decimal_place >> 1) - 192;
							flag2 = ((decimal_place & 1) != 0);
						}
						b2 = 1;
						b3 = (byte)OracleNumberCore.LNXBASE;
						b4 = 1;
					}
					else
					{
						if (decimal_place >= 0)
						{
							num3 = 63 + (decimal_place + 1 >> 1) - num2;
							flag2 = ((decimal_place & 1) != 0);
						}
						else
						{
							decimal_place = -decimal_place;
							num3 = 63 - (decimal_place >> 1) - num2;
							flag2 = ((decimal_place & 1) != 0);
						}
						b2 = (byte)(OracleNumberCore.LNXBASE + 1);
						b3 = 2;
						b4 = -1;
						num -= (((int)n[num - 1] == OracleNumberCore.LNXBASE + 2) ? 1 : 0);
					}
					byte[] array = new byte[num];
					Array.Copy(n, 0, array, 0, num);
					if (num3 > num - 1 || (num3 == num - 1 && (!flag2 || OracleNumberCore.LnxqFirstDigit[(int)n[num3]] == 1)))
					{
						result = OracleNumberCore.SetLength(n, num);
					}
					else if (num3 < 0 || (num3 == 0 && (flag2 || (flag ? (n[1] < 51) : (n[1] > 51)))) || (num3 == 1 && flag2 && (flag ? (n[1] < 6) : (n[1] > 96))))
					{
						result = OracleNumberCore.GetZeroByteRep();
					}
					else if (num3 == 0)
					{
						if (flag ? (n[(int)b] == 255) : (n[(int)b] == 0))
						{
							if (flag)
							{
								result = OracleNumberCore.GetPositiveInfinityByteRep();
							}
							else
							{
								result = OracleNumberCore.GetNegativeInfinityByteRep();
							}
						}
						else
						{
							array[0] = n[(int)b] + (byte)b4;
							array[1] = b2 + (byte)b4;
							result = OracleNumberCore.SetLength(array, 2);
						}
					}
					else
					{
						byte b6;
						byte b5 = b6 = (byte)num3;
						if (flag2)
						{
							array[(int)b5] = (flag ? OracleNumberCore.LnxqRound_P[(int)n[(int)b6]] : OracleNumberCore.LnxqRound_N[(int)n[(int)b6]]);
						}
						else if (flag ? (n[(int)(b6 + 1)] > 50) : (n[(int)(b6 + 1)] < 52))
						{
							array[(int)b5] = n[(int)b6] + (byte)b4;
						}
						else
						{
							array[(int)b5] = n[(int)b6];
						}
						b6 -= 1;
						int size;
						if (array[(int)b5] == b3 + (byte)b4)
						{
							while (b6 > b && n[(int)b6] == b3)
							{
								b6 -= 1;
							}
							if (b6 == b)
							{
								if (!(flag ? (n[(int)b] == 255) : (n[(int)b] == 0)))
								{
									array[0] = n[(int)b] + (byte)b4;
									array[1] = b2 + (byte)b4;
									return OracleNumberCore.SetLength(array, 2);
								}
								if (flag)
								{
									return OracleNumberCore.GetPositiveInfinityByteRep();
								}
								return OracleNumberCore.GetNegativeInfinityByteRep();
							}
							else
							{
								array[(int)(b6 - b)] = n[(int)b6] + (byte)b4;
								size = (int)(b6 - b + 1);
								b6 -= 1;
							}
						}
						else if (array[(int)b5] == b2)
						{
							while (n[(int)b6] == b2)
							{
								b6 -= 1;
							}
							size = (int)(b6 - b + 1);
						}
						else
						{
							size = num3 + 1;
						}
						result = OracleNumberCore.SetLength(array, size);
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
			return result;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000623E0 File Offset: 0x000605E0
		internal static byte[] lnxtru(byte[] n, int decimal_place)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				int num = n.Length;
				if (OracleNumberCore.IsZero(n))
				{
					result = OracleNumberCore.GetZeroByteRep();
				}
				else if (OracleNumberCore.IsNegativeInfinity(n))
				{
					result = OracleNumberCore.GetNegativeInfinityByteRep();
				}
				else if (OracleNumberCore.IsPositiveInfinity(n))
				{
					result = OracleNumberCore.GetPositiveInfinityByteRep();
				}
				else
				{
					int num2 = (n[0] < 0) ? (256 + (int)n[0]) : ((int)n[0]);
					bool flag;
					int num3;
					bool flag2;
					byte b;
					if (flag = OracleNumberCore.IsPositive(n))
					{
						if (decimal_place >= 0)
						{
							num3 = num2 + (decimal_place + 1 >> 1) - 192;
							flag2 = ((decimal_place & 1) == 1);
						}
						else
						{
							decimal_place = -decimal_place;
							num3 = num2 - (decimal_place >> 1) - 192;
							flag2 = ((decimal_place & 1) == 1);
						}
						b = 1;
					}
					else
					{
						if (decimal_place >= 0)
						{
							num3 = 63 + (decimal_place + 1 >> 1) - num2;
							flag2 = ((decimal_place & 1) == 1);
						}
						else
						{
							decimal_place = -decimal_place;
							num3 = 63 - (decimal_place >> 1) - num2;
							flag2 = ((decimal_place & 1) == 1);
						}
						b = (byte)(OracleNumberCore.LNXBASE + 1);
						if ((int)n[num - 1] == OracleNumberCore.LNXBASE + 2)
						{
							num--;
						}
					}
					byte[] array = new byte[num];
					Array.Copy(n, 0, array, 0, num);
					if (num3 > num - 1 || (num3 == num - 1 && flag2 && OracleNumberCore.LnxqFirstDigit[(int)n[num3]] == 1))
					{
						result = OracleNumberCore.SetLength(n, num);
					}
					else if (num3 <= 0 || (num3 == 1 && flag2 && (flag ? (n[1] < 11) : (n[1] > 91))))
					{
						result = OracleNumberCore.GetZeroByteRep();
					}
					else
					{
						byte b3;
						byte b2 = b3 = (byte)num3;
						if (flag2)
						{
							if (flag)
							{
								array[(int)b3] = OracleNumberCore.LnxqTruncate_P[(int)n[(int)b2]];
							}
							else
							{
								array[(int)b3] = OracleNumberCore.LnxqTruncate_N[(int)n[(int)b2]];
							}
						}
						else
						{
							array[(int)b3] = n[(int)b2];
						}
						b2 -= 1;
						int size;
						if (array[(int)b3] == b)
						{
							while (n[(int)b2] == b)
							{
								b2 -= 1;
							}
							size = (int)(b2 + 1);
						}
						else
						{
							size = num3 + 1;
						}
						result = OracleNumberCore.SetLength(array, size);
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
			return result;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0006263C File Offset: 0x0006083C
		internal static byte[] lnxpow(byte[] n, int power)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				byte[] array;
				if (power >= 0)
				{
					array = new byte[n.Length];
					Array.Copy(n, 0, array, 0, n.Length);
				}
				else
				{
					int minValue = int.MinValue;
					if (power == minValue)
					{
						array = OracleNumberCore.lnxpow(n, minValue + 1);
						return OracleNumberCore.lnxdiv(array, n);
					}
					power = -power;
					array = OracleNumberCore.lnxdiv(OracleNumberCore.lnxqone, n);
				}
				byte[] array2 = OracleNumberCore.lnxqone;
				while (power > 0)
				{
					if ((power & 1) == 1)
					{
						array2 = OracleNumberCore.lnxmul(array2, array);
					}
					if ((power >>= 1) > 0)
					{
						array = OracleNumberCore.lnxmul(array, array);
					}
				}
				result = array2;
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

		// Token: 0x060008E8 RID: 2280 RVA: 0x00062724 File Offset: 0x00060924
		internal static byte[] lnxbex(byte[] b, byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				switch (OracleNumberCore.lnxsgn(b))
				{
				case -1:
					if (OracleNumberCore.IsInt(n))
					{
						byte[] array = OracleNumberCore.lnxneg(b);
						array = OracleNumberCore.lnxln(array);
						array = OracleNumberCore.lnxmul(n, array);
						array = OracleNumberCore.lnxexp(array);
						if (!OracleNumberCore.IsZero(OracleNumberCore.lnxmod(n, OracleNumberCore.lnxqtwo)))
						{
							array = OracleNumberCore.lnxneg(array);
						}
						result = array;
					}
					else
					{
						result = OracleNumberCore.GetPositiveInfinityByteRep();
					}
					break;
				case 0:
					if (OracleNumberCore.IsZero(n))
					{
						byte[] array = new byte[OracleNumberCore.lnxqone.Length];
						Array.Copy(OracleNumberCore.lnxqone, 0, array, 0, OracleNumberCore.lnxqone.Length);
						result = array;
					}
					else
					{
						result = OracleNumberCore.GetZeroByteRep();
					}
					break;
				case 1:
				{
					byte[] array = OracleNumberCore.lnxln(b);
					array = OracleNumberCore.lnxmul(n, array);
					result = OracleNumberCore.lnxexp(array);
					break;
				}
				default:
					result = null;
					break;
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

		// Token: 0x060008E9 RID: 2281 RVA: 0x00062850 File Offset: 0x00060A50
		internal static byte[] lnxln(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				if (OracleNumberCore.lnxsgn(n) <= 0)
				{
					result = OracleNumberCore.GetNegativeInfinityByteRep();
				}
				else if (OracleNumberCore.IsPositiveInfinity(n))
				{
					result = OracleNumberCore.GetPositiveInfinityByteRep();
				}
				else
				{
					byte[] array = new byte[n.Length];
					Array.Copy(n, 0, array, 0, n.Length);
					int num = (int)((array[0] & byte.MaxValue) - 193);
					array[0] = 193;
					double d = OracleNumberCore.lnxnur(array);
					double doubleNum = Math.Log(d);
					byte[] byteRep = OracleNumberCore.GetByteRep(doubleNum);
					byte[] array2 = OracleNumberCore.lnxexp(byteRep);
					byte[] array3 = OracleNumberCore.lnxdiv(array, array2);
					array3 = OracleNumberCore.lnxsub(array3, OracleNumberCore.lnxqone);
					byte[] array4 = new byte[array3.Length];
					Array.Copy(array3, 0, array4, 0, array3.Length);
					byte[] array5 = OracleNumberCore.lnxmul(array3, array3);
					int num2 = 1;
					while ((array5[0] & 255) > 172)
					{
						num2++;
						array2 = OracleNumberCore.lnxqIDiv(array5, num2);
						array4 = OracleNumberCore.lnxsub(array4, array2);
						array5 = OracleNumberCore.lnxmul(array3, array5);
						num2++;
						array2 = OracleNumberCore.lnxqIDiv(array5, num2);
						array4 = OracleNumberCore.lnxadd(array4, array2);
						array5 = OracleNumberCore.lnxmul(array3, array5);
					}
					num *= 2;
					byte[] ln = OracleNumberCore.LN10;
					array3 = OracleNumberCore.lnxmin((long)num);
					array2 = OracleNumberCore.lnxmul(array3, ln);
					array2 = OracleNumberCore.lnxadd(array2, byteRep);
					result = OracleNumberCore.lnxadd(array2, array4);
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

		// Token: 0x060008EA RID: 2282 RVA: 0x00062A24 File Offset: 0x00060C24
		internal static byte[] lnxlog(byte[] n, byte[] b)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				double num = OracleNumberCore.lnxnur(b);
				if (num > 0.0)
				{
					if (num == 10.0)
					{
						byte[] n2 = OracleNumberCore.lnxln(n);
						byte[] ln = OracleNumberCore.LN10;
						result = OracleNumberCore.lnxdiv(n2, ln);
					}
					else
					{
						byte[] n2 = OracleNumberCore.lnxln(n);
						byte[] n3 = OracleNumberCore.lnxln(b);
						result = OracleNumberCore.lnxdiv(n2, n3);
					}
				}
				else
				{
					result = OracleNumberCore.GetNegativeInfinityByteRep();
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

		// Token: 0x060008EB RID: 2283 RVA: 0x00062AEC File Offset: 0x00060CEC
		internal static byte[] lnxexp(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				result = OracleNumberCore.lnxqtra(n, 9);
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

		// Token: 0x060008EC RID: 2284 RVA: 0x00062B64 File Offset: 0x00060D64
		internal static byte[] lnxsin(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				result = OracleNumberCore.lnxqtra(n, 4);
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

		// Token: 0x060008ED RID: 2285 RVA: 0x00062BDC File Offset: 0x00060DDC
		internal static byte[] lnxsnh(byte[] n)
		{
			return OracleNumberCore.lnxqtra(n, 7);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00062BE8 File Offset: 0x00060DE8
		internal static byte[] lnxasin(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				result = OracleNumberCore.lnxqtri(n, 1);
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

		// Token: 0x060008EF RID: 2287 RVA: 0x00062C60 File Offset: 0x00060E60
		internal static byte[] lnxcos(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				result = OracleNumberCore.lnxqtra(n, 3);
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

		// Token: 0x060008F0 RID: 2288 RVA: 0x00062CD8 File Offset: 0x00060ED8
		internal static byte[] lnxcsh(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				result = OracleNumberCore.lnxqtra(n, 6);
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

		// Token: 0x060008F1 RID: 2289 RVA: 0x00062D50 File Offset: 0x00060F50
		internal static byte[] lnxacos(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				result = OracleNumberCore.lnxqtri(n, 0);
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

		// Token: 0x060008F2 RID: 2290 RVA: 0x00062DC8 File Offset: 0x00060FC8
		internal static byte[] lnxtan(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				result = OracleNumberCore.lnxqtra(n, 5);
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

		// Token: 0x060008F3 RID: 2291 RVA: 0x00062E40 File Offset: 0x00061040
		internal static byte[] lnxatan(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				result = OracleNumberCore.lnxqtri(n, 2);
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

		// Token: 0x060008F4 RID: 2292 RVA: 0x00062EB8 File Offset: 0x000610B8
		internal static byte[] lnxatan2(byte[] y, byte[] x)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				if (OracleNumberCore.IsZero(y) && OracleNumberCore.IsZero(x))
				{
					throw new ArgumentException();
				}
				byte[] array = OracleNumberCore.lnxdiv(y, x);
				array = OracleNumberCore.lnxatan(array);
				if (OracleNumberCore.IsPositive(x))
				{
					result = array;
				}
				else
				{
					byte[] pi = OracleNumberCore.PI;
					if (OracleNumberCore.IsPositive(y))
					{
						result = OracleNumberCore.lnxadd(array, pi);
					}
					else
					{
						result = OracleNumberCore.lnxsub(array, pi);
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
			return result;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00062F78 File Offset: 0x00061178
		internal static byte[] lnxtnh(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				result = OracleNumberCore.lnxqtra(n, 8);
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

		// Token: 0x060008F6 RID: 2294 RVA: 0x00062FF0 File Offset: 0x000611F0
		private static int LnxmulSetSum(int[] ptr1, int[] ptr2, int index1, int index2, int element, int sum)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				int num = 0;
				try
				{
					num = sum + ptr1[index1 - element] * ptr2[index2 + element];
				}
				catch (IndexOutOfRangeException)
				{
					throw new IndexOutOfRangeException("INVALIDORLN");
				}
				result = num;
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

		// Token: 0x060008F7 RID: 2295 RVA: 0x0006308C File Offset: 0x0006128C
		private static int LnxmulSetDigit1(byte[] rslBuf, int index, int sum)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				int num = sum / OracleNumberCore.LNXBASE;
				int num2 = sum / (OracleNumberCore.LNXBASE * OracleNumberCore.LNXBASE);
				index -= 2;
				rslBuf[index + 1] = (byte)(sum - num * OracleNumberCore.LNXBASE + 1);
				rslBuf[index] = (byte)(num - num2 * OracleNumberCore.LNXBASE + 1);
				result = num2;
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

		// Token: 0x060008F8 RID: 2296 RVA: 0x00063138 File Offset: 0x00061338
		private static void LnxmulSetDigit2(byte[] rslBuf, int index, int sum)
		{
			int num = sum / OracleNumberCore.LNXBASE;
			index -= 2;
			rslBuf[index] = (byte)(num + 1);
			rslBuf[index + 1] = (byte)(sum - num * OracleNumberCore.LNXBASE + 1);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0006316C File Offset: 0x0006136C
		internal static int compareBytes(byte[] abyte0, byte[] abyte1)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				int num = abyte0.Length;
				int num2 = abyte1.Length;
				int i = 0;
				int num3 = Math.Min(num, num2);
				while (i < num3)
				{
					int num4 = (int)(abyte0[i] & byte.MaxValue);
					int num5 = (int)(abyte1[i] & byte.MaxValue);
					if (num4 != num5)
					{
						return (num4 < num5) ? -1 : 1;
					}
					i++;
				}
				if (num == num2)
				{
					result = 0;
				}
				else
				{
					result = ((num > num2) ? 1 : -1);
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

		// Token: 0x060008FA RID: 2298 RVA: 0x00063238 File Offset: 0x00061438
		private static int lnxcmp(byte[] n1, byte[] n2)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				result = OracleNumberCore.compareBytes(n1, n2);
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

		// Token: 0x060008FB RID: 2299 RVA: 0x000632B0 File Offset: 0x000614B0
		private static int lnxsgn(byte[] n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (OracleNumberCore.IsZero(n))
				{
					result = 0;
				}
				else if (OracleNumberCore.IsPositive(n))
				{
					result = 1;
				}
				else
				{
					result = -1;
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

		// Token: 0x060008FC RID: 2300 RVA: 0x00063338 File Offset: 0x00061538
		private static byte[] lnxqIDiv(byte[] y, int x)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				byte[] n = OracleNumberCore.lnxmin((long)x);
				result = OracleNumberCore.lnxdiv(y, n);
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

		// Token: 0x060008FD RID: 2301 RVA: 0x000633B8 File Offset: 0x000615B8
		private static byte[] lnxqtra(byte[] n, int funcid)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				byte[] pi = OracleNumberCore.PI;
				byte[] array = OracleNumberCore.lnxmin(-1L);
				long num = 0L;
				byte[] array3;
				byte[] n2;
				if (funcid == 3 || funcid == 4 || funcid == 5)
				{
					byte[] array2 = OracleNumberCore.lnxmul(OracleNumberCore.lnxqtwo, pi);
					array3 = OracleNumberCore.lnxabs(n);
					array3 = OracleNumberCore.lnxmod(array3, array2);
					if (OracleNumberCore.lnxcmp(array3, pi) > 0)
					{
						array3 = OracleNumberCore.lnxsub(array3, array2);
					}
					if (OracleNumberCore.lnxsgn(n) == -1)
					{
						array3 = OracleNumberCore.lnxneg(array3);
					}
					n2 = OracleNumberCore.lnxmul(array3, array3);
				}
				else if (funcid == 9)
				{
					array3 = OracleNumberCore.lnxmod(n, OracleNumberCore.lnxqone);
					byte[] array2 = OracleNumberCore.lnxsub(n, array3);
					if ((array2[0] & 255) < 60)
					{
						return OracleNumberCore.GetZeroByteRep();
					}
					if ((array2[0] & 255) > 195)
					{
						return OracleNumberCore.GetPositiveInfinityByteRep();
					}
					num = OracleNumberCore.lnxsni(array2);
					n2 = OracleNumberCore.lnxmul(array3, array3);
				}
				else
				{
					array3 = new byte[n.Length];
					Array.Copy(n, 0, array3, 0, n.Length);
					n2 = OracleNumberCore.lnxmul(array3, array3);
				}
				byte[] array4 = null;
				byte[] array5 = null;
				if (funcid != 4 && funcid != 7)
				{
					byte[] array2 = OracleNumberCore.lnxqone;
					array4 = OracleNumberCore.lnxqone;
					array5 = OracleNumberCore.GetZeroByteRep();
					int num2 = 0;
					do
					{
						array2 = OracleNumberCore.lnxmul(n2, array2);
						int x = (num2 + 1) * (num2 + 2);
						num2 += 2;
						array2 = OracleNumberCore.lnxqIDiv(array2, x);
						array5 = OracleNumberCore.lnxadd(array5, array2);
						array2 = OracleNumberCore.lnxmul(n2, array2);
						x = (num2 + 1) * (num2 + 2);
						num2 += 2;
						array2 = OracleNumberCore.lnxqIDiv(array2, x);
						array4 = OracleNumberCore.lnxadd(array4, array2);
					}
					while ((array2[0] & 255) + 20 >= (array4[0] & 255) && (array5[0] & 255) != 255);
				}
				byte[] array6 = null;
				byte[] array7 = null;
				if (funcid != 3 && funcid != 6)
				{
					byte[] array2 = new byte[array3.Length];
					Array.Copy(array3, 0, array2, 0, array3.Length);
					array6 = new byte[array3.Length];
					Array.Copy(array3, 0, array6, 0, array3.Length);
					array7 = OracleNumberCore.GetZeroByteRep();
					int num3 = 1;
					do
					{
						array2 = OracleNumberCore.lnxmul(n2, array2);
						int x = (num3 + 1) * (num3 + 2);
						num3 += 2;
						array2 = OracleNumberCore.lnxqIDiv(array2, x);
						array7 = OracleNumberCore.lnxadd(array7, array2);
						array2 = OracleNumberCore.lnxmul(n2, array2);
						x = (num3 + 1) * (num3 + 2);
						num3 += 2;
						array2 = OracleNumberCore.lnxqIDiv(array2, x);
						array6 = OracleNumberCore.lnxadd(array6, array2);
					}
					while (((array2[0] & 255) != 128 || array2.Length != 1) && ((array2[0] & 255) < 128 || (array2[0] & 255) + 20 >= (array6[0] & 255)) && ((array2[0] & 255) >= 128 || (array2[0] & 255) <= (array6[0] & 255) + 20) && (array7[0] & 255) != 255 && (array7[0] & 255) != 0);
				}
				byte[] array8 = null;
				if (funcid == 3 || funcid == 4 || funcid == 5)
				{
					if (funcid == 3 || funcid == 5)
					{
						array8 = OracleNumberCore.lnxsub(array4, array5);
						if (OracleNumberCore.lnxcmp(array8, OracleNumberCore.lnxqone) > 0)
						{
							array8 = OracleNumberCore.lnxqone;
						}
						else if (OracleNumberCore.lnxcmp(array8, array) < 0)
						{
							array8 = array;
						}
					}
					if (funcid == 3)
					{
						result = array8;
					}
					else
					{
						byte[] array9 = OracleNumberCore.lnxsub(array6, array7);
						if (OracleNumberCore.lnxcmp(array9, OracleNumberCore.lnxqone) > 0)
						{
							array9 = OracleNumberCore.lnxqone;
						}
						else if (OracleNumberCore.lnxcmp(array9, array) < 0)
						{
							array9 = array;
						}
						if (funcid == 4)
						{
							result = array9;
						}
						else
						{
							result = OracleNumberCore.lnxdiv(array9, array8);
						}
					}
				}
				else if (funcid == 6)
				{
					result = OracleNumberCore.lnxadd(array4, array5);
				}
				else if (funcid == 7)
				{
					result = OracleNumberCore.lnxadd(array6, array7);
				}
				else
				{
					byte[] array9 = OracleNumberCore.lnxadd(array6, array7);
					array8 = OracleNumberCore.lnxadd(array4, array5);
					if (funcid == 8)
					{
						result = OracleNumberCore.lnxdiv(array9, array8);
					}
					else
					{
						byte[] e = OracleNumberCore.E;
						byte[] array10 = OracleNumberCore.lnxadd(array8, array9);
						array3 = OracleNumberCore.lnxpow(e, (int)num);
						array10 = OracleNumberCore.lnxmul(array10, array3);
						result = array10;
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
			return result;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00063824 File Offset: 0x00061A24
		private static byte[] lnxqtri(byte[] n, int funcid)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				byte[] pi = OracleNumberCore.PI;
				byte[] array = OracleNumberCore.lnxdiv(pi, OracleNumberCore.lnxqtwo);
				if (funcid == 2)
				{
					if (OracleNumberCore.IsPositiveInfinity(n))
					{
						return array;
					}
					if (OracleNumberCore.IsNegativeInfinity(n))
					{
						return OracleNumberCore.lnxneg(array);
					}
				}
				byte[] array2 = OracleNumberCore.lnxabs(n);
				byte[] array4;
				if (funcid == 1 || funcid == 0)
				{
					if (OracleNumberCore.lnxcmp(array2, OracleNumberCore.lnxqone) > 0)
					{
						throw new ArgumentException("INVALIDINPUTN");
					}
					if ((array2[0] & 255) <= 183)
					{
						if (funcid == 1)
						{
							byte[] array3 = new byte[n.Length];
							Array.Copy(n, 0, array3, 0, n.Length);
							return array3;
						}
						return OracleNumberCore.lnxsub(array, n);
					}
					else
					{
						array4 = OracleNumberCore.lnxsub(OracleNumberCore.lnxqone, array2);
						byte[] array5 = OracleNumberCore.lnxadd(OracleNumberCore.lnxqone, array2);
						array2 = OracleNumberCore.lnxdiv(array4, array5);
						array2 = OracleNumberCore.lnxsqr(array2);
					}
				}
				int num;
				if ((num = OracleNumberCore.lnxcmp(array2, OracleNumberCore.lnxqone)) > 0)
				{
					array2 = OracleNumberCore.lnxdiv(OracleNumberCore.lnxqone, array2);
				}
				array4 = new byte[array2.Length];
				Array.Copy(array2, 0, array4, 0, array2.Length);
				int num2 = 1;
				for (;;)
				{
					byte[] array5 = OracleNumberCore.lnxtan(array4);
					byte[] n2 = OracleNumberCore.lnxsub(array2, array5);
					array5 = OracleNumberCore.lnxmul(array5, array5);
					array5 = OracleNumberCore.lnxadd(array5, OracleNumberCore.lnxqone);
					array5 = OracleNumberCore.lnxdiv(n2, array5);
					int num3 = (int)(((array5[0] & byte.MaxValue) >= 128) ? ((array5[0] & byte.MaxValue) - 193) : (62 - (array5[0] & byte.MaxValue)));
					int num4 = (int)(((array4[0] & byte.MaxValue) >= 128) ? ((array4[0] & byte.MaxValue) - 193) : (62 - (array4[0] & byte.MaxValue)));
					if (((array5[0] & 255) == 128 && array5.Length == 1) || (num3 & 255) + 15 < (num4 & 255) || num2 > 15)
					{
						break;
					}
					array4 = OracleNumberCore.lnxadd(array4, array5);
					num2++;
				}
				if (num > 0)
				{
					array4 = OracleNumberCore.lnxsub(array, array4);
				}
				if ((array4[0] & 255) < 128)
				{
					array4 = OracleNumberCore.GetZeroByteRep();
				}
				if (OracleNumberCore.lnxcmp(array4, array) > 0)
				{
					array4 = array;
				}
				if (funcid == 1 || funcid == 0)
				{
					array4 = OracleNumberCore.lnxmul(array4, OracleNumberCore.lnxqtwo);
				}
				switch (funcid)
				{
				case 0:
					if (OracleNumberCore.IsPositive(n))
					{
						result = array4;
					}
					else
					{
						result = OracleNumberCore.lnxsub(pi, array4);
					}
					break;
				case 1:
					if (OracleNumberCore.IsPositive(n))
					{
						result = OracleNumberCore.lnxsub(array, array4);
					}
					else
					{
						result = OracleNumberCore.lnxsub(array4, array);
					}
					break;
				case 2:
					if (OracleNumberCore.IsPositive(n))
					{
						result = array4;
					}
					else
					{
						result = OracleNumberCore.lnxneg(array4);
					}
					break;
				default:
					throw new ArgumentException("INVALIDINPUTN");
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

		// Token: 0x060008FF RID: 2303 RVA: 0x00063B4C File Offset: 0x00061D4C
		// Note: this type is marked as 'beforefieldinit'.
		static OracleNumberCore()
		{
			byte[] array = new byte[8];
			array[0] = byte.MaxValue;
			array[1] = 248;
			OracleNumberCore.NANREPD = array;
			OracleNumberCore.LNXDIV_LNXBASE_SQUARED = OracleNumberCore.LNXBASE * OracleNumberCore.LNXBASE;
			OracleNumberCore.MAX_LONG = OracleNumberCore.lnxmin(long.MaxValue);
			OracleNumberCore.MIN_LONG = OracleNumberCore.lnxmin(long.MinValue);
			byte[][] array2 = new byte[202][];
			byte[][] array3 = array2;
			int num = 0;
			byte[] array4 = new byte[2];
			array3[num] = array4;
			array2[1] = new byte[]
			{
				0,
				1
			};
			byte[][] array5 = array2;
			int num2 = 2;
			byte[] array6 = new byte[2];
			array6[0] = 1;
			array5[num2] = array6;
			byte[][] array7 = array2;
			int num3 = 3;
			byte[] array8 = new byte[2];
			array8[0] = 2;
			array7[num3] = array8;
			byte[][] array9 = array2;
			int num4 = 4;
			byte[] array10 = new byte[2];
			array10[0] = 3;
			array9[num4] = array10;
			byte[][] array11 = array2;
			int num5 = 5;
			byte[] array12 = new byte[2];
			array12[0] = 4;
			array11[num5] = array12;
			byte[][] array13 = array2;
			int num6 = 6;
			byte[] array14 = new byte[2];
			array14[0] = 5;
			array13[num6] = array14;
			byte[][] array15 = array2;
			int num7 = 7;
			byte[] array16 = new byte[2];
			array16[0] = 6;
			array15[num7] = array16;
			byte[][] array17 = array2;
			int num8 = 8;
			byte[] array18 = new byte[2];
			array18[0] = 7;
			array17[num8] = array18;
			byte[][] array19 = array2;
			int num9 = 9;
			byte[] array20 = new byte[2];
			array20[0] = 8;
			array19[num9] = array20;
			byte[][] array21 = array2;
			int num10 = 10;
			byte[] array22 = new byte[2];
			array22[0] = 9;
			array21[num10] = array22;
			byte[][] array23 = array2;
			int num11 = 11;
			byte[] array24 = new byte[2];
			array24[0] = 10;
			array23[num11] = array24;
			byte[][] array25 = array2;
			int num12 = 12;
			byte[] array26 = new byte[2];
			array26[0] = 11;
			array25[num12] = array26;
			byte[][] array27 = array2;
			int num13 = 13;
			byte[] array28 = new byte[2];
			array28[0] = 12;
			array27[num13] = array28;
			byte[][] array29 = array2;
			int num14 = 14;
			byte[] array30 = new byte[2];
			array30[0] = 13;
			array29[num14] = array30;
			byte[][] array31 = array2;
			int num15 = 15;
			byte[] array32 = new byte[2];
			array32[0] = 14;
			array31[num15] = array32;
			byte[][] array33 = array2;
			int num16 = 16;
			byte[] array34 = new byte[2];
			array34[0] = 15;
			array33[num16] = array34;
			byte[][] array35 = array2;
			int num17 = 17;
			byte[] array36 = new byte[2];
			array36[0] = 16;
			array35[num17] = array36;
			byte[][] array37 = array2;
			int num18 = 18;
			byte[] array38 = new byte[2];
			array38[0] = 17;
			array37[num18] = array38;
			byte[][] array39 = array2;
			int num19 = 19;
			byte[] array40 = new byte[2];
			array40[0] = 18;
			array39[num19] = array40;
			byte[][] array41 = array2;
			int num20 = 20;
			byte[] array42 = new byte[2];
			array42[0] = 19;
			array41[num20] = array42;
			byte[][] array43 = array2;
			int num21 = 21;
			byte[] array44 = new byte[2];
			array44[0] = 20;
			array43[num21] = array44;
			byte[][] array45 = array2;
			int num22 = 22;
			byte[] array46 = new byte[2];
			array46[0] = 21;
			array45[num22] = array46;
			byte[][] array47 = array2;
			int num23 = 23;
			byte[] array48 = new byte[2];
			array48[0] = 22;
			array47[num23] = array48;
			byte[][] array49 = array2;
			int num24 = 24;
			byte[] array50 = new byte[2];
			array50[0] = 23;
			array49[num24] = array50;
			byte[][] array51 = array2;
			int num25 = 25;
			byte[] array52 = new byte[2];
			array52[0] = 24;
			array51[num25] = array52;
			byte[][] array53 = array2;
			int num26 = 26;
			byte[] array54 = new byte[2];
			array54[0] = 25;
			array53[num26] = array54;
			byte[][] array55 = array2;
			int num27 = 27;
			byte[] array56 = new byte[2];
			array56[0] = 26;
			array55[num27] = array56;
			byte[][] array57 = array2;
			int num28 = 28;
			byte[] array58 = new byte[2];
			array58[0] = 27;
			array57[num28] = array58;
			byte[][] array59 = array2;
			int num29 = 29;
			byte[] array60 = new byte[2];
			array60[0] = 28;
			array59[num29] = array60;
			byte[][] array61 = array2;
			int num30 = 30;
			byte[] array62 = new byte[2];
			array62[0] = 29;
			array61[num30] = array62;
			byte[][] array63 = array2;
			int num31 = 31;
			byte[] array64 = new byte[2];
			array64[0] = 30;
			array63[num31] = array64;
			byte[][] array65 = array2;
			int num32 = 32;
			byte[] array66 = new byte[2];
			array66[0] = 31;
			array65[num32] = array66;
			byte[][] array67 = array2;
			int num33 = 33;
			byte[] array68 = new byte[2];
			array68[0] = 32;
			array67[num33] = array68;
			byte[][] array69 = array2;
			int num34 = 34;
			byte[] array70 = new byte[2];
			array70[0] = 33;
			array69[num34] = array70;
			byte[][] array71 = array2;
			int num35 = 35;
			byte[] array72 = new byte[2];
			array72[0] = 34;
			array71[num35] = array72;
			byte[][] array73 = array2;
			int num36 = 36;
			byte[] array74 = new byte[2];
			array74[0] = 35;
			array73[num36] = array74;
			byte[][] array75 = array2;
			int num37 = 37;
			byte[] array76 = new byte[2];
			array76[0] = 36;
			array75[num37] = array76;
			byte[][] array77 = array2;
			int num38 = 38;
			byte[] array78 = new byte[2];
			array78[0] = 37;
			array77[num38] = array78;
			byte[][] array79 = array2;
			int num39 = 39;
			byte[] array80 = new byte[2];
			array80[0] = 38;
			array79[num39] = array80;
			byte[][] array81 = array2;
			int num40 = 40;
			byte[] array82 = new byte[2];
			array82[0] = 39;
			array81[num40] = array82;
			byte[][] array83 = array2;
			int num41 = 41;
			byte[] array84 = new byte[2];
			array84[0] = 40;
			array83[num41] = array84;
			byte[][] array85 = array2;
			int num42 = 42;
			byte[] array86 = new byte[2];
			array86[0] = 41;
			array85[num42] = array86;
			byte[][] array87 = array2;
			int num43 = 43;
			byte[] array88 = new byte[2];
			array88[0] = 42;
			array87[num43] = array88;
			byte[][] array89 = array2;
			int num44 = 44;
			byte[] array90 = new byte[2];
			array90[0] = 43;
			array89[num44] = array90;
			byte[][] array91 = array2;
			int num45 = 45;
			byte[] array92 = new byte[2];
			array92[0] = 44;
			array91[num45] = array92;
			byte[][] array93 = array2;
			int num46 = 46;
			byte[] array94 = new byte[2];
			array94[0] = 45;
			array93[num46] = array94;
			byte[][] array95 = array2;
			int num47 = 47;
			byte[] array96 = new byte[2];
			array96[0] = 46;
			array95[num47] = array96;
			byte[][] array97 = array2;
			int num48 = 48;
			byte[] array98 = new byte[2];
			array98[0] = 47;
			array97[num48] = array98;
			byte[][] array99 = array2;
			int num49 = 49;
			byte[] array100 = new byte[2];
			array100[0] = 48;
			array99[num49] = array100;
			byte[][] array101 = array2;
			int num50 = 50;
			byte[] array102 = new byte[2];
			array102[0] = 49;
			array101[num50] = array102;
			byte[][] array103 = array2;
			int num51 = 51;
			byte[] array104 = new byte[2];
			array104[0] = 50;
			array103[num51] = array104;
			byte[][] array105 = array2;
			int num52 = 52;
			byte[] array106 = new byte[2];
			array106[0] = 51;
			array105[num52] = array106;
			byte[][] array107 = array2;
			int num53 = 53;
			byte[] array108 = new byte[2];
			array108[0] = 52;
			array107[num53] = array108;
			byte[][] array109 = array2;
			int num54 = 54;
			byte[] array110 = new byte[2];
			array110[0] = 53;
			array109[num54] = array110;
			byte[][] array111 = array2;
			int num55 = 55;
			byte[] array112 = new byte[2];
			array112[0] = 54;
			array111[num55] = array112;
			byte[][] array113 = array2;
			int num56 = 56;
			byte[] array114 = new byte[2];
			array114[0] = 55;
			array113[num56] = array114;
			byte[][] array115 = array2;
			int num57 = 57;
			byte[] array116 = new byte[2];
			array116[0] = 56;
			array115[num57] = array116;
			byte[][] array117 = array2;
			int num58 = 58;
			array116 = new byte[2];
			array116[0] = 57;
			array117[num58] = array116;
			byte[][] array118 = array2;
			int num59 = 59;
			array116 = new byte[2];
			array116[0] = 58;
			array118[num59] = array116;
			byte[][] array119 = array2;
			int num60 = 60;
			array116 = new byte[2];
			array116[0] = 59;
			array119[num60] = array116;
			byte[][] array120 = array2;
			int num61 = 61;
			array116 = new byte[2];
			array116[0] = 60;
			array120[num61] = array116;
			byte[][] array121 = array2;
			int num62 = 62;
			array116 = new byte[2];
			array116[0] = 61;
			array121[num62] = array116;
			byte[][] array122 = array2;
			int num63 = 63;
			array116 = new byte[2];
			array116[0] = 62;
			array122[num63] = array116;
			byte[][] array123 = array2;
			int num64 = 64;
			array116 = new byte[2];
			array116[0] = 63;
			array123[num64] = array116;
			byte[][] array124 = array2;
			int num65 = 65;
			array116 = new byte[2];
			array116[0] = 64;
			array124[num65] = array116;
			byte[][] array125 = array2;
			int num66 = 66;
			array116 = new byte[2];
			array116[0] = 65;
			array125[num66] = array116;
			byte[][] array126 = array2;
			int num67 = 67;
			array116 = new byte[2];
			array116[0] = 66;
			array126[num67] = array116;
			byte[][] array127 = array2;
			int num68 = 68;
			array116 = new byte[2];
			array116[0] = 67;
			array127[num68] = array116;
			byte[][] array128 = array2;
			int num69 = 69;
			array116 = new byte[2];
			array116[0] = 68;
			array128[num69] = array116;
			byte[][] array129 = array2;
			int num70 = 70;
			array116 = new byte[2];
			array116[0] = 69;
			array129[num70] = array116;
			byte[][] array130 = array2;
			int num71 = 71;
			array116 = new byte[2];
			array116[0] = 70;
			array130[num71] = array116;
			byte[][] array131 = array2;
			int num72 = 72;
			array116 = new byte[2];
			array116[0] = 71;
			array131[num72] = array116;
			byte[][] array132 = array2;
			int num73 = 73;
			array116 = new byte[2];
			array116[0] = 72;
			array132[num73] = array116;
			byte[][] array133 = array2;
			int num74 = 74;
			array116 = new byte[2];
			array116[0] = 73;
			array133[num74] = array116;
			byte[][] array134 = array2;
			int num75 = 75;
			array116 = new byte[2];
			array116[0] = 74;
			array134[num75] = array116;
			byte[][] array135 = array2;
			int num76 = 76;
			array116 = new byte[2];
			array116[0] = 75;
			array135[num76] = array116;
			byte[][] array136 = array2;
			int num77 = 77;
			array116 = new byte[2];
			array116[0] = 76;
			array136[num77] = array116;
			byte[][] array137 = array2;
			int num78 = 78;
			array116 = new byte[2];
			array116[0] = 77;
			array137[num78] = array116;
			byte[][] array138 = array2;
			int num79 = 79;
			array116 = new byte[2];
			array116[0] = 78;
			array138[num79] = array116;
			byte[][] array139 = array2;
			int num80 = 80;
			array116 = new byte[2];
			array116[0] = 79;
			array139[num80] = array116;
			byte[][] array140 = array2;
			int num81 = 81;
			array116 = new byte[2];
			array116[0] = 80;
			array140[num81] = array116;
			byte[][] array141 = array2;
			int num82 = 82;
			array116 = new byte[2];
			array116[0] = 81;
			array141[num82] = array116;
			byte[][] array142 = array2;
			int num83 = 83;
			array116 = new byte[2];
			array116[0] = 82;
			array142[num83] = array116;
			byte[][] array143 = array2;
			int num84 = 84;
			array116 = new byte[2];
			array116[0] = 83;
			array143[num84] = array116;
			byte[][] array144 = array2;
			int num85 = 85;
			array116 = new byte[2];
			array116[0] = 84;
			array144[num85] = array116;
			byte[][] array145 = array2;
			int num86 = 86;
			array116 = new byte[2];
			array116[0] = 85;
			array145[num86] = array116;
			byte[][] array146 = array2;
			int num87 = 87;
			array116 = new byte[2];
			array116[0] = 86;
			array146[num87] = array116;
			byte[][] array147 = array2;
			int num88 = 88;
			array116 = new byte[2];
			array116[0] = 87;
			array147[num88] = array116;
			byte[][] array148 = array2;
			int num89 = 89;
			array116 = new byte[2];
			array116[0] = 88;
			array148[num89] = array116;
			byte[][] array149 = array2;
			int num90 = 90;
			array116 = new byte[2];
			array116[0] = 89;
			array149[num90] = array116;
			byte[][] array150 = array2;
			int num91 = 91;
			array116 = new byte[2];
			array116[0] = 90;
			array150[num91] = array116;
			byte[][] array151 = array2;
			int num92 = 92;
			array116 = new byte[2];
			array116[0] = 91;
			array151[num92] = array116;
			byte[][] array152 = array2;
			int num93 = 93;
			array116 = new byte[2];
			array116[0] = 92;
			array152[num93] = array116;
			byte[][] array153 = array2;
			int num94 = 94;
			array116 = new byte[2];
			array116[0] = 93;
			array153[num94] = array116;
			byte[][] array154 = array2;
			int num95 = 95;
			array116 = new byte[2];
			array116[0] = 94;
			array154[num95] = array116;
			byte[][] array155 = array2;
			int num96 = 96;
			array116 = new byte[2];
			array116[0] = 95;
			array155[num96] = array116;
			byte[][] array156 = array2;
			int num97 = 97;
			array116 = new byte[2];
			array116[0] = 96;
			array156[num97] = array116;
			byte[][] array157 = array2;
			int num98 = 98;
			array116 = new byte[2];
			array116[0] = 97;
			array157[num98] = array116;
			byte[][] array158 = array2;
			int num99 = 99;
			array116 = new byte[2];
			array116[0] = 98;
			array158[num99] = array116;
			byte[][] array159 = array2;
			int num100 = 100;
			array116 = new byte[2];
			array116[0] = 99;
			array159[num100] = array116;
			byte[][] array160 = array2;
			int num101 = 101;
			array116 = new byte[2];
			array116[0] = 100;
			array160[num101] = array116;
			array2[102] = new byte[]
			{
				1,
				1
			};
			array2[103] = new byte[]
			{
				2,
				1
			};
			array2[104] = new byte[]
			{
				3,
				1
			};
			array2[105] = new byte[]
			{
				4,
				1
			};
			array2[106] = new byte[]
			{
				5,
				1
			};
			array2[107] = new byte[]
			{
				6,
				1
			};
			array2[108] = new byte[]
			{
				7,
				1
			};
			array2[109] = new byte[]
			{
				8,
				1
			};
			array2[110] = new byte[]
			{
				9,
				1
			};
			array2[111] = new byte[]
			{
				10,
				1
			};
			array2[112] = new byte[]
			{
				11,
				1
			};
			array2[113] = new byte[]
			{
				12,
				1
			};
			array2[114] = new byte[]
			{
				13,
				1
			};
			array2[115] = new byte[]
			{
				14,
				1
			};
			array2[116] = new byte[]
			{
				15,
				1
			};
			array2[117] = new byte[]
			{
				16,
				1
			};
			array2[118] = new byte[]
			{
				17,
				1
			};
			array2[119] = new byte[]
			{
				18,
				1
			};
			array2[120] = new byte[]
			{
				19,
				1
			};
			array2[121] = new byte[]
			{
				20,
				1
			};
			array2[122] = new byte[]
			{
				21,
				1
			};
			array2[123] = new byte[]
			{
				22,
				1
			};
			array2[124] = new byte[]
			{
				23,
				1
			};
			array2[125] = new byte[]
			{
				24,
				1
			};
			array2[126] = new byte[]
			{
				25,
				1
			};
			array2[127] = new byte[]
			{
				26,
				1
			};
			array2[128] = new byte[]
			{
				27,
				1
			};
			array2[129] = new byte[]
			{
				28,
				1
			};
			array2[130] = new byte[]
			{
				29,
				1
			};
			array2[131] = new byte[]
			{
				30,
				1
			};
			array2[132] = new byte[]
			{
				31,
				1
			};
			array2[133] = new byte[]
			{
				32,
				1
			};
			array2[134] = new byte[]
			{
				33,
				1
			};
			array2[135] = new byte[]
			{
				34,
				1
			};
			array2[136] = new byte[]
			{
				35,
				1
			};
			array2[137] = new byte[]
			{
				36,
				1
			};
			array2[138] = new byte[]
			{
				37,
				1
			};
			array2[139] = new byte[]
			{
				38,
				1
			};
			array2[140] = new byte[]
			{
				39,
				1
			};
			array2[141] = new byte[]
			{
				40,
				1
			};
			array2[142] = new byte[]
			{
				41,
				1
			};
			array2[143] = new byte[]
			{
				42,
				1
			};
			array2[144] = new byte[]
			{
				43,
				1
			};
			array2[145] = new byte[]
			{
				44,
				1
			};
			array2[146] = new byte[]
			{
				45,
				1
			};
			array2[147] = new byte[]
			{
				46,
				1
			};
			array2[148] = new byte[]
			{
				47,
				1
			};
			array2[149] = new byte[]
			{
				48,
				1
			};
			array2[150] = new byte[]
			{
				49,
				1
			};
			array2[151] = new byte[]
			{
				50,
				1
			};
			array2[152] = new byte[]
			{
				51,
				1
			};
			array2[153] = new byte[]
			{
				52,
				1
			};
			array2[154] = new byte[]
			{
				53,
				1
			};
			array2[155] = new byte[]
			{
				54,
				1
			};
			array2[156] = new byte[]
			{
				55,
				1
			};
			array2[157] = new byte[]
			{
				56,
				1
			};
			array2[158] = new byte[]
			{
				57,
				1
			};
			array2[159] = new byte[]
			{
				58,
				1
			};
			array2[160] = new byte[]
			{
				59,
				1
			};
			array2[161] = new byte[]
			{
				60,
				1
			};
			array2[162] = new byte[]
			{
				61,
				1
			};
			array2[163] = new byte[]
			{
				62,
				1
			};
			array2[164] = new byte[]
			{
				63,
				1
			};
			array2[165] = new byte[]
			{
				64,
				1
			};
			array2[166] = new byte[]
			{
				65,
				1
			};
			array2[167] = new byte[]
			{
				66,
				1
			};
			array2[168] = new byte[]
			{
				67,
				1
			};
			array2[169] = new byte[]
			{
				68,
				1
			};
			array2[170] = new byte[]
			{
				69,
				1
			};
			array2[171] = new byte[]
			{
				70,
				1
			};
			array2[172] = new byte[]
			{
				71,
				1
			};
			array2[173] = new byte[]
			{
				72,
				1
			};
			array2[174] = new byte[]
			{
				73,
				1
			};
			array2[175] = new byte[]
			{
				74,
				1
			};
			array2[176] = new byte[]
			{
				75,
				1
			};
			array2[177] = new byte[]
			{
				76,
				1
			};
			array2[178] = new byte[]
			{
				77,
				1
			};
			array2[179] = new byte[]
			{
				78,
				1
			};
			array2[180] = new byte[]
			{
				79,
				1
			};
			array2[181] = new byte[]
			{
				80,
				1
			};
			array2[182] = new byte[]
			{
				81,
				1
			};
			array2[183] = new byte[]
			{
				82,
				1
			};
			array2[184] = new byte[]
			{
				83,
				1
			};
			array2[185] = new byte[]
			{
				84,
				1
			};
			array2[186] = new byte[]
			{
				85,
				1
			};
			array2[187] = new byte[]
			{
				86,
				1
			};
			array2[188] = new byte[]
			{
				87,
				1
			};
			array2[189] = new byte[]
			{
				88,
				1
			};
			array2[190] = new byte[]
			{
				89,
				1
			};
			array2[191] = new byte[]
			{
				90,
				1
			};
			array2[192] = new byte[]
			{
				91,
				1
			};
			array2[193] = new byte[]
			{
				92,
				1
			};
			array2[194] = new byte[]
			{
				93,
				1
			};
			array2[195] = new byte[]
			{
				94,
				1
			};
			array2[196] = new byte[]
			{
				95,
				1
			};
			array2[197] = new byte[]
			{
				96,
				1
			};
			array2[198] = new byte[]
			{
				97,
				1
			};
			array2[199] = new byte[]
			{
				98,
				1
			};
			array2[200] = new byte[]
			{
				99,
				1
			};
			array2[201] = new byte[]
			{
				100,
				1
			};
			OracleNumberCore.LnxqAdd_PPP = array2;
			array2 = new byte[205][];
			array2[0] = new byte[]
			{
				0,
				2
			};
			array2[1] = new byte[]
			{
				0,
				1
			};
			byte[][] array161 = array2;
			int num102 = 2;
			array116 = new byte[2];
			array161[num102] = array116;
			byte[][] array162 = array2;
			int num103 = 3;
			array116 = new byte[2];
			array162[num103] = array116;
			byte[][] array163 = array2;
			int num104 = 4;
			array116 = new byte[2];
			array163[num104] = array116;
			array2[5] = new byte[]
			{
				2,
				1
			};
			array2[6] = new byte[]
			{
				3,
				1
			};
			array2[7] = new byte[]
			{
				4,
				1
			};
			array2[8] = new byte[]
			{
				5,
				1
			};
			array2[9] = new byte[]
			{
				6,
				1
			};
			array2[10] = new byte[]
			{
				7,
				1
			};
			array2[11] = new byte[]
			{
				8,
				1
			};
			array2[12] = new byte[]
			{
				9,
				1
			};
			array2[13] = new byte[]
			{
				10,
				1
			};
			array2[14] = new byte[]
			{
				11,
				1
			};
			array2[15] = new byte[]
			{
				12,
				1
			};
			array2[16] = new byte[]
			{
				13,
				1
			};
			array2[17] = new byte[]
			{
				14,
				1
			};
			array2[18] = new byte[]
			{
				15,
				1
			};
			array2[19] = new byte[]
			{
				16,
				1
			};
			array2[20] = new byte[]
			{
				17,
				1
			};
			array2[21] = new byte[]
			{
				18,
				1
			};
			array2[22] = new byte[]
			{
				19,
				1
			};
			array2[23] = new byte[]
			{
				20,
				1
			};
			array2[24] = new byte[]
			{
				21,
				1
			};
			array2[25] = new byte[]
			{
				22,
				1
			};
			array2[26] = new byte[]
			{
				23,
				1
			};
			array2[27] = new byte[]
			{
				24,
				1
			};
			array2[28] = new byte[]
			{
				25,
				1
			};
			array2[29] = new byte[]
			{
				26,
				1
			};
			array2[30] = new byte[]
			{
				27,
				1
			};
			array2[31] = new byte[]
			{
				28,
				1
			};
			array2[32] = new byte[]
			{
				29,
				1
			};
			array2[33] = new byte[]
			{
				30,
				1
			};
			array2[34] = new byte[]
			{
				31,
				1
			};
			array2[35] = new byte[]
			{
				32,
				1
			};
			array2[36] = new byte[]
			{
				33,
				1
			};
			array2[37] = new byte[]
			{
				34,
				1
			};
			array2[38] = new byte[]
			{
				35,
				1
			};
			array2[39] = new byte[]
			{
				36,
				1
			};
			array2[40] = new byte[]
			{
				37,
				1
			};
			array2[41] = new byte[]
			{
				38,
				1
			};
			array2[42] = new byte[]
			{
				39,
				1
			};
			array2[43] = new byte[]
			{
				40,
				1
			};
			array2[44] = new byte[]
			{
				41,
				1
			};
			array2[45] = new byte[]
			{
				42,
				1
			};
			array2[46] = new byte[]
			{
				43,
				1
			};
			array2[47] = new byte[]
			{
				44,
				1
			};
			array2[48] = new byte[]
			{
				45,
				1
			};
			array2[49] = new byte[]
			{
				46,
				1
			};
			array2[50] = new byte[]
			{
				47,
				1
			};
			array2[51] = new byte[]
			{
				48,
				1
			};
			array2[52] = new byte[]
			{
				49,
				1
			};
			array2[53] = new byte[]
			{
				50,
				1
			};
			array2[54] = new byte[]
			{
				51,
				1
			};
			array2[55] = new byte[]
			{
				52,
				1
			};
			array2[56] = new byte[]
			{
				53,
				1
			};
			array2[57] = new byte[]
			{
				54,
				1
			};
			array2[58] = new byte[]
			{
				55,
				1
			};
			array2[59] = new byte[]
			{
				56,
				1
			};
			array2[60] = new byte[]
			{
				57,
				1
			};
			array2[61] = new byte[]
			{
				58,
				1
			};
			array2[62] = new byte[]
			{
				59,
				1
			};
			array2[63] = new byte[]
			{
				60,
				1
			};
			array2[64] = new byte[]
			{
				61,
				1
			};
			array2[65] = new byte[]
			{
				62,
				1
			};
			array2[66] = new byte[]
			{
				63,
				1
			};
			array2[67] = new byte[]
			{
				64,
				1
			};
			array2[68] = new byte[]
			{
				65,
				1
			};
			array2[69] = new byte[]
			{
				66,
				1
			};
			array2[70] = new byte[]
			{
				67,
				1
			};
			array2[71] = new byte[]
			{
				68,
				1
			};
			array2[72] = new byte[]
			{
				69,
				1
			};
			array2[73] = new byte[]
			{
				70,
				1
			};
			array2[74] = new byte[]
			{
				71,
				1
			};
			array2[75] = new byte[]
			{
				72,
				1
			};
			array2[76] = new byte[]
			{
				73,
				1
			};
			array2[77] = new byte[]
			{
				74,
				1
			};
			array2[78] = new byte[]
			{
				75,
				1
			};
			array2[79] = new byte[]
			{
				76,
				1
			};
			array2[80] = new byte[]
			{
				77,
				1
			};
			array2[81] = new byte[]
			{
				78,
				1
			};
			array2[82] = new byte[]
			{
				79,
				1
			};
			array2[83] = new byte[]
			{
				80,
				1
			};
			array2[84] = new byte[]
			{
				81,
				1
			};
			array2[85] = new byte[]
			{
				82,
				1
			};
			array2[86] = new byte[]
			{
				83,
				1
			};
			array2[87] = new byte[]
			{
				84,
				1
			};
			array2[88] = new byte[]
			{
				85,
				1
			};
			array2[89] = new byte[]
			{
				86,
				1
			};
			array2[90] = new byte[]
			{
				87,
				1
			};
			array2[91] = new byte[]
			{
				88,
				1
			};
			array2[92] = new byte[]
			{
				89,
				1
			};
			array2[93] = new byte[]
			{
				90,
				1
			};
			array2[94] = new byte[]
			{
				91,
				1
			};
			array2[95] = new byte[]
			{
				92,
				1
			};
			array2[96] = new byte[]
			{
				93,
				1
			};
			array2[97] = new byte[]
			{
				94,
				1
			};
			array2[98] = new byte[]
			{
				95,
				1
			};
			array2[99] = new byte[]
			{
				96,
				1
			};
			array2[100] = new byte[]
			{
				97,
				1
			};
			array2[101] = new byte[]
			{
				98,
				1
			};
			array2[102] = new byte[]
			{
				99,
				1
			};
			array2[103] = new byte[]
			{
				100,
				1
			};
			array2[104] = new byte[]
			{
				101,
				1
			};
			array2[105] = new byte[]
			{
				2,
				2
			};
			array2[106] = new byte[]
			{
				3,
				2
			};
			array2[107] = new byte[]
			{
				4,
				2
			};
			array2[108] = new byte[]
			{
				5,
				2
			};
			array2[109] = new byte[]
			{
				6,
				2
			};
			array2[110] = new byte[]
			{
				7,
				2
			};
			array2[111] = new byte[]
			{
				8,
				2
			};
			array2[112] = new byte[]
			{
				9,
				2
			};
			array2[113] = new byte[]
			{
				10,
				2
			};
			array2[114] = new byte[]
			{
				11,
				2
			};
			array2[115] = new byte[]
			{
				12,
				2
			};
			array2[116] = new byte[]
			{
				13,
				2
			};
			array2[117] = new byte[]
			{
				14,
				2
			};
			array2[118] = new byte[]
			{
				15,
				2
			};
			array2[119] = new byte[]
			{
				16,
				2
			};
			array2[120] = new byte[]
			{
				17,
				2
			};
			array2[121] = new byte[]
			{
				18,
				2
			};
			array2[122] = new byte[]
			{
				19,
				2
			};
			array2[123] = new byte[]
			{
				20,
				2
			};
			array2[124] = new byte[]
			{
				21,
				2
			};
			array2[125] = new byte[]
			{
				22,
				2
			};
			array2[126] = new byte[]
			{
				23,
				2
			};
			array2[127] = new byte[]
			{
				24,
				2
			};
			array2[128] = new byte[]
			{
				25,
				2
			};
			array2[129] = new byte[]
			{
				26,
				2
			};
			array2[130] = new byte[]
			{
				27,
				2
			};
			array2[131] = new byte[]
			{
				28,
				2
			};
			array2[132] = new byte[]
			{
				29,
				2
			};
			array2[133] = new byte[]
			{
				30,
				2
			};
			array2[134] = new byte[]
			{
				31,
				2
			};
			array2[135] = new byte[]
			{
				32,
				2
			};
			array2[136] = new byte[]
			{
				33,
				2
			};
			array2[137] = new byte[]
			{
				34,
				2
			};
			array2[138] = new byte[]
			{
				35,
				2
			};
			array2[139] = new byte[]
			{
				36,
				2
			};
			array2[140] = new byte[]
			{
				37,
				2
			};
			array2[141] = new byte[]
			{
				38,
				2
			};
			array2[142] = new byte[]
			{
				39,
				2
			};
			array2[143] = new byte[]
			{
				40,
				2
			};
			array2[144] = new byte[]
			{
				41,
				2
			};
			array2[145] = new byte[]
			{
				42,
				2
			};
			array2[146] = new byte[]
			{
				43,
				2
			};
			array2[147] = new byte[]
			{
				44,
				2
			};
			array2[148] = new byte[]
			{
				45,
				2
			};
			array2[149] = new byte[]
			{
				46,
				2
			};
			array2[150] = new byte[]
			{
				47,
				2
			};
			array2[151] = new byte[]
			{
				48,
				2
			};
			array2[152] = new byte[]
			{
				49,
				2
			};
			array2[153] = new byte[]
			{
				50,
				2
			};
			array2[154] = new byte[]
			{
				51,
				2
			};
			array2[155] = new byte[]
			{
				52,
				2
			};
			array2[156] = new byte[]
			{
				53,
				2
			};
			array2[157] = new byte[]
			{
				54,
				2
			};
			array2[158] = new byte[]
			{
				55,
				2
			};
			array2[159] = new byte[]
			{
				56,
				2
			};
			array2[160] = new byte[]
			{
				57,
				2
			};
			array2[161] = new byte[]
			{
				58,
				2
			};
			array2[162] = new byte[]
			{
				59,
				2
			};
			array2[163] = new byte[]
			{
				60,
				2
			};
			array2[164] = new byte[]
			{
				61,
				2
			};
			array2[165] = new byte[]
			{
				62,
				2
			};
			array2[166] = new byte[]
			{
				63,
				2
			};
			array2[167] = new byte[]
			{
				64,
				2
			};
			array2[168] = new byte[]
			{
				65,
				2
			};
			array2[169] = new byte[]
			{
				66,
				2
			};
			array2[170] = new byte[]
			{
				67,
				2
			};
			array2[171] = new byte[]
			{
				68,
				2
			};
			array2[172] = new byte[]
			{
				69,
				2
			};
			array2[173] = new byte[]
			{
				70,
				2
			};
			array2[174] = new byte[]
			{
				71,
				2
			};
			array2[175] = new byte[]
			{
				72,
				2
			};
			array2[176] = new byte[]
			{
				73,
				2
			};
			array2[177] = new byte[]
			{
				74,
				2
			};
			array2[178] = new byte[]
			{
				75,
				2
			};
			array2[179] = new byte[]
			{
				76,
				2
			};
			array2[180] = new byte[]
			{
				77,
				2
			};
			array2[181] = new byte[]
			{
				78,
				2
			};
			array2[182] = new byte[]
			{
				79,
				2
			};
			array2[183] = new byte[]
			{
				80,
				2
			};
			array2[184] = new byte[]
			{
				81,
				2
			};
			array2[185] = new byte[]
			{
				82,
				2
			};
			array2[186] = new byte[]
			{
				83,
				2
			};
			array2[187] = new byte[]
			{
				84,
				2
			};
			array2[188] = new byte[]
			{
				85,
				2
			};
			array2[189] = new byte[]
			{
				86,
				2
			};
			array2[190] = new byte[]
			{
				87,
				2
			};
			array2[191] = new byte[]
			{
				88,
				2
			};
			array2[192] = new byte[]
			{
				89,
				2
			};
			array2[193] = new byte[]
			{
				90,
				2
			};
			array2[194] = new byte[]
			{
				91,
				2
			};
			array2[195] = new byte[]
			{
				92,
				2
			};
			array2[196] = new byte[]
			{
				93,
				2
			};
			array2[197] = new byte[]
			{
				94,
				2
			};
			array2[198] = new byte[]
			{
				95,
				2
			};
			array2[199] = new byte[]
			{
				96,
				2
			};
			array2[200] = new byte[]
			{
				97,
				2
			};
			array2[201] = new byte[]
			{
				98,
				2
			};
			array2[202] = new byte[]
			{
				99,
				2
			};
			array2[203] = new byte[]
			{
				100,
				2
			};
			array2[204] = new byte[]
			{
				101,
				2
			};
			OracleNumberCore.LnxqAdd_NNN = array2;
			array2 = new byte[204][];
			array2[0] = new byte[]
			{
				0,
				2
			};
			array2[1] = new byte[]
			{
				0,
				1
			};
			byte[][] array164 = array2;
			int num105 = 2;
			array116 = new byte[2];
			array164[num105] = array116;
			byte[][] array165 = array2;
			int num106 = 3;
			array116 = new byte[2];
			array165[num106] = array116;
			array2[4] = new byte[]
			{
				1,
				1
			};
			array2[5] = new byte[]
			{
				2,
				1
			};
			array2[6] = new byte[]
			{
				3,
				1
			};
			array2[7] = new byte[]
			{
				4,
				1
			};
			array2[8] = new byte[]
			{
				5,
				1
			};
			array2[9] = new byte[]
			{
				6,
				1
			};
			array2[10] = new byte[]
			{
				7,
				1
			};
			array2[11] = new byte[]
			{
				8,
				1
			};
			array2[12] = new byte[]
			{
				9,
				1
			};
			array2[13] = new byte[]
			{
				10,
				1
			};
			array2[14] = new byte[]
			{
				11,
				1
			};
			array2[15] = new byte[]
			{
				12,
				1
			};
			array2[16] = new byte[]
			{
				13,
				1
			};
			array2[17] = new byte[]
			{
				14,
				1
			};
			array2[18] = new byte[]
			{
				15,
				1
			};
			array2[19] = new byte[]
			{
				16,
				1
			};
			array2[20] = new byte[]
			{
				17,
				1
			};
			array2[21] = new byte[]
			{
				18,
				1
			};
			array2[22] = new byte[]
			{
				19,
				1
			};
			array2[23] = new byte[]
			{
				20,
				1
			};
			array2[24] = new byte[]
			{
				21,
				1
			};
			array2[25] = new byte[]
			{
				22,
				1
			};
			array2[26] = new byte[]
			{
				23,
				1
			};
			array2[27] = new byte[]
			{
				24,
				1
			};
			array2[28] = new byte[]
			{
				25,
				1
			};
			array2[29] = new byte[]
			{
				26,
				1
			};
			array2[30] = new byte[]
			{
				27,
				1
			};
			array2[31] = new byte[]
			{
				28,
				1
			};
			array2[32] = new byte[]
			{
				29,
				1
			};
			array2[33] = new byte[]
			{
				30,
				1
			};
			array2[34] = new byte[]
			{
				31,
				1
			};
			array2[35] = new byte[]
			{
				32,
				1
			};
			array2[36] = new byte[]
			{
				33,
				1
			};
			array2[37] = new byte[]
			{
				34,
				1
			};
			array2[38] = new byte[]
			{
				35,
				1
			};
			array2[39] = new byte[]
			{
				36,
				1
			};
			array2[40] = new byte[]
			{
				37,
				1
			};
			array2[41] = new byte[]
			{
				38,
				1
			};
			array2[42] = new byte[]
			{
				39,
				1
			};
			array2[43] = new byte[]
			{
				40,
				1
			};
			array2[44] = new byte[]
			{
				41,
				1
			};
			array2[45] = new byte[]
			{
				42,
				1
			};
			array2[46] = new byte[]
			{
				43,
				1
			};
			array2[47] = new byte[]
			{
				44,
				1
			};
			array2[48] = new byte[]
			{
				45,
				1
			};
			array2[49] = new byte[]
			{
				46,
				1
			};
			array2[50] = new byte[]
			{
				47,
				1
			};
			array2[51] = new byte[]
			{
				48,
				1
			};
			array2[52] = new byte[]
			{
				49,
				1
			};
			array2[53] = new byte[]
			{
				50,
				1
			};
			array2[54] = new byte[]
			{
				51,
				1
			};
			array2[55] = new byte[]
			{
				52,
				1
			};
			array2[56] = new byte[]
			{
				53,
				1
			};
			array2[57] = new byte[]
			{
				54,
				1
			};
			array2[58] = new byte[]
			{
				55,
				1
			};
			array2[59] = new byte[]
			{
				56,
				1
			};
			array2[60] = new byte[]
			{
				57,
				1
			};
			array2[61] = new byte[]
			{
				58,
				1
			};
			array2[62] = new byte[]
			{
				59,
				1
			};
			array2[63] = new byte[]
			{
				60,
				1
			};
			array2[64] = new byte[]
			{
				61,
				1
			};
			array2[65] = new byte[]
			{
				62,
				1
			};
			array2[66] = new byte[]
			{
				63,
				1
			};
			array2[67] = new byte[]
			{
				64,
				1
			};
			array2[68] = new byte[]
			{
				65,
				1
			};
			array2[69] = new byte[]
			{
				66,
				1
			};
			array2[70] = new byte[]
			{
				67,
				1
			};
			array2[71] = new byte[]
			{
				68,
				1
			};
			array2[72] = new byte[]
			{
				69,
				1
			};
			array2[73] = new byte[]
			{
				70,
				1
			};
			array2[74] = new byte[]
			{
				71,
				1
			};
			array2[75] = new byte[]
			{
				72,
				1
			};
			array2[76] = new byte[]
			{
				73,
				1
			};
			array2[77] = new byte[]
			{
				74,
				1
			};
			array2[78] = new byte[]
			{
				75,
				1
			};
			array2[79] = new byte[]
			{
				76,
				1
			};
			array2[80] = new byte[]
			{
				77,
				1
			};
			array2[81] = new byte[]
			{
				78,
				1
			};
			array2[82] = new byte[]
			{
				79,
				1
			};
			array2[83] = new byte[]
			{
				80,
				1
			};
			array2[84] = new byte[]
			{
				81,
				1
			};
			array2[85] = new byte[]
			{
				82,
				1
			};
			array2[86] = new byte[]
			{
				83,
				1
			};
			array2[87] = new byte[]
			{
				84,
				1
			};
			array2[88] = new byte[]
			{
				85,
				1
			};
			array2[89] = new byte[]
			{
				86,
				1
			};
			array2[90] = new byte[]
			{
				87,
				1
			};
			array2[91] = new byte[]
			{
				88,
				1
			};
			array2[92] = new byte[]
			{
				89,
				1
			};
			array2[93] = new byte[]
			{
				90,
				1
			};
			array2[94] = new byte[]
			{
				91,
				1
			};
			array2[95] = new byte[]
			{
				92,
				1
			};
			array2[96] = new byte[]
			{
				93,
				1
			};
			array2[97] = new byte[]
			{
				94,
				1
			};
			array2[98] = new byte[]
			{
				95,
				1
			};
			array2[99] = new byte[]
			{
				96,
				1
			};
			array2[100] = new byte[]
			{
				97,
				1
			};
			array2[101] = new byte[]
			{
				98,
				1
			};
			array2[102] = new byte[]
			{
				99,
				1
			};
			array2[103] = new byte[]
			{
				100,
				1
			};
			array2[104] = new byte[]
			{
				1,
				2
			};
			array2[105] = new byte[]
			{
				2,
				2
			};
			array2[106] = new byte[]
			{
				3,
				2
			};
			array2[107] = new byte[]
			{
				4,
				2
			};
			array2[108] = new byte[]
			{
				5,
				2
			};
			array2[109] = new byte[]
			{
				6,
				2
			};
			array2[110] = new byte[]
			{
				7,
				2
			};
			array2[111] = new byte[]
			{
				8,
				2
			};
			array2[112] = new byte[]
			{
				9,
				2
			};
			array2[113] = new byte[]
			{
				10,
				2
			};
			array2[114] = new byte[]
			{
				11,
				2
			};
			array2[115] = new byte[]
			{
				12,
				2
			};
			array2[116] = new byte[]
			{
				13,
				2
			};
			array2[117] = new byte[]
			{
				14,
				2
			};
			array2[118] = new byte[]
			{
				15,
				2
			};
			array2[119] = new byte[]
			{
				16,
				2
			};
			array2[120] = new byte[]
			{
				17,
				2
			};
			array2[121] = new byte[]
			{
				18,
				2
			};
			array2[122] = new byte[]
			{
				19,
				2
			};
			array2[123] = new byte[]
			{
				20,
				2
			};
			array2[124] = new byte[]
			{
				21,
				2
			};
			array2[125] = new byte[]
			{
				22,
				2
			};
			array2[126] = new byte[]
			{
				23,
				2
			};
			array2[127] = new byte[]
			{
				24,
				2
			};
			array2[128] = new byte[]
			{
				25,
				2
			};
			array2[129] = new byte[]
			{
				26,
				2
			};
			array2[130] = new byte[]
			{
				27,
				2
			};
			array2[131] = new byte[]
			{
				28,
				2
			};
			array2[132] = new byte[]
			{
				29,
				2
			};
			array2[133] = new byte[]
			{
				30,
				2
			};
			array2[134] = new byte[]
			{
				31,
				2
			};
			array2[135] = new byte[]
			{
				32,
				2
			};
			array2[136] = new byte[]
			{
				33,
				2
			};
			array2[137] = new byte[]
			{
				34,
				2
			};
			array2[138] = new byte[]
			{
				35,
				2
			};
			array2[139] = new byte[]
			{
				36,
				2
			};
			array2[140] = new byte[]
			{
				37,
				2
			};
			array2[141] = new byte[]
			{
				38,
				2
			};
			array2[142] = new byte[]
			{
				39,
				2
			};
			array2[143] = new byte[]
			{
				40,
				2
			};
			array2[144] = new byte[]
			{
				41,
				2
			};
			array2[145] = new byte[]
			{
				42,
				2
			};
			array2[146] = new byte[]
			{
				43,
				2
			};
			array2[147] = new byte[]
			{
				44,
				2
			};
			array2[148] = new byte[]
			{
				45,
				2
			};
			array2[149] = new byte[]
			{
				46,
				2
			};
			array2[150] = new byte[]
			{
				47,
				2
			};
			array2[151] = new byte[]
			{
				48,
				2
			};
			array2[152] = new byte[]
			{
				49,
				2
			};
			array2[153] = new byte[]
			{
				50,
				2
			};
			array2[154] = new byte[]
			{
				51,
				2
			};
			array2[155] = new byte[]
			{
				52,
				2
			};
			array2[156] = new byte[]
			{
				53,
				2
			};
			array2[157] = new byte[]
			{
				54,
				2
			};
			array2[158] = new byte[]
			{
				55,
				2
			};
			array2[159] = new byte[]
			{
				56,
				2
			};
			array2[160] = new byte[]
			{
				57,
				2
			};
			array2[161] = new byte[]
			{
				58,
				2
			};
			array2[162] = new byte[]
			{
				59,
				2
			};
			array2[163] = new byte[]
			{
				60,
				2
			};
			array2[164] = new byte[]
			{
				61,
				2
			};
			array2[165] = new byte[]
			{
				62,
				2
			};
			array2[166] = new byte[]
			{
				63,
				2
			};
			array2[167] = new byte[]
			{
				64,
				2
			};
			array2[168] = new byte[]
			{
				65,
				2
			};
			array2[169] = new byte[]
			{
				66,
				2
			};
			array2[170] = new byte[]
			{
				67,
				2
			};
			array2[171] = new byte[]
			{
				68,
				2
			};
			array2[172] = new byte[]
			{
				69,
				2
			};
			array2[173] = new byte[]
			{
				70,
				2
			};
			array2[174] = new byte[]
			{
				71,
				2
			};
			array2[175] = new byte[]
			{
				72,
				2
			};
			array2[176] = new byte[]
			{
				73,
				2
			};
			array2[177] = new byte[]
			{
				74,
				2
			};
			array2[178] = new byte[]
			{
				75,
				2
			};
			array2[179] = new byte[]
			{
				76,
				2
			};
			array2[180] = new byte[]
			{
				77,
				2
			};
			array2[181] = new byte[]
			{
				78,
				2
			};
			array2[182] = new byte[]
			{
				79,
				2
			};
			array2[183] = new byte[]
			{
				80,
				2
			};
			array2[184] = new byte[]
			{
				81,
				2
			};
			array2[185] = new byte[]
			{
				82,
				2
			};
			array2[186] = new byte[]
			{
				83,
				2
			};
			array2[187] = new byte[]
			{
				84,
				2
			};
			array2[188] = new byte[]
			{
				85,
				2
			};
			array2[189] = new byte[]
			{
				86,
				2
			};
			array2[190] = new byte[]
			{
				87,
				2
			};
			array2[191] = new byte[]
			{
				88,
				2
			};
			array2[192] = new byte[]
			{
				89,
				2
			};
			array2[193] = new byte[]
			{
				90,
				2
			};
			array2[194] = new byte[]
			{
				91,
				2
			};
			array2[195] = new byte[]
			{
				92,
				2
			};
			array2[196] = new byte[]
			{
				93,
				2
			};
			array2[197] = new byte[]
			{
				94,
				2
			};
			array2[198] = new byte[]
			{
				95,
				2
			};
			array2[199] = new byte[]
			{
				96,
				2
			};
			array2[200] = new byte[]
			{
				97,
				2
			};
			array2[201] = new byte[]
			{
				98,
				2
			};
			array2[202] = new byte[]
			{
				99,
				2
			};
			array2[203] = new byte[]
			{
				100,
				2
			};
			OracleNumberCore.LnxqAdd_PNP = array2;
			array2 = new byte[203][];
			byte[][] array166 = array2;
			int num107 = 0;
			array116 = new byte[2];
			array166[num107] = array116;
			array2[1] = new byte[]
			{
				0,
				1
			};
			byte[][] array167 = array2;
			int num108 = 2;
			array116 = new byte[2];
			array167[num108] = array116;
			byte[][] array168 = array2;
			int num109 = 3;
			array116 = new byte[2];
			array116[0] = 2;
			array168[num109] = array116;
			byte[][] array169 = array2;
			int num110 = 4;
			array116 = new byte[2];
			array116[0] = 3;
			array169[num110] = array116;
			byte[][] array170 = array2;
			int num111 = 5;
			array116 = new byte[2];
			array116[0] = 4;
			array170[num111] = array116;
			byte[][] array171 = array2;
			int num112 = 6;
			array116 = new byte[2];
			array116[0] = 5;
			array171[num112] = array116;
			byte[][] array172 = array2;
			int num113 = 7;
			array116 = new byte[2];
			array116[0] = 6;
			array172[num113] = array116;
			byte[][] array173 = array2;
			int num114 = 8;
			array116 = new byte[2];
			array116[0] = 7;
			array173[num114] = array116;
			byte[][] array174 = array2;
			int num115 = 9;
			array116 = new byte[2];
			array116[0] = 8;
			array174[num115] = array116;
			byte[][] array175 = array2;
			int num116 = 10;
			array116 = new byte[2];
			array116[0] = 9;
			array175[num116] = array116;
			byte[][] array176 = array2;
			int num117 = 11;
			array116 = new byte[2];
			array116[0] = 10;
			array176[num117] = array116;
			byte[][] array177 = array2;
			int num118 = 12;
			array116 = new byte[2];
			array116[0] = 11;
			array177[num118] = array116;
			byte[][] array178 = array2;
			int num119 = 13;
			array116 = new byte[2];
			array116[0] = 12;
			array178[num119] = array116;
			byte[][] array179 = array2;
			int num120 = 14;
			array116 = new byte[2];
			array116[0] = 13;
			array179[num120] = array116;
			byte[][] array180 = array2;
			int num121 = 15;
			array116 = new byte[2];
			array116[0] = 14;
			array180[num121] = array116;
			byte[][] array181 = array2;
			int num122 = 16;
			array116 = new byte[2];
			array116[0] = 15;
			array181[num122] = array116;
			byte[][] array182 = array2;
			int num123 = 17;
			array116 = new byte[2];
			array116[0] = 16;
			array182[num123] = array116;
			byte[][] array183 = array2;
			int num124 = 18;
			array116 = new byte[2];
			array116[0] = 17;
			array183[num124] = array116;
			byte[][] array184 = array2;
			int num125 = 19;
			array116 = new byte[2];
			array116[0] = 18;
			array184[num125] = array116;
			byte[][] array185 = array2;
			int num126 = 20;
			array116 = new byte[2];
			array116[0] = 19;
			array185[num126] = array116;
			byte[][] array186 = array2;
			int num127 = 21;
			array116 = new byte[2];
			array116[0] = 20;
			array186[num127] = array116;
			byte[][] array187 = array2;
			int num128 = 22;
			array116 = new byte[2];
			array116[0] = 21;
			array187[num128] = array116;
			byte[][] array188 = array2;
			int num129 = 23;
			array116 = new byte[2];
			array116[0] = 22;
			array188[num129] = array116;
			byte[][] array189 = array2;
			int num130 = 24;
			array116 = new byte[2];
			array116[0] = 23;
			array189[num130] = array116;
			byte[][] array190 = array2;
			int num131 = 25;
			array116 = new byte[2];
			array116[0] = 24;
			array190[num131] = array116;
			byte[][] array191 = array2;
			int num132 = 26;
			array116 = new byte[2];
			array116[0] = 25;
			array191[num132] = array116;
			byte[][] array192 = array2;
			int num133 = 27;
			array116 = new byte[2];
			array116[0] = 26;
			array192[num133] = array116;
			byte[][] array193 = array2;
			int num134 = 28;
			array116 = new byte[2];
			array116[0] = 27;
			array193[num134] = array116;
			byte[][] array194 = array2;
			int num135 = 29;
			array116 = new byte[2];
			array116[0] = 28;
			array194[num135] = array116;
			byte[][] array195 = array2;
			int num136 = 30;
			array116 = new byte[2];
			array116[0] = 29;
			array195[num136] = array116;
			byte[][] array196 = array2;
			int num137 = 31;
			array116 = new byte[2];
			array116[0] = 30;
			array196[num137] = array116;
			byte[][] array197 = array2;
			int num138 = 32;
			array116 = new byte[2];
			array116[0] = 31;
			array197[num138] = array116;
			byte[][] array198 = array2;
			int num139 = 33;
			array116 = new byte[2];
			array116[0] = 32;
			array198[num139] = array116;
			byte[][] array199 = array2;
			int num140 = 34;
			array116 = new byte[2];
			array116[0] = 33;
			array199[num140] = array116;
			byte[][] array200 = array2;
			int num141 = 35;
			array116 = new byte[2];
			array116[0] = 34;
			array200[num141] = array116;
			byte[][] array201 = array2;
			int num142 = 36;
			array116 = new byte[2];
			array116[0] = 35;
			array201[num142] = array116;
			byte[][] array202 = array2;
			int num143 = 37;
			array116 = new byte[2];
			array116[0] = 36;
			array202[num143] = array116;
			byte[][] array203 = array2;
			int num144 = 38;
			array116 = new byte[2];
			array116[0] = 37;
			array203[num144] = array116;
			byte[][] array204 = array2;
			int num145 = 39;
			array116 = new byte[2];
			array116[0] = 38;
			array204[num145] = array116;
			byte[][] array205 = array2;
			int num146 = 40;
			array116 = new byte[2];
			array116[0] = 39;
			array205[num146] = array116;
			byte[][] array206 = array2;
			int num147 = 41;
			array116 = new byte[2];
			array116[0] = 40;
			array206[num147] = array116;
			byte[][] array207 = array2;
			int num148 = 42;
			array116 = new byte[2];
			array116[0] = 41;
			array207[num148] = array116;
			byte[][] array208 = array2;
			int num149 = 43;
			array116 = new byte[2];
			array116[0] = 42;
			array208[num149] = array116;
			byte[][] array209 = array2;
			int num150 = 44;
			array116 = new byte[2];
			array116[0] = 43;
			array209[num150] = array116;
			byte[][] array210 = array2;
			int num151 = 45;
			array116 = new byte[2];
			array116[0] = 44;
			array210[num151] = array116;
			byte[][] array211 = array2;
			int num152 = 46;
			array116 = new byte[2];
			array116[0] = 45;
			array211[num152] = array116;
			byte[][] array212 = array2;
			int num153 = 47;
			array116 = new byte[2];
			array116[0] = 46;
			array212[num153] = array116;
			byte[][] array213 = array2;
			int num154 = 48;
			array116 = new byte[2];
			array116[0] = 47;
			array213[num154] = array116;
			byte[][] array214 = array2;
			int num155 = 49;
			array116 = new byte[2];
			array116[0] = 48;
			array214[num155] = array116;
			byte[][] array215 = array2;
			int num156 = 50;
			array116 = new byte[2];
			array116[0] = 49;
			array215[num156] = array116;
			byte[][] array216 = array2;
			int num157 = 51;
			array116 = new byte[2];
			array116[0] = 50;
			array216[num157] = array116;
			byte[][] array217 = array2;
			int num158 = 52;
			array116 = new byte[2];
			array116[0] = 51;
			array217[num158] = array116;
			byte[][] array218 = array2;
			int num159 = 53;
			array116 = new byte[2];
			array116[0] = 52;
			array218[num159] = array116;
			byte[][] array219 = array2;
			int num160 = 54;
			array116 = new byte[2];
			array116[0] = 53;
			array219[num160] = array116;
			byte[][] array220 = array2;
			int num161 = 55;
			array116 = new byte[2];
			array116[0] = 54;
			array220[num161] = array116;
			byte[][] array221 = array2;
			int num162 = 56;
			array116 = new byte[2];
			array116[0] = 55;
			array221[num162] = array116;
			byte[][] array222 = array2;
			int num163 = 57;
			array116 = new byte[2];
			array116[0] = 56;
			array222[num163] = array116;
			byte[][] array223 = array2;
			int num164 = 58;
			array116 = new byte[2];
			array116[0] = 57;
			array223[num164] = array116;
			byte[][] array224 = array2;
			int num165 = 59;
			array116 = new byte[2];
			array116[0] = 58;
			array224[num165] = array116;
			byte[][] array225 = array2;
			int num166 = 60;
			array116 = new byte[2];
			array116[0] = 59;
			array225[num166] = array116;
			byte[][] array226 = array2;
			int num167 = 61;
			array116 = new byte[2];
			array116[0] = 60;
			array226[num167] = array116;
			byte[][] array227 = array2;
			int num168 = 62;
			array116 = new byte[2];
			array116[0] = 61;
			array227[num168] = array116;
			byte[][] array228 = array2;
			int num169 = 63;
			array116 = new byte[2];
			array116[0] = 62;
			array228[num169] = array116;
			byte[][] array229 = array2;
			int num170 = 64;
			array116 = new byte[2];
			array116[0] = 63;
			array229[num170] = array116;
			byte[][] array230 = array2;
			int num171 = 65;
			array116 = new byte[2];
			array116[0] = 64;
			array230[num171] = array116;
			byte[][] array231 = array2;
			int num172 = 66;
			array116 = new byte[2];
			array116[0] = 65;
			array231[num172] = array116;
			byte[][] array232 = array2;
			int num173 = 67;
			array116 = new byte[2];
			array116[0] = 66;
			array232[num173] = array116;
			byte[][] array233 = array2;
			int num174 = 68;
			array116 = new byte[2];
			array116[0] = 67;
			array233[num174] = array116;
			byte[][] array234 = array2;
			int num175 = 69;
			array116 = new byte[2];
			array116[0] = 68;
			array234[num175] = array116;
			byte[][] array235 = array2;
			int num176 = 70;
			array116 = new byte[2];
			array116[0] = 69;
			array235[num176] = array116;
			byte[][] array236 = array2;
			int num177 = 71;
			array116 = new byte[2];
			array116[0] = 70;
			array236[num177] = array116;
			byte[][] array237 = array2;
			int num178 = 72;
			array116 = new byte[2];
			array116[0] = 71;
			array237[num178] = array116;
			byte[][] array238 = array2;
			int num179 = 73;
			array116 = new byte[2];
			array116[0] = 72;
			array238[num179] = array116;
			byte[][] array239 = array2;
			int num180 = 74;
			array116 = new byte[2];
			array116[0] = 73;
			array239[num180] = array116;
			byte[][] array240 = array2;
			int num181 = 75;
			array116 = new byte[2];
			array116[0] = 74;
			array240[num181] = array116;
			byte[][] array241 = array2;
			int num182 = 76;
			array116 = new byte[2];
			array116[0] = 75;
			array241[num182] = array116;
			byte[][] array242 = array2;
			int num183 = 77;
			array116 = new byte[2];
			array116[0] = 76;
			array242[num183] = array116;
			byte[][] array243 = array2;
			int num184 = 78;
			array116 = new byte[2];
			array116[0] = 77;
			array243[num184] = array116;
			byte[][] array244 = array2;
			int num185 = 79;
			array116 = new byte[2];
			array116[0] = 78;
			array244[num185] = array116;
			byte[][] array245 = array2;
			int num186 = 80;
			array116 = new byte[2];
			array116[0] = 79;
			array245[num186] = array116;
			byte[][] array246 = array2;
			int num187 = 81;
			array116 = new byte[2];
			array116[0] = 80;
			array246[num187] = array116;
			byte[][] array247 = array2;
			int num188 = 82;
			array116 = new byte[2];
			array116[0] = 81;
			array247[num188] = array116;
			byte[][] array248 = array2;
			int num189 = 83;
			array116 = new byte[2];
			array116[0] = 82;
			array248[num189] = array116;
			byte[][] array249 = array2;
			int num190 = 84;
			array116 = new byte[2];
			array116[0] = 83;
			array249[num190] = array116;
			byte[][] array250 = array2;
			int num191 = 85;
			array116 = new byte[2];
			array116[0] = 84;
			array250[num191] = array116;
			byte[][] array251 = array2;
			int num192 = 86;
			array116 = new byte[2];
			array116[0] = 85;
			array251[num192] = array116;
			byte[][] array252 = array2;
			int num193 = 87;
			array116 = new byte[2];
			array116[0] = 86;
			array252[num193] = array116;
			byte[][] array253 = array2;
			int num194 = 88;
			array116 = new byte[2];
			array116[0] = 87;
			array253[num194] = array116;
			byte[][] array254 = array2;
			int num195 = 89;
			array116 = new byte[2];
			array116[0] = 88;
			array254[num195] = array116;
			byte[][] array255 = array2;
			int num196 = 90;
			array116 = new byte[2];
			array116[0] = 89;
			array255[num196] = array116;
			byte[][] array256 = array2;
			int num197 = 91;
			array116 = new byte[2];
			array116[0] = 90;
			array256[num197] = array116;
			byte[][] array257 = array2;
			int num198 = 92;
			array116 = new byte[2];
			array116[0] = 91;
			array257[num198] = array116;
			byte[][] array258 = array2;
			int num199 = 93;
			array116 = new byte[2];
			array116[0] = 92;
			array258[num199] = array116;
			byte[][] array259 = array2;
			int num200 = 94;
			array116 = new byte[2];
			array116[0] = 93;
			array259[num200] = array116;
			byte[][] array260 = array2;
			int num201 = 95;
			array116 = new byte[2];
			array116[0] = 94;
			array260[num201] = array116;
			byte[][] array261 = array2;
			int num202 = 96;
			array116 = new byte[2];
			array116[0] = 95;
			array261[num202] = array116;
			byte[][] array262 = array2;
			int num203 = 97;
			array116 = new byte[2];
			array116[0] = 96;
			array262[num203] = array116;
			byte[][] array263 = array2;
			int num204 = 98;
			array116 = new byte[2];
			array116[0] = 97;
			array263[num204] = array116;
			byte[][] array264 = array2;
			int num205 = 99;
			array116 = new byte[2];
			array116[0] = 98;
			array264[num205] = array116;
			byte[][] array265 = array2;
			int num206 = 100;
			array116 = new byte[2];
			array116[0] = 99;
			array265[num206] = array116;
			byte[][] array266 = array2;
			int num207 = 101;
			array116 = new byte[2];
			array116[0] = 100;
			array266[num207] = array116;
			byte[][] array267 = array2;
			int num208 = 102;
			array116 = new byte[2];
			array116[0] = 101;
			array267[num208] = array116;
			array2[103] = new byte[]
			{
				2,
				1
			};
			array2[104] = new byte[]
			{
				3,
				1
			};
			array2[105] = new byte[]
			{
				4,
				1
			};
			array2[106] = new byte[]
			{
				5,
				1
			};
			array2[107] = new byte[]
			{
				6,
				1
			};
			array2[108] = new byte[]
			{
				7,
				1
			};
			array2[109] = new byte[]
			{
				8,
				1
			};
			array2[110] = new byte[]
			{
				9,
				1
			};
			array2[111] = new byte[]
			{
				10,
				1
			};
			array2[112] = new byte[]
			{
				11,
				1
			};
			array2[113] = new byte[]
			{
				12,
				1
			};
			array2[114] = new byte[]
			{
				13,
				1
			};
			array2[115] = new byte[]
			{
				14,
				1
			};
			array2[116] = new byte[]
			{
				15,
				1
			};
			array2[117] = new byte[]
			{
				16,
				1
			};
			array2[118] = new byte[]
			{
				17,
				1
			};
			array2[119] = new byte[]
			{
				18,
				1
			};
			array2[120] = new byte[]
			{
				19,
				1
			};
			array2[121] = new byte[]
			{
				20,
				1
			};
			array2[122] = new byte[]
			{
				21,
				1
			};
			array2[123] = new byte[]
			{
				22,
				1
			};
			array2[124] = new byte[]
			{
				23,
				1
			};
			array2[125] = new byte[]
			{
				24,
				1
			};
			array2[126] = new byte[]
			{
				25,
				1
			};
			array2[127] = new byte[]
			{
				26,
				1
			};
			array2[128] = new byte[]
			{
				27,
				1
			};
			array2[129] = new byte[]
			{
				28,
				1
			};
			array2[130] = new byte[]
			{
				29,
				1
			};
			array2[131] = new byte[]
			{
				30,
				1
			};
			array2[132] = new byte[]
			{
				31,
				1
			};
			array2[133] = new byte[]
			{
				32,
				1
			};
			array2[134] = new byte[]
			{
				33,
				1
			};
			array2[135] = new byte[]
			{
				34,
				1
			};
			array2[136] = new byte[]
			{
				35,
				1
			};
			array2[137] = new byte[]
			{
				36,
				1
			};
			array2[138] = new byte[]
			{
				37,
				1
			};
			array2[139] = new byte[]
			{
				38,
				1
			};
			array2[140] = new byte[]
			{
				39,
				1
			};
			array2[141] = new byte[]
			{
				40,
				1
			};
			array2[142] = new byte[]
			{
				41,
				1
			};
			array2[143] = new byte[]
			{
				42,
				1
			};
			array2[144] = new byte[]
			{
				43,
				1
			};
			array2[145] = new byte[]
			{
				44,
				1
			};
			array2[146] = new byte[]
			{
				45,
				1
			};
			array2[147] = new byte[]
			{
				46,
				1
			};
			array2[148] = new byte[]
			{
				47,
				1
			};
			array2[149] = new byte[]
			{
				48,
				1
			};
			array2[150] = new byte[]
			{
				49,
				1
			};
			array2[151] = new byte[]
			{
				50,
				1
			};
			array2[152] = new byte[]
			{
				51,
				1
			};
			array2[153] = new byte[]
			{
				52,
				1
			};
			array2[154] = new byte[]
			{
				53,
				1
			};
			array2[155] = new byte[]
			{
				54,
				1
			};
			array2[156] = new byte[]
			{
				55,
				1
			};
			array2[157] = new byte[]
			{
				56,
				1
			};
			array2[158] = new byte[]
			{
				57,
				1
			};
			array2[159] = new byte[]
			{
				58,
				1
			};
			array2[160] = new byte[]
			{
				59,
				1
			};
			array2[161] = new byte[]
			{
				60,
				1
			};
			array2[162] = new byte[]
			{
				61,
				1
			};
			array2[163] = new byte[]
			{
				62,
				1
			};
			array2[164] = new byte[]
			{
				63,
				1
			};
			array2[165] = new byte[]
			{
				64,
				1
			};
			array2[166] = new byte[]
			{
				65,
				1
			};
			array2[167] = new byte[]
			{
				66,
				1
			};
			array2[168] = new byte[]
			{
				67,
				1
			};
			array2[169] = new byte[]
			{
				68,
				1
			};
			array2[170] = new byte[]
			{
				69,
				1
			};
			array2[171] = new byte[]
			{
				70,
				1
			};
			array2[172] = new byte[]
			{
				71,
				1
			};
			array2[173] = new byte[]
			{
				72,
				1
			};
			array2[174] = new byte[]
			{
				73,
				1
			};
			array2[175] = new byte[]
			{
				74,
				1
			};
			array2[176] = new byte[]
			{
				75,
				1
			};
			array2[177] = new byte[]
			{
				76,
				1
			};
			array2[178] = new byte[]
			{
				77,
				1
			};
			array2[179] = new byte[]
			{
				78,
				1
			};
			array2[180] = new byte[]
			{
				79,
				1
			};
			array2[181] = new byte[]
			{
				80,
				1
			};
			array2[182] = new byte[]
			{
				81,
				1
			};
			array2[183] = new byte[]
			{
				82,
				1
			};
			array2[184] = new byte[]
			{
				83,
				1
			};
			array2[185] = new byte[]
			{
				84,
				1
			};
			array2[186] = new byte[]
			{
				85,
				1
			};
			array2[187] = new byte[]
			{
				86,
				1
			};
			array2[188] = new byte[]
			{
				87,
				1
			};
			array2[189] = new byte[]
			{
				88,
				1
			};
			array2[190] = new byte[]
			{
				89,
				1
			};
			array2[191] = new byte[]
			{
				90,
				1
			};
			array2[192] = new byte[]
			{
				91,
				1
			};
			array2[193] = new byte[]
			{
				92,
				1
			};
			array2[194] = new byte[]
			{
				93,
				1
			};
			array2[195] = new byte[]
			{
				94,
				1
			};
			array2[196] = new byte[]
			{
				95,
				1
			};
			array2[197] = new byte[]
			{
				96,
				1
			};
			array2[198] = new byte[]
			{
				97,
				1
			};
			array2[199] = new byte[]
			{
				98,
				1
			};
			array2[200] = new byte[]
			{
				99,
				1
			};
			array2[201] = new byte[]
			{
				100,
				1
			};
			array2[202] = new byte[]
			{
				101,
				1
			};
			OracleNumberCore.LnxqAdd_PNN = array2;
			OracleNumberCore.powerTable = new double[][]
			{
				new double[]
				{
					128.0,
					1E+256,
					1E-256
				},
				new double[]
				{
					64.0,
					1E+128,
					1E-128
				},
				new double[]
				{
					32.0,
					1E+64,
					1E-64
				},
				new double[]
				{
					16.0,
					1E+32,
					1E-32
				},
				new double[]
				{
					8.0,
					10000000000000000.0,
					1E-16
				},
				new double[]
				{
					4.0,
					100000000.0,
					1E-08
				},
				new double[]
				{
					2.0,
					10000.0,
					0.0001
				},
				new double[]
				{
					1.0,
					100.0,
					0.01
				}
			};
			OracleNumberCore.LnxqNegate = new byte[]
			{
				0,
				101,
				100,
				99,
				98,
				97,
				96,
				95,
				94,
				93,
				92,
				91,
				90,
				89,
				88,
				87,
				86,
				85,
				84,
				83,
				82,
				81,
				80,
				79,
				78,
				77,
				76,
				75,
				74,
				73,
				72,
				71,
				70,
				69,
				68,
				67,
				66,
				65,
				64,
				63,
				62,
				61,
				60,
				59,
				58,
				57,
				56,
				55,
				54,
				53,
				52,
				51,
				50,
				49,
				48,
				47,
				46,
				45,
				44,
				43,
				42,
				41,
				40,
				39,
				38,
				37,
				36,
				35,
				34,
				33,
				32,
				31,
				30,
				29,
				28,
				27,
				26,
				25,
				24,
				23,
				22,
				21,
				20,
				19,
				18,
				17,
				16,
				15,
				14,
				13,
				12,
				11,
				10,
				9,
				8,
				7,
				6,
				5,
				4,
				3,
				2,
				1
			};
			OracleNumberCore.LnxqTruncate_P = new byte[]
			{
				0,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				91
			};
			OracleNumberCore.LnxqTruncate_N = new byte[]
			{
				0,
				0,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				101,
				101,
				101,
				101,
				101,
				101,
				101,
				101,
				101,
				101
			};
			OracleNumberCore.LnxqFirstDigit = new byte[]
			{
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1
			};
			OracleNumberCore.factorTable = new double[][]
			{
				new double[]
				{
					15.0,
					1E+30,
					1E-30
				},
				new double[]
				{
					14.0,
					1E+28,
					1E-28
				},
				new double[]
				{
					13.0,
					1E+26,
					1E-26
				},
				new double[]
				{
					12.0,
					1E+24,
					1E-24
				},
				new double[]
				{
					11.0,
					1E+22,
					1E-22
				},
				new double[]
				{
					10.0,
					1E+20,
					1E-20
				},
				new double[]
				{
					9.0,
					1E+18,
					1E-18
				},
				new double[]
				{
					8.0,
					10000000000000000.0,
					1E-16
				},
				new double[]
				{
					7.0,
					100000000000000.0,
					1E-14
				},
				new double[]
				{
					6.0,
					1000000000000.0,
					1E-12
				},
				new double[]
				{
					5.0,
					10000000000.0,
					1E-10
				},
				new double[]
				{
					4.0,
					100000000.0,
					1E-08
				},
				new double[]
				{
					3.0,
					1000000.0,
					1E-06
				},
				new double[]
				{
					2.0,
					10000.0,
					0.0001
				},
				new double[]
				{
					1.0,
					100.0,
					0.01
				},
				new double[]
				{
					0.0,
					1.0,
					1.0
				},
				new double[]
				{
					-1.0,
					0.01,
					100.0
				},
				new double[]
				{
					-2.0,
					0.0001,
					10000.0
				},
				new double[]
				{
					-3.0,
					1E-06,
					1000000.0
				},
				new double[]
				{
					-4.0,
					1E-08,
					100000000.0
				},
				new double[]
				{
					-5.0,
					1E-10,
					10000000000.0
				},
				new double[]
				{
					-6.0,
					1E-12,
					1000000000000.0
				},
				new double[]
				{
					-7.0,
					1E-14,
					100000000000000.0
				},
				new double[]
				{
					-8.0,
					1E-16,
					10000000000000000.0
				},
				new double[]
				{
					-9.0,
					1E-18,
					1E+18
				},
				new double[]
				{
					-10.0,
					1E-20,
					1E+20
				},
				new double[]
				{
					-11.0,
					1E-22,
					1E+22
				},
				new double[]
				{
					-12.0,
					1E-24,
					1E+24
				},
				new double[]
				{
					-13.0,
					1E-26,
					1E+26
				},
				new double[]
				{
					-14.0,
					1E-28,
					1E+28
				},
				new double[]
				{
					-15.0,
					1E-30,
					1E+30
				},
				new double[]
				{
					-16.0,
					1E-32,
					1E+32
				},
				new double[]
				{
					-17.0,
					1E-34,
					1E+34
				},
				new double[]
				{
					-18.0,
					1E-36,
					1E+36
				},
				new double[]
				{
					-19.0,
					1E-38,
					1E+38
				},
				new double[]
				{
					-20.0,
					1E-40,
					1E+40
				},
				new double[]
				{
					-21.0,
					1E-42,
					1E+42
				},
				new double[]
				{
					-22.0,
					1E-44,
					1E+44
				},
				new double[]
				{
					-23.0,
					1E-46,
					1E+46
				},
				new double[]
				{
					-24.0,
					1E-48,
					1E+48
				},
				new double[]
				{
					-25.0,
					1E-50,
					1E+50
				},
				new double[]
				{
					-26.0,
					1E-52,
					1E+52
				},
				new double[]
				{
					-27.0,
					1E-54,
					1E+54
				},
				new double[]
				{
					-28.0,
					1E-56,
					1E+56
				},
				new double[]
				{
					-29.0,
					1E-58,
					1E+58
				},
				new double[]
				{
					-30.0,
					1E-60,
					1E+60
				},
				new double[]
				{
					-31.0,
					1E-62,
					1E+62
				},
				new double[]
				{
					-32.0,
					1E-64,
					1E+64
				},
				new double[]
				{
					-33.0,
					1E-66,
					1E+66
				},
				new double[]
				{
					-34.0,
					1E-68,
					1E+68
				}
			};
			OracleNumberCore.LnxqRound_P = new byte[]
			{
				0,
				1,
				1,
				1,
				1,
				1,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				101,
				101,
				101,
				101,
				101
			};
			OracleNumberCore.LnxqRound_N = new byte[]
			{
				0,
				0,
				1,
				1,
				1,
				1,
				1,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				21,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				31,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				51,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				61,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				71,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				81,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				91,
				101,
				101,
				101,
				101,
				101
			};
			array2 = new byte[101][];
			byte[][] array268 = array2;
			int num209 = 0;
			array116 = new byte[2];
			array268[num209] = array116;
			byte[][] array269 = array2;
			int num210 = 1;
			array116 = new byte[2];
			array269[num210] = array116;
			array2[2] = new byte[]
			{
				0,
				1
			};
			array2[3] = new byte[]
			{
				0,
				2
			};
			array2[4] = new byte[]
			{
				0,
				3
			};
			array2[5] = new byte[]
			{
				0,
				4
			};
			array2[6] = new byte[]
			{
				0,
				5
			};
			array2[7] = new byte[]
			{
				0,
				6
			};
			array2[8] = new byte[]
			{
				0,
				7
			};
			array2[9] = new byte[]
			{
				0,
				8
			};
			array2[10] = new byte[]
			{
				0,
				9
			};
			byte[][] array270 = array2;
			int num211 = 11;
			array116 = new byte[2];
			array116[0] = 1;
			array270[num211] = array116;
			array2[12] = new byte[]
			{
				1,
				1
			};
			array2[13] = new byte[]
			{
				1,
				2
			};
			array2[14] = new byte[]
			{
				1,
				3
			};
			array2[15] = new byte[]
			{
				1,
				4
			};
			array2[16] = new byte[]
			{
				1,
				5
			};
			array2[17] = new byte[]
			{
				1,
				6
			};
			array2[18] = new byte[]
			{
				1,
				7
			};
			array2[19] = new byte[]
			{
				1,
				8
			};
			array2[20] = new byte[]
			{
				1,
				9
			};
			byte[][] array271 = array2;
			int num212 = 21;
			array116 = new byte[2];
			array116[0] = 2;
			array271[num212] = array116;
			array2[22] = new byte[]
			{
				2,
				1
			};
			array2[23] = new byte[]
			{
				2,
				2
			};
			array2[24] = new byte[]
			{
				2,
				3
			};
			array2[25] = new byte[]
			{
				2,
				4
			};
			array2[26] = new byte[]
			{
				2,
				5
			};
			array2[27] = new byte[]
			{
				2,
				6
			};
			array2[28] = new byte[]
			{
				2,
				7
			};
			array2[29] = new byte[]
			{
				2,
				8
			};
			array2[30] = new byte[]
			{
				2,
				9
			};
			byte[][] array272 = array2;
			int num213 = 31;
			array116 = new byte[2];
			array116[0] = 3;
			array272[num213] = array116;
			array2[32] = new byte[]
			{
				3,
				1
			};
			array2[33] = new byte[]
			{
				3,
				2
			};
			array2[34] = new byte[]
			{
				3,
				3
			};
			array2[35] = new byte[]
			{
				3,
				4
			};
			array2[36] = new byte[]
			{
				3,
				5
			};
			array2[37] = new byte[]
			{
				3,
				6
			};
			array2[38] = new byte[]
			{
				3,
				7
			};
			array2[39] = new byte[]
			{
				3,
				8
			};
			array2[40] = new byte[]
			{
				3,
				9
			};
			byte[][] array273 = array2;
			int num214 = 41;
			array116 = new byte[2];
			array116[0] = 4;
			array273[num214] = array116;
			array2[42] = new byte[]
			{
				4,
				1
			};
			array2[43] = new byte[]
			{
				4,
				2
			};
			array2[44] = new byte[]
			{
				4,
				3
			};
			array2[45] = new byte[]
			{
				4,
				4
			};
			array2[46] = new byte[]
			{
				4,
				5
			};
			array2[47] = new byte[]
			{
				4,
				6
			};
			array2[48] = new byte[]
			{
				4,
				7
			};
			array2[49] = new byte[]
			{
				4,
				8
			};
			array2[50] = new byte[]
			{
				4,
				9
			};
			byte[][] array274 = array2;
			int num215 = 51;
			array116 = new byte[2];
			array116[0] = 5;
			array274[num215] = array116;
			array2[52] = new byte[]
			{
				5,
				1
			};
			array2[53] = new byte[]
			{
				5,
				2
			};
			array2[54] = new byte[]
			{
				5,
				3
			};
			array2[55] = new byte[]
			{
				5,
				4
			};
			array2[56] = new byte[]
			{
				5,
				5
			};
			array2[57] = new byte[]
			{
				5,
				6
			};
			array2[58] = new byte[]
			{
				5,
				7
			};
			array2[59] = new byte[]
			{
				5,
				8
			};
			array2[60] = new byte[]
			{
				5,
				9
			};
			byte[][] array275 = array2;
			int num216 = 61;
			array116 = new byte[2];
			array116[0] = 6;
			array275[num216] = array116;
			array2[62] = new byte[]
			{
				6,
				1
			};
			array2[63] = new byte[]
			{
				6,
				2
			};
			array2[64] = new byte[]
			{
				6,
				3
			};
			array2[65] = new byte[]
			{
				6,
				4
			};
			array2[66] = new byte[]
			{
				6,
				5
			};
			array2[67] = new byte[]
			{
				6,
				6
			};
			array2[68] = new byte[]
			{
				6,
				7
			};
			array2[69] = new byte[]
			{
				6,
				8
			};
			array2[70] = new byte[]
			{
				6,
				9
			};
			byte[][] array276 = array2;
			int num217 = 71;
			array116 = new byte[2];
			array116[0] = 7;
			array276[num217] = array116;
			array2[72] = new byte[]
			{
				7,
				1
			};
			array2[73] = new byte[]
			{
				7,
				2
			};
			array2[74] = new byte[]
			{
				7,
				3
			};
			array2[75] = new byte[]
			{
				7,
				4
			};
			array2[76] = new byte[]
			{
				7,
				5
			};
			array2[77] = new byte[]
			{
				7,
				6
			};
			array2[78] = new byte[]
			{
				7,
				7
			};
			array2[79] = new byte[]
			{
				7,
				8
			};
			array2[80] = new byte[]
			{
				7,
				9
			};
			byte[][] array277 = array2;
			int num218 = 81;
			array116 = new byte[2];
			array116[0] = 8;
			array277[num218] = array116;
			array2[82] = new byte[]
			{
				8,
				1
			};
			array2[83] = new byte[]
			{
				8,
				2
			};
			array2[84] = new byte[]
			{
				8,
				3
			};
			array2[85] = new byte[]
			{
				8,
				4
			};
			array2[86] = new byte[]
			{
				8,
				5
			};
			array2[87] = new byte[]
			{
				8,
				6
			};
			array2[88] = new byte[]
			{
				8,
				7
			};
			array2[89] = new byte[]
			{
				8,
				8
			};
			array2[90] = new byte[]
			{
				8,
				9
			};
			byte[][] array278 = array2;
			int num219 = 91;
			array116 = new byte[2];
			array116[0] = 9;
			array278[num219] = array116;
			array2[92] = new byte[]
			{
				9,
				1
			};
			array2[93] = new byte[]
			{
				9,
				2
			};
			array2[94] = new byte[]
			{
				9,
				3
			};
			array2[95] = new byte[]
			{
				9,
				4
			};
			array2[96] = new byte[]
			{
				9,
				5
			};
			array2[97] = new byte[]
			{
				9,
				6
			};
			array2[98] = new byte[]
			{
				9,
				7
			};
			array2[99] = new byte[]
			{
				9,
				8
			};
			array2[100] = new byte[]
			{
				9,
				9
			};
			OracleNumberCore.LnxqComponents_P = array2;
			array2 = new byte[102][];
			byte[][] array279 = array2;
			int num220 = 0;
			array116 = new byte[2];
			array279[num220] = array116;
			byte[][] array280 = array2;
			int num221 = 1;
			array116 = new byte[2];
			array280[num221] = array116;
			array2[2] = new byte[]
			{
				9,
				9
			};
			array2[3] = new byte[]
			{
				9,
				8
			};
			array2[4] = new byte[]
			{
				9,
				7
			};
			array2[5] = new byte[]
			{
				9,
				6
			};
			array2[6] = new byte[]
			{
				9,
				5
			};
			array2[7] = new byte[]
			{
				9,
				4
			};
			array2[8] = new byte[]
			{
				9,
				3
			};
			array2[9] = new byte[]
			{
				9,
				2
			};
			array2[10] = new byte[]
			{
				9,
				1
			};
			byte[][] array281 = array2;
			int num222 = 11;
			array116 = new byte[2];
			array116[0] = 9;
			array281[num222] = array116;
			array2[12] = new byte[]
			{
				8,
				9
			};
			array2[13] = new byte[]
			{
				8,
				8
			};
			array2[14] = new byte[]
			{
				8,
				7
			};
			array2[15] = new byte[]
			{
				8,
				6
			};
			array2[16] = new byte[]
			{
				8,
				5
			};
			array2[17] = new byte[]
			{
				8,
				4
			};
			array2[18] = new byte[]
			{
				8,
				3
			};
			array2[19] = new byte[]
			{
				8,
				2
			};
			array2[20] = new byte[]
			{
				8,
				1
			};
			byte[][] array282 = array2;
			int num223 = 21;
			array116 = new byte[2];
			array116[0] = 8;
			array282[num223] = array116;
			array2[22] = new byte[]
			{
				7,
				9
			};
			array2[23] = new byte[]
			{
				7,
				8
			};
			array2[24] = new byte[]
			{
				7,
				7
			};
			array2[25] = new byte[]
			{
				7,
				6
			};
			array2[26] = new byte[]
			{
				7,
				5
			};
			array2[27] = new byte[]
			{
				7,
				4
			};
			array2[28] = new byte[]
			{
				7,
				3
			};
			array2[29] = new byte[]
			{
				7,
				2
			};
			array2[30] = new byte[]
			{
				7,
				1
			};
			byte[][] array283 = array2;
			int num224 = 31;
			array116 = new byte[2];
			array116[0] = 7;
			array283[num224] = array116;
			array2[32] = new byte[]
			{
				6,
				9
			};
			array2[33] = new byte[]
			{
				6,
				8
			};
			array2[34] = new byte[]
			{
				6,
				7
			};
			array2[35] = new byte[]
			{
				6,
				6
			};
			array2[36] = new byte[]
			{
				6,
				5
			};
			array2[37] = new byte[]
			{
				6,
				4
			};
			array2[38] = new byte[]
			{
				6,
				3
			};
			array2[39] = new byte[]
			{
				6,
				2
			};
			array2[40] = new byte[]
			{
				6,
				1
			};
			byte[][] array284 = array2;
			int num225 = 41;
			array116 = new byte[2];
			array116[0] = 6;
			array284[num225] = array116;
			array2[42] = new byte[]
			{
				5,
				9
			};
			array2[43] = new byte[]
			{
				5,
				8
			};
			array2[44] = new byte[]
			{
				5,
				7
			};
			array2[45] = new byte[]
			{
				5,
				6
			};
			array2[46] = new byte[]
			{
				5,
				5
			};
			array2[47] = new byte[]
			{
				5,
				4
			};
			array2[48] = new byte[]
			{
				5,
				3
			};
			array2[49] = new byte[]
			{
				5,
				2
			};
			array2[50] = new byte[]
			{
				5,
				1
			};
			byte[][] array285 = array2;
			int num226 = 51;
			array116 = new byte[2];
			array116[0] = 5;
			array285[num226] = array116;
			array2[52] = new byte[]
			{
				4,
				9
			};
			array2[53] = new byte[]
			{
				4,
				8
			};
			array2[54] = new byte[]
			{
				4,
				7
			};
			array2[55] = new byte[]
			{
				4,
				6
			};
			array2[56] = new byte[]
			{
				4,
				5
			};
			array2[57] = new byte[]
			{
				4,
				4
			};
			array2[58] = new byte[]
			{
				4,
				3
			};
			array2[59] = new byte[]
			{
				4,
				2
			};
			array2[60] = new byte[]
			{
				4,
				1
			};
			byte[][] array286 = array2;
			int num227 = 61;
			array116 = new byte[2];
			array116[0] = 4;
			array286[num227] = array116;
			array2[62] = new byte[]
			{
				3,
				9
			};
			array2[63] = new byte[]
			{
				3,
				8
			};
			array2[64] = new byte[]
			{
				3,
				7
			};
			array2[65] = new byte[]
			{
				3,
				6
			};
			array2[66] = new byte[]
			{
				3,
				5
			};
			array2[67] = new byte[]
			{
				3,
				4
			};
			array2[68] = new byte[]
			{
				3,
				3
			};
			array2[69] = new byte[]
			{
				3,
				2
			};
			array2[70] = new byte[]
			{
				3,
				1
			};
			byte[][] array287 = array2;
			int num228 = 71;
			array116 = new byte[2];
			array116[0] = 3;
			array287[num228] = array116;
			array2[72] = new byte[]
			{
				2,
				9
			};
			array2[73] = new byte[]
			{
				2,
				8
			};
			array2[74] = new byte[]
			{
				2,
				7
			};
			array2[75] = new byte[]
			{
				2,
				6
			};
			array2[76] = new byte[]
			{
				2,
				5
			};
			array2[77] = new byte[]
			{
				2,
				4
			};
			array2[78] = new byte[]
			{
				2,
				3
			};
			array2[79] = new byte[]
			{
				2,
				2
			};
			array2[80] = new byte[]
			{
				2,
				1
			};
			byte[][] array288 = array2;
			int num229 = 81;
			array116 = new byte[2];
			array116[0] = 2;
			array288[num229] = array116;
			array2[82] = new byte[]
			{
				1,
				9
			};
			array2[83] = new byte[]
			{
				1,
				8
			};
			array2[84] = new byte[]
			{
				1,
				7
			};
			array2[85] = new byte[]
			{
				1,
				6
			};
			array2[86] = new byte[]
			{
				1,
				5
			};
			array2[87] = new byte[]
			{
				1,
				4
			};
			array2[88] = new byte[]
			{
				1,
				3
			};
			array2[89] = new byte[]
			{
				1,
				2
			};
			array2[90] = new byte[]
			{
				1,
				1
			};
			byte[][] array289 = array2;
			int num230 = 91;
			array116 = new byte[2];
			array116[0] = 1;
			array289[num230] = array116;
			array2[92] = new byte[]
			{
				0,
				9
			};
			array2[93] = new byte[]
			{
				0,
				8
			};
			array2[94] = new byte[]
			{
				0,
				7
			};
			array2[95] = new byte[]
			{
				0,
				6
			};
			array2[96] = new byte[]
			{
				0,
				5
			};
			array2[97] = new byte[]
			{
				0,
				4
			};
			array2[98] = new byte[]
			{
				0,
				3
			};
			array2[99] = new byte[]
			{
				0,
				2
			};
			array2[100] = new byte[]
			{
				0,
				1
			};
			byte[][] array290 = array2;
			int num231 = 101;
			array116 = new byte[2];
			array290[num231] = array116;
			OracleNumberCore.LnxqComponents_N = array2;
			OracleNumberCore.LnxqDigit_P = new byte[][]
			{
				new byte[]
				{
					1,
					2,
					3,
					4,
					5,
					6,
					7,
					8,
					9,
					10
				},
				new byte[]
				{
					11,
					12,
					13,
					14,
					15,
					16,
					17,
					18,
					19,
					20
				},
				new byte[]
				{
					21,
					22,
					23,
					24,
					25,
					26,
					27,
					28,
					29,
					30
				},
				new byte[]
				{
					31,
					32,
					33,
					34,
					35,
					36,
					37,
					38,
					39,
					40
				},
				new byte[]
				{
					41,
					42,
					43,
					44,
					45,
					46,
					47,
					48,
					49,
					50
				},
				new byte[]
				{
					51,
					52,
					53,
					54,
					55,
					56,
					57,
					58,
					59,
					60
				},
				new byte[]
				{
					61,
					62,
					63,
					64,
					65,
					66,
					67,
					68,
					69,
					70
				},
				new byte[]
				{
					71,
					72,
					73,
					74,
					75,
					76,
					77,
					78,
					79,
					80
				},
				new byte[]
				{
					81,
					82,
					83,
					84,
					85,
					86,
					87,
					88,
					89,
					90
				},
				new byte[]
				{
					91,
					92,
					93,
					94,
					95,
					96,
					97,
					98,
					99,
					100
				}
			};
			OracleNumberCore.LnxqDigit_N = new byte[][]
			{
				new byte[]
				{
					101,
					100,
					99,
					98,
					97,
					96,
					95,
					94,
					93,
					92
				},
				new byte[]
				{
					91,
					90,
					89,
					88,
					87,
					86,
					85,
					84,
					83,
					82
				},
				new byte[]
				{
					81,
					80,
					79,
					78,
					77,
					76,
					75,
					74,
					73,
					72
				},
				new byte[]
				{
					71,
					70,
					69,
					68,
					67,
					66,
					65,
					64,
					63,
					62
				},
				new byte[]
				{
					61,
					60,
					59,
					58,
					57,
					56,
					55,
					54,
					53,
					52
				},
				new byte[]
				{
					51,
					50,
					49,
					48,
					47,
					46,
					45,
					44,
					43,
					42
				},
				new byte[]
				{
					41,
					40,
					39,
					38,
					37,
					36,
					35,
					34,
					33,
					32
				},
				new byte[]
				{
					31,
					30,
					29,
					28,
					27,
					26,
					25,
					24,
					23,
					22
				},
				new byte[]
				{
					21,
					20,
					19,
					18,
					17,
					16,
					15,
					14,
					13,
					12
				},
				new byte[]
				{
					11,
					10,
					9,
					8,
					7,
					6,
					5,
					4,
					3,
					2
				}
			};
		}

		// Token: 0x04000BCA RID: 3018
		private const int LNXSGNBT = 128;

		// Token: 0x04000BCB RID: 3019
		private const int LNXEXPMN = 0;

		// Token: 0x04000BCC RID: 3020
		private const int LNXEXPMX = 127;

		// Token: 0x04000BCD RID: 3021
		private const byte LNXEXPBS = 64;

		// Token: 0x04000BCE RID: 3022
		private const byte LNXDIGS = 20;

		// Token: 0x04000BCF RID: 3023
		private const int LNXQTRIPREC = 15;

		// Token: 0x04000BD0 RID: 3024
		private const int LNXQTRIMAXITER = 15;

		// Token: 0x04000BD1 RID: 3025
		private const int LNXQACOS = 0;

		// Token: 0x04000BD2 RID: 3026
		private const int LNXQASIN = 1;

		// Token: 0x04000BD3 RID: 3027
		private const int LNXQATAN = 2;

		// Token: 0x04000BD4 RID: 3028
		private const int LNXQCOS = 3;

		// Token: 0x04000BD5 RID: 3029
		private const int LNXQSIN = 4;

		// Token: 0x04000BD6 RID: 3030
		private const int LNXQTAN = 5;

		// Token: 0x04000BD7 RID: 3031
		private const int LNXQCSH = 6;

		// Token: 0x04000BD8 RID: 3032
		private const int LNXQSNH = 7;

		// Token: 0x04000BD9 RID: 3033
		private const int LNXQTNH = 8;

		// Token: 0x04000BDA RID: 3034
		private const int LNXQEXP = 9;

		// Token: 0x04000BDB RID: 3035
		private const double ORANUM_FBASE = 100.0;

		// Token: 0x04000BDC RID: 3036
		private const int LNXM_NUM = 22;

		// Token: 0x04000BDD RID: 3037
		private const int LNXBYTEMASK = 255;

		// Token: 0x04000BDE RID: 3038
		private const int LNXSHORTMASK = 65535;

		// Token: 0x04000BDF RID: 3039
		private const int MINUB1MAXVAL = 255;

		// Token: 0x04000BE0 RID: 3040
		private static int LNXBASE = 100;

		// Token: 0x04000BE1 RID: 3041
		private static byte[] lnxqone = new byte[]
		{
			193,
			2
		};

		// Token: 0x04000BE2 RID: 3042
		private static byte[] lnxqtwo = new byte[]
		{
			193,
			3
		};

		// Token: 0x04000BE3 RID: 3043
		internal static byte[] PI = new byte[]
		{
			193,
			4,
			15,
			16,
			93,
			66,
			36,
			90,
			80,
			33,
			39,
			47,
			27,
			44,
			39,
			33,
			80,
			51,
			29,
			85,
			21
		};

		// Token: 0x04000BE4 RID: 3044
		internal static byte[] E = new byte[]
		{
			193,
			3,
			72,
			83,
			82,
			83,
			85,
			60,
			5,
			53,
			36,
			37,
			3,
			88,
			48,
			14,
			53,
			67,
			25,
			98,
			77
		};

		// Token: 0x04000BE5 RID: 3045
		internal static byte[] LN10 = new byte[]
		{
			193,
			3,
			31,
			26,
			86,
			10,
			30,
			95,
			5,
			57,
			85,
			2,
			80,
			92,
			46,
			47,
			85,
			37,
			43,
			8,
			61
		};

		// Token: 0x04000BE6 RID: 3046
		internal static byte[] NANREPD;

		// Token: 0x04000BE7 RID: 3047
		private static int LNXDIV_LNXBASE_SQUARED;

		// Token: 0x04000BE8 RID: 3048
		private static byte[] MAX_LONG;

		// Token: 0x04000BE9 RID: 3049
		private static byte[] MIN_LONG;

		// Token: 0x04000BEA RID: 3050
		private static byte[][] LnxqAdd_PPP;

		// Token: 0x04000BEB RID: 3051
		private static byte[][] LnxqAdd_NNN;

		// Token: 0x04000BEC RID: 3052
		private static byte[][] LnxqAdd_PNP;

		// Token: 0x04000BED RID: 3053
		private static byte[][] LnxqAdd_PNN;

		// Token: 0x04000BEE RID: 3054
		private static double[][] powerTable;

		// Token: 0x04000BEF RID: 3055
		private static byte[] LnxqNegate;

		// Token: 0x04000BF0 RID: 3056
		private static byte[] LnxqTruncate_P;

		// Token: 0x04000BF1 RID: 3057
		private static byte[] LnxqTruncate_N;

		// Token: 0x04000BF2 RID: 3058
		private static byte[] LnxqFirstDigit;

		// Token: 0x04000BF3 RID: 3059
		private static double[][] factorTable;

		// Token: 0x04000BF4 RID: 3060
		private static byte[] LnxqRound_P;

		// Token: 0x04000BF5 RID: 3061
		private static byte[] LnxqRound_N;

		// Token: 0x04000BF6 RID: 3062
		private static byte[][] LnxqComponents_P;

		// Token: 0x04000BF7 RID: 3063
		private static byte[][] LnxqComponents_N;

		// Token: 0x04000BF8 RID: 3064
		private static byte[][] LnxqDigit_P;

		// Token: 0x04000BF9 RID: 3065
		private static byte[][] LnxqDigit_N;
	}
}
