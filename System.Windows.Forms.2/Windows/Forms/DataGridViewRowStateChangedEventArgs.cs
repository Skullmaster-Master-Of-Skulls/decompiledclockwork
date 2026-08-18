using System;

namespace System.Windows.Forms
{
	// Token: 0x02000217 RID: 535
	public class DataGridViewRowStateChangedEventArgs : EventArgs
	{
		// Token: 0x060022D8 RID: 8920 RVA: 0x000A7213 File Offset: 0x000A5413
		public DataGridViewRowStateChangedEventArgs(DataGridViewRow dataGridViewRow, DataGridViewElementStates stateChanged)
		{
			this.dataGridViewRow = dataGridViewRow;
			this.stateChanged = stateChanged;
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x000A7229 File Offset: 0x000A5429
		public DataGridViewRow Row
		{
			get
			{
				return this.dataGridViewRow;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x000A7231 File Offset: 0x000A5431
		public DataGridViewElementStates StateChanged
		{
			get
			{
				return this.stateChanged;
			}
		}

		// Token: 0x04000E6A RID: 3690
		private DataGridViewRow dataGridViewRow;

		// Token: 0x04000E6B RID: 3691
		private DataGridViewElementStates stateChanged;
	}
}
