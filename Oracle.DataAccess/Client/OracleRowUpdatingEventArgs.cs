using System;
using System.Data;
using System.Data.Common;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000E4 RID: 228
	public sealed class OracleRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		// Token: 0x0600084A RID: 2122 RVA: 0x0005126B File Offset: 0x0005026B
		static OracleRowUpdatingEventArgs()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00051279 File Offset: 0x00050279
		public OracleRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00051286 File Offset: 0x00050286
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x00051293 File Offset: 0x00050293
		public new OracleCommand Command
		{
			get
			{
				return (OracleCommand)base.Command;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0005129C File Offset: 0x0005029C
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x000512A4 File Offset: 0x000502A4
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.BaseCommand;
			}
			set
			{
				base.BaseCommand = (value as OracleCommand);
			}
		}
	}
}
