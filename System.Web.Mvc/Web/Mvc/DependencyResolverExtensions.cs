using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
	// Token: 0x020000A5 RID: 165
	public static class DependencyResolverExtensions
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x0000D0F0 File Offset: 0x0000B2F0
		public static TService GetService<TService>(this IDependencyResolver resolver)
		{
			return (TService)((object)resolver.GetService(typeof(TService)));
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000D107 File Offset: 0x0000B307
		public static IEnumerable<TService> GetServices<TService>(this IDependencyResolver resolver)
		{
			return resolver.GetServices(typeof(TService)).Cast<TService>();
		}
	}
}
