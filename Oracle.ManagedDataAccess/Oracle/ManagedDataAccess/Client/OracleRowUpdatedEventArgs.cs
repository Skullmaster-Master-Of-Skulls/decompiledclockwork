using System;
using System.Data;
using System.Data.Common;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200007B RID: 123
	public sealed class OracleRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		// Token: 0x0600064A RID: 1610 RVA: 0x0003912C File Offset: 0x0003732C
		public OracleRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0003913C File Offset: 0x0003733C
		public new OracleCommand Command
		{
			get
			{
				return (OracleCommand)base.Command;
			}
		}
	}
}
