using System;
using System.Runtime.InteropServices;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200001F RID: 31
	internal class OpoIYMCtx : IDisposable
	{
		// Token: 0x0600012D RID: 301 RVA: 0x000118BC File Offset: 0x000108BC
		public OpoIYMCtx(int years, int months)
		{
			int num = 0;
			try
			{
				num = OpsIYM.AllocValCtxFromData(years, months, ref this.m_pValCtx);
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
				if (num != 0)
				{
					ITLMethods.FreeCtx(ref this.m_pValCtx);
				}
			}
			this.m_error = num;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0001192C File Offset: 0x0001092C
		public OpoIYMCtx(double years)
		{
			int num = 0;
			try
			{
				num = OpsIYM.AllocValCtxFromYears(years, ref this.m_pValCtx);
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
				if (num != 0)
				{
					ITLMethods.FreeCtx(ref this.m_pValCtx);
				}
			}
			this.m_error = num;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0001199C File Offset: 0x0001099C
		internal OpoIYMCtx(byte[] binData)
		{
			int num = 0;
			try
			{
				num = OpsIYM.AllocValCtxFromBytes(binData, out this.m_pValCtx, 9);
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
				if (num != 0)
				{
					ITLMethods.FreeCtx(ref this.m_pValCtx);
				}
			}
			this.m_error = num;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00011A0C File Offset: 0x00010A0C
		public OpoIYMCtx(string data)
		{
			int num = 0;
			IntPtr intPtr = Marshal.StringToCoTaskMemUni(data);
			try
			{
				num = OpsIYM.AllocValCtxFromStr(intPtr, ref this.m_pValCtx);
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
				if (num != 0)
				{
					ITLMethods.FreeCtx(ref this.m_pValCtx);
				}
			}
			Marshal.FreeCoTaskMem(intPtr);
			this.m_error = num;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00011A88 File Offset: 0x00010A88
		internal unsafe OpoIYMCtx(OpoITLValCtx* ctx)
		{
			this.m_pValCtx = ctx;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00011A98 File Offset: 0x00010A98
		~OpoIYMCtx()
		{
			this.Dispose();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00011AC4 File Offset: 0x00010AC4
		public void Dispose()
		{
			ITLMethods.FreeCtx(ref this.m_pValCtx);
			try
			{
				GC.SuppressFinalize(this);
			}
			catch
			{
			}
		}

		// Token: 0x040000BE RID: 190
		internal unsafe OpoITLValCtx* m_pValCtx;

		// Token: 0x040000BF RID: 191
		internal int m_error;
	}
}
