using System;
using System.Threading.Tasks;

namespace System.Web.Caching
{
	// Token: 0x0200088A RID: 2186
	public abstract class OutputCacheProviderAsync : OutputCacheProvider
	{
		// Token: 0x060066DA RID: 26330
		public abstract Task<object> GetAsync(string key);

		// Token: 0x060066DB RID: 26331
		public abstract Task<object> AddAsync(string key, object entry, DateTime utcExpiry);

		// Token: 0x060066DC RID: 26332
		public abstract Task SetAsync(string key, object entry, DateTime utcExpiry);

		// Token: 0x060066DD RID: 26333
		public abstract Task RemoveAsync(string key);
	}
}
