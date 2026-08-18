using System;

namespace System.Data
{
	// Token: 0x020000BE RID: 190
	public sealed class DataRowBuilder
	{
		// Token: 0x06000B4F RID: 2895 RVA: 0x00062D10 File Offset: 0x00062110
		internal DataRowBuilder(DataTable table, int record)
		{
			this._table = table;
			this._record = record;
		}

		// Token: 0x04000358 RID: 856
		internal readonly DataTable _table;

		// Token: 0x04000359 RID: 857
		internal int _record;
	}
}
