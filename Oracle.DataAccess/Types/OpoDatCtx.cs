using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200008E RID: 142
	internal class OpoDatCtx : IDisposable
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x0004595C File Offset: 0x0004495C
		public OpoDatCtx(int year, int month, int day, int hour, int minute, int second)
		{
			int num = 0;
			try
			{
				num = OpsDat.AllocValCtxFromData(year, month, day, hour, minute, second, out this.m_pValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0 && this.m_pValCtx != null)
				{
					try
					{
						OpsDat.FreeValCtx(this.m_pValCtx);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
					this.m_pValCtx = null;
				}
			}
			this.m_error = num;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00045A04 File Offset: 0x00044A04
		internal OpoDatCtx(byte[] binData)
		{
			int num = 0;
			try
			{
				num = OpsDat.AllocValCtxFromBytes(binData, out this.m_pValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0 && this.m_pValCtx != null)
				{
					try
					{
						OpsDat.FreeValCtx(this.m_pValCtx);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
					this.m_pValCtx = null;
				}
			}
			this.m_error = num;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00045AA4 File Offset: 0x00044AA4
		public OpoDatCtx(string datStr)
		{
			int num = 0;
			try
			{
				num = OpsDat.AllocValCtxFromStr(datStr, out this.m_pValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0 && this.m_pValCtx != null)
				{
					try
					{
						OpsDat.FreeValCtx(this.m_pValCtx);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
					this.m_pValCtx = null;
				}
			}
			this.m_error = num;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00045B44 File Offset: 0x00044B44
		internal unsafe OpoDatCtx(OpoDatValCtx* pCtx)
		{
			this.m_pValCtx = pCtx;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00045B54 File Offset: 0x00044B54
		~OpoDatCtx()
		{
			this.Dispose();
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00045B80 File Offset: 0x00044B80
		public void Dispose()
		{
			if (this.m_pValCtx != null)
			{
				try
				{
					OpsDat.FreeValCtx(this.m_pValCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
				this.m_pValCtx = null;
			}
			try
			{
				GC.SuppressFinalize(this);
			}
			catch
			{
			}
		}

		// Token: 0x04000410 RID: 1040
		internal unsafe OpoDatValCtx* m_pValCtx;

		// Token: 0x04000411 RID: 1041
		internal int m_error;
	}
}
