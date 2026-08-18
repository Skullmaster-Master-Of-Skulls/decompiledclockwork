using System;

namespace System.Data
{
	// Token: 0x02000084 RID: 132
	public class DataRowChangeEventArgs : EventArgs
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x001F5528 File Offset: 0x001F4928
		public DataRowChangeEventArgs(DataRow row, DataRowAction action)
		{
			this.row = row;
			this.action = action;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x001F5558 File Offset: 0x001F4958
		public DataRow Row
		{
			get
			{
				return this.row;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x001F5578 File Offset: 0x001F4978
		public DataRowAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x04000760 RID: 1888
		private DataRow row;

		// Token: 0x04000761 RID: 1889
		private DataRowAction action;
	}
}
