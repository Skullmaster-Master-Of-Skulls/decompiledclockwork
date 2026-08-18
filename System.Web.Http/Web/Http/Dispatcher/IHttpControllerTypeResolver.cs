using System;
using System.Collections.Generic;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x020000A8 RID: 168
	public interface IHttpControllerTypeResolver
	{
		// Token: 0x060003EE RID: 1006
		ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver);
	}
}
