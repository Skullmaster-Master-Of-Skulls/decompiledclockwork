using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000209 RID: 521
	public class DataGridViewRowCancelEventArgs : CancelEventArgs
	{
		// Token: 0x06002215 RID: 8725 RVA: 0x000A1F7A File Offset: 0x000A017A
		public DataGridViewRowCancelEventArgs(DataGridViewRow dataGridViewRow)
		{
			this.dataGridViewRow = dataGridViewRow;
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x000A1F89 File Offset: 0x000A0189
		public DataGridViewRow Row
		{
			get
			{
				return this.dataGridViewRow;
			}
		}

		// Token: 0x04000E2B RID: 3627
		private DataGridViewRow dataGridViewRow;
	}
}
