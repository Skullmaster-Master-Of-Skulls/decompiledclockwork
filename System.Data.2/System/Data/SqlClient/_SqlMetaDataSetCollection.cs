using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x02000221 RID: 545
	internal sealed class _SqlMetaDataSetCollection : ICloneable
	{
		// Token: 0x0600220F RID: 8719 RVA: 0x000EC9B4 File Offset: 0x000EBDB4
		internal _SqlMetaDataSetCollection()
		{
			this.altMetaDataSetArray = new List<_SqlMetaDataSet>();
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000EC9D4 File Offset: 0x000EBDD4
		internal void SetAltMetaData(_SqlMetaDataSet altMetaDataSet)
		{
			int id = (int)altMetaDataSet.id;
			for (int i = 0; i < this.altMetaDataSetArray.Count; i++)
			{
				if ((int)this.altMetaDataSetArray[i].id == id)
				{
					this.altMetaDataSetArray[i] = altMetaDataSet;
					return;
				}
			}
			this.altMetaDataSetArray.Add(altMetaDataSet);
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000ECA2C File Offset: 0x000EBE2C
		internal _SqlMetaDataSet GetAltMetaData(int id)
		{
			foreach (_SqlMetaDataSet sqlMetaDataSet in this.altMetaDataSetArray)
			{
				if ((int)sqlMetaDataSet.id == id)
				{
					return sqlMetaDataSet;
				}
			}
			return null;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000ECA94 File Offset: 0x000EBE94
		public object Clone()
		{
			_SqlMetaDataSetCollection sqlMetaDataSetCollection = new _SqlMetaDataSetCollection();
			sqlMetaDataSetCollection.metaDataSet = ((this.metaDataSet == null) ? null : ((_SqlMetaDataSet)this.metaDataSet.Clone()));
			foreach (_SqlMetaDataSet sqlMetaDataSet in this.altMetaDataSetArray)
			{
				sqlMetaDataSetCollection.altMetaDataSetArray.Add((_SqlMetaDataSet)sqlMetaDataSet.Clone());
			}
			return sqlMetaDataSetCollection;
		}

		// Token: 0x0400146F RID: 5231
		private readonly List<_SqlMetaDataSet> altMetaDataSetArray;

		// Token: 0x04001470 RID: 5232
		internal _SqlMetaDataSet metaDataSet;
	}
}
