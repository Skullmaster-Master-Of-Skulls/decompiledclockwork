using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020001F1 RID: 497
	public sealed class SqlRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		// Token: 0x06001F0C RID: 7948 RVA: 0x000D7DA8 File Offset: 0x000D71A8
		public SqlRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x000D7DC0 File Offset: 0x000D71C0
		public new SqlCommand Command
		{
			get
			{
				return (SqlCommand)base.Command;
			}
		}
	}
}
