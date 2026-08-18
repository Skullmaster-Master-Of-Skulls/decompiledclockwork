using System;
using System.Collections.Specialized;

namespace WebGrease.Css.Extensions
{
	// Token: 0x02000189 RID: 393
	public static class OrderedDictionaryExtensions
	{
		// Token: 0x0600147A RID: 5242 RVA: 0x000782BC File Offset: 0x000764BC
		public static void AppendWithOverride<TItem>(this OrderedDictionary dictionary, TItem item, Func<TItem, object> key)
		{
			if (dictionary != null)
			{
				object key2 = key(item);
				if (dictionary.Contains(key2))
				{
					dictionary.Remove(key2);
				}
				dictionary.Add(key2, item);
			}
		}
	}
}
