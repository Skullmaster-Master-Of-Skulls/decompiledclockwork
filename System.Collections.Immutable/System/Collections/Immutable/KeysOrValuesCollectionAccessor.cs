using System;
using System.Collections.Generic;
using System.Diagnostics;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000037 RID: 55
	internal abstract class KeysOrValuesCollectionAccessor<TKey, TValue, T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x0600035F RID: 863 RVA: 0x000092A0 File Offset: 0x000074A0
		protected KeysOrValuesCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary, IEnumerable<T> keysOrValues)
		{
			Requires.NotNull<IImmutableDictionary<TKey, TValue>>(dictionary, "dictionary");
			Requires.NotNull<IEnumerable<T>>(keysOrValues, "keysOrValues");
			this._dictionary = dictionary;
			this._keysOrValues = keysOrValues;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000038D6 File Offset: 0x00001AD6
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000361 RID: 865 RVA: 0x000092CF File Offset: 0x000074CF
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000362 RID: 866 RVA: 0x000092DC File Offset: 0x000074DC
		protected IImmutableDictionary<TKey, TValue> Dictionary
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00002D65 File Offset: 0x00000F65
		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00002D65 File Offset: 0x00000F65
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000365 RID: 869
		public abstract bool Contains(T item);

		// Token: 0x06000366 RID: 870 RVA: 0x000092F4 File Offset: 0x000074F4
		public void CopyTo(T[] array, int arrayIndex)
		{
			Requires.NotNull<T[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00002D65 File Offset: 0x00000F65
		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00009383 File Offset: 0x00007583
		public IEnumerator<T> GetEnumerator()
		{
			return this._keysOrValues.GetEnumerator();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00009390 File Offset: 0x00007590
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00009398 File Offset: 0x00007598
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Requires.NotNull<Array>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array.SetValue(t, new int[]
				{
					arrayIndex++
				});
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600036B RID: 875 RVA: 0x000038D6 File Offset: 0x00001AD6
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600036C RID: 876 RVA: 0x000052C4 File Offset: 0x000034C4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000045 RID: 69
		private readonly IImmutableDictionary<TKey, TValue> _dictionary;

		// Token: 0x04000046 RID: 70
		private readonly IEnumerable<T> _keysOrValues;
	}
}
