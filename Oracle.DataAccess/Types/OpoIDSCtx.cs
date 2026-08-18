using System;
using System.Runtime.InteropServices;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200008B RID: 139
	internal class OpoIDSCtx : IDisposable
	{
		// Token: 0x060006A3 RID: 1699 RVA: 0x00044488 File Offset: 0x00043488
		public OpoIDSCtx(int days, int hours, int minutes, int seconds, int fSeconds)
		{
			int num = 0;
			try
			{
				num = OpsIDS.AllocValCtxFromData(days, hours, minutes, seconds, fSeconds, ref this.m_pValCtx);
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

		// Token: 0x060006A4 RID: 1700 RVA: 0x000444FC File Offset: 0x000434FC
		public OpoIDSCtx(TimeSpan ts)
		{
			int num = 0;
			try
			{
				num = OpsIDS.AllocValCtx(ref this.m_pValCtx);
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
					this.m_error = num;
				}
			}
			if (num != 0)
			{
				return;
			}
			OracleIntervalDS.FillValCtxFromTimeSpan(this.m_pValCtx, ts);
			this.m_error = num;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00044580 File Offset: 0x00043580
		public OpoIDSCtx(double days)
		{
			int num = 0;
			try
			{
				num = OpsIDS.AllocValCtxFromDays(days, ref this.m_pValCtx);
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

		// Token: 0x060006A6 RID: 1702 RVA: 0x000445F0 File Offset: 0x000435F0
		public OpoIDSCtx(string data)
		{
			int num = 0;
			IntPtr intPtr = Marshal.StringToCoTaskMemUni(data);
			try
			{
				num = OpsIDS.AllocValCtxFromStr(intPtr, ref this.m_pValCtx);
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

		// Token: 0x060006A7 RID: 1703 RVA: 0x0004466C File Offset: 0x0004366C
		internal OpoIDSCtx(byte[] binData)
		{
			int num = 0;
			try
			{
				num = OpsIDS.AllocValCtxFromBytes(binData, out this.m_pValCtx, 9, 9);
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

		// Token: 0x060006A8 RID: 1704 RVA: 0x000446E0 File Offset: 0x000436E0
		internal unsafe OpoIDSCtx(OpoITLValCtx* ctx)
		{
			this.m_pValCtx = ctx;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000446F0 File Offset: 0x000436F0
		~OpoIDSCtx()
		{
			this.Dispose();
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0004471C File Offset: 0x0004371C
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

		// Token: 0x04000402 RID: 1026
		internal unsafe OpoITLValCtx* m_pValCtx;

		// Token: 0x04000403 RID: 1027
		internal int m_error;
	}
}
