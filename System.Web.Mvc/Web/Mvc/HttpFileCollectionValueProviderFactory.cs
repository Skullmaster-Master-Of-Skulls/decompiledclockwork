using System;

namespace System.Web.Mvc
{
	// Token: 0x02000111 RID: 273
	public sealed class HttpFileCollectionValueProviderFactory : ValueProviderFactory
	{
		// Token: 0x06000750 RID: 1872 RVA: 0x00013B99 File Offset: 0x00011D99
		public override IValueProvider GetValueProvider(ControllerContext controllerContext)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			return new HttpFileCollectionValueProvider(controllerContext);
		}
	}
}
