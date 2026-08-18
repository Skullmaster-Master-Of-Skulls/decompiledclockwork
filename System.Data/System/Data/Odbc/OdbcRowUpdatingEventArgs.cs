using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x02000203 RID: 515
	public sealed class OdbcRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		// Token: 0x06001C75 RID: 7285 RVA: 0x00269118 File Offset: 0x00268518
		public OdbcRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x00269138 File Offset: 0x00268538
		// (set) Token: 0x06001C77 RID: 7287 RVA: 0x00269158 File Offset: 0x00268558
		public new OdbcCommand Command
		{
			get
			{
				return base.Command as OdbcCommand;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x00269178 File Offset: 0x00268578
		// (set) Token: 0x06001C79 RID: 7289 RVA: 0x00269198 File Offset: 0x00268598
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.BaseCommand;
			}
			set
			{
				base.BaseCommand = (value as OdbcCommand);
			}
		}
	}
}
