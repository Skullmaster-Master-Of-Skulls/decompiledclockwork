using System;
using System.Data.Common;

namespace System.Data.OracleClient
{
	// Token: 0x02000076 RID: 118
	public sealed class OracleRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		// Token: 0x0600067E RID: 1662 RVA: 0x00071BB4 File Offset: 0x00070FB4
		public OracleRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00071BD4 File Offset: 0x00070FD4
		public new OracleCommand Command
		{
			get
			{
				return (OracleCommand)base.Command;
			}
		}
	}
}
