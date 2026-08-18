using System;

namespace System.Windows.Forms
{
	// Token: 0x020001B9 RID: 441
	public class DataGridViewCellToolTipTextNeededEventArgs : DataGridViewCellEventArgs
	{
		// Token: 0x06001EB6 RID: 7862 RVA: 0x00090AE0 File Offset: 0x0008ECE0
		internal DataGridViewCellToolTipTextNeededEventArgs(int columnIndex, int rowIndex, string toolTipText) : base(columnIndex, rowIndex)
		{
			this.toolTipText = toolTipText;
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x00090AF1 File Offset: 0x0008ECF1
		// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x00090AF9 File Offset: 0x0008ECF9
		public string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				this.toolTipText = value;
			}
		}

		// Token: 0x04000D0C RID: 3340
		private string toolTipText;
	}
}
