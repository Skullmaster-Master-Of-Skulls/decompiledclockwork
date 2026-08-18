using System;
using System.Collections.Generic;
using System.Linq;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000019 RID: 25
	public static class ImmutableArray
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00003386 File Offset: 0x00001586
		public static ImmutableArray<T> Create<T>()
		{
			return ImmutableArray<T>.Empty;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000338D File Offset: 0x0000158D
		public static ImmutableArray<T> Create<T>(T item)
		{
			return new ImmutableArray<T>(new T[]
			{
				item
			});
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000033A2 File Offset: 0x000015A2
		public static ImmutableArray<T> Create<T>(T item1, T item2)
		{
			return new ImmutableArray<T>(new T[]
			{
				item1,
				item2
			});
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000033BF File Offset: 0x000015BF
		public static ImmutableArray<T> Create<T>(T item1, T item2, T item3)
		{
			return new ImmutableArray<T>(new T[]
			{
				item1,
				item2,
				item3
			});
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000033E4 File Offset: 0x000015E4
		public static ImmutableArray<T> Create<T>(T item1, T item2, T item3, T item4)
		{
			return new ImmutableArray<T>(new T[]
			{
				item1,
				item2,
				item3,
				item4
			});
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003414 File Offset: 0x00001614
		public static ImmutableArray<T> CreateRange<T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			IImmutableArray immutableArray = items as IImmutableArray;
			if (immutableArray != null)
			{
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				T[] array = immutableArray.Array as T[];
				if (array != null || immutableArray.Array == null)
				{
					return new ImmutableArray<T>(array);
				}
			}
			int num;
			if (!items.TryGetCount(out num))
			{
				return new ImmutableArray<T>(items.ToArray<T>());
			}
			if (num == 0)
			{
				return ImmutableArray.Create<T>();
			}
			return new ImmutableArray<T>(items.ToArray(num));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003485 File Offset: 0x00001685
		public static ImmutableArray<T> Create<T>(params T[] items)
		{
			if (items == null)
			{
				return ImmutableArray.Create<T>();
			}
			return ImmutableArray.CreateDefensiveCopy<T>(items);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003498 File Offset: 0x00001698
		public static ImmutableArray<T> Create<T>(T[] items, int start, int length)
		{
			Requires.NotNull<T[]>(items, "items");
			Requires.Range(start >= 0 && start <= items.Length, "start", null);
			Requires.Range(length >= 0 && start + length <= items.Length, "length", null);
			if (length == 0)
			{
				return ImmutableArray.Create<T>();
			}
			T[] array = new T[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = items[start + i];
			}
			return new ImmutableArray<T>(array);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000351C File Offset: 0x0000171C
		public static ImmutableArray<T> Create<T>(ImmutableArray<T> items, int start, int length)
		{
			Requires.Range(start >= 0 && start <= items.Length, "start", null);
			Requires.Range(length >= 0 && start + length <= items.Length, "length", null);
			if (length == 0)
			{
				return ImmutableArray.Create<T>();
			}
			if (start == 0 && length == items.Length)
			{
				return items;
			}
			T[] array = new T[length];
			Array.Copy(items.array, start, array, 0, length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000035A0 File Offset: 0x000017A0
		public static ImmutableArray<TResult> CreateRange<TSource, TResult>(ImmutableArray<TSource> items, Func<TSource, TResult> selector)
		{
			Requires.NotNull<Func<TSource, TResult>>(selector, "selector");
			int length = items.Length;
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = selector(items[i]);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000035F8 File Offset: 0x000017F8
		public static ImmutableArray<TResult> CreateRange<TSource, TResult>(ImmutableArray<TSource> items, int start, int length, Func<TSource, TResult> selector)
		{
			int length2 = items.Length;
			Requires.Range(start >= 0 && start <= length2, "start", null);
			Requires.Range(length >= 0 && start + length <= length2, "length", null);
			Requires.NotNull<Func<TSource, TResult>>(selector, "selector");
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = selector(items[i + start]);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003688 File Offset: 0x00001888
		public static ImmutableArray<TResult> CreateRange<TSource, TArg, TResult>(ImmutableArray<TSource> items, Func<TSource, TArg, TResult> selector, TArg arg)
		{
			Requires.NotNull<Func<TSource, TArg, TResult>>(selector, "selector");
			int length = items.Length;
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = selector(items[i], arg);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000036E0 File Offset: 0x000018E0
		public static ImmutableArray<TResult> CreateRange<TSource, TArg, TResult>(ImmutableArray<TSource> items, int start, int length, Func<TSource, TArg, TResult> selector, TArg arg)
		{
			int length2 = items.Length;
			Requires.Range(start >= 0 && start <= length2, "start", null);
			Requires.Range(length >= 0 && start + length <= length2, "length", null);
			Requires.NotNull<Func<TSource, TArg, TResult>>(selector, "selector");
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = selector(items[i + start], arg);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003770 File Offset: 0x00001970
		public static ImmutableArray<T>.Builder CreateBuilder<T>()
		{
			return ImmutableArray.Create<T>().ToBuilder();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000378A File Offset: 0x0000198A
		public static ImmutableArray<T>.Builder CreateBuilder<T>(int initialCapacity)
		{
			return new ImmutableArray<T>.Builder(initialCapacity);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003792 File Offset: 0x00001992
		public static ImmutableArray<TSource> ToImmutableArray<TSource>(this IEnumerable<TSource> items)
		{
			if (items is ImmutableArray<TSource>)
			{
				return (ImmutableArray<TSource>)items;
			}
			return ImmutableArray.CreateRange<TSource>(items);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000037A9 File Offset: 0x000019A9
		public static int BinarySearch<T>(this ImmutableArray<T> array, T value)
		{
			return Array.BinarySearch<T>(array.array, value);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000037B7 File Offset: 0x000019B7
		public static int BinarySearch<T>(this ImmutableArray<T> array, T value, IComparer<T> comparer)
		{
			return Array.BinarySearch<T>(array.array, value, comparer);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000037C6 File Offset: 0x000019C6
		public static int BinarySearch<T>(this ImmutableArray<T> array, int index, int length, T value)
		{
			return Array.BinarySearch<T>(array.array, index, length, value);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000037D6 File Offset: 0x000019D6
		public static int BinarySearch<T>(this ImmutableArray<T> array, int index, int length, T value, IComparer<T> comparer)
		{
			return Array.BinarySearch<T>(array.array, index, length, value, comparer);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000037E8 File Offset: 0x000019E8
		internal static ImmutableArray<T> CreateDefensiveCopy<T>(T[] items)
		{
			if (items == null)
			{
				return default(ImmutableArray<T>);
			}
			if (items.Length == 0)
			{
				return ImmutableArray<T>.Empty;
			}
			T[] array = new T[items.Length];
			Array.Copy(items, array, items.Length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x0400000F RID: 15
		internal static readonly byte[] TwoElementArray = new byte[2];
	}
}
