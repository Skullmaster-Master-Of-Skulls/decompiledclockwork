using System;

namespace System.Data
{
	// Token: 0x020000C6 RID: 198
	public class MergeFailedEventArgs : EventArgs
	{
		// Token: 0x06000CAF RID: 3247 RVA: 0x00210EC8 File Offset: 0x002102C8
		public MergeFailedEventArgs(DataTable table, string conflict)
		{
			this.table = table;
			this.conflict = conflict;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00210EF8 File Offset: 0x002102F8
		public DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00210F18 File Offset: 0x00210318
		public string Conflict
		{
			get
			{
				return this.conflict;
			}
		}

		// Token: 0x040008BB RID: 2235
		private DataTable table;

		// Token: 0x040008BC RID: 2236
		private string conflict;
	}
}
