using System;
using System.Web.Http.Controllers;

namespace System.Web.Http.ValueProviders
{
	// Token: 0x0200019D RID: 413
	public abstract class ValueProviderFactory
	{
		// Token: 0x06000A78 RID: 2680
		public abstract IValueProvider GetValueProvider(HttpActionContext actionContext);
	}
}
