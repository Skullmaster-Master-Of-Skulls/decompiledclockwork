using System;

namespace System.Web.Mvc
{
	// Token: 0x02000118 RID: 280
	public sealed class RouteDataValueProviderFactory : ValueProviderFactory
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x00013D37 File Offset: 0x00011F37
		public override IValueProvider GetValueProvider(ControllerContext controllerContext)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			return new RouteDataValueProvider(controllerContext);
		}
	}
}
