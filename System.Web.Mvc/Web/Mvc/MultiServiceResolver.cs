using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
	// Token: 0x020000CC RID: 204
	internal static class MultiServiceResolver
	{
		// Token: 0x06000550 RID: 1360 RVA: 0x0000ED2C File Offset: 0x0000CF2C
		internal static TService[] GetCombined<TService>(IList<TService> items, IDependencyResolver resolver = null) where TService : class
		{
			if (resolver == null)
			{
				resolver = DependencyResolver.Current;
			}
			IEnumerable<TService> services = resolver.GetServices<TService>();
			return services.Concat(items).ToArray<TService>();
		}
	}
}
