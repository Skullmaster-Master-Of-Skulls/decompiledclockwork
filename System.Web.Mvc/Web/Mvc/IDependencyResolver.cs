using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x02000106 RID: 262
	public interface IDependencyResolver
	{
		// Token: 0x0600071F RID: 1823
		object GetService(Type serviceType);

		// Token: 0x06000720 RID: 1824
		IEnumerable<object> GetServices(Type serviceType);
	}
}
