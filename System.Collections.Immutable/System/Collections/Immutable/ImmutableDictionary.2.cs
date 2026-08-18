using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x0200001D RID: 29
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableDictionaryDebuggerProxy<, >))]
	public sealed class ImmutableDictionary<TKey, TValue> : IImmutableDictionary<!0, !1>, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable, IImmutableDictionaryInternal<TKey, TValue>, IHashKeyCollection<TKey>, IDictionary<!0, !1>, ICollection<KeyValuePair<!0, !1>>, IDictionary, ICollection
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00004CA9 File Offset: 0x00002EA9
		private ImmutableDictionary(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers, int count) : this(Requires.NotNullPassthrough<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers"))
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
			root.Freeze(ImmutableDictionary<TKey, TValue>.s_FreezeBucketAction);
			this._root = root;
			this._count = count;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004CE0 File Offset: 0x00002EE0
		private ImmutableDictionary(ImmutableDictionary<TKey, TValue>.Comparers comparers = null)
		{
			this._comparers = (comparers ?? ImmutableDictionary<TKey, TValue>.Comparers.Get(EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default));
			this._root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004D0D File Offset: 0x00002F0D
		public ImmutableDictionary<TKey, TValue> Clear()
		{
			if (!this.IsEmpty)
			{
				return ImmutableDictionary<TKey, TValue>.EmptyWithComparers(this._comparers);
			}
			return this;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004D24 File Offset: 0x00002F24
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00004D2C File Offset: 0x00002F2C
		public bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00004D37 File Offset: 0x00002F37
		public IEqualityComparer<TKey> KeyComparer
		{
			get
			{
				return this._comparers.KeyComparer;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00004D44 File Offset: 0x00002F44
		public IEqualityComparer<TValue> ValueComparer
		{
			get
			{
				return this._comparers.ValueComparer;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00004D51 File Offset: 0x00002F51
		public IEnumerable<TKey> Keys
		{
			get
			{
				foreach (KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> keyValuePair in this._root)
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair2 in keyValuePair.Value)
					{
						yield return keyValuePair2.Key;
					}
					ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator enumerator2 = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
				}
				SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator enumerator = default(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00004D61 File Offset: 0x00002F61
		public IEnumerable<TValue> Values
		{
			get
			{
				foreach (KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> keyValuePair in this._root)
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair2 in keyValuePair.Value)
					{
						yield return keyValuePair2.Value;
					}
					ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator enumerator2 = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
				}
				SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator enumerator = default(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004D71 File Offset: 0x00002F71
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00004D79 File Offset: 0x00002F79
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00004D81 File Offset: 0x00002F81
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004D89 File Offset: 0x00002F89
		private ImmutableDictionary<TKey, TValue>.MutationInput Origin
		{
			get
			{
				return new ImmutableDictionary<TKey, TValue>.MutationInput(this);
			}
		}

		// Token: 0x17000044 RID: 68
		public TValue this[TKey key]
		{
			get
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				TValue result;
				if (this.TryGetValue(key, out result))
				{
					return result;
				}
				throw new KeyNotFoundException();
			}
		}

		// Token: 0x17000045 RID: 69
		TValue IDictionary<!0, !1>.this[TKey key]
		{
			get
			{
				return this[key];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000038D6 File Offset: 0x00001AD6
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004DD1 File Offset: 0x00002FD1
		public ImmutableDictionary<TKey, TValue>.Builder ToBuilder()
		{
			return new ImmutableDictionary<TKey, TValue>.Builder(this);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004DDC File Offset: 0x00002FDC
		public ImmutableDictionary<TKey, TValue> Add(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent, new ImmutableDictionary<TKey, TValue>.MutationInput(this)).Finalize(this);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004E0B File Offset: 0x0000300B
		public ImmutableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(pairs, "pairs");
			return this.AddRange(pairs, false);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004E20 File Offset: 0x00003020
		public ImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue, new ImmutableDictionary<TKey, TValue>.MutationInput(this)).Finalize(this);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004E50 File Offset: 0x00003050
		public ImmutableDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return ImmutableDictionary<TKey, TValue>.AddRange(items, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue).Finalize(this);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004E80 File Offset: 0x00003080
		public ImmutableDictionary<TKey, TValue> Remove(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Remove(key, new ImmutableDictionary<TKey, TValue>.MutationInput(this)).Finalize(this);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004EB0 File Offset: 0x000030B0
		public ImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
		{
			Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
			int num = this._count;
			SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> sortedInt32KeyNode = this._root;
			foreach (TKey tkey in keys)
			{
				int hashCode = this.KeyComparer.GetHashCode(tkey);
				ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
				if (sortedInt32KeyNode.TryGetValue(hashCode, out hashBucket))
				{
					ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
					ImmutableDictionary<TKey, TValue>.HashBucket newBucket = hashBucket.Remove(tkey, this._comparers.KeyOnlyComparer, out operationResult);
					sortedInt32KeyNode = ImmutableDictionary<TKey, TValue>.UpdateRoot(sortedInt32KeyNode, hashCode, newBucket, this._comparers.HashBucketEqualityComparer);
					if (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged)
					{
						num--;
					}
				}
			}
			return this.Wrap(sortedInt32KeyNode, num);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004F64 File Offset: 0x00003164
		public bool ContainsKey(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.ContainsKey(key, new ImmutableDictionary<TKey, TValue>.MutationInput(this));
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004F7D File Offset: 0x0000317D
		public bool Contains(KeyValuePair<TKey, TValue> pair)
		{
			return ImmutableDictionary<TKey, TValue>.Contains(pair, this.Origin);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00004F8B File Offset: 0x0000318B
		public bool TryGetValue(TKey key, out TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.TryGetValue(key, this.Origin, out value);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004FA5 File Offset: 0x000031A5
		public bool TryGetKey(TKey equalKey, out TKey actualKey)
		{
			Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
			return ImmutableDictionary<TKey, TValue>.TryGetKey(equalKey, this.Origin, out actualKey);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00004FC0 File Offset: 0x000031C0
		public ImmutableDictionary<TKey, TValue> WithComparers(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			if (keyComparer == null)
			{
				keyComparer = EqualityComparer<TKey>.Default;
			}
			if (valueComparer == null)
			{
				valueComparer = EqualityComparer<TValue>.Default;
			}
			if (this.KeyComparer != keyComparer)
			{
				return new ImmutableDictionary<TKey, TValue>(ImmutableDictionary<TKey, TValue>.Comparers.Get(keyComparer, valueComparer)).AddRange(this, true);
			}
			if (this.ValueComparer == valueComparer)
			{
				return this;
			}
			ImmutableDictionary<TKey, TValue>.Comparers comparers = this._comparers.WithValueComparer(valueComparer);
			return new ImmutableDictionary<TKey, TValue>(this._root, comparers, this._count);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005028 File Offset: 0x00003228
		public ImmutableDictionary<TKey, TValue> WithComparers(IEqualityComparer<TKey> keyComparer)
		{
			return this.WithComparers(keyComparer, this._comparers.ValueComparer);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000503C File Offset: 0x0000323C
		public bool ContainsValue(TValue value)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				if (this.ValueComparer.Equals(value, keyValuePair.Value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000050A0 File Offset: 0x000032A0
		public ImmutableDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new ImmutableDictionary<TKey, TValue>.Enumerator(this._root, null);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000050AE File Offset: 0x000032AE
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			return this.Add(key, value);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000050B8 File Offset: 0x000032B8
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.SetItem(TKey key, TValue value)
		{
			return this.SetItem(key, value);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000050C2 File Offset: 0x000032C2
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return this.SetItems(items);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000050CB File Offset: 0x000032CB
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			return this.AddRange(pairs);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000050D4 File Offset: 0x000032D4
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.RemoveRange(IEnumerable<TKey> keys)
		{
			return this.RemoveRange(keys);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000050DD File Offset: 0x000032DD
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Remove(TKey key)
		{
			return this.Remove(key);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00002D65 File Offset: 0x00000F65
		void IDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00002D65 File Offset: 0x00000F65
		bool IDictionary<!0, !1>.Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00002D65 File Offset: 0x00000F65
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00002D65 File Offset: 0x00000F65
		void ICollection<KeyValuePair<!0, !1>>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00002D65 File Offset: 0x00000F65
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000510C File Offset: 0x0000330C
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				array[arrayIndex++] = keyValuePair;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000038D6 File Offset: 0x00001AD6
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000038D6 File Offset: 0x00001AD6
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00004D79 File Offset: 0x00002F79
		ICollection IDictionary.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00004D81 File Offset: 0x00002F81
		ICollection IDictionary.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000051AE File Offset: 0x000033AE
		internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00002D65 File Offset: 0x00000F65
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000051BD File Offset: 0x000033BD
		bool IDictionary.Contains(object key)
		{
			return this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000051CB File Offset: 0x000033CB
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00002D65 File Offset: 0x00000F65
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700004C RID: 76
		object IDictionary.this[object key]
		{
			get
			{
				return this[(TKey)((object)key)];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00002D65 File Offset: 0x00000F65
		void IDictionary.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005208 File Offset: 0x00003408
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Requires.NotNull<Array>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), new int[]
				{
					arrayIndex++
				});
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000052C4 File Offset: 0x000034C4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000038D6 File Offset: 0x00001AD6
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000052CA File Offset: 0x000034CA
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000052CA File Offset: 0x000034CA
		[ExcludeFromCodeCoverage]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000052E4 File Offset: 0x000034E4
		private static ImmutableDictionary<TKey, TValue> EmptyWithComparers(ImmutableDictionary<TKey, TValue>.Comparers comparers)
		{
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers");
			if (ImmutableDictionary<TKey, TValue>.Empty._comparers != comparers)
			{
				return new ImmutableDictionary<TKey, TValue>(comparers);
			}
			return ImmutableDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000530C File Offset: 0x0000350C
		private static bool TryCastToImmutableMap(IEnumerable<KeyValuePair<TKey, TValue>> sequence, out ImmutableDictionary<TKey, TValue> other)
		{
			other = (sequence as ImmutableDictionary<TKey, TValue>);
			if (other != null)
			{
				return true;
			}
			ImmutableDictionary<TKey, TValue>.Builder builder = sequence as ImmutableDictionary<TKey, TValue>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000533C File Offset: 0x0000353C
		private static bool ContainsKey(TKey key, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			TValue tvalue;
			return origin.Root.TryGetValue(hashCode, out hashBucket) && hashBucket.TryGetValue(key, origin.KeyOnlyComparer, out tvalue);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000537C File Offset: 0x0000357C
		private static bool Contains(KeyValuePair<TKey, TValue> keyValuePair, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			int hashCode = origin.KeyComparer.GetHashCode(keyValuePair.Key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			TValue x;
			return origin.Root.TryGetValue(hashCode, out hashBucket) && hashBucket.TryGetValue(keyValuePair.Key, origin.KeyOnlyComparer, out x) && origin.ValueComparer.Equals(x, keyValuePair.Value);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000053E0 File Offset: 0x000035E0
		private static bool TryGetValue(TKey key, ImmutableDictionary<TKey, TValue>.MutationInput origin, out TValue value)
		{
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(hashCode, out hashBucket))
			{
				return hashBucket.TryGetValue(key, origin.KeyOnlyComparer, out value);
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005428 File Offset: 0x00003628
		private static bool TryGetKey(TKey equalKey, ImmutableDictionary<TKey, TValue>.MutationInput origin, out TKey actualKey)
		{
			int hashCode = origin.KeyComparer.GetHashCode(equalKey);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(hashCode, out hashBucket))
			{
				return hashBucket.TryGetKey(equalKey, origin.KeyOnlyComparer, out actualKey);
			}
			actualKey = equalKey;
			return false;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005470 File Offset: 0x00003670
		private static ImmutableDictionary<TKey, TValue>.MutationResult Add(TKey key, TValue value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior behavior, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
			ImmutableDictionary<TKey, TValue>.HashBucket newBucket = origin.Root.GetValueOrDefault(hashCode).Add(key, value, origin.KeyOnlyComparer, origin.ValueComparer, behavior, out operationResult);
			if (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired)
			{
				return new ImmutableDictionary<TKey, TValue>.MutationResult(origin);
			}
			return new ImmutableDictionary<TKey, TValue>.MutationResult(ImmutableDictionary<TKey, TValue>.UpdateRoot(origin.Root, hashCode, newBucket, origin.HashBucketComparer), (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged) ? 1 : 0);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000054EC File Offset: 0x000036EC
		private static ImmutableDictionary<TKey, TValue>.MutationResult AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items, ImmutableDictionary<TKey, TValue>.MutationInput origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior collisionBehavior = ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			int num = 0;
			SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> sortedInt32KeyNode = origin.Root;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
			{
				int hashCode = origin.KeyComparer.GetHashCode(keyValuePair.Key);
				ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
				ImmutableDictionary<TKey, TValue>.HashBucket newBucket = sortedInt32KeyNode.GetValueOrDefault(hashCode).Add(keyValuePair.Key, keyValuePair.Value, origin.KeyOnlyComparer, origin.ValueComparer, collisionBehavior, out operationResult);
				sortedInt32KeyNode = ImmutableDictionary<TKey, TValue>.UpdateRoot(sortedInt32KeyNode, hashCode, newBucket, origin.HashBucketComparer);
				if (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged)
				{
					num++;
				}
			}
			return new ImmutableDictionary<TKey, TValue>.MutationResult(sortedInt32KeyNode, num);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000055AC File Offset: 0x000037AC
		private static ImmutableDictionary<TKey, TValue>.MutationResult Remove(TKey key, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(hashCode, out hashBucket))
			{
				ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
				return new ImmutableDictionary<TKey, TValue>.MutationResult(ImmutableDictionary<TKey, TValue>.UpdateRoot(origin.Root, hashCode, hashBucket.Remove(key, origin.KeyOnlyComparer, out operationResult), origin.HashBucketComparer), (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged) ? -1 : 0);
			}
			return new ImmutableDictionary<TKey, TValue>.MutationResult(origin);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005614 File Offset: 0x00003814
		private static SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> UpdateRoot(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, int hashCode, ImmutableDictionary<TKey, TValue>.HashBucket newBucket, IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> hashBucketComparer)
		{
			bool flag;
			if (newBucket.IsEmpty)
			{
				return root.Remove(hashCode, out flag);
			}
			bool flag2;
			return root.SetItem(hashCode, newBucket, hashBucketComparer, out flag2, out flag);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005641 File Offset: 0x00003841
		private static ImmutableDictionary<TKey, TValue> Wrap(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers, int count)
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers");
			Requires.Range(count >= 0, "count", null);
			return new ImmutableDictionary<TKey, TValue>(root, comparers, count);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005673 File Offset: 0x00003873
		private ImmutableDictionary<TKey, TValue> Wrap(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, int adjustedCountIfDifferentRoot)
		{
			if (root == null)
			{
				return this.Clear();
			}
			if (this._root == root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableDictionary<TKey, TValue>(root, this._comparers, adjustedCountIfDifferentRoot);
			}
			return this.Clear();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000056A8 File Offset: 0x000038A8
		private ImmutableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs, bool avoidToHashMap)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(pairs, "pairs");
			ImmutableDictionary<TKey, TValue> immutableDictionary;
			if (this.IsEmpty && !avoidToHashMap && ImmutableDictionary<TKey, TValue>.TryCastToImmutableMap(pairs, out immutableDictionary))
			{
				return immutableDictionary.WithComparers(this.KeyComparer, this.ValueComparer);
			}
			return ImmutableDictionary<TKey, TValue>.AddRange(pairs, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent).Finalize(this);
		}

		// Token: 0x04000013 RID: 19
		public static readonly ImmutableDictionary<TKey, TValue> Empty = new ImmutableDictionary<TKey, TValue>(null);

		// Token: 0x04000014 RID: 20
		private static readonly Action<KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket>> s_FreezeBucketAction = delegate(KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> kv)
		{
			kv.Value.Freeze();
		};

		// Token: 0x04000015 RID: 21
		private readonly int _count;

		// Token: 0x04000016 RID: 22
		private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

		// Token: 0x04000017 RID: 23
		private readonly ImmutableDictionary<TKey, TValue>.Comparers _comparers;

		// Token: 0x0200004A RID: 74
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableDictionaryBuilderDebuggerProxy<, >))]
		public sealed class Builder : IDictionary<!0, !1>, ICollection<KeyValuePair<!0, !1>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
		{
			// Token: 0x060003DD RID: 989 RVA: 0x0000A5FC File Offset: 0x000087FC
			internal Builder(ImmutableDictionary<TKey, TValue> map)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(map, "map");
				this._root = map._root;
				this._count = map._count;
				this._comparers = map._comparers;
				this._immutable = map;
			}

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060003DE RID: 990 RVA: 0x0000A650 File Offset: 0x00008850
			// (set) Token: 0x060003DF RID: 991 RVA: 0x0000A660 File Offset: 0x00008860
			public IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this._comparers.KeyComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<TKey>>(value, "value");
					if (value != this.KeyComparer)
					{
						ImmutableDictionary<TKey, TValue>.Comparers comparers = ImmutableDictionary<TKey, TValue>.Comparers.Get(value, this.ValueComparer);
						ImmutableDictionary<TKey, TValue>.MutationInput origin = new ImmutableDictionary<TKey, TValue>.MutationInput(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode, comparers, 0);
						ImmutableDictionary<TKey, TValue>.MutationResult mutationResult = ImmutableDictionary<TKey, TValue>.AddRange(this, origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent);
						this._immutable = null;
						this._comparers = comparers;
						this._count = mutationResult.CountAdjustment;
						this.Root = mutationResult.Root;
					}
				}
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000A6CD File Offset: 0x000088CD
			// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000A6DA File Offset: 0x000088DA
			public IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._comparers.ValueComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<TValue>>(value, "value");
					if (value != this.ValueComparer)
					{
						this._comparers = this._comparers.WithValueComparer(value);
						this._immutable = null;
					}
				}
			}

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000A709 File Offset: 0x00008909
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060003E3 RID: 995 RVA: 0x000020FC File Offset: 0x000002FC
			bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000A714 File Offset: 0x00008914
			public IEnumerable<TKey> Keys
			{
				get
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
					{
						yield return keyValuePair.Key;
					}
					ImmutableDictionary<TKey, TValue>.Enumerator enumerator = default(ImmutableDictionary<TKey, TValue>.Enumerator);
					yield break;
					yield break;
				}
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000A724 File Offset: 0x00008924
			ICollection<TKey> IDictionary<!0, !1>.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000A737 File Offset: 0x00008937
			public IEnumerable<TValue> Values
			{
				get
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
					{
						yield return keyValuePair.Value;
					}
					ImmutableDictionary<TKey, TValue>.Enumerator enumerator = default(ImmutableDictionary<TKey, TValue>.Enumerator);
					yield break;
					yield break;
				}
			}

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000A747 File Offset: 0x00008947
			ICollection<TValue> IDictionary<!0, !1>.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060003E8 RID: 1000 RVA: 0x000020FC File Offset: 0x000002FC
			bool IDictionary.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000020FC File Offset: 0x000002FC
			bool IDictionary.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000A724 File Offset: 0x00008924
			ICollection IDictionary.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000A747 File Offset: 0x00008947
			ICollection IDictionary.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000A786 File Offset: 0x00008986
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			object ICollection.SyncRoot
			{
				get
				{
					if (this._syncRoot == null)
					{
						Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
					}
					return this._syncRoot;
				}
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x060003ED RID: 1005 RVA: 0x000020FC File Offset: 0x000002FC
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060003EE RID: 1006 RVA: 0x0000A7AB File Offset: 0x000089AB
			void IDictionary.Add(object key, object value)
			{
				this.Add((TKey)((object)key), (TValue)((object)value));
			}

			// Token: 0x060003EF RID: 1007 RVA: 0x0000A7BF File Offset: 0x000089BF
			bool IDictionary.Contains(object key)
			{
				return this.ContainsKey((TKey)((object)key));
			}

			// Token: 0x060003F0 RID: 1008 RVA: 0x0000A7CD File Offset: 0x000089CD
			IDictionaryEnumerator IDictionary.GetEnumerator()
			{
				return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
			}

			// Token: 0x060003F1 RID: 1009 RVA: 0x0000A7DF File Offset: 0x000089DF
			void IDictionary.Remove(object key)
			{
				this.Remove((TKey)((object)key));
			}

			// Token: 0x170000B9 RID: 185
			object IDictionary.this[object key]
			{
				get
				{
					return this[(TKey)((object)key)];
				}
				set
				{
					this[(TKey)((object)key)] = (TValue)((object)value);
				}
			}

			// Token: 0x060003F4 RID: 1012 RVA: 0x0000A818 File Offset: 0x00008A18
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), new int[]
					{
						arrayIndex++
					});
				}
			}

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000A8D4 File Offset: 0x00008AD4
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000A8DC File Offset: 0x00008ADC
			private ImmutableDictionary<TKey, TValue>.MutationInput Origin
			{
				get
				{
					return new ImmutableDictionary<TKey, TValue>.MutationInput(this.Root, this._comparers, this._count);
				}
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000A8F5 File Offset: 0x00008AF5
			// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0000A8FD File Offset: 0x00008AFD
			private SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
				set
				{
					this._version++;
					if (this._root != value)
					{
						this._root = value;
						this._immutable = null;
					}
				}
			}

			// Token: 0x170000BD RID: 189
			public TValue this[TKey key]
			{
				get
				{
					TValue result;
					if (this.TryGetValue(key, out result))
					{
						return result;
					}
					throw new KeyNotFoundException();
				}
				set
				{
					ImmutableDictionary<TKey, TValue>.MutationResult result = ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue, this.Origin);
					this.Apply(result);
				}
			}

			// Token: 0x060003FB RID: 1019 RVA: 0x0000A968 File Offset: 0x00008B68
			public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult result = ImmutableDictionary<TKey, TValue>.AddRange(items, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent);
				this.Apply(result);
			}

			// Token: 0x060003FC RID: 1020 RVA: 0x0000A98C File Offset: 0x00008B8C
			public void RemoveRange(IEnumerable<TKey> keys)
			{
				Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
				foreach (TKey key in keys)
				{
					this.Remove(key);
				}
			}

			// Token: 0x060003FD RID: 1021 RVA: 0x0000A9E0 File Offset: 0x00008BE0
			public ImmutableDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new ImmutableDictionary<TKey, TValue>.Enumerator(this._root, this);
			}

			// Token: 0x060003FE RID: 1022 RVA: 0x0000A9F0 File Offset: 0x00008BF0
			public TValue GetValueOrDefault(TKey key)
			{
				return this.GetValueOrDefault(key, default(TValue));
			}

			// Token: 0x060003FF RID: 1023 RVA: 0x0000AA10 File Offset: 0x00008C10
			public TValue GetValueOrDefault(TKey key, TValue defaultValue)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				TValue result;
				if (this.TryGetValue(key, out result))
				{
					return result;
				}
				return defaultValue;
			}

			// Token: 0x06000400 RID: 1024 RVA: 0x0000AA36 File Offset: 0x00008C36
			public ImmutableDictionary<TKey, TValue> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableDictionary<TKey, TValue>.Wrap(this._root, this._comparers, this._count);
				}
				return this._immutable;
			}

			// Token: 0x06000401 RID: 1025 RVA: 0x0000AA64 File Offset: 0x00008C64
			public void Add(TKey key, TValue value)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult result = ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent, this.Origin);
				this.Apply(result);
			}

			// Token: 0x06000402 RID: 1026 RVA: 0x0000AA88 File Offset: 0x00008C88
			public bool ContainsKey(TKey key)
			{
				return ImmutableDictionary<TKey, TValue>.ContainsKey(key, this.Origin);
			}

			// Token: 0x06000403 RID: 1027 RVA: 0x0000AA98 File Offset: 0x00008C98
			public bool ContainsValue(TValue value)
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					if (this.ValueComparer.Equals(value, keyValuePair.Value))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000404 RID: 1028 RVA: 0x0000AAFC File Offset: 0x00008CFC
			public bool Remove(TKey key)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult result = ImmutableDictionary<TKey, TValue>.Remove(key, this.Origin);
				return this.Apply(result);
			}

			// Token: 0x06000405 RID: 1029 RVA: 0x0000AB1D File Offset: 0x00008D1D
			public bool TryGetValue(TKey key, out TValue value)
			{
				return ImmutableDictionary<TKey, TValue>.TryGetValue(key, this.Origin, out value);
			}

			// Token: 0x06000406 RID: 1030 RVA: 0x0000AB2C File Offset: 0x00008D2C
			public bool TryGetKey(TKey equalKey, out TKey actualKey)
			{
				return ImmutableDictionary<TKey, TValue>.TryGetKey(equalKey, this.Origin, out actualKey);
			}

			// Token: 0x06000407 RID: 1031 RVA: 0x0000AB3B File Offset: 0x00008D3B
			public void Add(KeyValuePair<TKey, TValue> item)
			{
				this.Add(item.Key, item.Value);
			}

			// Token: 0x06000408 RID: 1032 RVA: 0x0000AB51 File Offset: 0x00008D51
			public void Clear()
			{
				this.Root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;
				this._count = 0;
			}

			// Token: 0x06000409 RID: 1033 RVA: 0x0000AB65 File Offset: 0x00008D65
			public bool Contains(KeyValuePair<TKey, TValue> item)
			{
				return ImmutableDictionary<TKey, TValue>.Contains(item, this.Origin);
			}

			// Token: 0x0600040A RID: 1034 RVA: 0x0000AB74 File Offset: 0x00008D74
			void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array[arrayIndex++] = keyValuePair;
				}
			}

			// Token: 0x0600040B RID: 1035 RVA: 0x0000ABD4 File Offset: 0x00008DD4
			public bool Remove(KeyValuePair<TKey, TValue> item)
			{
				return this.Contains(item) && this.Remove(item.Key);
			}

			// Token: 0x0600040C RID: 1036 RVA: 0x0000ABEE File Offset: 0x00008DEE
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600040D RID: 1037 RVA: 0x0000ABEE File Offset: 0x00008DEE
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600040E RID: 1038 RVA: 0x0000AC08 File Offset: 0x00008E08
			private bool Apply(ImmutableDictionary<TKey, TValue>.MutationResult result)
			{
				this.Root = result.Root;
				this._count += result.CountAdjustment;
				return result.CountAdjustment != 0;
			}

			// Token: 0x04000070 RID: 112
			private SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;

			// Token: 0x04000071 RID: 113
			private ImmutableDictionary<TKey, TValue>.Comparers _comparers;

			// Token: 0x04000072 RID: 114
			private int _count;

			// Token: 0x04000073 RID: 115
			private ImmutableDictionary<TKey, TValue> _immutable;

			// Token: 0x04000074 RID: 116
			private int _version;

			// Token: 0x04000075 RID: 117
			private object _syncRoot;
		}

		// Token: 0x0200004B RID: 75
		internal class Comparers : IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket>, IEqualityComparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x0600040F RID: 1039 RVA: 0x0000AC35 File Offset: 0x00008E35
			internal Comparers(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				this._keyComparer = keyComparer;
				this._valueComparer = valueComparer;
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000AC61 File Offset: 0x00008E61
			internal IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this._keyComparer;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000411 RID: 1041 RVA: 0x000052C4 File Offset: 0x000034C4
			internal IEqualityComparer<KeyValuePair<TKey, TValue>> KeyOnlyComparer
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000AC6C File Offset: 0x00008E6C
			internal IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._valueComparer;
				}
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000413 RID: 1043 RVA: 0x000052C4 File Offset: 0x000034C4
			internal IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> HashBucketEqualityComparer
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06000414 RID: 1044 RVA: 0x0000AC78 File Offset: 0x00008E78
			public bool Equals(ImmutableDictionary<TKey, TValue>.HashBucket x, ImmutableDictionary<TKey, TValue>.HashBucket y)
			{
				return x.AdditionalElements == y.AdditionalElements && this.KeyComparer.Equals(x.FirstValue.Key, y.FirstValue.Key) && this.ValueComparer.Equals(x.FirstValue.Value, y.FirstValue.Value);
			}

			// Token: 0x06000415 RID: 1045 RVA: 0x0000ACEC File Offset: 0x00008EEC
			public int GetHashCode(ImmutableDictionary<TKey, TValue>.HashBucket obj)
			{
				return this.KeyComparer.GetHashCode(obj.FirstValue.Key);
			}

			// Token: 0x06000416 RID: 1046 RVA: 0x0000AD13 File Offset: 0x00008F13
			bool IEqualityComparer<KeyValuePair<!0, !1>>.Equals(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return this._keyComparer.Equals(x.Key, y.Key);
			}

			// Token: 0x06000417 RID: 1047 RVA: 0x0000AD2E File Offset: 0x00008F2E
			int IEqualityComparer<KeyValuePair<!0, !1>>.GetHashCode(KeyValuePair<TKey, TValue> obj)
			{
				return this._keyComparer.GetHashCode(obj.Key);
			}

			// Token: 0x06000418 RID: 1048 RVA: 0x0000AD42 File Offset: 0x00008F42
			internal static ImmutableDictionary<TKey, TValue>.Comparers Get(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				if (keyComparer != ImmutableDictionary<TKey, TValue>.Comparers.Default.KeyComparer || valueComparer != ImmutableDictionary<TKey, TValue>.Comparers.Default.ValueComparer)
				{
					return new ImmutableDictionary<TKey, TValue>.Comparers(keyComparer, valueComparer);
				}
				return ImmutableDictionary<TKey, TValue>.Comparers.Default;
			}

			// Token: 0x06000419 RID: 1049 RVA: 0x0000AD81 File Offset: 0x00008F81
			internal ImmutableDictionary<TKey, TValue>.Comparers WithValueComparer(IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				if (this._valueComparer != valueComparer)
				{
					return ImmutableDictionary<TKey, TValue>.Comparers.Get(this.KeyComparer, valueComparer);
				}
				return this;
			}

			// Token: 0x04000076 RID: 118
			internal static readonly ImmutableDictionary<TKey, TValue>.Comparers Default = new ImmutableDictionary<TKey, TValue>.Comparers(EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default);

			// Token: 0x04000077 RID: 119
			private readonly IEqualityComparer<TKey> _keyComparer;

			// Token: 0x04000078 RID: 120
			private readonly IEqualityComparer<TValue> _valueComparer;
		}

		// Token: 0x0200004C RID: 76
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x0600041B RID: 1051 RVA: 0x0000ADBB File Offset: 0x00008FBB
			internal Enumerator(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Builder builder = null)
			{
				this._builder = builder;
				this._mapEnumerator = new SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator(root);
				this._bucketEnumerator = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000ADEE File Offset: 0x00008FEE
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					this._mapEnumerator.ThrowIfDisposed();
					return this._bucketEnumerator.Current;
				}
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000AE06 File Offset: 0x00009006
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x0000AE14 File Offset: 0x00009014
			public bool MoveNext()
			{
				this.ThrowIfChanged();
				if (this._bucketEnumerator.MoveNext())
				{
					return true;
				}
				if (this._mapEnumerator.MoveNext())
				{
					KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> keyValuePair = this._mapEnumerator.Current;
					this._bucketEnumerator = new ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator(keyValuePair.Value);
					return this._bucketEnumerator.MoveNext();
				}
				return false;
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x0000AE6E File Offset: 0x0000906E
			public void Reset()
			{
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._mapEnumerator.Reset();
				this._bucketEnumerator.Dispose();
				this._bucketEnumerator = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
			}

			// Token: 0x06000420 RID: 1056 RVA: 0x0000AEAE File Offset: 0x000090AE
			public void Dispose()
			{
				this._mapEnumerator.Dispose();
				this._bucketEnumerator.Dispose();
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x0000AEC6 File Offset: 0x000090C6
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x04000079 RID: 121
			private readonly ImmutableDictionary<TKey, TValue>.Builder _builder;

			// Token: 0x0400007A RID: 122
			private SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator _mapEnumerator;

			// Token: 0x0400007B RID: 123
			private ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator _bucketEnumerator;

			// Token: 0x0400007C RID: 124
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x0200004D RID: 77
		internal struct HashBucket : IEnumerable<KeyValuePair<!0, !1>>, IEnumerable, IEquatable<ImmutableDictionary<TKey, TValue>.HashBucket>
		{
			// Token: 0x06000422 RID: 1058 RVA: 0x0000AEEE File Offset: 0x000090EE
			private HashBucket(KeyValuePair<TKey, TValue> firstElement, ImmutableList<KeyValuePair<TKey, TValue>>.Node additionalElements = null)
			{
				this._firstValue = firstElement;
				this._additionalElements = (additionalElements ?? ImmutableList<KeyValuePair<TKey, TValue>>.Node.EmptyNode);
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000AF07 File Offset: 0x00009107
			internal bool IsEmpty
			{
				get
				{
					return this._additionalElements == null;
				}
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000AF12 File Offset: 0x00009112
			internal KeyValuePair<TKey, TValue> FirstValue
			{
				get
				{
					if (this.IsEmpty)
					{
						throw new InvalidOperationException();
					}
					return this._firstValue;
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000AF28 File Offset: 0x00009128
			internal ImmutableList<KeyValuePair<TKey, TValue>>.Node AdditionalElements
			{
				get
				{
					return this._additionalElements;
				}
			}

			// Token: 0x06000426 RID: 1062 RVA: 0x0000AF30 File Offset: 0x00009130
			public ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator GetEnumerator()
			{
				return new ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator(this);
			}

			// Token: 0x06000427 RID: 1063 RVA: 0x0000AF3D File Offset: 0x0000913D
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x0000AF3D File Offset: 0x0000913D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x00002D65 File Offset: 0x00000F65
			bool IEquatable<ImmutableDictionary<!0, !1>.HashBucket>.Equals(ImmutableDictionary<TKey, TValue>.HashBucket other)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x0000AF60 File Offset: 0x00009160
			internal ImmutableDictionary<TKey, TValue>.HashBucket Add(TKey key, TValue value, IEqualityComparer<KeyValuePair<TKey, TValue>> keyOnlyComparer, IEqualityComparer<TValue> valueComparer, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior behavior, out ImmutableDictionary<TKey, TValue>.OperationResult result)
			{
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, value);
				if (this.IsEmpty)
				{
					result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
					return new ImmutableDictionary<TKey, TValue>.HashBucket(keyValuePair, null);
				}
				if (keyOnlyComparer.Equals(keyValuePair, this._firstValue))
				{
					switch (behavior)
					{
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.AppliedWithoutSizeChange;
						return new ImmutableDictionary<TKey, TValue>.HashBucket(keyValuePair, this._additionalElements);
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.Skip:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent:
						if (!valueComparer.Equals(this._firstValue.Value, value))
						{
							throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.DuplicateKey, new object[]
							{
								key
							}));
						}
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowAlways:
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.DuplicateKey, new object[]
						{
							key
						}));
					default:
						throw new InvalidOperationException();
					}
				}
				else
				{
					int num = this._additionalElements.IndexOf(keyValuePair, keyOnlyComparer);
					if (num < 0)
					{
						result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
						return new ImmutableDictionary<TKey, TValue>.HashBucket(this._firstValue, this._additionalElements.Add(keyValuePair));
					}
					switch (behavior)
					{
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.AppliedWithoutSizeChange;
						return new ImmutableDictionary<TKey, TValue>.HashBucket(this._firstValue, this._additionalElements.ReplaceAt(num, keyValuePair));
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.Skip:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent:
						if (!valueComparer.Equals(this._additionalElements[num].Value, value))
						{
							throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.DuplicateKey, new object[]
							{
								key
							}));
						}
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowAlways:
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.DuplicateKey, new object[]
						{
							key
						}));
					default:
						throw new InvalidOperationException();
					}
				}
			}

			// Token: 0x0600042B RID: 1067 RVA: 0x0000B130 File Offset: 0x00009330
			internal ImmutableDictionary<TKey, TValue>.HashBucket Remove(TKey key, IEqualityComparer<KeyValuePair<TKey, TValue>> keyOnlyComparer, out ImmutableDictionary<TKey, TValue>.OperationResult result)
			{
				if (this.IsEmpty)
				{
					result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
					return this;
				}
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, default(TValue));
				if (keyOnlyComparer.Equals(this._firstValue, keyValuePair))
				{
					if (this._additionalElements.IsEmpty)
					{
						result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
						return default(ImmutableDictionary<TKey, TValue>.HashBucket);
					}
					int count = this._additionalElements.Left.Count;
					result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
					return new ImmutableDictionary<TKey, TValue>.HashBucket(this._additionalElements.Key, this._additionalElements.RemoveAt(count));
				}
				else
				{
					int num = this._additionalElements.IndexOf(keyValuePair, keyOnlyComparer);
					if (num < 0)
					{
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					}
					result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
					return new ImmutableDictionary<TKey, TValue>.HashBucket(this._firstValue, this._additionalElements.RemoveAt(num));
				}
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x0000B1F4 File Offset: 0x000093F4
			internal bool TryGetValue(TKey key, IEqualityComparer<KeyValuePair<TKey, TValue>> keyOnlyComparer, out TValue value)
			{
				if (this.IsEmpty)
				{
					value = default(TValue);
					return false;
				}
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, default(TValue));
				if (keyOnlyComparer.Equals(this._firstValue, keyValuePair))
				{
					value = this._firstValue.Value;
					return true;
				}
				int num = this._additionalElements.IndexOf(keyValuePair, keyOnlyComparer);
				if (num < 0)
				{
					value = default(TValue);
					return false;
				}
				value = this._additionalElements[num].Value;
				return true;
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x0000B280 File Offset: 0x00009480
			internal bool TryGetKey(TKey equalKey, IEqualityComparer<KeyValuePair<TKey, TValue>> keyOnlyComparer, out TKey actualKey)
			{
				if (this.IsEmpty)
				{
					actualKey = equalKey;
					return false;
				}
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(equalKey, default(TValue));
				if (keyOnlyComparer.Equals(this._firstValue, keyValuePair))
				{
					actualKey = this._firstValue.Key;
					return true;
				}
				int num = this._additionalElements.IndexOf(keyValuePair, keyOnlyComparer);
				if (num < 0)
				{
					actualKey = equalKey;
					return false;
				}
				actualKey = this._additionalElements[num].Key;
				return true;
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x0000B30A File Offset: 0x0000950A
			internal void Freeze()
			{
				if (this._additionalElements != null)
				{
					this._additionalElements.Freeze();
				}
			}

			// Token: 0x0400007D RID: 125
			private readonly KeyValuePair<TKey, TValue> _firstValue;

			// Token: 0x0400007E RID: 126
			private readonly ImmutableList<KeyValuePair<TKey, TValue>>.Node _additionalElements;

			// Token: 0x02000072 RID: 114
			internal struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
			{
				// Token: 0x0600061F RID: 1567 RVA: 0x00010C73 File Offset: 0x0000EE73
				internal Enumerator(ImmutableDictionary<TKey, TValue>.HashBucket bucket)
				{
					this._bucket = bucket;
					this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst;
					this._additionalEnumerator = default(ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator);
				}

				// Token: 0x17000148 RID: 328
				// (get) Token: 0x06000620 RID: 1568 RVA: 0x00010C8F File Offset: 0x0000EE8F
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x17000149 RID: 329
				// (get) Token: 0x06000621 RID: 1569 RVA: 0x00010C9C File Offset: 0x0000EE9C
				public KeyValuePair<TKey, TValue> Current
				{
					get
					{
						ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position currentPosition = this._currentPosition;
						if (currentPosition == ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First)
						{
							return this._bucket._firstValue;
						}
						if (currentPosition != ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional)
						{
							throw new InvalidOperationException();
						}
						return this._additionalEnumerator.Current;
					}
				}

				// Token: 0x06000622 RID: 1570 RVA: 0x00010CD8 File Offset: 0x0000EED8
				public bool MoveNext()
				{
					if (this._bucket.IsEmpty)
					{
						this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End;
						return false;
					}
					switch (this._currentPosition)
					{
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst:
						this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First;
						return true;
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First:
						if (this._bucket._additionalElements.IsEmpty)
						{
							this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End;
							return false;
						}
						this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional;
						this._additionalEnumerator = new ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator(this._bucket._additionalElements, null, -1, -1, false);
						return this._additionalEnumerator.MoveNext();
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional:
						return this._additionalEnumerator.MoveNext();
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End:
						return false;
					default:
						throw new InvalidOperationException();
					}
				}

				// Token: 0x06000623 RID: 1571 RVA: 0x00010D81 File Offset: 0x0000EF81
				public void Reset()
				{
					this._additionalEnumerator.Dispose();
					this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst;
				}

				// Token: 0x06000624 RID: 1572 RVA: 0x00010D95 File Offset: 0x0000EF95
				public void Dispose()
				{
					this._additionalEnumerator.Dispose();
				}

				// Token: 0x04000112 RID: 274
				private readonly ImmutableDictionary<TKey, TValue>.HashBucket _bucket;

				// Token: 0x04000113 RID: 275
				private ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position _currentPosition;

				// Token: 0x04000114 RID: 276
				private ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator _additionalEnumerator;

				// Token: 0x02000075 RID: 117
				private enum Position
				{
					// Token: 0x0400011D RID: 285
					BeforeFirst,
					// Token: 0x0400011E RID: 286
					First,
					// Token: 0x0400011F RID: 287
					Additional,
					// Token: 0x04000120 RID: 288
					End
				}
			}
		}

		// Token: 0x0200004E RID: 78
		private struct MutationInput
		{
			// Token: 0x0600042F RID: 1071 RVA: 0x0000B31F File Offset: 0x0000951F
			internal MutationInput(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers, int count)
			{
				this._root = root;
				this._comparers = comparers;
				this._count = count;
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x0000B336 File Offset: 0x00009536
			internal MutationInput(ImmutableDictionary<TKey, TValue> map)
			{
				this._root = map._root;
				this._comparers = map._comparers;
				this._count = map._count;
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000B35C File Offset: 0x0000955C
			internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000B364 File Offset: 0x00009564
			internal IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this._comparers.KeyComparer;
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000B371 File Offset: 0x00009571
			internal IEqualityComparer<KeyValuePair<TKey, TValue>> KeyOnlyComparer
			{
				get
				{
					return this._comparers.KeyOnlyComparer;
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000B37E File Offset: 0x0000957E
			internal IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._comparers.ValueComparer;
				}
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000B38B File Offset: 0x0000958B
			internal IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> HashBucketComparer
			{
				get
				{
					return this._comparers.HashBucketEqualityComparer;
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000B398 File Offset: 0x00009598
			internal int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x0400007F RID: 127
			private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

			// Token: 0x04000080 RID: 128
			private readonly ImmutableDictionary<TKey, TValue>.Comparers _comparers;

			// Token: 0x04000081 RID: 129
			private readonly int _count;
		}

		// Token: 0x0200004F RID: 79
		private struct MutationResult
		{
			// Token: 0x06000437 RID: 1079 RVA: 0x0000B3A0 File Offset: 0x000095A0
			internal MutationResult(ImmutableDictionary<TKey, TValue>.MutationInput unchangedInput)
			{
				this._root = unchangedInput.Root;
				this._countAdjustment = 0;
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x0000B3B6 File Offset: 0x000095B6
			internal MutationResult(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, int countAdjustment)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
				this._root = root;
				this._countAdjustment = countAdjustment;
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000B3D1 File Offset: 0x000095D1
			internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000B3D9 File Offset: 0x000095D9
			internal int CountAdjustment
			{
				get
				{
					return this._countAdjustment;
				}
			}

			// Token: 0x0600043B RID: 1083 RVA: 0x0000B3E1 File Offset: 0x000095E1
			internal ImmutableDictionary<TKey, TValue> Finalize(ImmutableDictionary<TKey, TValue> priorMap)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(priorMap, "priorMap");
				return priorMap.Wrap(this.Root, priorMap._count + this.CountAdjustment);
			}

			// Token: 0x04000082 RID: 130
			private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

			// Token: 0x04000083 RID: 131
			private readonly int _countAdjustment;
		}

		// Token: 0x02000050 RID: 80
		internal enum KeyCollisionBehavior
		{
			// Token: 0x04000085 RID: 133
			SetValue,
			// Token: 0x04000086 RID: 134
			Skip,
			// Token: 0x04000087 RID: 135
			ThrowIfValueDifferent,
			// Token: 0x04000088 RID: 136
			ThrowAlways
		}

		// Token: 0x02000051 RID: 81
		internal enum OperationResult
		{
			// Token: 0x0400008A RID: 138
			AppliedWithoutSizeChange,
			// Token: 0x0400008B RID: 139
			SizeChanged,
			// Token: 0x0400008C RID: 140
			NoChangeRequired
		}
	}
}
