using System;
using System.Data.Common;

namespace System.Data.OracleClient
{
	// Token: 0x02000078 RID: 120
	public sealed class OracleRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x00071BF4 File Offset: 0x00070FF4
		public OracleRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00071C14 File Offset: 0x00071014
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x00071C34 File Offset: 0x00071034
		public new OracleCommand Command
		{
			get
			{
				return base.Command as OracleCommand;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00071C54 File Offset: 0x00071054
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x00071C74 File Offset: 0x00071074
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
