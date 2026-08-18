using System;

namespace System.Web.Mvc
{
	// Token: 0x02000134 RID: 308
	public static class ValueProviderFactories
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00015DB8 File Offset: 0x00013FB8
		public static ValueProviderFactoryCollection Factories
		{
			get
			{
				return ValueProviderFactories._factories;
			}
		}

		// Token: 0x0400023C RID: 572
		private static readonly ValueProviderFactoryCollection _factories = new ValueProviderFactoryCollection
		{
			new ChildActionValueProviderFactory(),
			new FormValueProviderFactory(),
			new JsonValueProviderFactory(),
			new RouteDataValueProviderFactory(),
			new QueryStringValueProviderFactory(),
			new HttpFileCollectionValueProviderFactory(),
			new JQueryFormValueProviderFactory()
		};
	}
}
