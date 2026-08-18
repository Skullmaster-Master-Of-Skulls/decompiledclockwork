using System;

namespace System.Data.SqlClient
{
	// Token: 0x020002B0 RID: 688
	internal sealed class _ColumnMapping
	{
		// Token: 0x060022F7 RID: 8951 RVA: 0x0028E0B8 File Offset: 0x0028D4B8
		internal _ColumnMapping(int columnId, _SqlMetaData metadata)
		{
			this._sourceColumnOrdinal = columnId;
			this._metadata = metadata;
		}

		// Token: 0x040016C2 RID: 5826
		internal int _sourceColumnOrdinal;

		// Token: 0x040016C3 RID: 5827
		internal _SqlMetaData _metadata;
	}
}
