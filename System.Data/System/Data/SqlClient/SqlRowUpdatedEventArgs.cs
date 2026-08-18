using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x02000309 RID: 777
	public sealed class SqlRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		// Token: 0x060028B1 RID: 10417 RVA: 0x002B1C48 File Offset: 0x002B1048
		public SqlRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x002B1C68 File Offset: 0x002B1068
		public new SqlCommand Command
		{
			get
			{
				return (SqlCommand)base.Command;
			}
		}
	}
}
