using System;
using System.Collections;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007EF RID: 2031
	internal sealed class NameCache
	{
		// Token: 0x060047BD RID: 18365 RVA: 0x000F5FB0 File Offset: 0x000F4FB0
		internal object GetCachedValue(string name)
		{
			this.name = name;
			return NameCache.ht[name];
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x000F5FC4 File Offset: 0x000F4FC4
		internal void SetCachedValue(object value)
		{
			NameCache.ht[this.name] = value;
		}

		// Token: 0x0400248D RID: 9357
		private static Hashtable ht = new Hashtable();

		// Token: 0x0400248E RID: 9358
		private string name;
	}
}
