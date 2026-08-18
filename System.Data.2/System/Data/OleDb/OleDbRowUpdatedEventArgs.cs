using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	// Token: 0x02000260 RID: 608
	public sealed class OleDbRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		// Token: 0x06002657 RID: 9815 RVA: 0x00103DE4 File Offset: 0x001031E4
		public OleDbRowUpdatedEventArgs(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(dataRow, command, statementType, tableMapping)
		{
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x00103DFC File Offset: 0x001031FC
		public new OleDbCommand Command
		{
			get
			{
				return (OleDbCommand)base.Command;
			}
		}
	}
}
