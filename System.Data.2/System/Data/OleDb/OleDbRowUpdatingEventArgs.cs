using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	// Token: 0x02000262 RID: 610
	public sealed class OleDbRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		// Token: 0x0600265D RID: 9821 RVA: 0x00103E14 File Offset: 0x00103214
		public OleDbRowUpdatingEventArgs(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(dataRow, command, statementType, tableMapping)
		{
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x00103E2C File Offset: 0x0010322C
		// (set) Token: 0x0600265F RID: 9823 RVA: 0x00103E44 File Offset: 0x00103244
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

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x00103E58 File Offset: 0x00103258
		// (set) Token: 0x06002661 RID: 9825 RVA: 0x00103E6C File Offset: 0x0010326C
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
