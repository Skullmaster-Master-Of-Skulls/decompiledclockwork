using System;

namespace System.Windows.Forms
{
	// Token: 0x020001A9 RID: 425
	public class DataGridViewCellErrorTextNeededEventArgs : DataGridViewCellEventArgs
	{
		// Token: 0x06001E36 RID: 7734 RVA: 0x0008F0AF File Offset: 0x0008D2AF
		internal DataGridViewCellErrorTextNeededEventArgs(int columnIndex, int rowIndex, string errorText) : base(columnIndex, rowIndex)
		{
			this.errorText = errorText;
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x0008F0C0 File Offset: 0x0008D2C0
		// (set) Token: 0x06001E38 RID: 7736 RVA: 0x0008F0C8 File Offset: 0x0008D2C8
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

		// Token: 0x04000CC5 RID: 3269
		private string errorText;
	}
}
