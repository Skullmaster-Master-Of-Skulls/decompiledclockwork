using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x020000FF RID: 255
	public interface IColumnMappingCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x17000262 RID: 610
		object this[string index]
		{
			get;
			set;
		}

		// Token: 0x0600105D RID: 4189
		IColumnMapping Add(string sourceColumnName, string dataSetColumnName);

		// Token: 0x0600105E RID: 4190
		bool Contains(string sourceColumnName);

		// Token: 0x0600105F RID: 4191
		IColumnMapping GetByDataSetColumn(string dataSetColumnName);

		// Token: 0x06001060 RID: 4192
		int IndexOf(string sourceColumnName);

		// Token: 0x06001061 RID: 4193
		void RemoveAt(string sourceColumnName);
	}
}
