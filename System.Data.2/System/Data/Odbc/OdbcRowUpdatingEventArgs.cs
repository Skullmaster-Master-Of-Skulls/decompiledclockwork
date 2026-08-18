using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x020002AD RID: 685
	public sealed class OdbcRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		// Token: 0x060029B2 RID: 10674 RVA: 0x00114B08 File Offset: 0x00113F08
		public OdbcRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060029B3 RID: 10675 RVA: 0x00114B20 File Offset: 0x00113F20
		// (set) Token: 0x060029B4 RID: 10676 RVA: 0x00114B38 File Offset: 0x00113F38
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

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060029B5 RID: 10677 RVA: 0x00114B4C File Offset: 0x00113F4C
		// (set) Token: 0x060029B6 RID: 10678 RVA: 0x00114B60 File Offset: 0x00113F60
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
