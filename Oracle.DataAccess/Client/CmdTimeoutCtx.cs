using System;
using System.Threading;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000059 RID: 89
	internal class CmdTimeoutCtx
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x000320F9 File Offset: 0x000310F9
		public CmdTimeoutCtx(IntPtr pOpsConCtx, int timeoutSec)
		{
			this.m_pOpsConCtx = pOpsConCtx;
			this.m_hWaitForOciBreakEvent = new ManualResetEvent(true);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00032114 File Offset: 0x00031114
		~CmdTimeoutCtx()
		{
			this.Dispose();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00032148 File Offset: 0x00031148
		public void Dispose()
		{
			this.m_pOpsConCtx = IntPtr.Zero;
			if (this.m_hWaitForOciBreakEvent != null)
			{
				this.m_hWaitForOciBreakEvent.Close();
				this.m_hWaitForOciBreakEvent = null;
			}
			if (this.m_pErrHnd != IntPtr.Zero)
			{
				try
				{
					OpsErr.FreeCtx(ref this.m_pErrHnd);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
				this.m_pErrHnd = IntPtr.Zero;
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000321C8 File Offset: 0x000311C8
		public void TimeoutNew(object state)
		{
			CmdTimeoutCtx cmdTimeoutCtx = (CmdTimeoutCtx)state;
			try
			{
				cmdTimeoutCtx.m_hWaitForOciBreakEvent.Reset();
				if (!cmdTimeoutCtx.m_bDoneExecution)
				{
					OpsSql.BreakExecution(cmdTimeoutCtx.m_pOpsConCtx, ref cmdTimeoutCtx.m_pErrHnd);
				}
				cmdTimeoutCtx.m_bDoneOCIBreak = true;
			}
			catch
			{
			}
			finally
			{
				try
				{
					cmdTimeoutCtx.m_hWaitForOciBreakEvent.Set();
				}
				catch
				{
				}
			}
		}

		// Token: 0x040002BE RID: 702
		public IntPtr m_pOpsConCtx;

		// Token: 0x040002BF RID: 703
		public IntPtr m_pErrHnd;

		// Token: 0x040002C0 RID: 704
		public ManualResetEvent m_hWaitForOciBreakEvent;

		// Token: 0x040002C1 RID: 705
		public bool m_bDoneExecution;

		// Token: 0x040002C2 RID: 706
		public bool m_bDoneOCIBreak;
	}
}
