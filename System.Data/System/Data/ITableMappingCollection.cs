using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x020000C3 RID: 195
	public interface ITableMappingCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x170001E9 RID: 489
		object this[string index]
		{
			get;
			set;
		}

		// Token: 0x06000CAA RID: 3242
		ITableMapping Add(string sourceTableName, string dataSetTableName);

		// Token: 0x06000CAB RID: 3243
		bool Contains(string sourceTableName);

		// Token: 0x06000CAC RID: 3244
		ITableMapping GetByDataSetTable(string dataSetTableName);

		// Token: 0x06000CAD RID: 3245
		int IndexOf(string sourceTableName);

		// Token: 0x06000CAE RID: 3246
		void RemoveAt(string sourceTableName);
	}
}
