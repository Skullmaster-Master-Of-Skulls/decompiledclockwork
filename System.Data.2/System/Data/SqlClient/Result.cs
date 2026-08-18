using System;
using System.Collections;

namespace System.Data.SqlClient
{
	// Token: 0x020001A7 RID: 423
	internal sealed class Result
	{
		// Token: 0x060018A2 RID: 6306 RVA: 0x000ADB5C File Offset: 0x000ACF5C
		internal Result(_SqlMetaDataSet metadata)
		{
			this._metadata = metadata;
			this._rowset = new ArrayList();
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x000ADB84 File Offset: 0x000ACF84
		internal int Count
		{
			get
			{
				return this._rowset.Count;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x000ADB9C File Offset: 0x000ACF9C
		internal _SqlMetaDataSet MetaData
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x17000378 RID: 888
		internal Row this[int index]
		{
			get
			{
				return (Row)this._rowset[index];
			}
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x000ADBD0 File Offset: 0x000ACFD0
		internal void AddRow(Row row)
		{
			this._rowset.Add(row);
		}

		// Token: 0x04000EB3 RID: 3763
		private _SqlMetaDataSet _metadata;

		// Token: 0x04000EB4 RID: 3764
		private ArrayList _rowset;
	}
}
