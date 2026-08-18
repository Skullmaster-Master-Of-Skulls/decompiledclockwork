using System;
using System.Configuration.Provider;

namespace System.Web.Caching
{
	// Token: 0x02000889 RID: 2185
	public abstract class OutputCacheProvider : ProviderBase
	{
		// Token: 0x060066D5 RID: 26325
		public abstract object Get(string key);

		// Token: 0x060066D6 RID: 26326
		public abstract object Add(string key, object entry, DateTime utcExpiry);

		// Token: 0x060066D7 RID: 26327
		public abstract void Set(string key, object entry, DateTime utcExpiry);

		// Token: 0x060066D8 RID: 26328
		public abstract void Remove(string key);
	}
}
