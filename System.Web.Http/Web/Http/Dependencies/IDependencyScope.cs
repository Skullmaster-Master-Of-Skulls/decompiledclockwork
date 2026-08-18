using System;
using System.Collections.Generic;

namespace System.Web.Http.Dependencies
{
	// Token: 0x020000B1 RID: 177
	public interface IDependencyScope : IDisposable
	{
		// Token: 0x0600040A RID: 1034
		object GetService(Type serviceType);

		// Token: 0x0600040B RID: 1035
		IEnumerable<object> GetServices(Type serviceType);
	}
}
