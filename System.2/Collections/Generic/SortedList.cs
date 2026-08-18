using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x020003C5 RID: 965
	[DebuggerTypeProxy(typeof(System_DictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[Serializable]
	public class SortedList<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x06002469 RID: 9321 RVA: 0x000AA6D4 File Offset: 0x000A88D4
		[__DynamicallyInvokable]
		public SortedList()
		{
			this.keys = SortedList<TKey, TValue>.emptyKeys;
			this.values = SortedList<TKey, TValue>.emptyValues;
			this._size = 0;
			this.comparer = Comparer<TKey>.Default;
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000AA704 File Offset: 0x000A8904
		[__DynamicallyInvokable]
		public SortedList(int capacity)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNumRequired);
			}
			this.keys = new TKey[capacity];
			this.values = new TValue[capacity];
			this.comparer = Comparer<TKey>.Default;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000AA73B File Offset: 0x000A893B
		[__DynamicallyInvokable]
		public SortedList(IComparer<TKey> comparer) : this()
		{
			if (comparer != null)
			{
				this.comparer = comparer;
			}
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000AA74D File Offset: 0x000A894D
		[__DynamicallyInvokable]
		public SortedList(int capacity, IComparer<TKey> comparer) : this(comparer)
		{
			this.Capacity = capacity;
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000AA75D File Offset: 0x000A895D
		[__DynamicallyInvokable]
		public SortedList(IDictionary<TKey, TValue> dictionary) : this(dictionary, null)
		{
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000AA768 File Offset: 0x000A8968
		[__DynamicallyInvokable]
		public SortedList(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer) : this((dictionary != null) ? dictionary.Count : 0, comparer)
		{
			if (dictionary == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
			}
			dictionary.Keys.CopyTo(this.keys, 0);
			dictionary.Values.CopyTo(this.values, 0);
			Array.Sort<TKey, TValue>(this.keys, this.values, comparer);
			this._size = dictionary.Count;
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000AA7D4 File Offset: 0x000A89D4
		[__DynamicallyInvokable]
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
			if (num >= 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
			}
			this.Insert(~num, key, value);
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000AA81D File Offset: 0x000A8A1D
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000AA834 File Offset: 0x000A8A34
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.IndexOfKey(keyValuePair.Key);
			return num >= 0 && EqualityComparer<TValue>.Default.Equals(this.values[num], keyValuePair.Value);
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000AA878 File Offset: 0x000A8A78
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.IndexOfKey(keyValuePair.Key);
			if (num >= 0 && EqualityComparer<TValue>.Default.Equals(this.values[num], keyValuePair.Value))
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x000AA8C0 File Offset: 0x000A8AC0
		// (set) Token: 0x06002474 RID: 9332 RVA: 0x000AA8CC File Offset: 0x000A8ACC
		[__DynamicallyInvokable]
		public int Capacity
		{
			[__DynamicallyInvokable]
			get
			{
				return this.keys.Length;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value != this.keys.Length)
				{
					if (value < this._size)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
					}
					if (value > 0)
					{
						TKey[] destinationArray = new TKey[value];
						TValue[] destinationArray2 = new TValue[value];
						if (this._size > 0)
						{
							Array.Copy(this.keys, 0, destinationArray, 0, this._size);
							Array.Copy(this.values, 0, destinationArray2, 0, this._size);
						}
						this.keys = destinationArray;
						this.values = destinationArray2;
						return;
					}
					this.keys = SortedList<TKey, TValue>.emptyKeys;
					this.values = SortedList<TKey, TValue>.emptyValues;
				}
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x000AA95E File Offset: 0x000A8B5E
		[__DynamicallyInvokable]
		public IComparer<TKey> Comparer
		{
			[__DynamicallyInvokable]
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000AA968 File Offset: 0x000A8B68
		[__DynamicallyInvokable]
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
			try
			{
				TKey key2 = (TKey)((object)key);
				try
				{
					this.Add(key2, (TValue)((object)value));
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
				}
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x000AA9E0 File Offset: 0x000A8BE0
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x000AA9E8 File Offset: 0x000A8BE8
		[__DynamicallyInvokable]
		public IList<TKey> Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetKeyListHelper();
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x000AA9F0 File Offset: 0x000A8BF0
		[__DynamicallyInvokable]
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetKeyListHelper();
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x0600247A RID: 9338 RVA: 0x000AA9F8 File Offset: 0x000A8BF8
		[__DynamicallyInvokable]
		ICollection IDictionary.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetKeyListHelper();
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x0600247B RID: 9339 RVA: 0x000AAA00 File Offset: 0x000A8C00
		[__DynamicallyInvokable]
		IEnumerable<TKey> IReadOnlyDictionary<!0, !1>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetKeyListHelper();
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x000AAA08 File Offset: 0x000A8C08
		[__DynamicallyInvokable]
		public IList<TValue> Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x000AAA10 File Offset: 0x000A8C10
		[__DynamicallyInvokable]
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x000AAA18 File Offset: 0x000A8C18
		[__DynamicallyInvokable]
		ICollection IDictionary.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x000AAA20 File Offset: 0x000A8C20
		[__DynamicallyInvokable]
		IEnumerable<TValue> IReadOnlyDictionary<!0, !1>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000AAA28 File Offset: 0x000A8C28
		private SortedList<TKey, TValue>.KeyList GetKeyListHelper()
		{
			if (this.keyList == null)
			{
				this.keyList = new SortedList<TKey, TValue>.KeyList(this);
			}
			return this.keyList;
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000AAA44 File Offset: 0x000A8C44
		private SortedList<TKey, TValue>.ValueList GetValueListHelper()
		{
			if (this.valueList == null)
			{
				this.valueList = new SortedList<TKey, TValue>.ValueList(this);
			}
			return this.valueList;
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x000AAA60 File Offset: 0x000A8C60
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x000AAA63 File Offset: 0x000A8C63
		[__DynamicallyInvokable]
		bool IDictionary.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x000AAA66 File Offset: 0x000A8C66
		[__DynamicallyInvokable]
		bool IDictionary.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x000AAA69 File Offset: 0x000A8C69
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x000AAA6C File Offset: 0x000A8C6C
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x000AAA8E File Offset: 0x000A8C8E
		[__DynamicallyInvokable]
		public void Clear()
		{
			this.version++;
			Array.Clear(this.keys, 0, this._size);
			Array.Clear(this.values, 0, this._size);
			this._size = 0;
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000AAAC9 File Offset: 0x000A8CC9
		[__DynamicallyInvokable]
		bool IDictionary.Contains(object key)
		{
			return SortedList<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000AAAE1 File Offset: 0x000A8CE1
		[__DynamicallyInvokable]
		public bool ContainsKey(TKey key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000AAAF0 File Offset: 0x000A8CF0
		[__DynamicallyInvokable]
		public bool ContainsValue(TValue value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000AAB00 File Offset: 0x000A8D00
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - arrayIndex < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			for (int i = 0; i < this.Count; i++)
			{
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(this.keys[i], this.values[i]);
				array[arrayIndex + i] = keyValuePair;
			}
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000AAB78 File Offset: 0x000A8D78
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - arrayIndex < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				for (int i = 0; i < this.Count; i++)
				{
					array2[i + arrayIndex] = new KeyValuePair<TKey, TValue>(this.keys[i], this.values[i]);
				}
				return;
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
			try
			{
				for (int j = 0; j < this.Count; j++)
				{
					array3[j + arrayIndex] = new KeyValuePair<TKey, TValue>(this.keys[j], this.values[j]);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000AAC80 File Offset: 0x000A8E80
		private void EnsureCapacity(int min)
		{
			int num = (this.keys.Length == 0) ? 4 : (this.keys.Length * 2);
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000AACBF File Offset: 0x000A8EBF
		private TValue GetByIndex(int index)
		{
			if (index < 0 || index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			return this.values[index];
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000AACE3 File Offset: 0x000A8EE3
		[__DynamicallyInvokable]
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000AACF1 File Offset: 0x000A8EF1
		[__DynamicallyInvokable]
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000AACFF File Offset: 0x000A8EFF
		[__DynamicallyInvokable]
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000AAD0D File Offset: 0x000A8F0D
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000AAD1B File Offset: 0x000A8F1B
		private TKey GetKey(int index)
		{
			if (index < 0 || index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			return this.keys[index];
		}

		// Token: 0x17000940 RID: 2368
		[__DynamicallyInvokable]
		public TValue this[TKey key]
		{
			[__DynamicallyInvokable]
			get
			{
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					return this.values[num];
				}
				ThrowHelper.ThrowKeyNotFoundException();
				return default(TValue);
			}
			[__DynamicallyInvokable]
			set
			{
				if (key == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
				if (num >= 0)
				{
					this.values[num] = value;
					this.version++;
					return;
				}
				this.Insert(~num, key, value);
			}
		}

		// Token: 0x17000941 RID: 2369
		[__DynamicallyInvokable]
		object IDictionary.this[object key]
		{
			[__DynamicallyInvokable]
			get
			{
				if (SortedList<TKey, TValue>.IsCompatibleKey(key))
				{
					int num = this.IndexOfKey((TKey)((object)key));
					if (num >= 0)
					{
						return this.values[num];
					}
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
			{
				if (!SortedList<TKey, TValue>.IsCompatibleKey(key))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
				try
				{
					TKey key2 = (TKey)((object)key);
					try
					{
						this[key2] = (TValue)((object)value);
					}
					catch (InvalidCastException)
					{
						ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
					}
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
				}
			}
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000AAE90 File Offset: 0x000A9090
		[__DynamicallyInvokable]
		public int IndexOfKey(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000AAECC File Offset: 0x000A90CC
		[__DynamicallyInvokable]
		public int IndexOfValue(TValue value)
		{
			return Array.IndexOf<TValue>(this.values, value, 0, this._size);
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000AAEE4 File Offset: 0x000A90E4
		private void Insert(int index, TKey key, TValue value)
		{
			if (this._size == this.keys.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this.keys, index, this.keys, index + 1, this._size - index);
				Array.Copy(this.values, index, this.values, index + 1, this._size - index);
			}
			this.keys[index] = key;
			this.values[index] = value;
			this._size++;
			this.version++;
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000AAF88 File Offset: 0x000A9188
		[__DynamicallyInvokable]
		public bool TryGetValue(TKey key, out TValue value)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				value = this.values[num];
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000AAFC0 File Offset: 0x000A91C0
		[__DynamicallyInvokable]
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this.keys, index + 1, this.keys, index, this._size - index);
				Array.Copy(this.values, index + 1, this.values, index, this._size - index);
			}
			this.keys[this._size] = default(TKey);
			this.values[this._size] = default(TValue);
			this.version++;
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000AB078 File Offset: 0x000A9278
		[__DynamicallyInvokable]
		public bool Remove(TKey key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
			return num >= 0;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000AB09F File Offset: 0x000A929F
		[__DynamicallyInvokable]
		void IDictionary.Remove(object key)
		{
			if (SortedList<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000AB0B8 File Offset: 0x000A92B8
		[__DynamicallyInvokable]
		public void TrimExcess()
		{
			int num = (int)((double)this.keys.Length * 0.9);
			if (this._size < num)
			{
				this.Capacity = this._size;
			}
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000AB0EF File Offset: 0x000A92EF
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			return key is TKey;
		}

		// Token: 0x0400201E RID: 8222
		private TKey[] keys;

		// Token: 0x0400201F RID: 8223
		private TValue[] values;

		// Token: 0x04002020 RID: 8224
		private int _size;

		// Token: 0x04002021 RID: 8225
		private int version;

		// Token: 0x04002022 RID: 8226
		private IComparer<TKey> comparer;

		// Token: 0x04002023 RID: 8227
		private SortedList<TKey, TValue>.KeyList keyList;

		// Token: 0x04002024 RID: 8228
		private SortedList<TKey, TValue>.ValueList valueList;

		// Token: 0x04002025 RID: 8229
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04002026 RID: 8230
		private static TKey[] emptyKeys = new TKey[0];

		// Token: 0x04002027 RID: 8231
		private static TValue[] emptyValues = new TValue[0];

		// Token: 0x04002028 RID: 8232
		private const int _defaultCapacity = 4;

		// Token: 0x04002029 RID: 8233
		private const int MaxArrayLength = 2146435071;

		// Token: 0x020007F4 RID: 2036
		[Serializable]
		private struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x0600443B RID: 17467 RVA: 0x0011EB76 File Offset: 0x0011CD76
			internal Enumerator(SortedList<TKey, TValue> sortedList, int getEnumeratorRetType)
			{
				this._sortedList = sortedList;
				this.index = 0;
				this.version = this._sortedList.version;
				this.getEnumeratorRetType = getEnumeratorRetType;
				this.key = default(TKey);
				this.value = default(TValue);
			}

			// Token: 0x0600443C RID: 17468 RVA: 0x0011EBB6 File Offset: 0x0011CDB6
			public void Dispose()
			{
				this.index = 0;
				this.key = default(TKey);
				this.value = default(TValue);
			}

			// Token: 0x17000F7A RID: 3962
			// (get) Token: 0x0600443D RID: 17469 RVA: 0x0011EBD7 File Offset: 0x0011CDD7
			object IDictionaryEnumerator.Key
			{
				get
				{
					if (this.index == 0 || this.index == this._sortedList.Count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.key;
				}
			}

			// Token: 0x0600443E RID: 17470 RVA: 0x0011EC08 File Offset: 0x0011CE08
			public bool MoveNext()
			{
				if (this.version != this._sortedList.version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				if (this.index < this._sortedList.Count)
				{
					this.key = this._sortedList.keys[this.index];
					this.value = this._sortedList.values[this.index];
					this.index++;
					return true;
				}
				this.index = this._sortedList.Count + 1;
				this.key = default(TKey);
				this.value = default(TValue);
				return false;
			}

			// Token: 0x17000F7B RID: 3963
			// (get) Token: 0x0600443F RID: 17471 RVA: 0x0011ECB8 File Offset: 0x0011CEB8
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					if (this.index == 0 || this.index == this._sortedList.Count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return new DictionaryEntry(this.key, this.value);
				}
			}

			// Token: 0x17000F7C RID: 3964
			// (get) Token: 0x06004440 RID: 17472 RVA: 0x0011ED04 File Offset: 0x0011CF04
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return new KeyValuePair<TKey, TValue>(this.key, this.value);
				}
			}

			// Token: 0x17000F7D RID: 3965
			// (get) Token: 0x06004441 RID: 17473 RVA: 0x0011ED18 File Offset: 0x0011CF18
			object IEnumerator.Current
			{
				get
				{
					if (this.index == 0 || this.index == this._sortedList.Count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					if (this.getEnumeratorRetType == 2)
					{
						return new DictionaryEntry(this.key, this.value);
					}
					return new KeyValuePair<TKey, TValue>(this.key, this.value);
				}
			}

			// Token: 0x17000F7E RID: 3966
			// (get) Token: 0x06004442 RID: 17474 RVA: 0x0011ED89 File Offset: 0x0011CF89
			object IDictionaryEnumerator.Value
			{
				get
				{
					if (this.index == 0 || this.index == this._sortedList.Count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.value;
				}
			}

			// Token: 0x06004443 RID: 17475 RVA: 0x0011EDBA File Offset: 0x0011CFBA
			void IEnumerator.Reset()
			{
				if (this.version != this._sortedList.version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.index = 0;
				this.key = default(TKey);
				this.value = default(TValue);
			}

			// Token: 0x04003524 RID: 13604
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x04003525 RID: 13605
			private TKey key;

			// Token: 0x04003526 RID: 13606
			private TValue value;

			// Token: 0x04003527 RID: 13607
			private int index;

			// Token: 0x04003528 RID: 13608
			private int version;

			// Token: 0x04003529 RID: 13609
			private int getEnumeratorRetType;

			// Token: 0x0400352A RID: 13610
			internal const int KeyValuePair = 1;

			// Token: 0x0400352B RID: 13611
			internal const int DictEntry = 2;
		}

		// Token: 0x020007F5 RID: 2037
		[Serializable]
		private sealed class SortedListKeyEnumerator : IEnumerator<TKey>, IDisposable, IEnumerator
		{
			// Token: 0x06004444 RID: 17476 RVA: 0x0011EDF5 File Offset: 0x0011CFF5
			internal SortedListKeyEnumerator(SortedList<TKey, TValue> sortedList)
			{
				this._sortedList = sortedList;
				this.version = sortedList.version;
			}

			// Token: 0x06004445 RID: 17477 RVA: 0x0011EE10 File Offset: 0x0011D010
			public void Dispose()
			{
				this.index = 0;
				this.currentKey = default(TKey);
			}

			// Token: 0x06004446 RID: 17478 RVA: 0x0011EE28 File Offset: 0x0011D028
			public bool MoveNext()
			{
				if (this.version != this._sortedList.version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				if (this.index < this._sortedList.Count)
				{
					this.currentKey = this._sortedList.keys[this.index];
					this.index++;
					return true;
				}
				this.index = this._sortedList.Count + 1;
				this.currentKey = default(TKey);
				return false;
			}

			// Token: 0x17000F7F RID: 3967
			// (get) Token: 0x06004447 RID: 17479 RVA: 0x0011EEAE File Offset: 0x0011D0AE
			public TKey Current
			{
				get
				{
					return this.currentKey;
				}
			}

			// Token: 0x17000F80 RID: 3968
			// (get) Token: 0x06004448 RID: 17480 RVA: 0x0011EEB6 File Offset: 0x0011D0B6
			object IEnumerator.Current
			{
				get
				{
					if (this.index == 0 || this.index == this._sortedList.Count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.currentKey;
				}
			}

			// Token: 0x06004449 RID: 17481 RVA: 0x0011EEE7 File Offset: 0x0011D0E7
			void IEnumerator.Reset()
			{
				if (this.version != this._sortedList.version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.index = 0;
				this.currentKey = default(TKey);
			}

			// Token: 0x0400352C RID: 13612
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x0400352D RID: 13613
			private int index;

			// Token: 0x0400352E RID: 13614
			private int version;

			// Token: 0x0400352F RID: 13615
			private TKey currentKey;
		}

		// Token: 0x020007F6 RID: 2038
		[Serializable]
		private sealed class SortedListValueEnumerator : IEnumerator<TValue>, IDisposable, IEnumerator
		{
			// Token: 0x0600444A RID: 17482 RVA: 0x0011EF16 File Offset: 0x0011D116
			internal SortedListValueEnumerator(SortedList<TKey, TValue> sortedList)
			{
				this._sortedList = sortedList;
				this.version = sortedList.version;
			}

			// Token: 0x0600444B RID: 17483 RVA: 0x0011EF31 File Offset: 0x0011D131
			public void Dispose()
			{
				this.index = 0;
				this.currentValue = default(TValue);
			}

			// Token: 0x0600444C RID: 17484 RVA: 0x0011EF48 File Offset: 0x0011D148
			public bool MoveNext()
			{
				if (this.version != this._sortedList.version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				if (this.index < this._sortedList.Count)
				{
					this.currentValue = this._sortedList.values[this.index];
					this.index++;
					return true;
				}
				this.index = this._sortedList.Count + 1;
				this.currentValue = default(TValue);
				return false;
			}

			// Token: 0x17000F81 RID: 3969
			// (get) Token: 0x0600444D RID: 17485 RVA: 0x0011EFCE File Offset: 0x0011D1CE
			public TValue Current
			{
				get
				{
					return this.currentValue;
				}
			}

			// Token: 0x17000F82 RID: 3970
			// (get) Token: 0x0600444E RID: 17486 RVA: 0x0011EFD6 File Offset: 0x0011D1D6
			object IEnumerator.Current
			{
				get
				{
					if (this.index == 0 || this.index == this._sortedList.Count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.currentValue;
				}
			}

			// Token: 0x0600444F RID: 17487 RVA: 0x0011F007 File Offset: 0x0011D207
			void IEnumerator.Reset()
			{
				if (this.version != this._sortedList.version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.index = 0;
				this.currentValue = default(TValue);
			}

			// Token: 0x04003530 RID: 13616
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x04003531 RID: 13617
			private int index;

			// Token: 0x04003532 RID: 13618
			private int version;

			// Token: 0x04003533 RID: 13619
			private TValue currentValue;
		}

		// Token: 0x020007F7 RID: 2039
		[DebuggerTypeProxy(typeof(System_DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		private sealed class KeyList : IList<TKey>, ICollection<!0>, IEnumerable<!0>, IEnumerable, ICollection
		{
			// Token: 0x06004450 RID: 17488 RVA: 0x0011F036 File Offset: 0x0011D236
			internal KeyList(SortedList<TKey, TValue> dictionary)
			{
				this._dict = dictionary;
			}

			// Token: 0x17000F83 RID: 3971
			// (get) Token: 0x06004451 RID: 17489 RVA: 0x0011F045 File Offset: 0x0011D245
			public int Count
			{
				get
				{
					return this._dict._size;
				}
			}

			// Token: 0x17000F84 RID: 3972
			// (get) Token: 0x06004452 RID: 17490 RVA: 0x0011F052 File Offset: 0x0011D252
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F85 RID: 3973
			// (get) Token: 0x06004453 RID: 17491 RVA: 0x0011F055 File Offset: 0x0011D255
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F86 RID: 3974
			// (get) Token: 0x06004454 RID: 17492 RVA: 0x0011F058 File Offset: 0x0011D258
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dict).SyncRoot;
				}
			}

			// Token: 0x06004455 RID: 17493 RVA: 0x0011F065 File Offset: 0x0011D265
			public void Add(TKey key)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x06004456 RID: 17494 RVA: 0x0011F06E File Offset: 0x0011D26E
			public void Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x06004457 RID: 17495 RVA: 0x0011F077 File Offset: 0x0011D277
			public bool Contains(TKey key)
			{
				return this._dict.ContainsKey(key);
			}

			// Token: 0x06004458 RID: 17496 RVA: 0x0011F085 File Offset: 0x0011D285
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				Array.Copy(this._dict.keys, 0, array, arrayIndex, this._dict.Count);
			}

			// Token: 0x06004459 RID: 17497 RVA: 0x0011F0A8 File Offset: 0x0011D2A8
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				try
				{
					Array.Copy(this._dict.keys, 0, array, arrayIndex, this._dict.Count);
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
				}
			}

			// Token: 0x0600445A RID: 17498 RVA: 0x0011F104 File Offset: 0x0011D304
			public void Insert(int index, TKey value)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x17000F87 RID: 3975
			public TKey this[int index]
			{
				get
				{
					return this._dict.GetKey(index);
				}
				set
				{
					ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
				}
			}

			// Token: 0x0600445D RID: 17501 RVA: 0x0011F124 File Offset: 0x0011D324
			public IEnumerator<TKey> GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListKeyEnumerator(this._dict);
			}

			// Token: 0x0600445E RID: 17502 RVA: 0x0011F131 File Offset: 0x0011D331
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListKeyEnumerator(this._dict);
			}

			// Token: 0x0600445F RID: 17503 RVA: 0x0011F140 File Offset: 0x0011D340
			public int IndexOf(TKey key)
			{
				if (key == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				int num = Array.BinarySearch<TKey>(this._dict.keys, 0, this._dict.Count, key, this._dict.comparer);
				if (num >= 0)
				{
					return num;
				}
				return -1;
			}

			// Token: 0x06004460 RID: 17504 RVA: 0x0011F18B File Offset: 0x0011D38B
			public bool Remove(TKey key)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_SortedListNestedWrite);
				return false;
			}

			// Token: 0x06004461 RID: 17505 RVA: 0x0011F195 File Offset: 0x0011D395
			public void RemoveAt(int index)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x04003534 RID: 13620
			private SortedList<TKey, TValue> _dict;
		}

		// Token: 0x020007F8 RID: 2040
		[DebuggerTypeProxy(typeof(System_DictionaryValueCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		private sealed class ValueList : IList<TValue>, ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection
		{
			// Token: 0x06004462 RID: 17506 RVA: 0x0011F19E File Offset: 0x0011D39E
			internal ValueList(SortedList<TKey, TValue> dictionary)
			{
				this._dict = dictionary;
			}

			// Token: 0x17000F88 RID: 3976
			// (get) Token: 0x06004463 RID: 17507 RVA: 0x0011F1AD File Offset: 0x0011D3AD
			public int Count
			{
				get
				{
					return this._dict._size;
				}
			}

			// Token: 0x17000F89 RID: 3977
			// (get) Token: 0x06004464 RID: 17508 RVA: 0x0011F1BA File Offset: 0x0011D3BA
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F8A RID: 3978
			// (get) Token: 0x06004465 RID: 17509 RVA: 0x0011F1BD File Offset: 0x0011D3BD
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F8B RID: 3979
			// (get) Token: 0x06004466 RID: 17510 RVA: 0x0011F1C0 File Offset: 0x0011D3C0
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dict).SyncRoot;
				}
			}

			// Token: 0x06004467 RID: 17511 RVA: 0x0011F1CD File Offset: 0x0011D3CD
			public void Add(TValue key)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x06004468 RID: 17512 RVA: 0x0011F1D6 File Offset: 0x0011D3D6
			public void Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x06004469 RID: 17513 RVA: 0x0011F1DF File Offset: 0x0011D3DF
			public bool Contains(TValue value)
			{
				return this._dict.ContainsValue(value);
			}

			// Token: 0x0600446A RID: 17514 RVA: 0x0011F1ED File Offset: 0x0011D3ED
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				Array.Copy(this._dict.values, 0, array, arrayIndex, this._dict.Count);
			}

			// Token: 0x0600446B RID: 17515 RVA: 0x0011F210 File Offset: 0x0011D410
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				try
				{
					Array.Copy(this._dict.values, 0, array, arrayIndex, this._dict.Count);
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
				}
			}

			// Token: 0x0600446C RID: 17516 RVA: 0x0011F26C File Offset: 0x0011D46C
			public void Insert(int index, TValue value)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x17000F8C RID: 3980
			public TValue this[int index]
			{
				get
				{
					return this._dict.GetByIndex(index);
				}
				set
				{
					ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_SortedListNestedWrite);
				}
			}

			// Token: 0x0600446F RID: 17519 RVA: 0x0011F28C File Offset: 0x0011D48C
			public IEnumerator<TValue> GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListValueEnumerator(this._dict);
			}

			// Token: 0x06004470 RID: 17520 RVA: 0x0011F299 File Offset: 0x0011D499
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListValueEnumerator(this._dict);
			}

			// Token: 0x06004471 RID: 17521 RVA: 0x0011F2A6 File Offset: 0x0011D4A6
			public int IndexOf(TValue value)
			{
				return Array.IndexOf<TValue>(this._dict.values, value, 0, this._dict.Count);
			}

			// Token: 0x06004472 RID: 17522 RVA: 0x0011F2C5 File Offset: 0x0011D4C5
			public bool Remove(TValue value)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_SortedListNestedWrite);
				return false;
			}

			// Token: 0x06004473 RID: 17523 RVA: 0x0011F2CF File Offset: 0x0011D4CF
			public void RemoveAt(int index)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x04003535 RID: 13621
			private SortedList<TKey, TValue> _dict;
		}
	}
}
