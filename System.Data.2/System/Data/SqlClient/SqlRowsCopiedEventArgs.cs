using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001A2 RID: 418
	public class SqlRowsCopiedEventArgs : EventArgs
	{
		// Token: 0x0600184E RID: 6222 RVA: 0x000AC2D0 File Offset: 0x000AB6D0
		public SqlRowsCopiedEventArgs(long rowsCopied)
		{
			this._rowsCopied = rowsCopied;
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x000AC2EC File Offset: 0x000AB6EC
		// (set) Token: 0x06001850 RID: 6224 RVA: 0x000AC300 File Offset: 0x000AB700
		public bool Abort
		{
			get
			{
				return this._abort;
			}
			set
			{
				this._abort = value;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x000AC314 File Offset: 0x000AB714
		public long RowsCopied
		{
			get
			{
				return this._rowsCopied;
			}
		}

		// Token: 0x04000EA7 RID: 3751
		private bool _abort;

		// Token: 0x04000EA8 RID: 3752
		private long _rowsCopied;
	}
}
