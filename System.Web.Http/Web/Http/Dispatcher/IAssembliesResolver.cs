using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x020000B2 RID: 178
	public interface IAssembliesResolver
	{
		// Token: 0x0600040C RID: 1036
		ICollection<Assembly> GetAssemblies();
	}
}
