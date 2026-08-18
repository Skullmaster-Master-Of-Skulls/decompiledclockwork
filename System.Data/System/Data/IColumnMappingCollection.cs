using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x020000B8 RID: 184
	public interface IColumnMappingCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x170001C6 RID: 454
		object this[string index]
		{
			get;
			set;
		}

		// Token: 0x06000C52 RID: 3154
		IColumnMapping Add(string sourceColumnName, string dataSetColumnName);

		// Token: 0x06000C53 RID: 3155
		bool Contains(string sourceColumnName);

		// Token: 0x06000C54 RID: 3156
		IColumnMapping GetByDataSetColumn(string dataSetColumnName);

		// Token: 0x06000C55 RID: 3157
		int IndexOf(string sourceColumnName);

		// Token: 0x06000C56 RID: 3158
		void RemoveAt(string sourceColumnName);
	}
}
