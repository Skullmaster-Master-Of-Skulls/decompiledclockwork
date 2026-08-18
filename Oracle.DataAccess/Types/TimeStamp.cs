using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000013 RID: 19
	internal class TimeStamp
	{
		// Token: 0x06000099 RID: 153 RVA: 0x0000E51E File Offset: 0x0000D51E
		private TimeStamp()
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000E528 File Offset: 0x0000D528
		internal static int Compare(OpoTSCtx TSCtx1, OpoTSCtx TSCtx2)
		{
			int result = 0;
			int num = 0;
			try
			{
				num = OpsTSA.Compare(TSCtx1.m_pValCtx, TSCtx2.m_pValCtx, ref result);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000E580 File Offset: 0x0000D580
		internal unsafe static void FillValCtxFromDateTime(OpoTSValCtx* pValCtx, DateTime dt)
		{
			int year = dt.Year;
			int month = dt.Month;
			int day = dt.Day;
			int hour = dt.Hour;
			int minute = dt.Minute;
			int second = dt.Second;
			DateTime dateTime = new DateTime(year, month, day, hour, minute, second);
			long num = dt.Ticks - dateTime.Ticks;
			pValCtx->m_year = (short)year;
			pValCtx->m_month = (byte)month;
			pValCtx->m_day = (byte)day;
			pValCtx->m_hour = (byte)hour;
			pValCtx->m_minute = (byte)minute;
			pValCtx->m_second = (byte)second;
			pValCtx->m_fSecond = (int)(num * 100L);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000E620 File Offset: 0x0000D620
		internal unsafe static int GetTSData(OpoTSValCtx* pValCtx, byte tsComponent)
		{
			int result = 0;
			switch (tsComponent)
			{
			case 0:
				result = (int)pValCtx->m_year;
				break;
			case 1:
				result = (int)pValCtx->m_month;
				break;
			case 2:
				result = (int)pValCtx->m_day;
				break;
			case 3:
				result = (int)pValCtx->m_hour;
				break;
			case 4:
				result = (int)pValCtx->m_minute;
				break;
			case 5:
				result = (int)pValCtx->m_second;
				break;
			case 6:
				result = pValCtx->m_fSecond / 1000000;
				break;
			case 7:
				result = pValCtx->m_fSecond;
				break;
			case 8:
				result = (int)pValCtx->m_tzHour;
				break;
			case 9:
				result = (int)pValCtx->m_tzMinute;
				break;
			}
			return result;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000E6C0 File Offset: 0x0000D6C0
		internal unsafe static OracleTimeStampTZ ToUniversalTime(OpoTSCtx ctx1)
		{
			int num = 0;
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForToUTC(ctx1.m_pValCtx, out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoTSCtx ctx2 = new OpoTSCtx(pCtx);
			return new OracleTimeStampTZ(ctx2);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000E71C File Offset: 0x0000D71C
		internal static bool IsValidDateTime(int year, int month, int day, int hour, int minute, int second, int nanosecond)
		{
			return year >= -4712 && year <= 9999 && month >= 1 && month <= 12 && day >= 1 && day <= 31 && hour >= 0 && hour <= 23 && minute >= 0 && minute <= 59 && second >= 0 && second <= 59 && nanosecond >= 0 && nanosecond <= 999999999;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000E788 File Offset: 0x0000D788
		internal static string ToTSString(OpoTSCtx ctx, int fSecondPrec, TimeStampType tsType)
		{
			int num = 0;
			string result;
			switch (tsType)
			{
			case TimeStampType.TSType_TS:
				try
				{
					num = OpsTS.ToString(ctx.m_pValCtx, fSecondPrec, out result);
					goto IL_B0;
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				break;
			case (TimeStampType)4:
			case TimeStampType.TSType_TSZ:
			case (TimeStampType)6:
				goto IL_8D;
			case TimeStampType.TSType_TSL:
				break;
			default:
				goto IL_8D;
			}
			OracleIntervalDS oracleIntervalDS = new OracleIntervalDS(0, TimeStamp.LocalTZOffset.m_tzHours, TimeStamp.LocalTZOffset.m_tzMinutes, 0, 0);
			try
			{
				num = OpsTSL.ToString(ctx.m_pValCtx, oracleIntervalDS.GetValCtx(), fSecondPrec, out result);
				goto IL_B0;
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				throw;
			}
			try
			{
				IL_8D:
				num = OpsTSZ.ToString(ctx.m_pValCtx, fSecondPrec, out result);
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				throw;
			}
			IL_B0:
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000E880 File Offset: 0x0000D880
		internal unsafe static TZInfo GetLocalTZOffset()
		{
			int num = 0;
			int tzHours;
			int tzMinutes;
			try
			{
				num = OpsTSA.GetTimeZoneOffset(&tzHours, &tzMinutes);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			return new TZInfo(tzHours, tzMinutes);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000E8D4 File Offset: 0x0000D8D4
		internal static string GetLocalTZName()
		{
			int num = 0;
			string result;
			try
			{
				num = OpsTSA.GetSysTZName(out result);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			return result;
		}

		// Token: 0x04000079 RID: 121
		internal const short MaxYear = 9999;

		// Token: 0x0400007A RID: 122
		internal const byte MaxMonth = 12;

		// Token: 0x0400007B RID: 123
		internal const byte MaxDay = 31;

		// Token: 0x0400007C RID: 124
		internal const byte MaxHour = 23;

		// Token: 0x0400007D RID: 125
		internal const byte MaxMinute = 59;

		// Token: 0x0400007E RID: 126
		internal const byte MaxSecond = 59;

		// Token: 0x0400007F RID: 127
		internal const double MaxMillisecond = 999.999999;

		// Token: 0x04000080 RID: 128
		internal const int MaxFSecond = 999999999;

		// Token: 0x04000081 RID: 129
		internal const int MaxTZHours = 14;

		// Token: 0x04000082 RID: 130
		internal const int MaxTZMinutes = 59;

		// Token: 0x04000083 RID: 131
		internal const short MinYear = -4712;

		// Token: 0x04000084 RID: 132
		internal const byte MinMonth = 1;

		// Token: 0x04000085 RID: 133
		internal const byte MinDay = 1;

		// Token: 0x04000086 RID: 134
		internal const byte MinHour = 0;

		// Token: 0x04000087 RID: 135
		internal const byte MinMinute = 0;

		// Token: 0x04000088 RID: 136
		internal const byte MinSecond = 0;

		// Token: 0x04000089 RID: 137
		internal const double MinMillisecond = 0.0;

		// Token: 0x0400008A RID: 138
		internal const byte MinFSecond = 0;

		// Token: 0x0400008B RID: 139
		internal const int MinTZHours = -12;

		// Token: 0x0400008C RID: 140
		internal const int MinTZMinutes = -59;

		// Token: 0x0400008D RID: 141
		internal const byte MaxFSecondPrec = 9;

		// Token: 0x0400008E RID: 142
		internal const byte MinFSecondPrec = 0;

		// Token: 0x0400008F RID: 143
		internal const byte YEAR = 0;

		// Token: 0x04000090 RID: 144
		internal const byte MONTH = 1;

		// Token: 0x04000091 RID: 145
		internal const byte DAY = 2;

		// Token: 0x04000092 RID: 146
		internal const byte HOUR = 3;

		// Token: 0x04000093 RID: 147
		internal const byte MINUTE = 4;

		// Token: 0x04000094 RID: 148
		internal const byte SECOND = 5;

		// Token: 0x04000095 RID: 149
		internal const byte MILLISECOND = 6;

		// Token: 0x04000096 RID: 150
		internal const byte FSECOND = 7;

		// Token: 0x04000097 RID: 151
		internal const byte TZHOURS = 8;

		// Token: 0x04000098 RID: 152
		internal const byte TZMINUTES = 9;

		// Token: 0x04000099 RID: 153
		internal static readonly TZInfo LocalTZOffset = TimeStamp.GetLocalTZOffset();

		// Token: 0x0400009A RID: 154
		internal static readonly string LocalTZName = TimeStamp.GetLocalTZName();
	}
}
