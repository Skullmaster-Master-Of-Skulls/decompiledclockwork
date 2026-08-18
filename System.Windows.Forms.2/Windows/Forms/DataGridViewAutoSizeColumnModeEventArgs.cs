using System;

namespace System.Windows.Forms
{
	// Token: 0x02000193 RID: 403
	public class DataGridViewAutoSizeColumnModeEventArgs : EventArgs
	{
		// Token: 0x06001CB9 RID: 7353 RVA: 0x00086AF4 File Offset: 0x00084CF4
		public DataGridViewAutoSizeColumnModeEventArgs(DataGridViewColumn dataGridViewColumn, DataGridViewAutoSizeColumnMode previousMode)
		{
			this.dataGridViewColumn = dataGridViewColumn;
			this.previousMode = previousMode;
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x00086B0A File Offset: 0x00084D0A
		public DataGridViewColumn Column
		{
			get
			{
				return this.dataGridViewColumn;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x00086B12 File Offset: 0x00084D12
		public DataGridViewAutoSizeColumnMode PreviousMode
		{
			get
			{
				return this.previousMode;
			}
		}

		// Token: 0x04000C3B RID: 3131
		private DataGridViewAutoSizeColumnMode previousMode;

		// Token: 0x04000C3C RID: 3132
		private DataGridViewColumn dataGridViewColumn;
	}
}
