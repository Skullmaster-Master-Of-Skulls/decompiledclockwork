using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000031 RID: 49
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableSortedSetDebuggerProxy<>))]
	public sealed class ImmutableSortedSet<T> : IImmutableSet<T>, IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable, ISortKeyCollection<T>, IReadOnlyList<T>, IList<T>, ICollection<T>, ISet<T>, IList, ICollection, IStrongEnumerable<T, ImmutableSortedSet<T>.Enumerator>
	{
		// Token: 0x060002FB RID: 763 RVA: 0x0000862D File Offset: 0x0000682D
		internal ImmutableSortedSet(IComparer<T> comparer = null)
		{
			this._root = ImmutableSortedSet<T>.Node.EmptyNode;
			this._comparer = (comparer ?? Comparer<T>.Default);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00008650 File Offset: 0x00006850
		private ImmutableSortedSet(ImmutableSortedSet<T>.Node root, IComparer<T> comparer)
		{
			Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
			Requires.NotNull<IComparer<T>>(comparer, "comparer");
			root.Freeze();
			this._root = root;
			this._comparer = comparer;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00008682 File Offset: 0x00006882
		public ImmutableSortedSet<T> Clear()
		{
			if (!this._root.IsEmpty)
			{
				return ImmutableSortedSet<T>.Empty.WithComparer(this._comparer);
			}
			return this;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000086A3 File Offset: 0x000068A3
		public T Max
		{
			get
			{
				return this._root.Max;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000086B0 File Offset: 0x000068B0
		public T Min
		{
			get
			{
				return this._root.Min;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000300 RID: 768 RVA: 0x000086BD File Offset: 0x000068BD
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000301 RID: 769 RVA: 0x000086CA File Offset: 0x000068CA
		public int Count
		{
			get
			{
				return this._root.Count;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000302 RID: 770 RVA: 0x000086D7 File Offset: 0x000068D7
		public IComparer<T> KeyComparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000303 RID: 771 RVA: 0x000086DF File Offset: 0x000068DF
		internal IBinaryTree Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17000087 RID: 135
		public T this[int index]
		{
			get
			{
				return this._root[index];
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000086F5 File Offset: 0x000068F5
		public ImmutableSortedSet<T>.Builder ToBuilder()
		{
			return new ImmutableSortedSet<T>.Builder(this);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00008700 File Offset: 0x00006900
		public ImmutableSortedSet<T> Add(T value)
		{
			Requires.NotNullAllowStructs<T>(value, "value");
			bool flag;
			return this.Wrap(this._root.Add(value, this._comparer, out flag));
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00008734 File Offset: 0x00006934
		public ImmutableSortedSet<T> Remove(T value)
		{
			Requires.NotNullAllowStructs<T>(value, "value");
			bool flag;
			return this.Wrap(this._root.Remove(value, this._comparer, out flag));
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00008768 File Offset: 0x00006968
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			Requires.NotNullAllowStructs<T>(equalValue, "equalValue");
			ImmutableSortedSet<T>.Node node = this._root.Search(equalValue, this._comparer);
			if (node.IsEmpty)
			{
				actualValue = equalValue;
				return false;
			}
			actualValue = node.Key;
			return true;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000087B4 File Offset: 0x000069B4
		public ImmutableSortedSet<T> Intersect(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet = this.Clear();
			foreach (T value in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (this.Contains(value))
				{
					immutableSortedSet = immutableSortedSet.Add(value);
				}
			}
			return immutableSortedSet;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00008828 File Offset: 0x00006A28
		public ImmutableSortedSet<T> Except(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T>.Node node = this._root;
			foreach (T key in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				bool flag;
				node = node.Remove(key, this._comparer, out flag);
			}
			return this.Wrap(node);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000088A0 File Offset: 0x00006AA0
		public ImmutableSortedSet<T> SymmetricExcept(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet = ImmutableSortedSet<T>.Empty.Union(other);
			ImmutableSortedSet<T> immutableSortedSet2 = this.Clear();
			foreach (T value in this)
			{
				if (!immutableSortedSet.Contains(value))
				{
					immutableSortedSet2 = immutableSortedSet2.Add(value);
				}
			}
			foreach (T value2 in immutableSortedSet)
			{
				if (!this.Contains(value2))
				{
					immutableSortedSet2 = immutableSortedSet2.Add(value2);
				}
			}
			return immutableSortedSet2;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00008964 File Offset: 0x00006B64
		public ImmutableSortedSet<T> Union(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet;
			if (ImmutableSortedSet<T>.TryCastToImmutableSortedSet(other, out immutableSortedSet) && immutableSortedSet.KeyComparer == this.KeyComparer)
			{
				if (immutableSortedSet.IsEmpty)
				{
					return this;
				}
				if (this.IsEmpty)
				{
					return immutableSortedSet;
				}
				if (immutableSortedSet.Count > this.Count)
				{
					return immutableSortedSet.Union(this);
				}
			}
			int num;
			if (this.IsEmpty || (other.TryGetCount(out num) && (float)(this.Count + num) * 0.15f > (float)this.Count))
			{
				return this.LeafToRootRefill(other);
			}
			return this.UnionIncremental(other);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000089F7 File Offset: 0x00006BF7
		public ImmutableSortedSet<T> WithComparer(IComparer<T> comparer)
		{
			if (comparer == null)
			{
				comparer = Comparer<T>.Default;
			}
			if (comparer == this._comparer)
			{
				return this;
			}
			return new ImmutableSortedSet<T>(ImmutableSortedSet<T>.Node.EmptyNode, comparer).Union(this);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00008A20 File Offset: 0x00006C20
		public bool SetEquals(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this == other)
			{
				return true;
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			if (this.Count != sortedSet.Count)
			{
				return false;
			}
			int num = 0;
			foreach (T value in sortedSet)
			{
				if (!this.Contains(value))
				{
					return false;
				}
				num++;
			}
			return num == this.Count;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00008AB8 File Offset: 0x00006CB8
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return other.Any<T>();
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			if (this.Count >= sortedSet.Count)
			{
				return false;
			}
			int num = 0;
			bool flag = false;
			foreach (T value in sortedSet)
			{
				if (this.Contains(value))
				{
					num++;
				}
				else
				{
					flag = true;
				}
				if (num == this.Count && flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00008B64 File Offset: 0x00006D64
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return false;
			}
			int num = 0;
			foreach (T value in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				num++;
				if (!this.Contains(value))
				{
					return false;
				}
			}
			return this.Count > num;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return true;
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			int num = 0;
			foreach (T value in sortedSet)
			{
				if (this.Contains(value))
				{
					num++;
				}
			}
			return num == this.Count;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00008C68 File Offset: 0x00006E68
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			foreach (T value in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (!this.Contains(value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00008CD4 File Offset: 0x00006ED4
		public bool Overlaps(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return false;
			}
			foreach (T value in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (this.Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00008D48 File Offset: 0x00006F48
		public IEnumerable<T> Reverse()
		{
			return new ImmutableSortedSet<T>.ReverseEnumerable(this._root);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00008D55 File Offset: 0x00006F55
		public int IndexOf(T item)
		{
			Requires.NotNullAllowStructs<T>(item, "item");
			return this._root.IndexOf(item, this._comparer);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00008D74 File Offset: 0x00006F74
		public bool Contains(T value)
		{
			Requires.NotNullAllowStructs<T>(value, "value");
			return this._root.Contains(value, this._comparer);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00008D93 File Offset: 0x00006F93
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00008D9B File Offset: 0x00006F9B
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Add(T value)
		{
			return this.Add(value);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00008DA4 File Offset: 0x00006FA4
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Remove(T value)
		{
			return this.Remove(value);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00008DAD File Offset: 0x00006FAD
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Intersect(IEnumerable<T> other)
		{
			return this.Intersect(other);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00008DB6 File Offset: 0x00006FB6
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Except(IEnumerable<T> other)
		{
			return this.Except(other);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00008DBF File Offset: 0x00006FBF
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.SymmetricExcept(IEnumerable<T> other)
		{
			return this.SymmetricExcept(other);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00008DC8 File Offset: 0x00006FC8
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Union(IEnumerable<T> other)
		{
			return this.Union(other);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00002D65 File Offset: 0x00000F65
		bool ISet<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00002D65 File Offset: 0x00000F65
		void ISet<!0>.ExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00002D65 File Offset: 0x00000F65
		void ISet<!0>.IntersectWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00002D65 File Offset: 0x00000F65
		void ISet<!0>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00002D65 File Offset: 0x00000F65
		void ISet<!0>.UnionWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000323 RID: 803 RVA: 0x000038D6 File Offset: 0x00001AD6
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00008DF7 File Offset: 0x00006FF7
		void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00002D65 File Offset: 0x00000F65
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00002D65 File Offset: 0x00000F65
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00002D65 File Offset: 0x00000F65
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000089 RID: 137
		T IList<!0>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00002D65 File Offset: 0x00000F65
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00002D65 File Offset: 0x00000F65
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600032C RID: 812 RVA: 0x000038D6 File Offset: 0x00001AD6
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600032D RID: 813 RVA: 0x000038D6 File Offset: 0x00001AD6
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600032E RID: 814 RVA: 0x000052C4 File Offset: 0x000034C4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600032F RID: 815 RVA: 0x000038D6 File Offset: 0x00001AD6
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00002D65 File Offset: 0x00000F65
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00002D65 File Offset: 0x00000F65
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00008E53 File Offset: 0x00007053
		bool IList.Contains(object value)
		{
			return this.Contains((T)((object)value));
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00008E61 File Offset: 0x00007061
		int IList.IndexOf(object value)
		{
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00002D65 File Offset: 0x00000F65
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00002D65 File Offset: 0x00000F65
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00002D65 File Offset: 0x00000F65
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700008E RID: 142
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00008E99 File Offset: 0x00007099
		void ICollection.CopyTo(Array array, int index)
		{
			this._root.CopyTo(array, index);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00008EA8 File Offset: 0x000070A8
		[ExcludeFromCodeCoverage]
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00008EA8 File Offset: 0x000070A8
		[ExcludeFromCodeCoverage]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00008EC2 File Offset: 0x000070C2
		public ImmutableSortedSet<T>.Enumerator GetEnumerator()
		{
			return this._root.GetEnumerator();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00008ED0 File Offset: 0x000070D0
		private static bool TryCastToImmutableSortedSet(IEnumerable<T> sequence, out ImmutableSortedSet<T> other)
		{
			other = (sequence as ImmutableSortedSet<T>);
			if (other != null)
			{
				return true;
			}
			ImmutableSortedSet<T>.Builder builder = sequence as ImmutableSortedSet<T>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00008F00 File Offset: 0x00007100
		private static ImmutableSortedSet<T> Wrap(ImmutableSortedSet<T>.Node root, IComparer<T> comparer)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableSortedSet<T>(root, comparer);
			}
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00008F20 File Offset: 0x00007120
		private ImmutableSortedSet<T> UnionIncremental(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableSortedSet<T>.Node node = this._root;
			foreach (T key in items.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				bool flag;
				node = node.Add(key, this._comparer, out flag);
			}
			return this.Wrap(node);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00008F98 File Offset: 0x00007198
		private ImmutableSortedSet<T> Wrap(ImmutableSortedSet<T>.Node root)
		{
			if (root == this._root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableSortedSet<T>(root, this._comparer);
			}
			return this.Clear();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00008FC0 File Offset: 0x000071C0
		private ImmutableSortedSet<T> LeafToRootRefill(IEnumerable<T> addedItems)
		{
			Requires.NotNull<IEnumerable<T>>(addedItems, "addedItems");
			ImmutableSortedSet<T>.Node root = ImmutableSortedSet<T>.Node.NodeTreeFromSortedSet(new SortedSet<T>(this.Concat(addedItems), this.KeyComparer));
			return this.Wrap(root);
		}

		// Token: 0x04000038 RID: 56
		private const float RefillOverIncrementalThreshold = 0.15f;

		// Token: 0x04000039 RID: 57
		public static readonly ImmutableSortedSet<T> Empty = new ImmutableSortedSet<T>(null);

		// Token: 0x0400003A RID: 58
		private readonly ImmutableSortedSet<T>.Node _root;

		// Token: 0x0400003B RID: 59
		private readonly IComparer<T> _comparer;

		// Token: 0x02000069 RID: 105
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableSortedSetBuilderDebuggerProxy<>))]
		public sealed class Builder : ISortKeyCollection<T>, IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable, ISet<!0>, ICollection<!0>, ICollection
		{
			// Token: 0x060005A3 RID: 1443 RVA: 0x0000F72C File Offset: 0x0000D92C
			internal Builder(ImmutableSortedSet<T> set)
			{
				Requires.NotNull<ImmutableSortedSet<T>>(set, "set");
				this._root = set._root;
				this._comparer = set.KeyComparer;
				this._immutable = set;
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000F77F File Offset: 0x0000D97F
			public int Count
			{
				get
				{
					return this.Root.Count;
				}
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x060005A5 RID: 1445 RVA: 0x000020FC File Offset: 0x000002FC
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000126 RID: 294
			public T this[int index]
			{
				get
				{
					return this._root[index];
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000F79D File Offset: 0x0000D99D
			public T Max
			{
				get
				{
					return this._root.Max;
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000F7AA File Offset: 0x0000D9AA
			public T Min
			{
				get
				{
					return this._root.Min;
				}
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000F7B7 File Offset: 0x0000D9B7
			// (set) Token: 0x060005AA RID: 1450 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
			public IComparer<T> KeyComparer
			{
				get
				{
					return this._comparer;
				}
				set
				{
					Requires.NotNull<IComparer<T>>(value, "value");
					if (value != this._comparer)
					{
						ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.EmptyNode;
						foreach (T key in this)
						{
							bool flag;
							node = node.Add(key, value, out flag);
						}
						this._immutable = null;
						this._comparer = value;
						this.Root = node;
					}
				}
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000F844 File Offset: 0x0000DA44
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000F84C File Offset: 0x0000DA4C
			// (set) Token: 0x060005AD RID: 1453 RVA: 0x0000F854 File Offset: 0x0000DA54
			private ImmutableSortedSet<T>.Node Root
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

			// Token: 0x060005AE RID: 1454 RVA: 0x0000F87C File Offset: 0x0000DA7C
			public bool Add(T item)
			{
				bool result;
				this.Root = this.Root.Add(item, this._comparer, out result);
				return result;
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
			public void ExceptWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				foreach (T key in other)
				{
					bool flag;
					this.Root = this.Root.Remove(key, this._comparer, out flag);
				}
			}

			// Token: 0x060005B0 RID: 1456 RVA: 0x0000F90C File Offset: 0x0000DB0C
			public void IntersectWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.EmptyNode;
				foreach (T t in other)
				{
					if (this.Contains(t))
					{
						bool flag;
						node = node.Add(t, this._comparer, out flag);
					}
				}
				this.Root = node;
			}

			// Token: 0x060005B1 RID: 1457 RVA: 0x0000F980 File Offset: 0x0000DB80
			public bool IsProperSubsetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsProperSubsetOf(other);
			}

			// Token: 0x060005B2 RID: 1458 RVA: 0x0000F98E File Offset: 0x0000DB8E
			public bool IsProperSupersetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsProperSupersetOf(other);
			}

			// Token: 0x060005B3 RID: 1459 RVA: 0x0000F99C File Offset: 0x0000DB9C
			public bool IsSubsetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsSubsetOf(other);
			}

			// Token: 0x060005B4 RID: 1460 RVA: 0x0000F9AA File Offset: 0x0000DBAA
			public bool IsSupersetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsSupersetOf(other);
			}

			// Token: 0x060005B5 RID: 1461 RVA: 0x0000F9B8 File Offset: 0x0000DBB8
			public bool Overlaps(IEnumerable<T> other)
			{
				return this.ToImmutable().Overlaps(other);
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x0000F9C6 File Offset: 0x0000DBC6
			public bool SetEquals(IEnumerable<T> other)
			{
				return this.ToImmutable().SetEquals(other);
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x0000F9D4 File Offset: 0x0000DBD4
			public void SymmetricExceptWith(IEnumerable<T> other)
			{
				this.Root = this.ToImmutable().SymmetricExcept(other)._root;
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
			public void UnionWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				foreach (T key in other)
				{
					bool flag;
					this.Root = this.Root.Add(key, this._comparer, out flag);
				}
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x0000FA58 File Offset: 0x0000DC58
			void ICollection<!0>.Add(T item)
			{
				this.Add(item);
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x0000FA62 File Offset: 0x0000DC62
			public void Clear()
			{
				this.Root = ImmutableSortedSet<T>.Node.EmptyNode;
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x0000FA6F File Offset: 0x0000DC6F
			public bool Contains(T item)
			{
				return this.Root.Contains(item, this._comparer);
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x0000FA83 File Offset: 0x0000DC83
			void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
			{
				this._root.CopyTo(array, arrayIndex);
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x0000FA94 File Offset: 0x0000DC94
			public bool Remove(T item)
			{
				bool result;
				this.Root = this.Root.Remove(item, this._comparer, out result);
				return result;
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x0000FABC File Offset: 0x0000DCBC
			public ImmutableSortedSet<T>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x0000FACA File Offset: 0x0000DCCA
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.Root.GetEnumerator();
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x0000FADC File Offset: 0x0000DCDC
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x0000FAE9 File Offset: 0x0000DCE9
			public IEnumerable<T> Reverse()
			{
				return new ImmutableSortedSet<T>.ReverseEnumerable(this._root);
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x0000FAF6 File Offset: 0x0000DCF6
			public ImmutableSortedSet<T> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableSortedSet<T>.Wrap(this.Root, this._comparer);
				}
				return this._immutable;
			}

			// Token: 0x060005C3 RID: 1475 RVA: 0x0000FB1D File Offset: 0x0000DD1D
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex);
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x060005C4 RID: 1476 RVA: 0x000020FC File Offset: 0x000002FC
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000FB2F File Offset: 0x0000DD2F
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

			// Token: 0x040000EA RID: 234
			private ImmutableSortedSet<T>.Node _root = ImmutableSortedSet<T>.Node.EmptyNode;

			// Token: 0x040000EB RID: 235
			private IComparer<T> _comparer = Comparer<T>.Default;

			// Token: 0x040000EC RID: 236
			private ImmutableSortedSet<T> _immutable;

			// Token: 0x040000ED RID: 237
			private int _version;

			// Token: 0x040000EE RID: 238
			private object _syncRoot;
		}

		// Token: 0x0200006A RID: 106
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable, ISecurePooledObjectUser, IStrongEnumerator<T>
		{
			// Token: 0x060005C6 RID: 1478 RVA: 0x0000FB54 File Offset: 0x0000DD54
			internal Enumerator(ImmutableSortedSet<T>.Node root, ImmutableSortedSet<T>.Builder builder = null, bool reverse = false)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
				this._root = root;
				this._builder = builder;
				this._current = null;
				this._reverse = reverse;
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!ImmutableSortedSet<T>.Enumerator.s_enumeratingStacks.TryTake(this, out this._stack))
				{
					this._stack = ImmutableSortedSet<T>.Enumerator.s_enumeratingStacks.PrepNew(this, new Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>(root.Height));
				}
				this.PushNext(this._root);
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000FBF1 File Offset: 0x0000DDF1
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000FBF9 File Offset: 0x0000DDF9
			public T Current
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

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000FC1A File Offset: 0x0000DE1A
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x0000FC28 File Offset: 0x0000DE28
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack;
				if (this._stack != null && this._stack.TryUse<ImmutableSortedSet<T>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedSet<T>.Node>>();
					ImmutableSortedSet<T>.Enumerator.s_enumeratingStacks.TryAdd(this, this._stack);
					this._stack = null;
				}
			}

			// Token: 0x060005CB RID: 1483 RVA: 0x0000FC80 File Offset: 0x0000DE80
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				this.ThrowIfChanged();
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack = this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this);
				if (stack.Count > 0)
				{
					ImmutableSortedSet<T>.Node value = stack.Pop().Value;
					this._current = value;
					this.PushNext(this._reverse ? value.Left : value.Right);
					return true;
				}
				this._current = null;
				return false;
			}

			// Token: 0x060005CC RID: 1484 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._current = null;
				this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this).ClearFastWhenEmpty<RefAsValueType<ImmutableSortedSet<T>.Node>>();
				this.PushNext(this._root);
			}

			// Token: 0x060005CD RID: 1485 RVA: 0x0000FD3B File Offset: 0x0000DF3B
			private void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableSortedSet<T>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableSortedSet<T>.Enumerator>(this);
				}
			}

			// Token: 0x060005CE RID: 1486 RVA: 0x0000FD66 File Offset: 0x0000DF66
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x060005CF RID: 1487 RVA: 0x0000FD90 File Offset: 0x0000DF90
			private void PushNext(ImmutableSortedSet<T>.Node node)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(node, "node");
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack = this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<ImmutableSortedSet<T>.Node>(node));
					node = (this._reverse ? node.Right : node.Left);
				}
			}

			// Token: 0x040000EF RID: 239
			private static readonly SecureObjectPool<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>, ImmutableSortedSet<T>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>, ImmutableSortedSet<T>.Enumerator>();

			// Token: 0x040000F0 RID: 240
			private readonly ImmutableSortedSet<T>.Builder _builder;

			// Token: 0x040000F1 RID: 241
			private readonly int _poolUserId;

			// Token: 0x040000F2 RID: 242
			private readonly bool _reverse;

			// Token: 0x040000F3 RID: 243
			private ImmutableSortedSet<T>.Node _root;

			// Token: 0x040000F4 RID: 244
			private SecurePooledObject<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>> _stack;

			// Token: 0x040000F5 RID: 245
			private ImmutableSortedSet<T>.Node _current;

			// Token: 0x040000F6 RID: 246
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x0200006B RID: 107
		private class ReverseEnumerable : IEnumerable<!0>, IEnumerable
		{
			// Token: 0x060005D1 RID: 1489 RVA: 0x0000FDEF File Offset: 0x0000DFEF
			internal ReverseEnumerable(ImmutableSortedSet<T>.Node root)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
				this._root = root;
			}

			// Token: 0x060005D2 RID: 1490 RVA: 0x0000FE09 File Offset: 0x0000E009
			public IEnumerator<T> GetEnumerator()
			{
				return this._root.Reverse();
			}

			// Token: 0x060005D3 RID: 1491 RVA: 0x0000FE16 File Offset: 0x0000E016
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040000F7 RID: 247
			private readonly ImmutableSortedSet<T>.Node _root;
		}

		// Token: 0x0200006C RID: 108
		[DebuggerDisplay("{_key}")]
		internal sealed class Node : IBinaryTree<T>, IBinaryTree, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x060005D4 RID: 1492 RVA: 0x0000FE1E File Offset: 0x0000E01E
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x060005D5 RID: 1493 RVA: 0x0000FE30 File Offset: 0x0000E030
			private Node(T key, ImmutableSortedSet<T>.Node left, ImmutableSortedSet<T>.Node right, bool frozen = false)
			{
				Requires.NotNullAllowStructs<T>(key, "key");
				Requires.NotNull<ImmutableSortedSet<T>.Node>(left, "left");
				Requires.NotNull<ImmutableSortedSet<T>.Node>(right, "right");
				this._key = key;
				this._left = left;
				this._right = right;
				this._height = checked(1 + Math.Max(left._height, right._height));
				this._count = 1 + left._count + right._count;
				this._frozen = frozen;
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0000FEB0 File Offset: 0x0000E0B0
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0000FEBB File Offset: 0x0000E0BB
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0000FEC3 File Offset: 0x0000E0C3
			public ImmutableSortedSet<T>.Node Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000FEC3 File Offset: 0x0000E0C3
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000FED3 File Offset: 0x0000E0D3
			public ImmutableSortedSet<T>.Node Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000FED3 File Offset: 0x0000E0D3
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x060005DC RID: 1500 RVA: 0x0000FEC3 File Offset: 0x0000E0C3
			IBinaryTree<T> IBinaryTree<!0>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000FED3 File Offset: 0x0000E0D3
			IBinaryTree<T> IBinaryTree<!0>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000FEF3 File Offset: 0x0000E0F3
			public T Value
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000FEFB File Offset: 0x0000E0FB
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000FEF3 File Offset: 0x0000E0F3
			internal T Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000FF0C File Offset: 0x0000E10C
			internal T Max
			{
				get
				{
					if (this.IsEmpty)
					{
						return default(T);
					}
					ImmutableSortedSet<T>.Node node = this;
					while (!node._right.IsEmpty)
					{
						node = node._right;
					}
					return node._key;
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000FF4C File Offset: 0x0000E14C
			internal T Min
			{
				get
				{
					if (this.IsEmpty)
					{
						return default(T);
					}
					ImmutableSortedSet<T>.Node node = this;
					while (!node._left.IsEmpty)
					{
						node = node._left;
					}
					return node._key;
				}
			}

			// Token: 0x1700013E RID: 318
			internal T this[int index]
			{
				get
				{
					Requires.Range(index >= 0 && index < this.Count, "index", null);
					if (index < this._left._count)
					{
						return this._left[index];
					}
					if (index > this._left._count)
					{
						return this._right[index - this._left._count - 1];
					}
					return this._key;
				}
			}

			// Token: 0x060005E4 RID: 1508 RVA: 0x0000FFFE File Offset: 0x0000E1FE
			public ImmutableSortedSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableSortedSet<T>.Enumerator(this, null, false);
			}

			// Token: 0x060005E5 RID: 1509 RVA: 0x00010008 File Offset: 0x0000E208
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060005E6 RID: 1510 RVA: 0x00010008 File Offset: 0x0000E208
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060005E7 RID: 1511 RVA: 0x00010022 File Offset: 0x0000E222
			internal ImmutableSortedSet<T>.Enumerator GetEnumerator(ImmutableSortedSet<T>.Builder builder)
			{
				return new ImmutableSortedSet<T>.Enumerator(this, builder, false);
			}

			// Token: 0x060005E8 RID: 1512 RVA: 0x0001002C File Offset: 0x0000E22C
			internal static ImmutableSortedSet<T>.Node NodeTreeFromSortedSet(SortedSet<T> collection)
			{
				Requires.NotNull<SortedSet<T>>(collection, "collection");
				if (collection.Count == 0)
				{
					return ImmutableSortedSet<T>.Node.EmptyNode;
				}
				IOrderedCollection<T> orderedCollection = collection.AsOrderedCollection<T>();
				return ImmutableSortedSet<T>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count);
			}

			// Token: 0x060005E9 RID: 1513 RVA: 0x00010068 File Offset: 0x0000E268
			internal void CopyTo(T[] array, int arrayIndex)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (T t in this)
				{
					array[arrayIndex++] = t;
				}
			}

			// Token: 0x060005EA RID: 1514 RVA: 0x000100F4 File Offset: 0x0000E2F4
			internal void CopyTo(Array array, int arrayIndex)
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

			// Token: 0x060005EB RID: 1515 RVA: 0x00010194 File Offset: 0x0000E394
			internal ImmutableSortedSet<T>.Node Add(T key, IComparer<T> comparer, out bool mutated)
			{
				Requires.NotNullAllowStructs<T>(key, "key");
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					mutated = true;
					return new ImmutableSortedSet<T>.Node(key, this, this, false);
				}
				ImmutableSortedSet<T>.Node node = this;
				int num = comparer.Compare(key, this._key);
				if (num > 0)
				{
					ImmutableSortedSet<T>.Node right = this._right.Add(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, right);
					}
				}
				else
				{
					if (num >= 0)
					{
						mutated = false;
						return this;
					}
					ImmutableSortedSet<T>.Node left = this._left.Add(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(left, null);
					}
				}
				if (!mutated)
				{
					return node;
				}
				return ImmutableSortedSet<T>.Node.MakeBalanced(node);
			}

			// Token: 0x060005EC RID: 1516 RVA: 0x00010234 File Offset: 0x0000E434
			internal ImmutableSortedSet<T>.Node Remove(T key, IComparer<T> comparer, out bool mutated)
			{
				Requires.NotNullAllowStructs<T>(key, "key");
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					mutated = false;
					return this;
				}
				ImmutableSortedSet<T>.Node node = this;
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					mutated = true;
					if (this._right.IsEmpty && this._left.IsEmpty)
					{
						node = ImmutableSortedSet<T>.Node.EmptyNode;
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
						ImmutableSortedSet<T>.Node node2 = this._right;
						while (!node2._left.IsEmpty)
						{
							node2 = node2._left;
						}
						bool flag;
						ImmutableSortedSet<T>.Node right = this._right.Remove(node2._key, comparer, out flag);
						node = node2.Mutate(this._left, right);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedSet<T>.Node left = this._left.Remove(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(left, null);
					}
				}
				else
				{
					ImmutableSortedSet<T>.Node right2 = this._right.Remove(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, right2);
					}
				}
				if (!node.IsEmpty)
				{
					return ImmutableSortedSet<T>.Node.MakeBalanced(node);
				}
				return node;
			}

			// Token: 0x060005ED RID: 1517 RVA: 0x00010384 File Offset: 0x0000E584
			internal bool Contains(T key, IComparer<T> comparer)
			{
				Requires.NotNullAllowStructs<T>(key, "key");
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				return !this.Search(key, comparer).IsEmpty;
			}

			// Token: 0x060005EE RID: 1518 RVA: 0x000103AC File Offset: 0x0000E5AC
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x060005EF RID: 1519 RVA: 0x000103D4 File Offset: 0x0000E5D4
			internal ImmutableSortedSet<T>.Node Search(T key, IComparer<T> comparer)
			{
				Requires.NotNullAllowStructs<T>(key, "key");
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					return this;
				}
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					return this;
				}
				if (num > 0)
				{
					return this._right.Search(key, comparer);
				}
				return this._left.Search(key, comparer);
			}

			// Token: 0x060005F0 RID: 1520 RVA: 0x00010434 File Offset: 0x0000E634
			internal int IndexOf(T key, IComparer<T> comparer)
			{
				Requires.NotNullAllowStructs<T>(key, "key");
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					return -1;
				}
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					return this._left.Count;
				}
				if (num > 0)
				{
					int num2 = this._right.IndexOf(key, comparer);
					bool flag = num2 < 0;
					if (flag)
					{
						num2 = ~num2;
					}
					num2 = this._left.Count + 1 + num2;
					if (flag)
					{
						num2 = ~num2;
					}
					return num2;
				}
				return this._left.IndexOf(key, comparer);
			}

			// Token: 0x060005F1 RID: 1521 RVA: 0x000104BE File Offset: 0x0000E6BE
			internal IEnumerator<T> Reverse()
			{
				return new ImmutableSortedSet<T>.Enumerator(this, null, true);
			}

			// Token: 0x060005F2 RID: 1522 RVA: 0x000104D0 File Offset: 0x0000E6D0
			private static ImmutableSortedSet<T>.Node RotateLeft(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node right = tree._right;
				return right.Mutate(tree.Mutate(null, right._left), null);
			}

			// Token: 0x060005F3 RID: 1523 RVA: 0x00010514 File Offset: 0x0000E714
			private static ImmutableSortedSet<T>.Node RotateRight(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node left = tree._left;
				return left.Mutate(null, tree.Mutate(left._right, null));
			}

			// Token: 0x060005F4 RID: 1524 RVA: 0x00010556 File Offset: 0x0000E756
			private static ImmutableSortedSet<T>.Node DoubleLeft(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				return ImmutableSortedSet<T>.Node.RotateLeft(tree.Mutate(null, ImmutableSortedSet<T>.Node.RotateRight(tree._right)));
			}

			// Token: 0x060005F5 RID: 1525 RVA: 0x00010589 File Offset: 0x0000E789
			private static ImmutableSortedSet<T>.Node DoubleRight(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				return ImmutableSortedSet<T>.Node.RotateRight(tree.Mutate(ImmutableSortedSet<T>.Node.RotateLeft(tree._left), null));
			}

			// Token: 0x060005F6 RID: 1526 RVA: 0x000105BC File Offset: 0x0000E7BC
			private static int Balance(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return (int)(tree._right._height - tree._left._height);
			}

			// Token: 0x060005F7 RID: 1527 RVA: 0x000105E0 File Offset: 0x0000E7E0
			private static bool IsRightHeavy(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return ImmutableSortedSet<T>.Node.Balance(tree) >= 2;
			}

			// Token: 0x060005F8 RID: 1528 RVA: 0x000105F9 File Offset: 0x0000E7F9
			private static bool IsLeftHeavy(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return ImmutableSortedSet<T>.Node.Balance(tree) <= -2;
			}

			// Token: 0x060005F9 RID: 1529 RVA: 0x00010614 File Offset: 0x0000E814
			private static ImmutableSortedSet<T>.Node MakeBalanced(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (ImmutableSortedSet<T>.Node.IsRightHeavy(tree))
				{
					if (ImmutableSortedSet<T>.Node.Balance(tree._right) >= 0)
					{
						return ImmutableSortedSet<T>.Node.RotateLeft(tree);
					}
					return ImmutableSortedSet<T>.Node.DoubleLeft(tree);
				}
				else
				{
					if (!ImmutableSortedSet<T>.Node.IsLeftHeavy(tree))
					{
						return tree;
					}
					if (ImmutableSortedSet<T>.Node.Balance(tree._left) <= 0)
					{
						return ImmutableSortedSet<T>.Node.RotateRight(tree);
					}
					return ImmutableSortedSet<T>.Node.DoubleRight(tree);
				}
			}

			// Token: 0x060005FA RID: 1530 RVA: 0x00010678 File Offset: 0x0000E878
			private static ImmutableSortedSet<T>.Node NodeTreeFromList(IOrderedCollection<T> items, int start, int length)
			{
				Requires.NotNull<IOrderedCollection<T>>(items, "items");
				if (length == 0)
				{
					return ImmutableSortedSet<T>.Node.EmptyNode;
				}
				int num = (length - 1) / 2;
				int num2 = length - 1 - num;
				ImmutableSortedSet<T>.Node left = ImmutableSortedSet<T>.Node.NodeTreeFromList(items, start, num2);
				ImmutableSortedSet<T>.Node right = ImmutableSortedSet<T>.Node.NodeTreeFromList(items, start + num2 + 1, num);
				return new ImmutableSortedSet<T>.Node(items[start + num2], left, right, true);
			}

			// Token: 0x060005FB RID: 1531 RVA: 0x000106CC File Offset: 0x0000E8CC
			private ImmutableSortedSet<T>.Node Mutate(ImmutableSortedSet<T>.Node left = null, ImmutableSortedSet<T>.Node right = null)
			{
				if (this._frozen)
				{
					return new ImmutableSortedSet<T>.Node(this._key, left ?? this._left, right ?? this._right, false);
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
				this._count = 1 + this._left._count + this._right._count;
				return this;
			}

			// Token: 0x040000F8 RID: 248
			internal static readonly ImmutableSortedSet<T>.Node EmptyNode = new ImmutableSortedSet<T>.Node();

			// Token: 0x040000F9 RID: 249
			private readonly T _key;

			// Token: 0x040000FA RID: 250
			private bool _frozen;

			// Token: 0x040000FB RID: 251
			private byte _height;

			// Token: 0x040000FC RID: 252
			private int _count;

			// Token: 0x040000FD RID: 253
			private ImmutableSortedSet<T>.Node _left;

			// Token: 0x040000FE RID: 254
			private ImmutableSortedSet<T>.Node _right;
		}
	}
}
