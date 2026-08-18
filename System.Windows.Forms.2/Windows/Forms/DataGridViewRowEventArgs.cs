using System;

namespace System.Windows.Forms
{
	// Token: 0x0200020F RID: 527
	public class DataGridViewRowEventArgs : EventArgs
	{
		// Token: 0x06002281 RID: 8833 RVA: 0x000A51C4 File Offset: 0x000A33C4
		public DataGridViewRowEventArgs(DataGridViewRow dataGridViewRow)
		{
			if (dataGridViewRow == null)
			{
				throw new ArgumentNullException("dataGridViewRow");
			}
			this.dataGridViewRow = dataGridViewRow;
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x000A51E1 File Offset: 0x000A33E1
		public DataGridViewRow Row
		{
			get
			{
				return this.dataGridViewRow;
			}
		}

		// Token: 0x04000E3A RID: 3642
		private DataGridViewRow dataGridViewRow;
	}
}
