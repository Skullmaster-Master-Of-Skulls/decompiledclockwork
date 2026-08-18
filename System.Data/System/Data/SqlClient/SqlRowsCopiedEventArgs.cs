using System;

namespace System.Data.SqlClient
{
	// Token: 0x020002A5 RID: 677
	public class SqlRowsCopiedEventArgs : EventArgs
	{
		// Token: 0x060022A8 RID: 8872 RVA: 0x0028C6B8 File Offset: 0x0028BAB8
		public SqlRowsCopiedEventArgs(long rowsCopied)
		{
			this._rowsCopied = rowsCopied;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x0028C6D8 File Offset: 0x0028BAD8
		// (set) Token: 0x060022AA RID: 8874 RVA: 0x0028C6F8 File Offset: 0x0028BAF8
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

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x0028C718 File Offset: 0x0028BB18
		public long RowsCopied
		{
			get
			{
				return this._rowsCopied;
			}
		}

		// Token: 0x04001685 RID: 5765
		private bool _abort;

		// Token: 0x04001686 RID: 5766
		private long _rowsCopied;
	}
}
