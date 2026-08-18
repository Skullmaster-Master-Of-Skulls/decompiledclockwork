using System;

namespace System.Windows.Forms
{
	// Token: 0x020001B5 RID: 437
	public class DataGridViewCellStyleContentChangedEventArgs : EventArgs
	{
		// Token: 0x06001EAF RID: 7855 RVA: 0x00090A44 File Offset: 0x0008EC44
		internal DataGridViewCellStyleContentChangedEventArgs(DataGridViewCellStyle dataGridViewCellStyle, bool changeAffectsPreferredSize)
		{
			this.dataGridViewCellStyle = dataGridViewCellStyle;
			this.changeAffectsPreferredSize = changeAffectsPreferredSize;
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x00090A5A File Offset: 0x0008EC5A
		public DataGridViewCellStyle CellStyle
		{
			get
			{
				return this.dataGridViewCellStyle;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x00090A62 File Offset: 0x0008EC62
		public DataGridViewCellStyleScopes CellStyleScope
		{
			get
			{
				return this.dataGridViewCellStyle.Scope;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x00090A6F File Offset: 0x0008EC6F
		internal bool ChangeAffectsPreferredSize
		{
			get
			{
				return this.changeAffectsPreferredSize;
			}
		}

		// Token: 0x04000CFC RID: 3324
		private DataGridViewCellStyle dataGridViewCellStyle;

		// Token: 0x04000CFD RID: 3325
		private bool changeAffectsPreferredSize;
	}
}
