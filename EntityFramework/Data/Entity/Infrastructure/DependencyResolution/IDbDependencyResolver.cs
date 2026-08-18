using System;
using System.Collections.Generic;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000144 RID: 324
	public interface IDbDependencyResolver
	{
		// Token: 0x06000AB4 RID: 2740
		object GetService(Type type, object key);

		// Token: 0x06000AB5 RID: 2741
		IEnumerable<object> GetServices(Type type, object key);
	}
}
