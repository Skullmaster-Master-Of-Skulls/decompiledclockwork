using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	// Token: 0x0200023C RID: 572
	public sealed class OleDbRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		// Token: 0x0600204B RID: 8267 RVA: 0x0027F498 File Offset: 0x0027E898
		public OleDbRowUpdatingEventArgs(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(dataRow, command, statementType, tableMapping)
		{
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x0027F4B8 File Offset: 0x0027E8B8
		// (set) Token: 0x0600204D RID: 8269 RVA: 0x0027F4D8 File Offset: 0x0027E8D8
		public new OleDbCommand Command
		{
			get
			{
				return base.Command as OleDbCommand;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x0027F4F8 File Offset: 0x0027E8F8
		// (set) Token: 0x0600204F RID: 8271 RVA: 0x0027F518 File Offset: 0x0027E918
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.BaseCommand;
			}
			set
			{
				base.BaseCommand = (value as OleDbCommand);
			}
		}
	}
}
