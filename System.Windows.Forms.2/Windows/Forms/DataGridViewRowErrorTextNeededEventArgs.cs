using System;

namespace System.Windows.Forms
{
	// Token: 0x0200020E RID: 526
	public class DataGridViewRowErrorTextNeededEventArgs : EventArgs
	{
		// Token: 0x0600227D RID: 8829 RVA: 0x000A5195 File Offset: 0x000A3395
		internal DataGridViewRowErrorTextNeededEventArgs(int rowIndex, string errorText)
		{
			this.rowIndex = rowIndex;
			this.errorText = errorText;
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x000A51AB File Offset: 0x000A33AB
		// (set) Token: 0x0600227F RID: 8831 RVA: 0x000A51B3 File Offset: 0x000A33B3
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
			set
			{
				this.errorText = value;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x000A51BC File Offset: 0x000A33BC
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000E38 RID: 3640
		private int rowIndex;

		// Token: 0x04000E39 RID: 3641
		private string errorText;
	}
}
