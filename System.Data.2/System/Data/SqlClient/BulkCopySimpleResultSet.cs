using System;
using System.Collections;

namespace System.Data.SqlClient
{
	// Token: 0x020001A8 RID: 424
	internal sealed class BulkCopySimpleResultSet
	{
		// Token: 0x060018A7 RID: 6311 RVA: 0x000ADBEC File Offset: 0x000ACFEC
		internal BulkCopySimpleResultSet()
		{
			this._results = new ArrayList();
		}

		// Token: 0x17000379 RID: 889
		internal Result this[int idx]
		{
			get
			{
				return (Result)this._results[idx];
			}
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x000ADC2C File Offset: 0x000AD02C
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

		// Token: 0x060018AA RID: 6314 RVA: 0x000ADC90 File Offset: 0x000AD090
		internal int[] CreateIndexMap()
		{
			return this.indexmap;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x000ADCA4 File Offset: 0x000AD0A4
		internal object[] CreateRowBuffer()
		{
			Row row = new Row(this.resultSet.MetaData.Length);
			this.resultSet.AddRow(row);
			return row.DataFields;
		}

		// Token: 0x04000EB5 RID: 3765
		private ArrayList _results;

		// Token: 0x04000EB6 RID: 3766
		private Result resultSet;

		// Token: 0x04000EB7 RID: 3767
		private int[] indexmap;
	}
}
