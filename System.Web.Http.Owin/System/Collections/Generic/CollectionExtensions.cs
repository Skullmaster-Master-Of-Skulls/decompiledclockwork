using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Collections.Generic
{
	// Token: 0x02000002 RID: 2
	internal static class CollectionExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static T[] AppendAndReallocate<T>(this T[] array, T value)
		{
			int num = array.Length;
			T[] array2 = new T[num + 1];
			array.CopyTo(array2, 0);
			array2[num] = value;
			return array2;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020FC File Offset: 0x000002FC
		public static T[] AsArray<T>(this IEnumerable<T> values)
		{
			T[] array = values as T[];
			if (array == null)
			{
				array = values.ToArray<T>();
			}
			return array;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000211C File Offset: 0x0000031C
		public static Collection<T> AsCollection<T>(this IEnumerable<T> enumerable)
		{
			Collection<T> collection = enumerable as Collection<T>;
			if (collection != null)
			{
				return collection;
			}
			IList<T> list = enumerable as IList<T>;
			if (list == null)
			{
				list = new List<T>(enumerable);
			}
			return new Collection<T>(list);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000214C File Offset: 0x0000034C
		public static IList<T> AsIList<T>(this IEnumerable<T> enumerable)
		{
			IList<T> list = enumerable as IList<T>;
			if (list != null)
			{
				return list;
			}
			return new List<T>(enumerable);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000216C File Offset: 0x0000036C
		public static List<T> AsList<T>(this IEnumerable<T> enumerable)
		{
			List<T> list = enumerable as List<T>;
			if (list != null)
			{
				return list;
			}
			ListWrapperCollection<T> listWrapperCollection = enumerable as ListWrapperCollection<T>;
			if (listWrapperCollection != null)
			{
				return listWrapperCollection.ItemsList;
			}
			return new List<T>(enumerable);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000219C File Offset: 0x0000039C
		public static void RemoveFrom<T>(this List<T> list, int start)
		{
			list.RemoveRange(start, list.Count - start);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021B0 File Offset: 0x000003B0
		public static T SingleDefaultOrError<T, TArg1>(this IList<T> list, Action<TArg1> errorAction, TArg1 errorArg1)
		{
			switch (list.Count)
			{
			case 0:
				return default(T);
			case 1:
				return list[0];
			default:
				errorAction(errorArg1);
				return default(T);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021F8 File Offset: 0x000003F8
		public static TMatch SingleOfTypeDefaultOrError<TInput, TMatch, TArg1>(this IList<TInput> list, Action<TArg1> errorAction, TArg1 errorArg1) where TMatch : class
		{
			TMatch tmatch = default(TMatch);
			for (int i = 0; i < list.Count; i++)
			{
				TMatch tmatch2 = list[i] as TMatch;
				if (tmatch2 != null)
				{
					if (tmatch != null)
					{
						errorAction(errorArg1);
						return default(TMatch);
					}
					tmatch = tmatch2;
				}
			}
			return tmatch;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000225C File Offset: 0x0000045C
		public static T[] ToArrayWithoutNulls<T>(this ICollection<T> collection) where T : class
		{
			T[] array = new T[collection.Count];
			int num = 0;
			foreach (T t in collection)
			{
				if (t != null)
				{
					array[num] = t;
					num++;
				}
			}
			if (num == collection.Count)
			{
				return array;
			}
			T[] array2 = new T[num];
			Array.Copy(array, array2, num);
			return array2;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022E0 File Offset: 0x000004E0
		public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this TValue[] array, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(array.Length, comparer);
			foreach (TValue tvalue in array)
			{
				dictionary.Add(keySelector(tvalue), tvalue);
			}
			return dictionary;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000231C File Offset: 0x0000051C
		public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this IList<TValue> list, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			TValue[] array = list as TValue[];
			if (array != null)
			{
				return array.ToDictionaryFast(keySelector, comparer);
			}
			return CollectionExtensions.ToDictionaryFastNoCheck<TKey, TValue>(list, keySelector, comparer);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002344 File Offset: 0x00000544
		public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this IEnumerable<TValue> enumerable, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			TValue[] array = enumerable as TValue[];
			if (array != null)
			{
				return array.ToDictionaryFast(keySelector, comparer);
			}
			IList<TValue> list = enumerable as IList<TValue>;
			if (list != null)
			{
				return CollectionExtensions.ToDictionaryFastNoCheck<TKey, TValue>(list, keySelector, comparer);
			}
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(comparer);
			foreach (TValue tvalue in enumerable)
			{
				dictionary.Add(keySelector(tvalue), tvalue);
			}
			return dictionary;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023C8 File Offset: 0x000005C8
		private static Dictionary<TKey, TValue> ToDictionaryFastNoCheck<TKey, TValue>(IList<TValue> list, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			int count = list.Count;
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(count, comparer);
			for (int i = 0; i < count; i++)
			{
				TValue tvalue = list[i];
				dictionary.Add(keySelector(tvalue), tvalue);
			}
			return dictionary;
		}
	}
}
