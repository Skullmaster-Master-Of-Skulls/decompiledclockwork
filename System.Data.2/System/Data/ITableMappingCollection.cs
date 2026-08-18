using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x0200010C RID: 268
	public interface ITableMappingCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x1700028B RID: 651
		object this[string index]
		{
			get;
			set;
		}

		// Token: 0x060010D5 RID: 4309
		ITableMapping Add(string sourceTableName, string dataSetTableName);

		// Token: 0x060010D6 RID: 4310
		bool Contains(string sourceTableName);

		// Token: 0x060010D7 RID: 4311
		ITableMapping GetByDataSetTable(string dataSetTableName);

		// Token: 0x060010D8 RID: 4312
		int IndexOf(string sourceTableName);

		// Token: 0x060010D9 RID: 4313
		void RemoveAt(string sourceTableName);
	}
}
