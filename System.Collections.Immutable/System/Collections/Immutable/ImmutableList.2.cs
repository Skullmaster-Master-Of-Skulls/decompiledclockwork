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
	// Token: 0x02000026 RID: 38
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableListDebuggerProxy<>))]
	public sealed class ImmutableList<T> : IImmutableList<!0>, IReadOnlyList<!0>, IReadOnlyCollection<!0>, IEnumerable<!0>, IEnumerable, IList<!0>, ICollection<!0>, IList, ICollection, IOrderedCollection<T>, IImmutableListQueries<T>, IStrongEnumerable<T, ImmutableList<T>.Enumerator>
	{
		// Token: 0x06000220 RID: 544 RVA: 0x00006E00 File Offset: 0x00005000
		internal ImmutableList()
		{
			this._root = ImmutableList<T>.Node.EmptyNode;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00006E13 File Offset: 0x00005013
		private ImmutableList(ImmutableList<T>.Node root)
		{
			Requires.NotNull<ImmutableList<T>.Node>(root, "root");
			root.Freeze();
			this._root = root;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00006E33 File Offset: 0x00005033
		public ImmutableList<T> Clear()
		{
			return ImmutableList<T>.Empty;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006E3A File Offset: 0x0000503A
		public int BinarySearch(T item)
		{
			return this.BinarySearch(item, null);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006E44 File Offset: 0x00005044
		public int BinarySearch(T item, IComparer<T> comparer)
		{
			return this.BinarySearch(0, this.Count, item, comparer);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006E55 File Offset: 0x00005055
		public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
		{
			return this._root.BinarySearch(index, count, item, comparer);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00006E67 File Offset: 0x00005067
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006E74 File Offset: 0x00005074
		IImmutableList<T> IImmutableList<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00006E7C File Offset: 0x0000507C
		public int Count
		{
			get
			{
				return this._root.Count;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000229 RID: 553 RVA: 0x000052C4 File Offset: 0x000034C4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000038D6 File Offset: 0x00001AD6
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005E RID: 94
		public T this[int index]
		{
			get
			{
				return this._root[index];
			}
		}

		// Token: 0x1700005F RID: 95
		T IOrderedCollection<!0>.this[int index]
		{
			get
			{
				return this[index];
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006EA6 File Offset: 0x000050A6
		public ImmutableList<T>.Builder ToBuilder()
		{
			return new ImmutableList<T>.Builder(this);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00006EB0 File Offset: 0x000050B0
		public ImmutableList<T> Add(T value)
		{
			ImmutableList<T>.Node root = this._root.Add(value);
			return this.Wrap(root);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006ED4 File Offset: 0x000050D4
		public ImmutableList<T> AddRange(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (this.IsEmpty)
			{
				return this.FillFromEmpty(items);
			}
			ImmutableList<T>.Node root = this._root.AddRange(items);
			return this.Wrap(root);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00006F10 File Offset: 0x00005110
		public ImmutableList<T> Insert(int index, T item)
		{
			Requires.Range(index >= 0 && index <= this.Count, "index", null);
			return this.Wrap(this._root.Insert(index, item));
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006F44 File Offset: 0x00005144
		public ImmutableList<T> InsertRange(int index, IEnumerable<T> items)
		{
			Requires.Range(index >= 0 && index <= this.Count, "index", null);
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableList<T>.Node root = this._root.InsertRange(index, items);
			return this.Wrap(root);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006F8F File Offset: 0x0000518F
		public ImmutableList<T> Remove(T value)
		{
			return this.Remove(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006FA0 File Offset: 0x000051A0
		public ImmutableList<T> Remove(T value, IEqualityComparer<T> equalityComparer)
		{
			int num = this.IndexOf(value, equalityComparer);
			if (num >= 0)
			{
				return this.RemoveAt(num);
			}
			return this;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00006FC4 File Offset: 0x000051C4
		public ImmutableList<T> RemoveRange(int index, int count)
		{
			Requires.Range(index >= 0 && (index < this.Count || (index == this.Count && count == 0)), "index", null);
			Requires.Range(count >= 0 && index + count <= this.Count, "count", null);
			ImmutableList<T>.Node node = this._root;
			int num = count;
			while (num-- > 0)
			{
				node = node.RemoveAt(index);
			}
			return this.Wrap(node);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007041 File Offset: 0x00005241
		public ImmutableList<T> RemoveRange(IEnumerable<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007050 File Offset: 0x00005250
		public ImmutableList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			if (this.IsEmpty)
			{
				return this;
			}
			ImmutableList<T>.Node node = this._root;
			foreach (T item in items.GetEnumerableDisposable<T, ImmutableList<T>.Enumerator>())
			{
				int num = node.IndexOf(item, equalityComparer);
				if (num >= 0)
				{
					node = node.RemoveAt(num);
				}
			}
			return this.Wrap(node);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000070E8 File Offset: 0x000052E8
		public ImmutableList<T> RemoveAt(int index)
		{
			Requires.Range(index >= 0 && index < this.Count, "index", null);
			ImmutableList<T>.Node root = this._root.RemoveAt(index);
			return this.Wrap(root);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007124 File Offset: 0x00005324
		public ImmutableList<T> RemoveAll(Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			return this.Wrap(this._root.RemoveAll(match));
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007143 File Offset: 0x00005343
		public ImmutableList<T> SetItem(int index, T value)
		{
			return this.Wrap(this._root.ReplaceAt(index, value));
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007158 File Offset: 0x00005358
		public ImmutableList<T> Replace(T oldValue, T newValue)
		{
			return this.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00007168 File Offset: 0x00005368
		public ImmutableList<T> Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			int num = this.IndexOf(oldValue, equalityComparer);
			if (num < 0)
			{
				throw new ArgumentException(SR.CannotFindOldValue, "oldValue");
			}
			return this.SetItem(num, newValue);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000071A5 File Offset: 0x000053A5
		public ImmutableList<T> Reverse()
		{
			return this.Wrap(this._root.Reverse());
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000071B8 File Offset: 0x000053B8
		public ImmutableList<T> Reverse(int index, int count)
		{
			return this.Wrap(this._root.Reverse(index, count));
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000071CD File Offset: 0x000053CD
		public ImmutableList<T> Sort()
		{
			return this.Wrap(this._root.Sort());
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000071E0 File Offset: 0x000053E0
		public ImmutableList<T> Sort(Comparison<T> comparison)
		{
			Requires.NotNull<Comparison<T>>(comparison, "comparison");
			return this.Wrap(this._root.Sort(comparison));
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000071FF File Offset: 0x000053FF
		public ImmutableList<T> Sort(IComparer<T> comparer)
		{
			Requires.NotNull<IComparer<T>>(comparer, "comparer");
			return this.Wrap(this._root.Sort(comparer));
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00007220 File Offset: 0x00005420
		public ImmutableList<T> Sort(int index, int count, IComparer<T> comparer)
		{
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0, "count", null);
			Requires.Range(index + count <= this.Count, "count", null);
			Requires.NotNull<IComparer<T>>(comparer, "comparer");
			return this.Wrap(this._root.Sort(index, count, comparer));
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000728C File Offset: 0x0000548C
		public void ForEach(Action<T> action)
		{
			Requires.NotNull<Action<T>>(action, "action");
			foreach (T obj in this)
			{
				action(obj);
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000072E8 File Offset: 0x000054E8
		public void CopyTo(T[] array)
		{
			Requires.NotNull<T[]>(array, "array");
			Requires.Range(array.Length >= this.Count, "array", null);
			this._root.CopyTo(array);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000731C File Offset: 0x0000551C
		public void CopyTo(T[] array, int arrayIndex)
		{
			Requires.NotNull<T[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000736E File Offset: 0x0000556E
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			this._root.CopyTo(index, array, arrayIndex, count);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007380 File Offset: 0x00005580
		public ImmutableList<T> GetRange(int index, int count)
		{
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0, "count", null);
			Requires.Range(index + count <= this.Count, "count", null);
			return this.Wrap(ImmutableList<T>.Node.NodeTreeFromList(this, index, count));
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000073D8 File Offset: 0x000055D8
		public ImmutableList<TOutput> ConvertAll<TOutput>(Func<T, TOutput> converter)
		{
			Requires.NotNull<Func<T, TOutput>>(converter, "converter");
			return ImmutableList<TOutput>.WrapNode(this._root.ConvertAll<TOutput>(converter));
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000073F6 File Offset: 0x000055F6
		public bool Exists(Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			return this._root.Exists(match);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000740F File Offset: 0x0000560F
		public T Find(Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			return this._root.Find(match);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007428 File Offset: 0x00005628
		public ImmutableList<T> FindAll(Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			return this._root.FindAll(match);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007441 File Offset: 0x00005641
		public int FindIndex(Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			return this._root.FindIndex(match);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000745C File Offset: 0x0000565C
		public int FindIndex(int startIndex, Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			Requires.Range(startIndex >= 0, "startIndex", null);
			Requires.Range(startIndex <= this.Count, "startIndex", null);
			return this._root.FindIndex(startIndex, match);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000074AC File Offset: 0x000056AC
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			Requires.Range(startIndex >= 0, "startIndex", null);
			Requires.Range(count >= 0, "count", null);
			Requires.Range(startIndex + count <= this.Count, "count", null);
			return this._root.FindIndex(startIndex, count, match);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000750F File Offset: 0x0000570F
		public T FindLast(Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			return this._root.FindLast(match);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007528 File Offset: 0x00005728
		public int FindLastIndex(Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			return this._root.FindLastIndex(match);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007544 File Offset: 0x00005744
		public int FindLastIndex(int startIndex, Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			Requires.Range(startIndex >= 0, "startIndex", null);
			Requires.Range(startIndex == 0 || startIndex < this.Count, "startIndex", null);
			return this._root.FindLastIndex(startIndex, match);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00007598 File Offset: 0x00005798
		public int FindLastIndex(int startIndex, int count, Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			Requires.Range(startIndex >= 0, "startIndex", null);
			Requires.Range(count <= this.Count, "count", null);
			Requires.Range(startIndex - count + 1 >= 0, "startIndex", null);
			return this._root.FindLastIndex(startIndex, count, match);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000075FD File Offset: 0x000057FD
		public int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
		{
			return this._root.IndexOf(item, index, count, equalityComparer);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000760F File Offset: 0x0000580F
		public int LastIndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
		{
			return this._root.LastIndexOf(item, index, count, equalityComparer);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007621 File Offset: 0x00005821
		public bool TrueForAll(Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			return this._root.TrueForAll(match);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000763A File Offset: 0x0000583A
		public bool Contains(T value)
		{
			return this.IndexOf(value) >= 0;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00007649 File Offset: 0x00005849
		public int IndexOf(T value)
		{
			return this.IndexOf(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00007657 File Offset: 0x00005857
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Add(T value)
		{
			return this.Add(value);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00007660 File Offset: 0x00005860
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.AddRange(IEnumerable<T> items)
		{
			return this.AddRange(items);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00007669 File Offset: 0x00005869
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Insert(int index, T item)
		{
			return this.Insert(index, item);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00007673 File Offset: 0x00005873
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.InsertRange(int index, IEnumerable<T> items)
		{
			return this.InsertRange(index, items);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000767D File Offset: 0x0000587D
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Remove(T value, IEqualityComparer<T> equalityComparer)
		{
			return this.Remove(value, equalityComparer);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00007687 File Offset: 0x00005887
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveAll(Predicate<T> match)
		{
			return this.RemoveAll(match);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00007690 File Offset: 0x00005890
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			return this.RemoveRange(items, equalityComparer);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000769A File Offset: 0x0000589A
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveRange(int index, int count)
		{
			return this.RemoveRange(index, count);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000076A4 File Offset: 0x000058A4
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveAt(int index)
		{
			return this.RemoveAt(index);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000076AD File Offset: 0x000058AD
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.SetItem(int index, T value)
		{
			return this.SetItem(index, value);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000076B7 File Offset: 0x000058B7
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			return this.Replace(oldValue, newValue, equalityComparer);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000076C2 File Offset: 0x000058C2
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000076C2 File Offset: 0x000058C2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00002D65 File Offset: 0x00000F65
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00002D65 File Offset: 0x00000F65
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000060 RID: 96
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

		// Token: 0x06000268 RID: 616 RVA: 0x00002D65 File Offset: 0x00000F65
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00002D65 File Offset: 0x00000F65
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000038D6 File Offset: 0x00001AD6
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00002D65 File Offset: 0x00000F65
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00007712 File Offset: 0x00005912
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00002D65 File Offset: 0x00000F65
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00002D65 File Offset: 0x00000F65
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00002D65 File Offset: 0x00000F65
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00007736 File Offset: 0x00005936
		bool IList.Contains(object value)
		{
			return this.Contains((T)((object)value));
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00007744 File Offset: 0x00005944
		int IList.IndexOf(object value)
		{
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00002D65 File Offset: 0x00000F65
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000273 RID: 627 RVA: 0x000038D6 File Offset: 0x00001AD6
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000038D6 File Offset: 0x00001AD6
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00002D65 File Offset: 0x00000F65
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000064 RID: 100
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

		// Token: 0x06000278 RID: 632 RVA: 0x0000777B File Offset: 0x0000597B
		public ImmutableList<T>.Enumerator GetEnumerator()
		{
			return new ImmutableList<T>.Enumerator(this._root, null, -1, -1, false);
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000778C File Offset: 0x0000598C
		internal ImmutableList<T>.Node Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00007794 File Offset: 0x00005994
		private static ImmutableList<T> WrapNode(ImmutableList<T>.Node root)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableList<T>(root);
			}
			return ImmutableList<T>.Empty;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000077AC File Offset: 0x000059AC
		private static bool TryCastToImmutableList(IEnumerable<T> sequence, out ImmutableList<T> other)
		{
			other = (sequence as ImmutableList<T>);
			if (other != null)
			{
				return true;
			}
			ImmutableList<T>.Builder builder = sequence as ImmutableList<T>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000077DC File Offset: 0x000059DC
		private ImmutableList<T> Wrap(ImmutableList<T>.Node root)
		{
			if (root == this._root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableList<T>(root);
			}
			return this.Clear();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00007800 File Offset: 0x00005A00
		private ImmutableList<T> FillFromEmpty(IEnumerable<T> items)
		{
			ImmutableList<T> result;
			if (ImmutableList<T>.TryCastToImmutableList(items, out result))
			{
				return result;
			}
			IOrderedCollection<T> orderedCollection = items.AsOrderedCollection<T>();
			if (orderedCollection.Count == 0)
			{
				return this;
			}
			return new ImmutableList<T>(ImmutableList<T>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count));
		}

		// Token: 0x04000023 RID: 35
		public static readonly ImmutableList<T> Empty = new ImmutableList<T>();

		// Token: 0x04000024 RID: 36
		private readonly ImmutableList<T>.Node _root;

		// Token: 0x02000060 RID: 96
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableListBuilderDebuggerProxy<>))]
		public sealed class Builder : IList<!0>, ICollection<!0>, IEnumerable<!0>, IEnumerable, IList, ICollection, IOrderedCollection<!0>, IImmutableListQueries<T>, IReadOnlyList<!0>, IReadOnlyCollection<!0>
		{
			// Token: 0x0600049B RID: 1179 RVA: 0x0000C178 File Offset: 0x0000A378
			internal Builder(ImmutableList<T> list)
			{
				Requires.NotNull<ImmutableList<T>>(list, "list");
				this._root = list._root;
				this._immutable = list;
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000C1A9 File Offset: 0x0000A3A9
			public int Count
			{
				get
				{
					return this.Root.Count;
				}
			}

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x0600049D RID: 1181 RVA: 0x000020FC File Offset: 0x000002FC
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000C1B9 File Offset: 0x0000A3B9
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000C1C1 File Offset: 0x0000A3C1
			// (set) Token: 0x060004A0 RID: 1184 RVA: 0x0000C1C9 File Offset: 0x0000A3C9
			internal ImmutableList<T>.Node Root
			{
				get
				{
					return this._root;
				}
				private set
				{
					this._version++;
					if (this._root != value)
					{
						this._root = value;
						this._immutable = null;
					}
				}
			}

			// Token: 0x170000EA RID: 234
			public T this[int index]
			{
				get
				{
					return this.Root[index];
				}
				set
				{
					this.Root = this.Root.ReplaceAt(index, value);
				}
			}

			// Token: 0x170000EB RID: 235
			T IOrderedCollection<!0>.this[int index]
			{
				get
				{
					return this[index];
				}
			}

			// Token: 0x060004A4 RID: 1188 RVA: 0x0000C21C File Offset: 0x0000A41C
			public int IndexOf(T item)
			{
				return this.Root.IndexOf(item, EqualityComparer<T>.Default);
			}

			// Token: 0x060004A5 RID: 1189 RVA: 0x0000C22F File Offset: 0x0000A42F
			public void Insert(int index, T item)
			{
				this.Root = this.Root.Insert(index, item);
			}

			// Token: 0x060004A6 RID: 1190 RVA: 0x0000C244 File Offset: 0x0000A444
			public void RemoveAt(int index)
			{
				this.Root = this.Root.RemoveAt(index);
			}

			// Token: 0x060004A7 RID: 1191 RVA: 0x0000C258 File Offset: 0x0000A458
			public void Add(T item)
			{
				this.Root = this.Root.Add(item);
			}

			// Token: 0x060004A8 RID: 1192 RVA: 0x0000C26C File Offset: 0x0000A46C
			public void Clear()
			{
				this.Root = ImmutableList<T>.Node.EmptyNode;
			}

			// Token: 0x060004A9 RID: 1193 RVA: 0x0000C279 File Offset: 0x0000A479
			public bool Contains(T item)
			{
				return this.IndexOf(item) >= 0;
			}

			// Token: 0x060004AA RID: 1194 RVA: 0x0000C288 File Offset: 0x0000A488
			public bool Remove(T item)
			{
				int num = this.IndexOf(item);
				if (num < 0)
				{
					return false;
				}
				this.Root = this.Root.RemoveAt(num);
				return true;
			}

			// Token: 0x060004AB RID: 1195 RVA: 0x0000C2B6 File Offset: 0x0000A4B6
			public ImmutableList<T>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x060004AC RID: 1196 RVA: 0x0000C2C4 File Offset: 0x0000A4C4
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060004AD RID: 1197 RVA: 0x0000C2C4 File Offset: 0x0000A4C4
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060004AE RID: 1198 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
			public void ForEach(Action<T> action)
			{
				Requires.NotNull<Action<T>>(action, "action");
				foreach (T obj in this)
				{
					action(obj);
				}
			}

			// Token: 0x060004AF RID: 1199 RVA: 0x0000C33C File Offset: 0x0000A53C
			public void CopyTo(T[] array)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(array.Length >= this.Count, "array", null);
				this._root.CopyTo(array);
			}

			// Token: 0x060004B0 RID: 1200 RVA: 0x0000C36E File Offset: 0x0000A56E
			public void CopyTo(T[] array, int arrayIndex)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				this._root.CopyTo(array, arrayIndex);
			}

			// Token: 0x060004B1 RID: 1201 RVA: 0x0000C3A3 File Offset: 0x0000A5A3
			public void CopyTo(int index, T[] array, int arrayIndex, int count)
			{
				this._root.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x060004B2 RID: 1202 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
			public ImmutableList<T> GetRange(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				return ImmutableList<T>.WrapNode(ImmutableList<T>.Node.NodeTreeFromList(this, index, count));
			}

			// Token: 0x060004B3 RID: 1203 RVA: 0x0000C40F File Offset: 0x0000A60F
			public ImmutableList<TOutput> ConvertAll<TOutput>(Func<T, TOutput> converter)
			{
				Requires.NotNull<Func<T, TOutput>>(converter, "converter");
				return ImmutableList<TOutput>.WrapNode(this._root.ConvertAll<TOutput>(converter));
			}

			// Token: 0x060004B4 RID: 1204 RVA: 0x0000C42D File Offset: 0x0000A62D
			public bool Exists(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				return this._root.Exists(match);
			}

			// Token: 0x060004B5 RID: 1205 RVA: 0x0000C446 File Offset: 0x0000A646
			public T Find(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				return this._root.Find(match);
			}

			// Token: 0x060004B6 RID: 1206 RVA: 0x0000C45F File Offset: 0x0000A65F
			public ImmutableList<T> FindAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				return this._root.FindAll(match);
			}

			// Token: 0x060004B7 RID: 1207 RVA: 0x0000C478 File Offset: 0x0000A678
			public int FindIndex(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				return this._root.FindIndex(match);
			}

			// Token: 0x060004B8 RID: 1208 RVA: 0x0000C494 File Offset: 0x0000A694
			public int FindIndex(int startIndex, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(startIndex <= this.Count, "startIndex", null);
				return this._root.FindIndex(startIndex, match);
			}

			// Token: 0x060004B9 RID: 1209 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
			public int FindIndex(int startIndex, int count, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(startIndex + count <= this.Count, "count", null);
				return this._root.FindIndex(startIndex, count, match);
			}

			// Token: 0x060004BA RID: 1210 RVA: 0x0000C547 File Offset: 0x0000A747
			public T FindLast(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				return this._root.FindLast(match);
			}

			// Token: 0x060004BB RID: 1211 RVA: 0x0000C560 File Offset: 0x0000A760
			public int FindLastIndex(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				return this._root.FindLastIndex(match);
			}

			// Token: 0x060004BC RID: 1212 RVA: 0x0000C57C File Offset: 0x0000A77C
			public int FindLastIndex(int startIndex, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(startIndex == 0 || startIndex < this.Count, "startIndex", null);
				return this._root.FindLastIndex(startIndex, match);
			}

			// Token: 0x060004BD RID: 1213 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
			public int FindLastIndex(int startIndex, int count, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(count <= this.Count, "count", null);
				Requires.Range(startIndex - count + 1 >= 0, "startIndex", null);
				return this._root.FindLastIndex(startIndex, count, match);
			}

			// Token: 0x060004BE RID: 1214 RVA: 0x0000C635 File Offset: 0x0000A835
			public int IndexOf(T item, int index)
			{
				return this._root.IndexOf(item, index, this.Count - index, EqualityComparer<T>.Default);
			}

			// Token: 0x060004BF RID: 1215 RVA: 0x0000C651 File Offset: 0x0000A851
			public int IndexOf(T item, int index, int count)
			{
				return this._root.IndexOf(item, index, count, EqualityComparer<T>.Default);
			}

			// Token: 0x060004C0 RID: 1216 RVA: 0x0000C666 File Offset: 0x0000A866
			public int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
			{
				Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
				return this._root.IndexOf(item, index, count, equalityComparer);
			}

			// Token: 0x060004C1 RID: 1217 RVA: 0x0000C684 File Offset: 0x0000A884
			public int LastIndexOf(T item)
			{
				if (this.Count == 0)
				{
					return -1;
				}
				return this._root.LastIndexOf(item, this.Count - 1, this.Count, EqualityComparer<T>.Default);
			}

			// Token: 0x060004C2 RID: 1218 RVA: 0x0000C6AF File Offset: 0x0000A8AF
			public int LastIndexOf(T item, int startIndex)
			{
				if (this.Count == 0 && startIndex == 0)
				{
					return -1;
				}
				return this._root.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
			}

			// Token: 0x060004C3 RID: 1219 RVA: 0x0000C6D3 File Offset: 0x0000A8D3
			public int LastIndexOf(T item, int startIndex, int count)
			{
				return this._root.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x060004C4 RID: 1220 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
			public int LastIndexOf(T item, int startIndex, int count, IEqualityComparer<T> equalityComparer)
			{
				return this._root.LastIndexOf(item, startIndex, count, equalityComparer);
			}

			// Token: 0x060004C5 RID: 1221 RVA: 0x0000C6FA File Offset: 0x0000A8FA
			public bool TrueForAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				return this._root.TrueForAll(match);
			}

			// Token: 0x060004C6 RID: 1222 RVA: 0x0000C713 File Offset: 0x0000A913
			public void AddRange(IEnumerable<T> items)
			{
				Requires.NotNull<IEnumerable<T>>(items, "items");
				this.Root = this.Root.AddRange(items);
			}

			// Token: 0x060004C7 RID: 1223 RVA: 0x0000C732 File Offset: 0x0000A932
			public void InsertRange(int index, IEnumerable<T> items)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				Requires.NotNull<IEnumerable<T>>(items, "items");
				this.Root = this.Root.InsertRange(index, items);
			}

			// Token: 0x060004C8 RID: 1224 RVA: 0x0000C770 File Offset: 0x0000A970
			public int RemoveAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				int count = this.Count;
				this.Root = this.Root.RemoveAll(match);
				return count - this.Count;
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x0000C79C File Offset: 0x0000A99C
			public void Reverse()
			{
				this.Reverse(0, this.Count);
			}

			// Token: 0x060004CA RID: 1226 RVA: 0x0000C7AC File Offset: 0x0000A9AC
			public void Reverse(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				this.Root = this.Root.Reverse(index, count);
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x0000C809 File Offset: 0x0000AA09
			public void Sort()
			{
				this.Root = this.Root.Sort();
			}

			// Token: 0x060004CC RID: 1228 RVA: 0x0000C81C File Offset: 0x0000AA1C
			public void Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				this.Root = this.Root.Sort(comparison);
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x0000C83B File Offset: 0x0000AA3B
			public void Sort(IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				this.Root = this.Root.Sort(comparer);
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x0000C85C File Offset: 0x0000AA5C
			public void Sort(int index, int count, IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				this.Root = this.Root.Sort(index, count, comparer);
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x0000C8C5 File Offset: 0x0000AAC5
			public int BinarySearch(T item)
			{
				return this.BinarySearch(item, null);
			}

			// Token: 0x060004D0 RID: 1232 RVA: 0x0000C8CF File Offset: 0x0000AACF
			public int BinarySearch(T item, IComparer<T> comparer)
			{
				return this.BinarySearch(0, this.Count, item, comparer);
			}

			// Token: 0x060004D1 RID: 1233 RVA: 0x0000C8E0 File Offset: 0x0000AAE0
			public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
			{
				return this.Root.BinarySearch(index, count, item, comparer);
			}

			// Token: 0x060004D2 RID: 1234 RVA: 0x0000C8F2 File Offset: 0x0000AAF2
			public ImmutableList<T> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableList<T>.WrapNode(this.Root);
				}
				return this._immutable;
			}

			// Token: 0x060004D3 RID: 1235 RVA: 0x0000C913 File Offset: 0x0000AB13
			int IList.Add(object value)
			{
				this.Add((T)((object)value));
				return this.Count - 1;
			}

			// Token: 0x060004D4 RID: 1236 RVA: 0x0000C929 File Offset: 0x0000AB29
			void IList.Clear()
			{
				this.Clear();
			}

			// Token: 0x060004D5 RID: 1237 RVA: 0x0000C931 File Offset: 0x0000AB31
			bool IList.Contains(object value)
			{
				return this.Contains((T)((object)value));
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x0000C93F File Offset: 0x0000AB3F
			int IList.IndexOf(object value)
			{
				return this.IndexOf((T)((object)value));
			}

			// Token: 0x060004D7 RID: 1239 RVA: 0x0000C94D File Offset: 0x0000AB4D
			void IList.Insert(int index, object value)
			{
				this.Insert(index, (T)((object)value));
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000020FC File Offset: 0x000002FC
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x060004D9 RID: 1241 RVA: 0x000020FC File Offset: 0x000002FC
			bool IList.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060004DA RID: 1242 RVA: 0x0000C962 File Offset: 0x0000AB62
			void IList.Remove(object value)
			{
				this.Remove((T)((object)value));
			}

			// Token: 0x170000EE RID: 238
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					this[index] = (T)((object)value);
				}
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x0000C98E File Offset: 0x0000AB8E
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex);
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x060004DE RID: 1246 RVA: 0x000020FC File Offset: 0x000002FC
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
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

			// Token: 0x040000B6 RID: 182
			private ImmutableList<T>.Node _root = ImmutableList<T>.Node.EmptyNode;

			// Token: 0x040000B7 RID: 183
			private ImmutableList<T> _immutable;

			// Token: 0x040000B8 RID: 184
			private int _version;

			// Token: 0x040000B9 RID: 185
			private object _syncRoot;
		}

		// Token: 0x02000061 RID: 97
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<!0>, IEnumerator, IDisposable, ISecurePooledObjectUser, IStrongEnumerator<T>
		{
			// Token: 0x060004E0 RID: 1248 RVA: 0x0000C9C4 File Offset: 0x0000ABC4
			internal Enumerator(ImmutableList<T>.Node root, ImmutableList<T>.Builder builder = null, int startIndex = -1, int count = -1, bool reversed = false)
			{
				Requires.NotNull<ImmutableList<T>.Node>(root, "root");
				Requires.Range(startIndex >= -1, "startIndex", null);
				Requires.Range(count >= -1, "count", null);
				Requires.Argument(reversed || count == -1 || ((startIndex == -1) ? 0 : startIndex) + count <= root.Count);
				Requires.Argument(!reversed || count == -1 || ((startIndex == -1) ? (root.Count - 1) : startIndex) - count + 1 >= 0);
				this._root = root;
				this._builder = builder;
				this._current = null;
				this._startIndex = ((startIndex >= 0) ? startIndex : (reversed ? (root.Count - 1) : 0));
				this._count = ((count == -1) ? root.Count : count);
				this._remainingCount = this._count;
				this._reversed = reversed;
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (this._count > 0)
				{
					if (!ImmutableList<T>.Enumerator.s_EnumeratingStacks.TryTake(this, out this._stack))
					{
						this._stack = ImmutableList<T>.Enumerator.s_EnumeratingStacks.PrepNew(this, new Stack<RefAsValueType<ImmutableList<T>.Node>>(root.Height));
					}
					this.ResetStack();
				}
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000CB19 File Offset: 0x0000AD19
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0000CB21 File Offset: 0x0000AD21
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

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000CB42 File Offset: 0x0000AD42
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060004E4 RID: 1252 RVA: 0x0000CB50 File Offset: 0x0000AD50
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<ImmutableList<T>.Node>> stack;
				if (this._stack != null && this._stack.TryUse<ImmutableList<T>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableList<T>.Node>>();
					ImmutableList<T>.Enumerator.s_EnumeratingStacks.TryAdd(this, this._stack);
				}
				this._stack = null;
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x0000CBA8 File Offset: 0x0000ADA8
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				this.ThrowIfChanged();
				if (this._stack != null)
				{
					Stack<RefAsValueType<ImmutableList<T>.Node>> stack = this._stack.Use<ImmutableList<T>.Enumerator>(ref this);
					if (this._remainingCount > 0 && stack.Count > 0)
					{
						ImmutableList<T>.Node value = stack.Pop().Value;
						this._current = value;
						this.PushNext(this.NextBranch(value));
						this._remainingCount--;
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x060004E6 RID: 1254 RVA: 0x0000CC20 File Offset: 0x0000AE20
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._remainingCount = this._count;
				if (this._stack != null)
				{
					this.ResetStack();
				}
			}

			// Token: 0x060004E7 RID: 1255 RVA: 0x0000CC60 File Offset: 0x0000AE60
			private void ResetStack()
			{
				Stack<RefAsValueType<ImmutableList<T>.Node>> stack = this._stack.Use<ImmutableList<T>.Enumerator>(ref this);
				stack.ClearFastWhenEmpty<RefAsValueType<ImmutableList<T>.Node>>();
				ImmutableList<T>.Node node = this._root;
				int num = this._reversed ? (this._root.Count - this._startIndex - 1) : this._startIndex;
				while (!node.IsEmpty && num != this.PreviousBranch(node).Count)
				{
					if (num < this.PreviousBranch(node).Count)
					{
						stack.Push(new RefAsValueType<ImmutableList<T>.Node>(node));
						node = this.PreviousBranch(node);
					}
					else
					{
						num -= this.PreviousBranch(node).Count + 1;
						node = this.NextBranch(node);
					}
				}
				if (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<ImmutableList<T>.Node>(node));
				}
			}

			// Token: 0x060004E8 RID: 1256 RVA: 0x0000CD17 File Offset: 0x0000AF17
			private ImmutableList<T>.Node NextBranch(ImmutableList<T>.Node node)
			{
				if (!this._reversed)
				{
					return node.Right;
				}
				return node.Left;
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x0000CD2E File Offset: 0x0000AF2E
			private ImmutableList<T>.Node PreviousBranch(ImmutableList<T>.Node node)
			{
				if (!this._reversed)
				{
					return node.Left;
				}
				return node.Right;
			}

			// Token: 0x060004EA RID: 1258 RVA: 0x0000CD45 File Offset: 0x0000AF45
			private void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableList<T>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableList<T>.Enumerator>(this);
				}
			}

			// Token: 0x060004EB RID: 1259 RVA: 0x0000CD70 File Offset: 0x0000AF70
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x060004EC RID: 1260 RVA: 0x0000CD98 File Offset: 0x0000AF98
			private void PushNext(ImmutableList<T>.Node node)
			{
				Requires.NotNull<ImmutableList<T>.Node>(node, "node");
				if (!node.IsEmpty)
				{
					Stack<RefAsValueType<ImmutableList<T>.Node>> stack = this._stack.Use<ImmutableList<T>.Enumerator>(ref this);
					while (!node.IsEmpty)
					{
						stack.Push(new RefAsValueType<ImmutableList<T>.Node>(node));
						node = this.PreviousBranch(node);
					}
				}
			}

			// Token: 0x040000BA RID: 186
			private static readonly SecureObjectPool<Stack<RefAsValueType<ImmutableList<T>.Node>>, ImmutableList<T>.Enumerator> s_EnumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<ImmutableList<T>.Node>>, ImmutableList<T>.Enumerator>();

			// Token: 0x040000BB RID: 187
			private readonly ImmutableList<T>.Builder _builder;

			// Token: 0x040000BC RID: 188
			private readonly int _poolUserId;

			// Token: 0x040000BD RID: 189
			private readonly int _startIndex;

			// Token: 0x040000BE RID: 190
			private readonly int _count;

			// Token: 0x040000BF RID: 191
			private int _remainingCount;

			// Token: 0x040000C0 RID: 192
			private bool _reversed;

			// Token: 0x040000C1 RID: 193
			private ImmutableList<T>.Node _root;

			// Token: 0x040000C2 RID: 194
			private SecurePooledObject<Stack<RefAsValueType<ImmutableList<T>.Node>>> _stack;

			// Token: 0x040000C3 RID: 195
			private ImmutableList<T>.Node _current;

			// Token: 0x040000C4 RID: 196
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x02000062 RID: 98
		[DebuggerDisplay("{_key}")]
		internal sealed class Node : IBinaryTree<!0>, IBinaryTree, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x060004EE RID: 1262 RVA: 0x0000CDF0 File Offset: 0x0000AFF0
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x060004EF RID: 1263 RVA: 0x0000CE00 File Offset: 0x0000B000
			private Node(T key, ImmutableList<T>.Node left, ImmutableList<T>.Node right, bool frozen = false)
			{
				Requires.NotNull<ImmutableList<T>.Node>(left, "left");
				Requires.NotNull<ImmutableList<T>.Node>(right, "right");
				this._key = key;
				this._left = left;
				this._right = right;
				this._height = checked(1 + Math.Max(left._height, right._height));
				this._count = 1 + left._count + right._count;
				this._frozen = frozen;
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000CE75 File Offset: 0x0000B075
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000CE80 File Offset: 0x0000B080
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000CE88 File Offset: 0x0000B088
			public ImmutableList<T>.Node Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000CE88 File Offset: 0x0000B088
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000CE98 File Offset: 0x0000B098
			public ImmutableList<T>.Node Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000CE98 File Offset: 0x0000B098
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000CE88 File Offset: 0x0000B088
			IBinaryTree<T> IBinaryTree<!0>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000CE98 File Offset: 0x0000B098
			IBinaryTree<T> IBinaryTree<!0>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
			public T Value
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000CEC0 File Offset: 0x0000B0C0
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
			internal T Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x170000FF RID: 255
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

			// Token: 0x060004FC RID: 1276 RVA: 0x0000CF42 File Offset: 0x0000B142
			public ImmutableList<T>.Enumerator GetEnumerator()
			{
				return new ImmutableList<T>.Enumerator(this, null, -1, -1, false);
			}

			// Token: 0x060004FD RID: 1277 RVA: 0x0000CF4E File Offset: 0x0000B14E
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060004FE RID: 1278 RVA: 0x0000CF4E File Offset: 0x0000B14E
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x0000CF68 File Offset: 0x0000B168
			internal ImmutableList<T>.Enumerator GetEnumerator(ImmutableList<T>.Builder builder)
			{
				return new ImmutableList<T>.Enumerator(this, builder, -1, -1, false);
			}

			// Token: 0x06000500 RID: 1280 RVA: 0x0000CF74 File Offset: 0x0000B174
			internal static ImmutableList<T>.Node NodeTreeFromList(IOrderedCollection<T> items, int start, int length)
			{
				Requires.NotNull<IOrderedCollection<T>>(items, "items");
				Requires.Range(start >= 0, "start", null);
				Requires.Range(length >= 0, "length", null);
				if (length == 0)
				{
					return ImmutableList<T>.Node.EmptyNode;
				}
				int num = (length - 1) / 2;
				int num2 = length - 1 - num;
				ImmutableList<T>.Node left = ImmutableList<T>.Node.NodeTreeFromList(items, start, num2);
				ImmutableList<T>.Node right = ImmutableList<T>.Node.NodeTreeFromList(items, start + num2 + 1, num);
				return new ImmutableList<T>.Node(items[start + num2], left, right, true);
			}

			// Token: 0x06000501 RID: 1281 RVA: 0x0000CFEC File Offset: 0x0000B1EC
			internal ImmutableList<T>.Node Add(T key)
			{
				return this.Insert(this._count, key);
			}

			// Token: 0x06000502 RID: 1282 RVA: 0x0000CFFC File Offset: 0x0000B1FC
			internal ImmutableList<T>.Node Insert(int index, T key)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				if (this.IsEmpty)
				{
					return new ImmutableList<T>.Node(key, this, this, false);
				}
				ImmutableList<T>.Node tree;
				if (index <= this._left._count)
				{
					ImmutableList<T>.Node left = this._left.Insert(index, key);
					tree = this.Mutate(left, null);
				}
				else
				{
					ImmutableList<T>.Node right = this._right.Insert(index - this._left._count - 1, key);
					tree = this.Mutate(null, right);
				}
				return ImmutableList<T>.Node.MakeBalanced(tree);
			}

			// Token: 0x06000503 RID: 1283 RVA: 0x0000D08B File Offset: 0x0000B28B
			internal ImmutableList<T>.Node AddRange(IEnumerable<T> keys)
			{
				return this.InsertRange(this._count, keys);
			}

			// Token: 0x06000504 RID: 1284 RVA: 0x0000D09C File Offset: 0x0000B29C
			internal ImmutableList<T>.Node InsertRange(int index, IEnumerable<T> keys)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				Requires.NotNull<IEnumerable<T>>(keys, "keys");
				if (!this.IsEmpty)
				{
					ImmutableList<T>.Node node;
					if (index <= this._left._count)
					{
						ImmutableList<T>.Node left = this._left.InsertRange(index, keys);
						node = this.Mutate(left, null);
					}
					else
					{
						ImmutableList<T>.Node right = this._right.InsertRange(index - this._left._count - 1, keys);
						node = this.Mutate(null, right);
					}
					return ImmutableList<T>.Node.BalanceNode(node);
				}
				ImmutableList<T> immutableList;
				if (ImmutableList<T>.TryCastToImmutableList(keys, out immutableList))
				{
					return immutableList._root;
				}
				IOrderedCollection<T> orderedCollection = keys.AsOrderedCollection<T>();
				return ImmutableList<T>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count);
			}

			// Token: 0x06000505 RID: 1285 RVA: 0x0000D154 File Offset: 0x0000B354
			internal ImmutableList<T>.Node RemoveAt(int index)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				ImmutableList<T>.Node node;
				if (index == this._left._count)
				{
					if (this._right.IsEmpty && this._left.IsEmpty)
					{
						node = ImmutableList<T>.Node.EmptyNode;
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
						ImmutableList<T>.Node node2 = this._right;
						while (!node2._left.IsEmpty)
						{
							node2 = node2._left;
						}
						ImmutableList<T>.Node right = this._right.RemoveAt(0);
						node = node2.Mutate(this._left, right);
					}
				}
				else if (index < this._left._count)
				{
					ImmutableList<T>.Node left = this._left.RemoveAt(index);
					node = this.Mutate(left, null);
				}
				else
				{
					ImmutableList<T>.Node right2 = this._right.RemoveAt(index - this._left._count - 1);
					node = this.Mutate(null, right2);
				}
				if (!node.IsEmpty)
				{
					return ImmutableList<T>.Node.MakeBalanced(node);
				}
				return node;
			}

			// Token: 0x06000506 RID: 1286 RVA: 0x0000D298 File Offset: 0x0000B498
			internal ImmutableList<T>.Node RemoveAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				ImmutableList<T>.Node node = this;
				int num = 0;
				foreach (T obj in this)
				{
					if (match(obj))
					{
						node = node.RemoveAt(num);
					}
					else
					{
						num++;
					}
				}
				return node;
			}

			// Token: 0x06000507 RID: 1287 RVA: 0x0000D308 File Offset: 0x0000B508
			internal ImmutableList<T>.Node ReplaceAt(int index, T value)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				ImmutableList<T>.Node result;
				if (index == this._left._count)
				{
					result = this.Mutate(value);
				}
				else if (index < this._left._count)
				{
					ImmutableList<T>.Node left = this._left.ReplaceAt(index, value);
					result = this.Mutate(left, null);
				}
				else
				{
					ImmutableList<T>.Node right = this._right.ReplaceAt(index - this._left._count - 1, value);
					result = this.Mutate(null, right);
				}
				return result;
			}

			// Token: 0x06000508 RID: 1288 RVA: 0x0000D397 File Offset: 0x0000B597
			internal ImmutableList<T>.Node Reverse()
			{
				return this.Reverse(0, this.Count);
			}

			// Token: 0x06000509 RID: 1289 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
			internal ImmutableList<T>.Node Reverse(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "index", null);
				ImmutableList<T>.Node node = this;
				int i = index;
				int num = index + count - 1;
				while (i < num)
				{
					T value = node[i];
					T value2 = node[num];
					node = node.ReplaceAt(num, value).ReplaceAt(i, value2);
					i++;
					num--;
				}
				return node;
			}

			// Token: 0x0600050A RID: 1290 RVA: 0x0000D42D File Offset: 0x0000B62D
			internal ImmutableList<T>.Node Sort()
			{
				return this.Sort(Comparer<T>.Default);
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x0000D43C File Offset: 0x0000B63C
			internal ImmutableList<T>.Node Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				T[] array = new T[this.Count];
				this.CopyTo(array);
				Array.Sort<T>(array, comparison);
				return ImmutableList<T>.Node.NodeTreeFromList(array.AsOrderedCollection<T>(), 0, this.Count);
			}

			// Token: 0x0600050C RID: 1292 RVA: 0x0000D480 File Offset: 0x0000B680
			internal ImmutableList<T>.Node Sort(IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				return this.Sort(0, this.Count, comparer);
			}

			// Token: 0x0600050D RID: 1293 RVA: 0x0000D49C File Offset: 0x0000B69C
			internal ImmutableList<T>.Node Sort(int index, int count, IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Argument(index + count <= this.Count);
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				T[] array = new T[this.Count];
				this.CopyTo(array);
				Array.Sort<T>(array, index, count, comparer);
				return ImmutableList<T>.Node.NodeTreeFromList(array.AsOrderedCollection<T>(), 0, this.Count);
			}

			// Token: 0x0600050E RID: 1294 RVA: 0x0000D51C File Offset: 0x0000B71C
			internal int BinarySearch(int index, int count, T item, IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				comparer = (comparer ?? Comparer<T>.Default);
				if (this.IsEmpty || count <= 0)
				{
					return ~index;
				}
				int count2 = this._left.Count;
				if (index + count <= count2)
				{
					return this._left.BinarySearch(index, count, item, comparer);
				}
				if (index > count2)
				{
					int num = this._right.BinarySearch(index - count2 - 1, count, item, comparer);
					int num2 = count2 + 1;
					if (num >= 0)
					{
						return num + num2;
					}
					return num - num2;
				}
				else
				{
					int num3 = comparer.Compare(item, this._key);
					if (num3 == 0)
					{
						return count2;
					}
					if (num3 > 0)
					{
						int num4 = count - (count2 - index) - 1;
						int num5 = (num4 < 0) ? -1 : this._right.BinarySearch(0, num4, item, comparer);
						int num6 = count2 + 1;
						if (num5 >= 0)
						{
							return num5 + num6;
						}
						return num5 - num6;
					}
					else
					{
						if (index == count2)
						{
							return ~index;
						}
						return this._left.BinarySearch(index, count, item, comparer);
					}
				}
			}

			// Token: 0x0600050F RID: 1295 RVA: 0x0000D61E File Offset: 0x0000B81E
			internal int IndexOf(T item, IEqualityComparer<T> equalityComparer)
			{
				return this.IndexOf(item, 0, this.Count, equalityComparer);
			}

			// Token: 0x06000510 RID: 1296 RVA: 0x0000D630 File Offset: 0x0000B830
			internal int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(count <= this.Count, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, index, count, false))
				{
					while (enumerator.MoveNext())
					{
						if (equalityComparer.Equals(item, enumerator.Current))
						{
							return index;
						}
						index++;
					}
				}
				return -1;
			}

			// Token: 0x06000511 RID: 1297 RVA: 0x0000D6F4 File Offset: 0x0000B8F4
			internal int LastIndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
			{
				Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "ValueComparer");
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0 && count <= this.Count, "count", null);
				Requires.Argument(index - count + 1 >= 0);
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, index, count, true))
				{
					while (enumerator.MoveNext())
					{
						if (equalityComparer.Equals(item, enumerator.Current))
						{
							return index;
						}
						index--;
					}
				}
				return -1;
			}

			// Token: 0x06000512 RID: 1298 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
			internal void CopyTo(T[] array)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Argument(array.Length >= this.Count);
				int num = 0;
				foreach (T t in this)
				{
					array[num++] = t;
				}
			}

			// Token: 0x06000513 RID: 1299 RVA: 0x0000D818 File Offset: 0x0000BA18
			internal void CopyTo(T[] array, int arrayIndex)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(arrayIndex <= array.Length, "arrayIndex", null);
				Requires.Argument(arrayIndex + this.Count <= array.Length);
				foreach (T t in this)
				{
					array[arrayIndex++] = t;
				}
			}

			// Token: 0x06000514 RID: 1300 RVA: 0x0000D8B4 File Offset: 0x0000BAB4
			internal void CopyTo(int index, T[] array, int arrayIndex, int count)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(arrayIndex + count <= array.Length, "arrayIndex", null);
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, index, count, false))
				{
					while (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						array[arrayIndex++] = t;
					}
				}
			}

			// Token: 0x06000515 RID: 1301 RVA: 0x0000D980 File Offset: 0x0000BB80
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

			// Token: 0x06000516 RID: 1302 RVA: 0x0000DA20 File Offset: 0x0000BC20
			internal ImmutableList<TOutput>.Node ConvertAll<TOutput>(Func<T, TOutput> converter)
			{
				ImmutableList<TOutput>.Node emptyNode = ImmutableList<TOutput>.Node.EmptyNode;
				if (this.IsEmpty)
				{
					return emptyNode;
				}
				return emptyNode.AddRange(this.Select(converter));
			}

			// Token: 0x06000517 RID: 1303 RVA: 0x0000DA4C File Offset: 0x0000BC4C
			internal bool TrueForAll(Predicate<T> match)
			{
				foreach (T obj in this)
				{
					if (!match(obj))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06000518 RID: 1304 RVA: 0x0000DAA4 File Offset: 0x0000BCA4
			internal bool Exists(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				foreach (T obj in this)
				{
					if (match(obj))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000519 RID: 1305 RVA: 0x0000DB08 File Offset: 0x0000BD08
			internal T Find(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				foreach (T t in this)
				{
					if (match(t))
					{
						return t;
					}
				}
				return default(T);
			}

			// Token: 0x0600051A RID: 1306 RVA: 0x0000DB74 File Offset: 0x0000BD74
			internal ImmutableList<T> FindAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Empty;
				}
				List<T> list = null;
				foreach (T t in this)
				{
					if (match(t))
					{
						if (list == null)
						{
							list = new List<T>();
						}
						list.Add(t);
					}
				}
				if (list == null)
				{
					return ImmutableList<T>.Empty;
				}
				return ImmutableList.CreateRange<T>(list);
			}

			// Token: 0x0600051B RID: 1307 RVA: 0x0000DC00 File Offset: 0x0000BE00
			internal int FindIndex(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				return this.FindIndex(0, this._count, match);
			}

			// Token: 0x0600051C RID: 1308 RVA: 0x0000DC1C File Offset: 0x0000BE1C
			internal int FindIndex(int startIndex, Predicate<T> match)
			{
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(startIndex <= this.Count, "startIndex", null);
				Requires.NotNull<Predicate<T>>(match, "match");
				return this.FindIndex(startIndex, this.Count - startIndex, match);
			}

			// Token: 0x0600051D RID: 1309 RVA: 0x0000DC70 File Offset: 0x0000BE70
			internal int FindIndex(int startIndex, int count, Predicate<T> match)
			{
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Argument(startIndex + count <= this.Count);
				Requires.NotNull<Predicate<T>>(match, "match");
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, startIndex, count, false))
				{
					int num = startIndex;
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							return num;
						}
						num++;
					}
				}
				return -1;
			}

			// Token: 0x0600051E RID: 1310 RVA: 0x0000DD14 File Offset: 0x0000BF14
			internal T FindLast(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, -1, -1, true))
				{
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							return enumerator.Current;
						}
					}
				}
				return default(T);
			}

			// Token: 0x0600051F RID: 1311 RVA: 0x0000DD88 File Offset: 0x0000BF88
			internal int FindLastIndex(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				if (this.IsEmpty)
				{
					return -1;
				}
				return this.FindLastIndex(this.Count - 1, this.Count, match);
			}

			// Token: 0x06000520 RID: 1312 RVA: 0x0000DDB4 File Offset: 0x0000BFB4
			internal int FindLastIndex(int startIndex, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(startIndex == 0 || startIndex < this.Count, "startIndex", null);
				if (this.IsEmpty)
				{
					return -1;
				}
				return this.FindLastIndex(startIndex, startIndex + 1, match);
			}

			// Token: 0x06000521 RID: 1313 RVA: 0x0000DE10 File Offset: 0x0000C010
			internal int FindLastIndex(int startIndex, int count, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(count <= this.Count, "count", null);
				Requires.Argument(startIndex - count + 1 >= 0);
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, startIndex, count, true))
				{
					int num = startIndex;
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							return num;
						}
						num--;
					}
				}
				return -1;
			}

			// Token: 0x06000522 RID: 1314 RVA: 0x0000DEB4 File Offset: 0x0000C0B4
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x06000523 RID: 1315 RVA: 0x0000DEDC File Offset: 0x0000C0DC
			private static ImmutableList<T>.Node RotateLeft(ImmutableList<T>.Node tree)
			{
				Requires.NotNull<ImmutableList<T>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableList<T>.Node right = tree._right;
				return right.Mutate(tree.Mutate(null, right._left), null);
			}

			// Token: 0x06000524 RID: 1316 RVA: 0x0000DF20 File Offset: 0x0000C120
			private static ImmutableList<T>.Node RotateRight(ImmutableList<T>.Node tree)
			{
				Requires.NotNull<ImmutableList<T>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableList<T>.Node left = tree._left;
				return left.Mutate(null, tree.Mutate(left._right, null));
			}

			// Token: 0x06000525 RID: 1317 RVA: 0x0000DF62 File Offset: 0x0000C162
			private static ImmutableList<T>.Node DoubleLeft(ImmutableList<T>.Node tree)
			{
				Requires.NotNull<ImmutableList<T>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				return ImmutableList<T>.Node.RotateLeft(tree.Mutate(null, ImmutableList<T>.Node.RotateRight(tree._right)));
			}

			// Token: 0x06000526 RID: 1318 RVA: 0x0000DF95 File Offset: 0x0000C195
			private static ImmutableList<T>.Node DoubleRight(ImmutableList<T>.Node tree)
			{
				Requires.NotNull<ImmutableList<T>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				return ImmutableList<T>.Node.RotateRight(tree.Mutate(ImmutableList<T>.Node.RotateLeft(tree._left), null));
			}

			// Token: 0x06000527 RID: 1319 RVA: 0x0000DFC8 File Offset: 0x0000C1C8
			private static int Balance(ImmutableList<T>.Node tree)
			{
				Requires.NotNull<ImmutableList<T>.Node>(tree, "tree");
				return (int)(tree._right._height - tree._left._height);
			}

			// Token: 0x06000528 RID: 1320 RVA: 0x0000DFEC File Offset: 0x0000C1EC
			private static bool IsRightHeavy(ImmutableList<T>.Node tree)
			{
				Requires.NotNull<ImmutableList<T>.Node>(tree, "tree");
				return ImmutableList<T>.Node.Balance(tree) >= 2;
			}

			// Token: 0x06000529 RID: 1321 RVA: 0x0000E005 File Offset: 0x0000C205
			private static bool IsLeftHeavy(ImmutableList<T>.Node tree)
			{
				Requires.NotNull<ImmutableList<T>.Node>(tree, "tree");
				return ImmutableList<T>.Node.Balance(tree) <= -2;
			}

			// Token: 0x0600052A RID: 1322 RVA: 0x0000E020 File Offset: 0x0000C220
			private static ImmutableList<T>.Node MakeBalanced(ImmutableList<T>.Node tree)
			{
				Requires.NotNull<ImmutableList<T>.Node>(tree, "tree");
				if (ImmutableList<T>.Node.IsRightHeavy(tree))
				{
					if (ImmutableList<T>.Node.Balance(tree._right) >= 0)
					{
						return ImmutableList<T>.Node.RotateLeft(tree);
					}
					return ImmutableList<T>.Node.DoubleLeft(tree);
				}
				else
				{
					if (!ImmutableList<T>.Node.IsLeftHeavy(tree))
					{
						return tree;
					}
					if (ImmutableList<T>.Node.Balance(tree._left) <= 0)
					{
						return ImmutableList<T>.Node.RotateRight(tree);
					}
					return ImmutableList<T>.Node.DoubleRight(tree);
				}
			}

			// Token: 0x0600052B RID: 1323 RVA: 0x0000E084 File Offset: 0x0000C284
			private static ImmutableList<T>.Node BalanceNode(ImmutableList<T>.Node node)
			{
				while (ImmutableList<T>.Node.IsRightHeavy(node) || ImmutableList<T>.Node.IsLeftHeavy(node))
				{
					if (ImmutableList<T>.Node.IsRightHeavy(node))
					{
						node = ((ImmutableList<T>.Node.Balance(node._right) < 0) ? ImmutableList<T>.Node.DoubleLeft(node) : ImmutableList<T>.Node.RotateLeft(node));
						node.Mutate(ImmutableList<T>.Node.BalanceNode(node._left), null);
					}
					else
					{
						node = ((ImmutableList<T>.Node.Balance(node._left) > 0) ? ImmutableList<T>.Node.DoubleRight(node) : ImmutableList<T>.Node.RotateRight(node));
						node.Mutate(null, ImmutableList<T>.Node.BalanceNode(node._right));
					}
				}
				return node;
			}

			// Token: 0x0600052C RID: 1324 RVA: 0x0000E110 File Offset: 0x0000C310
			private ImmutableList<T>.Node Mutate(ImmutableList<T>.Node left = null, ImmutableList<T>.Node right = null)
			{
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(this._key, left ?? this._left, right ?? this._right, false);
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

			// Token: 0x0600052D RID: 1325 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
			private ImmutableList<T>.Node Mutate(T value)
			{
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(value, this._left, this._right, false);
				}
				this._key = value;
				return this;
			}

			// Token: 0x040000C5 RID: 197
			internal static readonly ImmutableList<T>.Node EmptyNode = new ImmutableList<T>.Node();

			// Token: 0x040000C6 RID: 198
			private T _key;

			// Token: 0x040000C7 RID: 199
			private bool _frozen;

			// Token: 0x040000C8 RID: 200
			private byte _height;

			// Token: 0x040000C9 RID: 201
			private int _count;

			// Token: 0x040000CA RID: 202
			private ImmutableList<T>.Node _left;

			// Token: 0x040000CB RID: 203
			private ImmutableList<T>.Node _right;
		}
	}
}
