using System;
using System.Web.Caching;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x02000196 RID: 406
	public class DefaultViewLocationCache : IViewLocationCache
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x0001E4F4 File Offset: 0x0001C6F4
		public DefaultViewLocationCache() : this(DefaultViewLocationCache._defaultTimeSpan)
		{
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0001E501 File Offset: 0x0001C701
		public DefaultViewLocationCache(TimeSpan timeSpan)
		{
			if (timeSpan.Ticks < 0L)
			{
				throw new InvalidOperationException(MvcResources.DefaultViewLocationCache_NegativeTimeSpan);
			}
			this.TimeSpan = timeSpan;
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0001E526 File Offset: 0x0001C726
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x0001E52E File Offset: 0x0001C72E
		public TimeSpan TimeSpan { get; private set; }

		// Token: 0x06000B84 RID: 2948 RVA: 0x0001E537 File Offset: 0x0001C737
		public string GetViewLocation(HttpContextBase httpContext, string key)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			return (string)httpContext.Cache[key];
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0001E558 File Offset: 0x0001C758
		public void InsertViewLocation(HttpContextBase httpContext, string key, string virtualPath)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			httpContext.Cache.Insert(key, virtualPath, null, Cache.NoAbsoluteExpiration, this.TimeSpan);
		}

		// Token: 0x0400030D RID: 781
		private static readonly TimeSpan _defaultTimeSpan = new TimeSpan(0, 15, 0);

		// Token: 0x0400030E RID: 782
		public static readonly IViewLocationCache Null = new NullViewLocationCache();
	}
}
