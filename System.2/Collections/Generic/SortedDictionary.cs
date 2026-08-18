using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003C7 RID: 967
	[DebuggerTypeProxy(typeof(System_DictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public class SortedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<!0, !1>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x060024B5 RID: 9397 RVA: 0x000AB5AC File Offset: 0x000A97AC
		[__DynamicallyInvokable]
		public SortedDictionary() : this(null)
		{
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000AB5B5 File Offset: 0x000A97B5
		[__DynamicallyInvokable]
		public SortedDictionary(IDictionary<TKey, TValue> dictionary) : this(dictionary, null)
		{
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000AB5C0 File Offset: 0x000A97C0
		[__DynamicallyInvokable]
		public SortedDictionary(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
		{
			if (dictionary == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
			}
			this._set = new TreeSet<KeyValuePair<TKey, TValue>>(new SortedDictionary<TKey, TValue>.KeyValuePairComparer(comparer));
			foreach (KeyValuePair<TKey, TValue> item in dictionary)
			{
				this._set.Add(item);
			}
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000AB630 File Offset: 0x000A9830
		[__DynamicallyInvokable]
		public SortedDictionary(IComparer<TKey> comparer)
		{
			this._set = new TreeSet<KeyValuePair<TKey, TValue>>(new SortedDictionary<TKey, TValue>.KeyValuePairComparer(comparer));
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000AB649 File Offset: 0x000A9849
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this._set.Add(keyValuePair);
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000AB658 File Offset: 0x000A9858
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(keyValuePair);
			if (node == null)
			{
				return false;
			}
			if (keyValuePair.Value == null)
			{
				return node.Item.Value == null;
			}
			return EqualityComparer<TValue>.Default.Equals(node.Item.Value, keyValuePair.Value);
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000AB6B8 File Offset: 0x000A98B8
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(keyValuePair);
			if (node == null)
			{
				return false;
			}
			if (EqualityComparer<TValue>.Default.Equals(node.Item.Value, keyValuePair.Value))
			{
				this._set.Remove(keyValuePair);
				return true;
			}
			return false;
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x000AB705 File Offset: 0x000A9905
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000946 RID: 2374
		[__DynamicallyInvokable]
		public TValue this[TKey key]
		{
			[__DynamicallyInvokable]
			get
			{
				if (key == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
				if (node == null)
				{
					ThrowHelper.ThrowKeyNotFoundException();
				}
				return node.Item.Value;
			}
			[__DynamicallyInvokable]
			set
			{
				if (key == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
				if (node == null)
				{
					this._set.Add(new KeyValuePair<TKey, TValue>(key, value));
					return;
				}
				node.Item = new KeyValuePair<TKey, TValue>(node.Item.Key, value);
				this._set.UpdateVersion();
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060024BF RID: 9407 RVA: 0x000AB7C3 File Offset: 0x000A99C3
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this._set.Count;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x000AB7D0 File Offset: 0x000A99D0
		[__DynamicallyInvokable]
		public IComparer<TKey> Comparer
		{
			[__DynamicallyInvokable]
			get
			{
				return ((SortedDictionary<TKey, TValue>.KeyValuePairComparer)this._set.Comparer).keyComparer;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060024C1 RID: 9409 RVA: 0x000AB7E7 File Offset: 0x000A99E7
		[__DynamicallyInvokable]
		public SortedDictionary<TKey, TValue>.KeyCollection Keys
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.keys == null)
				{
					this.keys = new SortedDictionary<TKey, TValue>.KeyCollection(this);
				}
				return this.keys;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x000AB803 File Offset: 0x000A9A03
		[__DynamicallyInvokable]
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x000AB80B File Offset: 0x000A9A0B
		[__DynamicallyInvokable]
		IEnumerable<TKey> IReadOnlyDictionary<!0, !1>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060024C4 RID: 9412 RVA: 0x000AB813 File Offset: 0x000A9A13
		[__DynamicallyInvokable]
		public SortedDictionary<TKey, TValue>.ValueCollection Values
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.values == null)
				{
					this.values = new SortedDictionary<TKey, TValue>.ValueCollection(this);
				}
				return this.values;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x000AB82F File Offset: 0x000A9A2F
		[__DynamicallyInvokable]
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Values;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x000AB837 File Offset: 0x000A9A37
		[__DynamicallyInvokable]
		IEnumerable<TValue> IReadOnlyDictionary<!0, !1>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Values;
			}
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000AB83F File Offset: 0x000A9A3F
		[__DynamicallyInvokable]
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			this._set.Add(new KeyValuePair<TKey, TValue>(key, value));
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000AB862 File Offset: 0x000A9A62
		[__DynamicallyInvokable]
		public void Clear()
		{
			this._set.Clear();
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000AB870 File Offset: 0x000A9A70
		[__DynamicallyInvokable]
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			return this._set.Contains(new KeyValuePair<TKey, TValue>(key, default(TValue)));
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000AB8A8 File Offset: 0x000A9AA8
		[__DynamicallyInvokable]
		public bool ContainsValue(TValue value)
		{
			bool found = false;
			if (value == null)
			{
				this._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					if (node.Item.Value == null)
					{
						found = true;
						return false;
					}
					return true;
				});
			}
			else
			{
				EqualityComparer<TValue> valueComparer = EqualityComparer<TValue>.Default;
				this._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					if (valueComparer.Equals(node.Item.Value, value))
					{
						found = true;
						return false;
					}
					return true;
				});
			}
			return found;
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000AB926 File Offset: 0x000A9B26
		[__DynamicallyInvokable]
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this._set.CopyTo(array, index);
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000AB935 File Offset: 0x000A9B35
		[__DynamicallyInvokable]
		public SortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000AB93E File Offset: 0x000A9B3E
		[__DynamicallyInvokable]
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000AB94C File Offset: 0x000A9B4C
		[__DynamicallyInvokable]
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			return this._set.Remove(new KeyValuePair<TKey, TValue>(key, default(TValue)));
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000AB984 File Offset: 0x000A9B84
		[__DynamicallyInvokable]
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
			if (node == null)
			{
				value = default(TValue);
				return false;
			}
			value = node.Item.Value;
			return true;
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000AB9D8 File Offset: 0x000A9BD8
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this._set).CopyTo(array, index);
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x000AB9E7 File Offset: 0x000A9BE7
		[__DynamicallyInvokable]
		bool IDictionary.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x000AB9EA File Offset: 0x000A9BEA
		[__DynamicallyInvokable]
		bool IDictionary.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x000AB9ED File Offset: 0x000A9BED
		[__DynamicallyInvokable]
		ICollection IDictionary.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x000AB9F5 File Offset: 0x000A9BF5
		[__DynamicallyInvokable]
		ICollection IDictionary.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17000953 RID: 2387
		[__DynamicallyInvokable]
		object IDictionary.this[object key]
		{
			[__DynamicallyInvokable]
			get
			{
				TValue tvalue;
				if (SortedDictionary<TKey, TValue>.IsCompatibleKey(key) && this.TryGetValue((TKey)((object)key), out tvalue))
				{
					return tvalue;
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
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

		// Token: 0x060024D7 RID: 9431 RVA: 0x000ABAA8 File Offset: 0x000A9CA8
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

		// Token: 0x060024D8 RID: 9432 RVA: 0x000ABB20 File Offset: 0x000A9D20
		[__DynamicallyInvokable]
		bool IDictionary.Contains(object key)
		{
			return SortedDictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000ABB38 File Offset: 0x000A9D38
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			return key is TKey;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000ABB4C File Offset: 0x000A9D4C
		[__DynamicallyInvokable]
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000ABB5A File Offset: 0x000A9D5A
		[__DynamicallyInvokable]
		void IDictionary.Remove(object key)
		{
			if (SortedDictionary<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x000ABB71 File Offset: 0x000A9D71
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x060024DD RID: 9437 RVA: 0x000ABB74 File Offset: 0x000A9D74
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				return ((ICollection)this._set).SyncRoot;
			}
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000ABB81 File Offset: 0x000A9D81
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x04002030 RID: 8240
		[NonSerialized]
		private SortedDictionary<TKey, TValue>.KeyCollection keys;

		// Token: 0x04002031 RID: 8241
		[NonSerialized]
		private SortedDictionary<TKey, TValue>.ValueCollection values;

		// Token: 0x04002032 RID: 8242
		private TreeSet<KeyValuePair<TKey, TValue>> _set;

		// Token: 0x020007FA RID: 2042
		[__DynamicallyInvokable]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x0600447A RID: 17530 RVA: 0x0011F45A File Offset: 0x0011D65A
			internal Enumerator(SortedDictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
			{
				this.treeEnum = dictionary._set.GetEnumerator();
				this.getEnumeratorRetType = getEnumeratorRetType;
			}

			// Token: 0x0600447B RID: 17531 RVA: 0x0011F474 File Offset: 0x0011D674
			[__DynamicallyInvokable]
			public bool MoveNext()
			{
				return this.treeEnum.MoveNext();
			}

			// Token: 0x0600447C RID: 17532 RVA: 0x0011F481 File Offset: 0x0011D681
			[__DynamicallyInvokable]
			public void Dispose()
			{
				this.treeEnum.Dispose();
			}

			// Token: 0x17000F8F RID: 3983
			// (get) Token: 0x0600447D RID: 17533 RVA: 0x0011F48E File Offset: 0x0011D68E
			[__DynamicallyInvokable]
			public KeyValuePair<TKey, TValue> Current
			{
				[__DynamicallyInvokable]
				get
				{
					return this.treeEnum.Current;
				}
			}

			// Token: 0x17000F90 RID: 3984
			// (get) Token: 0x0600447E RID: 17534 RVA: 0x0011F49B File Offset: 0x0011D69B
			internal bool NotStartedOrEnded
			{
				get
				{
					return this.treeEnum.NotStartedOrEnded;
				}
			}

			// Token: 0x0600447F RID: 17535 RVA: 0x0011F4A8 File Offset: 0x0011D6A8
			internal void Reset()
			{
				this.treeEnum.Reset();
			}

			// Token: 0x06004480 RID: 17536 RVA: 0x0011F4B5 File Offset: 0x0011D6B5
			[__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				this.treeEnum.Reset();
			}

			// Token: 0x17000F91 RID: 3985
			// (get) Token: 0x06004481 RID: 17537 RVA: 0x0011F4C4 File Offset: 0x0011D6C4
			[__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.NotStartedOrEnded)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					KeyValuePair<TKey, TValue> keyValuePair;
					if (this.getEnumeratorRetType == 2)
					{
						keyValuePair = this.Current;
						object key = keyValuePair.Key;
						keyValuePair = this.Current;
						return new DictionaryEntry(key, keyValuePair.Value);
					}
					keyValuePair = this.Current;
					TKey key2 = keyValuePair.Key;
					keyValuePair = this.Current;
					return new KeyValuePair<TKey, TValue>(key2, keyValuePair.Value);
				}
			}

			// Token: 0x17000F92 RID: 3986
			// (get) Token: 0x06004482 RID: 17538 RVA: 0x0011F540 File Offset: 0x0011D740
			[__DynamicallyInvokable]
			object IDictionaryEnumerator.Key
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.NotStartedOrEnded)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x17000F93 RID: 3987
			// (get) Token: 0x06004483 RID: 17539 RVA: 0x0011F570 File Offset: 0x0011D770
			[__DynamicallyInvokable]
			object IDictionaryEnumerator.Value
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.NotStartedOrEnded)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x17000F94 RID: 3988
			// (get) Token: 0x06004484 RID: 17540 RVA: 0x0011F5A0 File Offset: 0x0011D7A0
			[__DynamicallyInvokable]
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.NotStartedOrEnded)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					object key = keyValuePair.Key;
					keyValuePair = this.Current;
					return new DictionaryEntry(key, keyValuePair.Value);
				}
			}

			// Token: 0x0400353A RID: 13626
			private SortedSet<KeyValuePair<TKey, TValue>>.Enumerator treeEnum;

			// Token: 0x0400353B RID: 13627
			private int getEnumeratorRetType;

			// Token: 0x0400353C RID: 13628
			internal const int KeyValuePair = 1;

			// Token: 0x0400353D RID: 13629
			internal const int DictEntry = 2;
		}

		// Token: 0x020007FB RID: 2043
		[DebuggerTypeProxy(typeof(System_DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[__DynamicallyInvokable]
		[Serializable]
		public sealed class KeyCollection : ICollection<!0>, IEnumerable<!0>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			// Token: 0x06004485 RID: 17541 RVA: 0x0011F5E7 File Offset: 0x0011D7E7
			[__DynamicallyInvokable]
			public KeyCollection(SortedDictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
				}
				this.dictionary = dictionary;
			}

			// Token: 0x06004486 RID: 17542 RVA: 0x0011F5FF File Offset: 0x0011D7FF
			[__DynamicallyInvokable]
			public SortedDictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			// Token: 0x06004487 RID: 17543 RVA: 0x0011F60C File Offset: 0x0011D80C
			[__DynamicallyInvokable]
			IEnumerator<TKey> IEnumerable<!0>.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			// Token: 0x06004488 RID: 17544 RVA: 0x0011F61E File Offset: 0x0011D81E
			[__DynamicallyInvokable]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			// Token: 0x06004489 RID: 17545 RVA: 0x0011F630 File Offset: 0x0011D830
			[__DynamicallyInvokable]
			public void CopyTo(TKey[] array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (index < 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
				}
				if (array.Length - index < this.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				this.dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					TKey[] array2 = array;
					int index2 = index;
					index = index2 + 1;
					array2[index2] = node.Item.Key;
					return true;
				});
			}

			// Token: 0x0600448A RID: 17546 RVA: 0x0011F6AC File Offset: 0x0011D8AC
			[__DynamicallyInvokable]
			void ICollection.CopyTo(Array array, int index)
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
				if (index < 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - index < this.dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] objects = (object[])array;
				if (objects == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
				}
				try
				{
					this.dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
					{
						object[] objects = objects;
						int index2 = index;
						index = index2 + 1;
						objects[index2] = node.Item.Key;
						return true;
					});
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
				}
			}

			// Token: 0x17000F95 RID: 3989
			// (get) Token: 0x0600448B RID: 17547 RVA: 0x0011F78C File Offset: 0x0011D98C
			[__DynamicallyInvokable]
			public int Count
			{
				[__DynamicallyInvokable]
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x17000F96 RID: 3990
			// (get) Token: 0x0600448C RID: 17548 RVA: 0x0011F799 File Offset: 0x0011D999
			[__DynamicallyInvokable]
			bool ICollection<!0>.IsReadOnly
			{
				[__DynamicallyInvokable]
				get
				{
					return true;
				}
			}

			// Token: 0x0600448D RID: 17549 RVA: 0x0011F79C File Offset: 0x0011D99C
			[__DynamicallyInvokable]
			void ICollection<!0>.Add(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x0600448E RID: 17550 RVA: 0x0011F7A5 File Offset: 0x0011D9A5
			[__DynamicallyInvokable]
			void ICollection<!0>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x0600448F RID: 17551 RVA: 0x0011F7AE File Offset: 0x0011D9AE
			[__DynamicallyInvokable]
			bool ICollection<!0>.Contains(TKey item)
			{
				return this.dictionary.ContainsKey(item);
			}

			// Token: 0x06004490 RID: 17552 RVA: 0x0011F7BC File Offset: 0x0011D9BC
			[__DynamicallyInvokable]
			bool ICollection<!0>.Remove(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
				return false;
			}

			// Token: 0x17000F97 RID: 3991
			// (get) Token: 0x06004491 RID: 17553 RVA: 0x0011F7C6 File Offset: 0x0011D9C6
			[__DynamicallyInvokable]
			bool ICollection.IsSynchronized
			{
				[__DynamicallyInvokable]
				get
				{
					return false;
				}
			}

			// Token: 0x17000F98 RID: 3992
			// (get) Token: 0x06004492 RID: 17554 RVA: 0x0011F7C9 File Offset: 0x0011D9C9
			[__DynamicallyInvokable]
			object ICollection.SyncRoot
			{
				[__DynamicallyInvokable]
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			// Token: 0x0400353E RID: 13630
			private SortedDictionary<TKey, TValue> dictionary;

			// Token: 0x02000929 RID: 2345
			[__DynamicallyInvokable]
			public struct Enumerator : IEnumerator<TKey>, IDisposable, IEnumerator
			{
				// Token: 0x06004696 RID: 18070 RVA: 0x00126D01 File Offset: 0x00124F01
				internal Enumerator(SortedDictionary<TKey, TValue> dictionary)
				{
					this.dictEnum = dictionary.GetEnumerator();
				}

				// Token: 0x06004697 RID: 18071 RVA: 0x00126D0F File Offset: 0x00124F0F
				[__DynamicallyInvokable]
				public void Dispose()
				{
					this.dictEnum.Dispose();
				}

				// Token: 0x06004698 RID: 18072 RVA: 0x00126D1C File Offset: 0x00124F1C
				[__DynamicallyInvokable]
				public bool MoveNext()
				{
					return this.dictEnum.MoveNext();
				}

				// Token: 0x17000FEC RID: 4076
				// (get) Token: 0x06004699 RID: 18073 RVA: 0x00126D2C File Offset: 0x00124F2C
				[__DynamicallyInvokable]
				public TKey Current
				{
					[__DynamicallyInvokable]
					get
					{
						KeyValuePair<TKey, TValue> keyValuePair = this.dictEnum.Current;
						return keyValuePair.Key;
					}
				}

				// Token: 0x17000FED RID: 4077
				// (get) Token: 0x0600469A RID: 18074 RVA: 0x00126D4C File Offset: 0x00124F4C
				[__DynamicallyInvokable]
				object IEnumerator.Current
				{
					[__DynamicallyInvokable]
					get
					{
						if (this.dictEnum.NotStartedOrEnded)
						{
							ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
						}
						return this.Current;
					}
				}

				// Token: 0x0600469B RID: 18075 RVA: 0x00126D6D File Offset: 0x00124F6D
				[__DynamicallyInvokable]
				void IEnumerator.Reset()
				{
					this.dictEnum.Reset();
				}

				// Token: 0x04003DCB RID: 15819
				private SortedDictionary<TKey, TValue>.Enumerator dictEnum;
			}
		}

		// Token: 0x020007FC RID: 2044
		[DebuggerTypeProxy(typeof(System_DictionaryValueCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[__DynamicallyInvokable]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			// Token: 0x06004493 RID: 17555 RVA: 0x0011F7D6 File Offset: 0x0011D9D6
			[__DynamicallyInvokable]
			public ValueCollection(SortedDictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
				}
				this.dictionary = dictionary;
			}

			// Token: 0x06004494 RID: 17556 RVA: 0x0011F7EE File Offset: 0x0011D9EE
			[__DynamicallyInvokable]
			public SortedDictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			// Token: 0x06004495 RID: 17557 RVA: 0x0011F7FB File Offset: 0x0011D9FB
			[__DynamicallyInvokable]
			IEnumerator<TValue> IEnumerable<!1>.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			// Token: 0x06004496 RID: 17558 RVA: 0x0011F80D File Offset: 0x0011DA0D
			[__DynamicallyInvokable]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			// Token: 0x06004497 RID: 17559 RVA: 0x0011F820 File Offset: 0x0011DA20
			[__DynamicallyInvokable]
			public void CopyTo(TValue[] array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (index < 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
				}
				if (array.Length - index < this.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				this.dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					TValue[] array2 = array;
					int index2 = index;
					index = index2 + 1;
					array2[index2] = node.Item.Value;
					return true;
				});
			}

			// Token: 0x06004498 RID: 17560 RVA: 0x0011F89C File Offset: 0x0011DA9C
			[__DynamicallyInvokable]
			void ICollection.CopyTo(Array array, int index)
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
				if (index < 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - index < this.dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] objects = (object[])array;
				if (objects == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
				}
				try
				{
					this.dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
					{
						object[] objects = objects;
						int index2 = index;
						index = index2 + 1;
						objects[index2] = node.Item.Value;
						return true;
					});
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
				}
			}

			// Token: 0x17000F99 RID: 3993
			// (get) Token: 0x06004499 RID: 17561 RVA: 0x0011F97C File Offset: 0x0011DB7C
			[__DynamicallyInvokable]
			public int Count
			{
				[__DynamicallyInvokable]
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x17000F9A RID: 3994
			// (get) Token: 0x0600449A RID: 17562 RVA: 0x0011F989 File Offset: 0x0011DB89
			[__DynamicallyInvokable]
			bool ICollection<!1>.IsReadOnly
			{
				[__DynamicallyInvokable]
				get
				{
					return true;
				}
			}

			// Token: 0x0600449B RID: 17563 RVA: 0x0011F98C File Offset: 0x0011DB8C
			[__DynamicallyInvokable]
			void ICollection<!1>.Add(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x0600449C RID: 17564 RVA: 0x0011F995 File Offset: 0x0011DB95
			[__DynamicallyInvokable]
			void ICollection<!1>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x0600449D RID: 17565 RVA: 0x0011F99E File Offset: 0x0011DB9E
			[__DynamicallyInvokable]
			bool ICollection<!1>.Contains(TValue item)
			{
				return this.dictionary.ContainsValue(item);
			}

			// Token: 0x0600449E RID: 17566 RVA: 0x0011F9AC File Offset: 0x0011DBAC
			[__DynamicallyInvokable]
			bool ICollection<!1>.Remove(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
				return false;
			}

			// Token: 0x17000F9B RID: 3995
			// (get) Token: 0x0600449F RID: 17567 RVA: 0x0011F9B6 File Offset: 0x0011DBB6
			[__DynamicallyInvokable]
			bool ICollection.IsSynchronized
			{
				[__DynamicallyInvokable]
				get
				{
					return false;
				}
			}

			// Token: 0x17000F9C RID: 3996
			// (get) Token: 0x060044A0 RID: 17568 RVA: 0x0011F9B9 File Offset: 0x0011DBB9
			[__DynamicallyInvokable]
			object ICollection.SyncRoot
			{
				[__DynamicallyInvokable]
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			// Token: 0x0400353F RID: 13631
			private SortedDictionary<TKey, TValue> dictionary;

			// Token: 0x0200092C RID: 2348
			[__DynamicallyInvokable]
			public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
			{
				// Token: 0x060046A0 RID: 18080 RVA: 0x00126DFA File Offset: 0x00124FFA
				internal Enumerator(SortedDictionary<TKey, TValue> dictionary)
				{
					this.dictEnum = dictionary.GetEnumerator();
				}

				// Token: 0x060046A1 RID: 18081 RVA: 0x00126E08 File Offset: 0x00125008
				[__DynamicallyInvokable]
				public void Dispose()
				{
					this.dictEnum.Dispose();
				}

				// Token: 0x060046A2 RID: 18082 RVA: 0x00126E15 File Offset: 0x00125015
				[__DynamicallyInvokable]
				public bool MoveNext()
				{
					return this.dictEnum.MoveNext();
				}

				// Token: 0x17000FEE RID: 4078
				// (get) Token: 0x060046A3 RID: 18083 RVA: 0x00126E24 File Offset: 0x00125024
				[__DynamicallyInvokable]
				public TValue Current
				{
					[__DynamicallyInvokable]
					get
					{
						KeyValuePair<TKey, TValue> keyValuePair = this.dictEnum.Current;
						return keyValuePair.Value;
					}
				}

				// Token: 0x17000FEF RID: 4079
				// (get) Token: 0x060046A4 RID: 18084 RVA: 0x00126E44 File Offset: 0x00125044
				[__DynamicallyInvokable]
				object IEnumerator.Current
				{
					[__DynamicallyInvokable]
					get
					{
						if (this.dictEnum.NotStartedOrEnded)
						{
							ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
						}
						return this.Current;
					}
				}

				// Token: 0x060046A5 RID: 18085 RVA: 0x00126E65 File Offset: 0x00125065
				[__DynamicallyInvokable]
				void IEnumerator.Reset()
				{
					this.dictEnum.Reset();
				}

				// Token: 0x04003DD0 RID: 15824
				private SortedDictionary<TKey, TValue>.Enumerator dictEnum;
			}
		}

		// Token: 0x020007FD RID: 2045
		[Serializable]
		internal class KeyValuePairComparer : Comparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x060044A1 RID: 17569 RVA: 0x0011F9C6 File Offset: 0x0011DBC6
			public KeyValuePairComparer(IComparer<TKey> keyComparer)
			{
				if (keyComparer == null)
				{
					this.keyComparer = Comparer<TKey>.Default;
					return;
				}
				this.keyComparer = keyComparer;
			}

			// Token: 0x060044A2 RID: 17570 RVA: 0x0011F9E4 File Offset: 0x0011DBE4
			public override int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return this.keyComparer.Compare(x.Key, y.Key);
			}

			// Token: 0x04003540 RID: 13632
			internal IComparer<TKey> keyComparer;
		}
	}
}
