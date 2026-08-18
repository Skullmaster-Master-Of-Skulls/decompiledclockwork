using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000020 RID: 32
	internal static class ImmutableExtensions
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x000057AC File Offset: 0x000039AC
		internal static bool TryGetCount<T>(this IEnumerable<T> sequence, out int count)
		{
			return sequence.TryGetCount(out count);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000057B8 File Offset: 0x000039B8
		internal static bool TryGetCount<T>(this IEnumerable sequence, out int count)
		{
			ICollection collection = sequence as ICollection;
			if (collection != null)
			{
				count = collection.Count;
				return true;
			}
			ICollection<T> collection2 = sequence as ICollection<T>;
			if (collection2 != null)
			{
				count = collection2.Count;
				return true;
			}
			IReadOnlyCollection<T> readOnlyCollection = sequence as IReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				count = readOnlyCollection.Count;
				return true;
			}
			count = 0;
			return false;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00005808 File Offset: 0x00003A08
		internal static int GetCount<T>(ref IEnumerable<T> sequence)
		{
			int count;
			if (!sequence.TryGetCount(out count))
			{
				List<T> list = sequence.ToList<T>();
				count = list.Count;
				sequence = list;
			}
			return count;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00005834 File Offset: 0x00003A34
		internal static T[] ToArray<T>(this IEnumerable<T> sequence, int count)
		{
			Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
			Requires.Range(count >= 0, "count", null);
			T[] array = new T[count];
			int num = 0;
			foreach (T t in sequence)
			{
				Requires.Argument(num < count);
				array[num++] = t;
			}
			Requires.Argument(num == count);
			return array;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000058BC File Offset: 0x00003ABC
		internal static IOrderedCollection<T> AsOrderedCollection<T>(this IEnumerable<T> sequence)
		{
			Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
			IOrderedCollection<T> orderedCollection = sequence as IOrderedCollection<T>;
			if (orderedCollection != null)
			{
				return orderedCollection;
			}
			IList<T> list = sequence as IList<T>;
			if (list != null)
			{
				return new ImmutableExtensions.ListOfTWrapper<T>(list);
			}
			return new ImmutableExtensions.FallbackWrapper<T>(sequence);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000058F7 File Offset: 0x00003AF7
		internal static void ClearFastWhenEmpty<T>(this Stack<T> stack)
		{
			if (stack.Count > 0)
			{
				stack.Clear();
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005908 File Offset: 0x00003B08
		internal static DisposableEnumeratorAdapter<T, TEnumerator> GetEnumerableDisposable<T, TEnumerator>(this IEnumerable<T> enumerable) where TEnumerator : struct, IStrongEnumerator<T>, IEnumerator<T>
		{
			Requires.NotNull<IEnumerable<T>>(enumerable, "enumerable");
			IStrongEnumerable<T, TEnumerator> strongEnumerable = enumerable as IStrongEnumerable<T, TEnumerator>;
			if (strongEnumerable != null)
			{
				return new DisposableEnumeratorAdapter<T, TEnumerator>(strongEnumerable.GetEnumerator());
			}
			return new DisposableEnumeratorAdapter<T, TEnumerator>(enumerable.GetEnumerator());
		}

		// Token: 0x02000055 RID: 85
		private class ListOfTWrapper<T> : IOrderedCollection<T>, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x06000453 RID: 1107 RVA: 0x0000B868 File Offset: 0x00009A68
			internal ListOfTWrapper(IList<T> collection)
			{
				Requires.NotNull<IList<T>>(collection, "collection");
				this._collection = collection;
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000B882 File Offset: 0x00009A82
			public int Count
			{
				get
				{
					return this._collection.Count;
				}
			}

			// Token: 0x170000D4 RID: 212
			public T this[int index]
			{
				get
				{
					return this._collection[index];
				}
			}

			// Token: 0x06000456 RID: 1110 RVA: 0x0000B89D File Offset: 0x00009A9D
			public IEnumerator<T> GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			// Token: 0x06000457 RID: 1111 RVA: 0x0000B8AA File Offset: 0x00009AAA
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400009A RID: 154
			private readonly IList<T> _collection;
		}

		// Token: 0x02000056 RID: 86
		private class FallbackWrapper<T> : IOrderedCollection<T>, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x06000458 RID: 1112 RVA: 0x0000B8B2 File Offset: 0x00009AB2
			internal FallbackWrapper(IEnumerable<T> sequence)
			{
				Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
				this._sequence = sequence;
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000B8CC File Offset: 0x00009ACC
			public int Count
			{
				get
				{
					if (this._collection == null)
					{
						int result;
						if (this._sequence.TryGetCount(out result))
						{
							return result;
						}
						this._collection = this._sequence.ToArray<T>();
					}
					return this._collection.Count;
				}
			}

			// Token: 0x170000D6 RID: 214
			public T this[int index]
			{
				get
				{
					if (this._collection == null)
					{
						this._collection = this._sequence.ToArray<T>();
					}
					return this._collection[index];
				}
			}

			// Token: 0x0600045B RID: 1115 RVA: 0x0000B935 File Offset: 0x00009B35
			public IEnumerator<T> GetEnumerator()
			{
				return this._sequence.GetEnumerator();
			}

			// Token: 0x0600045C RID: 1116 RVA: 0x0000B942 File Offset: 0x00009B42
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400009B RID: 155
			private readonly IEnumerable<T> _sequence;

			// Token: 0x0400009C RID: 156
			private IList<T> _collection;
		}
	}
}
