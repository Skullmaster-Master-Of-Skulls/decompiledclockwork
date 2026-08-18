using System;

namespace System.Data
{
	// Token: 0x020000CF RID: 207
	public sealed class DataTableClearEventArgs : EventArgs
	{
		// Token: 0x06000D93 RID: 3475 RVA: 0x00072A84 File Offset: 0x00071E84
		public DataTableClearEventArgs(DataTable dataTable)
		{
			this.dataTable = dataTable;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00072AA0 File Offset: 0x00071EA0
		public DataTable Table
		{
			get
			{
				return this.dataTable;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00072AB4 File Offset: 0x00071EB4
		public string TableName
		{
			get
			{
				return this.dataTable.TableName;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x00072ACC File Offset: 0x00071ECC
		public string TableNamespace
		{
			get
			{
				return this.dataTable.Namespace;
			}
		}

		// Token: 0x040003FB RID: 1019
		private readonly DataTable dataTable;
	}
}
