using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000220 RID: 544
	internal sealed class _SqlMetaDataSet : ICloneable
	{
		// Token: 0x06002209 RID: 8713 RVA: 0x000EC87C File Offset: 0x000EBC7C
		internal _SqlMetaDataSet(int count, SqlTceCipherInfoTable? cipherTable)
		{
			this.cekTable = cipherTable;
			this.metaDataArray = new _SqlMetaData[count];
			for (int i = 0; i < this.metaDataArray.Length; i++)
			{
				this.metaDataArray[i] = new _SqlMetaData(i);
			}
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000EC8C4 File Offset: 0x000EBCC4
		private _SqlMetaDataSet(_SqlMetaDataSet original)
		{
			this.id = original.id;
			this.indexMap = original.indexMap;
			this.visibleColumns = original.visibleColumns;
			this.schemaTable = original.schemaTable;
			if (original.metaDataArray == null)
			{
				this.metaDataArray = null;
				return;
			}
			this.metaDataArray = new _SqlMetaData[original.metaDataArray.Length];
			for (int i = 0; i < this.metaDataArray.Length; i++)
			{
				this.metaDataArray[i] = (_SqlMetaData)original.metaDataArray[i].Clone();
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x000EC958 File Offset: 0x000EBD58
		internal int Length
		{
			get
			{
				return this.metaDataArray.Length;
			}
		}

		// Token: 0x17000571 RID: 1393
		internal _SqlMetaData this[int index]
		{
			get
			{
				return this.metaDataArray[index];
			}
			set
			{
				this.metaDataArray[index] = value;
			}
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000EC9A0 File Offset: 0x000EBDA0
		public object Clone()
		{
			return new _SqlMetaDataSet(this);
		}

		// Token: 0x04001469 RID: 5225
		internal ushort id;

		// Token: 0x0400146A RID: 5226
		internal int[] indexMap;

		// Token: 0x0400146B RID: 5227
		internal int visibleColumns;

		// Token: 0x0400146C RID: 5228
		internal DataTable schemaTable;

		// Token: 0x0400146D RID: 5229
		internal readonly SqlTceCipherInfoTable? cekTable;

		// Token: 0x0400146E RID: 5230
		internal readonly _SqlMetaData[] metaDataArray;
	}
}
