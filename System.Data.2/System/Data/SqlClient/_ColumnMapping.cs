using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001A5 RID: 421
	internal sealed class _ColumnMapping
	{
		// Token: 0x0600189E RID: 6302 RVA: 0x000ADAEC File Offset: 0x000ACEEC
		internal _ColumnMapping(int columnId, _SqlMetaData metadata)
		{
			this._sourceColumnOrdinal = columnId;
			this._metadata = metadata;
		}

		// Token: 0x04000EB0 RID: 3760
		internal int _sourceColumnOrdinal;

		// Token: 0x04000EB1 RID: 3761
		internal _SqlMetaData _metadata;
	}
}
