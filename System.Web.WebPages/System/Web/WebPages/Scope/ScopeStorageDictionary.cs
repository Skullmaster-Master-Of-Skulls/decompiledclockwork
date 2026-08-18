using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.WebPages.Scope
{
	// Token: 0x02000073 RID: 115
	public class ScopeStorageDictionary : IDictionary<object, object>, ICollection<KeyValuePair<object, object>>, IEnumerable<KeyValuePair<object, object>>, IEnumerable
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0000C009 File Offset: 0x0000A209
		public ScopeStorageDictionary() : this(null)
		{
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000C012 File Offset: 0x0000A212
		public ScopeStorageDictionary(IDictionary<object, object> baseScope) : this(baseScope, new Dictionary<object, object>(ScopeStorageComparer.Instance))
		{
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000C025 File Offset: 0x0000A225
		internal ScopeStorageDictionary(IDictionary<object, object> baseScope, IDictionary<object, object> backingStore)
		{
			this._baseScope = baseScope;
			this._backingStore = backingStore;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000C03B File Offset: 0x0000A23B
		protected IDictionary<object, object> BackingStore
		{
			get
			{
				return this._backingStore;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000C043 File Offset: 0x0000A243
		protected IDictionary<object, object> BaseScope
		{
			get
			{
				return this._baseScope;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000C054 File Offset: 0x0000A254
		public virtual ICollection<object> Keys
		{
			get
			{
				return (from item in this.GetItems()
				select item.Key).ToList<object>();
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000C08C File Offset: 0x0000A28C
		public virtual ICollection<object> Values
		{
			get
			{
				return (from item in this.GetItems()
				select item.Value).ToList<object>();
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000C0BB File Offset: 0x0000A2BB
		public virtual int Count
		{
			get
			{
				return this.GetItems().Count<KeyValuePair<object, object>>();
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B0 RID: 176
		public object this[object key]
		{
			get
			{
				object result;
				this.TryGetValue(key, out result);
				return result;
			}
			set
			{
				this.SetValue(key, value);
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000C0EE File Offset: 0x0000A2EE
		public virtual void SetValue(object key, object value)
		{
			this._backingStore[key] = value;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000C0FD File Offset: 0x0000A2FD
		public virtual bool TryGetValue(object key, out object value)
		{
			return this._backingStore.TryGetValue(key, out value) || (this._baseScope != null && this._baseScope.TryGetValue(key, out value));
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000C127 File Offset: 0x0000A327
		public virtual bool Remove(object key)
		{
			return this._backingStore.Remove(key);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000C135 File Offset: 0x0000A335
		public virtual IEnumerator<KeyValuePair<object, object>> GetEnumerator()
		{
			return this.GetItems().GetEnumerator();
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000C142 File Offset: 0x0000A342
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000C14A File Offset: 0x0000A34A
		public virtual void Add(object key, object value)
		{
			this.SetValue(key, value);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000C154 File Offset: 0x0000A354
		public virtual bool ContainsKey(object key)
		{
			return this._backingStore.ContainsKey(key) || (this._baseScope != null && this._baseScope.ContainsKey(key));
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000C17C File Offset: 0x0000A37C
		public virtual void Add(KeyValuePair<object, object> item)
		{
			this.SetValue(item.Key, item.Value);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000C192 File Offset: 0x0000A392
		public virtual void Clear()
		{
			this._backingStore.Clear();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000C19F File Offset: 0x0000A39F
		public virtual bool Contains(KeyValuePair<object, object> item)
		{
			return this._backingStore.Contains(item) || (this._baseScope != null && this._baseScope.Contains(item));
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000C1C7 File Offset: 0x0000A3C7
		public virtual void CopyTo(KeyValuePair<object, object>[] array, int arrayIndex)
		{
			this.GetItems().ToList<KeyValuePair<object, object>>().CopyTo(array, arrayIndex);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000C1DB File Offset: 0x0000A3DB
		public virtual bool Remove(KeyValuePair<object, object> item)
		{
			return this._backingStore.Remove(item);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000C1EE File Offset: 0x0000A3EE
		protected virtual IEnumerable<KeyValuePair<object, object>> GetItems()
		{
			if (this._baseScope == null)
			{
				return this._backingStore;
			}
			return this._backingStore.Concat(this._baseScope).Distinct(ScopeStorageDictionary._keyValueComparer);
		}

		// Token: 0x04000102 RID: 258
		private static readonly ScopeStorageDictionary.StateStorageKeyValueComparer _keyValueComparer = new ScopeStorageDictionary.StateStorageKeyValueComparer();

		// Token: 0x04000103 RID: 259
		private readonly IDictionary<object, object> _baseScope;

		// Token: 0x04000104 RID: 260
		private readonly IDictionary<object, object> _backingStore;

		// Token: 0x02000074 RID: 116
		private class StateStorageKeyValueComparer : IEqualityComparer<KeyValuePair<object, object>>
		{
			// Token: 0x06000385 RID: 901 RVA: 0x0000C226 File Offset: 0x0000A426
			public bool Equals(KeyValuePair<object, object> x, KeyValuePair<object, object> y)
			{
				return this._stateStorageComparer.Equals(x.Key, y.Key);
			}

			// Token: 0x06000386 RID: 902 RVA: 0x0000C241 File Offset: 0x0000A441
			public int GetHashCode(KeyValuePair<object, object> obj)
			{
				return this._stateStorageComparer.GetHashCode(obj.Key);
			}

			// Token: 0x04000107 RID: 263
			private IEqualityComparer<object> _stateStorageComparer = ScopeStorageComparer.Instance;
		}
	}
}
