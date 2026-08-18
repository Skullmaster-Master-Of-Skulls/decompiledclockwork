using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000022 RID: 34
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableHashSetDebuggerProxy<>))]
	public sealed class ImmutableHashSet<T> : IImmutableSet<!0>, IReadOnlyCollection<!0>, IEnumerable<!0>, IEnumerable, IHashKeyCollection<T>, ICollection<!0>, ISet<!0>, ICollection, IStrongEnumerable<T, ImmutableHashSet<T>.Enumerator>
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00005A09 File Offset: 0x00003C09
		internal ImmutableHashSet(IEqualityComparer<T> equalityComparer) : this(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode, equalityComparer, 0)
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00005A18 File Offset: 0x00003C18
		private ImmutableHashSet(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, IEqualityComparer<T> equalityComparer, int count)
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			root.Freeze(ImmutableHashSet<T>.s_FreezeBucketAction);
			this._root = root;
			this._count = count;
			this._equalityComparer = equalityComparer;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005A56 File Offset: 0x00003C56
		public ImmutableHashSet<T> Clear()
		{
			if (!this.IsEmpty)
			{
				return ImmutableHashSet<T>.Empty.WithComparer(this._equalityComparer);
			}
			return this;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00005A72 File Offset: 0x00003C72
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00005A7A File Offset: 0x00003C7A
		public bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00005A85 File Offset: 0x00003C85
		public IEqualityComparer<T> KeyComparer
		{
			get
			{
				return this._equalityComparer;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005A8D File Offset: 0x00003C8D
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000052C4 File Offset: 0x000034C4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000038D6 File Offset: 0x00001AD6
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00005A9B File Offset: 0x00003C9B
		internal IBinaryTree Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00005AA3 File Offset: 0x00003CA3
		private ImmutableHashSet<T>.MutationInput Origin
		{
			get
			{
				return new ImmutableHashSet<T>.MutationInput(this);
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00005AAB File Offset: 0x00003CAB
		public ImmutableHashSet<T>.Builder ToBuilder()
		{
			return new ImmutableHashSet<T>.Builder(this);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005AB4 File Offset: 0x00003CB4
		public ImmutableHashSet<T> Add(T item)
		{
			Requires.NotNullAllowStructs<T>(item, "item");
			return ImmutableHashSet<T>.Add(item, this.Origin).Finalize(this);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005AE4 File Offset: 0x00003CE4
		public ImmutableHashSet<T> Remove(T item)
		{
			Requires.NotNullAllowStructs<T>(item, "item");
			return ImmutableHashSet<T>.Remove(item, this.Origin).Finalize(this);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005B14 File Offset: 0x00003D14
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			Requires.NotNullAllowStructs<T>(equalValue, "value");
			int hashCode = this._equalityComparer.GetHashCode(equalValue);
			ImmutableHashSet<T>.HashBucket hashBucket;
			if (this._root.TryGetValue(hashCode, out hashBucket))
			{
				return hashBucket.TryExchange(equalValue, this._equalityComparer, out actualValue);
			}
			actualValue = equalValue;
			return false;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00005B61 File Offset: 0x00003D61
		public ImmutableHashSet<T> Union(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return this.Union(other, false);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005B78 File Offset: 0x00003D78
		public ImmutableHashSet<T> Intersect(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Intersect(other, this.Origin).Finalize(this);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005BA8 File Offset: 0x00003DA8
		public ImmutableHashSet<T> Except(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Except(other, this._equalityComparer, this._root).Finalize(this);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005BDC File Offset: 0x00003DDC
		public ImmutableHashSet<T> SymmetricExcept(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.SymmetricExcept(other, this.Origin).Finalize(this);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005C09 File Offset: 0x00003E09
		public bool SetEquals(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return this == other || ImmutableHashSet<T>.SetEquals(other, this.Origin);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005C28 File Offset: 0x00003E28
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsProperSubsetOf(other, this.Origin);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005C41 File Offset: 0x00003E41
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsProperSupersetOf(other, this.Origin);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005C5A File Offset: 0x00003E5A
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsSubsetOf(other, this.Origin);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005C73 File Offset: 0x00003E73
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsSupersetOf(other, this.Origin);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005C8C File Offset: 0x00003E8C
		public bool Overlaps(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Overlaps(other, this.Origin);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005CA5 File Offset: 0x00003EA5
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Add(T item)
		{
			return this.Add(item);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005CAE File Offset: 0x00003EAE
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Remove(T item)
		{
			return this.Remove(item);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005CB7 File Offset: 0x00003EB7
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Union(IEnumerable<T> other)
		{
			return this.Union(other);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005CC0 File Offset: 0x00003EC0
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Intersect(IEnumerable<T> other)
		{
			return this.Intersect(other);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005CC9 File Offset: 0x00003EC9
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Except(IEnumerable<T> other)
		{
			return this.Except(other);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005CD2 File Offset: 0x00003ED2
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.SymmetricExcept(IEnumerable<T> other)
		{
			return this.SymmetricExcept(other);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00005CDB File Offset: 0x00003EDB
		public bool Contains(T item)
		{
			Requires.NotNullAllowStructs<T>(item, "item");
			return ImmutableHashSet<T>.Contains(item, this.Origin);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00005CF4 File Offset: 0x00003EF4
		public ImmutableHashSet<T> WithComparer(IEqualityComparer<T> equalityComparer)
		{
			if (equalityComparer == null)
			{
				equalityComparer = EqualityComparer<T>.Default;
			}
			if (equalityComparer == this._equalityComparer)
			{
				return this;
			}
			return new ImmutableHashSet<T>(equalityComparer).Union(this, true);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00002D65 File Offset: 0x00000F65
		bool ISet<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00002D65 File Offset: 0x00000F65
		void ISet<!0>.ExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00002D65 File Offset: 0x00000F65
		void ISet<!0>.IntersectWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00002D65 File Offset: 0x00000F65
		void ISet<!0>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00002D65 File Offset: 0x00000F65
		void ISet<!0>.UnionWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000038D6 File Offset: 0x00001AD6
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005D40 File Offset: 0x00003F40
		void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
		{
			Requires.NotNull<T[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00002D65 File Offset: 0x00000F65
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00002D65 File Offset: 0x00000F65
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00002D65 File Offset: 0x00000F65
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005DE4 File Offset: 0x00003FE4
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

		// Token: 0x060001E7 RID: 487 RVA: 0x00005E84 File Offset: 0x00004084
		public ImmutableHashSet<T>.Enumerator GetEnumerator()
		{
			return new ImmutableHashSet<T>.Enumerator(this._root, null);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005E92 File Offset: 0x00004092
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005E92 File Offset: 0x00004092
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005EAC File Offset: 0x000040AC
		private static bool IsSupersetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			using (DisposableEnumeratorAdapter<T, ImmutableHashSet<T>.Enumerator> enumerator = other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!ImmutableHashSet<T>.Contains(enumerator.Current, origin))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00005F14 File Offset: 0x00004114
		private static ImmutableHashSet<T>.MutationResult Add(T item, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNullAllowStructs<T>(item, "item");
			int hashCode = origin.EqualityComparer.GetHashCode(item);
			ImmutableHashSet<T>.OperationResult operationResult;
			ImmutableHashSet<T>.HashBucket newBucket = origin.Root.GetValueOrDefault(hashCode).Add(item, origin.EqualityComparer, out operationResult);
			if (operationResult == ImmutableHashSet<T>.OperationResult.NoChangeRequired)
			{
				return new ImmutableHashSet<T>.MutationResult(origin.Root, 0, ImmutableHashSet<T>.CountType.Adjustment);
			}
			return new ImmutableHashSet<T>.MutationResult(ImmutableHashSet<T>.UpdateRoot(origin.Root, hashCode, newBucket), 1, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00005F84 File Offset: 0x00004184
		private static ImmutableHashSet<T>.MutationResult Remove(T item, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNullAllowStructs<T>(item, "item");
			ImmutableHashSet<T>.OperationResult operationResult = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
			int hashCode = origin.EqualityComparer.GetHashCode(item);
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root = origin.Root;
			ImmutableHashSet<T>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(hashCode, out hashBucket))
			{
				ImmutableHashSet<T>.HashBucket newBucket = hashBucket.Remove(item, origin.EqualityComparer, out operationResult);
				if (operationResult == ImmutableHashSet<T>.OperationResult.NoChangeRequired)
				{
					return new ImmutableHashSet<T>.MutationResult(origin.Root, 0, ImmutableHashSet<T>.CountType.Adjustment);
				}
				root = ImmutableHashSet<T>.UpdateRoot(origin.Root, hashCode, newBucket);
			}
			return new ImmutableHashSet<T>.MutationResult(root, (operationResult == ImmutableHashSet<T>.OperationResult.SizeChanged) ? -1 : 0, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000600C File Offset: 0x0000420C
		private static bool Contains(T item, ImmutableHashSet<T>.MutationInput origin)
		{
			int hashCode = origin.EqualityComparer.GetHashCode(item);
			ImmutableHashSet<T>.HashBucket hashBucket;
			return origin.Root.TryGetValue(hashCode, out hashBucket) && hashBucket.Contains(item, origin.EqualityComparer);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000604C File Offset: 0x0000424C
		private static ImmutableHashSet<T>.MutationResult Union(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			int num = 0;
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> sortedInt32KeyNode = origin.Root;
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				int hashCode = origin.EqualityComparer.GetHashCode(t);
				ImmutableHashSet<T>.OperationResult operationResult;
				ImmutableHashSet<T>.HashBucket newBucket = sortedInt32KeyNode.GetValueOrDefault(hashCode).Add(t, origin.EqualityComparer, out operationResult);
				if (operationResult == ImmutableHashSet<T>.OperationResult.SizeChanged)
				{
					sortedInt32KeyNode = ImmutableHashSet<T>.UpdateRoot(sortedInt32KeyNode, hashCode, newBucket);
					num++;
				}
			}
			return new ImmutableHashSet<T>.MutationResult(sortedInt32KeyNode, num, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000060FC File Offset: 0x000042FC
		private static bool Overlaps(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return false;
			}
			using (DisposableEnumeratorAdapter<T, ImmutableHashSet<T>.Enumerator> enumerator = other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (ImmutableHashSet<T>.Contains(enumerator.Current, origin))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006174 File Offset: 0x00004374
		private static bool SetEquals(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			HashSet<T> hashSet = new HashSet<T>(other, origin.EqualityComparer);
			if (origin.Count != hashSet.Count)
			{
				return false;
			}
			int num = 0;
			using (HashSet<T>.Enumerator enumerator = hashSet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!ImmutableHashSet<T>.Contains(enumerator.Current, origin))
					{
						return false;
					}
					num++;
				}
			}
			return num == origin.Count;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006204 File Offset: 0x00004404
		private static SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> UpdateRoot(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, int hashCode, ImmutableHashSet<T>.HashBucket newBucket)
		{
			bool flag;
			if (newBucket.IsEmpty)
			{
				return root.Remove(hashCode, out flag);
			}
			bool flag2;
			return root.SetItem(hashCode, newBucket, EqualityComparer<ImmutableHashSet<T>.HashBucket>.Default, out flag2, out flag);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006238 File Offset: 0x00004438
		private static ImmutableHashSet<T>.MutationResult Intersect(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;
			int num = 0;
			foreach (T item in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				if (ImmutableHashSet<T>.Contains(item, origin))
				{
					ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Add(item, new ImmutableHashSet<T>.MutationInput(root, origin.EqualityComparer, num));
					root = mutationResult.Root;
					num += mutationResult.Count;
				}
			}
			return new ImmutableHashSet<T>.MutationResult(root, num, ImmutableHashSet<T>.CountType.FinalValue);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000062D8 File Offset: 0x000044D8
		private static ImmutableHashSet<T>.MutationResult Except(IEnumerable<T> other, IEqualityComparer<T> equalityComparer, SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
			int num = 0;
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> sortedInt32KeyNode = root;
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				int hashCode = equalityComparer.GetHashCode(t);
				ImmutableHashSet<T>.HashBucket hashBucket;
				if (sortedInt32KeyNode.TryGetValue(hashCode, out hashBucket))
				{
					ImmutableHashSet<T>.OperationResult operationResult;
					ImmutableHashSet<T>.HashBucket newBucket = hashBucket.Remove(t, equalityComparer, out operationResult);
					if (operationResult == ImmutableHashSet<T>.OperationResult.SizeChanged)
					{
						num--;
						sortedInt32KeyNode = ImmutableHashSet<T>.UpdateRoot(sortedInt32KeyNode, hashCode, newBucket);
					}
				}
			}
			return new ImmutableHashSet<T>.MutationResult(sortedInt32KeyNode, num, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000638C File Offset: 0x0000458C
		private static ImmutableHashSet<T>.MutationResult SymmetricExcept(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableHashSet<T> immutableHashSet = ImmutableHashSet<T>.Empty.Union(other);
			int num = 0;
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;
			foreach (T item in new ImmutableHashSet<T>.NodeEnumerable(origin.Root))
			{
				if (!immutableHashSet.Contains(item))
				{
					ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Add(item, new ImmutableHashSet<T>.MutationInput(root, origin.EqualityComparer, num));
					root = mutationResult.Root;
					num += mutationResult.Count;
				}
			}
			foreach (T item2 in immutableHashSet)
			{
				if (!ImmutableHashSet<T>.Contains(item2, origin))
				{
					ImmutableHashSet<T>.MutationResult mutationResult2 = ImmutableHashSet<T>.Add(item2, new ImmutableHashSet<T>.MutationInput(root, origin.EqualityComparer, num));
					root = mutationResult2.Root;
					num += mutationResult2.Count;
				}
			}
			return new ImmutableHashSet<T>.MutationResult(root, num, ImmutableHashSet<T>.CountType.FinalValue);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000064A8 File Offset: 0x000046A8
		private static bool IsProperSubsetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return other.Any<T>();
			}
			HashSet<T> hashSet = new HashSet<T>(other, origin.EqualityComparer);
			if (origin.Count >= hashSet.Count)
			{
				return false;
			}
			int num = 0;
			bool flag = false;
			using (HashSet<T>.Enumerator enumerator = hashSet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (ImmutableHashSet<T>.Contains(enumerator.Current, origin))
					{
						num++;
					}
					else
					{
						flag = true;
					}
					if (num == origin.Count && flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006558 File Offset: 0x00004758
		private static bool IsProperSupersetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return false;
			}
			int num = 0;
			foreach (T item in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				num++;
				if (!ImmutableHashSet<T>.Contains(item, origin))
				{
					return false;
				}
			}
			return origin.Count > num;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000065E0 File Offset: 0x000047E0
		private static bool IsSubsetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return true;
			}
			HashSet<T> hashSet = new HashSet<T>(other, origin.EqualityComparer);
			int num = 0;
			using (HashSet<T>.Enumerator enumerator = hashSet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (ImmutableHashSet<T>.Contains(enumerator.Current, origin))
					{
						num++;
					}
				}
			}
			return num == origin.Count;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006668 File Offset: 0x00004868
		private static ImmutableHashSet<T> Wrap(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, IEqualityComparer<T> equalityComparer, int count)
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			Requires.Range(count >= 0, "count", null);
			return new ImmutableHashSet<T>(root, equalityComparer, count);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000669A File Offset: 0x0000489A
		private ImmutableHashSet<T> Wrap(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, int adjustedCountIfDifferentRoot)
		{
			if (root == this._root)
			{
				return this;
			}
			return new ImmutableHashSet<T>(root, this._equalityComparer, adjustedCountIfDifferentRoot);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000066B4 File Offset: 0x000048B4
		private ImmutableHashSet<T> Union(IEnumerable<T> items, bool avoidWithComparer)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (this.IsEmpty && !avoidWithComparer)
			{
				ImmutableHashSet<T> immutableHashSet = items as ImmutableHashSet<T>;
				if (immutableHashSet != null)
				{
					return immutableHashSet.WithComparer(this.KeyComparer);
				}
			}
			return ImmutableHashSet<T>.Union(items, this.Origin).Finalize(this);
		}

		// Token: 0x0400001C RID: 28
		public static readonly ImmutableHashSet<T> Empty = new ImmutableHashSet<T>(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode, EqualityComparer<T>.Default, 0);

		// Token: 0x0400001D RID: 29
		private static readonly Action<KeyValuePair<int, ImmutableHashSet<T>.HashBucket>> s_FreezeBucketAction = delegate(KeyValuePair<int, ImmutableHashSet<T>.HashBucket> kv)
		{
			kv.Value.Freeze();
		};

		// Token: 0x0400001E RID: 30
		private readonly IEqualityComparer<T> _equalityComparer;

		// Token: 0x0400001F RID: 31
		private readonly int _count;

		// Token: 0x04000020 RID: 32
		private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

		// Token: 0x02000057 RID: 87
		[DebuggerDisplay("Count = {Count}")]
		public sealed class Builder : IReadOnlyCollection<!0>, IEnumerable<!0>, IEnumerable, ISet<!0>, ICollection<!0>
		{
			// Token: 0x0600045D RID: 1117 RVA: 0x0000B94C File Offset: 0x00009B4C
			internal Builder(ImmutableHashSet<T> set)
			{
				Requires.NotNull<ImmutableHashSet<T>>(set, "set");
				this._root = set._root;
				this._count = set._count;
				this._equalityComparer = set._equalityComparer;
				this._immutable = set;
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000B9A0 File Offset: 0x00009BA0
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x0600045F RID: 1119 RVA: 0x000020FC File Offset: 0x000002FC
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000B9AB File Offset: 0x00009BAB
			// (set) Token: 0x06000461 RID: 1121 RVA: 0x0000B9B4 File Offset: 0x00009BB4
			public IEqualityComparer<T> KeyComparer
			{
				get
				{
					return this._equalityComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<T>>(value, "value");
					if (value != this._equalityComparer)
					{
						ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Union(this, new ImmutableHashSet<T>.MutationInput(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode, value, 0));
						this._immutable = null;
						this._equalityComparer = value;
						this.Root = mutationResult.Root;
						this._count = mutationResult.Count;
					}
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000BA10 File Offset: 0x00009C10
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000BA18 File Offset: 0x00009C18
			private ImmutableHashSet<T>.MutationInput Origin
			{
				get
				{
					return new ImmutableHashSet<T>.MutationInput(this.Root, this._equalityComparer, this._count);
				}
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000BA31 File Offset: 0x00009C31
			// (set) Token: 0x06000465 RID: 1125 RVA: 0x0000BA39 File Offset: 0x00009C39
			private SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> Root
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

			// Token: 0x06000466 RID: 1126 RVA: 0x0000BA60 File Offset: 0x00009C60
			public ImmutableHashSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.Enumerator(this._root, this);
			}

			// Token: 0x06000467 RID: 1127 RVA: 0x0000BA6E File Offset: 0x00009C6E
			public ImmutableHashSet<T> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableHashSet<T>.Wrap(this._root, this._equalityComparer, this._count);
				}
				return this._immutable;
			}

			// Token: 0x06000468 RID: 1128 RVA: 0x0000BA9C File Offset: 0x00009C9C
			public bool Add(T item)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Add(item, this.Origin);
				this.Apply(result);
				return result.Count != 0;
			}

			// Token: 0x06000469 RID: 1129 RVA: 0x0000BAC8 File Offset: 0x00009CC8
			public bool Remove(T item)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Remove(item, this.Origin);
				this.Apply(result);
				return result.Count != 0;
			}

			// Token: 0x0600046A RID: 1130 RVA: 0x0000BAF3 File Offset: 0x00009CF3
			public bool Contains(T item)
			{
				return ImmutableHashSet<T>.Contains(item, this.Origin);
			}

			// Token: 0x0600046B RID: 1131 RVA: 0x0000BB01 File Offset: 0x00009D01
			public void Clear()
			{
				this._count = 0;
				this.Root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;
			}

			// Token: 0x0600046C RID: 1132 RVA: 0x0000BB18 File Offset: 0x00009D18
			public void ExceptWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Except(other, this._equalityComparer, this._root);
				this.Apply(result);
			}

			// Token: 0x0600046D RID: 1133 RVA: 0x0000BB40 File Offset: 0x00009D40
			public void IntersectWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Intersect(other, this.Origin);
				this.Apply(result);
			}

			// Token: 0x0600046E RID: 1134 RVA: 0x0000BB61 File Offset: 0x00009D61
			public bool IsProperSubsetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsProperSubsetOf(other, this.Origin);
			}

			// Token: 0x0600046F RID: 1135 RVA: 0x0000BB6F File Offset: 0x00009D6F
			public bool IsProperSupersetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsProperSupersetOf(other, this.Origin);
			}

			// Token: 0x06000470 RID: 1136 RVA: 0x0000BB7D File Offset: 0x00009D7D
			public bool IsSubsetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsSubsetOf(other, this.Origin);
			}

			// Token: 0x06000471 RID: 1137 RVA: 0x0000BB8B File Offset: 0x00009D8B
			public bool IsSupersetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsSupersetOf(other, this.Origin);
			}

			// Token: 0x06000472 RID: 1138 RVA: 0x0000BB99 File Offset: 0x00009D99
			public bool Overlaps(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.Overlaps(other, this.Origin);
			}

			// Token: 0x06000473 RID: 1139 RVA: 0x0000BBA7 File Offset: 0x00009DA7
			public bool SetEquals(IEnumerable<T> other)
			{
				return this == other || ImmutableHashSet<T>.SetEquals(other, this.Origin);
			}

			// Token: 0x06000474 RID: 1140 RVA: 0x0000BBBC File Offset: 0x00009DBC
			public void SymmetricExceptWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.SymmetricExcept(other, this.Origin);
				this.Apply(result);
			}

			// Token: 0x06000475 RID: 1141 RVA: 0x0000BBE0 File Offset: 0x00009DE0
			public void UnionWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Union(other, this.Origin);
				this.Apply(result);
			}

			// Token: 0x06000476 RID: 1142 RVA: 0x0000BC01 File Offset: 0x00009E01
			void ICollection<!0>.Add(T item)
			{
				this.Add(item);
			}

			// Token: 0x06000477 RID: 1143 RVA: 0x0000BC0C File Offset: 0x00009E0C
			void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (T t in this)
				{
					array[arrayIndex++] = t;
				}
			}

			// Token: 0x06000478 RID: 1144 RVA: 0x0000BC98 File Offset: 0x00009E98
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000479 RID: 1145 RVA: 0x0000BC98 File Offset: 0x00009E98
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600047A RID: 1146 RVA: 0x0000BCB2 File Offset: 0x00009EB2
			private void Apply(ImmutableHashSet<T>.MutationResult result)
			{
				this.Root = result.Root;
				if (result.CountType == ImmutableHashSet<T>.CountType.Adjustment)
				{
					this._count += result.Count;
					return;
				}
				this._count = result.Count;
			}

			// Token: 0x0400009D RID: 157
			private SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;

			// Token: 0x0400009E RID: 158
			private IEqualityComparer<T> _equalityComparer;

			// Token: 0x0400009F RID: 159
			private int _count;

			// Token: 0x040000A0 RID: 160
			private ImmutableHashSet<T> _immutable;

			// Token: 0x040000A1 RID: 161
			private int _version;
		}

		// Token: 0x02000058 RID: 88
		public struct Enumerator : IEnumerator<!0>, IEnumerator, IDisposable, IStrongEnumerator<T>
		{
			// Token: 0x0600047B RID: 1147 RVA: 0x0000BCEC File Offset: 0x00009EEC
			internal Enumerator(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, ImmutableHashSet<T>.Builder builder = null)
			{
				this._builder = builder;
				this._mapEnumerator = new SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.Enumerator(root);
				this._bucketEnumerator = default(ImmutableHashSet<T>.HashBucket.Enumerator);
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000BD1F File Offset: 0x00009F1F
			public T Current
			{
				get
				{
					this._mapEnumerator.ThrowIfDisposed();
					return this._bucketEnumerator.Current;
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000BD37 File Offset: 0x00009F37
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600047E RID: 1150 RVA: 0x0000BD44 File Offset: 0x00009F44
			public bool MoveNext()
			{
				this.ThrowIfChanged();
				if (this._bucketEnumerator.MoveNext())
				{
					return true;
				}
				if (this._mapEnumerator.MoveNext())
				{
					KeyValuePair<int, ImmutableHashSet<T>.HashBucket> keyValuePair = this._mapEnumerator.Current;
					this._bucketEnumerator = new ImmutableHashSet<T>.HashBucket.Enumerator(keyValuePair.Value);
					return this._bucketEnumerator.MoveNext();
				}
				return false;
			}

			// Token: 0x0600047F RID: 1151 RVA: 0x0000BD9E File Offset: 0x00009F9E
			public void Reset()
			{
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._mapEnumerator.Reset();
				this._bucketEnumerator.Dispose();
				this._bucketEnumerator = default(ImmutableHashSet<T>.HashBucket.Enumerator);
			}

			// Token: 0x06000480 RID: 1152 RVA: 0x0000BDDE File Offset: 0x00009FDE
			public void Dispose()
			{
				this._mapEnumerator.Dispose();
				this._bucketEnumerator.Dispose();
			}

			// Token: 0x06000481 RID: 1153 RVA: 0x0000BDF6 File Offset: 0x00009FF6
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x040000A2 RID: 162
			private readonly ImmutableHashSet<T>.Builder _builder;

			// Token: 0x040000A3 RID: 163
			private SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.Enumerator _mapEnumerator;

			// Token: 0x040000A4 RID: 164
			private ImmutableHashSet<T>.HashBucket.Enumerator _bucketEnumerator;

			// Token: 0x040000A5 RID: 165
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x02000059 RID: 89
		internal enum OperationResult
		{
			// Token: 0x040000A7 RID: 167
			SizeChanged,
			// Token: 0x040000A8 RID: 168
			NoChangeRequired
		}

		// Token: 0x0200005A RID: 90
		internal struct HashBucket
		{
			// Token: 0x06000482 RID: 1154 RVA: 0x0000BE1E File Offset: 0x0000A01E
			private HashBucket(T firstElement, ImmutableList<T>.Node additionalElements = null)
			{
				this._firstValue = firstElement;
				this._additionalElements = (additionalElements ?? ImmutableList<T>.Node.EmptyNode);
			}

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000BE37 File Offset: 0x0000A037
			internal bool IsEmpty
			{
				get
				{
					return this._additionalElements == null;
				}
			}

			// Token: 0x06000484 RID: 1156 RVA: 0x0000BE42 File Offset: 0x0000A042
			public ImmutableHashSet<T>.HashBucket.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.HashBucket.Enumerator(this);
			}

			// Token: 0x06000485 RID: 1157 RVA: 0x0000BE50 File Offset: 0x0000A050
			internal ImmutableHashSet<T>.HashBucket Add(T value, IEqualityComparer<T> valueComparer, out ImmutableHashSet<T>.OperationResult result)
			{
				if (this.IsEmpty)
				{
					result = ImmutableHashSet<T>.OperationResult.SizeChanged;
					return new ImmutableHashSet<T>.HashBucket(value, null);
				}
				if (valueComparer.Equals(value, this._firstValue) || this._additionalElements.IndexOf(value, valueComparer) >= 0)
				{
					result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
					return this;
				}
				result = ImmutableHashSet<T>.OperationResult.SizeChanged;
				return new ImmutableHashSet<T>.HashBucket(this._firstValue, this._additionalElements.Add(value));
			}

			// Token: 0x06000486 RID: 1158 RVA: 0x0000BEB3 File Offset: 0x0000A0B3
			internal bool Contains(T value, IEqualityComparer<T> valueComparer)
			{
				return !this.IsEmpty && (valueComparer.Equals(value, this._firstValue) || this._additionalElements.IndexOf(value, valueComparer) >= 0);
			}

			// Token: 0x06000487 RID: 1159 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
			internal bool TryExchange(T value, IEqualityComparer<T> valueComparer, out T existingValue)
			{
				if (!this.IsEmpty)
				{
					if (valueComparer.Equals(value, this._firstValue))
					{
						existingValue = this._firstValue;
						return true;
					}
					int num = this._additionalElements.IndexOf(value, valueComparer);
					if (num >= 0)
					{
						existingValue = this._additionalElements[num];
						return true;
					}
				}
				existingValue = value;
				return false;
			}

			// Token: 0x06000488 RID: 1160 RVA: 0x0000BF44 File Offset: 0x0000A144
			internal ImmutableHashSet<T>.HashBucket Remove(T value, IEqualityComparer<T> equalityComparer, out ImmutableHashSet<T>.OperationResult result)
			{
				if (this.IsEmpty)
				{
					result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
					return this;
				}
				if (equalityComparer.Equals(this._firstValue, value))
				{
					if (this._additionalElements.IsEmpty)
					{
						result = ImmutableHashSet<T>.OperationResult.SizeChanged;
						return default(ImmutableHashSet<T>.HashBucket);
					}
					int count = this._additionalElements.Left.Count;
					result = ImmutableHashSet<T>.OperationResult.SizeChanged;
					return new ImmutableHashSet<T>.HashBucket(this._additionalElements.Key, this._additionalElements.RemoveAt(count));
				}
				else
				{
					int num = this._additionalElements.IndexOf(value, equalityComparer);
					if (num < 0)
					{
						result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
						return this;
					}
					result = ImmutableHashSet<T>.OperationResult.SizeChanged;
					return new ImmutableHashSet<T>.HashBucket(this._firstValue, this._additionalElements.RemoveAt(num));
				}
			}

			// Token: 0x06000489 RID: 1161 RVA: 0x0000BFF3 File Offset: 0x0000A1F3
			internal void Freeze()
			{
				if (this._additionalElements != null)
				{
					this._additionalElements.Freeze();
				}
			}

			// Token: 0x040000A9 RID: 169
			private readonly T _firstValue;

			// Token: 0x040000AA RID: 170
			private readonly ImmutableList<T>.Node _additionalElements;

			// Token: 0x02000073 RID: 115
			internal struct Enumerator : IEnumerator<!0>, IEnumerator, IDisposable
			{
				// Token: 0x06000625 RID: 1573 RVA: 0x00010DA2 File Offset: 0x0000EFA2
				internal Enumerator(ImmutableHashSet<T>.HashBucket bucket)
				{
					this._disposed = false;
					this._bucket = bucket;
					this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst;
					this._additionalEnumerator = default(ImmutableList<T>.Enumerator);
				}

				// Token: 0x1700014A RID: 330
				// (get) Token: 0x06000626 RID: 1574 RVA: 0x00010DC5 File Offset: 0x0000EFC5
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x1700014B RID: 331
				// (get) Token: 0x06000627 RID: 1575 RVA: 0x00010DD4 File Offset: 0x0000EFD4
				public T Current
				{
					get
					{
						this.ThrowIfDisposed();
						ImmutableHashSet<T>.HashBucket.Enumerator.Position currentPosition = this._currentPosition;
						if (currentPosition == ImmutableHashSet<T>.HashBucket.Enumerator.Position.First)
						{
							return this._bucket._firstValue;
						}
						if (currentPosition != ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional)
						{
							throw new InvalidOperationException();
						}
						return this._additionalEnumerator.Current;
					}
				}

				// Token: 0x06000628 RID: 1576 RVA: 0x00010E18 File Offset: 0x0000F018
				public bool MoveNext()
				{
					this.ThrowIfDisposed();
					if (this._bucket.IsEmpty)
					{
						this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.End;
						return false;
					}
					switch (this._currentPosition)
					{
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst:
						this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.First;
						return true;
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.First:
						if (this._bucket._additionalElements.IsEmpty)
						{
							this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.End;
							return false;
						}
						this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional;
						this._additionalEnumerator = new ImmutableList<T>.Enumerator(this._bucket._additionalElements, null, -1, -1, false);
						return this._additionalEnumerator.MoveNext();
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional:
						return this._additionalEnumerator.MoveNext();
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.End:
						return false;
					default:
						throw new InvalidOperationException();
					}
				}

				// Token: 0x06000629 RID: 1577 RVA: 0x00010EC7 File Offset: 0x0000F0C7
				public void Reset()
				{
					this.ThrowIfDisposed();
					this._additionalEnumerator.Dispose();
					this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst;
				}

				// Token: 0x0600062A RID: 1578 RVA: 0x00010EE1 File Offset: 0x0000F0E1
				public void Dispose()
				{
					this._disposed = true;
					this._additionalEnumerator.Dispose();
				}

				// Token: 0x0600062B RID: 1579 RVA: 0x00010EF5 File Offset: 0x0000F0F5
				private void ThrowIfDisposed()
				{
					if (this._disposed)
					{
						Requires.FailObjectDisposed<ImmutableHashSet<T>.HashBucket.Enumerator>(this);
					}
				}

				// Token: 0x04000115 RID: 277
				private readonly ImmutableHashSet<T>.HashBucket _bucket;

				// Token: 0x04000116 RID: 278
				private bool _disposed;

				// Token: 0x04000117 RID: 279
				private ImmutableHashSet<T>.HashBucket.Enumerator.Position _currentPosition;

				// Token: 0x04000118 RID: 280
				private ImmutableList<T>.Enumerator _additionalEnumerator;

				// Token: 0x02000076 RID: 118
				private enum Position
				{
					// Token: 0x04000122 RID: 290
					BeforeFirst,
					// Token: 0x04000123 RID: 291
					First,
					// Token: 0x04000124 RID: 292
					Additional,
					// Token: 0x04000125 RID: 293
					End
				}
			}
		}

		// Token: 0x0200005B RID: 91
		private struct MutationInput
		{
			// Token: 0x0600048A RID: 1162 RVA: 0x0000C008 File Offset: 0x0000A208
			internal MutationInput(ImmutableHashSet<T> set)
			{
				Requires.NotNull<ImmutableHashSet<T>>(set, "set");
				this._root = set._root;
				this._equalityComparer = set._equalityComparer;
				this._count = set._count;
			}

			// Token: 0x0600048B RID: 1163 RVA: 0x0000C039 File Offset: 0x0000A239
			internal MutationInput(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, IEqualityComparer<T> equalityComparer, int count)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
				Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
				Requires.Range(count >= 0, "count", null);
				this._root = root;
				this._equalityComparer = equalityComparer;
				this._count = count;
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000C078 File Offset: 0x0000A278
			internal SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000C080 File Offset: 0x0000A280
			internal IEqualityComparer<T> EqualityComparer
			{
				get
				{
					return this._equalityComparer;
				}
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000C088 File Offset: 0x0000A288
			internal int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x040000AB RID: 171
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

			// Token: 0x040000AC RID: 172
			private readonly IEqualityComparer<T> _equalityComparer;

			// Token: 0x040000AD RID: 173
			private readonly int _count;
		}

		// Token: 0x0200005C RID: 92
		private enum CountType
		{
			// Token: 0x040000AF RID: 175
			Adjustment,
			// Token: 0x040000B0 RID: 176
			FinalValue
		}

		// Token: 0x0200005D RID: 93
		private struct MutationResult
		{
			// Token: 0x0600048F RID: 1167 RVA: 0x0000C090 File Offset: 0x0000A290
			internal MutationResult(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, int count, ImmutableHashSet<T>.CountType countType = ImmutableHashSet<T>.CountType.Adjustment)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
				this._root = root;
				this._count = count;
				this._countType = countType;
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000C0B2 File Offset: 0x0000A2B2
			internal SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000C0BA File Offset: 0x0000A2BA
			internal int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000C0C2 File Offset: 0x0000A2C2
			internal ImmutableHashSet<T>.CountType CountType
			{
				get
				{
					return this._countType;
				}
			}

			// Token: 0x06000493 RID: 1171 RVA: 0x0000C0CC File Offset: 0x0000A2CC
			internal ImmutableHashSet<T> Finalize(ImmutableHashSet<T> priorSet)
			{
				Requires.NotNull<ImmutableHashSet<T>>(priorSet, "priorSet");
				int num = this.Count;
				if (this.CountType == ImmutableHashSet<T>.CountType.Adjustment)
				{
					num += priorSet._count;
				}
				return priorSet.Wrap(this.Root, num);
			}

			// Token: 0x040000B1 RID: 177
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

			// Token: 0x040000B2 RID: 178
			private readonly int _count;

			// Token: 0x040000B3 RID: 179
			private readonly ImmutableHashSet<T>.CountType _countType;
		}

		// Token: 0x0200005E RID: 94
		private struct NodeEnumerable : IEnumerable<!0>, IEnumerable
		{
			// Token: 0x06000494 RID: 1172 RVA: 0x0000C109 File Offset: 0x0000A309
			internal NodeEnumerable(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
				this._root = root;
			}

			// Token: 0x06000495 RID: 1173 RVA: 0x0000C11D File Offset: 0x0000A31D
			public ImmutableHashSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.Enumerator(this._root, null);
			}

			// Token: 0x06000496 RID: 1174 RVA: 0x0000C12B File Offset: 0x0000A32B
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000497 RID: 1175 RVA: 0x0000C12B File Offset: 0x0000A32B
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040000B4 RID: 180
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;
		}
	}
}
