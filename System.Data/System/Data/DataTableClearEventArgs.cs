using System;

namespace System.Data
{
	// Token: 0x0200009B RID: 155
	public sealed class DataTableClearEventArgs : EventArgs
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x00208748 File Offset: 0x00207B48
		public DataTableClearEventArgs(DataTable dataTable)
		{
			this.dataTable = dataTable;
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00208768 File Offset: 0x00207B68
		public DataTable Table
		{
			get
			{
				return this.dataTable;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00208788 File Offset: 0x00207B88
		public string TableName
		{
			get
			{
				return this.dataTable.TableName;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x002087A8 File Offset: 0x00207BA8
		public string TableNamespace
		{
			get
			{
				return this.dataTable.Namespace;
			}
		}

		// Token: 0x0400081D RID: 2077
		private readonly DataTable dataTable;
	}
}
