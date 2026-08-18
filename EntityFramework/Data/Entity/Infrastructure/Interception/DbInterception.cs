using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200017F RID: 383
	public static class DbInterception
	{
		// Token: 0x06000D17 RID: 3351 RVA: 0x0003BC65 File Offset: 0x00039E65
		public static void Add(IDbInterceptor interceptor)
		{
			Check.NotNull<IDbInterceptor>(interceptor, "interceptor");
			DbInterception._dispatchers.Value.AddInterceptor(interceptor);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0003BC83 File Offset: 0x00039E83
		public static void Remove(IDbInterceptor interceptor)
		{
			Check.NotNull<IDbInterceptor>(interceptor, "interceptor");
			DbInterception._dispatchers.Value.RemoveInterceptor(interceptor);
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0003BCA1 File Offset: 0x00039EA1
		public static DbDispatchers Dispatch
		{
			get
			{
				return DbInterception._dispatchers.Value;
			}
		}

		// Token: 0x0400038D RID: 909
		private static readonly Lazy<DbDispatchers> _dispatchers = new Lazy<DbDispatchers>(() => new DbDispatchers());
	}
}
