using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x0200030B RID: 779
	public sealed class SqlRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		// Token: 0x060028B7 RID: 10423 RVA: 0x002B1C88 File Offset: 0x002B1088
		public SqlRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x002B1CA8 File Offset: 0x002B10A8
		// (set) Token: 0x060028B9 RID: 10425 RVA: 0x002B1CC8 File Offset: 0x002B10C8
		public new SqlCommand Command
		{
			get
			{
				return base.Command as SqlCommand;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x002B1CE8 File Offset: 0x002B10E8
		// (set) Token: 0x060028BB RID: 10427 RVA: 0x002B1D08 File Offset: 0x002B1108
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.BaseCommand;
			}
			set
			{
				base.BaseCommand = (value as SqlCommand);
			}
		}
	}
}
