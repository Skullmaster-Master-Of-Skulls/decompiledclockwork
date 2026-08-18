using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000047 RID: 71
	internal class DateTimeConv
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0002801E File Offset: 0x0002701E
		private DateTimeConv()
		{
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00028028 File Offset: 0x00027028
		public unsafe static DateTime GetDateTime(OpoDatValCtx* pValCtx, OracleDbType oraType, bool bCheck)
		{
			if (oraType != OracleDbType.Date)
			{
				throw new OracleTypeException(ErrRes.INT_ERR, new object[0]);
			}
			return new DateTime((int)pValCtx->m_year, (int)pValCtx->m_month, (int)pValCtx->m_day, (int)pValCtx->m_hour, (int)pValCtx->m_minute, (int)pValCtx->m_second);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00028074 File Offset: 0x00027074
		public unsafe static DateTime GetDateTime(byte* pDate)
		{
			int year = (int)((*pDate - 100) * 100 + pDate[1] - 100);
			int month = (int)pDate[2];
			int day = (int)pDate[3];
			int hour = (int)(pDate[4] - 1);
			int minute = (int)(pDate[5] - 1);
			int second = (int)(pDate[6] - 1);
			return new DateTime(year, month, day, hour, minute, second);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000280C8 File Offset: 0x000270C8
		public unsafe static DateTime GetDateTime(OpoTSValCtx* pValCtx, OracleDbType oraType, bool bCheck)
		{
			if (oraType != OracleDbType.TimeStamp && oraType != OracleDbType.TimeStampLTZ && oraType != OracleDbType.TimeStampTZ)
			{
				throw new OracleTypeException(ErrRes.INT_ERR, new object[0]);
			}
			if (oraType == OracleDbType.TimeStampTZ)
			{
				int num = 0;
				OpoTSValCtx opoTSValCtx;
				try
				{
					num = OpsTSZ.ConvertToTSL(pValCtx, &opoTSValCtx);
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
				pValCtx = &opoTSValCtx;
			}
			DateTime result = new DateTime((int)pValCtx->m_year, (int)pValCtx->m_month, (int)pValCtx->m_day, (int)pValCtx->m_hour, (int)pValCtx->m_minute, (int)pValCtx->m_second);
			if (pValCtx->m_fSecond > 0)
			{
				return result.AddTicks((long)(pValCtx->m_fSecond / 100));
			}
			return result;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00028188 File Offset: 0x00027188
		public unsafe static DateTimeOffset GetDateTimeOffset(OpoTSValCtx* pValCtx, OracleDbType oraType, bool bCheck)
		{
			if (oraType == OracleDbType.TimeStamp || oraType == OracleDbType.TimeStampLTZ || oraType == OracleDbType.TimeStampTZ)
			{
				if (oraType == OracleDbType.TimeStampTZ)
				{
					int num = 0;
					OpoTSValCtx opoTSValCtx;
					try
					{
						num = OpsTSZ.ConvertToTSL(pValCtx, &opoTSValCtx);
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
					pValCtx = &opoTSValCtx;
				}
				DateTimeOffset result = new DateTimeOffset((int)pValCtx->m_year, (int)pValCtx->m_month, (int)pValCtx->m_day, (int)pValCtx->m_hour, (int)pValCtx->m_minute, (int)pValCtx->m_second, new TimeSpan((int)pValCtx->m_tzHour, (int)pValCtx->m_tzMinute, 0));
				return result;
			}
			throw new OracleTypeException(ErrRes.INT_ERR, new object[0]);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0002823C File Offset: 0x0002723C
		internal unsafe static void ToBytes(DateTime data, byte* bytes)
		{
			*bytes = (byte)(data.Year / 100 + 100);
			bytes[1] = (byte)(data.Year % 100 + 100);
			bytes[2] = (byte)data.Month;
			bytes[3] = (byte)data.Day;
			bytes[4] = (byte)(data.Hour + 1);
			bytes[5] = (byte)(data.Minute + 1);
			bytes[6] = (byte)(data.Second + 1);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000282B4 File Offset: 0x000272B4
		internal unsafe static OpoTSValCtx* AllocTSValCtx(DateTime data)
		{
			OpoTSValCtx* ptr = null;
			try
			{
				OpsTS.AllocValCtx(ref ptr);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			TimeStamp.FillValCtxFromDateTime(ptr, data);
			return ptr;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000282F8 File Offset: 0x000272F8
		internal unsafe static OpoTSValCtx* AllocTSLValCtx(DateTime data)
		{
			OpoTSValCtx* ptr = null;
			try
			{
				OpsTSL.AllocValCtx(ref ptr);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			TimeStamp.FillValCtxFromDateTime(ptr, data);
			ptr->m_tzHour = (sbyte)TimeStamp.LocalTZOffset.m_tzHours;
			ptr->m_tzMinute = (sbyte)TimeStamp.LocalTZOffset.m_tzMinutes;
			return ptr;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0002835C File Offset: 0x0002735C
		internal unsafe static OpoTSValCtx* AllocTSZValCtx(DateTime data)
		{
			OpoTSValCtx opoTSValCtx;
			TimeStamp.FillValCtxFromDateTime(&opoTSValCtx, data);
			OpoTSValCtx* result;
			try
			{
				OpsTSZ.AllocValCtxFromData((int)opoTSValCtx.m_year, (int)opoTSValCtx.m_month, (int)opoTSValCtx.m_day, (int)opoTSValCtx.m_hour, (int)opoTSValCtx.m_minute, (int)opoTSValCtx.m_second, opoTSValCtx.m_fSecond, 0, 0, null, out result);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			return result;
		}
	}
}
