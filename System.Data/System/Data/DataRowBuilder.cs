using System;

namespace System.Data
{
	// Token: 0x02000082 RID: 130
	public sealed class DataRowBuilder
	{
		// Token: 0x060007C2 RID: 1986 RVA: 0x001F54F8 File Offset: 0x001F48F8
		internal DataRowBuilder(DataTable table, int record)
		{
			this._table = table;
			this._record = record;
		}

		// Token: 0x04000755 RID: 1877
		internal readonly DataTable _table;

		// Token: 0x04000756 RID: 1878
		internal int _record;
	}
}
