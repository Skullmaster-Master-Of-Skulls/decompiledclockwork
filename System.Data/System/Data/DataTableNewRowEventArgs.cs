using System;

namespace System.Data
{
	// Token: 0x0200009F RID: 159
	public sealed class DataTableNewRowEventArgs : EventArgs
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x002098D8 File Offset: 0x00208CD8
		public DataTableNewRowEventArgs(DataRow dataRow)
		{
			this.dataRow = dataRow;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x002098F8 File Offset: 0x00208CF8
		public DataRow Row
		{
			get
			{
				return this.dataRow;
			}
		}

		// Token: 0x04000826 RID: 2086
		private readonly DataRow dataRow;
	}
}
