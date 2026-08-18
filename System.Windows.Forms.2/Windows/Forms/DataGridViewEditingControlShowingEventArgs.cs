using System;

namespace System.Windows.Forms
{
	// Token: 0x020001D3 RID: 467
	public class DataGridViewEditingControlShowingEventArgs : EventArgs
	{
		// Token: 0x0600207A RID: 8314 RVA: 0x0009BA56 File Offset: 0x00099C56
		public DataGridViewEditingControlShowingEventArgs(Control control, DataGridViewCellStyle cellStyle)
		{
			this.control = control;
			this.cellStyle = cellStyle;
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x0009BA6C File Offset: 0x00099C6C
		// (set) Token: 0x0600207C RID: 8316 RVA: 0x0009BA74 File Offset: 0x00099C74
		public DataGridViewCellStyle CellStyle
		{
			get
			{
				return this.cellStyle;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.cellStyle = value;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x0009BA8B File Offset: 0x00099C8B
		public Control Control
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x04000DB6 RID: 3510
		private Control control;

		// Token: 0x04000DB7 RID: 3511
		private DataGridViewCellStyle cellStyle;
	}
}
