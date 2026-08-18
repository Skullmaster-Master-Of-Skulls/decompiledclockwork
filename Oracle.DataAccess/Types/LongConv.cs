using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000049 RID: 73
	internal class LongConv
	{
		// Token: 0x0600033A RID: 826 RVA: 0x00028525 File Offset: 0x00027525
		private LongConv()
		{
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0002852D File Offset: 0x0002752D
		public unsafe static long GetLong(OpoITLValCtx* pValCtx, OracleDbType oraType)
		{
			if (oraType != OracleDbType.IntervalYM)
			{
				throw new OracleTypeException(ErrRes.INT_ERR, new object[0]);
			}
			return (long)pValCtx->m_ym.m_years * 12L + (long)pValCtx->m_ym.m_months;
		}
	}
}
