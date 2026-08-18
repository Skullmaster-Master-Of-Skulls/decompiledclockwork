using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000365 RID: 869
	internal interface ISchemaElementLookUpTable<T> where T : SchemaElement
	{
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001F15 RID: 7957
		int Count { get; }

		// Token: 0x06001F16 RID: 7958
		bool ContainsKey(string key);

		// Token: 0x170003B6 RID: 950
		T this[string key]
		{
			get;
		}

		// Token: 0x06001F18 RID: 7960
		IEnumerator<T> GetEnumerator();

		// Token: 0x06001F19 RID: 7961
		T LookUpEquivalentKey(string key);
	}
}
