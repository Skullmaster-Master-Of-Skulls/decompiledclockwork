using System;
using System.Collections;

namespace System.Data.SqlClient
{
	// Token: 0x020002B3 RID: 691
	internal sealed class BulkCopySimpleResultSet
	{
		// Token: 0x06002300 RID: 8960 RVA: 0x0028E1F8 File Offset: 0x0028D5F8
		internal BulkCopySimpleResultSet()
		{
			this._results = new ArrayList();
		}

		// Token: 0x17000532 RID: 1330
		internal Result this[int idx]
		{
			get
			{
				return (Result)this._results[idx];
			}
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x0028E238 File Offset: 0x0028D638
		internal void SetMetaData(_SqlMetaDataSet metadata)
		{
			this.resultSet = new Result(metadata);
			this._results.Add(this.resultSet);
			this.indexmap = new int[this.resultSet.MetaData.Length];
			for (int i = 0; i < this.indexmap.Length; i++)
			{
				this.indexmap[i] = i;
			}
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x0028E2A8 File Offset: 0x0028D6A8
		internal int[] CreateIndexMap()
		{
			return this.indexmap;
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x0028E2C8 File Offset: 0x0028D6C8
		internal object[] CreateRowBuffer()
		{
			Row row = new Row(this.resultSet.MetaData.Length);
			this.resultSet.AddRow(row);
			return row.DataFields;
		}

		// Token: 0x040016C7 RID: 5831
		private ArrayList _results;

		// Token: 0x040016C8 RID: 5832
		private Result resultSet;

		// Token: 0x040016C9 RID: 5833
		private int[] indexmap;
	}
}
