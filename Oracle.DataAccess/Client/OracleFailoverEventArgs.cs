using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000130 RID: 304
	public sealed class OracleFailoverEventArgs : EventArgs
	{
		// Token: 0x06000C28 RID: 3112 RVA: 0x00078FD6 File Offset: 0x00077FD6
		static OracleFailoverEventArgs()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00078FE4 File Offset: 0x00077FE4
		public FailoverType FailoverType
		{
			get
			{
				return this.m_FailoverType;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00078FEC File Offset: 0x00077FEC
		public FailoverEvent FailoverEvent
		{
			get
			{
				return this.m_FailoverEvent;
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00078FF4 File Offset: 0x00077FF4
		internal OracleFailoverEventArgs(IntPtr svchp, IntPtr envhp, IntPtr fo_ctx, int fo_type, int fo_event)
		{
			this.pSvcCtx = svchp;
			this.pEnvHnd = envhp;
			this.m_FailoverCtx = fo_ctx;
			this.m_FailoverType = (FailoverType)fo_type;
			this.m_FailoverEvent = (FailoverEvent)fo_event;
		}

		// Token: 0x04000994 RID: 2452
		private IntPtr pSvcCtx;

		// Token: 0x04000995 RID: 2453
		private IntPtr pEnvHnd;

		// Token: 0x04000996 RID: 2454
		private IntPtr m_FailoverCtx;

		// Token: 0x04000997 RID: 2455
		private FailoverType m_FailoverType;

		// Token: 0x04000998 RID: 2456
		private FailoverEvent m_FailoverEvent;
	}
}
