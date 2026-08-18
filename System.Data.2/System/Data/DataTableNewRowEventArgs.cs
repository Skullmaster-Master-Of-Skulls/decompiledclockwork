using System;

namespace System.Data
{
	// Token: 0x020000D2 RID: 210
	public sealed class DataTableNewRowEventArgs : EventArgs
	{
		// Token: 0x06000DCA RID: 3530 RVA: 0x00073B4C File Offset: 0x00072F4C
		public DataTableNewRowEventArgs(DataRow dataRow)
		{
			this.dataRow = dataRow;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00073B68 File Offset: 0x00072F68
		public DataRow Row
		{
			get
			{
				return this.dataRow;
			}
		}

		// Token: 0x04000404 RID: 1028
		private readonly DataRow dataRow;
	}
}
