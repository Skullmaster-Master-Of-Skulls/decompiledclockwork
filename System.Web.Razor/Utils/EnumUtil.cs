using System;
using System.Collections.Generic;

namespace System.Web.Razor.Utils
{
	// Token: 0x02000092 RID: 146
	internal static class EnumUtil
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x00017668 File Offset: 0x00015868
		public static IEnumerable<T> Single<T>(T item)
		{
			yield return item;
			yield break;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00017840 File Offset: 0x00015A40
		public static IEnumerable<T> Prepend<T>(T item, IEnumerable<T> enumerable)
		{
			yield return item;
			foreach (T t in enumerable)
			{
				yield return t;
			}
			yield break;
		}
	}
}
