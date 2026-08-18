using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Specialized
{
	// Token: 0x020003B4 RID: 948
	[Serializable]
	public class OrderedDictionary : IOrderedDictionary, IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback
	{
		// Token: 0x06002396 RID: 9110 RVA: 0x000A85A2 File Offset: 0x000A67A2
		public OrderedDictionary() : this(0)
		{
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000A85AB File Offset: 0x000A67AB
		public OrderedDictionary(int capacity) : this(capacity, null)
		{
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000A85B5 File Offset: 0x000A67B5
		public OrderedDictionary(IEqualityComparer comparer) : this(0, comparer)
		{
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000A85BF File Offset: 0x000A67BF
		public OrderedDictionary(int capacity, IEqualityComparer comparer)
		{
			this._initialCapacity = capacity;
			this._comparer = comparer;
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000A85D8 File Offset: 0x000A67D8
		private OrderedDictionary(OrderedDictionary dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._readOnly = true;
			this._objectsArray = dictionary._objectsArray;
			this._objectsTable = dictionary._objectsTable;
			this._comparer = dictionary._comparer;
			this._initialCapacity = dictionary._initialCapacity;
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000A8630 File Offset: 0x000A6830
		protected OrderedDictionary(SerializationInfo info, StreamingContext context)
		{
			this._siInfo = info;
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x000A863F File Offset: 0x000A683F
		public int Count
		{
			get
			{
				return this.objectsArray.Count;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x000A864C File Offset: 0x000A684C
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x0600239E RID: 9118 RVA: 0x000A8654 File Offset: 0x000A6854
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x000A865C File Offset: 0x000A685C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x000A865F File Offset: 0x000A685F
		public ICollection Keys
		{
			get
			{
				return new OrderedDictionary.OrderedDictionaryKeyValueCollection(this.objectsArray, true);
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x000A866D File Offset: 0x000A686D
		private ArrayList objectsArray
		{
			get
			{
				if (this._objectsArray == null)
				{
					this._objectsArray = new ArrayList(this._initialCapacity);
				}
				return this._objectsArray;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x000A868E File Offset: 0x000A688E
		private Hashtable objectsTable
		{
			get
			{
				if (this._objectsTable == null)
				{
					this._objectsTable = new Hashtable(this._initialCapacity, this._comparer);
				}
				return this._objectsTable;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x000A86B5 File Offset: 0x000A68B5
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x1700090A RID: 2314
		public object this[int index]
		{
			get
			{
				return ((DictionaryEntry)this.objectsArray[index]).Value;
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException(SR.GetString("OrderedDictionary_ReadOnly"));
				}
				if (index < 0 || index >= this.objectsArray.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				object key = ((DictionaryEntry)this.objectsArray[index]).Key;
				this.objectsArray[index] = new DictionaryEntry(key, value);
				this.objectsTable[key] = value;
			}
		}

		// Token: 0x1700090B RID: 2315
		public object this[object key]
		{
			get
			{
				return this.objectsTable[key];
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException(SR.GetString("OrderedDictionary_ReadOnly"));
				}
				if (this.objectsTable.Contains(key))
				{
					this.objectsTable[key] = value;
					this.objectsArray[this.IndexOfKey(key)] = new DictionaryEntry(key, value);
					return;
				}
				this.Add(key, value);
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x000A87F7 File Offset: 0x000A69F7
		public ICollection Values
		{
			get
			{
				return new OrderedDictionary.OrderedDictionaryKeyValueCollection(this.objectsArray, false);
			}
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000A8805 File Offset: 0x000A6A05
		public void Add(object key, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("OrderedDictionary_ReadOnly"));
			}
			this.objectsTable.Add(key, value);
			this.objectsArray.Add(new DictionaryEntry(key, value));
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000A8844 File Offset: 0x000A6A44
		public void Clear()
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("OrderedDictionary_ReadOnly"));
			}
			this.objectsTable.Clear();
			this.objectsArray.Clear();
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000A8874 File Offset: 0x000A6A74
		public OrderedDictionary AsReadOnly()
		{
			return new OrderedDictionary(this);
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000A887C File Offset: 0x000A6A7C
		public bool Contains(object key)
		{
			return this.objectsTable.Contains(key);
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000A888A File Offset: 0x000A6A8A
		public void CopyTo(Array array, int index)
		{
			this.objectsTable.CopyTo(array, index);
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000A889C File Offset: 0x000A6A9C
		private int IndexOfKey(object key)
		{
			for (int i = 0; i < this.objectsArray.Count; i++)
			{
				object key2 = ((DictionaryEntry)this.objectsArray[i]).Key;
				if (this._comparer != null)
				{
					if (this._comparer.Equals(key2, key))
					{
						return i;
					}
				}
				else if (key2.Equals(key))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000A8900 File Offset: 0x000A6B00
		public void Insert(int index, object key, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("OrderedDictionary_ReadOnly"));
			}
			if (index > this.Count || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.objectsTable.Add(key, value);
			this.objectsArray.Insert(index, new DictionaryEntry(key, value));
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000A8964 File Offset: 0x000A6B64
		protected virtual void OnDeserialization(object sender)
		{
			if (this._siInfo == null)
			{
				throw new SerializationException(SR.GetString("Serialization_InvalidOnDeser"));
			}
			this._comparer = (IEqualityComparer)this._siInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
			this._readOnly = this._siInfo.GetBoolean("ReadOnly");
			this._initialCapacity = this._siInfo.GetInt32("InitialCapacity");
			object[] array = (object[])this._siInfo.GetValue("ArrayList", typeof(object[]));
			if (array != null)
			{
				foreach (object obj in array)
				{
					DictionaryEntry dictionaryEntry;
					try
					{
						dictionaryEntry = (DictionaryEntry)obj;
					}
					catch
					{
						throw new SerializationException(SR.GetString("OrderedDictionary_SerializationMismatch"));
					}
					this.objectsArray.Add(dictionaryEntry);
					this.objectsTable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000A8A68 File Offset: 0x000A6C68
		public void RemoveAt(int index)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("OrderedDictionary_ReadOnly"));
			}
			if (index >= this.Count || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			object key = ((DictionaryEntry)this.objectsArray[index]).Key;
			this.objectsArray.RemoveAt(index);
			this.objectsTable.Remove(key);
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000A8AD8 File Offset: 0x000A6CD8
		public void Remove(object key)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("OrderedDictionary_ReadOnly"));
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.IndexOfKey(key);
			if (num < 0)
			{
				return;
			}
			this.objectsTable.Remove(key);
			this.objectsArray.RemoveAt(num);
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000A8B30 File Offset: 0x000A6D30
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new OrderedDictionary.OrderedDictionaryEnumerator(this.objectsArray, 3);
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000A8B3E File Offset: 0x000A6D3E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new OrderedDictionary.OrderedDictionaryEnumerator(this.objectsArray, 3);
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x000A8B4C File Offset: 0x000A6D4C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("KeyComparer", this._comparer, typeof(IEqualityComparer));
			info.AddValue("ReadOnly", this._readOnly);
			info.AddValue("InitialCapacity", this._initialCapacity);
			object[] array = new object[this.Count];
			this._objectsArray.CopyTo(array);
			info.AddValue("ArrayList", array);
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000A8BC8 File Offset: 0x000A6DC8
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialization(sender);
		}

		// Token: 0x04001FEA RID: 8170
		private ArrayList _objectsArray;

		// Token: 0x04001FEB RID: 8171
		private Hashtable _objectsTable;

		// Token: 0x04001FEC RID: 8172
		private int _initialCapacity;

		// Token: 0x04001FED RID: 8173
		private IEqualityComparer _comparer;

		// Token: 0x04001FEE RID: 8174
		private bool _readOnly;

		// Token: 0x04001FEF RID: 8175
		private object _syncRoot;

		// Token: 0x04001FF0 RID: 8176
		private SerializationInfo _siInfo;

		// Token: 0x04001FF1 RID: 8177
		private const string KeyComparerName = "KeyComparer";

		// Token: 0x04001FF2 RID: 8178
		private const string ArrayListName = "ArrayList";

		// Token: 0x04001FF3 RID: 8179
		private const string ReadOnlyName = "ReadOnly";

		// Token: 0x04001FF4 RID: 8180
		private const string InitCapacityName = "InitialCapacity";

		// Token: 0x020007EE RID: 2030
		private class OrderedDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06004409 RID: 17417 RVA: 0x0011E236 File Offset: 0x0011C436
			internal OrderedDictionaryEnumerator(ArrayList array, int objectReturnType)
			{
				this.arrayEnumerator = array.GetEnumerator();
				this._objectReturnType = objectReturnType;
			}

			// Token: 0x17000F69 RID: 3945
			// (get) Token: 0x0600440A RID: 17418 RVA: 0x0011E254 File Offset: 0x0011C454
			public object Current
			{
				get
				{
					if (this._objectReturnType == 1)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
						return dictionaryEntry.Key;
					}
					if (this._objectReturnType == 2)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
						return dictionaryEntry.Value;
					}
					return this.Entry;
				}
			}

			// Token: 0x17000F6A RID: 3946
			// (get) Token: 0x0600440B RID: 17419 RVA: 0x0011E2B0 File Offset: 0x0011C4B0
			public DictionaryEntry Entry
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
					object key = dictionaryEntry.Key;
					dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
					return new DictionaryEntry(key, dictionaryEntry.Value);
				}
			}

			// Token: 0x17000F6B RID: 3947
			// (get) Token: 0x0600440C RID: 17420 RVA: 0x0011E2F4 File Offset: 0x0011C4F4
			public object Key
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
					return dictionaryEntry.Key;
				}
			}

			// Token: 0x17000F6C RID: 3948
			// (get) Token: 0x0600440D RID: 17421 RVA: 0x0011E31C File Offset: 0x0011C51C
			public object Value
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
					return dictionaryEntry.Value;
				}
			}

			// Token: 0x0600440E RID: 17422 RVA: 0x0011E341 File Offset: 0x0011C541
			public bool MoveNext()
			{
				return this.arrayEnumerator.MoveNext();
			}

			// Token: 0x0600440F RID: 17423 RVA: 0x0011E34E File Offset: 0x0011C54E
			public void Reset()
			{
				this.arrayEnumerator.Reset();
			}

			// Token: 0x0400350B RID: 13579
			private int _objectReturnType;

			// Token: 0x0400350C RID: 13580
			internal const int Keys = 1;

			// Token: 0x0400350D RID: 13581
			internal const int Values = 2;

			// Token: 0x0400350E RID: 13582
			internal const int DictionaryEntry = 3;

			// Token: 0x0400350F RID: 13583
			private IEnumerator arrayEnumerator;
		}

		// Token: 0x020007EF RID: 2031
		private class OrderedDictionaryKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06004410 RID: 17424 RVA: 0x0011E35B File Offset: 0x0011C55B
			public OrderedDictionaryKeyValueCollection(ArrayList array, bool isKeys)
			{
				this._objects = array;
				this.isKeys = isKeys;
			}

			// Token: 0x06004411 RID: 17425 RVA: 0x0011E374 File Offset: 0x0011C574
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				foreach (object obj in this._objects)
				{
					array.SetValue(this.isKeys ? ((DictionaryEntry)obj).Key : ((DictionaryEntry)obj).Value, index);
					index++;
				}
			}

			// Token: 0x17000F6D RID: 3949
			// (get) Token: 0x06004412 RID: 17426 RVA: 0x0011E410 File Offset: 0x0011C610
			int ICollection.Count
			{
				get
				{
					return this._objects.Count;
				}
			}

			// Token: 0x17000F6E RID: 3950
			// (get) Token: 0x06004413 RID: 17427 RVA: 0x0011E41D File Offset: 0x0011C61D
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F6F RID: 3951
			// (get) Token: 0x06004414 RID: 17428 RVA: 0x0011E420 File Offset: 0x0011C620
			object ICollection.SyncRoot
			{
				get
				{
					return this._objects.SyncRoot;
				}
			}

			// Token: 0x06004415 RID: 17429 RVA: 0x0011E42D File Offset: 0x0011C62D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new OrderedDictionary.OrderedDictionaryEnumerator(this._objects, this.isKeys ? 1 : 2);
			}

			// Token: 0x04003510 RID: 13584
			private ArrayList _objects;

			// Token: 0x04003511 RID: 13585
			private bool isKeys;
		}
	}
}
