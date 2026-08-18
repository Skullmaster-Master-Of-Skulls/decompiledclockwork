using System;

namespace System.Windows.Forms
{
	// Token: 0x020001B2 RID: 434
	public class DataGridViewCellStateChangedEventArgs : EventArgs
	{
		// Token: 0x06001E72 RID: 7794 RVA: 0x0008F93C File Offset: 0x0008DB3C
		public DataGridViewCellStateChangedEventArgs(DataGridViewCell dataGridViewCell, DataGridViewElementStates stateChanged)
		{
			if (dataGridViewCell == null)
			{
				throw new ArgumentNullException("dataGridViewCell");
			}
			this.dataGridViewCell = dataGridViewCell;
			this.stateChanged = stateChanged;
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x0008F960 File Offset: 0x0008DB60
		public DataGridViewCell Cell
		{
			get
			{
				return this.dataGridViewCell;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x0008F968 File Offset: 0x0008DB68
		public DataGridViewElementStates StateChanged
		{
			get
			{
				return this.stateChanged;
			}
		}

		// Token: 0x04000CE8 RID: 3304
		private DataGridViewCell dataGridViewCell;

		// Token: 0x04000CE9 RID: 3305
		private DataGridViewElementStates stateChanged;
	}
}
