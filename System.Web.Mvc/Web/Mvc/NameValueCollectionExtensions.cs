using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace System.Web.Mvc
{
	// Token: 0x020001D9 RID: 473
	public static class NameValueCollectionExtensions
	{
		// Token: 0x06000E13 RID: 3603 RVA: 0x0002547C File Offset: 0x0002367C
		public static void CopyTo(this NameValueCollection collection, IDictionary<string, object> destination)
		{
			collection.CopyTo(destination, false);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00025488 File Offset: 0x00023688
		public static void CopyTo(this NameValueCollection collection, IDictionary<string, object> destination, bool replaceEntries)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			foreach (object obj in collection.Keys)
			{
				string text = (string)obj;
				if (replaceEntries || !destination.ContainsKey(text))
				{
					destination[text] = collection[text];
				}
			}
		}
	}
}
