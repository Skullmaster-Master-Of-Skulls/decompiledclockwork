using System;

namespace System.Data
{
	// Token: 0x020000C0 RID: 192
	public class DataRowChangeEventArgs : EventArgs
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x00062D34 File Offset: 0x00062134
		public DataRowChangeEventArgs(DataRow row, DataRowAction action)
		{
			this.row = row;
			this.action = action;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00062D58 File Offset: 0x00062158
		public DataRow Row
		{
			get
			{
				return this.row;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00062D6C File Offset: 0x0006216C
		public DataRowAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x04000363 RID: 867
		private DataRow row;

		// Token: 0x04000364 RID: 868
		private DataRowAction action;
	}
}
