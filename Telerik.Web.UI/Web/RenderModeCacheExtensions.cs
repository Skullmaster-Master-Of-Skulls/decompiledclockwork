using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web
{
	// Token: 0x020001C5 RID: 453
	internal static class RenderModeCacheExtensions
	{
		// Token: 0x06001086 RID: 4230 RVA: 0x0003C55C File Offset: 0x0003A75C
		public static bool ContainsOrInheritsFromType(this SynchronizedCollection<Type> col, Type type)
		{
			bool flag = col.Contains(type);
			bool flag2 = col.Any((Type t) => type.IsSubclassOf(t));
			return flag || flag2;
		}
	}
}
