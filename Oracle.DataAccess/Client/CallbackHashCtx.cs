using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200013C RID: 316
	internal class CallbackHashCtx
	{
		// Token: 0x06000CA7 RID: 3239 RVA: 0x000841BE File Offset: 0x000831BE
		public CallbackHashCtx(OpoConCtx opoConCtxReg)
		{
			this.m_opoConCtxReg = opoConCtxReg;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x000841D0 File Offset: 0x000831D0
		protected override void Finalize()
		{
			try
			{
				try
				{
					OpsCon.UnRegisterCallbacks(ref this.m_opoConCtxReg.opsConCtx, ref this.m_opoConCtxReg.opsErrCtx, this.m_opoConCtxReg.pOpoConValCtx, ref this.m_opoConCtxReg.opoConRefCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
				this.m_opoConCtxReg = null;
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x040009F8 RID: 2552
		internal OpoConCtx m_opoConCtxReg;

		// Token: 0x040009F9 RID: 2553
		internal bool m_shared;
	}
}
