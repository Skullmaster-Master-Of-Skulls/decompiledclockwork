using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x02000137 RID: 311
	internal static class ValueProviderUtil
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x000161F4 File Offset: 0x000143F4
		public static bool CollectionContainsPrefix(IEnumerable<string> collection, string prefix)
		{
			foreach (string text in collection)
			{
				if (text != null)
				{
					if (prefix.Length == 0)
					{
						return true;
					}
					if (text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
					{
						if (text.Length == prefix.Length)
						{
							return true;
						}
						char c = text[prefix.Length];
						if (c == '.' || c == '[')
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}
}
