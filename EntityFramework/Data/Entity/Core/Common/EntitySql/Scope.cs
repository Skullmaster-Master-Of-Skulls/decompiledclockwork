using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000267 RID: 615
	internal sealed class Scope : IEnumerable<KeyValuePair<string, ScopeEntry>>, IEnumerable
	{
		// Token: 0x06001501 RID: 5377 RVA: 0x00063123 File Offset: 0x00061323
		internal Scope(IEqualityComparer<string> keyComparer)
		{
			this._scopeEntries = new Dictionary<string, ScopeEntry>(keyComparer);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00063137 File Offset: 0x00061337
		internal Scope Add(string key, ScopeEntry value)
		{
			this._scopeEntries.Add(key, value);
			return this;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00063147 File Offset: 0x00061347
		internal void Remove(string key)
		{
			this._scopeEntries.Remove(key);
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00063156 File Offset: 0x00061356
		internal void Replace(string key, ScopeEntry value)
		{
			this._scopeEntries[key] = value;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00063165 File Offset: 0x00061365
		internal bool Contains(string key)
		{
			return this._scopeEntries.ContainsKey(key);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00063173 File Offset: 0x00061373
		internal bool TryLookup(string key, out ScopeEntry value)
		{
			return this._scopeEntries.TryGetValue(key, out value);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00063182 File Offset: 0x00061382
		public Dictionary<string, ScopeEntry>.Enumerator GetEnumerator()
		{
			return this._scopeEntries.GetEnumerator();
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0006318F File Offset: 0x0006138F
		IEnumerator<KeyValuePair<string, ScopeEntry>> IEnumerable<KeyValuePair<string, ScopeEntry>>.GetEnumerator()
		{
			return this._scopeEntries.GetEnumerator();
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x000631A1 File Offset: 0x000613A1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._scopeEntries.GetEnumerator();
		}

		// Token: 0x0400074D RID: 1869
		private readonly Dictionary<string, ScopeEntry> _scopeEntries;
	}
}
