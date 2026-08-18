using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200004D RID: 77
	public class OracleRowsCopiedEventArgs : EventArgs
	{
		// Token: 0x0600034F RID: 847 RVA: 0x000287C4 File Offset: 0x000277C4
		static OracleRowsCopiedEventArgs()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000287D2 File Offset: 0x000277D2
		public OracleRowsCopiedEventArgs(long rowsCopied)
		{
			this.m_rowsCopied = rowsCopied;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000351 RID: 849 RVA: 0x000287E1 File Offset: 0x000277E1
		// (set) Token: 0x06000352 RID: 850 RVA: 0x000287E9 File Offset: 0x000277E9
		public bool Abort
		{
			get
			{
				return this.m_abort;
			}
			set
			{
				this.m_abort = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000353 RID: 851 RVA: 0x000287F2 File Offset: 0x000277F2
		public long RowsCopied
		{
			get
			{
				return this.m_rowsCopied;
			}
		}

		// Token: 0x04000263 RID: 611
		private bool m_abort;

		// Token: 0x04000264 RID: 612
		private long m_rowsCopied;
	}
}
