using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.Data
{
	// Token: 0x02001B86 RID: 7046
	internal static class CollectionExtensions
	{
		// Token: 0x06011130 RID: 69936 RVA: 0x003C3D13 File Offset: 0x003C1F13
		public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			items.ToList<T>().ForEach(new Action<T>(collection.Add));
		}
	}
}
