using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace System.Runtime.Collections
{
	// Token: 0x02000054 RID: 84
	internal class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection
	{
		// Token: 0x06000347 RID: 839 RVA: 0x000111A0 File Offset: 0x0000F3A0
		public OrderedDictionary()
		{
			this.privateDictionary = new OrderedDictionary();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000111B4 File Offset: 0x0000F3B4
		public OrderedDictionary(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary != null)
			{
				this.privateDictionary = new OrderedDictionary();
				foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
				{
					this.privateDictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0001122C File Offset: 0x0000F42C
		public int Count
		{
			get
			{
				return this.privateDictionary.Count;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600034A RID: 842 RVA: 0x000031F5 File Offset: 0x000013F5
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000087 RID: 135
		public TValue this[TKey key]
		{
			get
			{
				if (key == null)
				{
					throw Fx.Exception.ArgumentNull("key");
				}
				if (this.privateDictionary.Contains(key))
				{
					return (TValue)((object)this.privateDictionary[key]);
				}
				throw Fx.Exception.AsError(new KeyNotFoundException(InternalSR.KeyNotFoundInDictionary));
			}
			set
			{
				if (key == null)
				{
					throw Fx.Exception.ArgumentNull("key");
				}
				this.privateDictionary[key] = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000112D0 File Offset: 0x0000F4D0
		public ICollection<TKey> Keys
		{
			get
			{
				List<TKey> list = new List<TKey>(this.privateDictionary.Count);
				foreach (object obj in this.privateDictionary.Keys)
				{
					TKey item = (TKey)((object)obj);
					list.Add(item);
				}
				return list;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00011340 File Offset: 0x0000F540
		public ICollection<TValue> Values
		{
			get
			{
				List<TValue> list = new List<TValue>(this.privateDictionary.Count);
				foreach (object obj in this.privateDictionary.Values)
				{
					TValue item = (TValue)((object)obj);
					list.Add(item);
				}
				return list;
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000113B0 File Offset: 0x0000F5B0
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000113C6 File Offset: 0x0000F5C6
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw Fx.Exception.ArgumentNull("key");
			}
			this.privateDictionary.Add(key, value);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000113F7 File Offset: 0x0000F5F7
		public void Clear()
		{
			this.privateDictionary.Clear();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00011404 File Offset: 0x0000F604
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return item.Key != null && this.privateDictionary.Contains(item.Key) && this.privateDictionary[item.Key].Equals(item.Value);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00011462 File Offset: 0x0000F662
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw Fx.Exception.ArgumentNull("key");
			}
			return this.privateDictionary.Contains(key);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00011490 File Offset: 0x0000F690
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw Fx.Exception.ArgumentNull("array");
			}
			if (arrayIndex < 0)
			{
				throw Fx.Exception.AsError(new ArgumentOutOfRangeException("arrayIndex"));
			}
			if (array.Rank > 1 || arrayIndex >= array.Length || array.Length - arrayIndex < this.privateDictionary.Count)
			{
				throw Fx.Exception.Argument("array", InternalSR.BadCopyToArray);
			}
			int num = arrayIndex;
			foreach (object obj in this.privateDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				array[num] = new KeyValuePair<TKey, TValue>((TKey)((object)dictionaryEntry.Key), (TValue)((object)dictionaryEntry.Value));
				num++;
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00011570 File Offset: 0x0000F770
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			foreach (object obj in this.privateDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				yield return new KeyValuePair<TKey, TValue>((TKey)((object)dictionaryEntry.Key), (TValue)((object)dictionaryEntry.Value));
			}
			IDictionaryEnumerator dictionaryEnumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001157F File Offset: 0x0000F77F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00011587 File Offset: 0x0000F787
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			if (this.Contains(item))
			{
				this.privateDictionary.Remove(item.Key);
				return true;
			}
			return false;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000115AC File Offset: 0x0000F7AC
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw Fx.Exception.ArgumentNull("key");
			}
			if (this.privateDictionary.Contains(key))
			{
				this.privateDictionary.Remove(key);
				return true;
			}
			return false;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000115F8 File Offset: 0x0000F7F8
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw Fx.Exception.ArgumentNull("key");
			}
			bool flag = this.privateDictionary.Contains(key);
			value = (flag ? ((TValue)((object)this.privateDictionary[key])) : default(TValue));
			return flag;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001165A File Offset: 0x0000F85A
		void IDictionary.Add(object key, object value)
		{
			this.privateDictionary.Add(key, value);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000113F7 File Offset: 0x0000F5F7
		void IDictionary.Clear()
		{
			this.privateDictionary.Clear();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00011669 File Offset: 0x0000F869
		bool IDictionary.Contains(object key)
		{
			return this.privateDictionary.Contains(key);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00011677 File Offset: 0x0000F877
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return this.privateDictionary.GetEnumerator();
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00011684 File Offset: 0x0000F884
		bool IDictionary.IsFixedSize
		{
			get
			{
				return ((IDictionary)this.privateDictionary).IsFixedSize;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00011691 File Offset: 0x0000F891
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.privateDictionary.IsReadOnly;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0001169E File Offset: 0x0000F89E
		ICollection IDictionary.Keys
		{
			get
			{
				return this.privateDictionary.Keys;
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000116AB File Offset: 0x0000F8AB
		void IDictionary.Remove(object key)
		{
			this.privateDictionary.Remove(key);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000362 RID: 866 RVA: 0x000116B9 File Offset: 0x0000F8B9
		ICollection IDictionary.Values
		{
			get
			{
				return this.privateDictionary.Values;
			}
		}

		// Token: 0x1700008E RID: 142
		object IDictionary.this[object key]
		{
			get
			{
				return this.privateDictionary[key];
			}
			set
			{
				this.privateDictionary[key] = value;
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000116E3 File Offset: 0x0000F8E3
		void ICollection.CopyTo(Array array, int index)
		{
			this.privateDictionary.CopyTo(array, index);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0001122C File Offset: 0x0000F42C
		int ICollection.Count
		{
			get
			{
				return this.privateDictionary.Count;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000116F2 File Offset: 0x0000F8F2
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this.privateDictionary).IsSynchronized;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000368 RID: 872 RVA: 0x000116FF File Offset: 0x0000F8FF
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this.privateDictionary).SyncRoot;
			}
		}

		// Token: 0x040001C2 RID: 450
		private OrderedDictionary privateDictionary;
	}
}
