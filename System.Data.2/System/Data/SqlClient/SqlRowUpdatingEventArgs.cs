using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020001F3 RID: 499
	public sealed class SqlRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		// Token: 0x06001F12 RID: 7954 RVA: 0x000D7DD8 File Offset: 0x000D71D8
		public SqlRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x000D7DF0 File Offset: 0x000D71F0
		// (set) Token: 0x06001F14 RID: 7956 RVA: 0x000D7E08 File Offset: 0x000D7208
		public new SqlCommand Command
		{
			get
			{
				return base.Command as SqlCommand;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x000D7E1C File Offset: 0x000D721C
		// (set) Token: 0x06001F16 RID: 7958 RVA: 0x000D7E30 File Offset: 0x000D7230
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.BaseCommand;
			}
			set
			{
				base.BaseCommand = (value as SqlCommand);
			}
		}
	}
}
