using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000240 RID: 576
	internal static class DateTimeConv
	{
		// Token: 0x060014E3 RID: 5347 RVA: 0x000E1784 File Offset: 0x000DF984
		internal static DateTime GetDateTime(byte[] byteRep, OracleDbType oraType, int offset = 0, int length = -1)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DateTime result;
			try
			{
				if (oraType != OracleDbType.Date && oraType != OracleDbType.TimeStamp && oraType != OracleDbType.TimeStampLTZ && oraType != OracleDbType.TimeStampTZ)
				{
					throw new OracleTypeException(ResourceStringConstants.INT_ERR, new object[0]);
				}
				result = DateTimeConv.ToDateTime(byteRep, oraType != OracleDbType.TimeStampTZ, offset, length);
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

		// Token: 0x060014E4 RID: 5348 RVA: 0x000E1828 File Offset: 0x000DFA28
		internal static DateTime GetDateTime(byte[] byteRep, OraType oraType, int offset = 0, int length = -1)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DateTime result;
			try
			{
				if (oraType != OraType.ORA_DATE && oraType != OraType.ORA_TIMESTAMP && oraType != OraType.ORA_TIMESTAMP_DTY && oraType != OraType.ORA_TIMESTAMP_LTZ && oraType != OraType.ORA_TIMESTAMP_LTZ_DTY && oraType != OraType.ORA_TIMESTAMP_TZ && oraType != OraType.ORA_TIMESTAMP_TZ_DTY)
				{
					throw new OracleTypeException(ResourceStringConstants.INT_ERR, new object[0]);
				}
				result = DateTimeConv.ToDateTime(byteRep, oraType != OraType.ORA_TIMESTAMP_TZ && oraType != OraType.ORA_TIMESTAMP_TZ_DTY, offset, length);
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

		// Token: 0x060014E5 RID: 5349 RVA: 0x000E18FC File Offset: 0x000DFAFC
		internal static DateTime ToDateTime(byte[] byteRep, bool isNotTimeStampTZ, int offset, int length)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DateTime result;
			try
			{
				if (length == -1 && byteRep != null)
				{
					length = byteRep.Length - offset;
				}
				int num = (int)((byteRep[offset] - 100) * 100 + (byteRep[1 + offset] - 100));
				int num2 = (int)(byteRep[2 + offset] & byte.MaxValue);
				int num3 = (int)(byteRep[3 + offset] & byte.MaxValue);
				int num4 = (int)((byteRep[4 + offset] & byte.MaxValue) - 1);
				int num5 = (int)((byteRep[5 + offset] & byte.MaxValue) - 1);
				int second = (int)((byteRep[6 + offset] & byte.MaxValue) - 1);
				DateTime dateTime;
				if (isNotTimeStampTZ)
				{
					int millisecond = 0;
					int num6 = 0;
					if (length > 7)
					{
						num6 = (int)byteRep[7 + offset] << 24;
						num6 |= (int)byteRep[8 + offset] << 16;
						num6 |= (int)byteRep[9 + offset] << 8;
						num6 |= (int)(byteRep[10 + offset] & byte.MaxValue);
						millisecond = num6 / 1000000;
						num6 %= 1000000;
					}
					dateTime = new DateTime(num, num2, num3, num4, num5, second, millisecond);
					if (num6 > 0)
					{
						dateTime = dateTime.AddTicks((long)(num6 / 100));
					}
				}
				else
				{
					bool flag = true;
					DateTime? date = null;
					if (num >= 1800)
					{
						date = new DateTime?(new DateTime(num, num2, num3, num4, num5, second));
					}
					int num7;
					int num8;
					int num9;
					bool flag2;
					TimeStamp.GetTimeZoneOffsetInfo(date, byteRep, out num7, out num8, out num9, out flag2, false, offset, length);
					int num10 = 1;
					if (num < 0)
					{
						num10 = -1;
					}
					if (num7 < 0 || num8 < 0)
					{
						flag = false;
					}
					num5 += num8;
					num4 += num7;
					if (!flag)
					{
						if (num5 < 0)
						{
							num5 += 60;
							num4--;
						}
						if (num4 < 0)
						{
							num4 += 24;
							num3--;
						}
						if (num3 < 1)
						{
							num3 += DateTime.DaysInMonth((num == 0) ? 4 : (num * num10), (num2 - 1 == 0) ? 12 : (num2 - 1));
							num2--;
						}
						if (num2 < 1)
						{
							num2 += 12;
							num--;
						}
						if (num < -4712)
						{
							throw new OverflowException();
						}
					}
					else
					{
						if (num5 > 59)
						{
							num5 -= 60;
							num4++;
						}
						if (num4 > 23)
						{
							num4 -= 24;
							num3++;
						}
						if (num3 > DateTime.DaysInMonth((num == 0) ? 4 : (num * num10), num2))
						{
							num3 -= DateTime.DaysInMonth((num == 0) ? 4 : (num * num10), num2);
							num2++;
						}
						if (num2 > 12)
						{
							num2 -= 12;
							num++;
						}
					}
					int millisecond2 = 0;
					int num11 = 0;
					if (length > 7)
					{
						num11 = (int)byteRep[7 + offset] << 24;
						num11 |= (int)byteRep[8 + offset] << 16;
						num11 |= (int)byteRep[9 + offset] << 8;
						num11 |= (int)(byteRep[10 + offset] & byte.MaxValue);
						millisecond2 = num11 / 1000000;
						num11 %= 1000000;
					}
					dateTime = new DateTime(num, num2, num3, num4, num5, second, millisecond2);
					if (num11 > 0)
					{
						dateTime = dateTime.AddTicks((long)(num11 / 100));
					}
				}
				result = dateTime;
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
	}
}
