using System;
using System.Collections.Generic;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002F5 RID: 757
	internal interface ISchemaElementLookUpTable<T> where T : SchemaElement
	{
		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002D2B RID: 11563
		int Count { get; }

		// Token: 0x06002D2C RID: 11564
		bool ContainsKey(string key);

		// Token: 0x170008C9 RID: 2249
		T this[string key]
		{
			get;
		}

		// Token: 0x06002D2E RID: 11566
		IEnumerator<T> GetEnumerator();

		// Token: 0x06002D2F RID: 11567
		T LookUpEquivalentKey(string key);
	}
}
