using System;

namespace System.Windows.Forms
{
	// Token: 0x020001C4 RID: 452
	public class DataGridViewColumnEventArgs : EventArgs
	{
		// Token: 0x06001FB0 RID: 8112 RVA: 0x00095BF1 File Offset: 0x00093DF1
		public DataGridViewColumnEventArgs(DataGridViewColumn dataGridViewColumn)
		{
			if (dataGridViewColumn == null)
			{
				throw new ArgumentNullException("dataGridViewColumn");
			}
			this.dataGridViewColumn = dataGridViewColumn;
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x00095C0E File Offset: 0x00093E0E
		public DataGridViewColumn Column
		{
			get
			{
				return this.dataGridViewColumn;
			}
		}

		// Token: 0x04000D54 RID: 3412
		private DataGridViewColumn dataGridViewColumn;
	}
}
