using System;

namespace System.Web.Mvc
{
	// Token: 0x0200003B RID: 59
	public abstract class ValueProviderFactory
	{
		// Token: 0x06000121 RID: 289
		public abstract IValueProvider GetValueProvider(ControllerContext controllerContext);
	}
}
