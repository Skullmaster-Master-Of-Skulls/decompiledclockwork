using System;
using System.Collections;

namespace System.Data.SqlClient
{
	// Token: 0x020002B2 RID: 690
	internal sealed class Result
	{
		// Token: 0x060022FB RID: 8955 RVA: 0x0028E148 File Offset: 0x0028D548
		internal Result(_SqlMetaDataSet metadata)
		{
			this._metadata = metadata;
			this._rowset = new ArrayList();
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x0028E178 File Offset: 0x0028D578
		internal int Count
		{
			get
			{
				return this._rowset.Count;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x0028E198 File Offset: 0x0028D598
		internal _SqlMetaDataSet MetaData
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x17000531 RID: 1329
		internal Row this[int index]
		{
			get
			{
				return (Row)this._rowset[index];
			}
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x0028E1D8 File Offset: 0x0028D5D8
		internal void AddRow(Row row)
		{
			this._rowset.Add(row);
		}

		// Token: 0x040016C5 RID: 5829
		private _SqlMetaDataSet _metadata;

		// Token: 0x040016C6 RID: 5830
		private ArrayList _rowset;
	}
}
