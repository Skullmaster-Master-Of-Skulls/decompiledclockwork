using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Collections.Generic
{
	// Token: 0x02000003 RID: 3
	internal static class CollectionExtensions
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002228 File Offset: 0x00000428
		public static T[] AppendAndReallocate<T>(this T[] array, T value)
		{
			int num = array.Length;
			T[] array2 = new T[num + 1];
			array.CopyTo(array2, 0);
			array2[num] = value;
			return array2;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002254 File Offset: 0x00000454
		public static T[] AsArray<T>(this IEnumerable<T> values)
		{
			T[] array = values as T[];
			if (array == null)
			{
				array = values.ToArray<T>();
			}
			return array;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002274 File Offset: 0x00000474
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

		// Token: 0x06000011 RID: 17 RVA: 0x000022A4 File Offset: 0x000004A4
		public static IList<T> AsIList<T>(this IEnumerable<T> enumerable)
		{
			IList<T> list = enumerable as IList<T>;
			if (list != null)
			{
				return list;
			}
			return new List<T>(enumerable);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022C4 File Offset: 0x000004C4
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

		// Token: 0x06000013 RID: 19 RVA: 0x000022F4 File Offset: 0x000004F4
		public static void RemoveFrom<T>(this List<T> list, int start)
		{
			list.RemoveRange(start, list.Count - start);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002308 File Offset: 0x00000508
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

		// Token: 0x06000015 RID: 21 RVA: 0x00002350 File Offset: 0x00000550
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

		// Token: 0x06000016 RID: 22 RVA: 0x000023B4 File Offset: 0x000005B4
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

		// Token: 0x06000017 RID: 23 RVA: 0x00002438 File Offset: 0x00000638
		public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this TValue[] array, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(array.Length, comparer);
			foreach (TValue tvalue in array)
			{
				dictionary.Add(keySelector(tvalue), tvalue);
			}
			return dictionary;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002474 File Offset: 0x00000674
		public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this IList<TValue> list, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			TValue[] array = list as TValue[];
			if (array != null)
			{
				return array.ToDictionaryFast(keySelector, comparer);
			}
			return CollectionExtensions.ToDictionaryFastNoCheck<TKey, TValue>(list, keySelector, comparer);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000249C File Offset: 0x0000069C
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

		// Token: 0x0600001A RID: 26 RVA: 0x00002520 File Offset: 0x00000720
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
