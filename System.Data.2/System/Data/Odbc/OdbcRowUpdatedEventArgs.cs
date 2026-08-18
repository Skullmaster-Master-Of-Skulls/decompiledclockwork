using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x020002AE RID: 686
	public sealed class OdbcRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		// Token: 0x060029B7 RID: 10679 RVA: 0x00114B7C File Offset: 0x00113F7C
		public OdbcRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060029B8 RID: 10680 RVA: 0x00114B94 File Offset: 0x00113F94
		public new OdbcCommand Command
		{
			get
			{
				return (OdbcCommand)base.Command;
			}
		}
	}
}
