using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Web.Util
{
	// Token: 0x02000035 RID: 53
	internal class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x06000205 RID: 517 RVA: 0x0000D171 File Offset: 0x0000B371
		public OrderedDictionary() : this(0)
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000D17A File Offset: 0x0000B37A
		public OrderedDictionary(int capacity)
		{
			this._dictionary = new Dictionary<TKey, TValue>(capacity);
			this._keys = new List<TKey>(capacity);
			this._values = new List<TValue>(capacity);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000D1A6 File Offset: 0x0000B3A6
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000D1B3 File Offset: 0x0000B3B3
		public ICollection<TKey> Keys
		{
			get
			{
				return this._keys.AsReadOnly();
			}
		}

		// Token: 0x17000092 RID: 146
		public TValue this[TKey key]
		{
			get
			{
				return this._dictionary[key];
			}
			set
			{
				this.RemoveFromLists(key);
				this._dictionary[key] = value;
				this._keys.Add(key);
				this._values.Add(value);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000D1FC File Offset: 0x0000B3FC
		public ICollection<TValue> Values
		{
			get
			{
				return this._values.AsReadOnly();
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000D209 File Offset: 0x0000B409
		public void Add(TKey key, TValue value)
		{
			this._dictionary.Add(key, value);
			this._keys.Add(key);
			this._values.Add(value);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000D230 File Offset: 0x0000B430
		public void Clear()
		{
			this._dictionary.Clear();
			this._keys.Clear();
			this._values.Clear();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000D253 File Offset: 0x0000B453
		public bool ContainsKey(TKey key)
		{
			return this._dictionary.ContainsKey(key);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000D261 File Offset: 0x0000B461
		public bool ContainsValue(TValue value)
		{
			return this._dictionary.ContainsValue(value);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000D26F File Offset: 0x0000B46F
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			int i = 0;
			foreach (TKey key in this._keys)
			{
				yield return new KeyValuePair<TKey, TValue>(key, this._values[i]);
				int num = i;
				i = num + 1;
			}
			List<TKey>.Enumerator enumerator = default(List<TKey>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000D280 File Offset: 0x0000B480
		private void RemoveFromLists(TKey key)
		{
			int num = this._keys.IndexOf(key);
			if (num != -1)
			{
				this._keys.RemoveAt(num);
				this._values.RemoveAt(num);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000D2B6 File Offset: 0x0000B4B6
		public bool Remove(TKey key)
		{
			this.RemoveFromLists(key);
			return this._dictionary.Remove(key);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000D2CB File Offset: 0x0000B4CB
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this._dictionary.TryGetValue(key, out value);
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000D2DA File Offset: 0x0000B4DA
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<TKey, TValue>>)this._dictionary).IsReadOnly;
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000D2E7 File Offset: 0x0000B4E7
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000D2FD File Offset: 0x0000B4FD
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)this._dictionary).Contains(item);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000D30B File Offset: 0x0000B50B
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<TKey, TValue>>)this._dictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000D31C File Offset: 0x0000B51C
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			bool flag = ((ICollection<KeyValuePair<TKey, TValue>>)this._dictionary).Remove(item);
			if (flag)
			{
				this.RemoveFromLists(item.Key);
			}
			return flag;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000D347 File Offset: 0x0000B547
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040000D6 RID: 214
		private Dictionary<TKey, TValue> _dictionary;

		// Token: 0x040000D7 RID: 215
		private List<TKey> _keys;

		// Token: 0x040000D8 RID: 216
		private List<TValue> _values;
	}
}
