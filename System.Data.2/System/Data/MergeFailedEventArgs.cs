using System;

namespace System.Data
{
	// Token: 0x0200010F RID: 271
	public class MergeFailedEventArgs : EventArgs
	{
		// Token: 0x060010DA RID: 4314 RVA: 0x00081F5C File Offset: 0x0008135C
		public MergeFailedEventArgs(DataTable table, string conflict)
		{
			this.table = table;
			this.conflict = conflict;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00081F80 File Offset: 0x00081380
		public DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x00081F94 File Offset: 0x00081394
		public string Conflict
		{
			get
			{
				return this.conflict;
			}
		}

		// Token: 0x04000585 RID: 1413
		private DataTable table;

		// Token: 0x04000586 RID: 1414
		private string conflict;
	}
}
