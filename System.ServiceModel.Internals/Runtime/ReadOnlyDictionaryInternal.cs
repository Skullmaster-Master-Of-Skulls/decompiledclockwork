using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	internal class ReadOnlyDictionaryInternal<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public ReadOnlyDictionaryInternal(IDictionary<TKey, TValue> dictionary)
		{
			this.dictionary = dictionary;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005BE3 File Offset: 0x00003DE3
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00002940 File Offset: 0x00000B40
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005BF0 File Offset: 0x00003DF0
		public ICollection<TKey> Keys
		{
			get
			{
				return this.dictionary.Keys;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005BFD File Offset: 0x00003DFD
		public ICollection<TValue> Values
		{
			get
			{
				return this.dictionary.Values;
			}
		}

		// Token: 0x1700002E RID: 46
		public TValue this[TKey key]
		{
			get
			{
				return this.dictionary[key];
			}
			set
			{
				throw Fx.Exception.AsError(this.CreateReadOnlyException());
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005C2A File Offset: 0x00003E2A
		public static IDictionary<TKey, TValue> Create(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary.IsReadOnly)
			{
				return dictionary;
			}
			return new ReadOnlyDictionaryInternal<TKey, TValue>(dictionary);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005C3C File Offset: 0x00003E3C
		private Exception CreateReadOnlyException()
		{
			return new InvalidOperationException(InternalSR.DictionaryIsReadOnly);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005C18 File Offset: 0x00003E18
		public void Add(TKey key, TValue value)
		{
			throw Fx.Exception.AsError(this.CreateReadOnlyException());
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005C18 File Offset: 0x00003E18
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			throw Fx.Exception.AsError(this.CreateReadOnlyException());
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005C18 File Offset: 0x00003E18
		public void Clear()
		{
			throw Fx.Exception.AsError(this.CreateReadOnlyException());
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005C48 File Offset: 0x00003E48
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.dictionary.Contains(item);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005C56 File Offset: 0x00003E56
		public bool ContainsKey(TKey key)
		{
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005C64 File Offset: 0x00003E64
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005C73 File Offset: 0x00003E73
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005C80 File Offset: 0x00003E80
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005C18 File Offset: 0x00003E18
		public bool Remove(TKey key)
		{
			throw Fx.Exception.AsError(this.CreateReadOnlyException());
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005C18 File Offset: 0x00003E18
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			throw Fx.Exception.AsError(this.CreateReadOnlyException());
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005C88 File Offset: 0x00003E88
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.dictionary.TryGetValue(key, out value);
		}

		// Token: 0x04000095 RID: 149
		private IDictionary<TKey, TValue> dictionary;
	}
}
