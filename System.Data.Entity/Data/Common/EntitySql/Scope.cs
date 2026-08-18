using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000343 RID: 835
	internal sealed class Scope : IEnumerable<KeyValuePair<string, ScopeEntry>>, IEnumerable
	{
		// Token: 0x0600315D RID: 12637 RVA: 0x000C2936 File Offset: 0x000C0B36
		internal Scope(IEqualityComparer<string> keyComparer)
		{
			this._scopeEntries = new Dictionary<string, ScopeEntry>(keyComparer);
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000C294A File Offset: 0x000C0B4A
		internal Scope Add(string key, ScopeEntry value)
		{
			this._scopeEntries.Add(key, value);
			return this;
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000C295A File Offset: 0x000C0B5A
		internal void Remove(string key)
		{
			this._scopeEntries.Remove(key);
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000C2969 File Offset: 0x000C0B69
		internal void Replace(string key, ScopeEntry value)
		{
			this._scopeEntries[key] = value;
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000C2978 File Offset: 0x000C0B78
		internal bool Contains(string key)
		{
			return this._scopeEntries.ContainsKey(key);
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000C2986 File Offset: 0x000C0B86
		internal bool TryLookup(string key, out ScopeEntry value)
		{
			return this._scopeEntries.TryGetValue(key, out value);
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000C2995 File Offset: 0x000C0B95
		public Dictionary<string, ScopeEntry>.Enumerator GetEnumerator()
		{
			return this._scopeEntries.GetEnumerator();
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000C29A2 File Offset: 0x000C0BA2
		IEnumerator<KeyValuePair<string, ScopeEntry>> IEnumerable<KeyValuePair<string, ScopeEntry>>.GetEnumerator()
		{
			return this._scopeEntries.GetEnumerator();
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000C29A2 File Offset: 0x000C0BA2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._scopeEntries.GetEnumerator();
		}

		// Token: 0x04001572 RID: 5490
		private readonly Dictionary<string, ScopeEntry> _scopeEntries;
	}
}
