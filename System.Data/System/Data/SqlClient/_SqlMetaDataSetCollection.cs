using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x0200032B RID: 811
	internal sealed class _SqlMetaDataSetCollection
	{
		// Token: 0x06002A69 RID: 10857 RVA: 0x002BE978 File Offset: 0x002BDD78
		internal _SqlMetaDataSetCollection()
		{
			this.altMetaDataSetArray = new List<_SqlMetaDataSet>();
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x002BE998 File Offset: 0x002BDD98
		internal void Add(_SqlMetaDataSet altMetaDataSet)
		{
			this.altMetaDataSetArray.Add(altMetaDataSet);
		}

		// Token: 0x170006EE RID: 1774
		internal _SqlMetaDataSet this[int id]
		{
			get
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
		}

		// Token: 0x04001BE8 RID: 7144
		private readonly List<_SqlMetaDataSet> altMetaDataSetArray;

		// Token: 0x04001BE9 RID: 7145
		internal _SqlMetaDataSet metaDataSet;
	}
}
