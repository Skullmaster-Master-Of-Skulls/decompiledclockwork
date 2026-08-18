using System;

namespace System.Web.Mvc
{
	// Token: 0x02000085 RID: 133
	public sealed class ChildActionValueProviderFactory : ValueProviderFactory
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x0000BBD5 File Offset: 0x00009DD5
		public override IValueProvider GetValueProvider(ControllerContext controllerContext)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			return new ChildActionValueProvider(controllerContext);
		}
	}
}
