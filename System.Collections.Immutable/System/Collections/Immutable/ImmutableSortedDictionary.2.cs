using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x0200002D RID: 45
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableSortedDictionaryDebuggerProxy<, >))]
	public sealed class ImmutableSortedDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, ISortKeyCollection<TKey>, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x00007CBD File Offset: 0x00005EBD
		internal ImmutableSortedDictionary(IComparer<TKey> keyComparer = null, IEqualityComparer<TValue> valueComparer = null)
		{
			this._keyComparer = (keyComparer ?? Comparer<TKey>.Default);
			this._valueComparer = (valueComparer ?? EqualityComparer<TValue>.Default);
			this._root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007CF0 File Offset: 0x00005EF0
		private ImmutableSortedDictionary(ImmutableSortedDictionary<TKey, TValue>.Node root, int count, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(root, "root");
			Requires.Range(count >= 0, "count", null);
			Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
			Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
			root.Freeze(null);
			this._root = root;
			this._count = count;
			this._keyComparer = keyComparer;
			this._valueComparer = valueComparer;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00007D5B File Offset: 0x00005F5B
		public ImmutableSortedDictionary<TKey, TValue> Clear()
		{
			if (!this._root.IsEmpty)
			{
				return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(this._keyComparer, this._valueComparer);
			}
			return this;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00007D82 File Offset: 0x00005F82
		public IEqualityComparer<TValue> ValueComparer
		{
			get
			{
				return this._valueComparer;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00007D8A File Offset: 0x00005F8A
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00007D97 File Offset: 0x00005F97
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00007D9F File Offset: 0x00005F9F
		public IEnumerable<TKey> Keys
		{
			get
			{
				return this._root.Keys;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00007DAC File Offset: 0x00005FAC
		public IEnumerable<TValue> Values
		{
			get
			{
				return this._root.Values;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00007DB9 File Offset: 0x00005FB9
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00004D79 File Offset: 0x00002F79
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00004D81 File Offset: 0x00002F81
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000038D6 File Offset: 0x00001AD6
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00007DD4 File Offset: 0x00005FD4
		public IComparer<TKey> KeyComparer
		{
			get
			{
				return this._keyComparer;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00007DDC File Offset: 0x00005FDC
		internal ImmutableSortedDictionary<TKey, TValue>.Node Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17000076 RID: 118
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

		// Token: 0x17000077 RID: 119
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

		// Token: 0x060002BA RID: 698 RVA: 0x00007E1E File Offset: 0x0000601E
		public ImmutableSortedDictionary<TKey, TValue>.Builder ToBuilder()
		{
			return new ImmutableSortedDictionary<TKey, TValue>.Builder(this);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00007E28 File Offset: 0x00006028
		public ImmutableSortedDictionary<TKey, TValue> Add(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			bool flag;
			ImmutableSortedDictionary<TKey, TValue>.Node root = this._root.Add(key, value, this._keyComparer, this._valueComparer, out flag);
			return this.Wrap(root, this._count + 1);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00007E6C File Offset: 0x0000606C
		public ImmutableSortedDictionary<TKey, TValue> SetItem(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			bool flag;
			bool flag2;
			ImmutableSortedDictionary<TKey, TValue>.Node root = this._root.SetItem(key, value, this._keyComparer, this._valueComparer, out flag, out flag2);
			return this.Wrap(root, flag ? this._count : (this._count + 1));
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00007EBC File Offset: 0x000060BC
		public ImmutableSortedDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return this.AddRange(items, true, false);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00007ED2 File Offset: 0x000060D2
		public ImmutableSortedDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return this.AddRange(items, false, false);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00007EE8 File Offset: 0x000060E8
		public ImmutableSortedDictionary<TKey, TValue> Remove(TKey value)
		{
			Requires.NotNullAllowStructs<TKey>(value, "value");
			bool flag;
			ImmutableSortedDictionary<TKey, TValue>.Node root = this._root.Remove(value, this._keyComparer, out flag);
			return this.Wrap(root, this._count - 1);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00007F24 File Offset: 0x00006124
		public ImmutableSortedDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
		{
			Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root;
			int num = this._count;
			foreach (TKey key in keys)
			{
				bool flag;
				ImmutableSortedDictionary<TKey, TValue>.Node node2 = node.Remove(key, this._keyComparer, out flag);
				if (flag)
				{
					node = node2;
					num--;
				}
			}
			return this.Wrap(node, num);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00007FA4 File Offset: 0x000061A4
		public ImmutableSortedDictionary<TKey, TValue> WithComparers(IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			if (keyComparer == null)
			{
				keyComparer = Comparer<TKey>.Default;
			}
			if (valueComparer == null)
			{
				valueComparer = EqualityComparer<TValue>.Default;
			}
			if (keyComparer != this._keyComparer)
			{
				return new ImmutableSortedDictionary<TKey, TValue>(ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode, 0, keyComparer, valueComparer).AddRange(this, false, true);
			}
			if (valueComparer == this._valueComparer)
			{
				return this;
			}
			return new ImmutableSortedDictionary<TKey, TValue>(this._root, this._count, this._keyComparer, valueComparer);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008007 File Offset: 0x00006207
		public ImmutableSortedDictionary<TKey, TValue> WithComparers(IComparer<TKey> keyComparer)
		{
			return this.WithComparers(keyComparer, this._valueComparer);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008016 File Offset: 0x00006216
		public bool ContainsValue(TValue value)
		{
			return this._root.ContainsValue(value, this._valueComparer);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000802A File Offset: 0x0000622A
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			return this.Add(key, value);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00008034 File Offset: 0x00006234
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.SetItem(TKey key, TValue value)
		{
			return this.SetItem(key, value);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000803E File Offset: 0x0000623E
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return this.SetItems(items);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00008047 File Offset: 0x00006247
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			return this.AddRange(pairs);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00008050 File Offset: 0x00006250
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.RemoveRange(IEnumerable<TKey> keys)
		{
			return this.RemoveRange(keys);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00008059 File Offset: 0x00006259
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Remove(TKey key)
		{
			return this.Remove(key);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00008062 File Offset: 0x00006262
		public bool ContainsKey(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.ContainsKey(key, this._keyComparer);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00008081 File Offset: 0x00006281
		public bool Contains(KeyValuePair<TKey, TValue> pair)
		{
			return this._root.Contains(pair, this._keyComparer, this._valueComparer);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000809B File Offset: 0x0000629B
		public bool TryGetValue(TKey key, out TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.TryGetValue(key, this._keyComparer, out value);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000080BB File Offset: 0x000062BB
		public bool TryGetKey(TKey equalKey, out TKey actualKey)
		{
			Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
			return this._root.TryGetKey(equalKey, this._keyComparer, out actualKey);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00002D65 File Offset: 0x00000F65
		void IDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00002D65 File Offset: 0x00000F65
		bool IDictionary<!0, !1>.Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00002D65 File Offset: 0x00000F65
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00002D65 File Offset: 0x00000F65
		void ICollection<KeyValuePair<!0, !1>>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00002D65 File Offset: 0x00000F65
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00008100 File Offset: 0x00006300
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x000038D6 File Offset: 0x00001AD6
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x000038D6 File Offset: 0x00001AD6
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00004D79 File Offset: 0x00002F79
		ICollection IDictionary.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00004D81 File Offset: 0x00002F81
		ICollection IDictionary.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00002D65 File Offset: 0x00000F65
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000081A9 File Offset: 0x000063A9
		bool IDictionary.Contains(object key)
		{
			return this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000081B7 File Offset: 0x000063B7
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00002D65 File Offset: 0x00000F65
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700007C RID: 124
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

		// Token: 0x060002DE RID: 734 RVA: 0x00002D65 File Offset: 0x00000F65
		void IDictionary.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000081F1 File Offset: 0x000063F1
		void ICollection.CopyTo(Array array, int index)
		{
			this._root.CopyTo(array, index, this.Count);
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x000052C4 File Offset: 0x000034C4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x000038D6 File Offset: 0x00001AD6
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000820C File Offset: 0x0000640C
		[ExcludeFromCodeCoverage]
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000820C File Offset: 0x0000640C
		[ExcludeFromCodeCoverage]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00008226 File Offset: 0x00006426
		public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return this._root.GetEnumerator();
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00008233 File Offset: 0x00006433
		private static ImmutableSortedDictionary<TKey, TValue> Wrap(ImmutableSortedDictionary<TKey, TValue>.Node root, int count, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableSortedDictionary<TKey, TValue>(root, count, keyComparer, valueComparer);
			}
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00008254 File Offset: 0x00006454
		private static bool TryCastToImmutableMap(IEnumerable<KeyValuePair<TKey, TValue>> sequence, out ImmutableSortedDictionary<TKey, TValue> other)
		{
			other = (sequence as ImmutableSortedDictionary<TKey, TValue>);
			if (other != null)
			{
				return true;
			}
			ImmutableSortedDictionary<TKey, TValue>.Builder builder = sequence as ImmutableSortedDictionary<TKey, TValue>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00008284 File Offset: 0x00006484
		private ImmutableSortedDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items, bool overwriteOnCollision, bool avoidToSortedMap)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			if (this.IsEmpty && !avoidToSortedMap)
			{
				return this.FillFromEmpty(items, overwriteOnCollision);
			}
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root;
			int num = this._count;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
			{
				bool flag = false;
				bool flag2;
				ImmutableSortedDictionary<TKey, TValue>.Node node2 = overwriteOnCollision ? node.SetItem(keyValuePair.Key, keyValuePair.Value, this._keyComparer, this._valueComparer, out flag, out flag2) : node.Add(keyValuePair.Key, keyValuePair.Value, this._keyComparer, this._valueComparer, out flag2);
				if (flag2)
				{
					node = node2;
					if (!flag)
					{
						num++;
					}
				}
			}
			return this.Wrap(node, num);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000835C File Offset: 0x0000655C
		private ImmutableSortedDictionary<TKey, TValue> Wrap(ImmutableSortedDictionary<TKey, TValue>.Node root, int adjustedCountIfDifferentRoot)
		{
			if (this._root == root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableSortedDictionary<TKey, TValue>(root, adjustedCountIfDifferentRoot, this._keyComparer, this._valueComparer);
			}
			return this.Clear();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000838C File Offset: 0x0000658C
		private ImmutableSortedDictionary<TKey, TValue> FillFromEmpty(IEnumerable<KeyValuePair<TKey, TValue>> items, bool overwriteOnCollision)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary;
			if (ImmutableSortedDictionary<TKey, TValue>.TryCastToImmutableMap(items, out immutableSortedDictionary))
			{
				return immutableSortedDictionary.WithComparers(this.KeyComparer, this.ValueComparer);
			}
			IDictionary<TKey, TValue> dictionary = items as IDictionary<TKey, TValue>;
			SortedDictionary<TKey, TValue> sortedDictionary;
			if (dictionary != null)
			{
				sortedDictionary = new SortedDictionary<TKey, TValue>(dictionary, this.KeyComparer);
			}
			else
			{
				sortedDictionary = new SortedDictionary<TKey, TValue>(this.KeyComparer);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
				{
					TValue x;
					if (overwriteOnCollision)
					{
						sortedDictionary[keyValuePair.Key] = keyValuePair.Value;
					}
					else if (sortedDictionary.TryGetValue(keyValuePair.Key, out x))
					{
						if (!this._valueComparer.Equals(x, keyValuePair.Value))
						{
							throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.DuplicateKey, new object[]
							{
								keyValuePair.Key
							}));
						}
					}
					else
					{
						sortedDictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			if (sortedDictionary.Count == 0)
			{
				return this;
			}
			return new ImmutableSortedDictionary<TKey, TValue>(ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromSortedDictionary(sortedDictionary), sortedDictionary.Count, this.KeyComparer, this.ValueComparer);
		}

		// Token: 0x0400002F RID: 47
		public static readonly ImmutableSortedDictionary<TKey, TValue> Empty = new ImmutableSortedDictionary<TKey, TValue>(null, null);

		// Token: 0x04000030 RID: 48
		private readonly ImmutableSortedDictionary<TKey, TValue>.Node _root;

		// Token: 0x04000031 RID: 49
		private readonly int _count;

		// Token: 0x04000032 RID: 50
		private readonly IComparer<TKey> _keyComparer;

		// Token: 0x04000033 RID: 51
		private readonly IEqualityComparer<TValue> _valueComparer;

		// Token: 0x02000066 RID: 102
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableSortedDictionaryBuilderDebuggerProxy<, >))]
		public sealed class Builder : IDictionary<!0, !1>, ICollection<KeyValuePair<!0, !1>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
		{
			// Token: 0x0600053B RID: 1339 RVA: 0x0000E43C File Offset: 0x0000C63C
			internal Builder(ImmutableSortedDictionary<TKey, TValue> map)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>>(map, "map");
				this._root = map._root;
				this._keyComparer = map.KeyComparer;
				this._valueComparer = map.ValueComparer;
				this._count = map.Count;
				this._immutable = map;
			}

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x0600053C RID: 1340 RVA: 0x0000E4B2 File Offset: 0x0000C6B2
			ICollection<TKey> IDictionary<!0, !1>.Keys
			{
				get
				{
					return this.Root.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000E4CA File Offset: 0x0000C6CA
			public IEnumerable<TKey> Keys
			{
				get
				{
					return this.Root.Keys;
				}
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x0600053E RID: 1342 RVA: 0x0000E4D7 File Offset: 0x0000C6D7
			ICollection<TValue> IDictionary<!0, !1>.Values
			{
				get
				{
					return this.Root.Values.ToArray(this.Count);
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000E4EF File Offset: 0x0000C6EF
			public IEnumerable<TValue> Values
			{
				get
				{
					return this.Root.Values;
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x06000540 RID: 1344 RVA: 0x0000E4FC File Offset: 0x0000C6FC
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000541 RID: 1345 RVA: 0x000020FC File Offset: 0x000002FC
			bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x06000542 RID: 1346 RVA: 0x0000E507 File Offset: 0x0000C707
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000E50F File Offset: 0x0000C70F
			// (set) Token: 0x06000544 RID: 1348 RVA: 0x0000E517 File Offset: 0x0000C717
			private ImmutableSortedDictionary<TKey, TValue>.Node Root
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

			// Token: 0x1700010B RID: 267
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
					bool flag;
					bool flag2;
					this.Root = this._root.SetItem(key, value, this._keyComparer, this._valueComparer, out flag, out flag2);
					if (flag2 && !flag)
					{
						this._count++;
					}
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06000547 RID: 1351 RVA: 0x000020FC File Offset: 0x000002FC
			bool IDictionary.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x06000548 RID: 1352 RVA: 0x000020FC File Offset: 0x000002FC
			bool IDictionary.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000E5AA File Offset: 0x0000C7AA
			ICollection IDictionary.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x0600054A RID: 1354 RVA: 0x0000E5BD File Offset: 0x0000C7BD
			ICollection IDictionary.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x0600054B RID: 1355 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
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

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x0600054C RID: 1356 RVA: 0x000020FC File Offset: 0x000002FC
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x0600054D RID: 1357 RVA: 0x0000E5F5 File Offset: 0x0000C7F5
			// (set) Token: 0x0600054E RID: 1358 RVA: 0x0000E600 File Offset: 0x0000C800
			public IComparer<TKey> KeyComparer
			{
				get
				{
					return this._keyComparer;
				}
				set
				{
					Requires.NotNull<IComparer<TKey>>(value, "value");
					if (value != this._keyComparer)
					{
						ImmutableSortedDictionary<TKey, TValue>.Node node = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
						int num = 0;
						foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
						{
							bool flag;
							node = node.Add(keyValuePair.Key, keyValuePair.Value, value, this._valueComparer, out flag);
							if (flag)
							{
								num++;
							}
						}
						this._keyComparer = value;
						this.Root = node;
						this._count = num;
					}
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
			// (set) Token: 0x06000550 RID: 1360 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
			public IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._valueComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<TValue>>(value, "value");
					if (value != this._valueComparer)
					{
						this._valueComparer = value;
						this._immutable = null;
					}
				}
			}

			// Token: 0x06000551 RID: 1361 RVA: 0x0000E6CC File Offset: 0x0000C8CC
			void IDictionary.Add(object key, object value)
			{
				this.Add((TKey)((object)key), (TValue)((object)value));
			}

			// Token: 0x06000552 RID: 1362 RVA: 0x0000E6E0 File Offset: 0x0000C8E0
			bool IDictionary.Contains(object key)
			{
				return this.ContainsKey((TKey)((object)key));
			}

			// Token: 0x06000553 RID: 1363 RVA: 0x0000E6EE File Offset: 0x0000C8EE
			IDictionaryEnumerator IDictionary.GetEnumerator()
			{
				return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
			}

			// Token: 0x06000554 RID: 1364 RVA: 0x0000E700 File Offset: 0x0000C900
			void IDictionary.Remove(object key)
			{
				this.Remove((TKey)((object)key));
			}

			// Token: 0x17000114 RID: 276
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

			// Token: 0x06000557 RID: 1367 RVA: 0x0000E736 File Offset: 0x0000C936
			void ICollection.CopyTo(Array array, int index)
			{
				this.Root.CopyTo(array, index, this.Count);
			}

			// Token: 0x06000558 RID: 1368 RVA: 0x0000E74C File Offset: 0x0000C94C
			public void Add(TKey key, TValue value)
			{
				bool flag;
				this.Root = this.Root.Add(key, value, this._keyComparer, this._valueComparer, out flag);
				if (flag)
				{
					this._count++;
				}
			}

			// Token: 0x06000559 RID: 1369 RVA: 0x0000E78B File Offset: 0x0000C98B
			public bool ContainsKey(TKey key)
			{
				return this.Root.ContainsKey(key, this._keyComparer);
			}

			// Token: 0x0600055A RID: 1370 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
			public bool Remove(TKey key)
			{
				bool flag;
				this.Root = this.Root.Remove(key, this._keyComparer, out flag);
				if (flag)
				{
					this._count--;
				}
				return flag;
			}

			// Token: 0x0600055B RID: 1371 RVA: 0x0000E7D9 File Offset: 0x0000C9D9
			public bool TryGetValue(TKey key, out TValue value)
			{
				return this.Root.TryGetValue(key, this._keyComparer, out value);
			}

			// Token: 0x0600055C RID: 1372 RVA: 0x0000E7EE File Offset: 0x0000C9EE
			public bool TryGetKey(TKey equalKey, out TKey actualKey)
			{
				Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
				return this.Root.TryGetKey(equalKey, this._keyComparer, out actualKey);
			}

			// Token: 0x0600055D RID: 1373 RVA: 0x0000E80E File Offset: 0x0000CA0E
			public void Add(KeyValuePair<TKey, TValue> item)
			{
				this.Add(item.Key, item.Value);
			}

			// Token: 0x0600055E RID: 1374 RVA: 0x0000E824 File Offset: 0x0000CA24
			public void Clear()
			{
				this.Root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
				this._count = 0;
			}

			// Token: 0x0600055F RID: 1375 RVA: 0x0000E838 File Offset: 0x0000CA38
			public bool Contains(KeyValuePair<TKey, TValue> item)
			{
				return this.Root.Contains(item, this._keyComparer, this._valueComparer);
			}

			// Token: 0x06000560 RID: 1376 RVA: 0x0000E852 File Offset: 0x0000CA52
			void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex, this.Count);
			}

			// Token: 0x06000561 RID: 1377 RVA: 0x0000E867 File Offset: 0x0000CA67
			public bool Remove(KeyValuePair<TKey, TValue> item)
			{
				return this.Contains(item) && this.Remove(item.Key);
			}

			// Token: 0x06000562 RID: 1378 RVA: 0x0000E881 File Offset: 0x0000CA81
			public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x06000563 RID: 1379 RVA: 0x0000E88F File Offset: 0x0000CA8F
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000564 RID: 1380 RVA: 0x0000E88F File Offset: 0x0000CA8F
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000565 RID: 1381 RVA: 0x0000E8A9 File Offset: 0x0000CAA9
			public bool ContainsValue(TValue value)
			{
				return this._root.ContainsValue(value, this._valueComparer);
			}

			// Token: 0x06000566 RID: 1382 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
			public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
			{
				Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
				foreach (KeyValuePair<TKey, TValue> item in items)
				{
					this.Add(item);
				}
			}

			// Token: 0x06000567 RID: 1383 RVA: 0x0000E914 File Offset: 0x0000CB14
			public void RemoveRange(IEnumerable<TKey> keys)
			{
				Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
				foreach (TKey key in keys)
				{
					this.Remove(key);
				}
			}

			// Token: 0x06000568 RID: 1384 RVA: 0x0000E968 File Offset: 0x0000CB68
			public TValue GetValueOrDefault(TKey key)
			{
				return this.GetValueOrDefault(key, default(TValue));
			}

			// Token: 0x06000569 RID: 1385 RVA: 0x0000E988 File Offset: 0x0000CB88
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

			// Token: 0x0600056A RID: 1386 RVA: 0x0000E9AE File Offset: 0x0000CBAE
			public ImmutableSortedDictionary<TKey, TValue> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableSortedDictionary<TKey, TValue>.Wrap(this.Root, this._count, this._keyComparer, this._valueComparer);
				}
				return this._immutable;
			}

			// Token: 0x040000D5 RID: 213
			private ImmutableSortedDictionary<TKey, TValue>.Node _root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;

			// Token: 0x040000D6 RID: 214
			private IComparer<TKey> _keyComparer = Comparer<TKey>.Default;

			// Token: 0x040000D7 RID: 215
			private IEqualityComparer<TValue> _valueComparer = EqualityComparer<TValue>.Default;

			// Token: 0x040000D8 RID: 216
			private int _count;

			// Token: 0x040000D9 RID: 217
			private ImmutableSortedDictionary<TKey, TValue> _immutable;

			// Token: 0x040000DA RID: 218
			private int _version;

			// Token: 0x040000DB RID: 219
			private object _syncRoot;
		}

		// Token: 0x02000067 RID: 103
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable, ISecurePooledObjectUser
		{
			// Token: 0x0600056B RID: 1387 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
			internal Enumerator(ImmutableSortedDictionary<TKey, TValue>.Node root, ImmutableSortedDictionary<TKey, TValue>.Builder builder = null)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(root, "root");
				this._root = root;
				this._builder = builder;
				this._current = null;
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!this._root.IsEmpty)
				{
					if (!ImmutableSortedDictionary<TKey, TValue>.Enumerator.s_enumeratingStacks.TryTake(this, out this._stack))
					{
						this._stack = ImmutableSortedDictionary<TKey, TValue>.Enumerator.s_enumeratingStacks.PrepNew(this, new Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>(root.Height));
					}
					this.PushLeft(this._root);
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x0600056C RID: 1388 RVA: 0x0000EA87 File Offset: 0x0000CC87
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					this.ThrowIfDisposed();
					if (this._current != null)
					{
						return this._current.Value;
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000EAB0 File Offset: 0x0000CCB0
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600056F RID: 1391 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack;
				if (this._stack != null && this._stack.TryUse<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>();
					ImmutableSortedDictionary<TKey, TValue>.Enumerator.s_enumeratingStacks.TryAdd(this, this._stack);
				}
				this._stack = null;
			}

			// Token: 0x06000570 RID: 1392 RVA: 0x0000EB18 File Offset: 0x0000CD18
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				this.ThrowIfChanged();
				if (this._stack != null)
				{
					Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack = this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this);
					if (stack.Count > 0)
					{
						ImmutableSortedDictionary<TKey, TValue>.Node value = stack.Pop().Value;
						this._current = value;
						this.PushLeft(value.Right);
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x06000571 RID: 1393 RVA: 0x0000EB78 File Offset: 0x0000CD78
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._current = null;
				if (this._stack != null)
				{
					this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this).ClearFastWhenEmpty<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>();
					this.PushLeft(this._root);
				}
			}

			// Token: 0x06000572 RID: 1394 RVA: 0x0000EBD3 File Offset: 0x0000CDD3
			internal void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(this);
				}
			}

			// Token: 0x06000573 RID: 1395 RVA: 0x0000EBFE File Offset: 0x0000CDFE
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x06000574 RID: 1396 RVA: 0x0000EC28 File Offset: 0x0000CE28
			private void PushLeft(ImmutableSortedDictionary<TKey, TValue>.Node node)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(node, "node");
				Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack = this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>(node));
					node = node.Left;
				}
			}

			// Token: 0x040000DC RID: 220
			private static readonly SecureObjectPool<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator>();

			// Token: 0x040000DD RID: 221
			private readonly ImmutableSortedDictionary<TKey, TValue>.Builder _builder;

			// Token: 0x040000DE RID: 222
			private readonly int _poolUserId;

			// Token: 0x040000DF RID: 223
			private ImmutableSortedDictionary<TKey, TValue>.Node _root;

			// Token: 0x040000E0 RID: 224
			private SecurePooledObject<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>> _stack;

			// Token: 0x040000E1 RID: 225
			private ImmutableSortedDictionary<TKey, TValue>.Node _current;

			// Token: 0x040000E2 RID: 226
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x02000068 RID: 104
		[DebuggerDisplay("{_key} = {_value}")]
		internal sealed class Node : IBinaryTree<KeyValuePair<TKey, TValue>>, IBinaryTree, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
		{
			// Token: 0x06000576 RID: 1398 RVA: 0x0000EC77 File Offset: 0x0000CE77
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x0000EC88 File Offset: 0x0000CE88
			private Node(TKey key, TValue value, ImmutableSortedDictionary<TKey, TValue>.Node left, ImmutableSortedDictionary<TKey, TValue>.Node right, bool frozen = false)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(left, "left");
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(right, "right");
				this._key = key;
				this._value = value;
				this._left = left;
				this._right = right;
				this._height = checked(1 + Math.Max(left._height, right._height));
				this._frozen = frozen;
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000578 RID: 1400 RVA: 0x0000ECFD File Offset: 0x0000CEFD
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x06000579 RID: 1401 RVA: 0x0000ED08 File Offset: 0x0000CF08
			IBinaryTree<KeyValuePair<TKey, TValue>> IBinaryTree<KeyValuePair<!0, !1>>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000ED10 File Offset: 0x0000CF10
			IBinaryTree<KeyValuePair<TKey, TValue>> IBinaryTree<KeyValuePair<!0, !1>>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000ED18 File Offset: 0x0000CF18
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000ED08 File Offset: 0x0000CF08
			public ImmutableSortedDictionary<TKey, TValue>.Node Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x0600057D RID: 1405 RVA: 0x0000ED08 File Offset: 0x0000CF08
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x0600057E RID: 1406 RVA: 0x0000ED10 File Offset: 0x0000CF10
			public ImmutableSortedDictionary<TKey, TValue>.Node Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x0600057F RID: 1407 RVA: 0x0000ED10 File Offset: 0x0000CF10
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000ED40 File Offset: 0x0000CF40
			public KeyValuePair<TKey, TValue> Value
			{
				get
				{
					return new KeyValuePair<TKey, TValue>(this._key, this._value);
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x06000581 RID: 1409 RVA: 0x00002D65 File Offset: 0x00000F65
			int IBinaryTree.Count
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000ED5A File Offset: 0x0000CF5A
			internal IEnumerable<TKey> Keys
			{
				get
				{
					return from p in this
					select p.Key;
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x06000583 RID: 1411 RVA: 0x0000ED81 File Offset: 0x0000CF81
			internal IEnumerable<TValue> Values
			{
				get
				{
					return from p in this
					select p.Value;
				}
			}

			// Token: 0x06000584 RID: 1412 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
			public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new ImmutableSortedDictionary<TKey, TValue>.Enumerator(this, null);
			}

			// Token: 0x06000585 RID: 1413 RVA: 0x0000EDB1 File Offset: 0x0000CFB1
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000586 RID: 1414 RVA: 0x0000EDB1 File Offset: 0x0000CFB1
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000587 RID: 1415 RVA: 0x0000EDCB File Offset: 0x0000CFCB
			internal ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator(ImmutableSortedDictionary<TKey, TValue>.Builder builder)
			{
				return new ImmutableSortedDictionary<TKey, TValue>.Enumerator(this, builder);
			}

			// Token: 0x06000588 RID: 1416 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
			internal void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex, int dictionarySize)
			{
				Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + dictionarySize, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array[arrayIndex++] = keyValuePair;
				}
			}

			// Token: 0x06000589 RID: 1417 RVA: 0x0000EE5C File Offset: 0x0000D05C
			internal void CopyTo(Array array, int arrayIndex, int dictionarySize)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + dictionarySize, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), new int[]
					{
						arrayIndex++
					});
				}
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x0000EF10 File Offset: 0x0000D110
			internal static ImmutableSortedDictionary<TKey, TValue>.Node NodeTreeFromSortedDictionary(SortedDictionary<TKey, TValue> dictionary)
			{
				Requires.NotNull<SortedDictionary<TKey, TValue>>(dictionary, "dictionary");
				IOrderedCollection<KeyValuePair<TKey, TValue>> orderedCollection = dictionary.AsOrderedCollection<KeyValuePair<TKey, TValue>>();
				return ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count);
			}

			// Token: 0x0600058B RID: 1419 RVA: 0x0000EF3C File Offset: 0x0000D13C
			internal ImmutableSortedDictionary<TKey, TValue>.Node Add(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				bool flag;
				return this.SetOrAdd(key, value, keyComparer, valueComparer, false, out flag, out mutated);
			}

			// Token: 0x0600058C RID: 1420 RVA: 0x0000EF7B File Offset: 0x0000D17B
			internal ImmutableSortedDictionary<TKey, TValue>.Node SetItem(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, out bool replacedExistingValue, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				return this.SetOrAdd(key, value, keyComparer, valueComparer, true, out replacedExistingValue, out mutated);
			}

			// Token: 0x0600058D RID: 1421 RVA: 0x0000EFAF File Offset: 0x0000D1AF
			internal ImmutableSortedDictionary<TKey, TValue>.Node Remove(TKey key, IComparer<TKey> keyComparer, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				return this.RemoveRecursive(key, keyComparer, out mutated);
			}

			// Token: 0x0600058E RID: 1422 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
			internal TValue GetValueOrDefault(TKey key, IComparer<TKey> keyComparer)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(key, keyComparer);
				if (!node.IsEmpty)
				{
					return node._value;
				}
				return default(TValue);
			}

			// Token: 0x0600058F RID: 1423 RVA: 0x0000F014 File Offset: 0x0000D214
			internal bool TryGetValue(TKey key, IComparer<TKey> keyComparer, out TValue value)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(key, keyComparer);
				if (node.IsEmpty)
				{
					value = default(TValue);
					return false;
				}
				value = node._value;
				return true;
			}

			// Token: 0x06000590 RID: 1424 RVA: 0x0000F060 File Offset: 0x0000D260
			internal bool TryGetKey(TKey equalKey, IComparer<TKey> keyComparer, out TKey actualKey)
			{
				Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(equalKey, keyComparer);
				if (node.IsEmpty)
				{
					actualKey = equalKey;
					return false;
				}
				actualKey = node._key;
				return true;
			}

			// Token: 0x06000591 RID: 1425 RVA: 0x0000F0AA File Offset: 0x0000D2AA
			internal bool ContainsKey(TKey key, IComparer<TKey> keyComparer)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				return !this.Search(key, keyComparer).IsEmpty;
			}

			// Token: 0x06000592 RID: 1426 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
			internal bool ContainsValue(TValue value, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					if (valueComparer.Equals(value, keyValuePair.Value))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x0000F140 File Offset: 0x0000D340
			internal bool Contains(KeyValuePair<TKey, TValue> pair, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNullAllowStructs<TKey>(pair.Key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(pair.Key, keyComparer);
				return !node.IsEmpty && valueComparer.Equals(node._value, pair.Value);
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
			internal void Freeze(Action<KeyValuePair<TKey, TValue>> freezeAction = null)
			{
				if (!this._frozen)
				{
					if (freezeAction != null)
					{
						freezeAction(new KeyValuePair<TKey, TValue>(this._key, this._value));
					}
					this._left.Freeze(freezeAction);
					this._right.Freeze(freezeAction);
					this._frozen = true;
				}
			}

			// Token: 0x06000595 RID: 1429 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
			private static ImmutableSortedDictionary<TKey, TValue>.Node RotateLeft(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node right = tree._right;
				return right.Mutate(tree.Mutate(null, right._left), null);
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x0000F234 File Offset: 0x0000D434
			private static ImmutableSortedDictionary<TKey, TValue>.Node RotateRight(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node left = tree._left;
				return left.Mutate(null, tree.Mutate(left._right, null));
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x0000F276 File Offset: 0x0000D476
			private static ImmutableSortedDictionary<TKey, TValue>.Node DoubleLeft(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				return ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree.Mutate(null, ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree._right)));
			}

			// Token: 0x06000598 RID: 1432 RVA: 0x0000F2A9 File Offset: 0x0000D4A9
			private static ImmutableSortedDictionary<TKey, TValue>.Node DoubleRight(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				return ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree.Mutate(ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree._left), null));
			}

			// Token: 0x06000599 RID: 1433 RVA: 0x0000F2DC File Offset: 0x0000D4DC
			private static int Balance(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return (int)(tree._right._height - tree._left._height);
			}

			// Token: 0x0600059A RID: 1434 RVA: 0x0000F300 File Offset: 0x0000D500
			private static bool IsRightHeavy(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree) >= 2;
			}

			// Token: 0x0600059B RID: 1435 RVA: 0x0000F319 File Offset: 0x0000D519
			private static bool IsLeftHeavy(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree) <= -2;
			}

			// Token: 0x0600059C RID: 1436 RVA: 0x0000F334 File Offset: 0x0000D534
			private static ImmutableSortedDictionary<TKey, TValue>.Node MakeBalanced(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (ImmutableSortedDictionary<TKey, TValue>.Node.IsRightHeavy(tree))
				{
					if (ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree._right) >= 0)
					{
						return ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree);
					}
					return ImmutableSortedDictionary<TKey, TValue>.Node.DoubleLeft(tree);
				}
				else
				{
					if (!ImmutableSortedDictionary<TKey, TValue>.Node.IsLeftHeavy(tree))
					{
						return tree;
					}
					if (ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree._left) <= 0)
					{
						return ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree);
					}
					return ImmutableSortedDictionary<TKey, TValue>.Node.DoubleRight(tree);
				}
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x0000F398 File Offset: 0x0000D598
			private static ImmutableSortedDictionary<TKey, TValue>.Node NodeTreeFromList(IOrderedCollection<KeyValuePair<TKey, TValue>> items, int start, int length)
			{
				Requires.NotNull<IOrderedCollection<KeyValuePair<TKey, TValue>>>(items, "items");
				Requires.Range(start >= 0, "start", null);
				Requires.Range(length >= 0, "length", null);
				if (length == 0)
				{
					return ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
				}
				int num = (length - 1) / 2;
				int num2 = length - 1 - num;
				ImmutableSortedDictionary<TKey, TValue>.Node left = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(items, start, num2);
				ImmutableSortedDictionary<TKey, TValue>.Node right = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(items, start + num2 + 1, num);
				KeyValuePair<TKey, TValue> keyValuePair = items[start + num2];
				return new ImmutableSortedDictionary<TKey, TValue>.Node(keyValuePair.Key, keyValuePair.Value, left, right, true);
			}

			// Token: 0x0600059E RID: 1438 RVA: 0x0000F420 File Offset: 0x0000D620
			private ImmutableSortedDictionary<TKey, TValue>.Node SetOrAdd(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, bool overwriteExistingValue, out bool replacedExistingValue, out bool mutated)
			{
				replacedExistingValue = false;
				if (this.IsEmpty)
				{
					mutated = true;
					return new ImmutableSortedDictionary<TKey, TValue>.Node(key, value, this, this, false);
				}
				ImmutableSortedDictionary<TKey, TValue>.Node node = this;
				int num = keyComparer.Compare(key, this._key);
				if (num > 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node right = this._right.SetOrAdd(key, value, keyComparer, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, right);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node left = this._left.SetOrAdd(key, value, keyComparer, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
					if (mutated)
					{
						node = this.Mutate(left, null);
					}
				}
				else
				{
					if (valueComparer.Equals(this._value, value))
					{
						mutated = false;
						return this;
					}
					if (!overwriteExistingValue)
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.DuplicateKey, new object[]
						{
							key
						}));
					}
					mutated = true;
					replacedExistingValue = true;
					node = new ImmutableSortedDictionary<TKey, TValue>.Node(key, value, this._left, this._right, false);
				}
				if (!mutated)
				{
					return node;
				}
				return ImmutableSortedDictionary<TKey, TValue>.Node.MakeBalanced(node);
			}

			// Token: 0x0600059F RID: 1439 RVA: 0x0000F520 File Offset: 0x0000D720
			private ImmutableSortedDictionary<TKey, TValue>.Node RemoveRecursive(TKey key, IComparer<TKey> keyComparer, out bool mutated)
			{
				if (this.IsEmpty)
				{
					mutated = false;
					return this;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node node = this;
				int num = keyComparer.Compare(key, this._key);
				if (num == 0)
				{
					mutated = true;
					if (this._right.IsEmpty && this._left.IsEmpty)
					{
						node = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
					}
					else if (this._right.IsEmpty && !this._left.IsEmpty)
					{
						node = this._left;
					}
					else if (!this._right.IsEmpty && this._left.IsEmpty)
					{
						node = this._right;
					}
					else
					{
						ImmutableSortedDictionary<TKey, TValue>.Node node2 = this._right;
						while (!node2._left.IsEmpty)
						{
							node2 = node2._left;
						}
						bool flag;
						ImmutableSortedDictionary<TKey, TValue>.Node right = this._right.Remove(node2._key, keyComparer, out flag);
						node = node2.Mutate(this._left, right);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node left = this._left.Remove(key, keyComparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(left, null);
					}
				}
				else
				{
					ImmutableSortedDictionary<TKey, TValue>.Node right2 = this._right.Remove(key, keyComparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, right2);
					}
				}
				if (!node.IsEmpty)
				{
					return ImmutableSortedDictionary<TKey, TValue>.Node.MakeBalanced(node);
				}
				return node;
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x0000F65C File Offset: 0x0000D85C
			private ImmutableSortedDictionary<TKey, TValue>.Node Mutate(ImmutableSortedDictionary<TKey, TValue>.Node left = null, ImmutableSortedDictionary<TKey, TValue>.Node right = null)
			{
				if (this._frozen)
				{
					return new ImmutableSortedDictionary<TKey, TValue>.Node(this._key, this._value, left ?? this._left, right ?? this._right, false);
				}
				if (left != null)
				{
					this._left = left;
				}
				if (right != null)
				{
					this._right = right;
				}
				this._height = checked(1 + Math.Max(this._left._height, this._right._height));
				return this;
			}

			// Token: 0x060005A1 RID: 1441 RVA: 0x0000F6D4 File Offset: 0x0000D8D4
			private ImmutableSortedDictionary<TKey, TValue>.Node Search(TKey key, IComparer<TKey> keyComparer)
			{
				if (this.IsEmpty)
				{
					return this;
				}
				int num = keyComparer.Compare(key, this._key);
				if (num == 0)
				{
					return this;
				}
				if (num > 0)
				{
					return this._right.Search(key, keyComparer);
				}
				return this._left.Search(key, keyComparer);
			}

			// Token: 0x040000E3 RID: 227
			internal static readonly ImmutableSortedDictionary<TKey, TValue>.Node EmptyNode = new ImmutableSortedDictionary<TKey, TValue>.Node();

			// Token: 0x040000E4 RID: 228
			private readonly TKey _key;

			// Token: 0x040000E5 RID: 229
			private TValue _value;

			// Token: 0x040000E6 RID: 230
			private bool _frozen;

			// Token: 0x040000E7 RID: 231
			private byte _height;

			// Token: 0x040000E8 RID: 232
			private ImmutableSortedDictionary<TKey, TValue>.Node _left;

			// Token: 0x040000E9 RID: 233
			private ImmutableSortedDictionary<TKey, TValue>.Node _right;
		}
	}
}
