using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200032A RID: 810
	internal sealed class _SqlMetaDataSet
	{
		// Token: 0x06002A65 RID: 10853 RVA: 0x002BE8D8 File Offset: 0x002BDCD8
		internal _SqlMetaDataSet(int count)
		{
			this.metaDataArray = new _SqlMetaData[count];
			for (int i = 0; i < this.metaDataArray.Length; i++)
			{
				this.metaDataArray[i] = new _SqlMetaData(i);
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002A66 RID: 10854 RVA: 0x002BE918 File Offset: 0x002BDD18
		internal int Length
		{
			get
			{
				return this.metaDataArray.Length;
			}
		}

		// Token: 0x170006ED RID: 1773
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

		// Token: 0x04001BE3 RID: 7139
		internal ushort id;

		// Token: 0x04001BE4 RID: 7140
		internal int[] indexMap;

		// Token: 0x04001BE5 RID: 7141
		internal int visibleColumns;

		// Token: 0x04001BE6 RID: 7142
		internal DataTable schemaTable;

		// Token: 0x04001BE7 RID: 7143
		private readonly _SqlMetaData[] metaDataArray;
	}
}
