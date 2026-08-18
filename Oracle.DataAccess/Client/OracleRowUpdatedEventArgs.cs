using System;
using System.Data;
using System.Data.Common;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200001A RID: 26
	public sealed class OracleRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x0000F66C File Offset: 0x0000E66C
		static OracleRowUpdatedEventArgs()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000F67A File Offset: 0x0000E67A
		public OracleRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000F687 File Offset: 0x0000E687
		public new OracleCommand Command
		{
			get
			{
				return (OracleCommand)base.Command;
			}
		}
	}
}
