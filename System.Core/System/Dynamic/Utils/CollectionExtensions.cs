using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Dynamic.Utils
{
	// Token: 0x020000D4 RID: 212
	internal static class CollectionExtensions
	{
		// Token: 0x06000686 RID: 1670 RVA: 0x00015664 File Offset: 0x00013864
		internal static ReadOnlyCollection<T> ToReadOnly<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				return EmptyReadOnlyCollection<T>.Instance;
			}
			TrueReadOnlyCollection<T> trueReadOnlyCollection = enumerable as TrueReadOnlyCollection<T>;
			if (trueReadOnlyCollection != null)
			{
				return trueReadOnlyCollection;
			}
			ReadOnlyCollectionBuilder<T> readOnlyCollectionBuilder = enumerable as ReadOnlyCollectionBuilder<T>;
			if (readOnlyCollectionBuilder != null)
			{
				return readOnlyCollectionBuilder.ToReadOnlyCollection();
			}
			ICollection<T> collection = enumerable as ICollection<T>;
			if (collection == null)
			{
				return new TrueReadOnlyCollection<T>(new List<T>(enumerable).ToArray());
			}
			int count = collection.Count;
			if (count == 0)
			{
				return EmptyReadOnlyCollection<T>.Instance;
			}
			T[] array = new T[count];
			collection.CopyTo(array, 0);
			return new TrueReadOnlyCollection<T>(array);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x000156DC File Offset: 0x000138DC
		internal static int ListHashCode<T>(this IEnumerable<T> list)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			int num = 6551;
			foreach (T obj in list)
			{
				num ^= (num << 5 ^ @default.GetHashCode(obj));
			}
			return num;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00015738 File Offset: 0x00013938
		internal static bool ListEquals<T>(this ICollection<T> first, ICollection<T> second)
		{
			if (first.Count != second.Count)
			{
				return false;
			}
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			IEnumerator<T> enumerator = first.GetEnumerator();
			IEnumerator<T> enumerator2 = second.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator2.MoveNext();
				if (!@default.Equals(enumerator.Current, enumerator2.Current))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00015791 File Offset: 0x00013991
		internal static IEnumerable<U> Select<T, U>(this IEnumerable<T> enumerable, Func<T, U> select)
		{
			foreach (T arg in enumerable)
			{
				yield return select(arg);
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000157A8 File Offset: 0x000139A8
		internal static U[] Map<T, U>(this ICollection<T> collection, Func<T, U> select)
		{
			int num = collection.Count;
			U[] array = new U[num];
			num = 0;
			foreach (T arg in collection)
			{
				array[num++] = select(arg);
			}
			return array;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001580C File Offset: 0x00013A0C
		internal static IEnumerable<T> Where<T>(this IEnumerable<T> enumerable, Func<T, bool> where)
		{
			foreach (T t in enumerable)
			{
				if (where(t))
				{
					yield return t;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00015824 File Offset: 0x00013A24
		internal static bool Any<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			foreach (T arg in source)
			{
				if (predicate(arg))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00015878 File Offset: 0x00013A78
		internal static bool All<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			foreach (T arg in source)
			{
				if (!predicate(arg))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000158CC File Offset: 0x00013ACC
		internal static T[] RemoveFirst<T>(this T[] array)
		{
			T[] array2 = new T[array.Length - 1];
			Array.Copy(array, 1, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000158F4 File Offset: 0x00013AF4
		internal static T[] RemoveLast<T>(this T[] array)
		{
			T[] array2 = new T[array.Length - 1];
			Array.Copy(array, 0, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001591C File Offset: 0x00013B1C
		internal static T[] AddFirst<T>(this IList<T> list, T item)
		{
			T[] array = new T[list.Count + 1];
			array[0] = item;
			list.CopyTo(array, 1);
			return array;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00015948 File Offset: 0x00013B48
		internal static T[] AddLast<T>(this IList<T> list, T item)
		{
			T[] array = new T[list.Count + 1];
			list.CopyTo(array, 0);
			array[list.Count] = item;
			return array;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001597C File Offset: 0x00013B7C
		internal static T First<T>(this IEnumerable<T> source)
		{
			IList<T> list = source as IList<T>;
			if (list != null)
			{
				return list[0];
			}
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current;
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000159D8 File Offset: 0x00013BD8
		internal static T Last<T>(this IList<T> list)
		{
			return list[list.Count - 1];
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000159E8 File Offset: 0x00013BE8
		internal static T[] Copy<T>(this T[] array)
		{
			T[] array2 = new T[array.Length];
			Array.Copy(array, array2, array.Length);
			return array2;
		}
	}
}
