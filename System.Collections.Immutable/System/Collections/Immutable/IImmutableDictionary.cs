using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x0200000E RID: 14
	public interface IImmutableDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
	{
		// Token: 0x0600005B RID: 91
		IImmutableDictionary<TKey, TValue> Clear();

		// Token: 0x0600005C RID: 92
		IImmutableDictionary<TKey, TValue> Add(TKey key, TValue value);

		// Token: 0x0600005D RID: 93
		IImmutableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs);

		// Token: 0x0600005E RID: 94
		IImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value);

		// Token: 0x0600005F RID: 95
		IImmutableDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items);

		// Token: 0x06000060 RID: 96
		IImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);

		// Token: 0x06000061 RID: 97
		IImmutableDictionary<TKey, TValue> Remove(TKey key);

		// Token: 0x06000062 RID: 98
		bool Contains(KeyValuePair<TKey, TValue> pair);

		// Token: 0x06000063 RID: 99
		bool TryGetKey(TKey equalKey, out TKey actualKey);
	}
}
