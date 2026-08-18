using System;

namespace System.Windows.Forms
{
	// Token: 0x020001C7 RID: 455
	public class DataGridViewColumnStateChangedEventArgs : EventArgs
	{
		// Token: 0x06001FC5 RID: 8133 RVA: 0x00097749 File Offset: 0x00095949
		public DataGridViewColumnStateChangedEventArgs(DataGridViewColumn dataGridViewColumn, DataGridViewElementStates stateChanged)
		{
			this.dataGridViewColumn = dataGridViewColumn;
			this.stateChanged = stateChanged;
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x0009775F File Offset: 0x0009595F
		public DataGridViewColumn Column
		{
			get
			{
				return this.dataGridViewColumn;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x00097767 File Offset: 0x00095967
		public DataGridViewElementStates StateChanged
		{
			get
			{
				return this.stateChanged;
			}
		}

		// Token: 0x04000D68 RID: 3432
		private DataGridViewColumn dataGridViewColumn;

		// Token: 0x04000D69 RID: 3433
		private DataGridViewElementStates stateChanged;
	}
}
