using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x02000204 RID: 516
	public sealed class OdbcRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		// Token: 0x06001C7A RID: 7290 RVA: 0x002691B8 File Offset: 0x002685B8
		public OdbcRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x002691D8 File Offset: 0x002685D8
		public new OdbcCommand Command
		{
			get
			{
				return (OdbcCommand)base.Command;
			}
		}
	}
}
