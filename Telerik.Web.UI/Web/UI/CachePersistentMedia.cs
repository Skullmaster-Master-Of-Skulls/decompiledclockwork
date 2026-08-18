using System;
using System.Web;
using System.Web.Caching;

namespace Telerik.Web.UI
{
	// Token: 0x020016B7 RID: 5815
	internal class CachePersistentMedia : IPersistentMedia
	{
		// Token: 0x0600E061 RID: 57441 RVA: 0x0031E5F8 File Offset: 0x0031C7F8
		public T Get<T>(string key) where T : class
		{
			if (this.CurrentContext != null && this.CurrentContext.Cache != null)
			{
				return this.CurrentContext.Cache.Get(key) as T;
			}
			return default(T);
		}

		// Token: 0x0600E062 RID: 57442 RVA: 0x0031E640 File Offset: 0x0031C840
		public void Add<T>(string key, T item) where T : class
		{
			if (this.CurrentContext != null && this.CurrentContext.Cache != null)
			{
				this.CurrentContext.Cache.Add(key, item, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(2.0), CacheItemPriority.Normal, null);
			}
		}

		// Token: 0x170044C2 RID: 17602
		// (get) Token: 0x0600E063 RID: 57443 RVA: 0x0031E690 File Offset: 0x0031C890
		private HttpContext CurrentContext
		{
			get
			{
				return HttpContext.Current;
			}
		}
	}
}
