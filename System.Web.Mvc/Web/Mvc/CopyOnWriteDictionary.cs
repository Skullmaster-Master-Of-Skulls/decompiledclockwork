using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x02000031 RID: 49
	internal class CopyOnWriteDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x000051B2 File Offset: 0x000033B2
		public CopyOnWriteDictionary(IDictionary<TKey, TValue> sourceDictionary, IEqualityComparer<TKey> comparer)
		{
			this._sourceDictionary = sourceDictionary;
			this._comparer = comparer;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000051C8 File Offset: 0x000033C8
		private IDictionary<TKey, TValue> ReadDictionary
		{
			get
			{
				return this._innerDictionary ?? this._sourceDictionary;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000051DA File Offset: 0x000033DA
		private IDictionary<TKey, TValue> WriteDictionary
		{
			get
			{
				if (this._innerDictionary == null)
				{
					this._innerDictionary = new Dictionary<TKey, TValue>(this._sourceDictionary, this._comparer);
				}
				return this._innerDictionary;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005201 File Offset: 0x00003401
		public virtual ICollection<TKey> Keys
		{
			get
			{
				return this.ReadDictionary.Keys;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000520E File Offset: 0x0000340E
		public virtual ICollection<TValue> Values
		{
			get
			{
				return this.ReadDictionary.Values;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000521B File Offset: 0x0000341B
		public virtual int Count
		{
			get
			{
				return this.ReadDictionary.Count;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005228 File Offset: 0x00003428
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000043 RID: 67
		public virtual TValue this[TKey key]
		{
			get
			{
				return this.ReadDictionary[key];
			}
			set
			{
				this.WriteDictionary[key] = value;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005248 File Offset: 0x00003448
		public virtual bool ContainsKey(TKey key)
		{
			return this.ReadDictionary.ContainsKey(key);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005256 File Offset: 0x00003456
		public virtual void Add(TKey key, TValue value)
		{
			this.WriteDictionary.Add(key, value);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005265 File Offset: 0x00003465
		public virtual bool Remove(TKey key)
		{
			return this.WriteDictionary.Remove(key);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005273 File Offset: 0x00003473
		public virtual bool TryGetValue(TKey key, out TValue value)
		{
			return this.ReadDictionary.TryGetValue(key, out value);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005282 File Offset: 0x00003482
		public virtual void Add(KeyValuePair<TKey, TValue> item)
		{
			this.WriteDictionary.Add(item);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005290 File Offset: 0x00003490
		public virtual void Clear()
		{
			this.WriteDictionary.Clear();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000529D File Offset: 0x0000349D
		public virtual bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.ReadDictionary.Contains(item);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000052AB File Offset: 0x000034AB
		public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.ReadDictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000052BA File Offset: 0x000034BA
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return this.WriteDictionary.Remove(item);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000052C8 File Offset: 0x000034C8
		public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.ReadDictionary.GetEnumerator();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000052D5 File Offset: 0x000034D5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400003F RID: 63
		private readonly IDictionary<TKey, TValue> _sourceDictionary;

		// Token: 0x04000040 RID: 64
		private readonly IEqualityComparer<TKey> _comparer;

		// Token: 0x04000041 RID: 65
		private IDictionary<TKey, TValue> _innerDictionary;
	}
}
