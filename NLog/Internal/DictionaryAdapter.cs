using System;
using System.Collections;
using System.Collections.Generic;

namespace NLog.Internal
{
	// Token: 0x0200007A RID: 122
	internal class DictionaryAdapter<TKey, TValue> : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x000091A4 File Offset: 0x000073A4
		public DictionaryAdapter(IDictionary<TKey, TValue> implementation)
		{
			this.implementation = implementation;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x000091B3 File Offset: 0x000073B3
		public ICollection Values
		{
			get
			{
				return new List<TValue>(this.implementation.Values);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x000091C5 File Offset: 0x000073C5
		public int Count
		{
			get
			{
				return this.implementation.Count;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x000091D2 File Offset: 0x000073D2
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x000091D5 File Offset: 0x000073D5
		public object SyncRoot
		{
			get
			{
				return this.implementation;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000091DD File Offset: 0x000073DD
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x000091E0 File Offset: 0x000073E0
		public bool IsReadOnly
		{
			get
			{
				return this.implementation.IsReadOnly;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x000091ED File Offset: 0x000073ED
		public ICollection Keys
		{
			get
			{
				return new List<TKey>(this.implementation.Keys);
			}
		}

		// Token: 0x1700007A RID: 122
		public object this[object key]
		{
			get
			{
				TValue tvalue;
				if (this.implementation.TryGetValue((TKey)((object)key), out tvalue))
				{
					return tvalue;
				}
				return null;
			}
			set
			{
				this.implementation[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00009243 File Offset: 0x00007443
		public void Add(object key, object value)
		{
			this.implementation.Add((TKey)((object)key), (TValue)((object)value));
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000925C File Offset: 0x0000745C
		public void Clear()
		{
			this.implementation.Clear();
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00009269 File Offset: 0x00007469
		public bool Contains(object key)
		{
			return this.implementation.ContainsKey((TKey)((object)key));
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000927C File Offset: 0x0000747C
		public IDictionaryEnumerator GetEnumerator()
		{
			return new DictionaryAdapter<TKey, TValue>.MyEnumerator(this.implementation.GetEnumerator());
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000928E File Offset: 0x0000748E
		public void Remove(object key)
		{
			this.implementation.Remove((TKey)((object)key));
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000092A2 File Offset: 0x000074A2
		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000092A9 File Offset: 0x000074A9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040000D2 RID: 210
		private readonly IDictionary<TKey, TValue> implementation;

		// Token: 0x0200007B RID: 123
		private class MyEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x0600041E RID: 1054 RVA: 0x000092B1 File Offset: 0x000074B1
			public MyEnumerator(IEnumerator<KeyValuePair<TKey, TValue>> wrapped)
			{
				this.wrapped = wrapped;
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x0600041F RID: 1055 RVA: 0x000092C0 File Offset: 0x000074C0
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.wrapped.Current;
					object key = keyValuePair.Key;
					KeyValuePair<TKey, TValue> keyValuePair2 = this.wrapped.Current;
					return new DictionaryEntry(key, keyValuePair2.Value);
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x06000420 RID: 1056 RVA: 0x00009304 File Offset: 0x00007504
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.wrapped.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000932C File Offset: 0x0000752C
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.wrapped.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x06000422 RID: 1058 RVA: 0x00009351 File Offset: 0x00007551
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x0000935E File Offset: 0x0000755E
			public bool MoveNext()
			{
				return this.wrapped.MoveNext();
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x0000936B File Offset: 0x0000756B
			public void Reset()
			{
				this.wrapped.Reset();
			}

			// Token: 0x040000D3 RID: 211
			private IEnumerator<KeyValuePair<TKey, TValue>> wrapped;
		}
	}
}
