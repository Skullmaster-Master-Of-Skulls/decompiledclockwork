using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000155 RID: 341
	[__DynamicallyInvokable]
	public class Lookup<TKey, TElement> : IEnumerable<IGrouping<TKey, TElement>>, IEnumerable, ILookup<TKey, TElement>
	{
		// Token: 0x06000C02 RID: 3074 RVA: 0x0002C770 File Offset: 0x0002A970
		internal static Lookup<TKey, TElement> Create<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			Lookup<TKey, TElement> lookup = new Lookup<TKey, TElement>(comparer);
			foreach (TSource arg in source)
			{
				lookup.GetGrouping(keySelector(arg), true).Add(elementSelector(arg));
			}
			return lookup;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0002C800 File Offset: 0x0002AA00
		internal static Lookup<TKey, TElement> CreateForJoin(IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Lookup<TKey, TElement> lookup = new Lookup<TKey, TElement>(comparer);
			foreach (TElement telement in source)
			{
				TKey tkey = keySelector(telement);
				if (tkey != null)
				{
					lookup.GetGrouping(tkey, true).Add(telement);
				}
			}
			return lookup;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002C868 File Offset: 0x0002AA68
		private Lookup(IEqualityComparer<TKey> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TKey>.Default;
			}
			this.comparer = comparer;
			this.groupings = new Lookup<TKey, TElement>.Grouping[7];
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002C88D File Offset: 0x0002AA8D
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000227 RID: 551
		[__DynamicallyInvokable]
		public IEnumerable<TElement> this[TKey key]
		{
			[__DynamicallyInvokable]
			get
			{
				Lookup<TKey, TElement>.Grouping grouping = this.GetGrouping(key, false);
				if (grouping != null)
				{
					return grouping;
				}
				return EmptyEnumerable<TElement>.Instance;
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002C8B8 File Offset: 0x0002AAB8
		[__DynamicallyInvokable]
		public bool Contains(TKey key)
		{
			return this.GetGrouping(key, false) != null;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002C8C5 File Offset: 0x0002AAC5
		[__DynamicallyInvokable]
		public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
		{
			Lookup<TKey, TElement>.Grouping g = this.lastGrouping;
			if (g != null)
			{
				do
				{
					g = g.next;
					yield return g;
				}
				while (g != this.lastGrouping);
			}
			yield break;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002C8D4 File Offset: 0x0002AAD4
		[__DynamicallyInvokable]
		public IEnumerable<TResult> ApplyResultSelector<TResult>(Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		{
			Lookup<TKey, TElement>.Grouping g = this.lastGrouping;
			if (g != null)
			{
				do
				{
					g = g.next;
					if (g.count != g.elements.Length)
					{
						Array.Resize<TElement>(ref g.elements, g.count);
					}
					yield return resultSelector(g.key, g.elements);
				}
				while (g != this.lastGrouping);
			}
			yield break;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002C8EB File Offset: 0x0002AAEB
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002C8F3 File Offset: 0x0002AAF3
		internal int InternalGetHashCode(TKey key)
		{
			if (key != null)
			{
				return this.comparer.GetHashCode(key) & int.MaxValue;
			}
			return 0;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002C914 File Offset: 0x0002AB14
		internal Lookup<TKey, TElement>.Grouping GetGrouping(TKey key, bool create)
		{
			int num = this.InternalGetHashCode(key);
			for (Lookup<TKey, TElement>.Grouping grouping = this.groupings[num % this.groupings.Length]; grouping != null; grouping = grouping.hashNext)
			{
				if (grouping.hashCode == num && this.comparer.Equals(grouping.key, key))
				{
					return grouping;
				}
			}
			if (create)
			{
				if (this.count == this.groupings.Length)
				{
					this.Resize();
				}
				int num2 = num % this.groupings.Length;
				Lookup<TKey, TElement>.Grouping grouping2 = new Lookup<TKey, TElement>.Grouping();
				grouping2.key = key;
				grouping2.hashCode = num;
				grouping2.elements = new TElement[1];
				grouping2.hashNext = this.groupings[num2];
				this.groupings[num2] = grouping2;
				if (this.lastGrouping == null)
				{
					grouping2.next = grouping2;
				}
				else
				{
					grouping2.next = this.lastGrouping.next;
					this.lastGrouping.next = grouping2;
				}
				this.lastGrouping = grouping2;
				this.count++;
				return grouping2;
			}
			return null;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002CA0C File Offset: 0x0002AC0C
		private void Resize()
		{
			int num = checked(this.count * 2 + 1);
			Lookup<TKey, TElement>.Grouping[] array = new Lookup<TKey, TElement>.Grouping[num];
			Lookup<TKey, TElement>.Grouping next = this.lastGrouping;
			do
			{
				next = next.next;
				int num2 = next.hashCode % num;
				next.hashNext = array[num2];
				array[num2] = next;
			}
			while (next != this.lastGrouping);
			this.groupings = array;
		}

		// Token: 0x0400077C RID: 1916
		private IEqualityComparer<TKey> comparer;

		// Token: 0x0400077D RID: 1917
		private Lookup<TKey, TElement>.Grouping[] groupings;

		// Token: 0x0400077E RID: 1918
		private Lookup<TKey, TElement>.Grouping lastGrouping;

		// Token: 0x0400077F RID: 1919
		private int count;

		// Token: 0x02000398 RID: 920
		internal class Grouping : IGrouping<!0, !1>, IEnumerable<!1>, IEnumerable, IList<TElement>, ICollection<TElement>
		{
			// Token: 0x06001CEE RID: 7406 RVA: 0x00067908 File Offset: 0x00065B08
			internal void Add(TElement element)
			{
				if (this.elements.Length == this.count)
				{
					Array.Resize<TElement>(ref this.elements, checked(this.count * 2));
				}
				this.elements[this.count] = element;
				this.count++;
			}

			// Token: 0x06001CEF RID: 7407 RVA: 0x00067958 File Offset: 0x00065B58
			public IEnumerator<TElement> GetEnumerator()
			{
				int num;
				for (int i = 0; i < this.count; i = num + 1)
				{
					yield return this.elements[i];
					num = i;
				}
				yield break;
			}

			// Token: 0x06001CF0 RID: 7408 RVA: 0x00067967 File Offset: 0x00065B67
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x17000555 RID: 1365
			// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x0006796F File Offset: 0x00065B6F
			public TKey Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x17000556 RID: 1366
			// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x00067977 File Offset: 0x00065B77
			int ICollection<!1>.Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x17000557 RID: 1367
			// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x0006797F File Offset: 0x00065B7F
			bool ICollection<!1>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001CF4 RID: 7412 RVA: 0x00067982 File Offset: 0x00065B82
			void ICollection<!1>.Add(TElement item)
			{
				throw Error.NotSupported();
			}

			// Token: 0x06001CF5 RID: 7413 RVA: 0x00067989 File Offset: 0x00065B89
			void ICollection<!1>.Clear()
			{
				throw Error.NotSupported();
			}

			// Token: 0x06001CF6 RID: 7414 RVA: 0x00067990 File Offset: 0x00065B90
			bool ICollection<!1>.Contains(TElement item)
			{
				return Array.IndexOf<TElement>(this.elements, item, 0, this.count) >= 0;
			}

			// Token: 0x06001CF7 RID: 7415 RVA: 0x000679AB File Offset: 0x00065BAB
			void ICollection<!1>.CopyTo(TElement[] array, int arrayIndex)
			{
				Array.Copy(this.elements, 0, array, arrayIndex, this.count);
			}

			// Token: 0x06001CF8 RID: 7416 RVA: 0x000679C1 File Offset: 0x00065BC1
			bool ICollection<!1>.Remove(TElement item)
			{
				throw Error.NotSupported();
			}

			// Token: 0x06001CF9 RID: 7417 RVA: 0x000679C8 File Offset: 0x00065BC8
			int IList<!1>.IndexOf(TElement item)
			{
				return Array.IndexOf<TElement>(this.elements, item, 0, this.count);
			}

			// Token: 0x06001CFA RID: 7418 RVA: 0x000679DD File Offset: 0x00065BDD
			void IList<!1>.Insert(int index, TElement item)
			{
				throw Error.NotSupported();
			}

			// Token: 0x06001CFB RID: 7419 RVA: 0x000679E4 File Offset: 0x00065BE4
			void IList<!1>.RemoveAt(int index)
			{
				throw Error.NotSupported();
			}

			// Token: 0x17000558 RID: 1368
			TElement IList<!1>.this[int index]
			{
				get
				{
					if (index < 0 || index >= this.count)
					{
						throw Error.ArgumentOutOfRange("index");
					}
					return this.elements[index];
				}
				set
				{
					throw Error.NotSupported();
				}
			}

			// Token: 0x040010C6 RID: 4294
			internal TKey key;

			// Token: 0x040010C7 RID: 4295
			internal int hashCode;

			// Token: 0x040010C8 RID: 4296
			internal TElement[] elements;

			// Token: 0x040010C9 RID: 4297
			internal int count;

			// Token: 0x040010CA RID: 4298
			internal Lookup<TKey, TElement>.Grouping hashNext;

			// Token: 0x040010CB RID: 4299
			internal Lookup<TKey, TElement>.Grouping next;
		}
	}
}
