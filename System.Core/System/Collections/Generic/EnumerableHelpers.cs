using System;
using System.Linq;

namespace System.Collections.Generic
{
	// Token: 0x0200009D RID: 157
	internal static class EnumerableHelpers
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		internal static bool TryGetCount<T>(IEnumerable<T> source, out int count)
		{
			ICollection<T> collection = source as ICollection<T>;
			if (collection != null)
			{
				count = collection.Count;
				return true;
			}
			IIListProvider<T> iilistProvider = source as IIListProvider<T>;
			if (iilistProvider != null)
			{
				count = iilistProvider.GetCount(true);
				return count >= 0;
			}
			count = -1;
			return false;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000C204 File Offset: 0x0000A404
		internal static void Copy<T>(IEnumerable<T> source, T[] array, int arrayIndex, int count)
		{
			ICollection<T> collection = source as ICollection<T>;
			if (collection != null)
			{
				collection.CopyTo(array, arrayIndex);
				return;
			}
			EnumerableHelpers.IterativeCopy<T>(source, array, arrayIndex, count);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000C230 File Offset: 0x0000A430
		internal static void IterativeCopy<T>(IEnumerable<T> source, T[] array, int arrayIndex, int count)
		{
			int num = arrayIndex + count;
			foreach (T t in source)
			{
				array[arrayIndex++] = t;
			}
		}
	}
}
