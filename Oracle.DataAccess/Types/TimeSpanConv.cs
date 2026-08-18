using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000048 RID: 72
	internal class TimeSpanConv
	{
		// Token: 0x06000337 RID: 823 RVA: 0x000283D4 File Offset: 0x000273D4
		private TimeSpanConv()
		{
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000283DC File Offset: 0x000273DC
		internal unsafe static decimal ValCtxToTicks(OpoITLValCtx* pValCtx)
		{
			return pValCtx->m_ds.m_days * 864000000000m + (long)pValCtx->m_ds.m_hours * 36000000000L + (long)pValCtx->m_ds.m_minutes * 600000000L + (long)pValCtx->m_ds.m_seconds * 10000000L + pValCtx->m_ds.m_fSeconds * 0.01m;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0002848C File Offset: 0x0002748C
		public unsafe static TimeSpan GetTimeSpan(OpoITLValCtx* pValCtx, OracleDbType oraType)
		{
			if (oraType != OracleDbType.IntervalDS)
			{
				throw new OracleTypeException(ErrRes.INT_ERR, new object[0]);
			}
			if (Math.Abs(pValCtx->m_ds.m_fSeconds) % 100 > 0)
			{
				throw new OracleTypeException(ErrRes.TYP_GETDOTNETTYPE_FAIL, new object[0]);
			}
			decimal num = TimeSpanConv.ValCtxToTicks(pValCtx);
			if (num < -9223372036854775808m || num > 9223372036854775807m)
			{
				throw new OracleTypeException(ErrRes.TYP_GETDOTNETTYPE_FAIL, new object[0]);
			}
			return new TimeSpan((long)num);
		}

		// Token: 0x04000258 RID: 600
		internal const int FSecondsPerMilliSecond = 1000000;

		// Token: 0x04000259 RID: 601
		internal const int FSecondsPerTick = 100;

		// Token: 0x0400025A RID: 602
		internal const double TicksPerFSecond = 0.01;
	}
}
