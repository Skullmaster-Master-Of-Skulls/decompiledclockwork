using System;

namespace System.Web.Mvc
{
	// Token: 0x0200014B RID: 331
	public static class ModelValidatorProviders
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00017936 File Offset: 0x00015B36
		public static ModelValidatorProviderCollection Providers
		{
			get
			{
				return ModelValidatorProviders._providers;
			}
		}

		// Token: 0x0400026B RID: 619
		private static readonly ModelValidatorProviderCollection _providers = new ModelValidatorProviderCollection
		{
			new DataAnnotationsModelValidatorProvider(),
			new DataErrorInfoModelValidatorProvider(),
			new ClientDataTypeModelValidatorProvider()
		};
	}
}
